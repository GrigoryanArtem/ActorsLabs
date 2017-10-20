/*
Copyright 2017 Grigoryan Artem
Licensed under the Apache License, Version 2.0
*/

using Akka.Actor;
using System;
using Task3.Model;

namespace Task3.Actors
{
    public class BuyerActor : ReceiveActor, ILogReceive
    {
        private Buyer mBuyer;
        private ICancelable mSchedulerCancelable;

        public BuyerActor(Deal expectedDeal)
        {
            mBuyer = new Buyer(expectedDeal);

            FindSeller();
        }

        public static Props Create(Deal expectedDeal)
        {
            return Props.Create<BuyerActor>(expectedDeal);
        }

        #region Private methods

        private void FindSeller()
        {
            StartScheduler();

            Receive<Deal>(d =>
            {
                if (IsDealFits(d))
                {
                    StopScheduler();
                    Become(Buyed);

                    Sender.Tell(new SellerActor.DealAccept());
                }
                else
                {
                    Sender.Tell(new SellerActor.DealDenied());
                }
            });
        }

        private void Buyed()
        {
            Receive<Deal>(d =>
            {
                Sender.Tell(new SellerActor.DealDenied());
            });
        }

        private void StartScheduler()
        {
            mSchedulerCancelable = Context.System
                .Scheduler
                .ScheduleTellRepeatedlyCancelable(TimeSpan.FromSeconds(5),
                    TimeSpan.FromSeconds(10),
                    Context.ActorSelection(@"akka://mainActorSystem/user/sellerBuilder/*"),
                    mBuyer.ExpectedDeal, Self);
        }

        private void StopScheduler()
        {
            mSchedulerCancelable.Cancel();
        }

        private bool IsDealFits(Deal deal)
        {
            return deal.Cost <= mBuyer.ExpectedDeal.Cost && deal.Mileage <= mBuyer.ExpectedDeal.Mileage;
        }

        #endregion
    }
}

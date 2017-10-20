/*
Copyright 2017 Grigoryan Artem
Licensed under the Apache License, Version 2.0
*/

using Akka.Actor;
using Akka.Event;
using Task3.Model;

namespace Task3.Actors
{
    public class SellerActor : ReceiveActor, ILogReceive
    {
        private Seller mSeller;

        #region Messages

        public class DealAccept { }

        public class DealDenied { }

        #endregion

        public SellerActor(Deal deal)
        {
            mSeller = new Seller(deal);

            WaitBuyer();
        }

        #region Private methods

        private void WaitBuyer()
        {
            Receive<Deal>(d =>
            {
                Logging.GetLogger(Context).Info($"{Sender.Path.Name} send deal query ( Mileage: {d.Mileage} | Cost: {d.Cost} ) to {Self.Path.Name}.");

                if (IsDealFits(d))
                {
                    Sender.Tell(mSeller.Deal);
                    Become(WaitConfirmation);

                    Logging.GetLogger(Context).Info($"{Self.Path.Name} accept {Sender.Path.Name} deal.");
                }
            });
        }

        private void WaitConfirmation()
        {
            Receive<DealAccept>(d =>
            {
                string message = $"The {Sender.Path.Name} made a deal with the {Self.Path.Name}.";
                Logging.GetLogger(Context).Info(message);
                Context.ActorSelection(@"akka://mainActorSystem/user/consoleWriter")
                        .Tell(message);

                Become(Selled);
            });

            Receive<DealDenied>(d =>
            {
                Logging.GetLogger(Context).Info($"{Sender.Path.Name} denied deal.");

                UnbecomeStacked();
            });
        }

        private void Selled() { }

        public static Props Create(Deal deal)
        {
            return Props.Create<SellerActor>(deal);
        }

        private bool IsDealFits(Deal deal)
        {
            return deal.Cost >= mSeller.Deal.Cost && deal.Mileage >= mSeller.Deal.Mileage;
        }

        #endregion
    }
}

/*
Copyright 2017 Grigoryan Artem
Licensed under the Apache License, Version 2.0
*/

using Akka.Actor;
using Task3.Model;

namespace Task3.Actors
{
    public class BuyerBuilderActor : ReceiveActor, ILogReceive
    {
        private int mCounter = 0;

        #region Messages

        public class CreateBuyerMessage
        {
            public Deal Deal { get; private set; }

            public CreateBuyerMessage(Deal deal)
            {
                Deal = deal;
            }
        }

        #endregion

        public BuyerBuilderActor()
        {
            Receive<CreateBuyerMessage>(cbm => 
            {
                Context.ActorOf(BuyerActor.Create(cbm.Deal), $"buyer@{mCounter++}");
            });
        }

        public static Props Create()
        {
            return Props.Create<BuyerBuilderActor>();
        }
    }
}

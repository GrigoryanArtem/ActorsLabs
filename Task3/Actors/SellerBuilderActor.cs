/*
Copyright 2017 Grigoryan Artem
Licensed under the Apache License, Version 2.0
*/

using Akka.Actor;
using Task3.Model;

namespace Task3.Actors
{
    public class SellerBuilderActor : ReceiveActor, ILogReceive
    {
        private int mCounter = 0;

        #region Messages

        public class CreateSellerMessage
        {
            public Deal Deal { get; private set; }

            public CreateSellerMessage(Deal deal)
            {
                Deal = deal;
            }
        }

        #endregion

        public SellerBuilderActor()
        {
            Receive<CreateSellerMessage>(csm =>
            {
                Context.ActorOf(SellerActor.Create(csm.Deal), $"seller@{mCounter++}");
            });
        }

        public static Props Create()
        {
            return Props.Create<SellerBuilderActor>();
        }
    }
}

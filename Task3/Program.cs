/*
Copyright 2017 Grigoryan Artem
Licensed under the Apache License, Version 2.0
*/

using Akka.Actor;
using Task3.Actors;
using Task3.Model;

namespace Task3
{
    class Program
    {
        static void Main(string[] args)
        {
            ActorSystem actorSystem = ActorSystem.Create("mainActorSystem");
            
            IActorRef buyerBuilder = actorSystem.ActorOf(BuyerBuilderActor.Create(), "buyerBuilder");
            IActorRef sellerBuilder = actorSystem.ActorOf(SellerBuilderActor.Create(), "sellerBuilder");

            buyerBuilder.Tell(new BuyerBuilderActor.CreateBuyerMessage(new Deal(800, 5000)));
            buyerBuilder.Tell(new BuyerBuilderActor.CreateBuyerMessage(new Deal(900, 4500)));
            buyerBuilder.Tell(new BuyerBuilderActor.CreateBuyerMessage(new Deal(1000, 4200)));
            buyerBuilder.Tell(new BuyerBuilderActor.CreateBuyerMessage(new Deal(700, 4900)));
            buyerBuilder.Tell(new BuyerBuilderActor.CreateBuyerMessage(new Deal(1500, 5200)));

            sellerBuilder.Tell(new SellerBuilderActor.CreateSellerMessage(new Deal(1000, 4900)));
            sellerBuilder.Tell(new SellerBuilderActor.CreateSellerMessage(new Deal(650, 5000)));
            sellerBuilder.Tell(new SellerBuilderActor.CreateSellerMessage(new Deal(1000, 4750)));
            sellerBuilder.Tell(new SellerBuilderActor.CreateSellerMessage(new Deal(900, 4600)));
            sellerBuilder.Tell(new SellerBuilderActor.CreateSellerMessage(new Deal(750, 4000)));

            actorSystem.WhenTerminated.Wait();
        }
    }
}

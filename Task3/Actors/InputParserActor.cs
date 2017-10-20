/*
Copyright 2017 Grigoryan Artem
Licensed under the Apache License, Version 2.0
*/

using Akka.Actor;
using Newtonsoft.Json;
using System.IO;
using Task3.Model;

namespace Task3.Actors
{
    public class InputParserActor : ReceiveActor, ILogReceive
    {
        #region Json model

        private class Data
        {
            public Deals Deals { get;set; }
        }

        private class Deals
        {
            public Deal[] SellersDeals { get; set; }
            public Deal[] BuyersDeals { get; set; }
        }

        #endregion

        public InputParserActor()
        {
            Receive<string>(json => {
                Data data = JsonConvert.DeserializeObject<Data>(json);
                Deals deals = data.Deals;

                foreach (var sellerDeal in deals.SellersDeals)
                    Context.ActorSelection(@"akka://mainActorSystem/user/sellerBuilder").Tell(new SellerBuilderActor.CreateSellerMessage(sellerDeal));

                foreach (var buyersDeal in deals.BuyersDeals)
                    Context.ActorSelection(@"akka://mainActorSystem/user/buyerBuilder").Tell(new BuyerBuilderActor.CreateBuyerMessage(buyersDeal));
            });
        }

        public static Props Create()
        {
            return Props.Create<InputParserActor>();
        }
    }
}

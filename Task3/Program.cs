/*
Copyright 2017 Grigoryan Artem
Licensed under the Apache License, Version 2.0
*/

using Akka.Actor;
using Task3.Actors;

namespace Task3
{
    class Program
    {
        static void Main(string[] args)
        {
            ActorSystem actorSystem = ActorSystem.Create("mainActorSystem");
            
            actorSystem.ActorOf(BuyerBuilderActor.Create(), "buyerBuilder");
            actorSystem.ActorOf(SellerBuilderActor.Create(), "sellerBuilder");
            actorSystem.ActorOf(ConsoleWriterActor.Create(), "consoleWriter");
            actorSystem.ActorOf(InputParserActor.Create(), "inputParser");

            IActorRef consoleReader = actorSystem.ActorOf(ConsoleReaderActor.Create(), "consoleReader");

            consoleReader.Tell(ConsoleReaderActor.ReadCommand);

            actorSystem.WhenTerminated.Wait();
        }
    }
}

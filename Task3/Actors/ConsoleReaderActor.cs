/*
Copyright 2017 Grigoryan Artem
Licensed under the Apache License, Version 2.0
*/

using Akka.Actor;
using System;

namespace Task3.Actors
{
    public class ConsoleReaderActor : ReceiveActor, ILogReceive
    {
        private IActorRef mFileValidationActor;

        #region Commands

        public static string ReadCommand { get; } = "read";

        public static string CloseCommand { get; } = "close";

        #endregion

        protected override void PreStart()
        {
            mFileValidationActor = Context.ActorOf(FileValidationActor.Create(), "fileValidation");
            Context.ActorSelection(@"akka://mainActorSystem/user/consoleWriter")
                .Tell(AssemblyInfo.Copyright);

            base.PreStart();
        }

        public ConsoleReaderActor()
        {
            Receive<string>(s => String.Equals(s, ReadCommand, StringComparison.OrdinalIgnoreCase),
                    s => {
                    string path = Console.ReadLine();

                    mFileValidationActor.Tell(path);
                });

            Receive<string>(s => String.Equals(s, CloseCommand, StringComparison.OrdinalIgnoreCase),
                s => {
                    Context.System.Terminate();
                });
        }

        public static Props Create()
        {
            return Props.Create<ConsoleReaderActor>();
        }
    }
}

/*
Copyright 2017 Grigoryan Artem
Licensed under the Apache License, Version 2.0
*/

using Akka.Actor;
using System;
using System.IO;
using Task3.Actors.Model;

namespace Task3.Actors
{
    public class FileValidationActor : ReceiveActor, ILogReceive
    {
        public FileValidationActor()
        {
            Receive<string>(path => {
                if (File.Exists(path))
                {
                    try
                    {
                        string json = File.ReadAllText(path);
                        Context.ActorSelection(@"akka://mainActorSystem/user/inputParser").Tell(json);
                        Context.ActorSelection(@"akka://mainActorSystem/user/consoleWriter")
                        .Tell(new Messages.InputSuccess("File opened."));
                    }
                    catch(Exception exp)
                    {
                        HandleError(exp.Message);
                    }
                }
                else
                {
                    HandleError("File is not exist.");
                }
            });
        }

        private void HandleError(string message)
        {
            Context.ActorSelection(@"akka://mainActorSystem/user/consoleWriter")
                        .Tell(new Messages.ValidationError(message));
            Sender.Tell(ConsoleReaderActor.ReadCommand);
        }

        public static Props Create()
        {
            return Props.Create<FileValidationActor>();
        }
    }
}

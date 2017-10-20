/*
Copyright 2017 Grigoryan Artem
Licensed under the Apache License, Version 2.0
*/

using Akka.Actor;
using System;
using Task3.Actors.Model;

namespace Task3.Actors
{
    public class ConsoleWriterActor : ReceiveActor, ILogReceive
    {
        public ConsoleWriterActor()
        {
            Receive<string>(s => Console.WriteLine(s));
            Receive<Messages.InputError>(ipr => ColorPrint(ipr.Reason, ConsoleColor.Red));
            Receive<Messages.InputSuccess>(ips => ColorPrint(ips.Reason, ConsoleColor.Green));
        }

        public static Props Create()
        {
            return Props.Create<ConsoleWriterActor>();
        }

        #region private methods

        private void ColorPrint(string message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        #endregion
    }
}

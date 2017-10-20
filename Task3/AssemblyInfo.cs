/*
Copyright 2017 Grigoryan Artem
Licensed under the Apache License, Version 2.0
*/

using System;
using System.Linq;
using System.Reflection;

namespace Task3
{
    public static class AssemblyInfo
    {
        public static string Copyright
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);

                if (!attributes.Any())
                    return String.Empty;

                return ((AssemblyCopyrightAttribute)attributes.First()).Copyright;
            }

        }
    }
}

/*
Copyright 2017 Grigoryan Artem
Licensed under the Apache License, Version 2.0
*/

namespace Task3.Model
{
    public class Buyer
    {
        public Deal ExpectedDeal { get; private set; }

        public Buyer(Deal expectedDeal)
        {
            ExpectedDeal = expectedDeal;
        }
    }
}

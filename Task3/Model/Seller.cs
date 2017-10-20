/*
Copyright 2017 Grigoryan Artem
Licensed under the Apache License, Version 2.0
*/

namespace Task3.Model
{
    public class Seller
    {
        public Deal Deal { get; private set; }

        public Seller(Deal deal)
        {
            Deal = deal;
        }
    }
}

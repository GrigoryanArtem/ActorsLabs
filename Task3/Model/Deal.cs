/*
Copyright 2017 Grigoryan Artem
Licensed under the Apache License, Version 2.0
*/

namespace Task3.Model
{
    public class Deal
    {
        #region Properties

        public int Mileage { get; private set; }

        public double Cost { get; private set; }

        #endregion

        public Deal(int mileage, double cost)
        {
            Mileage = mileage;
            Cost = cost;
        }
    }
}

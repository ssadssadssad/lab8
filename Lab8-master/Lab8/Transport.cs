using System;
using System.Collections.Generic;
using System.Text;

namespace Lab8
{
    public class Transport
    {
        protected int wheels;
        protected int Wheels
        {
            get => wheels;
            set => wheels = value;
        }
        protected int weight;
        protected int Weight
        {
            get => weight;
            set => weight = value;
        }
        protected double length;
        protected double Length
        {
            get => length;
            set => length = value;
        }
        public Transport(int wheels, int weight, double length)
        {
            this.wheels = wheels;
            this.weight = weight;
            this.length = length;
        }
    }
}

using System;

namespace Telemetrador
{
    public class Class1
    {
        public int c;

        public void AddValues(int a, int b)
        {
            c = a + b;
        }

        public static int GenerateRandom(int min, int max)
        {
            System.Random rand = new System.Random();
            return rand.Next(min, max);
        }
    }
}
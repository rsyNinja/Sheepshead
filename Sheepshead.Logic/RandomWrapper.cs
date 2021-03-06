﻿using System;

namespace Sheepshead.Logic
{
    public interface IRandomWrapper
    {
        int Next();
        int Next(int maxValue);
        int Next(int minValue, int maxValue);
    }

    public class RandomWrapper : IRandomWrapper
    {
        public Random _rnd;

        public RandomWrapper()
        {
            _rnd = new Random();
        }

        public int Next()
        {
            return _rnd.Next();
        }

        public int Next(int maxValue)
        {
            return _rnd.Next(maxValue);
        }

        public int Next(int minValue, int maxValue)
        {
            return _rnd.Next(minValue, maxValue);
        }
    }
}
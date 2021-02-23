using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZobristLib
{
    public class RandomUlong
    {
        public RandomUlong(int seed)
        {
            _random = new Random(seed);
        }


        public RandomUlong()
        {
            _random = new Random();
        }

        public ulong Next()
        {
            // Create a 31 bit random number using Next()
            // ulong is a 64 bit unsigned number
            ulong longRand = (ulong)_random.Next();

            // Left shift that number two positions
            // Or the result with the last two bits of another random number
            // This gives me a 33 bit random number.
            longRand = (longRand << 2) | ((ulong)_random.Next() & 0x3);

            // Shift that left 31 positions to make room for the next random number.
            longRand = longRand << 31 | (ulong)_random.Next();

            return longRand;
        }

        Random _random;
    }
}

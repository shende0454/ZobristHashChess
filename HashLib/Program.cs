using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HashLib;
namespace FindCommonPositions
{
    class Program
    {
        // Using the hash table and zorbrist hash find the 
        // number of times the most common position occurs
        // after 1 ply, 5 ply, 10 ply and 15 ply.
        // (a ply is one piece moving)
        //  Note - you don't have to provide the actual position, although it
        //  would be interesting if you did.  I only require the count for the
        //  most commonly occuring position.


        // You might use 4 hash tables (one for each number of ply) or
        // you might have the program run multiple times, once for each
        // number of ply.

        // Output for the program should look something like this:
        //  1 ply - most common position occurs w number of times.
        //  5 ply - most common position occurs x number of times.
        //  10 ply - most common position occurs y number of times.
        //  15 ply - most common position occurs z number of times.

        static void Main(string[] args)
        {
            Console.WriteLine("heyy");
            string line = Console.ReadLine();
        }
    }
}

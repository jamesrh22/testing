using System;
using System.IO;
using System.Collections.Generic;
using static testing.MovieOrdering;
using static testing.FoodOrdering;
using static testing.ManagingMovies;


namespace testing
{
    public class Program
    {

        public static void Main()
        {
            Console.WriteLine("Would you like to order (F)ood?");
            Console.WriteLine("Would you like to order a (M)ovie?");
            Console.WriteLine("Are you a (U)ser of management updating information?");
            
            ConsoleKey inputKey;

            inputKey = Console.ReadKey().Key;
            switch (inputKey)
            {
                case ConsoleKey.F:
                    OrderFoods();
                    break;
                case ConsoleKey.M:
                    OrderMovies();
                    break;
                case ConsoleKey.U:
                    ChangingMovies();
                    break;
                case ConsoleKey.R:
                    //toMain;
                    break;
            }
            }
        }
}
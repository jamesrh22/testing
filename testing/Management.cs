using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using static testing.MovieOrdering;

namespace testing
{
    public class Management
    {
        public Movie movie;
        public MovieOrdering movieOrdering;
    }

    public class ManagingMovies
    {
   
        public static void ChangingMovies()
        {
            List<Movie> movies = new List<Movie>();  //Introducing the list of movies
            //Movie movie = new Movie();
            string input;//used for writing from text file
            int ratingInput;//used to identify rating with enum value
            int selection;
            string inputchange;
            string path = "C:\\Users\\james\\source\\repos\\moviehw4\\moviehw4\\movies.txt";
            if (File.Exists(path))
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    while (!sr.EndOfStream)
                    {
                        Movie movie = new Movie();

                        movie.title = sr.ReadLine();

                        input = sr.ReadLine();
                        movie.price = Convert.ToDouble(input);

                        input = sr.ReadLine();
                        ratingInput = Convert.ToInt32(input);
                        movie.rating = (Rating)ratingInput;

                        movie.time0 = sr.ReadLine();
                        movie.time1 = sr.ReadLine();
                        movie.time2 = sr.ReadLine();
                        movie.time3 = sr.ReadLine();

                        input = sr.ReadLine();
                        movie.screennum = Convert.ToInt32(input);

                        movies.Add(movie);
                    }
                    sr.Close();
                }
            }

            Updating(movies);

        }

        public static void ShowMovies(List<Movie> movies)//Method to show movies from text file
        {
            int counter = 0;
            Console.WriteLine("{0}", movies[0].title);
            foreach (Movie movie in movies)
            {
                switch (counter)
                {
                    case 0:
                        counter++;
                        continue;
                    default:
                        Console.WriteLine("{0}. {1}", counter, movie.title);
                        Console.WriteLine("Rating: {0}, Price: {1:C}", movie.rating, movie.price);
                        counter++;
                        continue;
                }
            }
        }

        public static void Updating(List<Movie> movies)
        {
            ShowMovies(movies);
            string input;
            int ratingInput;
            string path2 = "C:\\Users\\james\\source\\repos\\testing\\testing\\movies.txt";
            if (File.Exists(path2))
            {
                using (StreamReader sr2 = new StreamReader(path2))
                {
                    while (!sr2.EndOfStream)
                    {
                        Movie movie = new Movie();

                        movie.title = sr2.ReadLine();

                        input = sr2.ReadLine();
                        movie.price = Convert.ToDouble(input);

                        input = sr2.ReadLine();
                        ratingInput = Convert.ToInt32(input);
                        movie.rating = (Rating)ratingInput;

                        movie.time0 = sr2.ReadLine();
                        movie.time1 = sr2.ReadLine();
                        movie.time2 = sr2.ReadLine();
                        movie.time3 = sr2.ReadLine();

                        input = sr2.ReadLine();
                        movie.screennum = Convert.ToInt32(input);

                        movies.Add(movie);
                    }
                    sr2.Close();
                }
            }
            Console.WriteLine("Which movie would you like to update?");
            int selection;
            string inputchange;
            selection = ValidEntry(1, movies.Count - 1);

            Console.WriteLine("Which element of {0} would you like to change?", movies[selection].title);
            foreach (Movie movie in movies)
            {
                ConsoleKey inputKey;

                inputKey = Console.ReadKey().Key;
                switch (inputKey)
                {
                    case ConsoleKey.T:
                        Console.WriteLine("Please enter the new movie title:");
                        inputchange = movie.title;
                        File.WriteAllText(path2, inputchange);
                        continue;
                    case ConsoleKey.R:
                        continue;
                    case ConsoleKey.P:
                        continue;
                }
            }
        }

        static Boolean ValidValue(int min, int max, int selection)
        {
            Boolean validInput;
            if (selection < min || selection > max)
            {
                Console.WriteLine("Please enter a numerical value between {0} and {1}", min, max);
                validInput = false;
            }
            else
            {
                validInput = true;
            }
            return validInput;
        }

        static int ValidEntry(int min, int max)
        {
            Boolean valid;
            int value = -1;
            do
            {
                string response = Console.ReadLine();
                try  //Confirms an integer is entered.
                {
                    value = Convert.ToInt32(response);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine("Please enter a numerical value");
                    valid = false;
                    continue;
                }
                finally
                {
                    valid = ValidValue(min, max, value);
                }
            } while (!valid);
            return value;
        }
    }
}

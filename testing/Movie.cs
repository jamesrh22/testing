using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using static testing.FoodOrdering;
using static testing.OrderFood;
using System.ComponentModel.Design;
using System.Reflection.Metadata.Ecma335;

namespace testing
{
    public enum Rating  //Enumerated list of movie ratings
    {
        G,
        PG,
        PG13,
        R
    }
    public class Movie
    {
        public string title;
        public double price;
        public Rating rating;
        public string time0;
        public string time1;
        public string time2;
        public string time3;
        public int screennum;

        public override string ToString()
        {
            string s = "Movie:  " + title + "\n\tprice:  " + price + "\n\t\trating:  " + rating;
            return s;
        }
    }

    public struct OrderMovie//Structure to store movie selection, rating, price, time, tickets and cost information
    {
        public string mtitle;//Movie title
        public Rating rating1;//Enumerated list value for the movie rating
        public double price;//Price per movie
        public string time;//Showing time selected
        public int ticketsqty;//Amount of tickets ordered
        public int qty;//Used to calculate cost
        public double cost;//The cost for each movie
        public int screennumb;//Screen movie is showing on
        public string username1;//Order name
        public double ordernumber;//Order number

        public OrderMovie(string mt, Rating rtg, double pr, string tm, int tq, int q, double c, int snn, string usn, double ordn)//Constructor for order struct
        {
            mtitle = mt;
            rating1 = rtg;
            price = pr;
            time = tm;
            ticketsqty = tq;
            qty = q;
            cost = c;
            screennumb = snn;
            username1 = usn;
            ordernumber = ordn;
        }

        public string ToString(Movie t)//Converting values into a printable format.
        {
            string s;
            s = "Movie:  " + mtitle + "\tRating:  " + rating1 + "\tTickets:  " + ticketsqty + "\tTime: " + time;
            s += "\n\tScreen: " + screennumb + "\tMovie Cost:  " + cost + "\tQuantity:  " + qty + "\t Total Cost:  " + (cost * qty);
            return s;
        }

        public void PrintOrderItem(int index)//Printing an order object.
        {
            Console.WriteLine("{0,5}{1,17}{2,21}{3,16}{4,20}", index, mtitle, rating1, ticketsqty, time);
            Console.WriteLine("===============================================================================");
            Console.WriteLine("Screen: {3}\tMovie Cost: {0,6:C}\tQuantity:  {1,-4}\tTotal Cost:  {2,10:C}", cost, ticketsqty, cost * ticketsqty, screennumb);
            Console.WriteLine("===============================================================================");
        }

        public void PrintUsername()//Printing name entered
        {
            Console.WriteLine("Order Name: {0}", username1);
        }
    }

    public class MovieOrdering
    {
        public static void OrderMovies()
        {
            //Order list
            List<OrderMovie> order = new List<OrderMovie>();
            OrderMovie item = new OrderMovie();
            Boolean orderMore = true;

            //  Display Greeting, get user's name
            Console.WriteLine("Hello, welcome to Pre-Millenium Movies! Please enter your name below:");
            string username = Console.ReadLine();
            Console.WriteLine("Thank you {0}. Please select your movie below.", username);
            item.username1 = username;

            List<Movie> movies = new List<Movie>();  //Introducing the list of movies
            string input;//used for writing from text file
            int ratingInput=0;//used to identify rating with enum value
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

            //Functional Variables
            int selection;  //Used for the movie selection
            Boolean confirm;  //Used in confirmation loops
            const int maxtickets = 300;  //The maximum number of tickets available to purchase
            int time; //Used in movie showing time selection
            int age = 0;
            int numTickets = 0; //Variable for number of tickets selected per movie
            string numTicketsString;//Used to get number of tickets desired
            Boolean validTickets;//Used to verify if ticket amount is valid

            //Display all movies in a list format
            do
            {
                do  // Confirmation Loop, ensuring user chose the correct movie.
                {
                    ShowMovies(movies);
                    //Ask user for movie selection and confirm their response
                    Console.WriteLine("Which movie selection would you like to choose?");
                    selection = ValidEntry(1, movies.Count - 1);

                    //Have user confirm the movie selection
                    Console.WriteLine("You have selected to watch {0}. The price is {1:C}. The rating is {2}.",
                        movies[selection].title, movies[selection].price, movies[selection].rating);
                    Console.WriteLine("Please enter your age below to verify you are able to see this movie:");
                    age = ValidEntry(1, 115);
                    if (age < 13 && ratingInput == 2)
                    {
                        Console.WriteLine("Sorry, you are too young for this movie.");
                    }


                    Console.WriteLine("Would you like to proceed with this selection? (y/n)");  //  Confirm the selection
                    confirm = ConfirmYes();
                } while (!confirm);  //Return to confirmation loop


                //Adding movie information to order item
                item.mtitle = movies[selection].title;
                item.rating1 = movies[selection].rating;
                item.cost = movies[selection].price;

                Console.Clear();

                //Switch for selecting the showing time.
                do
                {
                    Console.WriteLine("The following screen times are available for this movie: \n1. {0}\n2. {1}\n3. {2}",
                    movies[selection].time1, movies[selection].time2, movies[selection].time3);
                    Console.WriteLine("Please select the number of showing you'd like to see:");
                    time = ValidEntry(1, movies.Count - 3);
                    switch (time)
                    {
                        case 1:
                            {
                                item.time = movies[selection].time1;
                                item.screennumb = movies[selection].screennum;
                                break;
                            }
                        case 2:
                            {
                                item.time = movies[selection].time2;
                                item.screennumb = movies[selection].screennum;
                                break;
                            }
                        case 3:
                            {
                                item.time = movies[selection].time3;
                                item.screennumb = movies[selection].screennum;
                                break;
                            }
                    }

                    //Confirming movie time
                    Console.WriteLine("You have selected to view the {0} showing. The movie will be in theater {1}."
                    , item.time, item.screennumb);
                    Console.WriteLine("Would you like to proceed with this selection? (y/n)");
                    confirm = ConfirmYes();
                } while (!confirm);

                Console.Clear();

                do  //Asking for number of tickets to purchase
                {
                    {
                        Console.WriteLine("How many tickets would you like to order?");
                        try
                        {
                            numTicketsString = Console.ReadLine();
                            numTickets = Convert.ToInt32(numTicketsString);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Please enter the amount of tickets you want from 1 to {0}.", maxtickets);
                            Console.WriteLine(e.Message);
                            validTickets = false;
                            continue;
                        }
                        if (numTickets > 0 && numTickets < 301)
                        {
                            validTickets = true;
                        }
                        else
                        {
                            Console.WriteLine("Please enter the amount of tickets you want from 1 to {0}.", maxtickets);
                            validTickets = false;
                        }
                    }
                } while (!validTickets);

                item.ticketsqty = numTickets;//Adding ticket quantity to the order

                order.Add(item);  //Add the ordered item to the movie list for the receipt.
                //Ask to order another movie
                Console.Clear();
                Console.WriteLine("Would you like to watch another movie? (y/n)");
                orderMore = ConfirmYes();
            } while (orderMore);

            Console.WriteLine("Would you like to add food to your order?");
            ConsoleKey inputKey;

            inputKey = Console.ReadKey().Key;
                switch (inputKey)
                {
                case ConsoleKey.Y:
                    OrderFoods();
                    break;
                case ConsoleKey.N:
                    break;
            }


            //Print the receipt and close.
            Console.Clear();
            PrintReceipt(order);
            Console.WriteLine("Thank you for your business!");
        }

        static void PrintReceipt(List<OrderMovie> oList)  //Print the receipt of all movies ordered
        {
            Console.WriteLine("==================================  Receipt ===================================");  //Receipt header
            Console.WriteLine("{0,5}{1,17}{2,21}{3,16}{4,20}", "Order", "Movie", "Rating", "Tickets", "Time");  //Column headings
            double grandTotal = 0;  //Variable to store the total amount of movies
            int i = 1;
            foreach (OrderMovie o in oList)
            {
                o.PrintOrderItem(i);
                grandTotal += (o.cost * o.ticketsqty);
                i++;
                o.PrintUsername();
            }
            Console.WriteLine("Grand Total:  {0:C}", grandTotal);  //Grand Total                
            string path2 = "receiptmovies.txt";
            using (StreamWriter sw = new StreamWriter(path2))
            {
                int ii = 0;
                foreach (OrderMovie o in oList)
                {
                    o.ToString();
                    ii++;
                    sw.WriteLine("{6}, {5}, {0}, {4}, {1}, {2:C}, {3}",
                        o.mtitle, o.time, o.cost * o.ticketsqty, o.screennumb, o.rating1, o.username1, ii);
                }
                sw.Close();
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

        static int ValidAge(int ageofuser)
        {
            Boolean age = true;
            if (ageofuser > 18)
            {
                age = true;
            }
            if (ageofuser < 13)
            {
                Console.WriteLine("You will need parental guidance to attend this movie.");
                age = false;
            }while (!age);
            return ageofuser;
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

        static Boolean ConfirmYes()  //Yes=true, no=false
        {
            ConsoleKey inputKey;  //Variable used to capture response.

            inputKey = Console.ReadKey().Key;
            switch (inputKey)  //Default response is yes.
            {
                case ConsoleKey.N:
                    return false;
                default:
                    return true;
            }
        }
    }
}


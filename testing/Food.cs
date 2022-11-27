using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace testing
{
    public class Food
    {
        public string category;
        public string option0;
        public string option1;
        public string option2;
        public string option3;
        public string option4;
        public double price;
    }

    public struct OrderFood
    {
        public string category;
        public string foodselection;
        public double price;
        public int quantity;
        public double cost;

        public void PrintFoodItem()
        {
            Console.WriteLine("{0,8}{1,12}{2,10}{3,10}{4,25}", category, foodselection, quantity, price, cost);
        }
    }

    public class FoodOrdering
    {
        public static void OrderFoods()
        {
            List<OrderFood> order = new List<OrderFood>();
            OrderFood item = new OrderFood();
            Boolean OrderMore;

            List<Food> foods = new List<Food>();
            string input;
            string path = "C:\\Users\\james\\source\\repos\\foodorder\\foodorder\\fooditems.txt";
            if (File.Exists(path))
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    while (!sr.EndOfStream)
                    {
                        Food food = new Food();

                        food.category = sr.ReadLine();

                        food.option0 = sr.ReadLine();
                        food.option1 = sr.ReadLine();
                        food.option2 = sr.ReadLine();
                        food.option3 = sr.ReadLine();
                        food.option4 = sr.ReadLine();

                        input = sr.ReadLine();
                        food.price = Convert.ToDouble(input);

                        foods.Add(food);
                    }
                    sr.Close();
                }
            }

            int selection;
            Boolean confirm;
            int foodselect;

            do
            {
                do
                {
                    ShowCategories(foods);
                    Console.WriteLine("Which category of food would you like to order from?");
                    selection = ValidEntry(1, foods.Count - 1);

                    Console.WriteLine("You have to selected to order from {0}. Is this correct? (y/n)", foods[selection].category);
                    confirm = ConfirmYes();
                } while (!confirm);

                item.category = foods[selection].category;

                do
                {
                    Console.WriteLine("The following options are available: \n1. {0}\n2. {1}\n3. {2}\n4. {3}",
                        foods[selection].option1, foods[selection].option2, foods[selection].option3, foods[selection].option4);
                    foodselect = ValidEntry(1, foods.Count);
                    switch (foodselect)
                    {
                        case 1:
                            {
                                item.foodselection = foods[selection].option1;
                                break;
                            }
                        case 2:
                            {
                                item.foodselection = foods[selection].option2;
                                break;
                            }
                        case 3:
                            {
                                item.foodselection = foods[selection].option3;
                                break;
                            }
                        case 4:
                            {
                                item.foodselection = foods[selection].option4;
                                break;
                            }
                    }
                    Console.WriteLine("Would you like to proceed with this selection? (y/n)");
                    confirm = ConfirmYes();
                } while (!confirm);
                Console.WriteLine("What quantity of this item would you like to add?");
                foodselect = ValidEntry(1, 20);
                item.quantity = foodselect;
                item.price = foods[selection].price;
                order.Add(item);
                Console.WriteLine("Would you like to order more food? (y/n)");
                OrderMore = ConfirmYes();
            } while (OrderMore);

            Console.Clear();
            PrintFoodReceipt(order);

            static void ShowCategories(List<Food> foods)
            {
                int counter = 0;
                Console.WriteLine("{0}", foods[0].category);
                foreach (Food food in foods)
                {
                    switch (counter)
                    {
                        case 0:
                            counter++;
                            continue;
                        default:
                            Console.WriteLine("{0}. {1}", counter, food.category);
                            counter++;
                            continue;
                    }
                }
            }

            static void PrintFoodReceipt(List<OrderFood> foodList)
            {
                Console.WriteLine("=============================Receipt=============================");
                Console.WriteLine("{0,8}{1,12}{2,10}{3,10}{4,25}", "Category", "Item", "Quantity", "Price", "Subtotal");
                double grtotal = 0;
                //int i = 1;
                foreach (OrderFood food in foodList)
                {
                    food.PrintFoodItem();
                    grtotal += food.price * food.quantity;
                }
                Console.WriteLine("Grand total: {0:C}", grtotal);
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
}


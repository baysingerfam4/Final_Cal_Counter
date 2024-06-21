using System.Threading;
using System;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Xml.Resolvers;
using System.ComponentModel.Design;
using static System.Reflection.Metadata.BlobBuilder;
using System.Security.AccessControl;
using System.Threading.Channels;
using System.Reflection;
using System.Xml.Linq;

namespace Final_Cal_Counter
{

    internal class Program
    {
        // Carla Baysinger
        // June 20th 2024
        // Final-Calorie Counter

        static FoodItem[] foodItems = new FoodItem[4]; // Create and initialize your array to the size of 4
        static void Main(string[] args)
        {
            // methods that should be called in main will be your Preload and Menu.
            PreLoad();
            Menu();
            // Name	Category	 Calories	Quantity	Total Calories
            // Apple Fruit         95          1            95
            // Banana Fruit        105         2            210
            // Carrot Vegetable    25          3            75
            // Broccoli Vegetable   55         2            110
            // Chicken Protein     165         1            165
            // Beef Protein        250         1            250
            // Rice Grain          205         1            205
            // Bread Grain         80          2            160
            // Milk Dairy          150         1            150
            // Cheese Dairy        110         2            220

        }//main
        public static void PreLoad()
        {  //This methods is used to populate your array with 2 items
            //Select two from the provided table
            // Apple Fruit      95      1      95
            // Banana Fruit   105     2      210
            foodItems[0] = new FoodItem("Apple", 1, 95, 1);
            foodItems[1] = new FoodItem("Banana", 1, 105, 2);
        }
        public static void DisplayAllFoodItems()
        {
            //Displays all the items in the FoodItem array
            //Needs to use.DisplayInformation()
            //used a foreach loop to loop through and Display all the food items
            foreach (FoodItem food in foodItems)
            {
                if (food != null)
                {
                    // Displays all information in the following format
                    Console.WriteLine(food.DisplayInformation());
                }
                else
                {
                    Console.WriteLine("Item not found");
                }
            }
        }//DisplayAllFoodItems
        public static void AddItem()
        { // A method that calls 3 separate methods to add a new food item
            try
            {
                // Prompt User for foodItem Object
                FoodItem newItem = MakeNewItem();
                // method that checks for the first empty array slot
                int firstIndex = FindEmptyIndex();

                if (firstIndex == -1)
                {
                    Console.WriteLine("The array is full");
                    IncreaseArraySize();//method that double the size of the array
                    firstIndex = FindEmptyIndex();
                }

                foodItems[firstIndex] = newItem;
                DisplayAllFoodItems(); // how it displays



            }
            catch (Exception ex)
            {
                Console.WriteLine("Please enter a valid number."); // a catch if information is not correct
            }

        }
        public static FoodItem MakeNewItem()
        { // method to ask user for a new food item name, category,
            Console.Write("Enter a fooditem name: ");
            string fooditemName = Console.ReadLine();
            Console.Write("Enter a Category name - 1 fruit  - 2 Vegetable - 3 Protein - 4 Grain - 5 Dairy: ");
            int category = int.Parse(Console.ReadLine());
            Console.Write("Enter number of calories: ");
            int calories = int.Parse(Console.ReadLine());
            Console.Write("Enter quantity: ");
            int quantity = int.Parse(Console.ReadLine());
            Console.WriteLine();
            FoodItem newFoodItem = new FoodItem(fooditemName, category, calories, quantity);
            return newFoodItem;
        }
        public static int FindEmptyIndex()
        { //Uses Linear Search

            for (int i = 0; i < foodItems.Length; i++)
            {
                // Checking for null, search your foodItems array for the first empty index
                FoodItem temp = foodItems[i];

                if (temp == null)
                {

                    return i;
                }

            }

            // If there are NO empty slots, return -1
            return -1;
        }

        public static void IncreaseArraySize()
        {
            // if array is full double the size of the first array
            FoodItem[] tempArray = new FoodItem[foodItems.Length * 2];

            // Move the elements from the first array to the second
            for (int i = 0; i < foodItems.Length; i++)
            {
                tempArray[i] = foodItems[i];
            }

            // Replace the original array with the new one
            foodItems = tempArray;
        }

        public static double TotalCaloriesEaten()
        { //Should loop through the array and sum the total calories eaten of all items
            try { 
            double TotalCalories = 0;
            for (int i = 0; i < foodItems.Length; i++)
            {
                double itemCalories = foodItems[i].Quantity * foodItems[i].Calories;
                TotalCalories += itemCalories;
                    Console.WriteLine($"Your total calories are {TotalCalories}");
                    return TotalCalories;
                }


            // Result : Your total calories are 305
            
            }
            catch (Exception ex)
            {
                Console.WriteLine("invalid");
            }
            return -1;
        }

        public static double AverageCaloriesEaten()
        {
            //Should loop through your array and get the average item calorie that you
            // add all the numbers then divide by how many in the array
            double sum = 0;
            try { 
              for (int i = 0; i < foodItems.Length; i++)
              {
                sum += foodItems[i].Calories;
                    double averageCalories = sum / foodItems.Length;
                    Console.WriteLine($"Youe average calories are {averageCalories}");
                    // Result : Your average calories are 152.5
                    return averageCalories;
              }
               
            }
            catch (Exception ex)
            {
                Console.WriteLine("invalid");
            }
            return -1;
        }
        public static void FindandDisplayCategory() // used this information to run through my menu
        {
            Console.Write("Enter a category name to search: ");
            string userinput = Console.ReadLine();

            DisplayByCategory(userinput); // called my method that searches by category
        }
        public static void DisplayByCategory(string foodcategory)
        {
            //Create a method that will ask the user to select a category
            //You should then display all items ONLY in that category

            try
            {
                foodcategory = foodcategory.ToUpper();
                bool found = false;
                for (int i = 0; i < foodItems.Length; i++)
                {
                    FoodItem currentItem = foodItems[i];
                    if (foodcategory == currentItem.CategoryName().ToUpper())
                    {
                        Console.WriteLine(currentItem.DisplayInformation());
                        found = true;
                        break;

                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("invalid");
            }
        }
        public static void FindandDisplayItem()
        {
            Console.Write("Enter a food name to search: ");
            string userinput = Console.ReadLine();

            DisplayItemWithName(userinput);
        }
        public static void DisplayItemWithName(string foodname)
        {
            //Ask the user for a food name
            //Display the item with that name
            //Otherwise display name doesn't exist
            //Uses linear search
            //Should be case insensitive(doesn't matter if there are upper or lower cases )
            
            try { 
            foodname = foodname.ToUpper();
                bool found = false;
              for (int i = 0; i < foodItems.Length; i++)
              {
                FoodItem currentItem = foodItems[i];
                if (foodname == currentItem.Name.ToUpper())
                {
                    Console.WriteLine(currentItem.DisplayInformation());
                        found = true;
                        break;
                    
                }
                
              }
            }
            catch(Exception ex)
            {
                Console.WriteLine("invalid");
            }


        }

        public static double DisplayCalories()
        { // Display Calories for an item that was eaten
            try {
                Console.WriteLine("Please choose a food item:"); // ask user
                double sum = double.Parse(Console.ReadLine());// convert to double
                
              for (int i = 0; i < foodItems.Length; i++) // loop through array
              {
                    double itemCalories = foodItems[i].Calories * foodItems[i].Quantity;
                    sum += foodItems[i].Calories;
                    Console.WriteLine($"Your calories are {itemCalories}");
                    return sum;
                }
            //double itemCalories = sum + foodItems[i].Calories;
            
            }
            catch(Exception ex)
            {
                Console.WriteLine("invalid"); // error
            }

            return -1;
        }
       

        public static void Menu()
        {
            //Menu Options below
            //Displaying all the calories you have eaten
            //Add New Items
            //Calculate your total calories eaten
            //Calculate the average calories of an item you've eaten
            //Display all food eaten of a certain category
            //Search for a food item by name
            //Exit
            
            bool isRunning = true;
            do
            {
                Console.WriteLine("1. Display all the calories you have eaten:");
                Console.WriteLine("2. Add New Items:");
                Console.WriteLine("3. Calculate your total calories eaten:");
                Console.WriteLine("4. Calculate the average calories of an item you've eaten: ");
                Console.WriteLine("5. Display all food eaten of a certain category: ");
                Console.WriteLine("6. Search for a food item by name");
                Console.WriteLine("7. Exit");
                Console.Write("Please Enter Your Choice");
                string userChoice = Console.ReadLine();
                switch (userChoice)
                {
                    case "1":
                        DisplayCalories(); // menu that goes to different methods
                        break;
                    case "2":
                        AddItem();
                        break;
                    case "3":
                        TotalCaloriesEaten();
                        break;
                    case "4":
                        AverageCaloriesEaten();
                        break;
                    case "5":
                        FindandDisplayCategory();
                        break;
                    case "6":
                        FindandDisplayItem();
                        break;
                    case "7":
                        isRunning = false;
                        Console.WriteLine("Exitting");
                        break;
                    default:
                        Console.WriteLine("Please select an option (1-7)");
                        break;
                }
                
            } while (isRunning) ;
            
        }

        
    } // class program
        

    
    public class FoodItem // create a class
    {
        //Fields
        public string Name;
        public int Category;
        public int Calories;
        public int Quantity;

        // Default Constructor
        public FoodItem()
        {
            Name = "No Item Listed";
            Category = -1;
            Calories = -1;
            Quantity = -1;
        }
        // Constructor
        public FoodItem(string name, int category, int calories, int quantity)
        {
            Name = name;
            Category = category;
            Calories = calories;
            Quantity = quantity;

        }
        //Methods
        public double TotalCalories()
        { // Do the calculation to return the total calories of an instance of the instanced object
            return Quantity * Calories;
        }

        public string CategoryName()
        {
            // Write the code that takes the category number assigned at returns the proper. Here's the list
            // 1.Fruit 2.Vegetable 3.Protein 4.Grain
            // 5.Dairy
            // For any other number put "No Category Chosen"
            string categoryassigned = "";
            switch(Category)
            {
                case 1:
                    categoryassigned = "Fruit";
                    break;
                case 2:
                    categoryassigned = "Vegetable";
                    break;
                case 3:
                    categoryassigned = "Protein";
                    break;
                case 4:
                    categoryassigned = "Grain";
                    break;
                case 5:
                    categoryassigned = "Dairy";
                    break;
                default:
                    categoryassigned = "No Category Chosen";
                    break;
            }
            return categoryassigned;
        }
        public string DisplayInformation()
        { // Returns a formatted string with the items information
          //Use the methods you created to display Total Calories and Category
            string formattedDisplay = "";
            formattedDisplay += $"Name: {Name} \n";
            formattedDisplay += $"Category: {CategoryName()} \n";
            formattedDisplay += $" Quantity: {Quantity} \n";
            formattedDisplay += $"Calories: {Calories} \n"; 
            formattedDisplay += $"Total Calories: {TotalCalories()} \n";

            return formattedDisplay;
        }

        

    } // FoodItem

}//namespace

using System;
using System.Linq;

namespace GuessMyNumberGame
{
    class Program
    {
        static int counter = 0;

        static void Main(string[] args)
        {
            int[] arr = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            string msg = Demo(GetNumber(), arr, 0, arr.Length - 1);
            Console.WriteLine(msg);
            Console.WriteLine($"Iterations: {counter}");
            Console.WriteLine("====================================================================\n");

            counter = 0;
            Console.WriteLine("Computer rendomly pick a number from 1 to 1000.");
            Console.WriteLine("You need to guess the number and computer will tell you the guess is correct, too high or to low.");
            Console.WriteLine("Let's go!");
            HumanPlays(new Random().Next(1, 1000));
            Console.WriteLine("====================================================================\n");

            counter = 0;
            Console.WriteLine("Pick a number from 1 to 100.\nComputer will try to guess the number.");
            ComputerPlays(Enumerable.Range(1, 100).ToArray(), 0, 99);
            Console.WriteLine($"Iterations: {counter}");
        }

        static int GetNumber()
        {
            int number = 0;
            do
            {
                Console.Clear();
                Console.Write("Please enter a number from 1 - 10: ");
                number = int.TryParse(Console.ReadLine(), out int result) ? result : 0;

            } while ((number < 1 || number > 10));
            return number;
        }
        static string Demo(int number, int[] arr, int min, int max)
        {
            counter++;

            if (min > max)
            {
                return "Number was not found";
            }

            var middleIndex = (min + max) / 2;
            var middleArrNumber = arr[middleIndex];

            if (middleArrNumber == number)
            {
                return $"Your number is {middleArrNumber}!";
            }
            else if (middleArrNumber > number)
            {
                Console.WriteLine($"{counter}: {number} < {middleArrNumber}");
                return Demo(number, arr, min, middleIndex - 1);
            }
            else
            {
                Console.WriteLine($"{counter}: {number} > {middleArrNumber}");
                return Demo(number, arr, middleIndex + 1, max);
            }
        }

        static void HumanPlays(int number)
        {
            ushort guess = 0;
            Console.Write("\nEnter a guess from 1 to 1000 > ");
            while (!ushort.TryParse(Console.ReadLine(), out guess))
            {
                Console.Write("Enter a valid number from 1 to 1000 > ");
            }
            if (guess == number)
            {
                Console.WriteLine($"You are correct!\nYou did {++counter} guesses.");
                return;
            }
            else if (guess > number)
            {
                Console.WriteLine($"Gues #{++counter}: {guess} is to high");
                HumanPlays(number);
            }
            else
            {
                Console.WriteLine($"Gues #{++counter}: {guess} is to low");
                HumanPlays(number);
            }

        }

        static void ComputerPlays(int[] arr, int min, int max)
        {

            counter++;
            bool exit = false;
            var middleIndex = (min + max) / 2;
            var middleArrNumber = arr[middleIndex];
            while (!exit)
            {
                Console.WriteLine($"{counter}: Is your number {middleArrNumber}?");
                Console.WriteLine("Y: Yes | H: Too High | L: Too Low");
                var response = Console.ReadKey(true).Key;
                switch (response)
                {
                    case ConsoleKey.Y:
                        Console.WriteLine($"Your number is {middleArrNumber}!");
                        return;

                    case ConsoleKey.H:
                        ComputerPlays(arr, min, middleIndex - 1);
                        exit = true;
                        break;
                    case ConsoleKey.L:
                        ComputerPlays(arr, middleIndex + 1, max);
                        exit = true;
                        break;

                    default:
                        Console.WriteLine("Not a valid answer!");
                        break;
                }

            }

        }

    }
}

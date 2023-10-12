using Figgle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace OnceUpenAGame
{
    public class ReachANumber
    {
        public int TargetValue;
        public int CurrentValue = 1;
        public List<Operator> Operators = new List<Operator>();
        public int Round = 0;
        public int RoundLimit = 7;
        private Random random = new Random();

        public ReachANumber()
        {
            TargetValue = random.Next(10, 51);
            for (int i = 0; i < RoundLimit; i++)
            {
                if (i == 0)
                    Operators.Add(Operator.add);
                else
                    Operators.Add((Operator)random.Next(0, 2)); // Vi tager vores operator og tilfældiggøre den mellem 0-3
            }
        }

        public int CalculateCurrentValue(int selectedNumber)
        {
            int newValue = CurrentValue;
            switch (Operators[Round])
            {
                case Operator.add:
                    newValue += selectedNumber;
                    break;

                case Operator.subtract:
                    newValue -= selectedNumber;
                    break;

                    //case Operator.multiply:
                    //    newValue *= selectedNumber;
                    //    break;

                    //case Operator.divide:
                    //    newValue = (int)Math.Round((double)newValue / selectedNumber);
                    //    if (newValue == 0)
                    //        newValue = 1;
                    //    break;

            }
            return newValue;
        }

        private void WriteOptions(List<int> numbers, int index)
        {
            Console.Clear();
            for (int i = 0; i < Operators.Count; i++)
            {
                if (i < Round)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                }
                else if (i == Round)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                }

                if (i == Operators.Count - 1)
                {
                    Console.Write($"{Operators[i]}\n\n");
                }
                else
                    Console.Write($"{Operators[i]}, ");
                Console.ForegroundColor = ConsoleColor.White;
            }
            Console.ForegroundColor = ConsoleColor.Yellow; Console.WriteLine($"The number you're trying to reach is: {TargetValue}");
            Console.WriteLine($"The number you have: {CurrentValue} \n");
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine("You can choose between the following numbers:");

            foreach (int number in numbers)
            {
                if (numbers.IndexOf(number) == index)
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write("<... ");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                {
                    Console.Write("     ");
                }

                Console.Write(number);
                if (number < 10)
                    Console.Write(' ');

                if (numbers.IndexOf(number) == index)
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine(" ...>");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                {
                    Console.WriteLine("     ");
                }
            }
        }

        public void SelectNumber()
        {
            while (Round + 1 <= RoundLimit)
            {
                if (CurrentValue == TargetValue)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(FiggleFonts.Epic.Render("You've  Won!"));
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                }

                List<int> numbers = new();
                for (int i = 0; i < 3; i++)
                {
                    int temp = random.Next(1, 31);
                    int tempNewValue = CalculateCurrentValue(temp);
                    while ((tempNewValue < 1 || tempNewValue > 100) && numbers.Contains(temp))
                    {
                        temp = random.Next(1, 31);
                    }

                    numbers.Add(temp);
                }

                int index = 0;
                WriteOptions(numbers, index);

                // Store key info in here
                ConsoleKeyInfo keyinfo;
                do
                {
                    keyinfo = Console.ReadKey();

                    // Handle each key input (down arrow will write the menu again with a different selected item)
                    if (keyinfo.Key == ConsoleKey.DownArrow)
                    {
                        if (index + 1 < numbers.Count)
                        {
                            index++;
                            WriteOptions(numbers, index);
                        }
                    }
                    if (keyinfo.Key == ConsoleKey.UpArrow)
                    {
                        if (index - 1 >= 0)
                        {
                            index--;
                            WriteOptions(numbers, index);
                        }
                    }

                    // Handle different action for the option
                    if (keyinfo.Key == ConsoleKey.Enter)
                    {
                        CurrentValue = CalculateCurrentValue(numbers[index]);

                        if (CurrentValue > 100)
                        {
                            Console.WriteLine("Adventurer you've exceeded the max number so i shall put you on 100");
                            CurrentValue = 100;
                            Thread.Sleep(3000);
                        }

                        if (CurrentValue < 1)
                        {
                            Console.WriteLine("Adventurer you subceeded the minimum number so i shall put you on 1");
                            CurrentValue = 1;
                            Thread.Sleep(3000);
                        }

                        index = 0;
                    }
                }
                while (keyinfo.Key != ConsoleKey.Enter);
                //Console.ReadKey();

                Round++;
            }
            if (CurrentValue != TargetValue)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(FiggleFonts.Epic.Render("You've  lost!"));
                Console.ForegroundColor = ConsoleColor.White;
            }

        }
    }
}

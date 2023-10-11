using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            TargetValue = random.Next(51);
            for (int i = 0; i < RoundLimit; i++)
            {
                Operators.Add((Operator)random.Next(0, 4)); // Vi tager vores operator og tilfældiggøre den mellem 0-3
            }
        }

        public void CalculateCurrentValue(int selectedNumber)
        {
            switch (Operators[Round])
            {
                case Operator.add:
                    CurrentValue += selectedNumber;
                    break;

                case Operator.subtract:
                    CurrentValue -= selectedNumber;
                    break;

                case Operator.multiply:
                    CurrentValue *= selectedNumber;
                    break;

                case Operator.divide:
                    CurrentValue = (int)Math.Round((double)CurrentValue / selectedNumber);
                    break;

            }
            Round++;

            // Udregn nuævrende værdi udfra det valgte nummer og tilfældig operator
            // Tjek om nummeret er ramt samt om runder er brugt
        }

        public void SelectNumber()
        {
            while (Round + 1 <= RoundLimit)
            {
                Console.Clear();
                for (int i = 0; i < Operators.Count; i++)
                {
                    if (i == Operators.Count - 1)
                    {
                        Console.Write($"{Operators[i]}\n\n");
                    }
                    else
                        Console.Write($"{Operators[i]}, ");
                }
                List<int> numbers = new();
                for (int i = 0; i < 3; i++)
                {
                    numbers.Add(random.Next(0, 51));
                }

                Console.WriteLine($"The number you're trying to reach is: {TargetValue}");
                Console.WriteLine($"The number you have: {CurrentValue} \n");

                Console.WriteLine("You can choose between the following numbers:");
                Console.WriteLine($"1) {numbers[0]}");
                Console.WriteLine($"2) {numbers[1]}");
                Console.WriteLine($"3) {numbers[2]}\n");

                Console.Write("You choose: ");
                _ = int.TryParse(Console.ReadLine(), out int chosen);

                CalculateCurrentValue(numbers[chosen -1]);
            }
            // Vis muligheder til brugeren. Modtag input fra bruger.
            // Vi kører metoden med det valgte input
        }
    }
}

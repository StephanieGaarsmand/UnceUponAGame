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
        public int CurrentValue = 0;
        public List<Operator> Operators = new List<Operator>();
        public int Round=0;
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
            Console.WriteLine(CurrentValue);



            // Udregn nuævrende værdi udfra det valgte nummer og tilfældig operator
            // Tjek om nummeret er ramt samt om runder er brugt
        }

        public void SelectNumber()
        {
            random.Next(0, 51);
            CalculateCurrentValue(0); // Vis muligheder til brugeren. Modtag input fra bruger.
                                      // Vi kører metoden med det valgte input

        }
    }
}

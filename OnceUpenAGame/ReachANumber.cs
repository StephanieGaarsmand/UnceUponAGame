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
        public int CurrentValue;
        public Operator Operator;
        public int Round;
        public int RoundLimit = 7;
        private Random random = new Random();

        public void CalculateCurrentValue(int selectedNumber) 
        {
            Operator = (Operator) random.Next(0,4); // Vi tager vores operator og tilfældiggøre den mellem 0-3
            // Udregn nuævrende værdi udfra det valgte nummer og tilfældig operator
            // Tjek om nummeret er ramt samt om runder er brugt
        }
        
        public void SelectNumber()
        {
            random.Next(0,);
            CalculateCurrentValue(0); // Vis muligheder til brugeren. Modtag input fra bruger.
                                      // Vi kører metoden med det valgte input

        }
    }
}

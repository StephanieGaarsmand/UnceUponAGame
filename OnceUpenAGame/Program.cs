namespace OnceUpenAGame
{
    public class Program
    {
        static void Main(string[] args)
        {

            ReachANumber reachANumber = new ReachANumber();
            /*reachANumber.CalculateCurrentValue(10);
            reachANumber.CalculateCurrentValue(2);
            reachANumber.CalculateCurrentValue(5);*/
            

            reachANumber.SelectNumber();
            //reachANumber.Operators.ForEach(Console.WriteLine);
            Console.ReadLine();
        }
    }
}
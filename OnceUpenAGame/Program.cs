using System.Runtime.CompilerServices;

namespace OnceUpenAGame
{
    public class Program
    {
        static void Main(string[] args)
        {
            Player player = new Player();
            Item goldCoin = new Item();
             
            Option openInventory = new(
                "Open Inventory",
                () => player.OpenInventory()
                );

            #region Great Hall
            Scene greatHall = new("The Hall Of Greatness",
                "Description here",
                new List<Scene>(),
                new List<Option>() { openInventory },
                true);
            #endregion

            #region Church
            Scene church3 = new("Church",
                "It's a big coin!",
                new List<Scene>(),
                new List<Option>() { new Option("Pick it up", ()=> goldCoin.PickUpItem()) });
            Scene church2 = new("Church",
                "Continuing to look around you spot another doorway",
                new List<Scene>(),
                new List<Option>() { openInventory });
            Scene church = new("Church",
                "Standing in the middle of the pews looking towards the alter, you see one of your family photos... Weird..\nIgnoring it you see something shiny on one of the pews.",
                new List<Scene>(),
                new List<Option>() { 
                    new("Continue looking for things", ()=> church2.DisplayScene()),
                    new("Examine the shiny thing", ()=> church3.DisplayScene()),
                    openInventory });
            #endregion
            #region Locker room
            Scene lockerroom2 = new("Locker room",
                "Continuing to look around you spot another doorway",
                new List<Scene>(),
                new List<Option>() {openInventory });
            Scene lockerroom = new("Locker room",
                "It looks like this locker room belongs to a bunch of miners. You see the names \"Bashful\" and \"Grumpy\" on some of the cubbies, odd names.. They remind you of something... You can't remember what...",
                new List<Scene>(),
                new List<Option>() { 
                    new Option("Continue looking around", ()=> lockerroom2.DisplayScene()),
                    openInventory });
            #endregion

            Scene cave = new("Cave",
                "Inside the cave you see more of the spiders crawling under a big door in front of you.\nLooking around you see two others open doorways to each side of you.",
                new List<Scene>() { church, lockerroom, greatHall },
                new List<Option>() {openInventory});

            Scene prolgue = new("Prologue",
                "You're standing outside a dark ominous looking cave, with small (adorable?) spiders crawling near the entrance.",
                new List<Scene>() {cave},
                new List<Option>() { new ("Leave the cave alone", () => Environment.Exit(0)) });

            


            ReachANumber reachANumber = new ReachANumber();
            /*reachANumber.CalculateCurrentValue(10);
            reachANumber.CalculateCurrentValue(2);
            reachANumber.CalculateCurrentValue(5);*/

            prolgue.DisplayScene();


            reachANumber.SelectNumber();
            //reachANumber.Operators.ForEach(Console.WriteLine);
            Console.ReadLine();
        }
    }
}
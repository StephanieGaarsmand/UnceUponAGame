using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace OnceUpenAGame
{
    public class Program
    {
        #region Used to maximise Window
        [DllImport("kernel32.dll", ExactSpelling = true)]
        private static extern IntPtr GetConsoleWindow();
        private static IntPtr ThisConsole = GetConsoleWindow();
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        #endregion
        static void Main(string[] args)
        {
            #region Used to maximise Window
            Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
            ShowWindow(ThisConsole, 3);
            #endregion

            Player player = new Player();
            Item goldCoin = new Item();
            ReachANumber reachANumber = new ReachANumber();

            Option openInventory = new(
                "Open Inventory",
                () => player.OpenInventory()
                );

            Scene reachANumberGame = new("Rules - Reach  a  number",
                "The Great Wizard has captured your parents and the only way to free them is by completing his DIE HARD CHALLENGE.\n" +
                "This challenge needs to be completed within 7 tries, BUT not all tries needs to be used, for the freedom of your parents.\n" +
                "To win the game you have to add, subtract and use the witts of Mathematics, The Great Wizards only accepts intellectual beings, no mere human can complete this.\n\n" +
                "Now you have to chose from a list of numbers, filled with new numbers each round, to use with the given operators.\n" +
                "The operators is already planed for you. - read the game design CAREFULLY",
                new List<Scene>(),
                new List<Option>() { new("Start game", () => reachANumber.SelectNumber()) });

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
                new List<Scene>() { reachANumberGame },
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
                new List<Option>() { openInventory});

            Scene prolgue = new("Prologue",
                "You suddenly find yourself standing outside a dark ominous looking cave, with small (adorable?) spiders crawling near the entrance.\n" +
                "You spot a note:\n\n\n" +
                "Adventurer,\n" +
                "I the GREATEST wizard of all, have your parents hidden away in my cave.\n" +
                "To find them, you must find a KEY... but where?\n\n" +
                "Good luck...",
                new List<Scene>() {cave},
                new List<Option>() { new ("Leave the cave alone", () => Environment.Exit(0)) });

            prolgue.DisplayScene();

            Console.ReadLine();
        }
    }
}
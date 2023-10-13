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

        #region Endgame (not marvel)
        public static Scene BedroomBad = new("", "Bedroom",
            "You awake covered in sweat, in your dark room hearing thunder outside, having peed the bed from the worst nightmare...\n" +
            "Your mean math teacher disguised as a wizard had kidnapped your parents, and you had to do math to save them, which you failed to do...\n" +
            "On the wall you see the family photo from the dream, and suddenly you remember that your mother read \"Snow White and the seven dwarfs\" to you last night, which explains the names from the locker room.\n\n" +
            "You consider going back to sleep... but do you dare?",
            new List<Scene>() { },
            new List<Option>() {
                    //new("Go back to sleep", () => Program.Main()),
                    new ("Stay awake till morning", () => Environment.Exit(0)) },
            new List<Item>() { });

        public static Scene BedroomGood = new("", "Bedroom",
           "You awake in your bed, smelling pancakes, eggs and bacon coming from the kitchen...\n" +
           "You had such a weird dream..  your mean math teacher disguised as a wizard had kidnapped your parents, and you had to do math to save them, \n" +
           "which you of course suceeded in doing, beacause you're amazing at math!\n" +
           "On the wall you see the family photo from the dream, and suddenly you remember that your mother read \"Snow White and the seven dwarfs\" to you last night, which explains the names from the locker room.\n\n" +
           "You try to deside between going back to sleep or going to eat breakfast..",
           new List<Scene>() { },
           new List<Option>() {
                    //new("Go back to sleep", () => Program.Main()),
                    new ("Go eat pancakes", () => Environment.Exit(0)) },
            new List<Item>() { });
        public static Scene GetKey = new("Get Key", "Wizard", wizard, 
                new List<Scene>() { },
                new List<Option>(),
                new List<Item>() { }
            );
        #endregion
        #region Wizard
        private static string wizard = @"
                    ____ 
                  .'* *.'
               __/_*_*(_
              / _______ \
             _\_)/___\(_/_ 
            / _((\- -/))_ \
            \ \())(-)(()/ /
             ' \(((()))/ '
            / ' \)).))/ ' \
           / _ \ - | - /_  \
          (   ( .;''';. .'  )
          _\""__ /    )\ __""/_
            \/  \   ' /  \/
             .'  '...' ' )
              / /  |  \ \
             / .   .   . \
            /   .     .   \
           /   /   |   \   \
         .'   /    b    '.  '.
     _.-'    /     Bb     '-. '-._ 
 _.-'       |      BBb       '-.  '-. 
(________mrf\____.dBBBb.________)____)";

        private static Scene reachANumberGame = new("Reach a number", "Rules - Reach  a  number",
                "The Great Wizard has captured your parents and the only way to free them is by completing his DIE HARD CHALLENGE.\n" +
                "This challenge needs to be completed within 7 tries, BUT not all tries needs to be used, for the freedom of your parents.\n" +
                "To win the game you have to add, subtract and use the witts of Mathematics, The Great Wizards only accepts intellectual beings, no mere human can complete this.\n\n" +
                "Now you have to chose from a list of numbers, filled with new numbers each round, to use with the given operators.\n" +
                "The operators is already planed for you. - read the game design CAREFULLY",
                new List<Scene>() { },
                new List<Option>() { new("Start game", () => Program.StartGame()) },
            new List<Item>() { });

        public static Scene MeetTheWizard = new("Meet the old man", "Wizard",
                Program.wizard,
                new List<Scene>() { Program.reachANumberGame },
                new List<Option>(),
                new List<Item>() { });

        #endregion

        public static void Main(string[] args)
        {
            #region Used to maximise Window
            Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
            ShowWindow(ThisConsole, 3);
            #endregion

            Item goldCoin = new("Gold Coin", "A coin with seemingly no use");
            Item flashlight = new("Flashlight", "A flashlight! Maybe it can be used to illuminate a dark room");
            Item key = new("Old key", "This key unlocks the room with your parents");


            #region Great Hall
            Scene greatHall = new("Go to door in front of you", "The Hall Of Greatness",
                "Description here",
                new List<Scene>() { },
                new List<Option>() { },
                new List<Item>() { },
                isLocked: LockType.Locked);
            greatHall.Options = new List<Option> { new(
                "Open Inventory",
                () => Player.OpenInventory(greatHall)
                ) };
            #endregion

            #region Church
            Scene church4 = new("", "Church",
                "You've gotten the key to rescue your parents!",
                new List<Scene>() { },
                new List<Option>() { },
                new List<Item>() { },
                type: SceneType.SubScene);
            church4.Options = new List<Option> { new(
                "Open Inventory", () => Player.OpenInventory(church4) ) };

            Scene church3 = new("", "Church",
                "Finding no other items worth investigating you try to find any place where \"The Greatest Wizard of All\"\ncould've hidden your parents.\n" +
                "You see a doorway hidden sligthly to the left of the alter",
                new List<Scene>() { },
                new List<Option>() { },
                new List<Item>() { },
                type: SceneType.SubScene);
            church3.Options = new List<Option> { new(
                "Open Inventory", () => Player.OpenInventory(church3) ) };

            Scene church2 = new("", "Church",
                "It's a big coin!",
                new List<Scene>() { },
                new List<Option>() { },
                new List<Item>() { goldCoin },
                type: SceneType.SubScene);

            Scene church = new("Go to door on the left", "Church",
                "Standing in the middle of the pews looking towards the alter, you see one of your family photos... Weird..\nIgnoring it you see something shiny on one of the pews.",
                new List<Scene>() { },
                new List<Option>() { }, new List<Item>() { });
            church.Options = new List<Option> {
                    new("Continue looking for things", ()=> church3.DisplayScene()),
                    new("Examine the shiny thing", ()=> church2.DisplayScene()),
                    new("Open Inventory", () => Player.OpenInventory(church)) };
            #endregion

            #region Locker room
            Scene lockerroom3 = new("", "Locker room",
                "There's seemingly not anything else you can find of use in here.",
                new List<Scene>() { },
                new List<Option>() { },
                new List<Item>() { },
                type: SceneType.SubScene);
            lockerroom3.Options = new List<Option> { new(
                "Open Inventory",
                () => Player.OpenInventory(lockerroom3)
                ) };

            Scene lockerroom2 = new("", "Locker room",
                "In the cubby of the miner \"Dopey\" you find a flaslight.",
                new List<Scene>() { },
                new List<Option>() { },
                new List<Item>() { flashlight },
                type: SceneType.SubScene);

            Scene lockerroom = new("Go to door on the right", "Locker room",
                "It looks like this locker room belongs to a bunch of miners. You see the names \"Bashful\" and \"Grumpy\" on some of the cubbies, odd names.. They remind you of something... You can't remember what...",
                new List<Scene>() { },
                new List<Option>()
                {
                }, new List<Item>() { });
            lockerroom.Options = new List<Option> {
                new("See if you can find anything useful", ()=> lockerroom2.DisplayScene()),
                new("Open Inventory", () => Player.OpenInventory(lockerroom)) };
            #endregion

            Scene darkRoom = new("Enter dark room", "Dark Room", "Using the flashlight you see an old looking man standing near a blackboard.\n",
                new List<Scene>() { church3, Program.MeetTheWizard }, new List<Option>(), new List<Item>(), LockType.TooDark);


            Scene cave = new("Go inside cave", "Cave",
                "Inside the cave you see more of the spiders crawling under a big door in front of you.\nLooking around you see two others open doorways to each side of you.",
                new List<Scene>() { church, lockerroom, greatHall },
                new List<Option>() { },
                new List<Item>() { });
            cave.Options = new List<Option> { new(
                "Open Inventory",
                () => Player.OpenInventory(cave)
                ) };

            #region Prologue
            Scene prolgue = new("", "Prologue",
                "You suddenly find yourself standing outside a dark ominous looking cave, with small (adorable?) spiders crawling near the entrance.\n" +
                "You spot a note:\n\n\n" +
                "Adventurer,\n" +
                "I the GREATEST wizard of all, have your parents hidden away in my cave.\n" +
                "To find them, you must find a KEY... but where?\n\n" +
                "Good luck...",
                new List<Scene>() { cave },
                new List<Option>() { new("Leave the cave alone", () => Environment.Exit(0)) },
                new List<Item> { },
                type: SceneType.None);
            #endregion

            church.NearestScenes = new() { cave };
            church2.NearestScenes = new() { cave };
            church2.Options = new() {
                new("Pick it up", () => Program.PickUpItem(goldCoin, church2, null, church3, church)),
                    new("Leave the coin", ()=> church3.DisplayScene()),
                    new("Open Inventory", () => Player.OpenInventory(church2))
            };
            church4.NearestScenes = new() { cave };
            church3.NearestScenes = new() { darkRoom, cave };
            lockerroom.NearestScenes = new() { cave };
            lockerroom2.NearestScenes = new() { cave };
            lockerroom2.Options = new() {
                new("Pick it up", () => Program.PickUpItem(flashlight, lockerroom2, darkRoom, lockerroom3, lockerroom)),
                    new("Leave the flashlight", ()=> lockerroom3.DisplayScene()),
                    new("Open Inventory", () => Player.OpenInventory(lockerroom2))};
            lockerroom3.NearestScenes = new() { cave };
            greatHall.NearestScenes = new() { cave };

            Program.MeetTheWizard.CharacterDialog = "Hello Adventurer..\n" +
                "To get the key, to unlock the room with your parents, you'll have to defeat my challenge..\n" +
                "I wish you luck";
            Program.MeetTheWizard.Options = new() { new("Open Inventory", () => Player.OpenInventory(MeetTheWizard)) };

            Program.GetKey.CharacterDialog = "Adventurer,\nYou are smarter than i anticipated, you have defeated my challenge\nCongratulations, here's the key, now find the room with your parents.";
            Program.GetKey.Options = new() { new("Take Key", () => Program.PickUpItem(key, GetKey, greatHall, church4, darkRoom)) };
            Program.GetKey.Items = new() { key };
            prolgue.DisplayScene();

            Console.ReadLine();
        }

        public static void StartGame()
        {
            ReachANumber reachANumber = new();
            reachANumber.SelectNumber();
        }

        public static void PickUpItem(Item item, Scene scene, Scene? sceneWhichUsesItem, Scene nextScene, Scene parentScene)
        {
            Player.Inventory.Add(item);
            scene.Items.Remove(item);
            if (sceneWhichUsesItem != null)
                sceneWhichUsesItem.Lock = LockType.Unlocked;
            parentScene = nextScene;
            nextScene.DisplayScene();
        }
    }
}
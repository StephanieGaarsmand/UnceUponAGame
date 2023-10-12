using Figgle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OnceUpenAGame
{
    public class Scene
    {
        List<Option> options = new();
        List<Option> optionsAndScenes = new();
        private string navigationName;
        public string Name;
        public string Description;
        public string CharacterDialog = "";
        public LockType Lock;
        private List<Scene> nearestScenes = new();
        private SceneType type;
        public Scene? PreviousScene;

        public List<Item> Items;

        public List<Scene> NearestScenes
        {
            get
            {
                return nearestScenes;
            }
            set
            {
                nearestScenes = value;
                foreach (Scene scene in value)
                {
                    this.optionsAndScenes.Add(new(scene.NavigationName, () => scene.GoToScene(this)));
                }

                GoToGoesLastButBeforeOpenInventory();
            }
        }
        public string NavigationName
        {
            get { return navigationName; }
            set
            {
                navigationName = value;
                optionsAndScenes.Clear();
                NearestScenes = NearestScenes;
                Options = Options;
            }
        }

        public List<Option> Options
        {
            get { return options; }
            set
            {
                options = value;
                value.ForEach(optionsAndScenes.Add);
                GoToGoesLastButBeforeOpenInventory();
            }
        }
        public Scene(string navigationName, string name, string description, List<Scene> nearestScenes, List<Option> options, List<Item> items, LockType isLocked = LockType.Unlocked, SceneType type = SceneType.MainScene, Scene? PreviousScene = null)
        {
            NavigationName = navigationName;
            Name = name;
            Description = description;
            this.nearestScenes = nearestScenes;
            this.type = type;
            Lock = isLocked;
            this.PreviousScene = PreviousScene;

            Items = items;

            NearestScenes = nearestScenes;
            Options = options;

            //foreach (Scene scene in nearestScenes)
            //{
            //    this.optionsAndScenes.Add(new(scene.NavigationName, () => scene.GoToScene(this)));
            //}

            //options.ForEach(this.optionsAndScenes.Add);
            GoToGoesLastButBeforeOpenInventory();
        }

        public void OpenInventoryGoesLast()
        {
            Option oi = optionsAndScenes.FirstOrDefault(o => o.Name == "Open Inventory");
            if (oi != null)
            {
                optionsAndScenes.Remove(oi);
                optionsAndScenes.Add(oi);
            }
        }

        private void GoToGoesLastButBeforeOpenInventory()
        {
            var gt = optionsAndScenes.Where(o => o.Name.Contains("Go to")).ToList();
            foreach (var g in gt)  
            { 
                optionsAndScenes.Remove(g);
                optionsAndScenes.Add(g);
            }
            OpenInventoryGoesLast();
        }

        public void GoToScene(Scene PreviousScene)
        {
            this.PreviousScene = PreviousScene;
            DisplayScene();
        }

        public void DisplayScene()
        {
            Console.Clear();
            
            if (Lock == LockType.Unlocked || Lock == LockType.Inventory)
            {
                if (type != SceneType.MinigameIntro)
                {
                    this.NavigationName = $"Go to {Name}";
                }
                int index = 0;
                // Write the menu out
                WriteOptions(optionsAndScenes[index]);

                // Store key info in here
                ConsoleKeyInfo keyinfo;
                do
                {
                    keyinfo = Console.ReadKey();

                    // Handle each key input (down arrow will write the menu again with a different selected item)
                    if (keyinfo.Key == ConsoleKey.DownArrow)
                    {
                        if (index + 1 < optionsAndScenes.Count)
                        {
                            index++;
                            WriteOptions(optionsAndScenes[index]);
                        }
                    }
                    if (keyinfo.Key == ConsoleKey.UpArrow)
                    {
                        if (index - 1 >= 0)
                        {
                            index--;
                            WriteOptions(optionsAndScenes[index]);
                        }
                    }
                    // Handle different action for the option
                    if (keyinfo.Key == ConsoleKey.Enter)
                    {
                        optionsAndScenes[index].Selected.Invoke();
                        index = 0;
                    }
                }
                while (keyinfo.Key != ConsoleKey.X);
                Console.ReadKey();
            }
            else if (Lock == LockType.Locked)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("The door is locked, try to find a key.");
                Console.ForegroundColor = ConsoleColor.White;
                Thread.Sleep(1000);
                Console.ReadKey();
                PreviousScene.DisplayScene();
            }
            else if (Lock == LockType.TooDark)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("The room is too dark, try to find a flashlight.");
                Console.ForegroundColor = ConsoleColor.White;
                Thread.Sleep(1000);
                Console.ReadKey();
                PreviousScene.DisplayScene();
            }
        }
        void WriteOptions(Option selectedOption)
        {
            optionsAndScenes = optionsAndScenes.Distinct().ToList();
            Console.SetCursorPosition(0, 0);
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine(FiggleFonts.Epic.Render(Name));

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"\n{Description}\n\n");
            Console.ForegroundColor = ConsoleColor.White;

            if (CharacterDialog != String.Empty)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"{CharacterDialog}\n\n");
                Console.ForegroundColor = ConsoleColor.White;
            }
            foreach (Option option in optionsAndScenes)
            {
                if (option == selectedOption)
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write("<... ");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                {
                    Console.Write("     ");
                }

                Console.Write(option.Name);

                if (option == selectedOption)
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

    }
}

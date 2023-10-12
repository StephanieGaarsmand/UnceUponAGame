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
        Scene? previousScene;
        List<Option> options = new();
        public string Name;
        public string Description;
        bool locked;

        public Scene(string name, string description, List<Scene> nearestScenes, List<Option> options, bool locked = false, Scene? previousScene = null)
        {
            Name = name;
            Description = description;
            this.locked = locked;
            this.previousScene = previousScene;

            foreach (Scene scene in nearestScenes)
            {
                this.options.Add(new($"Go to {scene.Name}", () => scene.GoToScene(this)));
            }

            options.ForEach(this.options.Add);
            this.locked = locked;
        }

        public void GoToScene(Scene previousScene)
        {
            this.previousScene = previousScene;
            DisplayScene();
        }

        public void DisplayScene()
        {
            if (!locked)
            {
                int index = 0;
                // Write the menu out
                WriteOptions(options[index]);

                // Store key info in here
                ConsoleKeyInfo keyinfo;
                do
                {
                    keyinfo = Console.ReadKey();

                    // Handle each key input (down arrow will write the menu again with a different selected item)
                    if (keyinfo.Key == ConsoleKey.DownArrow)
                    {
                        if (index + 1 < options.Count)
                        {
                            index++;
                            WriteOptions(options[index]);
                        }
                    }
                    if (keyinfo.Key == ConsoleKey.UpArrow)
                    {
                        if (index - 1 >= 0)
                        {
                            index--;
                            WriteOptions(options[index]);
                        }
                    }
                    // Handle different action for the option
                    if (keyinfo.Key == ConsoleKey.Enter)
                    {
                        options[index].Selected.Invoke();
                        index = 0;
                    }
                }
                while (keyinfo.Key != ConsoleKey.X);
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("The door is locked, try to find a key.");
                Thread.Sleep(3000);
                previousScene.DisplayScene();
            }
        }
        void WriteOptions(Option selectedOption)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine(FiggleFonts.Epic.Render(Name));

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"\n{Description}\n\n");
            Console.ForegroundColor = ConsoleColor.White;
            foreach (Option option in options)
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

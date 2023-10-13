using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace OnceUpenAGame
{
    public static class Player
    {
        public static List<Item> Inventory = new();
        public static void OpenInventory(Scene prev)
        {
            Scene inventory = new("Open Inventory", "Inventory", InventoryString(), new List<Scene>(), new List<Option>(), new List<Item>(), LockType.Inventory);
            inventory.Options = new() { new("Exit Inventory", () => prev.DisplayScene()) };
            inventory.DisplayScene();
        }

        public static string InventoryString()
        { 
            string s = "";
            foreach (Item item in Inventory)
            {
                s += $"{item.Name} - {item.Description}\n";
            }
            return s;
        }
    }
}

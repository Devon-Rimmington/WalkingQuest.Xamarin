
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;

namespace WalkingQuest.Droid.Database.Objects
{
    public class Inventory
    {
        private List<Item> Items { get; }

        public Inventory()
        {
            Items = new List<Item>();
        }

        public Inventory(IEnumerable<Item> items)
        {
            this.Items = new List<Item>();
            foreach (var item in items)
            {
                this.Items.Add(item);
            }
        }

        public void addItem(Item item)
        {
            if (item != null)
            {
                Items.Add(item);
            }
        }

        public void addItems(IEnumerable<Item> items)
        {
            foreach(var item in items)
            {
                this.Items.Add(item);
            }
        }

        public void removeItem(Item item)
        {
            this.Items.Remove(item);
        }
    }
}
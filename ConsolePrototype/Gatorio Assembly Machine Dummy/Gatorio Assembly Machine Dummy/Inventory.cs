using System;
using System.Collections.Generic;

namespace Gatorio_Assembly_Machine_Dummy
{
    public class Inventory
    {
        private string ownerName;
        private List<Item> myItems;
        public List<Item> MyItems => myItems;

        public Inventory(string ownerName)
        {
            this.ownerName = ownerName;
            myItems = new List<Item>();
        }

        public void AddItem(Item item, int amount = 1)
        {
            if (item != Item.None)
            {
                for (int i = 0; i < amount; i++)
                {
                    myItems.Add(item);
                }

                Console.WriteLine($"Item {item} {amount} mal ins Inventar von {ownerName} hinzugefügt");
            }
        }

        public void AddItems(List<Item> items)
        {
            foreach (Item item in items)
            {
                AddItem(item);
            }
        }

        public void AddItems(KeyValuePair<Item, uint> items)
        {
            for (int i = 0; i < items.Value; i++)
            {
                AddItem(items.Key);
            }
        }

        public Item GetItem(Item item)
        {
            if (myItems.Contains(item))
            {
                myItems.Remove(item);
                return item;
            }
            else
            {
                return Item.None;
            }
        }

        public override string ToString()
        {
            myItems.Sort();
            return "Items im Inventar von " + ownerName + ": " + String.Join(',', myItems).Replace(",", ", ");
        }
    }
}
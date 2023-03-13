using System.Collections.Generic;

namespace Gatorio_Assembly_Machine_Dummy
{
    public struct ItemStack
    {
        public Item ItemName { get; }
        public int ItemAmount { get; private set; }
        public int ItemMax { get; }

        
        public ItemStack(Item name, int maxAmount)
        {
            ItemName = name;
            ItemMax = maxAmount;
            ItemAmount = 0;
        }


        public bool IsStackFull()
        {
            return ItemAmount == ItemMax;
        }


        public int AddItemToStack(KeyValuePair<Item, int> item)
        {
            int newStackAmount = ItemAmount + item.Value;
            if (newStackAmount <= ItemMax)
            {
                return 0;
            }
            else
            {
                ItemAmount = ItemMax;
                return newStackAmount - ItemMax;
            }
        }

        public int AddItemToStack(Item item)
        {
            return AddItemToStack(new KeyValuePair<Item, int>(item, 1));
        }

        public int AddItemToStack(Item item, int amount)
        {
            return AddItemToStack(new KeyValuePair<Item, int>(item, amount));
        }
        

        public int RemoveItemFromStack(KeyValuePair<Item, int> item)
        {
            int newStackAmount = ItemAmount - item.Value;
            if (newStackAmount <= ItemMax)
            {
                return 0;
            }
            else
            {
                ItemAmount = ItemMax;
                return newStackAmount - ItemMax;
            }
        }

        
        public override string ToString()
        {
            return ItemName.ToString();
        }
    }
}
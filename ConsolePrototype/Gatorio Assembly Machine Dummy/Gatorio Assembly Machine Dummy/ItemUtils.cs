using System.Collections.Generic;
using System.Linq;

namespace Gatorio_Assembly_Machine_Dummy
{
    public static class ItemUtils
    {
        public static List<Item> GetBasicItems(List<Item> items)
        {
            List<Item> itemList = new List<Item>(items);
            bool stillHasRecipe = false;
            
            do
            {
                for (int i = itemList.Count - 1; i >= 0; i--)
                {
                    stillHasRecipe = false;
                    Recipe testedItem = Recipe.GetRecipeFor(itemList[i]);
                    if (testedItem != null)
                    {
                        stillHasRecipe = true;
                        itemList.AddRange(testedItem.Ingredients);
                        itemList.Remove(itemList[i]);
                        break;
                    }
                }
            } while (stillHasRecipe);

            return itemList;
        }


        public static List<Item> GetBasicItems(Recipe recipe)
        {
            return GetBasicItems(recipe.Ingredients);
        }

        public static List<Item> GetBasicItems(Inventory inventory)
        {
            return GetBasicItems(inventory.MyItems);
        }


        public static int GetAmountOfItemsToProduce(Inventory inventory, Recipe recipe)
        {
            List<Item> basicsInventory = new List<Item>(GetBasicItems(inventory));
            List<Item> basicsRecipe = new List<Item>(GetBasicItems(recipe));
            int amount = 0;
            bool canBeProduced = false;
            
            do
            {
                foreach (Item basicsRecipeItem in basicsRecipe)
                {
                    if (!basicsInventory.Remove(basicsRecipeItem))
                    {
                        canBeProduced = false;
                        break;
                    }
                    canBeProduced = true;
                }

                if (canBeProduced)
                {
                    amount++;
                }
            } while (canBeProduced);
            
            return amount;
        }


        public static Dictionary<Item, int> ItemListToDictionary(List<Item> itemList)
        {
            Dictionary<Item, int> newBasicItemDict = new Dictionary<Item, int>();
            foreach (Item item in itemList)
            {
                if (!newBasicItemDict.ContainsKey(item))
                {
                    newBasicItemDict.Add(item, 1);
                }
                else
                {
                    newBasicItemDict[item]++;
                }
            }

            return newBasicItemDict;
        }


        public static List<Item> ItemCollectionToList(Dictionary<Item, int> itemDictionary)
        {
            List<Item> newItemList = new List<Item>();
            foreach (KeyValuePair<Item, int> itemPair in itemDictionary)
            {
                newItemList.AddRange(Enumerable.Repeat(itemPair.Key, itemPair.Value));
            }

            return newItemList;
        }
    }
}
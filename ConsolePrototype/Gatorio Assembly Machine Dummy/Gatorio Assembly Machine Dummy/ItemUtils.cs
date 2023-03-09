using System.Collections.Generic;
using System.Linq;

namespace Gatorio_Assembly_Machine_Dummy
{
    public static class ItemUtils
    {
        public static void GetBasicItems(ref Dictionary<Item, int> items, out Dictionary<Item, int> basicItems)
        {
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
        }


        public static Dictionary<Item, int> GetBasicItems(Recipe recipe)
        {
            return GetBasicItems(recipe.Ingredients);
        }

        public static Dictionary<Item, int> GetBasicItems(Inventory inventory)
        {
            return GetBasicItems(inventory.MyItems);
        }


        public static void AddToItemDict(ref Dictionary<Item, int> source, KeyValuePair<Item, int> item)
        {
            if (!source.ContainsKey(item.Key))
            {
                source.Add(item.Key, item.Value);
            }
            else
            {
                source[item.Key] += item.Value;
            }
        }
        
        
        public static void AddToItemDict(ref Dictionary<Item, int> source, Item item)
        {
            AddToItemDict(ref source, new KeyValuePair<Item, int>(item, 1));
        }

        
        public static void AddToItemDict(ref Dictionary<Item, int> source, Dictionary<Item, int> items)
        {
            foreach (KeyValuePair<Item, int> itemPair in items)
            {
                AddToItemDict(ref source, itemPair);
            }
        }

        
        public static bool SubFromItemDict(ref Dictionary<Item, int> source, KeyValuePair<Item, int> item)
        {
            if (source.ContainsKey(item.Key))
            {
                source[item.Key] -= item.Value;
                if (source[item.Key] >= 0)
                {
                    if (source[item.Key] == 0)
                    {
                        source.Remove(item.Key);
                    }
                    return true;
                }

                source[item.Key] += item.Value;
            }
            
            return false;
        }

        public static bool SubFromItemDict(ref Dictionary<Item, int> source, Item item)
        {
            return SubFromItemDict(ref source, new KeyValuePair<Item, int>(item, 1));
        }

        public static bool SubFromItemDict(ref Dictionary<Item, int> source, Dictionary<Item, int> items)
        {
            bool canContinue = false;
            
            foreach (KeyValuePair<Item, int> itemPair in source)
            {
                if (SubFromItemDict(ref source, itemPair))
                {
                    canContinue = true;
                }
                else
                {
                    canContinue = false;
                    break;
                }
            }

            return canContinue;
        }
        

        // TODO fix for dictionary structure
        public static int GetAmountOfItemsToProduce(Inventory inventory, Recipe recipe)
        {
            Dictionary<Item, int> basicsInventory = new Dictionary<Item, int>(GetBasicItems(inventory));
            Dictionary<Item, int> basicsRecipe = new Dictionary<Item, int>(GetBasicItems(recipe));
            int amount = 0;
            bool canBeProduced = false;

            do
            {
                foreach (KeyValuePair<Item, int> basicsRecipeItem in basicsRecipe)
                {
                }
            } while (canBeProduced);

            return amount;
        }
    }
}
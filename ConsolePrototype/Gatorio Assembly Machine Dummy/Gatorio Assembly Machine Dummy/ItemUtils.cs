using System.Collections.Generic;

namespace Gatorio_Assembly_Machine_Dummy
{
    public abstract class ItemUtils
    {
        
        protected static List<Item> GetBasicItems(List<Item> items)
        {
            List<Item> basicItemList = new List<Item>(items);
            bool stillHasRecipe = false;

            do
            {
                for (int i = basicItemList.Count - 1; i >= 0; i--)
                {
                    stillHasRecipe = false;
                    Recipe testedItem = Recipe.GetRecipeFor(basicItemList[i]);
                    if (testedItem != null)
                    {
                        stillHasRecipe = true;
                        basicItemList.AddRange(items);
                        basicItemList.Remove(basicItemList[i]);
                        break;
                    }
                }
            } while (stillHasRecipe);

            return basicItemList;
        }
        
        
        protected static Dictionary<Item, int> GetBasicItems(Recipe recipe)
        {
            List<Item> recipeIngredients = new List<Item>(recipe.Ingredients);
            Dictionary<Item, int> newBasicItemDict = new Dictionary<Item, int>();

            foreach (Item item in recipeIngredients)
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
    }
}
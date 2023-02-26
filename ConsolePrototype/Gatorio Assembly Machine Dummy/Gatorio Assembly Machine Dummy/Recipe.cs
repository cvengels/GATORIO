using System;
using System.Collections.Generic;
using System.Linq;

namespace Gatorio_Assembly_Machine_Dummy
{
    public class Recipe : IComparable<Recipe>
    {
        private static List<Item> nonSelfCraftableItems; // Which items can't be crafted by the player?
        private static List<Recipe> myRecipes;

        private List<Item> ingredients;
        private Item product;
        private Dictionary<Item, int> basicIngredients;

        public static List<Item> NonSelfCraftableItems => nonSelfCraftableItems;
        public static List<Recipe> MyRecipes => myRecipes;
        public List<Item> Ingredients => ingredients;
        public Item Product => product;

        
        public Recipe(Item product, params object[] ingredients)
        {
            this.ingredients = new List<Item>();
            
            if (myRecipes == null)
            {
                myRecipes = new List<Recipe>();
                nonSelfCraftableItems = new List<Item>();
            }

            if (ingredients != null && product != Item.None)
            {
                if (myRecipes.FirstOrDefault(r => r.product == product) == null)
                {
                    basicIngredients = new Dictionary<Item, int>();

                    foreach (var ingredient in ingredients)
                    {
                        if (ingredient is Item)
                        {
                            this.ingredients.Add((Item)ingredient);
                        }

                        else if (ingredient is KeyValuePair<Item, int>)
                        {
                            for (int i = 0; i < ((KeyValuePair<Item, int>)ingredient).Value; i++)
                            {
                                this.ingredients.Add(((KeyValuePair<Item, int>) ingredient).Key);
                            }
                        }
                    }
                    
                    this.ingredients.Sort();
                    this.product = product;

                    myRecipes.Add(this);
                    myRecipes.Sort();
                }
                else
                {
                    throw new ArgumentException(
                        $"Rezept für das Endprodukt {this.product.ToString()} bereits vorhanden!");
                }
            }
            else
            {
                throw new ArgumentException("Rezept muss mindestens eine Zutat und ein Endprodukt enthalten!");
            }
        }

        public static void DeclareItemsNotSelfCraftable(params Item[] myItems)
        {
            if (myItems.Length > 0)
            {
                foreach (var item in myItems)
                {
                    if (!nonSelfCraftableItems.Contains(item))
                    {
                        nonSelfCraftableItems.Add(item);
                    }
                }

                nonSelfCraftableItems = nonSelfCraftableItems.OrderBy(item => item.ToString()).ToList();
            }
        }

        public static Recipe GetRecipeFor(Item product)
        {
            Recipe recipeForItem = myRecipes.FirstOrDefault(r => r.product == product);
            if (recipeForItem != null && !nonSelfCraftableItems.Contains(product))
            {
                // Item has a recipe ...
                return recipeForItem;
            }

            // ... or not (basic ingredient)
            return null;
        }

        public void WriteBasicRecipe()
        {
            string basicItemList = product + ": ";

            List<string> individualIngredients = new List<string>();
            foreach (var basicIngredient in basicIngredients)
            {
                individualIngredients.Add(basicIngredient.Value + "x " + basicIngredient.Key);
            }

            basicItemList += String.Join(',', individualIngredients).Replace(",", ", ");

            Console.WriteLine(basicItemList);
        }

        public static void CalculateBasicItems()
        {
            foreach (Recipe mainRecipe in myRecipes)
            {
                List<Item> basicItemList = new List<Item>(mainRecipe.ingredients);
                bool stillHasRecipe = false;

                do
                {
                    for (int i = basicItemList.Count - 1; i >= 0; i--)
                    {
                        stillHasRecipe = false;
                        Recipe testedItem = GetRecipeFor(basicItemList[i]);
                        if (testedItem != null)
                        {
                            stillHasRecipe = true;
                            basicItemList.AddRange(testedItem.ingredients);
                            basicItemList.Remove(basicItemList[i]);
                            break;
                        }
                    }
                } while (stillHasRecipe);

                foreach (Item item in basicItemList)
                {
                    if (!mainRecipe.basicIngredients.ContainsKey(item))
                    {
                        mainRecipe.basicIngredients.Add(item, 1);
                    }
                    else
                    {
                        mainRecipe.basicIngredients[item]++;
                    }
                }
            }
        }

        public static void PrintRecipes()
        {
            Console.WriteLine("Rezepte: ");

            foreach (Recipe recipe in myRecipes)
            {
                string itemString = "\t- " + recipe.product + " = ";
                IOrderedEnumerable<KeyValuePair<Item, int>> ingredientDict = recipe.ingredients.
                    GroupBy(x => x)
                    .ToDictionary(y=>y.Key, y=>y.Count())
                    .OrderByDescending(z => z.Key.ToString());
                List<string> ingredientList = new List<string>();
                foreach (KeyValuePair<Item, int> ingredient in ingredientDict)
                {
                    ingredientList.Add($"{ingredient.Value}x {ingredient.Key}");
                }
                itemString += String.Join('+', ingredientList).Replace("+", " + ");
                
                //itemString += String.Join('+', recipe.ingredients).Replace("+", " + ");
                Console.WriteLine(itemString);

                List<string> basicIngredientList = new List<string>();
                foreach (KeyValuePair<Item, int> basicItem in recipe.basicIngredients)
                {
                    basicIngredientList.Add($"{basicItem.Value}x {basicItem.Key}");
                }

                itemString = String.Join(',', basicIngredientList).Replace(",", ", ");
                Console.WriteLine($"\t\t({itemString})");
            }

            Console.WriteLine();

            if (nonSelfCraftableItems.Count > 0)
            {
                Console.WriteLine("Nicht selbst herstellbare Zutaten: ");
                string itemString = "";
                itemString += String.Join(',', nonSelfCraftableItems).Replace(",", ", ");
                Console.WriteLine(itemString);
            }

            Console.WriteLine();
        }

        public int CompareTo(Recipe other)
        {
            return String.Compare(this.product.ToString(), other.product.ToString(),
                StringComparison.OrdinalIgnoreCase);
        }
    }
}
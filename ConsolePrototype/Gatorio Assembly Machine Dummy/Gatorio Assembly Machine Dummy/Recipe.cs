using System;
using System.Collections.Generic;
using System.Linq;

namespace Gatorio_Assembly_Machine_Dummy
{
    public class Recipe : IComparable<Recipe>
    {
        // Static fields
        private static List<Recipe> myRecipes;
        private static HashSet<Item> nonSelfCraftableItems;

        // Object fields
        private KeyValuePair<Item, int> product;
        private Dictionary<Item, int> ingredients;
        private Dictionary<Item, int> basicIngredients;
        
        // Properties
        public static List<Recipe> MyRecipes => myRecipes;
        public static HashSet<Item> NonSelfCraftableItems => nonSelfCraftableItems;
        public KeyValuePair<Item, int> Product => product;
        public Dictionary<Item, int> Ingredients => ingredients;
        public Dictionary<Item, int> BasicIngredients => basicIngredients;


        public Recipe(Item product, params object[] ingredients)
        {
            this.ingredients = new Dictionary<Item, int>();

            // initialize static fields
            if (myRecipes == null)
            {
                myRecipes = new List<Recipe>();
                nonSelfCraftableItems = new HashSet<Item>();
            }

            if (ingredients != null && product != Item.None)
            {
                if (myRecipes.FirstOrDefault(r => r.product.Key == product) == null)
                {
                    basicIngredients = new Dictionary<Item, int>();

                    foreach (var ingredient in ingredients)
                    {
                        if (ingredient is Item)
                        {
                            ItemUtils.AddToItemDict(ref this.ingredients, (Item)ingredient);
                        }

                        else if (ingredient is KeyValuePair<Item, int>)
                        {
                            ItemUtils.AddToItemDict(ref this.ingredients, (KeyValuePair<Item, int>)ingredient);
                        }

                        else if (ingredient is Dictionary<Item, int>)
                        {
                            ItemUtils.AddToItemDict(ref this.ingredients, (Dictionary<Item, int>)ingredient);
                        }
                    }

                    this.product = new KeyValuePair<Item, int>(product, 1);

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
        
        public Recipe(KeyValuePair<Item, int> product, params object[] ingredients) : this(product.Key, ingredients)
        {
            this.product = product;
        }
        
        
        public Recipe(Item product, int quantity, params object[] ingredients) : this(product, ingredients)
        {
            this.product = new KeyValuePair<Item, int>(product, quantity);
        }
        
        
        public int CompareTo(Recipe other)
        {
            return String.Compare(this.product.Key.ToString(), other.product.Key.ToString(),
                StringComparison.OrdinalIgnoreCase);
        }
        
        
        public static void DeclareItemsNotSelfCraftable(params Item[] basicItem)
        {
            foreach (Item item in nonSelfCraftableItems)
            {
                nonSelfCraftableItems.Add(item);
            }
        }
    }
}
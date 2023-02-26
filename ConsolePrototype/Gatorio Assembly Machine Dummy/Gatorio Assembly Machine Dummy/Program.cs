
using System.Collections.Generic;

namespace Gatorio_Assembly_Machine_Dummy
{
    class Program
    {
        static void Main(string[] args)
        {
            new Recipe(Item.MetalSheet, Item.MetalOre);
            new Recipe(Item.Paper, Item.Wood, Item.Water);
            new Recipe(Item.TinCan, Item.MetalSheet, Item.Paper);
            new Recipe(Item.CannedFish, Item.TinCan, Item.Water, Item.Fish);
            new Recipe(Item.CardboardBox, KeyValuePair.Create(Item.Paper, 10), Item.Water);
            new Recipe(Item.PackOfCannedFish, KeyValuePair.Create(Item.CannedFish, 36), Item.CardboardBox);

            //Recipe.PrintRecipes();

            Recipe.DeclareItemsNotSelfCraftable(Item.MetalOre);

            Recipe.CalculateBasicItems();
            
            Recipe.PrintRecipes();

            /*
            Console.WriteLine("Benötigte Grundzutaten für alle Rezepte:");
            foreach (var myRecipe in Recipe.MyRecipes)
            {
                myRecipe.WriteBasicRecipe();
            }
            */

            Inventory myInventory = new Inventory("Player");

            /*
            AssemblyMachine metalsheetFabricator = new AssemblyMachine(2, Recipe.GetRecipeFor(Item.MetalSheet));
            metalsheetFabricator.InputIngredient(Item.MetalOre, 10);
            Console.WriteLine(metalsheetFabricator.ToString());
            
            
            myInventory.AddItem(metalsheetFabricator.Produce());
            Console.WriteLine(myInventory);
            Console.WriteLine();

            
            AssemblyMachine paperSheetMaker = new AssemblyMachine(2, Recipe.GetRecipeFor(Item.Paper));
            Console.WriteLine(paperSheetMaker.ToString());
            paperSheetMaker.InputIngredient(Item.Wood, 10);
            myInventory.AddItem(paperSheetMaker.Produce());
            paperSheetMaker.InputIngredient(Item.Water, 10);
            myInventory.AddItem(paperSheetMaker.Produce());
            Console.WriteLine(myInventory);
            Console.WriteLine();
            

            AssemblyMachine tinCanMaker = new AssemblyMachine(2, Recipe.GetRecipeFor(Item.TinCan));
            AssemblyMachine fishFoodMaker = new AssemblyMachine(3, Recipe.GetRecipeFor(Item.CannedFish));

            tinCanMaker.InputIngredient(metalsheetFabricator.Produce());
            tinCanMaker.InputIngredient(Item.Paper);
            myInventory.AddItem(Item.Water, 2);
            myInventory.AddItem(Item.Fish, 2);
            
            fishFoodMaker.InputIngredients(new List<Item>()
            {
                myInventory.GetItem(Item.Water),
                tinCanMaker.Produce(),
                myInventory.GetItem(Item.Fish)
            });
            myInventory.AddItem(fishFoodMaker.Produce());
            Console.WriteLine(myInventory);
            */
        }
    }
}
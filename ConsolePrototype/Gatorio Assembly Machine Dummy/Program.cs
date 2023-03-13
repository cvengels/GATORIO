using System;
using System.Collections.Generic;
using System.Diagnostics;

// ReSharper disable ObjectCreationAsStatement

namespace Gatorio_Assembly_Machine_Dummy
{
    class Program
    {
        static void Main(string[] args)
        {
            new Recipe(Item.MetalSheet, Item.MetalOre);
            new Recipe(Item.Paper, 2, Item.Wood, Item.Water);
            new Recipe(Item.TinCan, Item.MetalSheet, Item.Paper);
            new Recipe(Item.CannedFish, Item.TinCan, Item.Water, Item.Fish);
            new Recipe(Item.CannedFishPremium, Item.TinCan, Item.Water, Item.Fish, Item.CatNip);
            new Recipe(Item.CardboardBox, KeyValuePair.Create(Item.Paper, 4), Item.Water);
            new Recipe(Item.PackOfCannedFish, KeyValuePair.Create(Item.CannedFish, 36), Item.CardboardBox);
            new Recipe(Item.PackOfCannedFishPremium, KeyValuePair.Create(Item.CannedFishPremium, 12), Item.CardboardBox);

            //Recipe.PrintRecipes();

            Recipe.DeclareItemsNotSelfCraftable(Item.MetalOre);

            Recipe.CalculateBasicItemsForAllRecipes();
            
            Recipe.PrintRecipes();
            
            Inventory myInventory = new Inventory("Player");

            
            /*
            for (int i = 0; i < 1000; i++)
            {
                myInventory.AddItems(new List<Item>
                {
                    Item.Fish, Item.Wood, Item.Water, Item.MetalOre
                }
                );
            }
            
            myInventory.AddItem(Item.CatNip, 120);

            foreach (Recipe recipe in Recipe.MyRecipes)
            {
                int inventoryAmount = ItemUtils.GetAmountOfItemsToProduce(myInventory, recipe);
                Console.WriteLine($"Produkt {recipe.Product.ToString()} {inventoryAmount}x herstellbar mit Inventar von {myInventory.OwnerName}");
            }

            
            
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
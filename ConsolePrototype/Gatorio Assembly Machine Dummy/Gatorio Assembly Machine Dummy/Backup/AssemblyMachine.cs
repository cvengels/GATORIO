using System;
using System.Collections.Generic;
using System.Linq;

namespace Gatorio_Assembly_Machine_Dummy
{
    public class AssemblyMachine
    {
        private static int globalId;
        private int ownID;

        private int stage; // How many input items can this machine process?
        private Inventory myInventory;
        private List<Item> myInputItems;
        private Item myProduct;

        public int OwnId => ownID;
        public int Stage => stage;
        public Inventory MyInventory => myInventory;
        public Item MyProduct => myProduct;

        public AssemblyMachine(int stage)
        {
            ownID = globalId++;
            myInventory = new Inventory($"Automat (ID {ownID})");
            myInputItems = new List<Item>();
            this.stage = stage;
        }

        public AssemblyMachine(int stage, Recipe receipe)
        {
            this.stage = stage;
            if (SetNewReceipe(receipe))
            {
                ownID = globalId++;
                myInventory = new Inventory($"Automat (ID {ownID})");
            }
        }

        public override string ToString()
        {
            return $"Automat (ID {ownID}) produziert {myProduct} und braucht dafür: " +
                   $"{String.Join('+', myInputItems).Replace("+", " + ")}";
        }

        public bool SetNewReceipe(Recipe newReceipe)
        {
            if (stage >= newReceipe.Ingredients.Count)
            {
                myInputItems = new List<Item>();
                this.myInputItems = newReceipe.Ingredients;
                this.myProduct = newReceipe.Product;
                Console.WriteLine(
                    $"Automat (ID {ownID}), Klasse {stage} kann nun {myProduct.ToString()} mit " +
                    $"'{String.Join('+', myInputItems).Replace("+", " + ")}' herstellen."
                );
                return true;
            }

            throw new InvalidOperationException(
                $"Automatentyp Klasse {stage} kann Rezept für {newReceipe.Product.ToString()} " +
                $"({newReceipe.Ingredients.Count.ToString()} Items) nicht verarbeiten"
            );
        }

        public bool InputIngredient(Item item, int amount = 1)
        {
            if (myInputItems.Contains(item))
            {
                myInventory.AddItem(item, amount);
                return true;
            }
            else
            {
                Console.WriteLine($"Item {item.ToString()} kann von Automat (ID {ownID}) nicht verarbeitet werden!");
                Console.WriteLine(
                    $"Gültige Eingabe-Items: {String.Join(',', myInputItems.Distinct()).Replace(",", ", ")}");
                Console.WriteLine();
                return false;
            }
        }

        public void InputIngredients(List<Item> items)
        {
            foreach (var item in items)
            {
                InputIngredient(item);
            }
        }

        public bool CheckItems()
        {
            if (myInventory.MyItems.All(myInputItems.Contains))
            {
                return true;
            }

            return false;
        }

        public Item Produce()
        {
            if (CheckItems())
            {
                foreach (Item item in myInputItems)
                {
                    myInventory.GetItem(item);
                }

                return myProduct;
            }

            Console.WriteLine($"Maschine (ID {ownID}): Nicht genug Items für {myProduct.ToString()} vorhanden");
            return Item.None;
        }
    }
}
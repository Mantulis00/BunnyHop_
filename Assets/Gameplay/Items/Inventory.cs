

using Assets.Memory.Adaptors;
using Assets.Memory.Models;
using UnityEngine;

namespace Assets.Gameplay.Items
{
    public class Inventory
    {

        public int boosts { get; private set; }
        public int jumps { get; private set; }
        public int money { get; private set; }

        public Inventory()
        {
            InventoryModel inventory = InventoryAdaptor.LoadInventory();

            money = inventory.money;
            boosts = inventory.boosts;
            jumps = inventory.jumps;
        }

        public Inventory (Inventory inventory)
        {
            money = inventory.money;
            boosts = inventory.boosts;
            jumps = inventory.jumps;
        }



        public void MoneyUpdate(int money = 1)
        {
            this.money += money;
        }

        public void BoostsUpdate(int boosts = 1)
        {
            this.boosts += boosts;
        }

        public void JumpsUpdate(int jumps = 1)
        {
            this.jumps += jumps;
        }

        public void Save()
        {
            InventoryAdaptor.SaveInventory(money, jumps, boosts);
        }



     
        // boosts, jumps;




    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Gameplay.ScoresManagers
{
    public class ItemsManager
    {

        public Items.Inventory inventory { get; private set; }
        private Items.Inventory ShopCart;
        public bool shopMode { get; private set; }

        public ItemsManager()
        {
            inventory = new Items.Inventory();
        }


        public string GetMoney()
        {
            return inventory.money.ToString();
        }
        public string GetJumps()
        {
            return inventory.jumps.ToString();
        }
        public string GetBoosts()
        {
            return inventory.boosts.ToString();
        }

        public bool UseJump()
        {
            if (inventory.jumps > 0)
            {
                inventory.JumpsUpdate(-1);
                return true;
            }
            return false;
        }

        public bool UseBoost()
        {
            if (inventory.boosts > 0)
            {
                inventory.BoostsUpdate(-1);
                return true;
            }
            return false;
        }

        public void EnterShopMode()
        {
            ShopCart = new Items.Inventory(inventory);
        }
      
        public void LeaveShopMode(bool confirmed)
        {
            if (!confirmed) // if no reset
            {
                inventory = ShopCart;
            }
            else if (ShopCart.money != inventory.money) // if money didn change, nothing to save
            {
                Save();
            }
        }


        public void Save()
        {
            inventory.Save();
        }




    }
}

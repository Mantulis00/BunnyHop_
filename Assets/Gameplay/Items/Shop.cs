using Assets.Gameplay.ScoresManagers;


namespace Assets.Gameplay.Items
{
    internal static class Shop
    {


        private static bool CheckBuy(int price, Inventory inventory)
        {
            if (inventory.money >= price)
            {
                inventory.MoneyUpdate(-price);
                return true;
            }
            return false;
        }


        public static bool BuyJumps(Inventory inventory)
        {
            if(CheckBuy(Assets.Gameplay.Constants.ShopPrices.JumpPrice(), inventory)) // confirms that purchase can be made
            { 
                inventory.JumpsUpdate();
                return true;
            }

            return false;
        }

        public static  bool BuyBoosts(Inventory inventory)
        {
            if (CheckBuy(Assets.Gameplay.Constants.ShopPrices.BoostPrice(), inventory)) // confirms that purchase can be made
            {
                inventory.BoostsUpdate();
                return true;
            }

            return false;
        }





    }
}

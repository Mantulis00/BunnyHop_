using Assets;
using Assets.Gameplay.Items;
using UnityEngine;



public class ShopMenu : Menues
    {

    public AudioSource buySound;

        void Update()
        {
            CheckClick();
        }

        public override void ClickChoices(RaycastHit hit)
        {
             if (menuManager.state == States.ShopMenu)
             {
                 if (hit.transform.name == "ButtonBuyJump")
                 {
                    if (Shop.BuyJumps(menuManager.Player.items.inventory) )
                    {
                         buySound.Play();
                    }
                 }
                 else if (hit.transform.name == "ButtonBuyBoost")
                 {
                    if( Shop.BuyBoosts(menuManager.Player.items.inventory) )
                    {
                        buySound.Play();
                    }
                 }
                 else if (hit.transform.name == "ButtonApply")
                 {
                     menuManager.Player.items.LeaveShopMode(true);
                     menuManager.SetupState(States.StartMenu);
                 }

                 else if (hit.transform.name == "ButtonBack")
                 {
                    menuManager.Player.items.LeaveShopMode(false);
                    menuManager.SetupState(States.StartMenu);
                 }
            }

        }



    }




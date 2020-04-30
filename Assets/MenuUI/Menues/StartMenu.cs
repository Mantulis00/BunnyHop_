using Assets;
using UnityEngine;

public class StartMenu : Menues
{

    private void Start()
    {

    }

    void Update()
    {
        CheckClick();
    }

    public override  void ClickChoices(RaycastHit hit)
    {
        if (menuManager.state == States.StartMenu)
        {
            if (hit.transform.name == "ButtonStart")
            {
                menuManager.SetupState(States.Ingame);
            }
            else if (hit.transform.name == "ButtonShop")
            {
                menuManager.SetupState(States.ShopMenu);
            }
            else if (hit.transform.name == "ButtonOptions")
            {

            }
        }
    }



}


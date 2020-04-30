using Assets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverMenu : Menues
{
    private delegate void GameResetEvent();
    private event GameResetEvent ResetGame;
    public Launchables launchables;

    private void Start()
    {

        if (launchables.LaunchableList != null)
        {
            foreach (var launchable in launchables.LaunchableList)
            {
                ResetGame += launchable.Restart;
            }
        }
        
    }

    void Update()
    {
        CheckClick();
    }

    public override void ClickChoices(RaycastHit hit)
    {
        if (menuManager.state == States.GameOverMenu)
        {
            if (hit.transform.name == "ButtonStart")
            {
                ResetGame();
                menuManager.SetupState(States.Ingame);
            }
            else if (hit.transform.name == "ButtonShop")
            {
                ResetGame();
                menuManager.SetupState(States.ShopMenu);
            }
            else if (hit.transform.name == "ButtonOptions")
            {

            }
        }
    }



}




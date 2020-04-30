using Assets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngameMenu : Menues
{

    private void Update()
    {
        CheckClick();
    }


    public override void ClickChoices(RaycastHit hit)
    {
        if (menuManager.state == States.Ingame)
        {
            
            if (hit.transform.name == "ButtonOptions" && (hit.transform.parent.gameObject.GetComponent("IngameMenu") as IngameMenu) != null)
            {
                menuManager.SetupState(States.StartMenu);
            }
        }
    }



}




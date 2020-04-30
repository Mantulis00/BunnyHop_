using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{

    public States state { get; set; }

    //MIN 3
    private int treshHorizontal = 8, treshVertical = 8;

    void Start()
    {

    }
    public void ChangeStateEvent(object o, States e)
    {
        state = e;
    }



    /// <summary>
    /// make menu open button bool to prevent boosting, etc. by pressing menu keys done
    /// </summary>

    public void GetIngameControls(PlayerPhysix playerPhysix)
        {
        if (Input.GetMouseButtonDown(0) && state == States.Ingame)
        {
            if (Input.mousePosition.x <= Screen.width * (float)(treshHorizontal - 1) / (treshHorizontal * 2))
            {
                playerPhysix.InputLeft();
            }
            else if (Input.mousePosition.x >= Screen.width * (float)(treshHorizontal + 1) / (treshHorizontal * 2))
            {
                playerPhysix.InputRight();
            }
            else if (state == States.Ingame)
            {
                if (Input.mousePosition.y <= Screen.height * (float)(treshVertical - 1) / (treshVertical * 2))
                {
                    playerPhysix.UseBoost();
                }
                else if (Input.mousePosition.y >= Screen.height * (float)(treshVertical + 1) / (treshVertical * 2))
                {
                    playerPhysix.UseJump();
                }
            }
        }
    }
    






}

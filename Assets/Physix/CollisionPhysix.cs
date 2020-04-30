using Assets.Memory.Adaptors;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Physix
{

    /// <summary>
    /// bug when restarting the game, objects apear flying, maybe set all objects invisible, then set their locations, then set visible;
    /// </summary>

    public class CollisionPhysix : MonoBehaviour
    {
       public MenuManager menuManager;

       


        private void OnCollisionEnter(Collision collision) // clean up after
        {
            menuManager.SetupState(States.GameOverMenu);
            
        }


    }
}

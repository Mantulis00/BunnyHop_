using UnityEngine;

namespace Assets.Physix
{

    /// <summary>
    /// bug when restarting the game, objects apear flying, maybe set all objects invisible, then set their locations, then set visible;
    /// </summary>

    public class CollisionPhysix : MonoBehaviour
    {
       public MenuManager menuManager;

       


        private void OnCollisionEnter(Collision collision) // clean up after // controlled by boxcolider
        {
            menuManager.SetupState(States.GameOverMenu);
            
        }


    }
}

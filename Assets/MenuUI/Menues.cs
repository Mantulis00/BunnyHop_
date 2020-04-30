using System;
using UnityEngine;

namespace Assets
{
    public abstract class Menues : MonoBehaviour 
    {

        public GameObject Plane;
        public MenuManager menuManager;


        internal void CheckClick()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

       

            if (Input.GetMouseButtonDown(0) )
            {

                if (Physics.Raycast(ray, out hit))
                {
                    ClickChoices(hit);
                }

                ClickControls(menuManager.state);
            }

        }

        public virtual void ClickChoices(RaycastHit hit)
        {
            throw new NotImplementedException();
        }

        /// if u will press on anything on menu it will change state, and click wont work, if u will press anywhere where 
        /// it doesnt change state click will be register as ingame control
        public virtual void ClickControls(States state)
        {
            if (state == States.Ingame && this is IngameMenu) 
            {
                menuManager.controls.GetIngameControls(menuManager.Player);
            }
                
        }

    }

    public delegate void MenuHandler(object o, States e);
}

using Assets.Gameplay.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets
{
    public class MenuManager : MonoBehaviour, ILaunchable
    {
        public StartMenu startMenu;
        public IngameMenu ingameMenu;
        public GameOverMenu gameOverMenu;
        public ShopMenu shopMenu;


        // needed foor change state events
        public AiPhysix AI;
        public PlayerPhysix Player;
        public PlayerControls controls;

        public AudioSource cofin;


        public States state {  get; private set; }
        /// <summary>
        /// if current state is ingame, then lastste is visible as cload, (preferably semi visible) <-- to be solved
        /// </summary>
        public States lastState { get; private set; }

        public event MenuHandler menuEvent;

        void Start()
        {
            menuEvent += AI.ChangeStateEvent;
            menuEvent += Player.ChangeStateEvent;
            menuEvent += controls.ChangeStateEvent;

            SetupState(States.StartMenu);

            cofin.Play();
        }


        public void Restart()
        {
            SetupState(States.Ingame);
            UpdateMenuObjects(new object(), Camera.main.transform);
        }

        private void Update()
        {
            SetupInvetoryText();

        }

        private void UpdateAIObject(Vector3 AI, Vector3 Player)
        {
            Vector3 alertPos = new Vector3();
            float cv = 7.5f;
            float ch = 5f;

            float ccofin = 100;

            float letterW ;

            alertPos.z = AI.z;


            // Horizontal
            if (AI.z < Player.z)
                alertPos.z += Math.Abs(Player.z - AI.z) / ch;
            else if (AI.z > Player.z )
                alertPos.z -=  Math.Abs(AI.z - Player.z) / ch;

            /// cofin
                 if (Player.x - AI.x < ccofin) cofin.volume = 1.05f - (Player.x - AI.x) / ccofin;
                else cofin.volume = 0.05f;




            // Vertical text
            if (Player.x - AI.x < cv)
            {
                alertPos.x = AI.x;
            }

            else
            {
                alertPos.x = Player.x - cv;
            }

            
            // vertical arrow
            if (Player.x - AI.x < cv * 3)
            {
                if (ingameMenu.transform.Find("AIPlane").Find("ArrowAI").gameObject.activeSelf)
                    ingameMenu.transform.Find("AIPlane").Find("ArrowAI").gameObject.SetActive(false);
            }
            else
            {
                if (!ingameMenu.transform.Find("AIPlane").Find("ArrowAI").gameObject.activeSelf)
                    ingameMenu.transform.Find("AIPlane").Find("ArrowAI").gameObject.SetActive(true);
            }


            if (Player.x - AI.x >= 100) letterW = -0.583f;
            else if (Player.x - AI.x >= 10) letterW = -0.743f;
            else letterW = -1;

           

            ingameMenu.transform.Find("AIPlane").Find("TextAIDistance").transform.localPosition = new Vector3(1, 0,
                ingameMenu.transform.Find("AIPlane").Find("ArrowAI").localPosition.z + letterW);



            ingameMenu.transform.Find("AIPlane").transform.position = new Vector3 (alertPos.x, 0, alertPos.z);

            ingameMenu.transform.Find("AIPlane").Find("TextAIDistance").gameObject.GetComponent<TMPro.TextMeshPro>().text =
               ((int)(Player.x - AI.x)).ToString();
        }

        // trigger event to set menu
        public  void SetupState(States state) // need to set all children false only one tru
        {
            lastState = this.state;
            this.state = state;


            foreach (Transform menuChild in this.transform) // hide all menues
            {
                menuChild.gameObject.SetActive(false);
            }

            SetupVisiblePlains(state); // setup menues which should be shown

            if (state == States.Ingame) // if state is ingame, show last menue as "transperent cload"
            {

                SetupVisiblePlains(lastState);
                MakeTransperent(startMenu.transform);
            }

            if (state == States.StartMenu)
            {
                MakeNotTransperent(startMenu.transform);
            }

              
            
            menuEvent(this, state);
        }

  


        // is used to show selected menu, may be used whith additional actions
        private void SetupVisiblePlains(States sState)
        {
            UpdateMenuObjects(this, Camera.main.transform);
            switch (sState)
            {
                case States.Ingame:
                    {
                        ingameMenu.Plane.SetActive(true);
                        break;
                    }
                case States.StartMenu:
                    {
                        startMenu.Plane.SetActive(true);
                        break;
                    }
                case States.GameOverMenu:
                    {
                        Player.items.Save(); /// temporary 
                        Player.achievments.Save();
                        SetupAchievmentsText();

                        gameOverMenu.Plane.SetActive(true);
                        break;
                    }
                case States.ShopMenu:
                    {
                        Player.items.EnterShopMode();
                        shopMenu.Plane.SetActive(true);
                        break;
                    }

            }
        }


        // is triggered from camera class to setup menu location
        internal void UpdateMenuObjects(object o, Transform pos)
        {

            Vector3 menuPos = new Vector3();
            menuPos = pos.position;
            menuPos.y -= 30;

            

             switch (state)
             {
                 case States.Ingame:
                 {
                        ingameMenu.gameObject.transform.position = menuPos;
                        UpdateAIObject(AI.transform.position, Player.transform.position);

                        break;
                 }
                 case States.StartMenu:
                 {
                      startMenu.gameObject.transform.position = menuPos;
                      break;
                 }
                 case States.GameOverMenu:
                 {
                      gameOverMenu.gameObject.transform.position = menuPos;
                      break;
                 }
                 case States.ShopMenu:
                 {
                      shopMenu.gameObject.transform.position = menuPos;
                      break;
                 }

             }
        }






        // Make selected menu transperent
        private void MakeTransperent(Transform t)
        {
            for (int x = 0; x < t.childCount; x++)
            {

                if (t.GetChild(x).GetComponent<MeshRenderer>())
                {
                    Material m = t.GetChild(x).GetComponent<MeshRenderer>().material;

                    Color color = m.color;
                    color.a = 0.5f;

                    m.SetFloat("_Mode", 3f);
                    m.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                    m.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                    m.EnableKeyword("_ALPHABLEND_ON");
                    m.renderQueue = 3000;

                    m.SetColor("_Color", color);
                }
            }
        }

        private void MakeNotTransperent(Transform t)
        {
            for (int x = 0; x < t.childCount; x++)
            {
                if (t.GetChild(x).GetComponent<MeshRenderer>())
                {
                    Material m = t.GetChild(x).GetComponent<MeshRenderer>().material;
                    Color color = m.color;
                    color.a = 1.0f;

                    m.EnableKeyword("_ALPHABLEND_OFF");
                    m.SetColor("_Color", color);

                }
            }
        }


        // get inventory item texts and update them
        private bool SetupInvetoryText()
        {
            if (Player.items != null)
            {
                startMenu.transform.Find("TextsItems").Find("TextMoney").gameObject.GetComponent<TMPro.TextMeshPro>().text =
                    Player.items.GetMoney();

                startMenu.transform.Find("TextsItems").Find("TextJumps").gameObject.GetComponent<TMPro.TextMeshPro>().text =
                    Player.items.GetJumps();

                startMenu.transform.Find("TextsItems").Find("TextBoosts").gameObject.GetComponent<TMPro.TextMeshPro>().text =
                    Player.items.GetBoosts();

                return false;
            }

            return false;
        }

        private bool SetupAchievmentsText()
        {
            if (Player.achievments != null)
            {
                gameOverMenu.transform.Find("TextsItems").Find("TextLastDistance").gameObject.GetComponent<TMPro.TextMeshPro>().text =
                    Player.achievments.GetLastDistance();

                gameOverMenu.transform.Find("TextsItems").Find("TextMaxDistance").gameObject.GetComponent<TMPro.TextMeshPro>().text =
                    Player.achievments.GetMaxDistance();


                return false;
            }

            return false;
        }


    }
}

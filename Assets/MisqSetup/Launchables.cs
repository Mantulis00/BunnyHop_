
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets
{
    public class Launchables : MonoBehaviour
    {
        public List<ILaunchable> LaunchableList { get; private set; }

        public PlayerPhysix player;
        public AiPhysix AI;
        public MenuManager menuManager;
        public Spawner spawner;


        private void Awake()
        {
            LaunchableList = new List<ILaunchable>();
            LaunchableList.Add((ILaunchable)menuManager);
            LaunchableList.Add((ILaunchable)spawner);
            LaunchableList.Add((ILaunchable)AI);
            LaunchableList.Add((ILaunchable)player);




        }
    }
}

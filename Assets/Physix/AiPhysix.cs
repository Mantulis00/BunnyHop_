﻿using System.Collections;
using System.Collections.Generic;
using Assets;
using UnityEngine;

public class AiPhysix : MonoBehaviour , ILaunchable
{
    public Transform AI;
    public Transform player;
    public Spawner spawner;
    private Vector3 speed, startPos, startSpeed;

    private float forceCof;
    private float sideDistance;


    public States state;

 /// <summary>
 /// need to clear up constants
 /// bug if ai catches player faster than  player  reaches first obsticle
 /// </summary>


    void Start()
    {
        SetupVars();
        SetupStartVars();
    }

    public void Restart()
    {
        AI.position = startPos;
        speed = startSpeed;
    }

    private void SetupVars()
    {
        AI.position = new Vector3(-100, 0, 0);
        

        forceCof = 100;
        sideDistance = 8;
        speed = new Vector3(0, 0, 0);
    }

    private void SetupStartVars()
    {
        startSpeed = new Vector3(speed.x, speed.y, speed.z);
        startPos = new Vector3(AI.position.x, AI.position.y, AI.position.z);
    }

    public void ChangeStateEvent(object o, States e)
    {
        state = e;
    }





    // Update is called once per frame
    void Update()
    {
        if (state == 0)
        {
            ManageSpeed();
            
            AI.position += speed * Time.deltaTime;
            FollowPlayer();
        }
    }

    private void ManageSpeed()
    {
        if (speed.x <= 50)
            speed.x += 0.05f;
        else
            speed.x += 0.02f;


        if (player.position.x < AI.position.x)
        {
            speed.x = 0;
            return;
        }

        speed.z = speed.z * (float)System.Math.Pow(0.90f, Time.deltaTime);

        
    }




    private void FollowPlayer()
    {
        


        if (AI.position.x > spawner.TileList[0].transform.position.x) // to follow player
        {
            float xDistance = player.position.x - AI.position.x;
            float zDistance = (player.position.z - AI.position.z);
            if (xDistance >= -2 && xDistance <= 2)
            {
                if (xDistance < 0) xDistance = -1;
                else xDistance = 1;
            }

            speed.z += (zDistance / xDistance * forceCof) *Time.deltaTime;

            if (System.Math.Abs(player.position.z - AI.position.z) < sideDistance || System.Math.Abs(zDistance + speed.z) != zDistance + speed.z)
            {
               speed.z = speed.z * (float)System.Math.Pow(0.01f, Time.deltaTime);
                //speed.z = speed.z * 0.5f;
            }


        }



        else // to pass obsticles
        {
            float xDistance = spawner.TileList[0].transform.position.x - AI.position.x;
            float zDistance = (spawner.TileList[0].transform.position.z + spawner.TileList[1].transform.position.z) / 2 - AI.position.z;

            if (xDistance >= -2 && xDistance <= 2)
            {
                if (xDistance < 0) xDistance = -1;
                else xDistance = 1;
            }


            speed.z += (zDistance / xDistance * forceCof) *Time.deltaTime;

            if (System.Math.Abs(zDistance) < sideDistance || System.Math.Abs(zDistance + speed.z) != zDistance + speed.z)
            {
                speed.z = speed.z * (float)System.Math.Pow(0.01f, Time.deltaTime);
                //speed.z = speed.z * 0.5f;
            }

        }


    }
}

using Assets;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPhysix : MonoBehaviour, ILaunchable
{

    public Transform player { get; private set; }
    private Vector3 speed, startSpeed, startPos;
    private bool inputLeft;
    private float sideForce, forwardForce, gravityForce;

    public Assets.Gameplay.ScoresManagers.ItemsManager items { get; private set; }
    public Assets.Gameplay.ScoresManagers.AchievmentsManager achievments { get; private set; }

    public States state { get; private set; }

    /// <summary>
    /// clear up constants
    /// </summary>
    /// 



    void Start()
    {
        player = this.transform;
        
        SetupVars();
        SetupStartVars();
    }
    void Awake()
    {
        items = new Assets.Gameplay.ScoresManagers.ItemsManager();
        achievments = new Assets.Gameplay.ScoresManagers.AchievmentsManager();
    }

    private void SetupVars()
    {
        speed = new Vector3(4, 0, 0);

        sideForce = 4f;
        forwardForce = 1f;
        gravityForce = 75f;
    }

    private void SetupStartVars()
    {
        startSpeed = new Vector3(speed.x, speed.y, speed.z);
        startPos = new Vector3(player.position.x, player.position.y, player.position.z);
    }

    public void Restart()
    {
        player.position = startPos;
        speed = startSpeed;

        achievments.Restart();
    }


    public void ChangeStateEvent(object o, States e)
    {
        state = e;
    }


    private void Update()
    {

        if (state == States.Ingame)
        {
            player.position += speed * Time.deltaTime;
            achievments.CheckSimpleMoneyScore(player, items.inventory);
            CheckFriction();
            
        }
        GravityCheck();

    }

    private bool GravityCheck()
    {
        if (player.position.y > 0)
        {
            Gravity();
            return false;
        }
        else if (player.position.y < 0)
        {
            player.transform.position = new Vector3(player.transform.position.x, 0, player.transform.position.z);
            speed.y = 0;
        }
        return true;
    }



    private void Gravity()
    {
        if (player.transform.position.y > 0)
        {
            speed.y -= gravityForce * Time.deltaTime;
        }
        else
        {
            speed.y = 0;
        }
            
    }


    private void CheckFriction()
    {
        if (speed.x >= 50) // friction
            speed = speed * (float)System.Math.Pow(0.99f, Time.deltaTime);
        else
            speed = speed * (float)System.Math.Pow(1.1f, Time.deltaTime);
    }

    internal void InputLeft()
    {

        float forwardForceBySpeed;
        float sideForceBySpeed;
        if (speed.x > 10) // stopping force when goin fast
        {
             forwardForceBySpeed = forwardForce * speed.x / 3;
             sideForceBySpeed = sideForce * speed.x / 10;
        }
        else
        {
            forwardForceBySpeed = forwardForce;
            sideForceBySpeed = sideForce;
        }


        if (!inputLeft) // last input was right
        {
            speed.x += forwardForce / 2;
            speed.z += sideForceBySpeed * 2;
        }
        else if (inputLeft) // last input was left
        {
            speed.x -= forwardForceBySpeed;
            speed.z += sideForceBySpeed;
        }

        if (speed.x <= 0) speed.x = 0;

        inputLeft = true;
    }

    internal void InputRight()
    {
        float forwardForceBySpeed;
        float sideForceBySpeed;
        if (speed.x > 10) // stopping force when goin fast
        {
            forwardForceBySpeed = forwardForce * speed.x / 3;
            sideForceBySpeed = sideForce * speed.x / 10;
        }
        else
        {
            forwardForceBySpeed = forwardForce;
            sideForceBySpeed = sideForce;
        }


        if (inputLeft) // last input was left
        {
            speed.x += forwardForce / 2;
            speed.z -= sideForceBySpeed * 2;
        }
        else if (!inputLeft) // last input was right
        {
            speed.x -= forwardForceBySpeed;
            speed.z -= sideForceBySpeed;
        }

        if (speed.x <= 0) speed.x = 0;


        inputLeft = false;
    }


    internal void UseBoost()
    {
        if (items.UseBoost())
        {
            speed += Assets.Gameplay.Constants.Powerup.Boost();
        }
    }

    internal void UseJump()
    {
        if (items.UseJump())
        {
            if (GravityCheck()) speed += Assets.Gameplay.Constants.Powerup.Jump();
        }
    }



}

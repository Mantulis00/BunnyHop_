using Assets;
using System.Collections;
using System.Collections.Generic;
using System.Security.AccessControl;
using UnityEngine;
using UnityEngine.Timeline;

public class Spawner : MonoBehaviour, ILaunchable
{
    public Transform player;


    internal List<GameObject> TileList; // main fence object
    public GameObject cloneObject;

    internal List<GameObject> SideList; // side fence object
    public GameObject sideObject;

    internal List<GameObject> GroundList; // ground objects
    public GameObject groundObject;


    private int numberTiles; // number of tiles to spwan
    private int  lastObsticleX, startObsticleX, startSideX, startGroundX;
    private int slitMaxDifference, slitLastLocation, slitLastLocationStart;
    private int mapMaxRadius;
    public bool passedFirst;
    
    struct rangeVars
    {
       public int from;
       public int to;
    }

    private rangeVars spaceRange, slitRange, slitLocationRange;



        void Start()
    {
        SetupVars();
        SetupStartVars();

        TileList = new List<GameObject>();
        SideList = new List<GameObject>();
        GroundList = new List<GameObject>();


        SetupStartTiles();

    }

    private void SetupStartTiles()
    {
        for (int x = 0; x < numberTiles; x++)
        {
            CreateObsticle(TileList, cloneObject, SpawnerTypes.MainObsticle);
            CreateObsticle(SideList, sideObject, SpawnerTypes.SideObsticle);
            CreateObsticle(GroundList, groundObject, SpawnerTypes.BG_Ground);
        }

    }



    public void Restart()
    {
        while (DestroyTile(TileList, SpawnerTypes.MainObsticle) == true ||
               DestroyTile(SideList, SpawnerTypes.SideObsticle) == true ||
               DestroyTile(GroundList, SpawnerTypes.BG_Ground) == true);

        RestartVars();
        SetupStartTiles();

    }

    private void RestartVars()
    {
        lastObsticleX= startObsticleX ;
        startSideX = (int)-sideObject.transform.localScale.x;
        startGroundX = (int)-groundObject.transform.localScale.x;

        slitLastLocation = slitLastLocationStart;

        passedFirst = false;
    }


    private void SetupVars()
    {
        lastObsticleX = 0;
        startSideX = (int)-sideObject.transform.localScale.x;
        startGroundX = (int)-groundObject.transform.localScale.x;

        numberTiles = 4; // how many obsticles spawn at same time

        spaceRange.from = 50; // obsticle placment (how far)
        spaceRange.to = 80;

        slitRange.from = 7; // obsticle slit variation
        slitRange.to = 12;

        slitLocationRange.from = -10; // obsticle slit placment
        slitLocationRange.to = 10;

        slitMaxDifference = 10; // how far slit can be from middle

        slitLastLocation = 0;

        mapMaxRadius = 50; // map width

        passedFirst = false;
    }

    private void SetupStartVars()
    {
        startObsticleX = lastObsticleX;


        slitLastLocationStart = slitLastLocation;
    }



    // Update is called once per frame
    void Update()
    {
        SpawnCheck();
    }


    private void UpdatePassedObsticles()
    {
        if (player.transform.position.x > TileList[1].transform.position.x)
        {
            passedFirst = !passedFirst;
        }
    }


    private void SpawnCheck()
    {
        if (!passedFirst) UpdatePassedObsticles();



        if (player.position.x >= TileList[2].transform.position.x) // seperate lists for obsticles
        {
            DestroyTile(TileList, SpawnerTypes.MainObsticle);
           
            CreateObsticle(TileList, cloneObject, SpawnerTypes.MainObsticle);

        }

        if (player.position.x >= SideList[2].transform.position.x) // and sides
        {
            DestroyTile(SideList, SpawnerTypes.SideObsticle);

            CreateObsticle(SideList, sideObject, SpawnerTypes.SideObsticle);
        }


        if (player.position.x >= GroundList[2].transform.position.x) // and background grounds
        {
            DestroyTile(GroundList, SpawnerTypes.BG_Ground);

            CreateObsticle(GroundList, groundObject, SpawnerTypes.BG_Ground);
        }

    }





    private void SetupObsticle(GameObject obsticleLeft, GameObject obsticleRight)
    {
        int slitLocationVal, slitWidthVal;

        //Spacer
        lastObsticleX += Random.Range(spaceRange.from, spaceRange.to);


        slitLocationVal = Random.Range(slitLocationRange.from, slitLocationRange.to);

        if (System.Math.Abs(slitLocationVal - slitLastLocation) > slitMaxDifference) // to make sure slit isnt too far away from last slit (horizontal)
        {
            if (slitLastLocation > 0)
                slitLocationVal = slitLastLocation - slitMaxDifference;
            else
                slitLocationVal = slitLastLocation + slitMaxDifference;
        }


        slitWidthVal = Random.Range(slitRange.from, slitRange.to);



        obsticleLeft.transform.position = new Vector3(lastObsticleX, 0, slitLocationVal - slitWidthVal - obsticleLeft.transform.localScale.z/2 );
        obsticleRight.transform.position = new Vector3(lastObsticleX, 0, slitLocationVal + slitWidthVal + obsticleRight.transform.localScale.z / 2);


        slitLastLocation = slitLocationVal;

    }



    private void SetupSide(GameObject obsticleLeft, GameObject obsticleRight)
    {
        startSideX += (int)sideObject.transform.localScale.x;
        obsticleLeft.transform.position = new Vector3(startSideX, 0, -mapMaxRadius); // - maxmaxradius <- scuffed temp
        obsticleRight.transform.position = new Vector3(startSideX, 0, mapMaxRadius);

        obsticleLeft.transform.rotation =  Quaternion.Euler(0, 180, 0);


    }

    private void SetupGround(GameObject ground)
    {
        startGroundX += (int)groundObject.transform.localScale.x;
        ground.transform.position = new Vector3(startGroundX, -player.transform.localScale.y, 0);
    }

    private bool GetBiActions(SpawnerTypes type)
    {
        if (type == SpawnerTypes.BG_Ground) return false;

        return true;
    }



    private void CreateObsticle(List<GameObject> TileList, GameObject obsticleType, SpawnerTypes type)
    {

            GameObject goL;
            goL = Instantiate(obsticleType) as GameObject;
            goL.transform.SetParent(transform);

             if (GetBiActions(type))
            {
                GameObject goR;
                goR = Instantiate(obsticleType) as GameObject;
                goR.transform.SetParent(transform);

                if (type == SpawnerTypes.MainObsticle)
                    SetupObsticle(goL, goR);
                else if (type == SpawnerTypes.SideObsticle)
                    SetupSide(goL, goR);

                 TileList.Add(goL);
                 TileList.Add(goR);

            }
             else
             { 
                if (type == SpawnerTypes.BG_Ground)
                     SetupGround(goL);

                 TileList.Add(goL);
            }

           

        
       

        
           

   


    }





    private bool DestroyTile(List<GameObject> TileList, SpawnerTypes type)
    {
        if (TileList.Count > 0)
        {
            for (int x = 0; x < 2; x++)
            {
                if (x == 1 &&
                    type == SpawnerTypes.BG_Ground)
                {
                    break;
                }


                Destroy(TileList[0]);
                TileList.RemoveAt(0);
            }
        }
        

        return TileList.Count > 0;//?  true :  false;
    }



}

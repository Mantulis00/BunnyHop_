using Assets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour, ILaunchable
{
    public Transform player;

    internal List<GameObject> TileList;
    public GameObject cloneObject;

    internal List<GameObject> SideList;
    public GameObject sideObject;


    private int numberTiles;
    private int  lastObsticleX, startObsticleX, startSideX;
    private int slitMaxDifference, slitLastLocation, slitLastLocationStart;
    private int mapMaxRadius;


    
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
        SetupStartTiles();

    }

    private void SetupStartTiles()
    {
        for (int x = 0; x < numberTiles; x++)
        {
            CreateObsticle(TileList, cloneObject, true);
            CreateObsticle(SideList, sideObject, false);
        }
    }



    public void Restart()
    {
        while (DestroyTile(TileList) == true ||
               DestroyTile(SideList) == true) ;

        RestartVars();
        SetupStartTiles();

    }

    private void RestartVars()
    {
        lastObsticleX= startObsticleX ;
        startSideX = (int)-sideObject.transform.localScale.x;

        slitLastLocation = slitLastLocationStart;
    }


    private void SetupVars()
    {
        lastObsticleX = 0;
        startSideX = (int)-sideObject.transform.localScale.x;

        numberTiles = 4;

        spaceRange.from = 50;
        spaceRange.to = 80;

        slitRange.from = 7;
        slitRange.to = 12;

        slitLocationRange.from = -10;
        slitLocationRange.to = 10;

        slitMaxDifference = 10;

        slitLastLocation = 0;

        mapMaxRadius = 50;
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



    private void SpawnCheck()
    {
        if (player.position.x >= TileList[2].transform.position.x)
        {
            DestroyTile(TileList);
           
            CreateObsticle(TileList, cloneObject, true);
           

        }

        if (player.position.x >= SideList[2].transform.position.x)
        {
            DestroyTile(SideList);

            CreateObsticle(SideList, sideObject, false);
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
        obsticleLeft.transform.rotation =  Quaternion.Euler(0, 180, 0);



        obsticleRight.transform.position = new Vector3(startSideX, 0, mapMaxRadius);
    }



    private void CreateObsticle(List<GameObject> TileList, GameObject obsticleType, bool mainObsticle)
    {

            GameObject goL;
            goL = Instantiate(obsticleType) as GameObject;
            goL.transform.SetParent(transform);

            GameObject goR;
            goR = Instantiate(obsticleType) as GameObject;
            goR.transform.SetParent(transform);

        if (mainObsticle)
            SetupObsticle(goL, goR);
        else
            SetupSide(goL, goR);


            TileList.Add(goL);
            TileList.Add(goR);

    }


    private bool DestroyTile(List<GameObject> TileList)
    {
        if (TileList.Count > 0)
        {
            for (int x = 0; x < 2; x++)
            {
                Destroy(TileList[0]);
                TileList.RemoveAt(0);
            }
        }
        

        return TileList.Count > 0;//?  true :  false;
    }



}

using Assets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Assets
{
    public delegate void CameraLocationEvent(object o, Transform e);
}


public class CameraFollower : MonoBehaviour
{
   public Camera cameraPlayer;

   public Transform player;
   public Transform AI;

    public MenuManager menuManager;


    public Vector3 cameraPositionOffset;
    public  Vector3 cameraAngleOffset;

    private float distanceTreshAI, distanceMinAI;


    private event CameraLocationEvent updateCameraLocation;

    void Start()
    {
        SetupVars();

    }

    void SetupVars()
    {
        updateCameraLocation += menuManager.UpdateMenuObjects;

        cameraPositionOffset = new Vector3(20, 50, 0);
        cameraAngleOffset = new Vector3(90, 0, -90);

        distanceTreshAI = 20f;
        distanceMinAI = 3f;
    }



    void Update()
    {
        RefreshCamera();
        CameraMoveToAI();
        updateCameraLocation(this, cameraPlayer.transform);

    }

    void CameraMoveToAI()
    {
        if (player.position.x - AI.position.x < distanceTreshAI && player.position.x - AI.position.x > distanceTreshAI / distanceMinAI)
        {
            cameraPositionOffset.x = (player.position.x - AI.position.x);
        }
        else if (player.position.x - AI.position.x < distanceTreshAI)
            cameraPositionOffset.x = distanceTreshAI / distanceMinAI; 
        else
            cameraPositionOffset.x = distanceTreshAI;
    }

    void RefreshCamera()
    {
        cameraPlayer.transform.position = player.position + cameraPositionOffset;
        cameraPlayer.transform.rotation = Quaternion.Euler(cameraAngleOffset);
    }




}

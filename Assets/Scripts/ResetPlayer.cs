using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPlayer : MonoBehaviour
{

    public GameObject motorcyclePrefab;
    public Vector3 resetPosition;
    public Quaternion resetRotation;

    // Start is called before the first frame update
    void Start()
    {
     
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            PlayerReset();
        }
    }

    void PlayerReset()
    {
        GameObject motorcycle = GameObject.Find("motorcycle");
        CrashScript crash = motorcycle.GetComponent<CrashScript>();

        if (crash.crashed == true)
        {
            Destroy(motorcycle);

            motorcycle = Instantiate(motorcyclePrefab, resetPosition, resetRotation);
            motorcycle.name = "motorcycle";


            GameObject camera = GameObject.Find("Main Camera");
            CameraController cameraController = camera.GetComponent<CameraController>();

            cameraController.target = motorcycle.transform;
            
            crash.crashed = false;
        }
    }
}

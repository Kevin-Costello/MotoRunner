using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrashScript : MonoBehaviour
{

    public bool crashed = false;

    void Start()
    {
    }
    void Update()
    {
        GameObject motorcycle = GameObject.Find("motorcycle");
        if (motorcycle.transform.position.y < -1)
        {
            Crashed();
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "DestroyObstacle")
        {
            Crashed();
        }

        if (collision.gameObject.tag == "Obstacle")
        {
            GameObject motorcycle = GameObject.Find("motorcycle");
            Speedometer speed = motorcycle.GetComponent<Speedometer>();
            if (speed.currentSpeed > 30)
            {
                Crashed();
            }
        }
    }


    void Crashed()
    {
        int children = transform.childCount;

        for(int i = 0; i < children; i++)
        {
            MeshCollider meshCollider = transform.GetChild(i).gameObject.AddComponent<MeshCollider>();
            meshCollider.convex = true;
            transform.GetChild(i).gameObject.AddComponent<Rigidbody>();
        }

        transform.DetachChildren();
        crashed = true;
    }
}

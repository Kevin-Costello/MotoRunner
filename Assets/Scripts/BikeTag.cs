using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BikeTag : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("collided with " + collision.gameObject.name);
    }

}

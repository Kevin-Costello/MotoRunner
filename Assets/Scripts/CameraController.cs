﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public Vector3 offset;
    public Transform target;
    public float translateSpeed;
    public float rotationSpeed;

    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        CameraTranslation();
        CameraRotation();

        GameObject motorcycle = GameObject.Find("motorcycle");
        CrashScript crash = motorcycle.GetComponent<CrashScript>();
        if (crash.crashed == true)
        {
            target = null;
        }
    }

    private void CameraTranslation()
    {
        var targetPosition = target.TransformPoint(offset);
        transform.position = Vector3.Lerp(transform.position, targetPosition, translateSpeed * Time.deltaTime);
    }

    private void CameraRotation()
    {
        var direction = target.position - transform.position;
        var rotation = Quaternion.LookRotation(direction, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Speedometer : MonoBehaviour
{

    public float currentSpeed;
    public Text speedText;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
       
        currentSpeed = rb.velocity.magnitude * 2.23694f;
        speedText.text = currentSpeed.ToString("F2") + "MPH";
        if(currentSpeed > 60)
        {
            speedText.color = Color.red;
        }
        else
        {
            speedText.color = Color.black;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrivingScript : MonoBehaviour
{

    public WheelCollider WC;
    public float torque = 100;
    public float brakes = 0;
    public float maxSteerAngle = 35;
    public GameObject Wheel;



    public GameObject Motorcycle;

    private Vector3 currentAngle;


    private float currentLean;



    float rotationZ = 0;


    // Start is called before the first frame update
    void Start()
    {
        currentAngle = Motorcycle.transform.eulerAngles;


        WC = this.GetComponent<WheelCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        float a = Input.GetAxis("Vertical");
        Accelerate(-a);


        float s = Input.GetAxis("Horizontal");
        Steer(s);

        Balance();
    }




    //Acceleration/Decceleration
    void Accelerate(float accel)
    {
        //Set the current speed
        accel = Mathf.Clamp(accel, -1, 1);
        float thrustTorque = accel * torque;
        WC.motorTorque = thrustTorque;      //Current Speed
        Debug.Log(WC.motorTorque);



        //Control visual rotation of the wheels
        Quaternion quat = Quaternion.Euler(0, -90, 90);
        Vector3 position;
        WC.GetWorldPose(out position, out quat);
        Wheel.transform.position = position;
        Wheel.transform.rotation = quat;

        //Control the bikes brakes if spacebar is pressed or released
        if(Input.GetKeyDown(KeyCode.Space))
        {
            brakes = 3000;
            WC.brakeTorque = brakes;
            Wheel.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        if(Input.GetKeyUp(KeyCode.Space))
        {
            brakes = 0;
            WC.brakeTorque = brakes;
        }


        //LSHIFT TO BOOST WIP

        /*
        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            if(WC.name == "RWCollider")
            {
                WC.mass = 100000;
                WC.motorTorque = 100000000f;
            }
        }
        */
    }

    //Control Front Wheel Steering
    void Steer(float steer)
    {
        steer = Mathf.Clamp(steer, -1, 1) * maxSteerAngle;
        WC.steerAngle = Mathf.Lerp(WC.steerAngle, steer, 0.5f);

    }

    void Balance()
    {
        /*
        rotationZ = Mathf.Clamp(rotationZ, -30, 30);

        if (rotationZ > 0)
        {
            rotationZ -= 0.1f;
        }
        else if (rotationZ < 0)
        {
            rotationZ += 0.1f;
        }


        if (Input.GetAxis("Horizontal") >= 0.1 && Input.GetAxis("Vertical") >= 0.1)
        {

            //rotationZ += Input.GetAxis("Horizontal") * Time.deltaTime;
            rotationZ += 0.2f;
            //rotationY += 0.01f;
            currentAngle = new Vector3(currentAngle.x, currentAngle.y, rotationZ);

            Motorcycle.transform.eulerAngles = currentAngle;


        }

        if (Input.GetAxis("Horizontal") <= -0.1 && Input.GetAxis("Vertical") >= 0.1)
        {

            //rotationZ -= Input.GetAxis("Horizontal") * Time.deltaTime;
            rotationZ -= 0.2f;
            //rotationY += 0.01f;
            currentAngle = new Vector3(currentAngle.x, currentAngle.y, rotationZ);

            Motorcycle.transform.eulerAngles = currentAngle;


        }
        */

        if(Input.GetAxis("Horizontal") == 0)
        {

            float rotSpeed = 1f;

            Vector3 uprightLeanDirection = new Vector3(0f, 1f, 0f) * 5;                                                                         //Vector position points upright
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, new Vector3(0f, 0f, uprightLeanDirection.z), Time.deltaTime, 0.0f); //Vector takes the current lean vector and updates z position to move towards upright
            
            var newRot = Quaternion.LookRotation(newDirection);                                                                                 //LookDirection required in order to Lerp rather than jump to new rotation
            
            Motorcycle.transform.rotation = Quaternion.Lerp(Motorcycle.transform.rotation, newRot, rotSpeed*Time.deltaTime);                     //Lerp between the Motorcycles ccurrent rotation and the upright rotation

        }

        /*
            float turnLean = Mathf.Clamp(thrustTorque, -30, 30);
            Quaternion lean = Quaternion.Euler(0, 0, turnLean);
            Motorcycle.transform.rotation = lean;
        */

    }

    private void OnDrawGizmos()
    {
        if (Application.isPlaying)
        {

            Gizmos.color = Color.red;
            Vector3 currentLeanDirection = Motorcycle.transform.TransformDirection(Vector3.up) * 5;
            Gizmos.DrawRay(Motorcycle.transform.position, currentLeanDirection);
            
            Gizmos.color = Color.blue;
            Vector3 uprightLeanDirection = new Vector3(0f, 1f, 0f) * 5;
            Gizmos.DrawRay(Motorcycle.transform.position, uprightLeanDirection);
        }
    }

}
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



    // Start is called before the first frame update
    void Start()
    {
        //currentAngle = Motorcycle.transform.eulerAngles;


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
        //Debug.Log(WC.motorTorque);



        //Control visual rotation of the wheels
        Quaternion quat = Quaternion.Euler(0, -90, 90);
        Vector3 position;
        WC.GetWorldPose(out position, out quat);
        Wheel.transform.position = position;
        Wheel.transform.rotation = quat;

        //Control the bikes brakes if spacebar is pressed or released
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(WC.name == "RWCollider")
            {
                brakes = 16000;
                WC.brakeTorque = brakes;
                Wheel.transform.rotation = Quaternion.Euler(0, 0, 0);
            }
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
        Rigidbody rb = Motorcycle.GetComponent<Rigidbody>();
        float tiltingAmount = Mathf.Abs(rb.velocity.x) + Mathf.Abs(rb.velocity.z) / 1.5f;

        if (Input.GetAxis("Horizontal") >= 0.1)
        {
            if(Motorcycle.transform.localRotation.eulerAngles.z > 330 || Motorcycle.transform.localRotation.eulerAngles.z < 30)
            {
                
                Motorcycle.transform.Rotate(new Vector3(0, 0, tiltingAmount) * Time.deltaTime);
            }
            
        }

        if (Input.GetAxis("Horizontal") <= -0.1)
        {
            if(Motorcycle.transform.localRotation.eulerAngles.z > 330 || Motorcycle.transform.localRotation.eulerAngles.z < 30)
            {
                Motorcycle.transform.Rotate(new Vector3(0, 0, -tiltingAmount) * Time.deltaTime);
            }

        }


        if (Input.GetAxis("Horizontal") == 0)
        {
            //Create new Vector from product of the forward vector and a vector pointing straight up
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, new Vector3(0f, 0f, 0f), Time.deltaTime, 0.0f);

            //LookDirection required in order to Lerp rather than jump to new rotation
            var newRot = Quaternion.LookRotation(newDirection);

            //Lerp between the Motorcycles ccurrent rotation and the upright rotation
            Motorcycle.transform.rotation = Quaternion.Lerp(Motorcycle.transform.rotation, newRot, Time.deltaTime);                     
        }
       
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

            Gizmos.color = Color.green;
            Vector3 leftLeanDirection = Motorcycle.transform.TransformDirection(new Vector3(.4f, 1f, 0f) * 5);
            Gizmos.DrawRay(Motorcycle.transform.position, leftLeanDirection);

            Gizmos.color = Color.yellow;
            Vector3 rightLeanDirection = Motorcycle.transform.TransformDirection(new Vector3(-.4f, 1f, 0f) * 5);
            Gizmos.DrawRay(Motorcycle.transform.position, rightLeanDirection);

        }
    }

}
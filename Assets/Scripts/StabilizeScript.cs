using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StabilizeScript : MonoBehaviour
{

    public GameObject Motorcycle;
    private Vector3 uprightAngle = new Vector3(0f, 0f, 0f);
    private Vector3 currentAngle;


    // Start is called before the first frame update
    void Start()
    {
        currentAngle = Motorcycle.transform.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        currentAngle = new Vector3(Mathf.LerpAngle(currentAngle.x, uprightAngle.x, Time.deltaTime),
                                   Mathf.LerpAngle(currentAngle.y, uprightAngle.y, Time.deltaTime),
                                   Mathf.LerpAngle(currentAngle.z, uprightAngle.z, Time.deltaTime));
        transform.eulerAngles = currentAngle;
    }
}

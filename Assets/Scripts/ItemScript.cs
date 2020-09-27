using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScript : MonoBehaviour
{
    public float rotSpeed = 10.0f;
    public float amplitude = 0.5f;
    public float frequency = 1f;

    Vector3 posOffset = new Vector3();
    Vector3 tempPos = new Vector3();
    // Start is called before the first frame update
    void Start()
    {
        posOffset = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //Spin the item
        transform.Rotate(new Vector3(0f, Time.deltaTime * rotSpeed, 0f), Space.World);

        //Float up and down
        tempPos = posOffset;
        tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) * amplitude;

        transform.position = tempPos;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            PickedUp();
        }
    }


    void PickedUp()
    {
        GameObject motorcycle = GameObject.Find("motorcycle");
        PlayerScore playerScoreScript = motorcycle.GetComponent<PlayerScore>();
        playerScoreScript.playerScore += 1;
        Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ResetPlayer : MonoBehaviour
{

    public GameObject motorcyclePrefab;
    public GameObject startingChunk;
    public GameObject levelGeneratorPrefab;
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
            /*********** Reset and Reinstantiate the player *************/

            //Destroy the motorcycle and all parts and reinstantiate
            GameObject[] bikeParts = GameObject.FindGameObjectsWithTag("BikePart");
            for (var i = 0; i < bikeParts.Length; i++)
            {
                Destroy(bikeParts[i]);
            }
            Destroy(motorcycle);
            motorcycle = Instantiate(motorcyclePrefab, resetPosition, resetRotation);
            motorcycle.name = "motorcycle";
            /************************************************************/

            /********* Reset and reinstantiate all the roads ************/

            //Destroy all existing chunks in the world
            GameObject[] roadChunks = GameObject.FindGameObjectsWithTag("Road");
            for (var i = 0; i < roadChunks.Length; i++)
            {
                Destroy(roadChunks[i]);
            }

            //Find, Destroy, and Spawn in a new level generator
            GameObject levelGenerator = GameObject.Find("LevelGenerator");
            Destroy(levelGenerator);
            levelGenerator = Instantiate(levelGeneratorPrefab);
            levelGenerator.name = "LevelGenerator";

            //Reinstantiate the origin road chunk
            GameObject startingRoad = startingChunk;
            startingRoad = Instantiate(startingRoad, new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0));
            startingRoad.name = "SouthToNorthRoad";
            /***********************************************************/


            /******** Reset and reapply all the UI elements ************/

            //Reset and reapply Speedometer
            Speedometer speedometer = motorcycle.GetComponent<Speedometer>();
            GameObject canvasObject = GameObject.Find("Canvas");
            Transform spText = canvasObject.transform.Find("CurrentSpeed");
            Text spdtext = spText.GetComponent<Text>();
            speedometer.speedText = spdtext;

            //Reset and reapply score
            PlayerScore playerscore = motorcycle.GetComponent<PlayerScore>();
            Transform scText = canvasObject.transform.Find("CurrentScore");
            Text scrtext = scText.GetComponent<Text>();
            playerscore.playerScoreText = scrtext;
            playerscore.playerScore = 0;

            //Reapply high score
            Transform hiText = canvasObject.transform.Find("HighScore");
            Text hightext = hiText.GetComponent<Text>();
            playerscore.highScoreText = hightext;
            /***********************************************************/

            //Reapply the camera
            GameObject camera = GameObject.Find("Main Camera");
            CameraController cameraController = camera.GetComponent<CameraController>();

            cameraController.target = motorcycle.transform;
            
            crash.crashed = false;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName ="LevelChunkData")]
public class LevelChunkData : ScriptableObject
{
    // Start is called before the first frame update
   public enum Direction
    {
        North, East, South, West
    }

    public Vector2 chunkSize = new Vector2(50f, 50f);

    public GameObject[] levelChunk;
    public Direction entryDirection;
    public Direction exitDirection;
}

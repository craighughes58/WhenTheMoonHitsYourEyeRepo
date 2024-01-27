using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    #region Private Variables

    #endregion
    #region Serialized Variables

    public Star[] starTypes;

    public GameObject horror;
    public GameObject moon;

    public GameObject gameSpace;

    public Vector2 upperRightCorner;
    public Vector2 lowerLeftCorner;

    public float moonOffset = 5;
    public float starCount = 30;

    #endregion


    public static GameController Instance;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        float sizeX = gameSpace.GetComponent<BoxCollider2D>().size.x;
        float sizeY = gameSpace.GetComponent<BoxCollider2D>().size.y;

        float leftLoc = -sizeX / 2;
        float rightLoc = sizeX / 2;
        float topLoc = sizeY / 2;
        float bottomLoc = -sizeY / 2;

        upperRightCorner = new Vector2(rightLoc, topLoc);
        lowerLeftCorner= new Vector2(leftLoc, bottomLoc);

        Instantiate(moon, new Vector2(0, bottomLoc - 5), Quaternion.identity);

        SpawnStars();



    }

    void Start()
    {
        
    }
    
    void Update()
    {
        
    }

    public void SpawnStars() 
    {
        //get random positions

        for(int i = 0; i < starCount; i++)
        {
            int starToSpawn = Random.RandomRange(0, starTypes.Length);

            float spawnX = Random.RandomRange(lowerLeftCorner.x, upperRightCorner.x);
            float spawnY = Random.RandomRange(lowerLeftCorner.y, upperRightCorner.y);


            Vector2 spawnPos = new Vector2(spawnX, spawnY);

            Instantiate(starTypes[starToSpawn], spawnPos, Quaternion.identity);

        }
    }


    #region End Conditions

    public void WinGame()
    {
        
    }

    public void LoseGame()
    {

    }

    #endregion
}

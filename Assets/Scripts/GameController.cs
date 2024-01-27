using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    #region Private Variables

    //
    private bool _failedCast = false;
    //
    private bool _successfulCast = false;
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

    [Tooltip("The ")]
    [SerializeField] private float _endingDelay;

    [Header("CAMERA POSITIONS")]
    [SerializeField] private Vector3 _winPosition;
    [SerializeField] private Vector3 _lossPosition;
    [SerializeField] private Vector3 _castingPosition;


    #endregion


    public static GameController Instance;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        //float bottomLoc = NewMethod();

        //Instantiate(moon, new Vector2(0, bottomLoc - 5), Quaternion.identity);

        //SpawnStars();

    }

    /*    private float NewMethod()
        {
            float sizeX = gameSpace.GetComponent<BoxCollider2D>().size.x;
            float sizeY = gameSpace.GetComponent<BoxCollider2D>().size.y;

            float leftLoc = -sizeX / 2;
            float rightLoc = sizeX / 2;
            float topLoc = sizeY / 2;
            float bottomLoc = -sizeY / 2;

            upperRightCorner = new Vector2(rightLoc, topLoc);
            lowerLeftCorner = new Vector2(leftLoc, bottomLoc);
            return bottomLoc;
        }
    */
    void Start()
    {

    }

    private IEnumerator ExecuteCoreLoop()
    {

        //show horror
        CameraController.Instance.UpdatePosition(HorrorBehaviour.Instance.GetCurrentPosition());
        //wait
        yield return new WaitForSeconds(CameraController.Instance.GetDesiredDuration() + 4f);
        if (_failedCast)
        {
            //wait
            HorrorBehaviour.Instance.MoveForward();
            _failedCast = false;
        }
        else if (_successfulCast)
        {
            //
            _successfulCast = false;
        }
        yield return new WaitForSeconds(CameraController.Instance.GetDesiredDuration() + 2f);
        //horror does roar
        //wait
        yield return new WaitForSeconds(CameraController.Instance.GetDesiredDuration() + 2f);
        //pan to show player
        CameraController.Instance.UpdatePosition(_castingPosition);
        //wait
        yield return new WaitForSeconds(CameraController.Instance.GetDesiredDuration() + 2f);
        //player cast

        //wait
        yield return new WaitForSeconds(CameraController.Instance.GetDesiredDuration() + 2f);
        //move to player position
        
        //WAIT UNTIL FAILURE
        yield return new WaitUntil(() => _failedCast);
        yield return null;
    }


    /*    public void SpawnStars() 
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
        }*/


    #region End Conditions

    public void WinGame()
    {
        StartCoroutine(WinningCoroutine());
    }

    private IEnumerator WinningCoroutine()
    {
        //Move Camera to Win Position
        CameraController.Instance.UpdatePosition(_winPosition);
        //wait 
        yield return new WaitForSeconds(10f);
        //Launch horror away
        HorrorBehaviour.Instance.StartLaunch();
        //wait
        yield return new WaitForSeconds(5f);
        //Fade to black 
        //wait 
        yield return new WaitForSeconds(5f);
        //change scene
        yield return null;
    }

    public void LoseGame()
    {
        StartCoroutine(LosingCoroutine());
    }

    private IEnumerator LosingCoroutine()
    {
        //wait
        yield return new WaitForSeconds(10f);
        //move camera to lose position
        CameraController.Instance.UpdatePosition(_lossPosition);
        //wait
        yield return new WaitForSeconds(5f);
        //The horror Consumes the Moon
        //wait
        yield return new WaitForSeconds(5f);
        //Fade to black
        //Wait
        yield return new WaitForSeconds(5f);
        //Change scene
        yield return null;
    }

    #endregion


    #region PlayerCommunication

    public void NotifyCastFailure()
    {
        _failedCast = true;
    }
    public void NotifyCastSuccess()
    {
        _successfulCast = true;
    }
    #endregion
}


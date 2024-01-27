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
    //
    private GameObject _currentPlayer;


    #endregion
    #region Serialized Variables


    [SerializeField] Animator playerAnimator;

    [Tooltip("")]
    [SerializeField] private float _endingDelay;

    [Tooltip("")]
    [SerializeField] private GameObject _player;
    [Tooltip("")]
    [SerializeField] private Vector3 _playerStartingPosition;


    [Header("CAMERA POSITIONS")]
    [SerializeField] private Vector3 _winPosition;
    [SerializeField] private Vector3 _lossPosition;
    [SerializeField] private Vector3 _castingPosition;
    [SerializeField] private bool _playStartingAnimation;

    #endregion

    public delegate void Alert();
    public Alert onRoundEnd;

    public static GameController Instance;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        //float bottomLoc = NewMethod();

        //Instantiate(moon, new Vector2(0, bottomLoc - 5), Quaternion.identity);

        GetComponent<StarSpawner>().SpawnAllStars();

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
        StartCoroutine(ExecuteCoreLoop());
    }

    private IEnumerator ExecuteCoreLoop()
    {
        if (_playStartingAnimation)
        {
            //show horror
            CameraController.Instance.UpdatePosition(HorrorBehaviour.Instance.GetCurrentPosition(),false);
            yield return new WaitForSeconds(CameraController.Instance.GetDesiredDuration() + .5f);
            //wait
            if (_failedCast)
            {
                //wait
                HorrorBehaviour.Instance.MoveForward();
                CameraController.Instance.UpdatePosition(HorrorBehaviour.Instance.GetCurrentPosition(),false);
                yield return new WaitForSeconds(CameraController.Instance.GetDesiredDuration());
                _failedCast = false;
            }
            else if (_successfulCast)
            {
                //
                _successfulCast = false;
            }
            //horror does roar
            HorrorBehaviour.Instance.ActivateRoar();
            //shake the screen
            ScreenShaker.shakeDuration = 2.5f;
            //wait
            yield return new WaitForSeconds(CameraController.Instance.GetDesiredDuration() + 2f);
        }
        _playStartingAnimation = true;
        playerAnimator.SetTrigger("Recall");
        //pan to show player
        CameraController.Instance.UpdatePosition(_castingPosition,false);
        //wait
        yield return new WaitForSeconds(CameraController.Instance.GetDesiredDuration() + 2f);

        //player cast
        playerAnimator.SetTrigger("Cast");
       
        //wait
        yield return new WaitForSeconds(CameraController.Instance.GetDesiredDuration());
        _currentPlayer = Instantiate(_player,_playerStartingPosition,Quaternion.identity);
        CastController.Instance.OnFire(null);
        //WAIT UNTIL FAILURE
        yield return new WaitUntil(() => _failedCast || _successfulCast);
        print("1");
        playerAnimator.SetTrigger("Recall");
        CameraController.Instance.UpdatePosition(_castingPosition, false);
        print("2");
        yield return new WaitForSeconds(CameraController.Instance.GetDesiredDuration() + 1f);
        print("3");
        Destroy(_currentPlayer);
        onRoundEnd?.Invoke();

        StartCoroutine(ExecuteCoreLoop());
    }





    #region End Conditions

    public void WinGame()
    {
        StartCoroutine(WinningCoroutine());
    }

    private IEnumerator WinningCoroutine()
    {
        //Move Camera to Win Position
        CameraController.Instance.UpdatePosition(_winPosition,false);
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
        CameraController.Instance.UpdatePosition(_lossPosition,false);
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


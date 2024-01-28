using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    [SerializeField] private Vector3 _lossPosition;
    [SerializeField] private Vector3 _castingPosition;
    [SerializeField] private bool _playStartingAnimation;

    #endregion

    public delegate void Alert();
    public Alert onRoundEnd;

    public static GameController Instance;
    Coroutine _coreGameLoop;

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
   
    void Start()
    {
        _coreGameLoop = StartCoroutine(ExecuteCoreLoop());
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
        yield return new WaitForSeconds(CameraController.Instance.GetDesiredDuration());

        //player cast
        playerAnimator.SetTrigger("Cast");
       
        //wait
        yield return new WaitForSeconds(CameraController.Instance.GetDesiredDuration());
        _currentPlayer = Instantiate(_player,_playerStartingPosition,Quaternion.identity);
        CastController.Instance.OnFire(null);
        //WAIT UNTIL FAILURE
        yield return new WaitUntil(() => _failedCast || _successfulCast);
        playerAnimator.SetTrigger("Idle");
        CameraController.Instance.UpdatePosition(_castingPosition, false);
        yield return new WaitForSeconds(CameraController.Instance.GetDesiredDuration() + 1f);
        Destroy(_currentPlayer);
        onRoundEnd?.Invoke();

        _coreGameLoop = StartCoroutine(ExecuteCoreLoop());
    }





    #region End Conditions

    public void WinGame()
    {
        StartCoroutine(WinningCoroutine());
    }

    private IEnumerator WinningCoroutine()
    {
        StopCoroutine(_coreGameLoop);
        //Move Camera to Win Position
        CameraController.Instance.UpdatePosition(HorrorBehaviour.Instance.GetCurrentPosition(),false);
        //wait 
        yield return new WaitForSeconds(2f);
        //Launch horror away
        HorrorBehaviour.Instance.StartLaunch();
        //wait
        yield return new WaitForSeconds(3f);
        //Fade to black 
        CameraController.Instance.UpdatePosition(_castingPosition, false);
        //wait 
        yield return new WaitForSeconds(2.5f);
        playerAnimator.Play("Happy");
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("WinCrawl");
        /*        print("dfk.juhgkjdfhg");
                FaderBehaviour.Instance.ChangeVignette();*/

        //change scene
        yield return null;
    }

    public void LoseGame()
    {
        StartCoroutine(LosingCoroutine());
    }

    private IEnumerator LosingCoroutine()
    {
        StopCoroutine(_coreGameLoop);
        //wait
        yield return new WaitForSeconds(2f);
        //move camera to lose position
        CameraController.Instance.UpdatePosition(_lossPosition,false);
        //wait
        yield return new WaitForSeconds(2f);

        playerAnimator.SetTrigger("Crying");
        //The horror Consumes the Moon
        HorrorBehaviour.Instance.StartLaunch(true);
        //wait
        yield return new WaitForSeconds(5f);
        //Fade to black
        //Wait
        yield return new WaitForSeconds(5f);
        //Change scene
        SceneManager.LoadScene("LossCrawl");
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


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


    [SerializeField] Animator playerAnimator;

    [Tooltip("")]
    [SerializeField] private float _endingDelay;

    [Tooltip("The sound made when the bobber fails")]
    [SerializeField] private AudioClip _failedCastSound;


    [Header("CAMERA POSITIONS")]
    [SerializeField] private Vector3 _winPosition;
    [SerializeField] private Vector3 _lossPosition;
    [SerializeField] private Vector3 _castingPosition;
    [SerializeField] private bool _playStartingAnimation;

    [Header("Star Spawn Values")]
    [SerializeField] private float _starsToSpawn;
    [Tooltip("Ends the Star spawn after failing to place a start this many times")]
    [SerializeField] private float _maxAttempts;
    [Tooltip("Stars must be at least this far apart from each other")]
    [SerializeField] private float _minStarDist;
    [SerializeField] private Vector2 _xMinandMax;
    [SerializeField] private Vector2 _yMinandMax;
    [SerializeField] private GameObject _starNode;

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

        SpawnStars();

    }

    void SpawnStars()
    {
        int attempts = 0;
        for(int i = 0; i < _starsToSpawn; ++i)
        {
            attempts = 0;
            bool starSpawned = false;
            while(!starSpawned)
            {
                ++attempts;
                if(attempts >= _maxAttempts)
                {
                    return;
                }
                starSpawned = TrySpawnStar();
            }
        }
    }

    bool TrySpawnStar()
    {
        Vector2 starPos = new Vector2(Random.Range(_xMinandMax.x, _xMinandMax.y), Random.Range(_yMinandMax.x, _yMinandMax.y));
        if(Physics2D.CircleCast(starPos, _minStarDist, Vector2.zero)) return false;

        Instantiate(_starNode, starPos, Quaternion.identity);

        return true;
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
            CameraController.Instance.UpdatePosition(HorrorBehaviour.Instance.GetCurrentPosition());
            yield return new WaitForSeconds(CameraController.Instance.GetDesiredDuration() + .5f);
            //wait
            if (_failedCast)
            {
                //wait
                HorrorBehaviour.Instance.MoveForward();
                CameraController.Instance.UpdatePosition(HorrorBehaviour.Instance.GetCurrentPosition());
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
        //pan to show player
        CameraController.Instance.UpdatePosition(_castingPosition);
        //wait
        yield return new WaitForSeconds(CameraController.Instance.GetDesiredDuration() + 2f);

        //player cast
        playerAnimator.Play("Cast");

        //wait
        yield return new WaitForSeconds(CameraController.Instance.GetDesiredDuration());
        CastController.Instance.OnFire(null);
        //move to player position

        //WAIT UNTIL FAILURE
        yield return new WaitUntil(() => _failedCast || _successfulCast);
        yield return null;
        onRoundEnd?.Invoke();
    }





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
        AudioManager.Instance.PlayClip2D(_failedCastSound);
        _failedCast = true;
    }
    public void NotifyCastSuccess()
    {
        _successfulCast = true;
    }
    #endregion
}


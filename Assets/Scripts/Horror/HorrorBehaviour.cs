using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class HorrorBehaviour : MonoBehaviour
{
    #region Private Variables
    //how much damage the monster can take
    private int _health;

    //The current position of the horror in the array
    private int _currentPosition;
    #endregion
    #region Serialized Variables
    [Tooltip("The sound the horror makes when it moves forward")]
    [SerializeField] private AudioClip _roar;
    [Tooltip("The sound the horror makes when it gets hit")]
    [SerializeField] private AudioClip _hit;

    [Tooltip("The position that the monster will move to and from")]
    [SerializeField] private List<Vector3> _positionNodes;

    [Tooltip("The List of eyes the monster has (directly connected to health)")]
    [SerializeField] private List<GameObject> _eyes;

    [Tooltip("How fast the object moves ")]
    [SerializeField] private float _speed;
    #endregion
    public static HorrorBehaviour Instance;

    [SerializeField] Animator zaneMouthAnimator;
    [SerializeField] Animator addyMouthAnimator;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        } 
    }


    // Start is called before the first frame update
    void Start()
    {
        _currentPosition = 0;
        _health = _eyes.Count;
    }



    private void FixedUpdate()
    {
        if(_currentPosition < _positionNodes.Count && transform.position !=_positionNodes[_currentPosition] && _health > 0)
        {
            transform.position = Vector3.MoveTowards(transform.position, _positionNodes[_currentPosition],_speed);
        }
    }

    public void LoseEye()
    {
        _health--;
        AudioManager.Instance.PlayClip2D(_hit);
        if (_health <= 0)
        {
            GameController.Instance.WinGame();
            //WIN condition
            return;
        }
        GameController.Instance.NotifyCastSuccess();
    }

    public void ActivateRoar()
    {
        AudioManager.Instance.PlayClip2D(_roar);
        zaneMouthAnimator.SetBool("isRoaring", true);
        addyMouthAnimator.SetBool("isRoaring", true);
        StartCoroutine(StopRoar());
    }

    IEnumerator StopRoar()
    {
        yield return new WaitForSeconds(_roar.length);
        zaneMouthAnimator.SetBool("isRoaring", false);
        addyMouthAnimator.SetBool("isRoaring", false);
    }

    #region Movement


    public void MoveForward()
    {
        _currentPosition++;

        //Win Condition
        if (_currentPosition >= _positionNodes.Count)
        {
            GameController.Instance.LoseGame();
            //LOSE conditions
        }
    }
     
    public void MoveBackward()
    {
        _currentPosition--;
        if(_currentPosition < 0)
        {
            _currentPosition = 0;
        }
    }

    public void StartLaunch(bool lose)
    {
        AudioManager.Instance.PlayClip2D(_roar);
        StartCoroutine(LaunchIntoSpace(lose));
    }

    private IEnumerator LaunchIntoSpace(bool lose)
    {
        _health = 0;
        float _target;
        if (lose)
        {
            _target = -1.62f;
        }
        else
        {
            _target = 2000000f;
        }
        while (true)
        {
            yield return new WaitForEndOfFrame();
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(0f,_target,0f), _speed);
        }
    }
    #endregion

    #region Getters and Setters

    public int GetHealth()
    {
        return _health;
    }

    public Vector3 GetCurrentPosition()
    {
        if (_currentPosition < _positionNodes.Count) return _positionNodes[_currentPosition];
        else return transform.position;
    }

    public int GetIndex()
    {
        return _currentPosition;
    }

    #endregion
}

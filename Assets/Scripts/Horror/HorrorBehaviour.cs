using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    #endregion
    public static HorrorBehaviour Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }



    #region Serialized Variables
    [Tooltip("The position that the monster will move to and from")]
    [SerializeField] private List<Vector3> _positionNodes;

    [Tooltip("The List of eyes the monster has (directly connected to health)")]
    [SerializeField] private List<GameObject> _eyes;

    [Tooltip("How fast the object moves ")]
    [SerializeField] private float _speed;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        _currentPosition = _positionNodes.Count - 1;
    }



    private void FixedUpdate()
    {
        if(transform.position !=_positionNodes[_currentPosition])
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
        }
    }

    public void ActivateRoar()
    {
        AudioManager.Instance.PlayClip2D(_roar);
    }

    #region Movement


    public void MoveForward()
    {
        _currentPosition++;

        //Win Condition
        if (_currentPosition == _positionNodes.Count)
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

    public void StartLaunch()
    {
        AudioManager.Instance.PlayClip2D(_roar);
        LaunchIntoSpace();
    }

    private IEnumerator LaunchIntoSpace()
    {
        while (true)
        {
            yield return new WaitForSeconds(.1f);
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(0f,2000000f,0f), _speed);
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
        return _positionNodes[_currentPosition];
    }

    public int GetIndex()
    {
        return _currentPosition;
    }

    #endregion
}

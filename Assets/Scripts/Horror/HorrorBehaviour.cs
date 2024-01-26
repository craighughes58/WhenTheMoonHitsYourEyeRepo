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
        _currentPosition = _positionNodes.Count;
        
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
        if(_health <= 0)
        {
            //WIN condition
        }
    }

    #region Movement


    public void MoveForward()
    {
        _currentPosition++;
        //Win Condition
        if(_currentPosition == _positionNodes.Count)
        {
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
}

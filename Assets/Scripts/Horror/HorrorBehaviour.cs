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

    #endregion
    // Start is called before the first frame update
    void Start()
    {
        _currentPosition = _positionNodes.Count;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if(transform.position !=_positionNodes[_currentPosition])
        {

        }
    }

    public void LoseEye()
    {
        _health--;
        if(_health <= 0)
        {

        }
    }

    #region Movement


    public void MoveForward()
    {

    }

    public void MoveBackward()
    {

    }
    #endregion 
}

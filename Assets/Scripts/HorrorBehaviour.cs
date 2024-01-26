using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorrorBehaviour : MonoBehaviour
{
    #region Private Variables
    //how much damage the monster can take
    private int _health;
    #endregion
    #region Serialized Variables
    [Tooltip("The position that the monster will move to and from")]
    [SerializeField] private List<Transform> _positionNodes;

    [Tooltip("The List of eyes the monster has (directly connected to health)")]
    [SerializeField] private List<GameObject> _eyes;

    #endregion
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoseEye()
    {
        _health--;
        if(_health <= 0)
        {

        }
    }
}

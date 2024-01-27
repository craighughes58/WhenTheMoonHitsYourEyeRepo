using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakingCharacterBehaviour : MonoBehaviour
{

    #region Serialized Variables
    
    private Vector3 _startingPosition;
    
    //private Quaternion _startingRotation;
    #endregion

    #region Private Variables

    [Tooltip("How eratically the object shakes")]
    [SerializeField] private float _shakeAMT;

    #endregion
    // Start is called before the first frame update
    void Start()
    {
        //_startingRotation = transform.rotation;
        _startingPosition = transform.position;
        StartCoroutine(StartShaking());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator StartShaking()
    {
        while (true)
        {
            transform.position = transform.position + (Random.insideUnitSphere * _shakeAMT);
            //transform.rotation = transform.rotation * Random.rotation;
            yield return new WaitForSeconds(.1f);
            transform.position = _startingPosition;
            //transform.rotation = _startingRotation;
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    #region Serialized Variables
    [Tooltip("How fast the camera moves towards the next position")]
    [SerializeField] private float _speed;
    #endregion

    #region Private Variables
    //the position the camera is moving to
    private Vector3 _targetPosition;
    //the position where the camera was
    private Vector3 _lastPositon;
    #endregion

    public static CameraController Instance;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _targetPosition = transform.position;
        _lastPositon = transform.position;
    }

    float dist = 0;
    private void FixedUpdate()
    {
        dist += _speed * Time.fixedDeltaTime;
        if (_targetPosition != transform.position)
        {
            transform.position = Vector3.MoveTowards(transform.position, _targetPosition,_speed);//Vector3.Lerp(_lastPositon, _targetPosition, dist);
        }
    }

    public void UpdatePosition(Vector3 _nextPos)
    {
        _lastPositon = transform.position;
        _targetPosition = new Vector3(_nextPos.x, _nextPos.y, -10f);
    }
}

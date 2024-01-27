using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BobberManager : MonoBehaviour
{
    [SerializeField] GameObject castPointer;
    Rigidbody2D rb;


    [Tooltip("The sound made when the bobber fails")]
    [SerializeField] private AudioClip _failedCastSound;

    //the last viable position of the player
    private Vector3 _lastPosition;

    //
    private bool _hasFailed = false;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        StartCast();
    }

    void StartCast(float pointerDist = .45f)
    {
        CastController cast = Instantiate(castPointer).GetComponent<CastController>();
        cast.transform.position = transform.position;
        cast.SetPlayer(this, pointerDist);

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(_hasFailed)
        {
            return;
        }
        if (other.tag.Equals("MainCamera"))
        {
            StartCoroutine(ReturnToLastPosition());
            return;

        }
        IHookable hookableObj = other.GetComponent<IHookable>();
        if (hookableObj == null) return;

        rb.velocity = Vector2.zero;
        hookableObj.Hook(this);
        Star star = other.GetComponent<Star>();
        if (star == null) return;

        transform.position = star.transform.position;
        CameraController.Instance.UpdatePosition(transform.position,true);
        StartCast(star.starRadius);
        _lastPosition = transform.position;
    }

/*    private IEnumerator DelayedCameraUpdate()
    {
        yield return new WaitForSeconds 
    }*/

    public void MissPosition()
    {
        StartCoroutine(ReturnToLastPosition());
    }
    private IEnumerator ReturnToLastPosition()
    {
        ScreenShaker.shakeDuration = .1f;
        //fail cast sound
        AudioManager.Instance.PlayClip2D(_failedCastSound);
        while (transform.position != _lastPosition)
        {
            transform.position = Vector3.MoveTowards(transform.position, _lastPosition, castPointer.GetComponent<CastController>().GetCastSpeed());
            yield return new WaitForEndOfFrame();

        }
        print("DONJE");
        
        GameController.Instance.NotifyCastFailure();
    }



}

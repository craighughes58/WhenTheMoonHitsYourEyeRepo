using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BobberManager : MonoBehaviour
{
    [SerializeField] GameObject castPointer;
    Rigidbody2D rb;

    [SerializeField] AudioManager _audioM;

    [Tooltip("The sound made when the bobber fails")]
    [SerializeField] private AudioClip _failedCastSound;

    [Tooltip("The sound made when the bobber is cast")]
    [SerializeField] private AudioClip _castSound;

    [Tooltip("The sound made when the bobber hits a star")]
    [SerializeField] private AudioClip _hitStar;

    [Tooltip("The particles that appear when the bobber hits the wall")]
    [SerializeField] private GameObject _explosionParticles;
    //the last viable position of the player
    private Vector3 _lastPosition;

    //
    private bool _hasFailed = false;


    private void Awake()
    {
        _audioM = FindObjectOfType<AudioManager>();

        rb = GetComponent<Rigidbody2D>();
        StartCast();
    }

    void StartCast(float pointerDist = .45f)
    {
        if (_hasFailed) return;

        

        CastController cast = Instantiate(castPointer).GetComponent<CastController>();
        cast.transform.position = transform.position;
        cast.SetPlayer(this, pointerDist);
        cast.castSound = _castSound;

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(_hasFailed)
        {
            return;
        }
        if (other.tag.Equals("MainCamera"))
        {
            if (transform.position == new Vector3(3, 10, 0)) return;
            //print("globes");
            Instantiate(_explosionParticles, transform.position, new Quaternion(transform.rotation.x, transform.rotation.y, transform.rotation.z + 180f, transform.rotation.w));
            StartCoroutine(ReturnToLastPosition());
            GetComponent<Collider2D>().enabled = false;
            return;

        }
        IHookable hookableObj = other.GetComponent<IHookable>();
        if (hookableObj == null) return;

        rb.velocity = Vector2.zero;
        hookableObj.Hook(this);
        Star star = other.GetComponent<Star>();
        if (star == null) return;

        _audioM.PlayClip2D(_hitStar);

        transform.position = star.transform.position;
        CameraController.Instance.UpdatePosition(transform.position,true);
        StartCast(star.starRadius);
        _lastPosition = transform.position;
    }

    public void MissPosition()
    {
        StartCoroutine(ReturnToLastPosition());
    }
    private IEnumerator ReturnToLastPosition()
    {
        ScreenShaker.shakeDuration = .1f;
        _hasFailed = true;
        //fail cast sound
        AudioManager.Instance.PlayClip2D(_failedCastSound);
        rb.velocity = Vector2.zero;
        while (transform.position != _lastPosition)
        {
            transform.position = Vector3.MoveTowards(transform.position, _lastPosition, castPointer.GetComponent<CastController>().GetCastSpeed());
            yield return new WaitForEndOfFrame();

        }
        GameController.Instance.NotifyCastFailure();
    }



}

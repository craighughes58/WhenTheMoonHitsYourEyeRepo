using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrumblingStar : Star
{
    public AudioManager _audio;
    public AudioClip explode;

    public float timeBeforeCrumble = 3f;
    public float shakeAdder = 1;

    private BobberManager _player;
    [Tooltip("The objects that spawn after destruction")]
    [SerializeField] private GameObject _particles;

    public Animator _anim;
    public float timer;

    private void Start()
    {
        _audio = FindObjectOfType<AudioManager>();
    }

    public override void Hook(BobberManager bobber)
    {
        _anim = GetComponent<Animator>();

        StartCoroutine(Crumble());
    }

    private IEnumerator Crumble()
    {
        timer = 0;

        while (timer < timeBeforeCrumble)
        {
            timer += Time.deltaTime;

            _anim.speed = timer * timer + shakeAdder;
            //Debug.Log(_anim.speed);

            yield return null;
        }
        //yield return new WaitForSeconds(timeBeforeCrumble);


        _audio.PlayClip2D(explode);
        
        gc.NotifyCastFailure();
        Instantiate(_particles,transform.position,Quaternion.identity);
        if(_player!= null)
        {
            _player.MissPosition();
        }
        Destroy(this.gameObject);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<BobberManager>() != null)
        {
            StopAllCoroutines();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<BobberManager>() != null)
        {
            _player = collision.GetComponent<BobberManager>();
        }
    }

}

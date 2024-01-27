using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BobberManager : MonoBehaviour
{
    [SerializeField] GameObject castPointer;
    Rigidbody2D rb;

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

        IHookable hookableObj = other.GetComponent<IHookable>();
        if (hookableObj == null) return;

        rb.velocity = Vector2.zero;
        hookableObj.Hook(this);
        Star star = other.GetComponent<Star>();
        if (star == null) return;

        transform.position = star.transform.position;
        CameraController.Instance.UpdatePosition(transform.position);
        StartCast(star.starRadius);
    }

/*    private IEnumerator DelayedCameraUpdate()
    {
        yield return new WaitForSeconds 
    }*/
}

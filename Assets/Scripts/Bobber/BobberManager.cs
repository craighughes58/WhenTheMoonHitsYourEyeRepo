using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BobberManager : MonoBehaviour
{
    [SerializeField] GameObject castPointer;

    private void Awake()
    {
        StartCast();
    }

    void StartCast(float pointerDist = .45f)
    {
        Instantiate(castPointer).GetComponent<CastController>().SetPlayer(this, pointerDist);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        IHookable hookableObj = other.GetComponent<IHookable>();

        if (hookableObj == null) return;
        
        hookableObj.Hook(this);
        Star star = other.GetComponent<Star>();
        if (star == null) return;

        transform.position = star.transform.position;
        StartCast(star.starRadius);
    }
}

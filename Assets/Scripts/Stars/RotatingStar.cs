using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingStar : Star
{
    public bool playerOnStar = false;

    public override void Hook(BobberManager bobber)
    {
        StartCoroutine(Rotate(bobber));
    }

    private IEnumerator Rotate(BobberManager bobber)
    {
        yield return null;
        int result = (Random.Range(0, 2) * 2) - 1;

        CastController cast = FindObjectOfType<CastController>();
        if(cast != null ) cast.ForceRotDir(result);

        yield return new WaitUntil(() => !playerOnStar);
        
        StopAllCoroutines();
    }
}

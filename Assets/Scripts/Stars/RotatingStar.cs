using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingStar : Star
{
    public bool playerOnStar = false;

    public override void Hook(BobberManager bobber)
    {
        StartCoroutine(Rotate());
    }

    private IEnumerator Rotate()
    {
        //rotate the player

        yield return new WaitUntil(() => !playerOnStar);
        
        StopAllCoroutines();
    }
}

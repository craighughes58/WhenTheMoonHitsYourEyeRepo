using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrumblingStar : Star
{
    public float timeBeforeCrumble = 3f;

    public override void Hook(BobberManager bobber)
    {
        StartCoroutine(Crumble());
    }

    private IEnumerator Crumble()
    {
        yield return new WaitForSeconds(timeBeforeCrumble);

        gc.NotifyCastFailure();

        StopAllCoroutines();
        Destroy(this.gameObject);
    }

}

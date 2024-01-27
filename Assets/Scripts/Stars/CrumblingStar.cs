using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrumblingStar : Star
{
    public float timeBeforeCrumble = 3f;

    [Tooltip("The objects that spawn after destruction")]
    [SerializeField] private GameObject _particles;

    public override void Hook(BobberManager bobber)
    {
        StartCoroutine(Crumble());
    }

    private IEnumerator Crumble()
    {
        yield return new WaitForSeconds(timeBeforeCrumble);

        gc.NotifyCastFailure();
        Instantiate(_particles,transform.position,Quaternion.identity);
        Destroy(this.gameObject);
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.GetComponent<BobberManager>() != null)
        {
            StopAllCoroutines();
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarEater : MonoBehaviour
{
    public ParticleSystem explodeStars;
    [SerializeField] AudioClip _explodeSFX;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("ate " + collision.gameObject.name);

        if (collision.gameObject.tag == "Star")
        {
            
            Vector3 spawnPos = collision.transform.position;

            Destroy(Instantiate(explodeStars, collision.transform.position, Quaternion.identity), 5);
            AudioManager.Instance.PlayClip2D(_explodeSFX);
            Destroy(collision.gameObject);
        }
    }
}

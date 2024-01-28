using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarEater : MonoBehaviour
{
    public ParticleSystem explodeStars;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("ate " + collision.gameObject.name);

        if (collision.gameObject.tag == "Star")
        {
            
            Vector3 spawnPos = collision.transform.position;

            Instantiate(explodeStars, collision.transform.position, Quaternion.identity);

            Destroy(collision.gameObject);
        }
    }
}

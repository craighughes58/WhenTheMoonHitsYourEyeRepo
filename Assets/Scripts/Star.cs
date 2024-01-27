using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]

public class Star : MonoBehaviour, IHookable
{
    public string Name = "StarName";
    public float starRadius = 1f;


    public void caughtByPlayer()
    {

    }

    public void Hook(BobberManager bobber)
    {

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]

public class Star : MonoBehaviour, IHookable
{
    public string Name = "StarName";
    public float starRadius = 1f;

    [HideInInspector]
    public GameController gc;

    private void Awake()
    {
        gc = FindObjectOfType<GameController>();
    }

    public virtual void Hook(BobberManager bobber)
    {
        Debug.Log("Something is wrong");
    }
}

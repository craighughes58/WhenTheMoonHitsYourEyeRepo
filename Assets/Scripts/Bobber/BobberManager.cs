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

    void StartCast()
    {
         Instantiate(castPointer).GetComponent<CastController>().SetPlayer(this);
    }
}

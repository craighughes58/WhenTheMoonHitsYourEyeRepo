using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarBig : Star
{
    public override void Hook(BobberManager bobber)
    {
        Debug.Log("I'm gonna esploooooode");
    }
}

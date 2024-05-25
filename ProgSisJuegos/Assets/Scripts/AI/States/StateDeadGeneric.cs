using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateDeadGeneric<T> : StateBase<T>
{
    public StateDeadGeneric()
    {

    }

    public override void Execute()
    {
        Debug.Log("Dead State");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Behaviour : MonoBehaviour
{
    protected FiniteStateMachine fsm;
    public virtual void EnterBehaviour()
    {
        // Do something when entering the state
    }

    public virtual void UpdateBehaviour()
    {
        // Do something while in the state
    }

    public virtual void ExitBehaviour()
    {
        // Do something when exiting the state
    }
    public void SetFSM(FiniteStateMachine newFSM)
    {
        this.fsm = newFSM;
    }
}

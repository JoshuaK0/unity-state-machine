using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Decision : MonoBehaviour
{
    protected FiniteStateMachine fsm;

    [SerializeField] bool passed;

    public void SetFSM(FiniteStateMachine newFSM)
    {
        fsm = newFSM;
    }
    public virtual void InitDecision()
    {
    }
    public abstract bool Evaluate();

    public void UpdateResult(bool result)
    {
        passed = result;
    }
}

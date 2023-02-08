using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public abstract class FiniteStateMachine : MonoBehaviour
{
    [Header("FSM Properties")]

    [SerializeField] State startState;

    [SerializeField] protected State currentState;

    int tracebackLength;

    [SerializeField] List<FSMTraceback> traceback = new List<FSMTraceback>();

    public virtual void Start()
    {
        tracebackLength = traceback.Count;
        ChangeState(startState);
    }

    public void ChangeState(State newState)
    {
        State lastState = currentState;
        DoChange(newState);
        traceback.Add(new FSMTraceback(currentState, lastState, null));
    }

    public void ChangeState(State newState, Transition transition)
    {
        State lastState = currentState;
        DoChange(newState);
        AddTraceback(currentState, lastState, transition);
    }

    void DoChange(State newState)
    {
        if (currentState != null)
        {
            currentState.ExitState();
        }

        currentState = newState;
        currentState.EnterState(this);
    }

    void AddTraceback(State newState, State lastState, Transition transition)
    {
        traceback.Add(new FSMTraceback(newState, lastState, transition));
        while (traceback.Count > tracebackLength)
        {
            traceback.RemoveAt(0);
        }
    }

    public virtual void Update()
    {
        if (currentState != null)
        {
            currentState.UpdateState();
        }
    }

    public virtual void OnDrawGizmos()
    {
        if(currentState!=null)
        {
            Handles.Label(transform.position, currentState.ToString().Remove(currentState.ToString().Length - 8));
        }
    }
}

[System.Serializable]

public class FSMTraceback
{
    public string stateName;
    public State toState;
    public State fromState;
    public Transition transition;

    public FSMTraceback(State toState, State fromState, Transition transition)
    {
        if (fromState != null)
        {
            this.stateName = toState.ToString().Remove(toState.ToString().Length - 8) + " from " + fromState.ToString().Remove(fromState.ToString().Length - 8);
        }
        else
        {
            this.stateName = "Set state to " + toState.ToString().Remove(toState.ToString().Length - 8);
        }
        this.toState = toState;
        this.fromState = fromState;
        this.transition = transition;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class State : MonoBehaviour
{
    protected FiniteStateMachine fsm;

    [SerializeField] List<Behaviour> behaviours = new List<Behaviour>();
    [SerializeField] List<Transition> transitions = new List<Transition>();

    public void EnterState(FiniteStateMachine newFSM)
    {
        fsm = newFSM;
        foreach (Behaviour behaviour in behaviours)
        {
            behaviour.SetFSM(fsm);
        }

        foreach (Behaviour behaviour in behaviours)
        {
            behaviour.EnterBehaviour();
        }

        foreach (Transition transition in transitions)
        {
            foreach (Decision decision in transition.GetDecisions())
            {
                decision.SetFSM(fsm);
                decision.InitDecision();
            }
        }
    }

    public void UpdateState()
    {
        foreach (Behaviour behaviour in behaviours)
        {
            behaviour.UpdateBehaviour();
        }

        foreach (Transition transition in transitions)
        {
            bool canTransition = true;
            foreach (Decision decision in transition.GetDecisions())
            {
                bool decisionResult = decision.Evaluate();
                if (!decisionResult)
                {
                    canTransition = false;
                }
                decision.UpdateResult(decisionResult);
            }
            if(canTransition)
            {
                fsm.ChangeState(transition.GetTransitionState(), transition);
            }
        }
    }

    public void ExitState()
    {
        foreach (Behaviour behaviour in behaviours)
        {
            behaviour.ExitBehaviour();
        }
    }
}

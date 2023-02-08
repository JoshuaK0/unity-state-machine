using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Transition
{
    [SerializeField]  string name;
    [SerializeField] State goesToState;
    [SerializeField] [TextArea(3, 10)] string when;
    [SerializeField] List<Decision> decisions;

    public List<Decision> GetDecisions()
    {
        return decisions;
    }
    public State GetTransitionState()
    {
        return goesToState;
    }
}

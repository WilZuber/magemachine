// Authored by Wilson Zuber in an attempt to make the AI code cleaner, and work better.
// OOD Patterns Applied: State Pattern
// 2023-12-10
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAIContext : MonoBehaviour
{
    private IMeleeAIState currentState; // the current state of the AI
    private IMeleeAIState lookState; // in the future we may look into making the states singletons
    private IMeleeAIState attackState;
    private IMeleeAIState chaseState;

    public void SetState(IMeleeAIState state)
    {
        currentState = state;
    }

    public void PlayerEnterSight()
    {
        currentState.PlayerEnterSight();
    }

    public void PlayerEnterMeleeRange()
    {
        currentState.PlayerEnterMeleeRange();
    }

    public void PlayerLeaveSight()
    {
        currentState.PlayerLeaveSight();
    }

    public void PlayerLeaveMeleeRange()
    {
        currentState.PlayerLeaveMeleeRange();
    }
}

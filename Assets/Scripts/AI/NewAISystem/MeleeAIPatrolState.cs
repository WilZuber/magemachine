// Authored by Wilson Zuber in an attempt to make the AI code cleaner, and work better.
// OOD Patterns Applied: State Pattern
// 2023-12-10
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// In the state pattern, MeleeAIPatrolState is one state the MeleeAI can currently be in.
public class MeleeAIPatrolState : IMeleeAIState
{
    private MeleeAIContext meleeAI;

    public MeleeAIPatrolState(MeleeAIContext meleeAI)
    {
        this.meleeAI = meleeAI;
    }

    void Update()
    {
        meleeAI.BeginPatrol();
    }

    // Represents the transition that would happen if player enters sight
    public void PlayerEnterSight()
    {
        Debug.Log("entering chase state");
        meleeAI.SetState(meleeAI.GetChaseState());
    }

    // Represents the transition that would happen if player enters melee range
    public void PlayerEnterMeleeRange()
    {
        Debug.Log("entering attack state");
        meleeAI.SetState(meleeAI.GetAttackState());
    }

    // Represents the transition that would happen if player leaves sight
    public void PlayerLeaveSight()
    {
        Debug.Log("entering look state");
        meleeAI.SetState(meleeAI.GetLookState());
    }

    // Represents the transition that would happen if player leaves melee range
    public void PlayerLeaveMeleeRange()
    {
        Debug.Log("entering patrol state");
        meleeAI.SetState(meleeAI.GetPatrolState());
    }
}

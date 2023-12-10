// Authored by Wilson Zuber in an attempt to make the AI code cleaner, and work better.
// OOD Patterns Applied: State Pattern
// 2023-12-10
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// Contexts in the State Pattern represent the system. Think of a State Machine Diagram. The entire diagram would be the context.
public class MeleeAIContext : MonoBehaviour
{
    private IMeleeAIState currentState; // the current state of the AI
    private IMeleeAIState lookState; // in the future we may look into making the states singletons
    private IMeleeAIState attackState;
    private IMeleeAIState chaseState;
    private IMeleeAIState patrolState;

    private NavMeshAgent agent;

    // On start we want to do a few things:
    // Find our Melee AI agent and assign it to our field
    // Initialize lookState, attackState, patrolState and chaseState to be 
    //          references to the corresponding Concrete States
    // Initialize currentState to patrol.
    void Start()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
        lookState = new MeleeAILookState(this);
        attackState = new MeleeAIAttackState(this);
        chaseState = new MeleeAIChaseState(this);
        patrolState = new MeleeAIPatrolState(this);

        SetState(patrolState); // we want to start off patrolling
    }

    // STATE PATTERN METHODS
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

    // AI BEHAVIOR METHODS
}

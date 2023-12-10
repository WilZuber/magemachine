// Authored by Wilson Zuber in an attempt to make the AI code cleaner, and work better.
// OOD Patterns Applied: State Pattern
// 2023-12-10
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// In the state pattern, MeleeAILookState is one state the MeleeAI can currently be in.
public class MeleeAILookState : IMeleeAIState
{
    private MeleeAIContext meleeAI;

    public MeleeAILookState(MeleeAIContext meleeAI)
    {
        this.meleeAI = meleeAI;
    }

    public void PlayerEnterSight()
    {
        // What should the Melee AI do if the player is in sight when ai is looking around
    }

    public void PlayerEnterMeleeRange()
    {
        // What should the Melee AI do if the player is in melee range when ai is looking around
    }

    public void PlayerLeaveSight()
    {
        // What should the Melee AI do if the player leaves sight when ai is looking around
    }

    public void PlayerLeaveMeleeRange()
    {
        // What should the Melee AI do if the player leaves melee range when ai is looking around
    }
}

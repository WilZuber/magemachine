// Authored by Wilson Zuber in an attempt to make the AI code cleaner, and work better.
// OOD Patterns Applied: State Pattern
// 2023-12-10
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAIChaseState : IMeleeAIState
{
    private MeleeAIContext meleeAI;

    public MeleeAIChaseState(MeleeAIContext meleeAI)
    {
        this.meleeAI = meleeAI;
    }

    public void PlayerInSight()
    {
        // What should the Melee AI do if the player is in sight when ai is chasing
    }

    public void PlayerInMeleeRange()
    {
        // What should the Melee AI do if the player is in melee range when ai is chasing
    }
}

// Authored by Wilson Zuber in an attempt to make the AI code cleaner, and work better.
// OOD Patterns Applied: State Pattern
// 2023-12-10
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMeleeAIState
{
    public void PlayerInSight();
    public void PlayerInMeleeRange();
}

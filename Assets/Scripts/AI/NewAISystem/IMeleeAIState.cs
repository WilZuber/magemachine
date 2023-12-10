// Authored by Wilson Zuber in an attempt to make the AI code cleaner, and work better.
// OOD Patterns Applied: State Pattern
// 2023-12-10
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// In the State Pattern, we use an interface to hold all transitions as abstract methods.
public interface IMeleeAIState
{
    public void PlayerEnterSight();
    public void PlayerEnterMeleeRange();
    public void PlayerLeaveSight();
    public void PlayerLeaveMeleeRange();
}

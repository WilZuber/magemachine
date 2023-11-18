using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAIBehavior
{
    public void Act(AI ai);
}

public class AIWaitBehavior : IAIBehavior
{
    public void Act(AI ai)
    {
        if (ai.CanSeePlayer())
        {
            ai.state = ai.chase;
        }
        else
        {
            ai.Idle();
        }
    }
}

public class AIChaseBehavior : IAIBehavior
{
    float attackDistance;

    public AIChaseBehavior(float attackDistance)
    {
        this.attackDistance = attackDistance;
    }

    public void Act(AI ai)
    {
        if (ai.CanSeePlayer())
        {
            ai.Chase();
            if (ai.agent.remainingDistance < attackDistance)
            {
                ai.state = ai.attack;
            }
        }
        //todo: maintain last destination
        else
        {
            ai.state = ai.wait;
        }
    }
}

public interface IAIAttackBehavior : IAIBehavior
{

}

public class AIShootingBehavior : IAIAttackBehavior
{
    WeaponHolder guns;

    public AIShootingBehavior(WeaponHolder guns)
    {
        this.guns = guns;
    }

    public void Act(AI ai)
    {
        
    }
}

public class AIMeleeBehavior : IAIAttackBehavior
{
    MeleeWeaponController melee;
    float attackDistance;

    public AIMeleeBehavior(MeleeWeaponController melee, float attackDistance)
    {
        this.melee = melee;
        this.attackDistance = attackDistance;
    }

    public void Act(AI ai)
    {
        melee.Attack();
        ai.Chase();
        if (ai.agent.remainingDistance > attackDistance)
        {
            ai.state = ai.chase;
        }
    }
}

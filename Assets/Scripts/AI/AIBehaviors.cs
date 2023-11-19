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
    float startAttackDistance;

    public AIChaseBehavior(float startAttackDistance)
    {
        this.startAttackDistance = startAttackDistance;
    }

    public void Act(AI ai)
    {
        if (ai.CanSeePlayer())
        {
            ai.Chase();
            if (ai.agent.remainingDistance < startAttackDistance)
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
    float defaultSpeed;
    float escapeSpeed;
    float minAttackDistanceSq;
    float maxAttackDistanceSq;

    public AIShootingBehavior(WeaponHolder guns, float minAttackDistance, float maxAttackDistance, float defaultSpeed, float escapeSpeed)
    {
        this.guns = guns;
        minAttackDistanceSq = minAttackDistance*minAttackDistance;
        maxAttackDistanceSq = maxAttackDistance*maxAttackDistance;
        this.defaultSpeed = defaultSpeed;
        this.escapeSpeed = escapeSpeed;
    }

    public void Act(AI ai)
    {
        ai.Chase();
        if (ai.CanSeePlayer())
        {
            float sqDistance = ai.SquareDistanceToPlayer();
            if (sqDistance > minAttackDistanceSq)
            {
                if (sqDistance < maxAttackDistanceSq)
                {
                    ai.agent.speed = 1;
                    guns.Fire(0);
                }
                else
                {
                    ai.agent.speed = defaultSpeed;
                }
            }
            else
            {
                ai.agent.speed = escapeSpeed;
            }
        }
        else
        {
            ai.agent.speed = defaultSpeed;
        }
    }
}

public class AIMeleeBehavior : IAIAttackBehavior
{
    MeleeWeaponController melee;
    float maxAttackDistance;

    public AIMeleeBehavior(MeleeWeaponController melee, float maxAttackDistance)
    {
        this.melee = melee;
        this.maxAttackDistance = maxAttackDistance;
    }

    public void Act(AI ai)
    {
        melee.Attack();
        ai.Chase();
        if (ai.agent.remainingDistance > maxAttackDistance)
        {
            ai.state = ai.chase;
        }
    }
}

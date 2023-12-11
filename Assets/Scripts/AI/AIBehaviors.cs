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
    float startAttackDistanceSq;

    public AIChaseBehavior(float startAttackDistance)
    {
        startAttackDistanceSq = startAttackDistance * startAttackDistance;
    }

    public void Act(AI ai)
    {
        if (ai.CanSeePlayer())
        {
            ai.Chase();
            if (ai.SquareDistanceToPlayer() < startAttackDistanceSq)
            {
                ai.state = ai.attack;
            }
        }
        else
        {
            ai.anim.SetFloat("VVelocity", 1);
            if (ai.agent.remainingDistance < 0.5f)
            {
                ai.state = ai.wait;
            }
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
        minAttackDistanceSq = minAttackDistance * minAttackDistance;
        maxAttackDistanceSq = maxAttackDistance * maxAttackDistance;
        this.defaultSpeed = defaultSpeed;
        this.escapeSpeed = escapeSpeed;
    }

    private bool CanShootPlayer(AI ai)
    {
        Vector3 origin = ai.transform.position + ai.transform.up;
        Vector3 direction = AI.player.transform.position - origin;
        Ray ray = new(origin, direction);
        if (Physics.SphereCast(ray, minAttackDistanceSq, out RaycastHit hit, Mathf.Infinity, -1, QueryTriggerInteraction.Ignore))
        {
            if (hit.collider.CompareTag("Player"))
            {
                return true;
            }
        }
        return false;
    }

    public void Act(AI ai)
    {
        if (ai.CanSeePlayer())
        {
            float sqDistance = ai.SquareDistanceToPlayer();
            if (sqDistance > minAttackDistanceSq)
            {
                if ((sqDistance < maxAttackDistanceSq))
                {
                    ai.agent.speed = 0;
                    ai.anim.SetFloat("VVelocity", 0);
                    ai.anim.SetBool("HasRightGun", true);
                    ai.anim.SetLayerWeight(3, 0.8f);
                    if (CanShootPlayer(ai)) {
                        guns.Fire(0);
                    }
                    ai.RotateToPlayer();
                }
                else
                {
                    ai.anim.SetBool("HasRightGun", false);
                    ai.anim.SetLayerWeight(3, 0.0f);
                    ai.agent.speed = defaultSpeed;
                    ai.state = ai.chase;
                }
            }
            else
            {
                ai.agent.speed = escapeSpeed;
                ai.anim.SetBool("HasRightGun", false);
                ai.anim.SetLayerWeight(3, 0.0f);
            }
        }
        else
        {
            ai.agent.speed = defaultSpeed;
            ai.state = ai.chase;
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
        ai.Chase();
        melee.Attack();
        if (ai.agent.remainingDistance > maxAttackDistance)
        {
            ai.state = ai.chase;
        }
    }
}

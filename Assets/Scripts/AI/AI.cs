using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class AI : MonoBehaviour
{
    public float distancetmp;

    public NavMeshAgent agent;
    public Animator anim;
    public GameObject player;
    public Vector3 home;
    public IAIBehavior state;
    public AIWaitBehavior wait;
    public AIChaseBehavior chase;
    public IAIAttackBehavior attack;

    public void Initialize()
    {
        player = GameObject.Find("MainCharacter");
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        home = transform.position;
        anim.SetFloat("VVelocity", 0);
        state = wait;
    }

    void FixedUpdate()
    {
        state.Act(this);
        distancetmp = MathF.Sqrt(SquareDistanceToPlayer());
    }

    private (Vector3, Vector3) DirectionToPlayer()
    {
        Vector3 origin = transform.position + transform.up;
        Vector3 direction = player.transform.position - origin;
        return (origin, direction);
    }

    // Whether the AI has a direct line of sight to the player
    public bool CanSeePlayer()
    {
        (Vector3 origin, Vector3 direction) = DirectionToPlayer();
        Ray ray = new(origin, direction);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            return hit.collider.CompareTag("Player");
        }
        return false;
    }

    public float SquareDistanceToPlayer()
    {
        Vector3 v = player.transform.position - transform.position;
        return v.sqrMagnitude;
    }

    // Chase the player
    public void Chase()
    {
        agent.SetDestination(player.transform.position);
        anim.SetFloat("VVelocity", 1);
    }

    private void GoHome()
    {
        agent.SetDestination(home);
        anim.SetFloat("VVelocity", 1);
    }

    public void Idle()
    {
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            anim.SetFloat("VVelocity", 0);
        }
        else
        {
            GoHome();
        }
    }

    public void RotateToPlayer()
    {
        (_, Vector3 direction) = DirectionToPlayer();
        Quaternion look = Quaternion.LookRotation(direction, transform.up);
        transform.rotation = look;
    }
}

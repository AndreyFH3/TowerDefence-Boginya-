using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Agent2D : MonoBehaviour
{
    [SerializeField, Range(.01f, 3f)] private float seekTime = .2f;
    [SerializeField] private LayerMask playerLayerMask;
    private Transform target;
    private NavMeshAgent agent;
    private AttackEnemy attackEnemy;
    private Animator _animator;
    private float DistanceToAttack;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;


        _animator = GetComponent<Animator>();
        attackEnemy = GetComponent<AttackEnemy>();

        InvokeRepeating(nameof(FindTarget), 0, seekTime);
    }

    void Update()
    {
        if (!agent.enabled && target != null) return;

        agent.SetDestination(target.position);
        float distance = Vector3.Distance(target.position, transform.position);
        if (distance <= DistanceToAttack)
        {
            agent.isStopped = true;
            _animator.SetTrigger("Attack");
            _animator.SetBool("IsWalking", false);
            if (target.TryGetComponent(out Health h) && h.IsDead) FindTarget();
        }
        else
        {
            _animator.SetBool("IsWalking", true);
            _animator.SetFloat("idleX", agent.velocity.x);
            _animator.SetFloat("idleY", agent.velocity.y);
            agent.isStopped = false;
        }
    }

    public void DisableAgent() => agent.enabled = false;

    public void EnableAgent() => agent.enabled = true;

    private void FindTarget()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 500f, playerLayerMask);

        if (colliders.Length <= 0)
            return;
        else
            target = colliders[0].transform;

        foreach (Collider2D collider in colliders)
        {
            float targetDistance = Vector3.Distance(transform.position, target.position);
            if (collider.transform.TryGetComponent(out Health h) && !h.IsDead && targetDistance > Vector3.Distance(transform.position, collider.transform.position) )
            {
                target = collider.transform;
                DistanceToAttack = h is TowerHealth ? 4 : 2;
            }
        }
    }
    private void OnDrawGizmos()
    {
        if(target != null)
            Gizmos.DrawRay(transform.position, target.position-transform.position);
    }
}

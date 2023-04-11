using System.Collections;
using System.Collections.Generic;
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

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;


        _animator = GetComponent<Animator>();
        attackEnemy = GetComponent<AttackEnemy>();

        InvokeRepeating(nameof(FindTarget), 0, seekTime);
    }

    // Update is called once per frame
    void Update()
    {
        if (!agent.enabled) return;

        if(target != null)
            agent.SetDestination(target.position);
        else
            FindTarget();
        float distance = Vector3.Distance(target.position, transform.position);
        if (distance <= 2f)
        {
            //тут должна быть анимация, типа ломает и ломалась башня потом по анимационному событию
            agent.isStopped = true;
            _animator.SetTrigger("Attack");
            _animator.SetBool("IsWalking", false);
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

    private void FindTarget()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 500f, playerLayerMask);

        if (colliders.Length <= 0)
            return;
        else
            target = colliders[0].transform;

        float distance = Vector3.Distance(transform.position, target.position);

        foreach (Collider2D collider in colliders)
        {
            if(target.TryGetComponent(out Health h) || distance > Vector3.Distance(transform.position, collider.transform.position) )
            {
                if (h is TowerHealth health && !health.IsBroken)
                {
                    continue;
                }
                target = collider.transform;
            }
        }
    }

}

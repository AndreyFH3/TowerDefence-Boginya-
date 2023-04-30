using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

[RequireComponent(typeof(NavMeshAgent))]
public class MainCharacter : MonoBehaviour
{
    private NavMeshAgent agent;
    private Transform target;
    private Animator _animator;
    [SerializeField, Range(.01f, 3f)] private float seekTime = .2f;
    [SerializeField] private LayerMask enemyLayerMask;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        _animator = GetComponent<Animator>();

        InvokeRepeating(nameof(FindTarget), 0, seekTime);
    }

    void Update()
    {
        if (!agent.enabled) return;

        if (target != null)
            agent.SetDestination(target.position);
        else
            FindTarget();
        float distance = Vector3.Distance(target.position, transform.position);
        if (distance <= 1.4f)
        {
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
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 500f, enemyLayerMask);

        if (colliders.Length <= 0)
            return;
        else
            target = colliders[0].transform;

        float distance = Vector3.Distance(transform.position, target.position);

        foreach (Collider2D collider in colliders)
        {
            if (target.TryGetComponent(out Health h) || distance > Vector3.Distance(transform.position, collider.transform.position))
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

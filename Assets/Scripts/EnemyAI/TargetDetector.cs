using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TargetDetector : Detector
{
    [SerializeField] private float targetrDetectionRange = 5;

    [SerializeField] private LayerMask obstacleLayerMask, playerLayerMask;

    [SerializeField] private bool showGizmos = false;

    private List<Transform> colliders;

    public override void Detect(AIData aiData)
    {
        Collider2D playerCollider = 
            Physics2D.OverlapCircle(transform.position, targetrDetectionRange, playerLayerMask);
        if(playerCollider != null)
        {
            Vector2 direction = (playerCollider.transform.position - transform.position).normalized;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, targetrDetectionRange, obstacleLayerMask);
        
            if(hit.collider != null && (playerLayerMask & (1 << hit.collider.gameObject.layer)) != 0)
            {
                Debug.DrawRay(transform.position, direction * targetrDetectionRange, Color.magenta);
                colliders = new List<Transform> { playerCollider.transform };
            }
            else
            {
                colliders = null;
            }
        }
        else
        {
            colliders = null;
        }
        aiData.targets = colliders;
    }
    private void OnDrawGizmosSelected()
    {
        if(showGizmos == false) { return; }

        Gizmos.DrawWireSphere(transform.position, targetrDetectionRange);

        if (colliders == null) return;
        foreach(var item in colliders)
        {
            Gizmos.DrawSphere(item.position, .3f);
        }
    }
}

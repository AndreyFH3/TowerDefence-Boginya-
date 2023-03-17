using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TargetDetector : Detector
{
    [SerializeField] private float targetDetectionRange = 40;

    [SerializeField] private LayerMask obstacleLayerMask, playerLayerMask;

    [SerializeField] private bool showGizmos = false;

    private List<Transform> colliders;

    public override void Detect(AIData aiData)
    {
        //Find out if player is near
        Collider2D[] playersColliders =
            Physics2D.OverlapCircleAll(transform.position, targetDetectionRange, playerLayerMask);

        Collider2D playerCollider = GetNearest(playersColliders);
        if (playerCollider != null)
        {
            aiData.currentTarget = playerCollider.transform;
            //Check if you see the player
            Vector2 direction = (playerCollider.transform.position - transform.position).normalized;
            RaycastHit2D hit =
                Physics2D.Raycast(transform.position, direction, targetDetectionRange, obstacleLayerMask);

            //Make sure that the collider we see is on the "Player" layer
            if (hit.collider != null )
            {
                Debug.DrawRay(transform.position, direction * targetDetectionRange, Color.magenta);
                colliders = new List<Transform>()
                { 
                    playerCollider.transform
                };
            }
            else
            {
                colliders = null;
            }
        }
        else
        {
            //Enemy doesn't see the player
            colliders = null;
        }
        aiData.targets = colliders;
    }
    private void OnDrawGizmosSelected()
    {
        if(showGizmos == false) { return; }

        Gizmos.DrawWireSphere(transform.position, targetDetectionRange);

        if (colliders == null) return;
        foreach(var item in colliders)
        {
            Gizmos.DrawSphere(item.position, .3f);
        }
    }

    private Collider2D GetNearest(Collider2D[] array)
    {
        Collider2D closest = null;
        float distance = float.MaxValue;
        foreach(Collider2D item in array)
        {
            float distanceToTower = Vector3.Distance(transform.position, item.transform.position);
            if (distance >= distanceToTower)
            {
                distance = distanceToTower;
                closest = item;
            }
        }
        return closest;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private List<SteeringBehaviour> steeringBehaviours;
    [SerializeField] private List<Detector> detectors;

    [SerializeField] private AIData aiData;

    [SerializeField] private float detectorDelay = 0.05f, aiUpdateDelay = 0.6f;
    [SerializeField] private ContextSolver moveDirectionSolver;
    [SerializeField] private bool follow = false;
    [SerializeField] private Vector2 moveDirection;
    [SerializeField] private float _speed = 3;

    private void Start()
    {
        InvokeRepeating(nameof(PerformDetection), 0 ,detectorDelay);
    }

    private void Update()
    {
        if (aiData.currentTarget != null)
        {
            follow = true;
            StartCoroutine(Chase());
        }
        else if (aiData.GetTargetCount() > 0)
        {
            aiData.currentTarget = aiData.targets[0];
        }
        transform.Translate(moveDirection * (Time.deltaTime * _speed));
    }

    private IEnumerator Chase()
    {
        if(aiData.currentTarget == null)
        {
            Debug.Log($"Stopping: {name}");
            moveDirection = Vector2.zero;
            follow = false;
            yield return null;
        }
        else
        {
            float distance = Vector2.Distance(aiData.currentTarget.position, transform.position);

            moveDirection = moveDirectionSolver.GetDirectionToMove(steeringBehaviours, aiData);
            yield return new WaitForSeconds(aiUpdateDelay);
            StartCoroutine(Chase());
        }
        yield return null;
    }

    private void PerformDetection()
    {
        foreach(Detector detector in detectors)
        {
            detector.Detect(aiData);
        }
    }
}

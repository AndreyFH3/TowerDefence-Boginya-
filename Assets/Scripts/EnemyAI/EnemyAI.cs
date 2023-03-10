using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    //MAKE ENEMY GREAT AGAIN  - ���������� ���...
    [SerializeField] private Bullet b;

    
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.TryGetComponent(out Health health))
        {
            Bullet bullet = Instantiate(b);
            bullet.SetDamage(1);
            bullet.SetType(GetComponent<EnemyHealth>().GetDamageType);
            CombatEngine.DamageObject(bullet, health);
            Destroy(bullet.gameObject);
            Destroy(gameObject);
        }

    }
}

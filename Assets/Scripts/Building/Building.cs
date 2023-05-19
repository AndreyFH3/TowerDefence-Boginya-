using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditorInternal.Profiling.Memory.Experimental.FileFormat;
using UnityEngine;
using UnityEngine.AI;

public class Building : MonoBehaviour
{
    [SerializeField] private SpriteRenderer renderer;
    [SerializeField] private int price;
    private Material material;
    public int Price => price;

    public bool CanSet { private set; get; }

    private void Awake()
    {
        GetComponent<PhysicalDamageTower>().enabled = false;
        CanSet = true;
        material = GetComponent<SpriteRenderer>().sharedMaterial;
    }

    public void DestroyTowerGameObject()
    {
        Destroy(gameObject);
        //дописать код для продажи башни или уничтожения
    }

    public void SetTower()
    {
        GetComponent<PhysicalDamageTower>().enabled = true;
        renderer.color = Color.white;
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        GetComponent<CircleCollider2D>().isTrigger = false;
        GetComponent<NavMeshObstacle>().enabled = true;
        if (transform.TryGetComponent(out Animator animator) && transform.TryGetComponent(out TowerHealth h))
        {
            animator.SetTrigger("Builded");
            h.IsSet = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        material.color = new Color(120, 0, 0, 120);
        CanSet = false;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        material.color = new Color(0, 120, 0, 120);
        CanSet = true;
    }

}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditorInternal.Profiling.Memory.Experimental.FileFormat;
using UnityEngine;

public class Building : MonoBehaviour
{
    [SerializeField] private SpriteRenderer renderer;
    [SerializeField] private int price;

    public int Price => price;

    public bool CanSet { private set; get; }

    private void Awake()
    {
        GetComponent<PhysicalDamageTower>().enabled = false;
        CanSet = true;
        renderer.color = new Color(255,255,255,125);
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
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        renderer.color = new Color(255,0,0,125);
        CanSet = false;
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        renderer.color = new Color(255,255,255,125);
        CanSet = true;
    }
}

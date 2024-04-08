using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class EnemyEnzo : MonoBehaviour
{

    private Rigidbody2D rb;
    [SerializeField]private float hp;
    [SerializeField] private float EnemySpeed;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();   
    }

    // Update is called once per frame
    void Update()
    {
        if(hp <= 0)
            Destroy(gameObject);
    }

    public void TakeDamage(float damage)
    {
        hp -= damage;
    }

    public void EnemyShoot()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

    }
}

using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    private Rigidbody2D rb;
    [SerializeField]private float hp;
    [SerializeField] private float EnemySpeed;
    [SerializeField] private GameObject firePoint;
    [SerializeField] private GameObject shuriken;
    private GameObject currentShuriken;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(ShootTime());
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
        if (currentShuriken == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            float angle = Mathf.Atan2(player.transform.position.y - transform.position.y, player.transform.position.x - transform.position.x) * Mathf.Rad2Deg - 90f;
            firePoint.transform.localRotation = Quaternion.Euler(0, 0, angle);

            currentShuriken = Instantiate(shuriken, firePoint.transform.position, firePoint.transform.rotation);
        }
    }

    public IEnumerator ShootTime()
    {
        yield return new WaitForSeconds(2f);
        EnemyShoot();
        StartCoroutine(ShootTime());
    }

    public IEnumerator Invicibility()
    {
        yield return new WaitForSeconds(1);
        
    }

}

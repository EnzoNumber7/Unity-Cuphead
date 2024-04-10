using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum Direction
{
    None,
    Left,
    Right
}
public class PlayerMovementEnzo : MonoBehaviour
{

    [SerializeField] private float kunaiRadius;
    [SerializeField] private float rangeRadius;
    [SerializeField] private float stopPower;

    [SerializeField] private GameObject Grab;
    [SerializeField] private GameObject firePoint;

    //kunai
    [SerializeField] private GameObject Kunai;
    private bool isUsed;

    private Vector2 mousePos;

    [SerializeField] GameObject currentKunai;

    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    // Update is called once per frame
    void Update()
    {
        if (PauseMenu.isPaused == false)
        {
            Vector2 currentVelocity = new Vector2(0, rb.velocity.y);

            if (currentKunai == null)
            {
                mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                firePointPos();
                isUsed = false;
            }

            if (Input.GetMouseButtonDown(0) && isUsed == false)
            {
                Shoot();
                isUsed = true;
            }
            if (Input.GetMouseButtonDown(1) && isUsed == true)
            {
                currentKunai.GetComponent<Kunai>().ReturnToPlayer(transform.position);
            }
            if (currentKunai != null)
            {
                if (Input.GetKeyDown(KeyCode.Q) && currentKunai.GetComponent<Kunai>().isAttached)
                {
                    currentKunai.GetComponent<Kunai>().Detach(transform.position);
                }
            }
        }
    }

    private void Shoot()
    {
        float angle = Mathf.Atan2(mousePos.y - firePoint.transform.position.y, mousePos.x - firePoint.transform.position.x) * Mathf.Rad2Deg - 90f;

        firePoint.transform.localRotation = Quaternion.Euler(0, 0, angle);
        currentKunai = Instantiate(Kunai, firePoint.transform.position, firePoint.transform.rotation);
        Grab.GetComponent<Grab>().GetCurrentKunai(currentKunai);
        currentKunai.GetComponent<Kunai>().destiny = mousePos;

    }

    private void firePointPos()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float angle = Mathf.Atan2(mousePos.y - firePoint.transform.position.y, mousePos.x - firePoint.transform.position.x) * Mathf.Rad2Deg - 90f;

        firePoint.transform.localRotation = Quaternion.Euler(0, 0, angle);
        Vector2 firePointPos = firePoint.transform.position;
        Vector2 playerPos = transform.position;
        Vector2 direction = (mousePos - firePointPos).normalized;

        firePoint.transform.position = playerPos + direction * kunaiRadius;


    }

    private void CheckRange()
    {
        if (currentKunai == null)
            return;

        Kunai scriptKunai = currentKunai.GetComponent<Kunai>();
        Vector2 playerPos = transform.position;
        Vector2 KunaiPos = currentKunai.transform.position;
        Vector2 direction = (playerPos - KunaiPos).normalized;


        float distance = Vector2.Distance(KunaiPos, playerPos);
        if (distance > rangeRadius && scriptKunai.attachable == true && scriptKunai.isAttached == false)
        {
            Rigidbody2D KunaiRb = currentKunai.GetComponent<Rigidbody2D>();
            KunaiRb.AddForce(direction * stopPower, ForceMode2D.Impulse);
            KunaiRb.bodyType = RigidbodyType2D.Dynamic;
            scriptKunai.attachable = false;
            scriptKunai.fallen = true;
        }
        if (distance > rangeRadius + 1.15f && scriptKunai.fallen == true && scriptKunai.isAttached == false)
        {
            Rigidbody2D KunaiRb = currentKunai.GetComponent<Rigidbody2D>();
            KunaiRb.AddForce((playerPos - direction) * 0.5f, ForceMode2D.Impulse);
        }
        if (distance > rangeRadius + 1.15f && scriptKunai.isAttached == true)
        {
            rb.AddForce((KunaiPos - playerPos) * 0.5f);
        }
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, rangeRadius + 1.5f);
        Gizmos.DrawWireSphere(transform.position, rangeRadius + 1.15f);
    }
}

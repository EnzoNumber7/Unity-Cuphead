using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]private float speed = 5f;
    [SerializeField] private float JumpPower;
    private Rigidbody2D rb;

    [SerializeField] private float kunaiRadius;
    [SerializeField] private float rangeRadius;
    [SerializeField] private float stopPower;
    [SerializeField] private float ropeStretch;

    [SerializeField] private GameObject Grab;
    [SerializeField] private GameObject feet;
    [SerializeField] private GameObject firePoint;

    //kunai
    [SerializeField] private GameObject Kunai;
    private bool isUsed;

    private Vector2 mousePos;

    [SerializeField] GameObject currentKunai;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 currentVelocity = new Vector2(0,rb.velocity.y);
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        firePointPos();
        CheckRange();

        if (Input.GetKey(KeyCode.A))
        {
            currentVelocity.x -= speed;
        }
        if (Input.GetKey(KeyCode.D))
        {
            currentVelocity.x += speed;
        }
        if (Input.GetMouseButtonDown(0) && isUsed == false)
        {
            Shoot();
            isUsed = true;
        }
        if (Input.GetMouseButton(1) && isUsed == true)
        {
            currentKunai.GetComponent<Kunai>().ReturnToPlayer(transform.position);
        }
        if (Input.GetKeyDown(KeyCode.Space) && feet.GetComponent<Player_Feet>().isGrounded) 
        {
            rb.AddForce(new Vector2(0,JumpPower));
        }
        if(currentKunai != null)
        {
            if (Input.GetKeyDown(KeyCode.Q) && currentKunai.GetComponent<Kunai>().isAttached)
            {
                currentKunai.GetComponent<Kunai>().Detach(transform.position);
            }
            if (Input.GetKey(KeyCode.E) && currentKunai.GetComponent<Kunai>().isAttached)
            {
                transform.position = Vector2.MoveTowards(transform.position,currentKunai.transform.position,0.03f);
            }
        }
        
        

        if (currentKunai == null)
        {
            isUsed = false;
        }
        rb.velocity = currentVelocity;
    }

    private void Shoot()
    {
        float angle = Mathf.Atan2(mousePos.y - firePoint.transform.position.y, mousePos.x - firePoint.transform.position.x) * Mathf.Rad2Deg - 90f;

        firePoint.transform.localRotation = Quaternion.Euler(0, 0, angle);

        currentKunai = Instantiate(Kunai, firePoint.transform.position, firePoint.transform.rotation);

        Grab.GetComponent<Grab>().GetCurrentKunai(currentKunai);
        
    }

    private void firePointPos()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

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
        if(distance > rangeRadius && scriptKunai.attachable == true && scriptKunai.isAttached == false) 
        {
            Rigidbody2D KunaiRb = currentKunai.GetComponent<Rigidbody2D>();
            KunaiRb.AddForce(direction * stopPower,ForceMode2D.Impulse);
            KunaiRb.bodyType = RigidbodyType2D.Dynamic;
            scriptKunai.attachable = false;
            scriptKunai.fallen = true;
        }
        if(distance > rangeRadius + 1.15f && scriptKunai.fallen == true && scriptKunai.isAttached == false)
        {
            Rigidbody2D KunaiRb = currentKunai.GetComponent<Rigidbody2D>();
            KunaiRb.AddForce((playerPos - direction) * ropeStretch, ForceMode2D.Impulse);
        }
        if (distance > rangeRadius + 1.15f && scriptKunai.isAttached == true)
        {
            rb.AddForce((KunaiPos - playerPos) * 0.5f);
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position,rangeRadius + 1.15f);
    }

}

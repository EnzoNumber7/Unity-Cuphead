using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]private float speed = 5f;
    [SerializeField] private float JumpPower;
    private Rigidbody2D rb;


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
        Vector2 distance = firePointPos - playerPos;

        Vector2 direction = firePointPos - mousePos;
        
    }

}

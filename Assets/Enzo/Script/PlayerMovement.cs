using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementEnzo : MonoBehaviour
{
    [SerializeField]private float speed = 5f;
    [SerializeField] private float JumpPower;
    private Rigidbody2D rb;

    [SerializeField] private GameObject feet;

    //kunai
    [SerializeField] private GameObject Kunai;
    [SerializeField] private Transform FirePoint;
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
        if(Input.GetKey(KeyCode.A))
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
            currentKunai.GetComponent<KunaiEnzo>().ReturnToPlayer(transform.position);
        }
        if (Input.GetKeyDown(KeyCode.Space) && feet.GetComponent<Player_FeetEnzo>().isGrounded) 
        {
            rb.AddForce(new Vector2(0,JumpPower));
        }
        if(currentKunai != null)
        {
            if (Input.GetKeyDown(KeyCode.Q) && currentKunai.GetComponent<KunaiEnzo>().isAttached)
            {
                currentKunai.GetComponent<KunaiEnzo>().Detach(transform.position);
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
        float angle = Mathf.Atan2(mousePos.y - FirePoint.position.y, mousePos.x - FirePoint.position.x) * Mathf.Rad2Deg - 90f;
        FirePoint.localRotation = Quaternion.Euler(0, 0, angle);
        currentKunai = Instantiate(Kunai, FirePoint.position,FirePoint.rotation);
    }
}

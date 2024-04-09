using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction
{
    None,
    Left,
    Right
}
public class PlayerMovementEnzo : MonoBehaviour
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
    [SerializeField] private GameObject ropeObject;

    private Direction blockedDirection;

    //kunai
    [SerializeField] private GameObject Kunai;
    private bool isUsed;

    private Vector2 mousePos;
    private Vector2 firePos = new Vector2(0, 0);

    [SerializeField] GameObject currentKunai;

    // Start is called before the first frame update
    void Start()
    {
        blockedDirection = Direction.None;
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
            }

            if (Input.GetKey(KeyCode.A) && blockedDirection != Direction.Left)
            {
                currentVelocity.x -= speed;
            }
            if (Input.GetKey(KeyCode.D) && blockedDirection != Direction.Right)
            {
                currentVelocity.x += speed;
            }
            if (Input.GetMouseButtonDown(0) && isUsed == false)
            {
                Shoot();
                isUsed = true;
            }
            if (Input.GetMouseButtonDown(1) && isUsed == true)
            {
                if (Vector2.Distance(ropeObject.GetComponent<Rope>().topObject.transform.position, ropeObject.GetComponent<Rope>().anchor.transform.position) < 0.01)
                {
                    ropeObject.GetComponent<Rope>().RemoveElement();
                }

                //currentKunai.GetComponent<Kunai>().ReturnToPlayer(transform.position);
            }
            if (Input.GetKeyDown(KeyCode.Space) && feet.GetComponent<Player_Feet>().isGrounded)
            {
                rb.AddForce(new Vector2(0, JumpPower));
            }
            if (currentKunai != null)
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
            //CheckRange();
        }
    }

    private void Shoot()
    {
        float angle = Mathf.Atan2(mousePos.y - firePoint.transform.position.y, mousePos.x - firePoint.transform.position.x) * Mathf.Rad2Deg - 90f;

        firePoint.transform.localRotation = Quaternion.Euler(0, 0, angle);

        currentKunai = Instantiate(Kunai);

        Rope rope = ropeObject.GetComponent<Rope>();
        rope.transform.position = firePoint.transform.position;
        rope.kunai = currentKunai;
        rope.GenerateRope();
        firePos = firePoint.transform.position;
        currentKunai.transform.rotation = firePoint.transform.rotation;

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

        if (currentKunai.GetComponent<Kunai>().isAttached == false)
        {
            return;
        }

        Rope rope = ropeObject.GetComponent<Rope>();
        Vector2 ropeOriginPos = rope.anchor.transform.position;
        Vector2 playerPos = transform.position;
        Vector2 kunaiPos = currentKunai.transform.position;
        Vector2 direction = (playerPos - kunaiPos).normalized;

        if (rope.GetDistanceFromAnchor() > rope.GetRopeMaxPossibleLenght())
        {
            if (direction.x < 0.0f)
            {
                blockedDirection = Direction.Left;
            }
            else
            {
                 blockedDirection = Direction.Right;
            }
            if (direction.y > 0.0f)
            {
                rb.AddForce(new Vector2(0, -10));
            }
            if (direction.y < 0.0f)
            {
                rb.velocity = new Vector2(rb.velocity.x, 0);
                rb.gravityScale = 0.0f;
            }
            
        }
        else
        {
            blockedDirection = Direction.None;
            rb.gravityScale = 1.0f;
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position,rangeRadius + 1.5f);
    }
}

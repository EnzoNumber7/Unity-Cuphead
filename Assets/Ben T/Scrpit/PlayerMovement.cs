using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]private float speed = 5f;
    private Rigidbody2D rb;
    private float x;
    private float y;

    //kunai
    [SerializeField] private GameObject Kunai;
    [SerializeField] private Transform FirePoint;

    private Vector2 mousePos;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        x = Input.GetAxisRaw("Horizontal");
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if(Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(x,y).normalized * speed;
    }

    private void Shoot()
    {
        float angle = Mathf.Atan2(mousePos.y - FirePoint.position.y, mousePos.x - FirePoint.position.x) * Mathf.Rad2Deg - 90f;
        FirePoint.localRotation = Quaternion.Euler(0, 0, angle);
        Instantiate(Kunai, FirePoint.position,FirePoint.rotation);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KunaiVisu : MonoBehaviour
{

    public GameObject firePoint;
    public float kunaiRadius;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        if(player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
    }

    // Update is called once per frame
    void Update()
    {
        firePointPos();
        transform.position = firePoint.transform.position;
    }

    private void firePointPos()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float angle = Mathf.Atan2(mousePos.y - (firePoint.transform.position.y), mousePos.x - (firePoint.transform.position.x)) * Mathf.Rad2Deg - 90f;

        transform.localRotation = Quaternion.Euler(0, 0, angle);
        Vector2 firePointPos = firePoint.transform.position;
        Vector2 playerPos = player.transform.position;
        Vector2 direction = (mousePos - firePointPos).normalized;
        firePoint.transform.position = playerPos + direction * kunaiRadius;

    }
}

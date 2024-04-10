using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPlayer : MonoBehaviour
{
    [SerializeField] private float hp;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float damage, GameObject entity)
    {
        hp -= damage;
        KnockBack(entity);
    }

    public void KnockBack(GameObject entity)
    {
        Vector2 entityPos = entity.transform.position;
        if (entityPos.x < transform.position.x)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(-100,2);
        }
        else if (entityPos.x > transform.position.x)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(100, 2);
        }
    }
}

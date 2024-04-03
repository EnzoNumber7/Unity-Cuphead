using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{

    public float _speed = 10f;
    public Rigidbody2D _rb;
    public float _jumpAmount = 10f;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.D) == true)
        {
            gameObject.GetComponent<Transform>().Translate(new Vector2(10 * _speed * Time.deltaTime, 0));
        }
        if(Input.GetKey(KeyCode.A) == true)
        {
            gameObject.GetComponent<Transform>().Translate(new Vector2(-10 * _speed * Time.deltaTime, 0));
        }
        if(Input.GetKey(KeyCode.Escape) == true)
        {
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KunaiVisu : MonoBehaviour
{

    public GameObject firePoint;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = firePoint.transform.position;
    }
}

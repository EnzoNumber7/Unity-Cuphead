using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Parralax : MonoBehaviour
{
    private float _startingPos;
    private float _lengthOfSprite;
    public float AmountOfParallax;
    public Camera MainCamera;

    void Start()
    {
        _startingPos = transform.position.x;
        _lengthOfSprite = GetComponent<Tilemap>().size.x;
    }
    void Update()
    {
        Vector2 Position = MainCamera.transform.position;
        float Temp = Position.x * (1 - AmountOfParallax);
        float Distance = Position.x * AmountOfParallax;

        Vector2 NewPosition = new Vector2(_startingPos + Distance, transform.position.y);

        transform.position = NewPosition;
    }
}

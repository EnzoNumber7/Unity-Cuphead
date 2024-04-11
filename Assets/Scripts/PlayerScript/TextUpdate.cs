using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextUpdate : MonoBehaviour
{
    public Player player;
    public void Update()
    {
        gameObject.GetComponent<Text>().text = player.coins.ToString();
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextUpdate : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    public void UpdateText()
    {
        
        //text.text = gameObject.GetComponent<Player>().coins.ToString();
            text.text = "kaka";
    }
}

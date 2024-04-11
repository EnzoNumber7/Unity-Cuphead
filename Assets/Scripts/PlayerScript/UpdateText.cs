using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpdateText : MonoBehaviour
{
    public TextMeshProUGUI textObj;

    private void Awake()
    {
        if(textObj == null) { print("C miteux"); }
    }

    public void Update()
    {
        
    }

    public void ChangeText()
    {
        textObj.text = gameObject.GetComponent<Player>().coins.ToString();
        print(textObj.text);
    }

}

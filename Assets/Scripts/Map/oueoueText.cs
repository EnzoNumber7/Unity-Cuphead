using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.UI;

public class OueoueText : MonoBehaviour
{
    public Text obj;

    public void Awake()
    {
        PrintZob();
    }

    public void PrintZob()
    {
        if(obj == null) { print("C NULL"); }
        else { print(obj.text); }
    }

}

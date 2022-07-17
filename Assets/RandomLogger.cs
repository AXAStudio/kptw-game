using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RandomLogger : MonoBehaviour
{

    public TextMeshPro_UGUI LoggerText;

    public string[] messages;

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Log(string message){
        Instanitate(LoggerText.gameObject)
    }
}

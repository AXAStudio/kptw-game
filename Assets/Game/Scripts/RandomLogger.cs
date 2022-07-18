using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RandomLogger : MonoBehaviour
{

    public TextMeshProUGUI LoggerText;

    public string[] messages;

    public float interval = 1f;

    // Update is called once per frame
    void Start()
    {
        StartCoroutine(printRandomMessages(interval));
    }



    IEnumerator printRandomMessages(float timeBetweenMessages){
        foreach (string message in messages)
        {
            Log(message);
            yield return new WaitForSeconds(timeBetweenMessages);
        }
    }
    

    void Log(string message){
        GameObject logObject = Instantiate(LoggerText.gameObject);
        logObject.GetComponent<TextMeshProUGUI>().text = message;
        Destroy(logObject,10);
        
    }
}

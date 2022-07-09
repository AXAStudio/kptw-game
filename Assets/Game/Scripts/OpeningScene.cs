using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpeningScene : MonoBehaviour
{
    public GameObject canvas1, canvas2;
    public float titleTime = 5f;

    IEnumerator Open()
    {
        canvas1.SetActive(true);
        yield return new WaitForSeconds(titleTime);
        canvas1.SetActive(false);
        canvas2.SetActive(true);
    }

    void Start()
    {
        canvas2.SetActive(false);
        StartCoroutine(Open());
    }
}

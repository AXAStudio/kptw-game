using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseGun : MonoBehaviour
{

    public GameObject[] guns;
    int i;

    void Awake()
    {
        transform.GetChild(PlayerPrefs.GetInt("Gun Selected")).gameObject.SetActive(true);
    }
}

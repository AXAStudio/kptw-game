using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Surround : MonoBehaviour
{
    void Update(){
        transform.Rotate(Random.Range(0,360),Random.Range(0,360),0);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slide : MonoBehaviour
{
    Rigidbody rig;
    CapsuleCollider collider;

    float originalHeight;
    public float reducedHeight;

    bool slided = false;

    public float slideSpeed = 10f;
    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<CapsuleCollider>();
        rig = GetComponent<Rigidbody>();
        originalHeight = collider.height;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl) && Input.GetKey(KeyCode.W)){
            Sliding();
            slided = true;
        }
        else if (Input.GetKeyUp(KeyCode.LeftControl)){
            slided = false;
            GoUp();
        }

        

    }

    private void Sliding(){
        collider.height = reducedHeight;
        rig.AddForce(transform.forward * slideSpeed, ForceMode.Impulse);
    }

    private void GoUp(){
        collider.height = originalHeight;
    }
}

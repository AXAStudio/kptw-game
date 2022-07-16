using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Selecter : MonoBehaviour
{

    public GameObject[] weapons;
    public int i = 0;

    void Update()
    {
        
    }

    void Start() {
        UpdateGS();
    }

    public void Right(){

        if (i == weapons.Length - 1)
            {
                i = 0;
            }
        else
            {
                i++;
            }

        UpdateGS();

    }

    public void Left(){

        if (i == 0)
        {
            i = weapons.Length - 1;
        }

        else{
            i--;
        }

        UpdateGS();

    }

    void UpdateGS(){

        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        GameObject gs = Instantiate(weapons[i], 
        transform.position, Quaternion.identity);
        gs.transform.localScale = new Vector3(1000,1000,1000);
        gs.transform.parent = gameObject.transform;

    }
}
 
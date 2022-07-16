using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Selecter : MonoBehaviour
{

    public Transform[] weapons;
    public int i = 0;

    void Update()
    {
        foreach ( Transform item in weapons)
        {
            if (item.GetSiblingIndex() == i){
                item.gameObject.SetActive(true);
            }

            else if (item.GetSiblingIndex() != i) {
                item.gameObject.SetActive(false);
            }
        }
    }

    public void Right(){

        if (i == transform.childCount - 1)
            {
                i = 0;
            }
        else
            {
                i++;
            }



    }

        public void Left(){

        if (i == 0)
        {
            i = transform.childCount - 1;
        }

        else{
            i--;
        }

    }
}

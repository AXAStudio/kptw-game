using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Selecter : MonoBehaviour
{

    public Transform[] weapons;
    int i = 0;

    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        foreach ( Transform item in weapons)
        {
            if (transform.GetSiblingIndex() == i){
                item.gameObject.SetActive(true);
            }

            else{
                item.gameObject.SetActive(false);
            }
        }
    }

    public void Right(){

        if (i == transform.childCount)
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
            i = transform.childCount;
        }

        else{
            i--;
        }

    }
}

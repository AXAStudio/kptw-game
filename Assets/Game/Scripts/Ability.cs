using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : MonoBehaviour
{
    public GameObject[] abils;
    public KeyCode[] triggers;

    private bool abilCooldown;

    void Update() {
        if (Input.GetKeyDown(triggers[0]) && abilCooldown == false){
            StartCoroutine(RunAblity(abils[0],5));
        }
        
        if (Input.GetKeyDown(triggers[1]) && abilCooldown == false){
            StartCoroutine(RunAblity(abils[1],1));
        }
        
        if (Input.GetKeyDown(triggers[2]) && abilCooldown == false){
            StartCoroutine(RunAblity(abils[2],8));
        }
    }

    IEnumerator RunAblity(GameObject Ability, float time){
        
        abilCooldown = true;
        GameObject go = Instantiate(Ability,new Vector3(transform.position.x,transform.position.y,transform.position.z),Quaternion.Euler(-90,0,0));
        yield return new WaitForSeconds(time);
        Destroy(go);
        abilCooldown = false;
    }
}

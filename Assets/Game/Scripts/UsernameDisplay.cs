using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class UsernameDisplay : MonoBehaviour
{
    [SerializeField] PhotonView playerPV;
    [SerializeField] TMP_Text text;
    [SerializeField] GameObject head;

    void Start()
    {
        if (playerPV.IsMine)
        {
            gameObject.SetActive(false);
            head.SetActive(false);
        }
        text.text = playerPV.Owner.NickName;
    }
}

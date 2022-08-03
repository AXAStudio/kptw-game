using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Voice.Unity;


namespace Photon.Voice.Pun
{
    public class VoiceOnoff : MonoBehaviour
    {   
        public Recorder recorder;
        public GameObject redCross;
        

        void Start(){
            recorder = GameObject.Find("VoiceManager").GetComponent<Recorder>();
        }

        public void Switch(){
            recorder.TransmitEnabled = !recorder.TransmitEnabled;
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.M)){
                recorder.TransmitEnabled = !recorder.TransmitEnabled;
            }

            redCross.SetActive(!recorder.TransmitEnabled);
        }


        
    }
}


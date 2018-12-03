using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PunLauncher : MonoBehaviour {

    void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

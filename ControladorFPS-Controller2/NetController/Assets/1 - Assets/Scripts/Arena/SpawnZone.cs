//                                          ▂ ▃ ▅ ▆ █ ZEN █ ▆ ▅ ▃ ▂ 
//                                        ..........<(+_+)>...........
// SpawnZone.cs (09/12/2018)
//Autor: Alejandro Rivas                 alejandrotejemundos@hotmail.es
//Desc: ZoneSpawn
//Mod : 
//Rev :ini
//..............................................................................................\\
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class SpawnZone : MonoBehaviour {

	
	void Start () {
		
	}
	
	
	void Update () {
		
	}
    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
        
        }
    }
   
}


//                                          ▂ ▃ ▅ ▆ █ ZEN █ ▆ ▅ ▃ ▂ 
//                                        ..........<(+_+)>...........
// .cs (//)
//Autor: Alejandro Rivas                 alejandrotejemundos@hotmail.es
//Desc:
//Mod : 
//Rev :
//..............................................................................................\\
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Photon.Pun;
public class PhotonPlayer : MonoBehaviour {

    public PhotonView PV;
    public GameObject MyAvatar;
    public int MyTeam;
	
	void Start () {
        PV = GetComponent<PhotonView>();
        if(PV.IsMine){
            PV.RPC("RPC_GetTeam", RpcTarget.MasterClient);
        }
   
	}
	
	
	void Update () {
		
	}
     
    [PunRPC]
    void Rpc_GetTeam()
    {
        MyTeam = PhotonManager.instance.nextPlayersTeam;
        PhotonManager.instance.UpdateTeam();
        PV.RPC("RPC_SentTeam",RpcTarget.OthersBuffered,MyTeam);
    }
    [PunRPC]
    void RPC_SentTeam(int wichTeam)
    {
        MyTeam = wichTeam;
    }
}

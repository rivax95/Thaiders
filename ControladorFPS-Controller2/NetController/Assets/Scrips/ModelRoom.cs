//                                          ▂ ▃ ▅ ▆ █ ZEN █ ▆ ▅ ▃ ▂ 
//                                        ..........<(+_+)>...........
// ModelRoom.cs (12/08/2018)
//Autor: Alejandro Rivas                 alejandrotejemundos@hotmail.es
//Desc: Controller del controlador de room, aplicado bajo el patron MVC.
//Mod : 
//Rev : inicial
//..............................................................................................\\
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModelRoom : MonoBehaviour
{
    // Singleton
    public static ModelRoom Instance;

    public InputField nick;


    void Awake()
    {
        Instance = this;
    }
    public void OnJoinOrCreateRoom()
    {
        // PhotonNetwork.playerName = nick.text;
    }

 
}
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

public class MenuFriendList : MonoBehaviour {
    public static MenuFriendList istance;
    public RectTransform Trans;
    public GameObject VerPerfil;
    public GameObject Chat;
    public GameObject Invitar;
    public GameObject Eliminar;
    public GameObject Aceptar;
    public GameObject MenuObj;

    public void Start(){
        istance = this;
    }
  

    public void Abrir(bool IsFriend) {
        MenuObj.SetActive(true);
        if (IsFriend)
        {
            Aceptar.gameObject.SetActive(false);
            Invitar.gameObject.SetActive(true);
        }
        else
        {
            Aceptar.gameObject.SetActive(true);
            Invitar.gameObject.SetActive(false);
        }
    }
    public void cerrar()
    {
        MenuObj.SetActive(false);
    }
}

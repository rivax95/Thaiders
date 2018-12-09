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

public class WindowID : MonoBehaviour {

    public int ID;
    public GameObject Ventana;
    public bool abierto;
    public void Inicializate()
    {
        Ventana = this.gameObject;
    }
    public void abrir()
    {
        if (abierto)
        {
            cerrar();
            abierto = false;
            Debug.Log("cerrado");
        }
        else 
        {
            abierto = true;
            Ventana.SetActive(true);
            Debug.Log("abierto");
        }
    }
    public void cerrar() { Ventana.SetActive(false); }
}

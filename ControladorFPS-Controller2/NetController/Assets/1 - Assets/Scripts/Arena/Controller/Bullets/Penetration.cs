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

public class Penetration : MonoBehaviour
{

    public float value;
    public enum Material { Cemento, Metal, Madera, Cristal, Suelo,Player }
    public Material TipoMaterial;
    private void Start()
    {
        switch(TipoMaterial){ 
            case Material.Player:
                    value =2;
                    break;
                
            case Material.Cemento:
                value = 3;
                break;
            case Material.Madera:
                value = 1;
                break;
            case Material.Metal:
                value = 4;
                break;
            case Material.Suelo:
                value = 8;
                break;
            case Material.Cristal:
                value = 0.5f;
                break;
    }

}

}
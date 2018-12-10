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
using UnityEngine.SceneManagement;
[CreateAssetMenu(fileName = "Partida", menuName = "Partida/rule", order = 1)]

public class Rules : ScriptableObject
{
    public enum TipoPartida
    {
        DeathMath
    }
    public string NamePartida;
    [SerializeField]
    public TipoPartida tipe;

}


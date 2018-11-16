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

public class Lista : MonoBehaviour {

    public List<int> jaja;
	void Start () {
		
	}
	
	
	void Update () {
        if (Input.GetKeyDown(KeyCode.P))
        {
            jaja.Add(Random.Range(0, 20));
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            jaja.RemoveAt(jaja.Count - 1);
        }
	}
}

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

public class Escopeta : ArmaDeTubo
{
    public void OnPump()
    {
        audiosource.PlayOneShot(boltSound);
    }
    public override void OnBoltForwarded()
    {
        //silencio
        Invoke("resetReloading", 0.5f);
    }

}

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

public abstract class HabilityBase : MonoBehaviour {

    bool locked;
    float recuperation;

    public virtual void CheckHability()
    {

    }
    public virtual void OnFinishHability()
    {

    }
    public virtual void OnStartHability()
    {

    }
    public virtual void OnStayHability()
    {

    }
    public IEnumerator Recuperation()
    {
        OnStartHability();
        locked = false;
        yield return new WaitForSeconds(recuperation);
        locked = true;
        OnFinishHability();
    }
}

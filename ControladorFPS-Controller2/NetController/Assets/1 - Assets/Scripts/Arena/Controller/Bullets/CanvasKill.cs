using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasKill : MonoBehaviour {
    GameObject Log;
    GameObject Result;
public void CreateKill(Transform parent)
    {
        Log = Resources.Load("KillLog") as GameObject;
       GameObject Loged= Instantiate(Log, null);
        Loged.transform.SetParent(parent);
        Result = Loged;
        Destroy(Loged, 2.2f);
    }
    public void SetInfo(string Nombre,string Nombre1,Sprite Gun,Sprite Site,bool WaLLbANG)
    {
        if (Result == null)
        {
            Debug.LogWarning("No ahi creado un CanvasKill");
            return;
        }

        Log.transform.Find("Nombre0").GetComponent<Text>().text = Nombre;
        Log.transform.Find("Nombre1").GetComponent<Text>().text = Nombre1;
        Log.transform.Find("Gun").GetComponent<Image>().sprite = Gun;
        Log.transform.Find("Site").GetComponent<Image>().sprite = Site;
        Log.transform.Find("WallBang").GetComponent<Image>().sprite = Site;
        if (WaLLbANG) { 
        Log.transform.Find("WallBang").GetComponent<Image>().color = Color.white;
        }
        else
        {
            Log.transform.Find("WallBang").GetComponent<Image>().color = Color.red;
        }
    }
}

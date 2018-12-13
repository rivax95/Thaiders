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

public class DataLoader : MonoBehaviour {

  public  WWW LoadData;
  public WWWForm form;
    public string url;
    public string Data;
    public string[] Userdat;
    public string nick;
    public string Password;
	void Start () {
        //StartCoroutine(GetData());
	}
	
	
	void Update () {
		
	}
    IEnumerator GetData()
    {
        form.AddField("Nick", nick);
        form.AddField("Password", Password);
        LoadData = new WWW(url,form);
        yield return LoadData;
        Data = LoadData.text;
        Userdat = Data.Split(';');
        Debug.Log(Data);
    }
}

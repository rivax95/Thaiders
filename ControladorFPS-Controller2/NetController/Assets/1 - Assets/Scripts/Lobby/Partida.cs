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
using UnityEngine.UI;
public class Partida : MonoBehaviour {

    public  Rules Rule;
    public bool Ini;
    public float restTime=5;
    [HideInInspector]
    public float calculateTime;
    [HideInInspector]
  public   Image img;
     Text time;
    public void Start()
    {
        calculateTime = restTime;
        img = transform.Find("Fondo").GetChild(0).GetComponent<Image>();
        time = transform.Find("Fondo").GetChild(0).GetChild(0).GetComponent<Text>();
    }
    public void Update()
    {
        if (Ini)
        {
         
            img.gameObject.SetActive(true);
            calculateTime -= Time.fixedDeltaTime;
            time.text = Mathf.CeilToInt(calculateTime).ToString();
            if (calculateTime <= 0)
            {
                //TODOO LOAD RULE2S
                Ini = false;
                PhotonManager.instance.EstablecerRoomOptions(10);
                PhotonManager.instance.MyLoadRoom(Rule);
            }
          
        }

    }
    public void Enable()
    {
        if (Ini == true)
        {
            WindowsManager.istance.ResetButtonParitda();
        }
        else
        {
            WindowsManager.istance.ResetButtonParitda();
            Ini = true;
        }
        
    }
}

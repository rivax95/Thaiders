//                                          ▂ ▃ ▅ ▆ █ ZEN █ ▆ ▅ ▃ ▂ 
//                                        ..........<(+_+)>...........
// LogginDisplay.cs (21/08/18)
//Autor: Alejandro Rivas                 alejandrotejemundos@hotmail.es
//Desc: Display, controla la interfaz del loggin. SOLO PP(playerprefs)
//Mod : 
//Rev :Ini
//..............................................................................................\\
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LogginDisplay : MonoBehaviour {

    public Toggle recordar;
    public int recordarCheck;
    public bool conecxion;
    string nameAcc;
    public Text textoiNPUT;
    public InputField username;
	void Start () {
        getPrefs();

	}

    void Update()
    {

    }
    public void Connectar()
    {
        if (recordar.isOn)
        {
             PlayerPrefs.SetString("NameAcc", textoiNPUT.text);
             Debug.Log(textoiNPUT.text);
        }
        if (conecxion)
        {
            SceneManager.LoadScene("MainLobby");
        }
    }
    public void getPrefs(){
        recordarCheck = PlayerPrefs.GetInt("Recordar", 0);
        if (recordarCheck == 0)
        {
            recordar.isOn = false;
        }
        else
        {
            recordar.isOn = true;
            nameAcc = PlayerPrefs.GetString("NameAcc", "");
            username.text = nameAcc;
        }
       
        //Debug.Log("Set es: "+nameAcc);
    }
    public void RecordarChecking(bool ison)
    {
        if (!ison)
        {
            PlayerPrefs.SetInt("Recordar", 0);
        }
        else
        {
            PlayerPrefs.SetInt("Recordar", 1);
        }
    }
	
}

//                                          ▂ ▃ ▅ ▆ █ Thaiders █ ▆ ▅ ▃ ▂ 
//                                        ..........<(+_+)>...........
// WindowsManager.cs (06/12/2018)
//Autor: Alejandro Rivas                 alejandrotejemundos@hotmail.es
//Desc: Gestiona la apertura de ventanas en Esta escena (Lobby)
//Mod : 
//Rev :Ini
//..............................................................................................\\
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
public class WindowsManager : MonoBehaviour {
    public List<WindowID> MyWindows;
    public WindowID LobbyBlock;
    public bool BlockLobbyWindows=false;
    public Animator animConection;
    public GameObject friendListGui;
    public GameObject chatGui;
    public GameObject Barra;
    public int idMatch;
    public Text cantPlayers;
    public Text Log;
    public static WindowsManager istance;
    public Button vs5;
    public Button vs2;
    public List<Partida> BotonesPartida;
    
    void Awake()
    {
        if (istance == null)
        {
            istance = this;
        }
        foreach (var item in MyWindows)
        {
            item.Inicializate();
        }
        vs5.onClick.AddListener(()=>PhotonManager.instance.EstablecerRoomOptions(10));
        vs2.onClick.AddListener(() => PhotonManager.instance.EstablecerRoomOptions(4));
    }
    void Start()
    {
        CloseWindows();
    }
    void Update()
    {
        //cantPlayers.text = PhotonManager.instance.totalPlayers.ToString() + " Players Only";
        //Log.text = PhotonManager.instance.log;
    }
    public void OpenWindow(WindowID ID)
    {
        if (BlockLobbyWindows) return;
        CloseWindows();
        ID.Ventana.SetActive(true);
    }
    public void BloquearLobby(){
        ConectionReady(false);
        CloseWindows();
        LobbyBlock.Ventana.SetActive(true);
        BlockLobbyWindows = true;
    }
   
    public void NewFriendConnection(string name)
    {
        
        animConection.Rebind();
        Barra.transform.Find("NombrePerfil").GetComponent<Text>().text = name;
    }

    public void OpenFriendList()
    {
      //  ResetButtonParitda();
        if (friendListGui.activeInHierarchy)
        {
            friendListGui.SetActive(false);
        }
        else
        {
            CloseWindows();
            friendListGui.SetActive(true);
        }
    }
    public void ReturnToLoggin()
    {
        BloquearLobby();
     //   ResetButtonParitda();
        //todoo carga escena
        
    }
    public void OpenChat()
    {
       // ResetButtonParitda();
        if (chatGui.activeInHierarchy)
        {
            chatGui.SetActive(false);
        }
        else
        {
            CloseWindows();
            chatGui.SetActive(true);
          
        }
    }
    public void CloseWindows()
    {
        ResetButtonParitda();
        foreach (var item in MyWindows)
        {
            item.Ventana.SetActive(false);
        }
    }
    public void EscribirEnChat(string text)
    {

    }
    public void OpenMatch()
    {
        CloseWindows();
        OpenID(idMatch);
    }
    public void OpenID(int id)
    {
        WindowID opened = MyWindows.FirstOrDefault(i => i.ID == id);
        opened.abrir();
    }
    public void OpenProfile()
    {

    }
    public void LoadDataMysql()
    {

    }
    public void ResetButtonParitda()
    {
        foreach (var item in BotonesPartida)
        {
            item.Ini = false;
           item.calculateTime = item.restTime;
          // Debug.Log(item.restTime + " res   calc " + item.calculateTime);
           item.img.gameObject.SetActive(false);
        }
    }
    public void ConectionReady(bool ready)
    {
        Barra.SetActive(ready);
    }
}

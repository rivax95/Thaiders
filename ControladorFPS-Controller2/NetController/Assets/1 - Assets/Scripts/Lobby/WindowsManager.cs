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
    public int idMatch;
    public Text cantPlayers;
    public Text Log;
    public static WindowsManager istance;
    public Button vs5;
    public Button vs2;

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
        CloseWindows();
        LobbyBlock.Ventana.SetActive(true);
        BlockLobbyWindows = true;
    }
   
    public void NewFriendConnection()
    {
        animConection.Rebind();
    }

    public void OpenFriendList()
    {
        CloseWindows();
    }
    public void ReturnToLoggin()
    {
        BloquearLobby();
        //todoo carga escena
        
    }
    public void OpenChat()
    {

    }
    public void CloseWindows()
    {
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
    public void OpenJugar() { }
}

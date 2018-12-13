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
using UnityEngine.EventSystems;
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
    public Text FriendsOnline;
    public static WindowsManager istance;
    public Button vs5;
    public Button vs2;
    public List<Partida> BotonesPartida;
    public GameObject LiderObj;
    public GameObject FriendMenu;
    public List<FriendState> MyCustomFrienStates;
    public List<FriendState> MyCustomFriendsNoAcept;
 public   GraphicRaycaster m_Raycaster;
    PointerEventData m_PointerEventData;
 public   EventSystem m_EventSystem;
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
        cantPlayers.text = PhotonManager.instance.totalPlayers.ToString() + " Players Online";
        //Log.text = PhotonManager.instance.log;
        LiderObj.SetActive(PhotonManager.instance.IsLider);
        //Friend Menu

        if (Input.GetKey(KeyCode.Mouse0))
        {
            //Set up the new Pointer Event
            m_PointerEventData = new PointerEventData(m_EventSystem);
            //Set the Pointer Event Position to that of the mouse position
            m_PointerEventData.position = Input.mousePosition;

            //Create a list of Raycast Results
            List<RaycastResult> results = new List<RaycastResult>();

            //Raycast using the Graphics Raycaster and mouse click position
            m_Raycaster.Raycast(m_PointerEventData, results);
            bool toca=false;
            //For every result returned, output the name of the GameObject on the Canvas hit by the Ray
            foreach (RaycastResult result in results)
            {
                if (result.gameObject.tag == "Menu")
                {
                    toca = true;
                }

            }
            if (!toca)
            {
                MenuFriendList.istance.cerrar();
                ResetMenuFriendOptions();
            }
        }
     
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
    public void ResetMenuFriendOptions()
    {
        foreach (var item in MyCustomFrienStates)
        {
            item.Statebtn = false;
        }
    }
    public void OnClickFriendBar(FriendState state)
    {
        
        //Configure State
        ResetMenuFriendOptions();
            if (state.Statebtn) // menu Abierto ((Lo cierras
            {
                state.Statebtn = false;
                MenuFriendList.istance.cerrar();
                MenuFriendList.istance.Trans.position = state.point.position;
            }
            else
            {
               
                state.Statebtn = true;
                MenuFriendList.istance.Abrir(state.isFriend);
                MenuFriendList.istance.Trans.position = state.point.position;

            }
        
    }
   
    public void OpenChat()
    {
       // ResetButtonParitda();
        if (chatGui.activeInHierarchy)
        {
            chatGui.SetActive(false);
            FriendMenu.SetActive(false);
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

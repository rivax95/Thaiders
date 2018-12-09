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
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
public class PhotonManager : MonoBehaviourPunCallbacks
{
    string gameVersion = "1";
    public string NickPlayer;
    public int totalPlayers;
    public TypedLobby MyLobby;
    public static PhotonManager instance;
    public string log;
    public int nextPlayersTeam;
    public Transform[] spawnpointBlue;
    public Transform[] spawnpointRed;
    RoomOptions MyOptions;
    RoomOptions Options5;
    RoomOptions Options2;
    Scene[] scenes5vs5;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        DontDestroyOnLoad(this);
        MyOptions = new RoomOptions();
    }
    void Start()
    {
        if (!PhotonNetwork.IsConnectedAndReady)
        {
            PhotonNetwork.AutomaticallySyncScene = true;
            PhotonNetwork.GameVersion = gameVersion;
            PhotonNetwork.ConnectUsingSettings();
            PhotonNetwork.ConnectToBestCloudServer(); //Conéctese a la región de Photon Cloud con el ping más bajo (en plataformas que admiten Ping de Unity).
            SetPlayerName(NickPlayer);
            PhotonNetwork.ConnectUsingSettings();
            EstablecerRoomOptions(10);

        }
        if (PhotonNetwork.IsConnected)
        {
           
        }

    }


    void Update()
    {
        totalPlayers = PhotonNetwork.CountOfPlayers;
        if (!PhotonNetwork.IsConnectedAndReady)
        {
            WindowsManager.istance.BloquearLobby();
            log = PhotonNetwork.LevelLoadingProgress.ToString();
        }

    }
    public void EstablecerRoomOptions(byte players)
    {
        MyOptions.MaxPlayers = players;
        MyOptions.IsVisible = true;
        MyOptions.IsOpen = true;
        MyOptions.PublishUserId = true;
        
    }
    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
        Debug.Log("this is lobby");
        if (PhotonNetwork.IsConnectedAndReady)
        {
            WindowsManager.istance.CloseWindows();
        }
    }
    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        if (MyLobby == null)
        {
            MyLobby = new TypedLobby();
        }

        MyLobby.Type = LobbyType.SqlLobby;
        PhotonNetwork.JoinLobby(MyLobby);

        //MyOptions.CustomRoomProperties = new ExitGames.Client.Photon.Hashtable() { { "C0", 1 } };
        //MyOptions.CustomRoomProperties = new ExitGames.Client.Photon.Hashtable() { { "C1", 2 } };
        //MyOptions.CustomRoomProperties = new ExitGames.Client.Photon.Hashtable() { { "M0", 3 } };
        //MyOptions.CustomRoomProperties = new ExitGames.Client.Photon.Hashtable() { { "M1", 4 } };
        //MyOptions.CustomRoomProperties = new ExitGames.Client.Photon.Hashtable() { { "M2", 5 } };
        //MyOptions.CustomRoomPropertiesForLobby = new string[] { "C0" };



        Debug.Log("cONECTADO");
    }
    public override void OnDisconnected(DisconnectCause cause)
    {
        base.OnDisconnected(cause);
        Debug.Log("Disconect" + cause.ToString());
    }
    public void crearSala()
    {

        PhotonNetwork.JoinOrCreateRoom("Room", MyOptions, MyLobby);
    }
    public void SetPlayerName(string value)
    {
        // #Important
        if (string.IsNullOrEmpty(value))
        {
            Debug.LogError("Player Name is null or empty");
            return;
        }
        PhotonNetwork.NickName = value;


        PlayerPrefs.SetString(NickPlayer, value);
    }
    public override void OnFriendListUpdate(List<FriendInfo> friendList)
    {
        base.OnFriendListUpdate(friendList);
    }
    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        Debug.Log("Entro a sala");
    }
    public override void OnPlayerEnteredRoom(Player newPlayer) // cuando un jugador entra a la sala
    {
        base.OnPlayerEnteredRoom(newPlayer);
       
    }
    public void UpdateTeam()
    {
        if (nextPlayersTeam == 1)
        {
            nextPlayersTeam = 2;
        }
        else
        {
            nextPlayersTeam = 1;
        }
    }
}

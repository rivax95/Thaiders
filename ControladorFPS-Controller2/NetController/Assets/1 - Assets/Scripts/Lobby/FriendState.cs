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


public class FriendState : MonoBehaviour {
   public enum StateFriend
    {
        AFK, Disconnect, Ocuped, Only, Playing
    }
    public StateFriend MyState=StateFriend.Disconnect;
    public  bool isFriend;
    public   bool inGroup;
    public bool recibeInv;

    public string Nick;
    [Header("GUI")]
   // [HideInInspector]
    public bool Statebtn;
    public RectTransform point;
    
    public GameObject InvButton;
    public float timeToRecibe;
    private float maxTime;
    public void Start()
    {
        maxTime = Random.RandomRange(19, 20);
    }
    public void Update()
    {
         InvButton.SetActive(recibeInv);
        if (recibeInv)
        {
            timeToRecibe += Time.deltaTime;
           
            if (timeToRecibe > maxTime)
            {
             //ResetValors 
                recibeInv = false;
                timeToRecibe = 0;
                maxTime = Random.RandomRange(19, 20);
            }
        }
    }

}

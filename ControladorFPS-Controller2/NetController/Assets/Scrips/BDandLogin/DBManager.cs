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

public static class DBManager  {

    public static string Email;
    public static List <string> Friends;
    public static bool LoggedIn { get { return Email != null; } }
    public static void LogOut()
    {
        Email = null;
    }
}

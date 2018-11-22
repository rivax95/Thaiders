using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
public class CanvasManager : MonoBehaviour {
    [Header("LogKill Settings")]
    public GameObject LogKill;
    public List<Sprite> Sites = new List<Sprite>(8);
    public List<Sprite> Guns = new List<Sprite>(8);
    [Header("Radar Settings")]
    public RectTransform CanvasRadar;
    public static CanvasManager instance;
    public Transform player;
    // Use this for initialization
    void Start () {
        if (instance == null)
        {
            instance = this;
        }
	}
	
	// Update is called once per frame
	void Update () {
        CanvasRadarUpdate();
	}
    public void CanvasRadarUpdate()
    {
        CanvasRadar.localEulerAngles=(new Vector3(0,0,player.localEulerAngles.y*-1));
        ////Debug.Log(player.localEulerAngles.y);
    }
    public void Kill(string nombre,string nombre2,string gun,string site,int wall)
    {
        CanvasKill Kill = new CanvasKill();
      Kill.CreateKill(LogKill.transform);
        
        Kill.SetInfo(nombre,nombre2, Guns.Find(x=> x.name==gun), Sites.Find(x => x.name == site), (wall>0) ? true:false );
        
            }
}

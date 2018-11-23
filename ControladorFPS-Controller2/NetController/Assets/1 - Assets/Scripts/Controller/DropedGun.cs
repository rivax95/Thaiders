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
public class DropedGun : MonoBehaviour {

    GameObject target;
    bool activate = false;
    Image ui;
	void Start () {
        gameObject.AddComponent<SphereCollider>();
        GetComponent<SphereCollider>().radius = 20f;
        GetComponent<SphereCollider>().isTrigger = true;
        StartCoroutine(Activate());
        ui = this.gameObject.transform.Find("Image").GetComponent<Image>();

    }


    void Update () {
        if (target)
        {
            transform.LookAt(target.transform);
        }
       
	}
    public void OnTriggerStay(Collider colider)
    {
        if (colider.transform.CompareTag("Player") && activate)
        {
           
            RaycastHit hit;
            target = colider.transform.gameObject;
            if(Physics.Raycast( transform.root.position, new Vector3(target.transform.position.x,target.transform.position.y+0.2f,target.transform.position.z), out hit, 10f ))
            {
                Debug.Log("Player");
                Debug.DrawRay(transform.root.position,target.transform.position);
                this.gameObject.transform.Find("Image").gameObject.SetActive(true);
                if (Vector3.Distance(this.transform.position, hit.point) < 1.5f)
                {
                    //Mandamos el mensaje al objeto de que puede cojernos
                    WeaponManager.instance.GetGunOBJ = transform.root.gameObject;
                    Debug.Log("Armarecojer");
                }
                else
                {
                    WeaponManager.instance.GetGunOBJ =null;
                }
            }
            else
            {
                this.gameObject.transform.Find("Image").gameObject.SetActive(false);
            }
        }
    }
    public void OnTriggerExit(Collider colision)
    {
        this.gameObject.transform.Find("Image").gameObject.SetActive(false);
        target = null;
        WeaponManager.instance.GetGunOBJ = null;
    }
    IEnumerator Activate()
    {
        yield return new WaitForSeconds (0.6f);
        activate = true;
    }
}

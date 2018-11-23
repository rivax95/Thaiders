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
using System.Linq;
public class DropedGun : MonoBehaviour {

    GameObject target;
    bool activate = false;
    Image ui;
    Transform root;
	void Start () {
        gameObject.AddComponent<SphereCollider>();
        GetComponent<SphereCollider>().radius = 20f;
        GetComponent<SphereCollider>().isTrigger = true;
        StartCoroutine(Activate());
        ui = this.gameObject.transform.Find("Container").Find("Image").GetComponent<Image>();
        root = this.transform.root.transform;
      //  transform.parent = null;
    }


    void Update () {
        if (target)
        {
            transform.LookAt(target.transform);
        }

        transform.Find("Container").localPosition = transform.up * 4;

	}
    void OnDrawGizmosSelected()
    {
        Gizmos.DrawSphere(this.transform.root.position, 1.2f);
    }
    public void OnTriggerStay(Collider colider)
    {
        if (colider.transform.CompareTag("Player") && activate)
        {
           
            RaycastHit hit;
            target = colider.transform.gameObject;
           
              
                Debug.DrawRay(transform.root.localPosition, new Vector3(target.transform.position.x, target.transform.position.y + 0.2f, target.transform.position.z));
               ui.gameObject.SetActive(true);
                Collider[] col = Physics.OverlapSphere(this.transform.root.position, 1.2f);
                Collider player = new Collider();
                foreach (var item in col)
                {
                    if(item.transform.root.CompareTag("Player"))
                    {
                        player = item.transform.root.GetComponent<Collider>();
                    }
                }
                if (player!=null && player.CompareTag("Player"))
                {
                    //Mandamos el mensaje al objeto de que puede cojernos
                    WeaponManager.instance.GetGunOBJ = transform.root.gameObject;
                  ui.GetComponent<Image>().color = Color.green;
                    Debug.Log("Armarecojer");
                }
                else
                {
                    WeaponManager.instance.GetGunOBJ =null;
                    ui.GetComponent<Image>().color = Color.white;
                }
           
        }
    }
    public void OnTriggerExit(Collider colision)
    {
   ui.gameObject.SetActive(false);
        target = null;
        WeaponManager.instance.GetGunOBJ = null;
    }
    IEnumerator Activate()
    {
        yield return new WaitForSeconds (0.6f);
        activate = true;
    }
}

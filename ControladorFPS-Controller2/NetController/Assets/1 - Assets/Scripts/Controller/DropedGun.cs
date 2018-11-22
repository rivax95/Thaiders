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

public class DropedGun : MonoBehaviour {

    GameObject target;
    bool activate = false;
	void Start () {
      gameObject.AddComponent<SphereCollider>();
       GetComponent<SphereCollider>().radius = 20f;
      GetComponent<SphereCollider>().isTrigger=true;
      StartCoroutine(Activate());
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
            Debug.Log("Player");
            RaycastHit hit;
            target = colider.transform.gameObject;
            if(Physics.Raycast(transform.root.position, new Vector3(target.transform.position.x,target.transform.position.y+1f,target.transform.position.z), out hit, 10f ))
            {
                Debug.DrawRay(transform.position, new Vector3(target.transform.position.x, target.transform.position.y + 1f, target.transform.position.z));
                this.gameObject.transform.Find("Image").gameObject.SetActive(true);
                if (Vector3.Distance(this.transform.position, colider.transform.position) < 0.3f)
                {
                    //Mandamos el mensaje al objeto de que puede cojernos
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
    }
    IEnumerator Activate()
    {
        yield return new WaitForSeconds (0.6f);
        activate = true;
    }
}

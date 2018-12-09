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
using System.Linq;
public class BidirectionalRaycast : MonoBehaviour {
    [Range(0f, 0.1f)]
    public float radius = 0f;
    public Transform rayDestination;

  public  RaycastHit[] entries = new RaycastHit[8],
                     exits = new RaycastHit[8];
   public List<RaycastHit> intersections = new List<RaycastHit>(16);
    public GameObject marcador;
    public List<float> Distancias;
    public GameObject[] Players=new GameObject[8];
    public string Tag;

    //void OnDrawGizmos()
    //{
    //    if (rayDestination != null)
    //    {
    //        Vector3 direction = rayDestination.position - transform.position;
    //        BidirectionalRaycastNonAlloc(transform.position, radius, direction, Vector3.Distance(transform.position, rayDestination.position), ref entries, ref exits, ref intersections);

    //        Gizmos.DrawLine(transform.position, rayDestination.position);

    //        for (int i = 0; i < intersections.Count; i++)
    //        {
    //            Gizmos.color = Vector3.Dot(intersections[i].normal, direction) < 0f ? Color.green : Color.red;

    //            Gizmos.DrawSphere(intersections[i].point + (intersections[i].normal * radius), radius);
    //            Gizmos.DrawLine(intersections[i].point, intersections[i].point + (intersections[i].normal * 0.25f));
    //        }
    //    }
    //}
    public void Update()
    {
         Vector3 direction = transform.forward;
         BidirectionalRaycastNonAlloc(transform.position, radius, direction, 20f, ref entries, ref exits, ref intersections,Tag);
    }
    public void BidirectionalRaycastNonAlloc(Vector3 origin, float radius, Vector3 direction, float length, ref RaycastHit[] entries, ref RaycastHit[] exits, ref List<RaycastHit> hits,string Tag)
    {
        Distancias.Clear();
        hits.Clear();
        for (int i = 0; i <Players.Length; i++)
        {
            Players[i] = null;
        }
        int hitNumber1, hitNumber2;
        direction.Normalize();
        if (radius <= 0f)
        {
            hitNumber1 = Physics.RaycastNonAlloc(origin, direction, entries, length);
            hitNumber2 = Physics.RaycastNonAlloc(origin + (direction * length), -direction, exits, length);
        }
        else
        {
            hitNumber1 = Physics.SphereCastNonAlloc(origin, radius, direction, entries, length);
            hitNumber2 = Physics.SphereCastNonAlloc(origin + (direction * length), radius, -direction, exits, length);
        }

        for (int i = 0; i < Mathf.Min(hitNumber1, entries.Length); i++)
        {
            hits.Add(entries[i]);
           
        }

        for (int i = 0; i < Mathf.Min(hitNumber2, exits.Length); i++)
        {
            exits[i].distance = length - exits[i].distance;
            hits.Add(exits[i]);
        }

        hits.Sort((x, y) => x.distance.CompareTo(y.distance));
       
        entries = entries.OrderBy(o => Vector3.Distance(o.point, origin)).ToArray<RaycastHit>();
        exits = exits.OrderBy(o => Vector3.Distance(o.point, origin)).ToArray<RaycastHit>();

        if (Input.GetKeyUp(KeyCode.A))
        {
            Debug.Log("click");
            for (int i = 0; i < hits.Count; i++)
            {
                GameObject marca = Instantiate(marcador);
                marca.transform.position = hits[i].point;
            }
        }
        Debug.Log(hits.Count);
     
           for (int i = 0; i < Mathf.CeilToInt(hits.Count / 2); i++)
            {
                if (hits.Count == 0) break;
                Distancias.Add(Vector3.Distance(entries[i].point, exits[i].point));
            }
        //Buscamos a player
           for (int i = 0; i < hits.Count; i++)
           {
               if (hits.Count == 0) break;

               if (hits[i].collider.CompareTag(Tag))
               {
                   int e = Mathf.CeilToInt(i / 2);
                   Players[e] = hits[i].collider.gameObject;
               }
           }

       // Debug.Log( Vector3.Distance( hits[1].point,hits[2].point));
    }
}

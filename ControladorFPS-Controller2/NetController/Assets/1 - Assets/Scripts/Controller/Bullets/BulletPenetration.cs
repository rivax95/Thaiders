using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BulletPenetration : MonoBehaviour
{
    [Range(0f, 0.1f)]
    public float radius = 0f;
    public Transform rayDestination;

    public RaycastHit[] entries = new RaycastHit[8],
                      exits = new RaycastHit[8];
    public List<RaycastHit> intersections = new List<RaycastHit>(16);
    public GameObject marcador;
    public List<float> Distancias = new List<float>(16);
    public GameObject[] Players = new GameObject[8];

    public List<Penetration> WallBang = new List<Penetration>(16);
    public Transform LasPoint;
    public void BidirectionalRaycastNonAlloc(Vector3 origin, float radius, Vector3 direction, float length, LayerMask Maskara, RaycastHit[] entries, RaycastHit[] exits, List<RaycastHit> hits, string Tag, bool MarkedPositions)
    {
        Distancias.Clear();
        hits.Clear();
        for (int i = 0; i < Players.Length; i++)
        {
            Players[i] = null;
        }
        int hitNumber1, hitNumber2;
        direction.Normalize();
        if (radius <= 0f)
        {
            hitNumber1 = Physics.RaycastNonAlloc(origin, direction, entries, length, Maskara);
            Debug.DrawRay(origin, direction, Color.red, 4f);
            hitNumber2 = Physics.RaycastNonAlloc(origin + (direction * length), -direction, exits, length, Maskara);
        }
        else
        {
            hitNumber1 = Physics.SphereCastNonAlloc(origin, radius, direction, entries, length, Maskara);
            hitNumber2 = Physics.SphereCastNonAlloc(origin + (direction * length), radius, -direction, exits, length, Maskara);
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
        ////Veo los que son inguales  
        //List<RaycastHit> tmpEntries = entries.ToList<RaycastHit>(); 
        //List<RaycastHit> tmpExits = entries.ToList<RaycastHit>(); 
        //List<RaycastHit> Removes=new List<RaycastHit>(20); 
        //int exception; 

        //for (int o = 0; o < tmpEntries.Count; o++) //Exception for 
        //{ 
        //    exception = o; 

        //    for (int i = 0; i < tmpEntries.Count; i++) 
        //    { 
        //        if (i != o && tmpEntries[i].collider!=null && tmpEntries[o].collider!=null&& !Removes.Contains(tmpEntries[i])) 
        //        { 
        //            Debug.Log(i + " "+o); 
        //            if (tmpEntries[o].transform.root.gameObject == tmpEntries[i].transform.root.gameObject) 
        //            { 
        //                Removes.Add(tmpEntries[i]); 
        //                Removes.Add(tmpExits[i]); 
        //            } 
        //            //List<RaycastHit> item = tmpEntries.Where(x => x.collider.transform.root == tmpEntries[i].collider.transform.root).ToList(); 
        //            //for (int e = 0; e < item.Count; e++) 
        //            //{ 
        //            //    Removes.Add(item[e]); 
        //            //} 
        //        } 
        //    } 
        //} 
        //if (entries.Length >= 1) 
        //{ 
        //    for (int i = 0; i < entries.Length; i++) 
        //    { 
        //        bool comprobation = i != 0 ? entries[i - 1].collider : entries[i].collider; 
        //        if (comprobation) 
        //        {   if() 
        //            if (entries[i].collider.transform.root == (i != 0 ? entries[i - 1].collider.transform.root : entries[i].collider.transform.root)) 
        //            { 
        //                Removes.Add(i); 
        //            } 
        //        } 

        //    } 
        //} 
        ////Los elimino 
        //Debug.Log(Removes.Count); 
        //for (int i = 0; i < Removes.Count; i++) 
        //{ 
        //    tmpEntries.Remove(Removes[i]); 
        //    tmpExits.Remove(Removes[i]); 
        //} 
        //entries = tmpEntries.ToArray(); 
        //exits = tmpExits.ToArray(); 
        if (MarkedPositions)
        {
            for (int i = 0; i < hits.Count; i++)
            {

                marcador = Resources.Load("Marcador") as GameObject;
                GameObject marca = Instantiate(marcador);
                marca.transform.position = hits[i].point;
                Destroy(marca, 4f);
            }
        }
        //marcador = Resources.Load("Marcador") as GameObject; 
        //GameObject marcaa = Instantiate(marcador); 
        //marcaa.transform.position = entries[entries.Length].point; 
        //marcaa.name = "Hitfinal"; 


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
    public void AplicarDaño(float Resistencia, float daño, Vector3 origin)
    {
        entries = entries.OrderBy(o => Vector3.Distance(o.point, origin)).ToArray<RaycastHit>();
        exits = exits.OrderBy(o => Vector3.Distance(o.point, origin)).ToArray<RaycastHit>();
        WallBang.Clear();
        float calculateRes = Resistencia;
        //Debug.Log(entries.Length); 
        for (int i = 0; i < entries.Length; i++)
        {
            if (entries[i].collider != null)
            {
                if (entries[i].collider.GetComponent<Penetration>() != null)
                {
                    WallBang.Add(entries[i].collider.GetComponent<Penetration>());
                    //Debug.Log(i + "entries"); 
                }
                else
                {
                    LasPoint = intersections[i].collider.GetComponent<Transform>();
                    return;
                }
            }
        }
        List<GameObject> Players = new List<GameObject>(16);
        for (int i = 0; i < Distancias.Count; i++)
        {
            if (i == 5) break; // Maximo de wallbang que va a hacer 
            calculateRes -= Distancias[i]
                * WallBang[i].value;
            //Debug.Log(Distancias[i] + " " + WallBang[i].value); 
            float dañofinal = calculateRes * daño / Resistencia;
            dañofinal = (dañofinal < 0) ? 0 : dañofinal;
            if (WallBang[i].gameObject.transform.GetComponent<Collider>().CompareTag("Enemy") && !Players.Contains(WallBang[i].transform.root.gameObject) && dañofinal > 0)
            {
                //Debug.Log("EsEnemigo"); 
                Players.Add(WallBang[i].transform.root.gameObject);
                WallBang[i].gameObject.transform.GetComponent<HitInfoSite>().Damage(dañofinal);
            }
            //Debug.Log(calculateRes+" "+ dañofinal +" "+WallBang.Count +" "+Distancias.Count); 
            //Debug.Log(i + "Daño final :"+dañofinal); 
        }
        Players.Clear();
    }
    public static float CalculateDrag(float velocityVec, float distance, float rhoo)
    {
        //F_drag = k * v^2 = m * a 
        //k = 0.5 * C_d * rho * A  

        float m = 0.2f; // kg peso de bala 
        float C_d = 0.5f; // coheficiente areodinamico de resistencia 
        float A = Mathf.PI * 0.05f * 0.05f; // m^2 Velocidad Inicial 
        float rho = rhoo; // kg/m3 tipo de densidad de material 

        float k = distance * C_d * rho * A;

        float vSqr = velocityVec;

        float aDrag = (k * vSqr) / m;

        //Has to be in a direction opposite of the bullet's velocity vector 
        float dragVec = aDrag * velocityVec * -1f;
        //  Debug.Log(dragVec); 
        return dragVec;
    }
    public static float calculateVelocity(float velocidad, float distanciaa, float resistencia)
    {
        float ec = 0.008f * velocidad; //energia cinetica 
        float Trabajoresistencia = resistencia * (distanciaa);
        float EnergiaCinetica = ec - Trabajoresistencia;
        float velocdiad = 0.008f * (Mathf.Pow(EnergiaCinetica, 2) / 2);
        Debug.Log("velocidad");
        return velocdiad;
    }
















    #region 2ºIteracion //Descartada por alto consumo y baja eficiencia 
    //    public void Raycasting(float velocity, Vector3 orig,Vector3 direc, LayerMask mascara) 
    //    { 
    //        Vector3[] posiciones = new Vector3[20]; 
    //        maxObjetos *= 2; 
    //        Vector3 origen = orig; 
    //        Vector3 direccion =direc; 
    //        bool collision = false; 
    //        RaycastHit hit; 
    //        int contador = 0; 
    //        int contador2 = 0; 
    //        float Matdensity; 

    //        RaycastHit[] results; 

    //        results = Physics.RaycastAll(origen, direccion, Mathf.Infinity, mascara); 

    //        foreach (RaycastHit impacto in results) 
    //        { 
    //            Debug.Log(string.Format("Has impactado con {0} en el punto {1}", impacto.collider.name, impacto.point)); 

    //            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube); 
    //            cube.transform.position = impacto.point; 
    //            cube.transform.localScale = new Vector3(0.03f, 0.03f, 0.03f); 
    //            cube.GetComponent<Collider>().enabled = false; 

    //            cube.name = "CubeFront"; 
    //        } 

    //        List<RaycastHit> impactos_delanteros = results.OfType<RaycastHit>().ToList(); 
    //        impactos_delanteros = impactos_delanteros.OrderBy(o => Vector3.Distance(o.point, origen)).ToList<RaycastHit>(); 

    //        List<RaycastHit> impactos_traseros = new List<RaycastHit>(); 

    //        foreach (RaycastHit impacto in impactos_delanteros) 
    //        { 
    ////            Debug.Log(Vector3.Distance(impacto.point, origen)); 
    //        } 
    //        for (int i = impactos_delanteros.Count -1; i >= 0; i--) 
    //        { 
    //            Vector3 inverseDirection; 
    //            if (i > 0) 
    //            { 
    //                inverseDirection = (impactos_delanteros[i-1].point - impactos_delanteros[i].point).normalized; 
    //                Debug.DrawLine(impactos_delanteros[i].point, impactos_delanteros[i - 1].point, Color.blue, Mathf.Infinity); 
    //            } 
    //            else 
    //            { 
    //                inverseDirection = (origen - impactos_delanteros[0].point).normalized; 
    //            } 


    //            if (Physics.Raycast(impactos_delanteros[i].point, inverseDirection, out hit, Mathf.Infinity, mascara)) 
    //            { 
    //                GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube); 
    //                cube.transform.position = hit.point; 
    //                cube.transform.localScale = new Vector3(0.03f, 0.03f, 0.03f); 
    //                cube.GetComponent<Collider>().enabled = false; 
    //                cube.name = "CubeBack"; 

    //                //LineRenderer lr = cube.AddComponent<LineRenderer>(); 
    //                //Vector3[] points = { cube.transform.position, results[i].point }; 

    //                //lr.SetPositions(points); 
    //                //lr.startColor = Color.red; 

    //                impactos_traseros.Add(hit); 
    //            } 

    //        } 
    #endregion
    #region 1ºIteracion // no esta completo consta de una mala arquitectura 

    //while (velocity > velocityMIN) 
    //{ 
    //    Debug.Log("entro al metodo"); 
    //    if (Physics.Raycast(origen, direccion, out hit, mascara)) 
    //    { 

    //        collision = false ? true : false; 
    //        if (collision) 
    //        { 
    //            if (hit.transform.GetComponent<Penetration>().TipoMaterial == Penetration.Material.Suelo) 
    //            { 

    //                //fuera  
    //            } 
    //            //coje el componente del material  
    //            Matdensity = hit.transform.GetComponent<Penetration>().value; 



    //        } 
    //        else 
    //        { 
    //            //el material es aire  
    //            Matdensity = 1.225f; 
    //        } 

    //        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube); 
    //        cube.transform.position = hit.point; 
    //        cube.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f); 

    //        //Vector3 offsetDirection = -1 * hit.normal; 
    //        ////offset a long way, minimum thickness of the object 
    //        //origen= hit.point + offsetDirection * 100; 

    //        origen = hit.point; 

    //       Vector3 Offset= -1 *hit.normal; 
    //        origen = hit.point + Offset; 



    //        Debug.Log(hit.collider.name); 

    //        posiciones[contador] = hit.point; 
    //    } 

    //    if (contador >= maxObjetos) 
    //    { 
    //        break; 
    //    } 
    //    contador++; 
    //} 
    //while (contador2 == posiciones.Length) 
    //{ 
    //    Debug.Log("segundo while"); 
    //    if (Physics.Raycast(posiciones[posiciones.Length-contador2], posiciones[posiciones.Length -contador2+1], out hit, mascara)) 
    //    { 

    //        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube); 
    //        cube.transform.position = hit.point; 
    //        cube.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f); 
    //    } 
    //    contador2++; 
    //} 
    #endregion

}
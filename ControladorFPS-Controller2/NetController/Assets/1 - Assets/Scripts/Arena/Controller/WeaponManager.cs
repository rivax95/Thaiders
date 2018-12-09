﻿//                                          ▂ ▃ ▅ ▆ █ ARC █ ▆ ▅ ▃ ▂ 
//                                        ..........<(+_+)>...........
// WeaponManager.cs (x/10/2018)
//Autor: Alejandro Rivas                 alejandrotejemundos@hotmail.es
//Desc: Gestiona todo el armamento
//Mod : 0.4
//Rev : -------------------------
//..............................................................................................\\
#region Librerias
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Alex.Controller;
using System.Linq;
using Alex.MouseLook;
#endregion
#region DeclaracionEnums
public enum ObjectInHands
{
    fire,grenade,mele
}
public enum Weapon{

    Police9mm,

    UMP45,
    DefenderShotgun,
    Snipper
}
public enum Mele { }
public enum Grenades {Bang,Flash,Toxic }
#endregion
public class WeaponManager : MonoBehaviour {
    //Utilizar eventos y delegados para optimizar esto.
    #region Variables
    public static WeaponManager instance;
    public ObjectInHands Hands = ObjectInHands.fire;
    public Weapon currentWeapon = Weapon.Police9mm;
    public int CurrenWeaponIndex=0;
    private Weapon[] weapons = { Weapon.Police9mm, Weapon.UMP45,Weapon.DefenderShotgun,Weapon.Snipper };
  //  [HideInInspector]
    public WeaponBase WeaponbaseCurrent;
    public List <WeaponBase> WeaponsInInventory;
    [HideInInspector]
    public GrenadeBase GrenadesAvailables;
    [HideInInspector]
    public bool Switch=false;
    public GameObject dropPoint;
    public List<GameObject>  GetGunOBJ;
    public List<GameObject> Guns;
    public LayerMask layer;
    #endregion
    #region Inicializadores
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        transform.Find("FireWeapon").Find(weapons[CurrenWeaponIndex].ToString()).gameObject.SetActive(true);
        WeaponbaseCurrent = transform.Find("FireWeapon").Find(weapons[CurrenWeaponIndex].ToString()).GetComponent<WeaponBase>();
        ActualizarInventario();
    }
    void Start()
    {
      

        foreach (WeaponBase item in WeaponsInInventory)
        {
            if (item.isPistol) { item.gameObject.SetActive(true); }
            else
            {
                item.gameObject.SetActive(false);
            }
        }

    }
    #endregion
    #region Actualizadores
    void Update()
    {
        if (Input.GetKey(KeyCode.Alpha1))
        {
            ActualizarInventario();
        }
        SwitchModeWeapon();
      
            CheckWeaponSwitch();
            if (Input.GetButtonDown("DropWeapon"))
            {
                CheckDrop();
            }
     
            CheckGetGun();
        
        if (transform.Find("FireWeapon").childCount ==0 && Hands != ObjectInHands.grenade)
        {
            Hands = ObjectInHands.mele;
            OnSwichHandWeapon(Hands);
        }
    }
    #endregion
    #region Checkers
    public void CheckGetGun()
    {
        RaycastHit hit;
        GameObject gun=null;
        if (GetGunOBJ.Count >0) { 
            if (Physics.SphereCast(this.transform.parent.position,1.5f,transform.forward,out hit, 1.5f,layer))
            {
                Debug.Log("Ray");
                //if (hit.collider.transform.root.gameObject.GetComponent<WeaponBase>())
                //{
                    gun = hit.collider.transform.root.gameObject;
                    Debug.Log(gun.name);
                //}
            }
        }
        if (gun == null) return;
        GetGun(gun);
    }
    void CheckDrop()
    {
        if (Hands != ObjectInHands.fire) return;
        if (transform.Find("FireWeapon").childCount ==0) return;
        WeaponbaseCurrent.OnSwich();
        dropWeapon(WeaponbaseCurrent);
        //ActualizarInventario();
        //selecNextWeapon();

    }
    void CheckWeaponSwitch()
    {
        float mousewheel = Input.GetAxis("Mouse ScrollWheel");
        if (WeaponbaseCurrent.fireLock) return;
        if (Hands == ObjectInHands.grenade) return;
        if (WeaponsInInventory.Count < 2) return;

        if (mousewheel > 0)
        {
            SelectPreviousWeapon();
        }
        else if (mousewheel < 0)
        {
            selecNextWeapon();
        }

        Switch = true;
    }
    void checkAmountGrenades()
    {

    }
    #endregion 
    #region InventoryOperations //Solo operaciones Logicas
    public void ActualizarInventario()
    {
        Debug.Log("Llamada");
        for (int i = 0; i < WeaponsInInventory.Count; i++)
        {
            try
            {
                WeaponsInInventory[i].gameObject.SetActive(true);

            }
            catch { }
        }

        WeaponsInInventory.Clear();
        foreach (Transform t in transform.Find("FireWeapon").transform)
        {
            WeaponsInInventory.Add(t.gameObject.GetComponent<WeaponBase>());
        }


        foreach (WeaponBase item in WeaponsInInventory)
        {
            item.gameObject.SetActive(false);
            item.fireLock = false;
        }


        try
        {
            WeaponsInInventory[CurrenWeaponIndex].gameObject.SetActive(true);
            WeaponbaseCurrent = WeaponsInInventory[CurrenWeaponIndex];
        }
        catch { Debug.LogWarning("Un indice ha salido del rango, pero no afecta al sistema"); }

    }
    void ChangeGunPerIndex(int index)
    {
        WeaponbaseCurrent.OnSwich();
        if (index <= WeaponsInInventory.Count)
        {

            foreach (WeaponBase item in WeaponsInInventory)
            {
                item.gameObject.SetActive(false);
            }
            CurrenWeaponIndex = index;
            WeaponsInInventory[CurrenWeaponIndex].gameObject.SetActive(true);
            WeaponbaseCurrent = WeaponsInInventory[CurrenWeaponIndex];
        }
    }

    public void OnSwichHandWeapon(ObjectInHands type)
    {
        GameObject fire = transform.Find("FireWeapon").gameObject, grenade = transform.Find("GrenadeWeapon").gameObject, mele = transform.Find("MeleWeapon").gameObject;
        WeaponbaseCurrent.OnSwich();
        fire.SetActive(false); grenade.SetActive(false); mele.SetActive(false);
        switch (type)
        {
            case ObjectInHands.fire:
                fire.SetActive(true);
                StartCoroutine(actuTemp());
                break;
            case ObjectInHands.grenade:
                grenade.SetActive(true);
                break;
            case ObjectInHands.mele:
                mele.SetActive(true);
                break;
            default:
                break;
        }
    }
    void SelectPreviousWeapon()
    {

        if (CurrenWeaponIndex == 0)
        {
            CurrenWeaponIndex = WeaponsInInventory.Count - 1;
        }
        else
        {
            CurrenWeaponIndex--;
        }
        SwitchToCurrentWeapon();
    }
    void selecNextWeapon()
    {
        if (CurrenWeaponIndex >= WeaponsInInventory.Count - 1)
        {
            CurrenWeaponIndex = 0;
        }
        else
        {
            CurrenWeaponIndex++;
        }
        SwitchToCurrentWeapon();
    }
    void SwitchToCurrentWeapon() // Conectar con el controller //Conectado por otro lado
    {
        Hands = ObjectInHands.fire;
        OnSwichHandWeapon(Hands);
        for (int i = 0; i < WeaponsInInventory.Count; i++)
        {
            WeaponsInInventory[i].gameObject.SetActive(false);
            WeaponsInInventory[i].isReloading = false;
            WeaponsInInventory[i].fireLock = false;
        }
        //transform.Find(weapons[CurrenWeaponIndex].ToString()).gameObject.SetActive(true);
        WeaponbaseCurrent.OnSwich();
        try { 
        WeaponsInInventory[CurrenWeaponIndex].gameObject.SetActive(true);
        WeaponbaseCurrent = WeaponsInInventory[CurrenWeaponIndex].gameObject.GetComponent<WeaponBase>();
        }
        catch { Debug.LogWarning("Un indice ha salido del rango, pero no afecta al sistema"); }
        WeaponbaseCurrent.AsingConfigurations();
        Switch = false;
    }
  
    void Copia(WeaponBase orig, WeaponBase copia)
    {
        //copia.penetration = orig.penetration;
        //copia.minpenetration = orig.minpenetration;
        copia.TipoDeArma = orig.TipoDeArma;
        copia.RecoilAfect = orig.RecoilAfect;
        copia.SpraySystem = orig.SpraySystem;
        copia.SprayConf = orig.SprayConf;
        copia.Playeranim = orig.Playeranim;
        copia.ShootPoint = orig.ShootPoint;
        copia.DropedImg = orig.DropedImg;
        copia.Model = orig.Model;
        copia.Name = orig.Name;
        copia.clipSize = orig.clipSize;
        copia.bulletsLeft = orig.bulletsLeft;
        copia.bulletsInClip = orig.bulletsInClip;
        copia.maxAmmo = orig.maxAmmo;
        if (orig.TipoDeArma == Tipo.RifleFrancoTirador)
        {
            copia.GetComponent<Sniper>().PointOfView= orig.GetComponent<Sniper>().PointOfView;
            copia.GetComponent<Sniper>().CamaraSnip = this.transform.parent.GetComponent<Camera>();
            copia.GetComponent<Sniper>().Guncamera = this.transform.GetComponent<Camera>();
            copia.GetComponent<Sniper>().Sensi = new MauseLook[2];
            copia.GetComponent<Sniper>().Sensi[0] = this.transform.root.GetComponent<MauseLook>();
            copia.GetComponent<Sniper>().Sensi[1] = this.transform.parent.GetComponent<MauseLook>();
        }
    }
    #endregion
    #region ReturnFunctions
    public WeaponBase buscarArmaFuego(string name)
   {
       foreach (var item in WeaponsInInventory)
       {
           if (item.Name == name)
           {
               return item;
           }
       }
       return null;
   }

    #endregion
    #region Inputs
    void SwitchModeWeapon()
    {
        if (Input.GetMouseButtonDown(2))
        {
            Debug.Log("Cambio");
            Hands = (Hands==ObjectInHands.grenade) ? ObjectInHands.fire : ObjectInHands.grenade;
            OnSwichHandWeapon(Hands);
            //TODOOOOOOOOOOOOOOOOOOO
            //2WeaponbaseCurrent.OnSwich();
        }
        if (Input.GetKey(KeyCode.V))
        {
            Hands = ObjectInHands.mele;
            OnSwichHandWeapon(Hands);
        }
       
    }
    public void GetGun(GameObject gun)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
         
            foreach (var item in Guns)
            {
                if (item.GetComponent<WeaponBase>().Name == gun.GetComponent<WeaponBase>().Name)
                {
                    Copia(gun.GetComponent<WeaponBase>(), item.GetComponent<WeaponBase>());
                    GameObject obj = Instantiate(item, this.transform.Find("FireWeapon").transform);
                  

                    //Destuir en networking sin mas
                    if (buscarArmaFuego(gun.GetComponent<WeaponBase>().Name)) 
                    {
                        WeaponBase droped = buscarArmaFuego(gun.GetComponent<WeaponBase>().Name); //buscar esto en la lista

                        ChangeGunPerIndex(WeaponsInInventory.IndexOf(droped)); //pasarle el indice de la de arriba
                        dropWeapon(WeaponbaseCurrent);
                    }
                    Destroy(gun);
                    Hands = ObjectInHands.fire;
                    OnSwichHandWeapon(Hands);
                    ActualizarInventario();
                    ChangeGunPerIndex(WeaponsInInventory.Count - 1);
                   GetGunOBJ.Remove(gun);
                   WeaponbaseCurrent.gameObject.SetActive(true);
                 
                }
                
            }
        }

    }

    #endregion
    #region Drop
    public void dropIstantiate(GameObject current)
    {
        GameObject cube = new GameObject();
     
        cube = Instantiate(current.GetComponent<WeaponBase>().Model);
        switch (current.GetComponent<WeaponBase>().TipoDeArma)
        {
            case Tipo.Pistola:
                cube.AddComponent<WeaponBase>().enabled = false; ;
                Copia(current.GetComponent<WeaponBase>(), cube.GetComponent<WeaponBase>());
                break;
            case Tipo.RifleSemiAutomatico:
                cube.AddComponent<SlideStopWeapon>().enabled = false; ;
                Copia(current.GetComponent<WeaponBase>(), cube.GetComponent<SlideStopWeapon>());

                break;
            case Tipo.Escopeta:
                cube.AddComponent<Escopeta>().enabled = false;
                Copia(current.GetComponent<WeaponBase>(), cube.GetComponent<Escopeta>());
                break;
            case Tipo.RifleFrancoTirador:
                cube.AddComponent<Sniper>().enabled = false;
                Copia(current.GetComponent<WeaponBase>(), cube.GetComponent<Sniper>());
                break;
            case Tipo.Rifle:
                 cube.AddComponent<SlideStopWeapon>().enabled = false; ;
                Copia(current.GetComponent<WeaponBase>(), cube.GetComponent<SlideStopWeapon>());
                break;
            default:
                cube.AddComponent<WeaponBase>().enabled = false; ;
                Copia(current.GetComponent<WeaponBase>(), cube.GetComponent<WeaponBase>());
                break;
        }


        cube.transform.position = new Vector3(dropPoint.transform.position.x, dropPoint.transform.position.y, dropPoint.transform.position.z + 0.2f);
        //cube.transform.position = new Vector3(dropPoint.transform.position.x,dropPoint.transform.position.y,transform.position.z*-1);
        //añadimos RB
        if (cube.GetComponent<Rigidbody>() == null)
        {
            cube.AddComponent<Rigidbody>();
        }
        //fuerzas
        cube.GetComponent<Rigidbody>().AddForce(transform.forward * 2, ForceMode.Impulse);
        Rigidbody rig = cube.GetComponent<Rigidbody>();
        rig.mass = 2f;
        rig.drag = 2f;
        rig.AddForce(dropPoint.transform.forward * 15, ForceMode.Impulse);
        cube.transform.Find("Canvas").gameObject.AddComponent<DropedGun>();
       
    }
    void dropWeapon(WeaponBase current)
    {
        int i = WeaponsInInventory.IndexOf(WeaponbaseCurrent);

        dropIstantiate(current.gameObject);
        WeaponsInInventory.RemoveAt(i);
        Destroy(current.gameObject);
       
        selecNextWeapon();
    }
    #endregion
    #region corrutinas
    IEnumerator actuTemp()
   {
       yield return new WaitForEndOfFrame();
       ActualizarInventario();
   }
    IEnumerator OnSwichHandsCorr(ObjectInHands obj)
    {
        yield return new WaitForEndOfFrame();
        OnSwichHandWeapon(obj);
    }
    #endregion
}
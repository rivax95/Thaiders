﻿//                                          ▂ ▃ ▅ ▆ █ ZEN █ ▆ ▅ ▃ ▂ 
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
using Alex.Controller;
using System.Linq;

public enum Weapon{

    Police9mm,

    UMP45,
    DefenderShotgun,
    Snipper
}
public enum Grenades {Bang,Flash,Toxic }
public class WeaponManager : MonoBehaviour {
   
    public static WeaponManager instance;
    public Weapon currentWeapon = Weapon.Police9mm;
    public int CurrenWeaponIndex=0;
    private Weapon[] weapons = { Weapon.Police9mm, Weapon.UMP45,Weapon.DefenderShotgun,Weapon.Snipper };
  //  [HideInInspector]
    public WeaponBase WeaponbaseCurrent;
    public List <WeaponBase> WeaponsInInventory;
    [HideInInspector]
    public GrenadeBase GrenadesAvailables;
    public bool isWeapon = true;
    [HideInInspector]
    public bool Switch=false;
    public GameObject dropPoint;
    public GameObject GetGunOBJ;
    public List<GameObject> Guns;
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
    }
    void Start()
    {
        transform.Find(weapons[CurrenWeaponIndex].ToString()).gameObject.SetActive(true);
        WeaponbaseCurrent= transform.Find(weapons[CurrenWeaponIndex].ToString()).GetComponent<WeaponBase>();
        ActualizarInventario();

        foreach (WeaponBase item in WeaponsInInventory)
        {
            if (item.isPistol) { item.gameObject.SetActive(true); }
            else
            {
                item.gameObject.SetActive(false);
            }
        }

    }
    void Update()
    {
        SwitchModeWeapon();
        if (isWeapon)
        {

            CheckWeaponSwitch();
            if (Input.GetButtonDown("DropWeapon"))
            {
                CheckDrop();
            }
        }
        else // isGrenade
        {

        }
        if (GetGunOBJ)
        {
            CheckGetGun(GetGunOBJ);
        }
    }
   public void CheckGetGun (GameObject gun)
    {
        if(Vector3.Distance(transform.root.position,gun.transform.position) > 1.3f){
            return;
        }
        GetGun(gun);
    }
    public void GetGun(GameObject gun)
    {
        if (Input.GetKey(KeyCode.E))
        {
            Debug.Log("furula");
            foreach (var item in Guns)
            {
                if(item.GetComponent<WeaponBase>().Name == gun.GetComponent<WeaponBase>().Name)
                {
                   GameObject obj= Instantiate(item, this.transform);
                    Copia(obj.GetComponent<WeaponBase>(), gun.GetComponent<WeaponBase>());
                    Destroy(gun); //Destuir en networking sin mas
                    break;
                }
            }
        }

    }
    void CheckDrop()
    {
        if (WeaponbaseCurrent.isPistol) return;
        
        dropWeapon(WeaponbaseCurrent);
     //   ActualizarInventario();
        selecNextWeapon();
    }
    void SwitchModeWeapon()
    {
        if (Input.GetMouseButton(2))
        {
            Debug.Log("Cambio");
            isWeapon = (isWeapon) ? false : true;
            //TODOOOOOOOOOOOOOOOOOOO
            //2WeaponbaseCurrent.OnSwich();
        }
    }
    void SwitchToCurrentWeapon() // Conectar con el controller //Conectado por otro lado
    {
        
        for (int i = 0; i < WeaponsInInventory.Count ; i++)
        {
            WeaponsInInventory[i].gameObject.SetActive(false);
            WeaponsInInventory[i].isReloading = false;
            WeaponsInInventory[i].fireLock = false;
        }
        //transform.Find(weapons[CurrenWeaponIndex].ToString()).gameObject.SetActive(true);
        WeaponbaseCurrent.OnSwich();
        WeaponsInInventory[CurrenWeaponIndex].gameObject.SetActive(true);
        WeaponbaseCurrent = WeaponsInInventory[CurrenWeaponIndex].gameObject.GetComponent<WeaponBase>();
        WeaponbaseCurrent.AsingConfigurations();
        Switch = false;
    }
    void CheckWeaponSwitch()
    {
        float mousewheel = Input.GetAxis("Mouse ScrollWheel");
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
    void checkAmountGrenades()
    {

    }
    void dropWeapon(WeaponBase current)
    {
        int i = WeaponsInInventory.IndexOf(WeaponbaseCurrent);
        WeaponsInInventory.RemoveAt(i);
        
        dropIstantiate(current.gameObject);
        Destroy(current.gameObject);
       
    }

  public  void dropIstantiate(GameObject current)
    {
        GameObject cube = Instantiate(current.GetComponent<WeaponBase>().Model);

        cube.transform.position = new Vector3(dropPoint.transform.position.x, dropPoint.transform.position.y, dropPoint.transform.position.z+0.2f);
        //cube.transform.position = new Vector3(dropPoint.transform.position.x,dropPoint.transform.position.y,transform.position.z*-1);
        Rigidbody rig = cube.GetComponent<Rigidbody>();
        rig.mass = 2f;
        rig.drag = 2f;
        rig.AddForce(dropPoint.transform.forward*15, ForceMode.Impulse);
        cube.transform.Find("Canvas").gameObject.AddComponent<DropedGun>();
        //cube.transform.localScale = new Vector3(0.1f, 0.2f, 0.1f);
        // copiamos valores 
       
        switch (weapons[CurrenWeaponIndex])
        {
            case Weapon.Police9mm:
                cube.AddComponent<WeaponBase>().enabled = false; ;
                Copia(current.GetComponent<WeaponBase>(), cube.GetComponent<WeaponBase>());
                break;
            case Weapon.UMP45:
                cube.AddComponent<SlideStopWeapon>().enabled = false; ;
                Copia(current.GetComponent<WeaponBase>(), cube.GetComponent<SlideStopWeapon>());
                
                break;
            case Weapon.DefenderShotgun:
                cube.AddComponent<Escopeta>().enabled = false;
                Copia(current.GetComponent<WeaponBase>(), cube.GetComponent<Escopeta>());
                break;
            case Weapon.Snipper:
                cube.AddComponent<Sniper>().enabled = false;
                Copia(current.GetComponent<WeaponBase>(), cube.GetComponent<Sniper>());
                break;
        }

        //añadimos RB
        cube.AddComponent<Rigidbody>();
        //fuerzas
        cube.GetComponent<Rigidbody>().AddForce(transform.forward  *2, ForceMode.Impulse);
    }
    void Copia(WeaponBase orig,WeaponBase copia)
    {
        //copia.penetration = orig.penetration;
        //copia.minpenetration = orig.minpenetration;
        copia.Name = orig.Name;
        copia.clipSize = orig.clipSize;
        copia.bulletsLeft = orig.bulletsLeft;
        copia.maxAmmo = orig.maxAmmo;
    }
   public void ActualizarInventario()
    {
        for (int i = 0; i < WeaponsInInventory.Count; i++)
        {
              try
            {
               WeaponsInInventory[i].gameObject.SetActive(true);
                 
            }
            catch { }
        }
      
        WeaponsInInventory.Clear();
        foreach (Transform t in this.transform)
        {
            WeaponsInInventory.Add(t.gameObject.GetComponent<WeaponBase>());
        }
       
      
        foreach (WeaponBase item in WeaponsInInventory)
        {
            item.gameObject.SetActive(false);
        }
        WeaponsInInventory[0].gameObject.SetActive(true);
        WeaponbaseCurrent = WeaponsInInventory[0];
    }
   
}

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
using Alex.Controller;

public enum Weapon{

    Police9mm,

    UMP45,
    DefenderShotgun

}
public enum Grenades {Bang,Flash,Toxic }
public class WeaponManager : MonoBehaviour {
   
    public static WeaponManager instance;
    public Weapon currentWeapon = Weapon.Police9mm;
    public int CurrenWeaponIndex=0;
    private Weapon[] weapons = { Weapon.Police9mm, Weapon.UMP45,Weapon.DefenderShotgun };
  //  [HideInInspector]
    public WeaponBase WeaponbaseCurrent;
    public List <WeaponBase> WeaponsInInventory;
    [HideInInspector]
    public GrenadeBase GrenadesAvailables;
    public bool isWeapon = true;
    [HideInInspector]
    public bool Switch=false;
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
    }
    void CheckDrop()
    {
        if (WeaponbaseCurrent.isPistol) return;
        int i=WeaponsInInventory.IndexOf(WeaponbaseCurrent);
        WeaponsInInventory.RemoveAt(i);
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
        WeaponsInInventory[CurrenWeaponIndex].gameObject.SetActive(true);
        WeaponbaseCurrent = WeaponsInInventory[CurrenWeaponIndex].gameObject.GetComponent<WeaponBase>();
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
        
        
        dropIstantiate(current.gameObject);
        Destroy(current.gameObject);
       
    }

  public  void dropIstantiate(GameObject current)
    {
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.position = this.transform.position;
     
        cube.transform.localScale = new Vector3(0.1f, 0.2f, 0.1f);
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
        }

        //añadimos RB
        cube.AddComponent<Rigidbody>();
        //fuerzas
        cube.GetComponent<Rigidbody>().AddForce(transform.forward  *2, ForceMode.Impulse);
    }
    void Copia(WeaponBase orig,WeaponBase copia)
    {
        copia.penetration = orig.penetration;
        copia.minpenetration = orig.minpenetration;
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

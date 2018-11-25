//                                          ▂ ▃ ▅ ▆ █ ARC █ ▆ ▅ ▃ ▂ 
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
    public GameObject GetGunOBJ;
    public List<GameObject> Guns;
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
        if (GetGunOBJ)
        {
            CheckGetGun(GetGunOBJ);
        }
        if (transform.Find("FireWeapon").childCount ==0 && Hands != ObjectInHands.grenade)
        {
            Hands = ObjectInHands.mele;
            OnSwichHandWeapon(Hands);
        }
    }
    #endregion
    #region Checkers
    public void CheckGetGun(GameObject gun)
    {

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
        WeaponsInInventory[CurrenWeaponIndex].gameObject.SetActive(true);
        WeaponbaseCurrent = WeaponsInInventory[CurrenWeaponIndex];
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
        WeaponsInInventory[CurrenWeaponIndex].gameObject.SetActive(true);
        WeaponbaseCurrent = WeaponsInInventory[CurrenWeaponIndex].gameObject.GetComponent<WeaponBase>();
        WeaponbaseCurrent.AsingConfigurations();
        Switch = false;
    }
  
    void Copia(WeaponBase orig, WeaponBase copia)
    {
        //copia.penetration = orig.penetration;
        //copia.minpenetration = orig.minpenetration;
        copia.RecoilAfect = orig.RecoilAfect;
        copia.SpraySystem = orig.SpraySystem;
        copia.Playeranim = orig.Playeranim;
        copia.ShootPoint = orig.ShootPoint;
        copia.DropedImg = orig.DropedImg;
        copia.Model = orig.Model;
        copia.Name = orig.Name;
        copia.clipSize = orig.clipSize;
        copia.bulletsLeft = orig.bulletsLeft;
        copia.bulletsInClip = orig.bulletsInClip;
        copia.maxAmmo = orig.maxAmmo;
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
        if (Input.GetKey(KeyCode.E))
        {
            Debug.Log("furula");
            foreach (var item in Guns)
            {
                if (item.GetComponent<WeaponBase>().Name == gun.GetComponent<WeaponBase>().Name)
                {
                    GameObject obj = Instantiate(item, this.transform.Find("FireWeapon").transform);
                    Copia(gun.GetComponent<WeaponBase>(), obj.GetComponent<WeaponBase>());

                    //Destuir en networking sin mas
                    if (buscarArmaFuego(gun.GetComponent<WeaponBase>().Name))
                    {
                        WeaponBase droped = buscarArmaFuego(gun.GetComponent<WeaponBase>().Name); //buscar esto en la lista

                        ChangeGunPerIndex(WeaponsInInventory.IndexOf(droped)); //pasarle el indice de la de arriba
                        dropWeapon(WeaponbaseCurrent);
                    }
                    Destroy(gun);
                    ActualizarInventario();
                    ChangeGunPerIndex(WeaponsInInventory.Count - 1);
                    break;
                }
            }
        }

    }

    #endregion
    #region Drop
    public void dropIstantiate(GameObject current)
    {
        GameObject cube = Instantiate(current.GetComponent<WeaponBase>().Model);

        cube.transform.position = new Vector3(dropPoint.transform.position.x, dropPoint.transform.position.y, dropPoint.transform.position.z + 0.2f);
        //cube.transform.position = new Vector3(dropPoint.transform.position.x,dropPoint.transform.position.y,transform.position.z*-1);
        Rigidbody rig = cube.GetComponent<Rigidbody>();
        rig.mass = 2f;
        rig.drag = 2f;
        rig.AddForce(dropPoint.transform.forward * 15, ForceMode.Impulse);
        cube.transform.Find("Canvas").gameObject.AddComponent<DropedGun>();
        //cube.transform.localScale = new Vector3(0.1f, 0.2f, 0.1f);
        // copiamos valores 

        switch (current.GetComponent<WeaponBase>().Name)
        {
            case "Police9mm":
                cube.AddComponent<WeaponBase>().enabled = false; ;
                Copia(current.GetComponent<WeaponBase>(), cube.GetComponent<WeaponBase>());
                break;
            case "UMP45":
                cube.AddComponent<SlideStopWeapon>().enabled = false; ;
                Copia(current.GetComponent<WeaponBase>(), cube.GetComponent<SlideStopWeapon>());

                break;
            case "DefenderShotgun":
                cube.AddComponent<Escopeta>().enabled = false;
                Copia(current.GetComponent<WeaponBase>(), cube.GetComponent<Escopeta>());
                break;
            case "Sniper":
                cube.AddComponent<Sniper>().enabled = false;
                Copia(current.GetComponent<WeaponBase>(), cube.GetComponent<Sniper>());
                break;
            default:
                cube.AddComponent<WeaponBase>().enabled = false; ;
                Copia(current.GetComponent<WeaponBase>(), cube.GetComponent<WeaponBase>());
                break;
        }

        //añadimos RB
        cube.AddComponent<Rigidbody>();
        //fuerzas
        cube.GetComponent<Rigidbody>().AddForce(transform.forward * 2, ForceMode.Impulse);
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
    #endregion
}

//                                          ▂ ▃ ▅ ▆ █ ZEN █ ▆ ▅ ▃ ▂ 
//                                        ..........<(+_+)>...........
// .cs (15/10/18)
//Autor: Alejandro Rivas                 alejandrotejemundos@hotmail.es
//Desc:
//Mod : 
//Rev : 0.2
//..............................................................................................\\
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Alex.Controller;
using Alex.MouseLook;
public enum ModoDeFuego
{
    SemiAuto,
    FullAuto
}
public enum Tipo
{
    Pistola,
    RifleSemiAutomatico,
    Rifle,
    Escopeta,
    RifleFrancoTirador
}
public class WeaponBase : MonoBehaviour
{

    protected AudioSource audiosource;
    public bool fireLock;
    public bool canShoot;
    public bool Shoot = false;
    public bool isReloading = false;
    [Header("Sonidos")]
    public AudioClip Fire;
    public AudioClip DryFire;
    public AudioClip Draw;
    public AudioClip MagOutSound;
    public AudioClip MagInSound;
    public AudioClip boltSound;
    [Header("Referencias")]
    public RecoilAffect RecoilAfect;
    public SpreadSystem SpraySystem;
    protected Animator animator;
    // public GameObject sparkPrefab; 
    public ParticleSystem muzzleFlash;
    public ParticleSystem bloodFX;
    [Header("Weapon Attributes")]
    public SpreadConfiguration SprayConf;
    public string Name;
    [Range(1, 8)]
    public int BulletsPerShoot;
    public bool isPistol;
    public bool MarkedShoots;
    public LayerMask ShootRayLayer;
    public ModoDeFuego fireMode = ModoDeFuego.FullAuto;
    public Tipo TipoDeArma = Tipo.Pistola;
    public float damage = 20f;
    [Range(1f, 15f)]
    public float Resistencia;
    [HideInInspector]
    public float SprayShoot = 0.01f;
    public float fireRate = 1f;
    public int bulletsInClip;
    [HideInInspector]
    public float spreatBase = 0f;
    [HideInInspector]
    public float spreat = 0.1f;
    //[HideInInspector]
    public bool Pointing;
    public float recoil = 1f;
    //   public float BulletAmountPenetration; 
    public float Distance;

    //public float penetration;
    //public float minpenetration;
    public int clipSize = 12;
    //  [HideInInspector] 
    public int bulletsLeft;
    public int maxAmmo = 100;
    public Animator Playeranim;
    public GameObject ShootPoint;
    public GameObject Model;
    public Sprite DropedImg;
    [HideInInspector]
    public int type;
   public virtual void Awake()
    {
        ConfigurationType();
        AsingConfigurations();
    }
  public  virtual void Start()
    {
        isReloading = false;
        animator = GetComponent<Animator>();
        audiosource = GetComponent<AudioSource>();
        if (!transform.name.Contains("Clone"))
        {
            bulletsInClip = clipSize;
            bulletsLeft = maxAmmo;
        }
        Invoke("EnableWeapon", 1f); // pasar a corrutinas 
       

    }
    void EnableWeapon()
    {
        canShoot = true;
    }
    void Update()
    {
        if (fireMode == ModoDeFuego.FullAuto && Input.GetButton("Fire1"))
        {
            checkFire();
        }
        else if (fireMode == ModoDeFuego.SemiAuto && Input.GetButtonDown("Fire1"))
        {
            checkFire();
        }
        if (Input.GetButtonDown("Reload"))
        {
            //Debug.Log("PressR"); 
            ChekReload();
        }
        if (Input.GetMouseButtonDown(1))
        {


            CheckApuntar();
        }
    }
  protected  virtual void CheckApuntar() { }
    void checkFire()
    {
        if (isReloading) return;
        if (!canShoot) { return; }
        if (fireLock) { return; }
        if (bulletsInClip > 0)
        {
            FIRE();
            //Debug.Log("DisaproController1"); 
        }
        else
        {
            Debug.Log("NoDisaproController1");
            DRYFIRE();
        }
    }
    public virtual void PlayFireAnimation()
    {
        animator.CrossFadeInFixedTime("Fire", 0.1f);



    }
    public void CreateBlood(Vector3 pos, Quaternion rot)
    {
        ParticleSystem blood = Instantiate(bloodFX, pos, rot);
        blood.Play();
        Destroy(blood, 1f);
    }
    public virtual void DetectedHit()
    {
        #region primer intento 
        // RaycastHit[] hit; 
        // float penetrationHit=penetration; 
        // float damegehit=damage; 
        //// float amount = BulletAmountPenetration; 
        //  //Ray Ray = ShootPoint.ScreenPointToRay(Input.mousePosition); 
        ////  Debug.DrawRay(ShootPoint.ViewportPointToRay(Vector3.forward),); 
        // hit = Physics.RaycastAll(ShootPoint.transform.position, ShootPoint.transform.forward, ShootRayLayer); 
        // // Debug.Log(hit.transform.gameObject.name); 

        // for (int i = 0; i < hit.Length; i++) 
        // { 

        //     Penetration takevalue = hit[i].transform.GetComponent<Penetration>(); 
        //     float body = takevalue.value; 
        //     Health health = hit[i].transform.GetComponent<Health>(); 

        //     if (minpenetration <= body) 
        //     { 
        //         Debug.Log("Enrta"); 
        //         if (takevalue != null) 
        //         { 
        //             Debug.Log("Enrta1"); 
        //             if (penetrationHit - body > 0) 
        //             { 
        //                 Debug.Log("Enrta2"); 
        //                 penetrationHit -= body; 
        //               //sigue haciendo daño 
        //                 float porcentaje = penetrationHit * 100 / penetration ;//cuanto por ciento 
        //                 float damageHit = porcentaje / damage; 
        //                 float finalDamage = damageHit - damage; 
        //                 finalDamage = Mathf.Abs(finalDamage); 
        //                 Debug.Log("damge" + finalDamage); 

        //             } 
        //             else { 
        //                 //final damage 
        //                 penetrationHit -= minpenetration; 

        //                 break; } 


        //         } 
        //     } 

        //     else 
        //     { 
        //         penetrationHit -= minpenetration; 
        //         //calcula daño 
        //         // haz daño una vez 
        //         if (hit[i].transform.CompareTag("Enemy")) 
        //         { 
        //             //Debug.Log("hace"); 

        //             if (health == null) 
        //             { 
        //                 //ealth.take 
        //                 Debug.Log("No ahi vida"); 
        //             } 
        //             else 
        //             { 
        //                 Debug.Log("hit"); 
        //                 health.TakeDamage(damage); 
        //                 CreateBlood(hit[i].point, Quaternion.identity); 
        //             } 
        //         } 
        //         break; 
        //     } 
        // } 
        #endregion
        #region segundoIntento 
        for (int i = 0; i < BulletsPerShoot; i++)
        {
            BulletPenetration balaPen = new BulletPenetration();

            Vector3 direct = CrearSpread(spreat, ShootPoint.transform);
            //  balaPen.Raycasting(1200, ShootPoint.transform.position, direct, ShootRayLayer); 
            balaPen.BidirectionalRaycastNonAlloc(ShootPoint.transform.position, 0.1f, direct, Distance, ShootRayLayer, balaPen.entries, balaPen.exits, balaPen.intersections, "Enemy", MarkedShoots);
            balaPen.AplicarDaño(Resistencia, damage, ShootPoint.transform.position,"Player",Name);
            //Debug.Log("Disparo el hit: "+balaPen.intersections.Count); 

        }
        #endregion
    }
    Vector3 CrearSpread(float spread, Transform shootpoint)
    {
        return Vector3.Lerp(shootpoint.TransformDirection(Vector3.forward * 100), Random.onUnitSphere, spread);
    }

    public virtual void FIRE()
    {
        Shoot = true;
        audiosource.PlayOneShot(Fire);
        fireLock = true;
        //DetectedHit(); 

        muzzleFlash.Stop();
        muzzleFlash.Play();

        PlayFireAnimation();
        bulletsInClip--;

        DetectedHit();

        Recoil();

        StartCoroutine(CoResetFireLook());
    }
   public virtual void DRYFIRE()
    {
        audiosource.PlayOneShot(DryFire);
        fireLock = true;

        // animator.CrossFadeInFixedTime("Fire", 0.1f); 
        StartCoroutine(CoResetFireLook());
    }
   public virtual  IEnumerator CoResetFireLook()
    {
        yield return new WaitForSeconds(fireRate);
        Shoot = false;
        fireLock = false;
    }
    void ChekReload()
    {
        if (bulletsLeft > 0 && bulletsInClip < clipSize)
        {
            Debug.Log("Accion de recargar");
            Reload();
        }


    }
    protected virtual void Reload()
    {
        if (isReloading) return;
        isReloading = true;
        animator.CrossFadeInFixedTime("Reload", 0.1f);
        Playeranim.CrossFadeInFixedTime("Reload", 0.1f);
    }
    protected virtual void ReloadAmmo()
    {
        int bulletsToLoad = clipSize - bulletsInClip;
        int bulletsToSub = (bulletsLeft >= bulletsToLoad) ? bulletsToLoad : bulletsLeft;
        bulletsLeft -= bulletsToSub;
        bulletsInClip += bulletsToLoad;
    }
    protected virtual void Recoil()
    {
        RecoilAfect.Recoil(recoil);
        RecoilAfect.SpreadRecoil(SprayShoot);
    }
    public virtual void OnDraw()
    {
        audiosource.PlayOneShot(Draw);
    }
    public virtual void OnMagOut()
    {
        audiosource.PlayOneShot(MagOutSound);
    }
    public virtual void OnMagIn()
    {
        ReloadAmmo();
        Debug.Log("hola");
        audiosource.PlayOneShot(MagInSound);
    }
    public virtual void OnBoltForwarded()
    {

        audiosource.PlayOneShot(boltSound);
        CancelInvoke("resetReloading");
        Debug.Log("Recargando");
        Invoke("resetReloading", 1f);// invoke por probar 
    }
    protected void resetReloading()
    {
        isReloading = false;

    }
    //string getWeaponName() 
    //{ 
    //    string name = ""; 
    //    if (this is SlideStopWeapon) 
    //    { 
    //        name = "UMP45"; 
    //    } 
    //    else if (this is Escopeta){ 
    //        name = "DefenderShoutgun"; 
    //    } 
    //    return name; 
    //} 
    public void ConfigurationType()
    {

        switch (TipoDeArma)
        {

            case Tipo.Pistola:
                type = 0;

                break;
            case Tipo.RifleSemiAutomatico:
                type = 1;

                break;

            case Tipo.Rifle:
                type = 2;

                break;
            case Tipo.Escopeta:
                type = 3;
                break;
            case Tipo.RifleFrancoTirador:
                type = 4;
                break;

        }
    }
    public void AsingConfigurations()
    {
        SpraySystem.penalizationCrounch = SprayConf.PenalizationCrounch;
        SpraySystem.penalizationGrounded = SprayConf.PenalizationGrounded;
        SpraySystem.penalizationMoving = SprayConf.PenalizationMoving;
        //  RecoilAfect.spreadRecoil = SprayConf.RecoilSpray; 
        RecoilAfect.maxSpreadRecoil = SprayConf.maxRecoilSpray;
        RecoilAfect.enfriamiento = SprayConf.Enfriamiento;
        SprayShoot = SprayConf.SprayShoot;
        spreatBase = SprayConf.SprayBase;
        SpraySystem.PenalizationSniper = SprayConf.Sniper;
    }
    /// <summary>
    /// No Animation Connecting
    /// </summary>
    public virtual void OnSwich(){
        //Debug.Log("Cambio de Arma My name is:" +name);
       // AsingConfigurations();
    }
}

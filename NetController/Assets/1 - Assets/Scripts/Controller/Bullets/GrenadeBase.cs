using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeBase : MonoBehaviour {
    #region vars
    protected AudioSource sonido;
    public LayerMask GrenadeLayer;
    public AudioClip click_Grenade;
    public AudioClip click_GrenadeUp;
    [Header("Referencias")]
    protected Animator anim;
    public GameObject Proyectile;
    public Rigidbody Physics_Proyectile;
    public int maxGrenade;
    public int GrenadeinInventory;
    public float damage;
    public GameObject InstantiatePoint;
    public float fireRate;
    private bool fireLock;
    private bool GrenadeEnable;
    public Vector3 Forces;
    private float contador;
    #endregion
    void Awake () {
        GrenadeEnable = false;
        anim = GetComponent<Animator>();
        sonido = GetComponent<AudioSource>();
        StartCoroutine(ActivarGrenade());
        Physics_Proyectile = Proyectile.GetComponent<Rigidbody>();
        Physics_Proyectile.isKinematic = true;
        contador = 0;
	}
	
	
	void Update () {
        CheckFire();
	}

    public void CheckFire()
    {
        if (fireLock) return;
        if (!GrenadeEnable) return;
        contador = contador > 1 ? contador = 1 : contador;
        Fire();
    }

   public  void Fire()
    {
        
        if (Input.GetMouseButton(0))
        {
            //animation
            contador = 1f;
        }
        else if (Input.GetMouseButton(1))
        {
            //animation
            contador += Time.fixedDeltaTime;
        }
        if (Input.GetMouseButtonUp(0))
        {
            GrenadeEnable = false;
            contador = 0;
            //animation
            StartCoroutine(GrenadeRate());
        }
       
    }
    #region virtualMet
    public virtual void ForcesProyectile(GameObject grenade)
    {
        // Solo fuerzas
        Physics_Proyectile.isKinematic = false;
        Rigidbody rb = grenade.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward*10, ForceMode.Impulse);
    }

    public virtual void explosionEffect(Transform grenade) //
    {
        Vector3 position = grenade.transform.position;

    }
    #endregion
    #region AnimationsEvents
    public void OnGrenadeIstantiate() // animation event
    {
       GameObject grenade= Instantiate(Proyectile, transform.parent = null);
        ForcesProyectile(grenade);
        StartCoroutine(ExplosionCorrutine(grenade.transform));
    }
#endregion
    #region corrutines
    IEnumerator ActivarGrenade()
    {
        yield return new WaitForSeconds(1f);
        GrenadeEnable = true;
    }
    IEnumerator GrenadeRate()
    {
        yield return new WaitForSeconds(1.5f);
        GrenadeEnable = true;
    }
    IEnumerator ExplosionCorrutine(Transform grenade)
    {
        yield return new WaitForSeconds(2f);
        explosionEffect(grenade);
    }
#endregion
}

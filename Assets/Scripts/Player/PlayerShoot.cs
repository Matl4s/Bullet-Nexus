using System.Collections;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
   public float bulletSpeed;
   public float fireRate, bulletDamage;
   public bool isAuto;

   public Transform bulletSpawnTransform;
   public GameObject bulletPrefab;
    public GameObject MuzzleFlash;
    public Light MuzzleFlashLight;

   private float timer;

   public int currentClip=12;
   public int clipSize=12;
   public int currentAmmo=48;
   public int maxAmmo=48;

    void Start()
    {
        MuzzleFlash.SetActive(false);
    }

    private void Update()
   {
    if(timer>0)
    {
        timer -= Time.deltaTime / fireRate;
    }
    if (isAuto)
    {
        if(Input.GetButton("Fire1")&& timer <= 0)
        {
            Shoot();
        }
    }
    else
    {
        if(Input.GetButtonDown("Fire1") && timer <= 0)
        {
            Shoot();
        }
    }

    if(Input.GetKeyDown(KeyCode.R))
    {
        Reload();
    }
   }
   void Shoot()
   {
    if(currentClip>0)
    {   
        StartCoroutine(FlashMuzzleSprite());
        StartCoroutine(FlashMuzzleLight());
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnTransform.position, Quaternion.identity, GameObject.FindGameObjectWithTag("WorldObjectHolder").transform);
        bullet.GetComponent<Rigidbody>().AddForce(bulletSpawnTransform.forward * bulletSpeed, ForceMode.Impulse);
        bullet.GetComponent<BulletController>().damage = bulletDamage;
        currentClip--;
        timer = 1;
        }
   }
   
   private IEnumerator FlashMuzzleLight()
   {
        MuzzleFlashLight.enabled = true;
        yield return new WaitForSeconds(0.05f);
        MuzzleFlashLight.enabled = false;
   }

       private IEnumerator FlashMuzzleSprite()
    {
        MuzzleFlash.SetActive(true);
        yield return new WaitForSeconds(0.05f);
        MuzzleFlash.SetActive(false);
    }

    public void Reload()
    {
        int reloadAmount = clipSize - currentClip;
        reloadAmount = (currentAmmo - reloadAmount) >=0 ? reloadAmount : currentAmmo;
        currentClip += reloadAmount;
        currentAmmo -= reloadAmount;
    }
    
}

using System.Collections;
using UnityEngine;

public class HandGunShooting : MonoBehaviour
{
    [SerializeField] AudioSource gunShot;
    [SerializeField] GameObject handgun;
    [SerializeField] bool canFire = true;

    public PlayerShoot playerShoot;

    void Update()
    {
            if (Input.GetMouseButton(0))
            {
                if(playerShoot.currentClip>0)
                {
                    if(canFire == true)
                    {
                        canFire = false;
                        StartCoroutine(FiringGun()); 
                    }
                }
            }
    }

    //Jak se deklaruje co routine (ko rutina) a FiringGun je coroutine
    //coroutine - funkce, která může pozastavit svou exekuci, dokud daná instrukce neskončí
    IEnumerator FiringGun()
    {
        gunShot.Play();
        handgun.GetComponent<Animator>().Play("HandgunShoot");
        yield return new WaitForSeconds(0.5f);
        //0.5 float
        handgun.GetComponent<Animator>().Play("New State");
        canFire = true;
    }
}

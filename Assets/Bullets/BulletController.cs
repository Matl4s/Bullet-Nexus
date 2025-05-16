using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float damage;
    public float lifeTime = 3;

    private void Update()
    {
        lifeTime -= Time.deltaTime;
        if(lifeTime <0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            if(other.GetComponent<Enemy>() != null)
                other.GetComponent<Enemy>().health -= damage ;
            Destroy(gameObject);
        }
        if (other.CompareTag("Untagged"))
        {
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }



}

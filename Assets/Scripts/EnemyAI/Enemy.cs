using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health;

    private void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}

using UnityEngine;
using UnityEngine.AI;


public class EnemyAnimation : MonoBehaviour
{
    public NavMeshAgent agent;
    Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("Speed",agent.velocity.magnitude);
    }
}

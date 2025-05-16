using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBrain : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask whatIsGround, whatIsPlayer, whatIsVisible;

    //patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public GameObject projectile;
    public Transform BulletSpawnTransform;
    public GameObject MuzzleFlash;
    public Light MuzzleFlashLight;

    //states
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    //audio
    public AudioSource GunShotSound;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        //Checking for sight and range
        playerInSightRange = Physics.CheckSphere(transform.position,sightRange,whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position,attackRange,whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange) Patroling();
        if (playerInSightRange && !playerInAttackRange) ChasePLayer();
        if (playerInSightRange && playerInAttackRange) AttackPlayer();
    }

    

    private void SearchWalkPoint()
    {
        // calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
    
        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
        {
            walkPointSet = true;
        }
    }
    private void Patroling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
        {
            agent.SetDestination(walkPoint);
        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //walkpoint reached
        if (distanceToWalkPoint.magnitude <1f)
        {
            walkPointSet = false;
        }

        
    }

    private void ChasePLayer()
    {
        agent.SetDestination(player.position);
    }


    private void ResetAttack()
    {
        alreadyAttacked=false;
    }
    private void AttackPlayer()
    {
        agent.SetDestination(transform.position);
        transform.LookAt(player);
        if (!alreadyAttacked)
        {

            //raycast aby se zjistilo, jestli se nenachází mezí hráčem a AI zeď nebo cokoliv jiného, co by mohlo blokovat střelbu
            Vector3 directionY = (player.position - BulletSpawnTransform.position).normalized;
            float distanceToPlayer = Vector3.Distance(BulletSpawnTransform.position,player.position);
            RaycastHit hit;

            if (Physics.Raycast(BulletSpawnTransform.position,directionY,out hit,distanceToPlayer,whatIsVisible))
            {
                if (hit.transform.CompareTag("Player"))
                {
                    StartCoroutine(FlashMuzzleSprite());
                    StartCoroutine(FlashMuzzleLight());
                    GunShotSound.Play();
                    //attack
                    Rigidbody rb = Instantiate(projectile, BulletSpawnTransform.position, Quaternion.identity).GetComponent<Rigidbody>();
                    Vector3 direction = (player.position - BulletSpawnTransform.position).normalized;
                    rb.AddForce(direction * 32f, ForceMode.Impulse);

                    alreadyAttacked = true;
                    Invoke(nameof(ResetAttack), timeBetweenAttacks);
                }
            }
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
}

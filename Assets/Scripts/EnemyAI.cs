using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyAI : MonoBehaviour
{
    // 3 State: patrolling(), chasing(), attacking();
          
    public NavMeshAgent botAI;
    public Transform playerPos;   
    public LayerMask whatIsGround, whatIsPlayer;
    bool playerInRange;
    public bool beAttacked;

    //Patroling()
    [SerializeField] float defaultWatchRange;
    [SerializeField] float walkingRange;
    [SerializeField] int speedChasing;
    private float rangeOfWatch;
    private bool isWalkPointSet;
    private Vector3 newDes;
    private bool beDizzy;


    private void Start()
    {
        botAI = gameObject.GetComponent<NavMeshAgent>();
        beAttacked = false;
        botAI.speed = speedChasing;
    }

    private void Update()
    {
        if(GameManager.gameManager.isPlaying)
        {
            //CheckSphere let object detect layer in a decent range
            playerInRange = Physics.CheckSphere(transform.position, rangeOfWatch, whatIsPlayer);

            if (!playerInRange)
            {
                botAI.speed = speedChasing;
                Patroling();
            }
            if (playerInRange && !beDizzy)
            {
                botAI.speed = speedChasing * 1.5f;
                Chasing();
            }
            if (beAttacked)
            {
                StartCoroutine(BeAttackedCD());
                beAttacked = false;
            }
        }
    }



    private void Patroling()
    {
        botAI.autoBraking = false;

        rangeOfWatch = defaultWatchRange;

        if (!isWalkPointSet) CreateNewDes();

        if (isWalkPointSet) botAI.SetDestination(newDes);

        Vector3 walkingDistance = transform.position - newDes;

        if (walkingDistance.magnitude < 1f)
            isWalkPointSet = false;
    }

    private void CreateNewDes()
    {
        rangeOfWatch = defaultWatchRange * 1.5f;

        float randomX = Random.Range(-walkingRange, walkingRange);
        float randomZ = Random.Range(-walkingRange, walkingRange);

        newDes = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        //Raycast allow object detect another layer object that not include in parameter
        //in a decent range
        if (Physics.Raycast(newDes, -transform.up, 2f, whatIsGround))
            isWalkPointSet = true;
    }

    private void Chasing()
    {
        rangeOfWatch = defaultWatchRange * 1.5f;
        botAI.autoBraking = true;
        botAI.SetDestination(playerPos.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            GameManager.gameManager.isPlaying = false;
            Object.Destroy(other.gameObject);
        }

            
    }

    IEnumerator BeAttackedCD()
    {
        float temp = botAI.speed;
        beDizzy = true;
        botAI.speed = 0;
        yield return new WaitForSeconds(3);
        botAI.speed = temp;
        beDizzy = false;
    }
}

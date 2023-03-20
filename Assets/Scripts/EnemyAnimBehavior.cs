using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimBehavior : MonoBehaviour
{
    public enum FSMStates
    {
        Idle,
        Patrol,
        Chase,
        Attack,
        Dead
    }

    public FSMStates currentState;

    public float attackDistance = 2;
    public float chaseDistance = 6;
    public float enemySpeed = 5;
    public GameObject player;
    public AudioClip goblinDeathSFX;
    public GameObject weaponTip;
    public static int enemyCount = 0;

    GameObject[] wanderPoints;
    Vector3 nextDestination;
    Animator anim;
    float distanceToPlayer;
    float elapsedTime = 0;
    bool isAlive = true;

    // Start is called before the first frame update
    void Start()
    {
        enemyCount++;
        wanderPoints = GameObject.FindGameObjectsWithTag("WanderPoint");
        DisableWeaponTip();
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        if (isAlive)
        {
            distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
            switch (currentState)
            {
                case FSMStates.Patrol:
                    UpdatePatrolState();
                    break;
                case FSMStates.Chase:
                    UpdateChaseState();
                    break;
                case FSMStates.Attack:
                    UpdateAttackState();
                    break;
                case FSMStates.Dead:
                    UpdateDeadState();
                    break;
            }

            elapsedTime += Time.deltaTime;
        }
    }

    private void Initialize()
    {
        currentState = FSMStates.Patrol;
        FindNextPoint();
    }

    void UpdatePatrolState()
    {
        anim.SetInteger("goblinState", 1);

        if (Vector3.Distance(transform.position, nextDestination) < 3)
        {
            FindNextPoint();
        }
        else if (distanceToPlayer <= chaseDistance)
        {
            currentState = FSMStates.Chase;
        }

        FaceTarget(nextDestination);

        /*
        transform.position = Vector3.MoveTowards
            (transform.position, nextDestination, enemySpeed * Time.deltaTime);*/

    }
    void UpdateChaseState()
    {
        anim.SetInteger("goblinState", 2);

        nextDestination = player.transform.position;

        if (distanceToPlayer <= attackDistance)
        {
            currentState = FSMStates.Attack;
        }
        else if (distanceToPlayer > chaseDistance)
        {
            currentState = FSMStates.Patrol;
        }

        FaceTarget(nextDestination);

        /*
        transform.position = Vector3.MoveTowards
            (transform.position, nextDestination, enemySpeed * Time.deltaTime);
        */
    }
    void UpdateAttackState()
    {
        nextDestination = player.transform.position;

        if (distanceToPlayer <= attackDistance)
        {
            currentState = FSMStates.Attack;
        }
        else if (distanceToPlayer > attackDistance && distanceToPlayer <= chaseDistance)
        {
            currentState = FSMStates.Chase;
        }
        else if (distanceToPlayer > chaseDistance)
        {
            currentState = FSMStates.Patrol;
        }

        FaceTarget(nextDestination);

        anim.SetInteger("goblinState", 3);

    }
    void UpdateDeadState()
    {
        isAlive = false;

        anim.SetInteger("goblinState", 4);

        AudioSource.PlayClipAtPoint(goblinDeathSFX, transform.position);

        Destroy(gameObject, anim.GetCurrentAnimatorStateInfo(0).length);

        currentState = FSMStates.Dead;
    }

    void FindNextPoint()
    {
        int randomIndex = Random.Range(0, wanderPoints.Length);
        nextDestination = wanderPoints[randomIndex].transform.position;
    }

    void FaceTarget(Vector3 target)
    {
        Vector3 directionToTarget = (target - transform.position).normalized;
        directionToTarget.y = 0;
        Quaternion lookRotation = Quaternion.LookRotation(directionToTarget);
        transform.rotation = Quaternion.Slerp
            (transform.rotation, lookRotation, 10 * Time.deltaTime);
    }

    private void OnDrawGizmos()
    {
        //attack
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackDistance);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, chaseDistance);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Sword") && isAlive)
        {
            UpdateDeadState();
        }
    }
    
    public void EnableWeaponTip()
    {
        weaponTip.SetActive(true);
    }

    public void DisableWeaponTip()
    {
        weaponTip.SetActive(false);
    }

    private void OnDestroy()
    {
        enemyCount--;
        if (enemyCount <= 0)
        {
            Debug.Log("You win!");
            LevelManager instance = LevelManager.instance;
            if (instance)
            {
                LevelManager.instance.LevelBeat();
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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
    public float maxHealth = 100f;
    public float currentHealth;
    public float attackDistance = 2;
    public float chaseDistance = 6;
    public GameObject player;
    public AudioClip goblinDeathSFX;
    public AudioClip goblinStabSFX;
    public GameObject bloodFX;
    public GameObject weaponTip;
    private MeshCollider weaponTipCollider;
    public static int enemyCount = 0;
    [SerializeField]
    private float patrolSpeed = 1f;
    [SerializeField]
    private float chaseSpeed = 3.5f;
    GameObject[] wanderPoints;
    Vector3 nextDestination;
    private NavMeshAgent agent;
    Animator anim;
    float distanceToPlayer;
    float elapsedTime = 0;
    bool isAlive = true;
    Transform eyes;
    [SerializeField]
    private float fieldOfView = 75f;

    void Start()
    {
        currentHealth = maxHealth;
        eyes = transform.GetChild(0);
        enemyCount++;
        wanderPoints = GameObject.FindGameObjectsWithTag("WanderPoint");
        weaponTipCollider = weaponTip.GetComponent<MeshCollider>();
        DisableWeaponTip();
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        Initialize();
        agent = GetComponent<NavMeshAgent>();
        if (wanderPoints.Length > 0)
        {
            currentState = FSMStates.Patrol;
            FindNextPoint();
        }
        else
        {
            currentState = FSMStates.Idle;
        }
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
        bool canSeePlayer = IsPlayerInClearFOV();

        if (Vector3.Distance(transform.position, nextDestination) < 3)
        {
            FindNextPoint();
        }
        else if (distanceToPlayer <= chaseDistance && canSeePlayer)
        {
            currentState = FSMStates.Chase;
        }

        FaceTarget(nextDestination);
        agent.SetDestination(nextDestination);
        agent.stoppingDistance = 0;
        agent.speed = patrolSpeed;
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
        agent.SetDestination(nextDestination);
        agent.stoppingDistance = attackDistance;
        agent.speed = chaseSpeed;
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

        if (elapsedTime >= 2.5)
        {
            AudioSource.PlayClipAtPoint(goblinStabSFX, transform.position);
            elapsedTime = 0;
        }

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
            currentHealth -= PlayerStats.instance.WeaponDamage;
            SpawnBloodFX(other.ClosestPointOnBounds(transform.position));
            if (currentHealth <= 0)
            {
                UpdateDeadState();
            }
            else
            {
                UpdateChaseState();
            }
        }
    }

    private void SpawnBloodFX(Vector3 point)
    {
        Instantiate(bloodFX, point, Quaternion.identity);
    }

    public void EnableWeaponTip()
    {
        weaponTipCollider.enabled = true;
    }

    public void DisableWeaponTip()
    {
        weaponTipCollider.enabled = false;
    }

    private bool IsPlayerInClearFOV()
    {
        Vector3 playerHeadPos = player.transform.position;
        playerHeadPos.y = eyes.position.y;
        Vector3 directionToPlayer = playerHeadPos - eyes.position;
        float angle = Vector3.Angle(directionToPlayer, eyes.forward);

        if (angle <= fieldOfView)
        {
            RaycastHit hit;
            if (Physics.Raycast(eyes.position, directionToPlayer, out hit, chaseDistance))
            {
                if (hit.collider.CompareTag("Player"))
                {
                    return true;
                }
            }
        }
        return false;
    }

    private void OnDestroy()
    {
        enemyCount--;
    }
}

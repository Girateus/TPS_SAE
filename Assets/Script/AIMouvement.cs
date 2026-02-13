using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class AIMouvement : MonoBehaviour
{
    [SerializeField] private UnityEvent _Death;
    [Header("Configurations")]
    public Transform target;
    public float patrolSpeed = 2f;
    public float chaseSpeed = 5f;
    public float visionRange = 10f;
    public float visionAngle = 45f; 

    [Header("Patrole")]
    public float patrolRadius = 10f;
    private Vector3 _patrolTarget;

    private NavMeshAgent _agent;
    private Animator _animator;
    public bool _isChasing = false;
    
    AudioManager _audioManager;
    
    private void Awake()
    {
        _audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    
    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _agent.speed = patrolSpeed;
        SetRandomPatrolPoint();
    }

    private void Update()
    {
        CheckLineOfSight();

        if (_isChasing)
        {
            _agent.destination = target.position;
            _agent.speed = chaseSpeed;
        }
        else
        {
            _agent.speed = patrolSpeed;
            if (!_agent.pathPending && _agent.remainingDistance < 0.5f)
            {
                SetRandomPatrolPoint();
            }
        }
        
        _animator.SetFloat("Velocity", _agent.velocity.magnitude);
        _animator.SetBool("IsChasing", _isChasing);
        
    }

   private void CheckLineOfSight()
    {
        Vector3 directionToTarget = (target.position - transform.position).normalized;
        float distanceToTarget = Vector3.Distance(transform.position, target.position);
    
        bool canSeePlayer = false;

        if (distanceToTarget < visionRange)
        {
            float angle = Vector3.Angle(transform.forward, directionToTarget);

            if (angle < visionAngle)
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position + Vector3.up, directionToTarget, out hit, visionRange))
                {
                    if (hit.transform == target)
                    {
                        canSeePlayer = true;
                    }
                }
            }
        }
        
        if (canSeePlayer && !_isChasing)
        {
            _audioManager.PlaySound(_audioManager.PlayerDetected);
            _isChasing = true;
        }
        
        else if (!canSeePlayer && _isChasing)
        {
            _isChasing = false;
        }
    }

    private void SetRandomPatrolPoint()
    {
        Vector3 randomDirection = Random.insideUnitSphere * patrolRadius;
        randomDirection += transform.position;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection, out hit, patrolRadius, 1);
        _patrolTarget = hit.position;
        _agent.destination = _patrolTarget;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.GetComponent<PlayerController>() != null)
        {
            _audioManager.PlaySound(_audioManager.GameOver);
            _Death.Invoke();
            Time.timeScale = 1f;
            
        }
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, visionRange);
        Vector3 leftBoundary = Quaternion.Euler(0, -visionAngle, 0) * transform.forward;
        Vector3 rightBoundary = Quaternion.Euler(0, visionAngle, 0) * transform.forward;
        Gizmos.DrawRay(transform.position, leftBoundary * visionRange);
        Gizmos.DrawRay(transform.position, rightBoundary * visionRange);
    }
}
using UnityEngine;
using UnityEngine.AI;

public class AIMouvement : MonoBehaviour
{
    [Header("Configurations")]
    public Transform target;
    public float patrolSpeed = 2f;
    public float chaseSpeed = 5f;
    public float visionRange = 10f;
    public float visionAngle = 45f; // Angle de vue total (ex: 45° de chaque côté)

    [Header("Patrouille")]
    public float patrolRadius = 10f;
    private Vector3 _patrolTarget;

    private NavMeshAgent _agent;
    private Animator _animator;
    private bool _isChasing = false;

    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _agent.speed = patrolSpeed;
        SetRandomPatrolPoint();
    }

    void Update()
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
            // Si l'agent arrive proche de son point de patrouille, on en cherche un autre
            if (!_agent.pathPending && _agent.remainingDistance < 0.5f)
            {
                SetRandomPatrolPoint();
            }
        }

        // Mise à jour de l'Animator
        _animator.SetFloat("Velocity", _agent.velocity.magnitude);
        _animator.SetBool("IsChasing", _isChasing);
    }

    void CheckLineOfSight()
    {
        Vector3 directionToTarget = (target.position - transform.position).normalized;
        float distanceToTarget = Vector3.Distance(transform.position, target.position);

        // 1. Vérifier la distance
        if (distanceToTarget < visionRange)
        {
            // 2. Vérifier l'angle (Ligne de vue)
            float angle = Vector3.Angle(transform.forward, directionToTarget);

            if (angle < visionAngle)
            {
                // 3. Raycast pour vérifier les obstacles (murs, etc.)
                RaycastHit hit;
                if (Physics.Raycast(transform.position + Vector3.up, directionToTarget, out hit, visionRange))
                {
                    if (hit.transform == target)
                    {
                        _isChasing = true;
                        return;
                    }
                }
            }
        }
        
        // Si on perd le joueur de vue (optionnel : tu peux ajouter un timer avant d'abandonner)
        _isChasing = false;
    }

    void SetRandomPatrolPoint()
    {
        Vector3 randomDirection = Random.insideUnitSphere * patrolRadius;
        randomDirection += transform.position;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection, out hit, patrolRadius, 1);
        _patrolTarget = hit.position;
        _agent.destination = _patrolTarget;
    }

    // Pour visualiser la vision dans l'éditeur
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
using UnityEngine;

public class BruteBot : MonoBehaviour
{
    public float detectionRadius = 15f; // Vision range
    public LayerMask enemyLayer;
    public LayerMask powerUpLayer;
    public Transform target;  // Current attack target
    public Transform powerUp; // Current power-up target

    public float speed = 5f;
    public float attackRange = 2f;
    public float attackCooldown = 1.5f;

    private float nextAttackTime = 0f;

    void Update()
    {
        EvaluateSurroundings();

        if (powerUp != null) // Collect power-ups
        {
            MoveTo(powerUp.position);
        }
        else if (target != null && Vector3.Distance(transform.position, target.position) <= attackRange) // Attack enemies
        {
            TryAttack();
        }
        else if (target != null) // Chase enemies
        {
            MoveTo(target.position);
        }
        else
        {
            Wander(); // Random roaming
        }
    }

    void EvaluateSurroundings()
    {
        // Detect power-ups
        Collider[] powerUps = Physics.OverlapSphere(transform.position, detectionRadius, powerUpLayer);
        powerUp = powerUps.Length > 0 ? powerUps[0].transform : null;

        // Detect enemies
        Collider[] enemies = Physics.OverlapSphere(transform.position, detectionRadius, enemyLayer);
        target = enemies.Length > 0 ? enemies[0].transform : null;
    }

    void MoveTo(Vector3 destination)
    {
        Vector3 direction = (destination - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;
        RotateTowards(destination);
    }

    void RotateTowards(Vector3 targetPos)
    {
        Vector3 direction = (targetPos - transform.position).normalized;
        float angle = Mathf.Atan2(direction.z, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, -angle, 0);
    }

    void TryAttack()
    {
        if (Time.time >= nextAttackTime)
        {
            nextAttackTime = Time.time + attackCooldown;
            Debug.Log("Attacking: " + target.name);
            // Simulate damage (e.g., reduce target's health)
            // Add attack animations here if needed
        }
    }

    void Wander()
    {
        Vector3 randomDirection = Random.insideUnitSphere * detectionRadius;
        randomDirection.y = 0; // Stay on ground
        MoveTo(transform.position + randomDirection);
    }
}

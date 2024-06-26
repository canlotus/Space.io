using System.Collections;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float speed = 5.0f; // Hareket h�z�
    public float detectionRange = 10.0f; // Tespit menzili
    public float minStoppingDistance = 8.0f; // Minimum takip mesafesi
    public float maxStoppingDistance = 15.0f; // Maksimum takip mesafesi
    public float evadeSpeed = 3.0f; // Ka�ma h�z�
    public float rotationSpeed = 5.0f; // Rotasyon h�z�
    public float targetCheckInterval = 0.5f; // Hedef kontrol aral���

    private Transform target;
    private EnemyAttack enemyAttack;
    private bool isEvading;

    void Start()
    {
        StartCoroutine(CheckForTargetRoutine());
        enemyAttack = GetComponentInChildren<EnemyAttack>();
        isEvading = false;
    }

    void Update()
    {
        if (target == null)
        {
            // Hedef yoksa yeni hedef arar
            FindNewTarget();
            if (target == null)
            {
                // Hala hedef yoksa rastgele hareket et
                MoveRandomly();
            }
            return;
        }

        // Hedefe olan uzakl��� hesapla
        float distanceToTarget = Vector2.Distance(transform.position, target.position);

        if (distanceToTarget <= detectionRange)
        {
            if (!isEvading)
            {
                if (distanceToTarget > minStoppingDistance)
                {
                    // Hedefe do�ru d�n
                    Vector3 direction = (target.position - transform.position).normalized;
                    transform.up = Vector3.Lerp(transform.up, direction, rotationSpeed * Time.deltaTime);

                    // Hedefe do�ru hareket et
                    transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
                }
                else if (distanceToTarget <= minStoppingDistance)
                {
                    // Ka�ma moduna ge�
                    StartCoroutine(Evade());
                }

                // EnemyAttack scriptini ate� etmesi i�in tetikle
                if (enemyAttack != null)
                {
                    enemyAttack.Attack();
                }
            }
        }

        // S�n�rlar� kontrol et
        ClampPosition();
    }

    private IEnumerator CheckForTargetRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(targetCheckInterval);
            FindNewTarget();
        }
    }

    private void FindNewTarget()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, detectionRange);
        float closestDistance = detectionRange;
        Transform closestTarget = null;

        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Player") || collider.CompareTag("Enemy"))
            {
                float distance = Vector2.Distance(transform.position, collider.transform.position);
                if (distance < closestDistance && collider.transform != transform)
                {
                    closestDistance = distance;
                    closestTarget = collider.transform;
                }
            }
        }

        if (closestTarget != null)
        {
            target = closestTarget;
        }
        else
        {
            target = null;
        }
    }

    private void MoveRandomly()
    {
        // Mevcut pozisyonu al
        Vector2 currentPosition = transform.position;

        // Rastgele bir hedef pozisyon belirle
        Vector2 randomTargetPosition = new Vector2(
            Random.Range(-100f, 100f), // x s�n�rlar�
            Random.Range(-100f, 100f)  // y s�n�rlar�
        );

        // Hedefe do�ru y�nelme vekt�r� hesapla
        Vector2 direction = (randomTargetPosition - currentPosition).normalized;

        // Yeni pozisyonu belirle
        Vector2 movePosition = currentPosition + direction * speed * Time.deltaTime;

        // S�n�rlar� kontrol et ve ge�erli konum belirle
        Vector2 clampedPosition = new Vector2(
            Mathf.Clamp(movePosition.x, -100f, 100f),
            Mathf.Clamp(movePosition.y, -100f, 100f)
        );

        // Yeni pozisyona do�ru hareket et
        transform.position = Vector2.MoveTowards(currentPosition, clampedPosition, speed * Time.deltaTime);

        // Hedefe do�ru y�nelme
        Vector3 targetDirection = (Vector3)direction;
        transform.up = Vector3.Lerp(transform.up, targetDirection, rotationSpeed * Time.deltaTime);
    }

    private IEnumerator Evade()
    {
        isEvading = true;
        float timer = 0;
        Vector2 evadeOffset = Random.insideUnitCircle.normalized;

        while (timer < 1.0f)
        {
            // Hedefe do�ru d�n
            Vector3 direction = (target.position - transform.position).normalized;
            transform.up = Vector3.Lerp(transform.up, direction, rotationSpeed * Time.deltaTime);

            // Hedefin etraf�nda rastgele bir y�ne hareket et
            Vector2 evadePosition = (Vector2)target.position + evadeOffset * minStoppingDistance;
            transform.position = Vector2.MoveTowards(transform.position, evadePosition, evadeSpeed * Time.deltaTime);

            timer += Time.deltaTime;
            yield return null;
        }

        isEvading = false;
    }

    private void ClampPosition()
    {
        Vector3 clampedPosition = transform.position;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, -100f, 100f);
        clampedPosition.y = Mathf.Clamp(clampedPosition.y, -100f, 100f);
        transform.position = clampedPosition;
    }
}
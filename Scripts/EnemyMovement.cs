using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 5.0f; // Hareket h�z�
    public float rotationSpeed = 5.0f; // Rotasyon h�z�
    public float evadeSpeed = 3.0f; // Ka�ma h�z�

    public void MoveTowards(Vector3 targetPosition)
    {
        // Hedefe do�ru d�n
        Vector3 direction = (targetPosition - transform.position).normalized;
        transform.up = Vector3.Lerp(transform.up, direction, rotationSpeed * Time.deltaTime);

        // Hedefe do�ru hareket et
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
    }

    public void MoveRandomly(float minX, float maxX, float minY, float maxY)
    {
        // Rastgele bir hedef pozisyon belirle
        Vector2 randomTargetPosition = new Vector2(
            Random.Range(minX, maxX),
            Random.Range(minY, maxY)
        );

        // Mevcut pozisyonu al
        Vector2 currentPosition = transform.position;

        // Hedefe do�ru y�nelme vekt�r� hesapla
        Vector2 direction = (randomTargetPosition - currentPosition).normalized;

        // Yeni pozisyonu belirle
        Vector2 movePosition = currentPosition + direction * speed * Time.deltaTime;

        // S�n�rlar� kontrol et ve ge�erli konum belirle
        Vector2 clampedPosition = new Vector2(
            Mathf.Clamp(movePosition.x, minX, maxX),
            Mathf.Clamp(movePosition.y, minY, maxY)
        );

        // Yeni pozisyona do�ru hareket et
        transform.position = Vector2.MoveTowards(currentPosition, clampedPosition, speed * Time.deltaTime);

        // Hedefe do�ru y�nelme
        Vector3 targetDirection = (Vector3)direction;
        transform.up = Vector3.Lerp(transform.up, targetDirection, rotationSpeed * Time.deltaTime);
    }
}

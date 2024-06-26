using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public float attackCooldown = 0.7f; // Sald�r� bekleme s�resi
    private float attackTimer;
    public Transform[] firePoints; // Ate� etme noktalar�
    public GameObject bulletPrefab; // Kur�un prefab�

    void Start()
    {
        attackTimer = 0f; // �lk at�� i�in bekleme s�resini s�f�rl�yoruz
    }

    void Update()
    {
        if (attackTimer > 0)
        {
            attackTimer -= Time.deltaTime;
        }
    }

    public void Attack()
    {
        if (attackTimer <= 0)
        {
            foreach (Transform firePoint in firePoints)
            {
                Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            }
            attackTimer = attackCooldown;
        }
    }
}
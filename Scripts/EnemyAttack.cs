using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public float attackCooldown = 0.7f; // Saldýrý bekleme süresi
    private float attackTimer;
    public Transform[] firePoints; // Ateþ etme noktalarý
    public GameObject bulletPrefab; // Kurþun prefabý

    void Start()
    {
        attackTimer = 0f; // Ýlk atýþ için bekleme süresini sýfýrlýyoruz
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
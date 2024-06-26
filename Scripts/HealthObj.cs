using UnityEngine;

public class HealthObj : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public int healAmount = 300; // Player veya d��man yok edildi�inde bu kadar sa�l�k eklenir veya d���r�l�r
    public HealthSpawn spawner; // Spawner referans�

    void Start()
    {
        currentHealth = maxHealth;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.RestoreHealth(healAmount); // Player'a sa�l�k ekle
            }
            spawner.RemoveObject(gameObject); // Nesneyi yok et ve spawn i�lemini tekrar ba�lat
        }
        else if (collision.CompareTag("Enemy"))
        {
            EnemyHealth enemyHealth = collision.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.RestoreHealth(healAmount); // D��mana sa�l�k ekle
            }
            spawner.RemoveObject(gameObject); // Nesneyi yok et ve spawn i�lemini tekrar ba�lat
        }
    }
}
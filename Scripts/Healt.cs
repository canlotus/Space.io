using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public int expValue = 5; // Bu nesnenin verdiði deneyim puaný

    void Start()
    {
        currentHealth = maxHealth;
    }

    void Update()
    {
        // Can barýný güncellemek için bir þey yapmaya gerek yok
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            TakeDamage(100); // Örnek olarak 100 hasar alýyoruz
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if (currentHealth <= 0)
        {
            Spawner spawner = FindObjectOfType<Spawner>(); // Spawner'ý bul
            if (spawner != null)
            {
                spawner.RemoveObject(gameObject); // Bu Health bileþenine sahip nesneyi kaldýr
            }
            Destroy(gameObject); // Bu Health bileþenine sahip nesneyi yok et
        }
    }

    public void TakeDamage(int damage, bool isPlayerBullet)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if (currentHealth <= 0)
        {
            Spawner spawner = FindObjectOfType<Spawner>(); // Spawner'ý bul
            if (spawner != null)
            {
                spawner.RemoveObject(gameObject); // Bu Health bileþenine sahip nesneyi kaldýr
            }
            if (isPlayerBullet)
            {
                PlayerHealth playerHealth = FindObjectOfType<PlayerHealth>();
                if (playerHealth != null)
                {
                    playerHealth.GetComponent<ExperienceSystem>().GainXP(expValue);
                }
            }
            Destroy(gameObject); // Bu Health bileþenine sahip nesneyi yok et
        }
    }
}
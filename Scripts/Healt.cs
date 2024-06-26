using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public int expValue = 5; // Bu nesnenin verdi�i deneyim puan�

    void Start()
    {
        currentHealth = maxHealth;
    }

    void Update()
    {
        // Can bar�n� g�ncellemek i�in bir �ey yapmaya gerek yok
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            TakeDamage(100); // �rnek olarak 100 hasar al�yoruz
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if (currentHealth <= 0)
        {
            Spawner spawner = FindObjectOfType<Spawner>(); // Spawner'� bul
            if (spawner != null)
            {
                spawner.RemoveObject(gameObject); // Bu Health bile�enine sahip nesneyi kald�r
            }
            Destroy(gameObject); // Bu Health bile�enine sahip nesneyi yok et
        }
    }

    public void TakeDamage(int damage, bool isPlayerBullet)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if (currentHealth <= 0)
        {
            Spawner spawner = FindObjectOfType<Spawner>(); // Spawner'� bul
            if (spawner != null)
            {
                spawner.RemoveObject(gameObject); // Bu Health bile�enine sahip nesneyi kald�r
            }
            if (isPlayerBullet)
            {
                PlayerHealth playerHealth = FindObjectOfType<PlayerHealth>();
                if (playerHealth != null)
                {
                    playerHealth.GetComponent<ExperienceSystem>().GainXP(expValue);
                }
            }
            Destroy(gameObject); // Bu Health bile�enine sahip nesneyi yok et
        }
    }
}
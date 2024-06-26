using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    private float attackSpeed; // Düşmanın rastgele saldırı hızı
    private EnemySpawner enemySpawner;
    public GameObject enemyPrefab;
    public int expValue = 10; // Bu düşmanın verdiği deneyim puanı

    public Image healthBarForeground; // Sağlık barının dolu kısmı
    public Transform healthBarCanvas; // Sağlık barının Canvas'ı

    void Start()
    {
        currentHealth = maxHealth;

        // Düşmanın rastgele saldırı hızını belirle
        attackSpeed = Random.Range(7f, 100f); // Örnek aralık, istediğiniz aralığı buraya yazabilirsiniz

        // Düşmanın silahlarının saldırı hızını ayarla
        EnemyBullet[] bullets = GetComponentsInChildren<EnemyBullet>();
        foreach (EnemyBullet bullet in bullets)
        {
            bullet.speed = attackSpeed;
        }

        // EnemySpawner bileşenini bul
        enemySpawner = FindObjectOfType<EnemySpawner>();
        if (enemySpawner == null)
        {
            Debug.LogError("EnemySpawner component not found in the scene!");
        }
    }

    void Update()
    {
        // Sağlık barını düşmanın pozisyonuna göre güncelle
        UpdateHealthBarPosition();
    }

    void UpdateHealthBarPosition()
    {
        if (healthBarCanvas != null)
        {
            Vector3 healthBarPosition = transform.position + new Vector3(0, 1, 0); // Sağlık barını düşmanın üstünde tut
            healthBarCanvas.position = healthBarPosition;
            healthBarCanvas.rotation = Quaternion.identity; // Sağlık barının rotasyonunu sıfır yaparak düşmanın rotasyonundan bağımsız tut
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            TakeDamage(100); // Örnek olarak 100 hasar alıyoruz
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("HealthObj"))
        {
            HealthObj healthObj = collision.GetComponent<HealthObj>();
            if (healthObj != null)
            {
                RestoreHealth(healthObj.healAmount); // Düşmana sağlık ekle
                Destroy(collision.gameObject); // Sağlık objesini yok et
            }
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if (currentHealth <= 0)
        {
            if (enemySpawner != null && enemyPrefab != null)
            {
                enemySpawner.RemoveObject(gameObject, enemyPrefab); // enemyPrefab'ı parametre olarak geç
            }
            Destroy(gameObject); // Nesne yok oluyor
        }

        UpdateHealthBar(); // Hasar aldıktan sonra sağlık çubuğunu güncelle
    }

    public void TakeDamage(int damage, bool isPlayerBullet)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if (currentHealth <= 0)
        {
            if (enemySpawner != null && enemyPrefab != null)
            {
                enemySpawner.RemoveObject(gameObject, enemyPrefab); // enemyPrefab'ı parametre olarak geç
            }
            if (isPlayerBullet)
            {
                PlayerHealth playerHealth = FindObjectOfType<PlayerHealth>();
                if (playerHealth != null)
                {
                    playerHealth.GetComponent<ExperienceSystem>().GainXP(expValue);
                }
            }
            Destroy(gameObject); // Nesne yok oluyor
        }

        UpdateHealthBar(); // Hasar aldıktan sonra sağlık çubuğunu güncelle
    }

    public void RestoreHealth(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // Maksimum sağlık değerini aşmasını önle

        UpdateHealthBar(); // Sağlık ekledikten sonra sağlık çubuğunu güncelle
    }

    void UpdateHealthBar()
    {
        if (healthBarForeground != null)
        {
            healthBarForeground.fillAmount = (float)currentHealth / maxHealth; // Sağlık barını güncelle
        }
    }
}
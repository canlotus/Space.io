using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed; // Mermi h�z�
    public float lifetime = 2f; // Mermi ya�am s�resi
    public int damageToPlayer = 100; // Oyuncuya verilecek hasar
    public int damageToObjects = 100; // Nesnelere verilecek hasar

    void Start()
    {
        // Belirli bir s�re sonra mermiyi yok et
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        // Mermiyi ileri do�ru hareket ettir
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Mermi bir nesneye �arpt���nda
        if (collision.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damageToPlayer);
            }
            Destroy(gameObject); // Mermiyi yok et
        }
        else if (collision.CompareTag("Enemy"))
        {
            EnemyHealth enemyHealth = collision.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damageToObjects);
            }
            Destroy(gameObject); // Mermiyi yok et
        }
        else if (collision.CompareTag("Nesne"))
        {
            Health objectHealth = collision.GetComponent<Health>();
            if (objectHealth != null)
            {
                objectHealth.TakeDamage(damageToObjects);
            }
            Destroy(gameObject); // Mermiyi yok et
        }
        else
        {
            Destroy(gameObject); // E�er ba�ka bir �eyle �arp���rsa da mermiyi yok et
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public float speed = 15f; // Kurþunun hýzý
    public float lifetime = 2f; // Kurþunun yaþam süresi
    public int damage = 100; // Kurþunun verdiði hasar

    void Start()
    {
        // Belirli bir süre sonra kurþunu yok et
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        // Kurþunu ileri doðru hareket ettir
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Kurþun bir nesneye çarptýðýnda
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<EnemyHealth>().TakeDamage(damage, true); // isPlayerBullet true
            Destroy(gameObject); // Kurþunu yok et
        }
        else if (collision.gameObject.CompareTag("Nesne"))
        {
            collision.gameObject.GetComponent<Health>().TakeDamage(damage, true); // isPlayerBullet true
            Destroy(gameObject); // Kurþunu yok et
        }
    }
}
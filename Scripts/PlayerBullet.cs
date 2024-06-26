using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public float speed = 15f; // Kur�unun h�z�
    public float lifetime = 2f; // Kur�unun ya�am s�resi
    public int damage = 100; // Kur�unun verdi�i hasar

    void Start()
    {
        // Belirli bir s�re sonra kur�unu yok et
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        // Kur�unu ileri do�ru hareket ettir
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Kur�un bir nesneye �arpt���nda
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<EnemyHealth>().TakeDamage(damage, true); // isPlayerBullet true
            Destroy(gameObject); // Kur�unu yok et
        }
        else if (collision.gameObject.CompareTag("Nesne"))
        {
            collision.gameObject.GetComponent<Health>().TakeDamage(damage, true); // isPlayerBullet true
            Destroy(gameObject); // Kur�unu yok et
        }
    }
}
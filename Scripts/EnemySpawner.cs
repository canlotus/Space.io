using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] objectsToSpawn; // Spawnlanacak düþman nesneleri
    public Vector2 spawnAreaMin; // Spawn alanýnýn sol alt köþesi
    public Vector2 spawnAreaMax; // Spawn alanýnýn sað üst köþesi
    public int totalObjects = 10; // Toplam spawnlanacak düþman sayýsý
    public int minHealth = 1000; // Minimum can deðeri
    public int maxHealth = 2000; // Maximum can deðeri
    public float minAttackSpeed = 7f; // Minimum saldýrý hýzý
    public float maxAttackSpeed = 20f; // Maximum saldýrý hýzý
    public float minBulletSpeed = 7f; // Minimum mermi hýzý
    public float maxBulletSpeed = 100f; // Maximum mermi hýzý
    public float minMoveSpeed = 5f; // Minimum hareket hýzý
    public float maxMoveSpeed = 8f; // Maximum hareket hýzý
    private List<GameObject> spawnedObjects = new List<GameObject>();

    void Start()
    {
        // Her nesne türü için kaç adet spawnlanacaðýný hesapla
        int objectsPerType = totalObjects / objectsToSpawn.Length;

        for (int i = 0; i < objectsToSpawn.Length; i++)
        {
            for (int j = 0; j < objectsPerType; j++)
            {
                GameObject obj = objectsToSpawn[i];
                SetEnemyProperties(obj); // Düþmanýn özelliklerini ayarla
                SpawnObject(obj);
            }
        }

        // Eðer toplam nesne sayýsý eþit olarak daðýtýlamýyorsa, kalan nesneleri de ekleyin
        int remainingObjects = totalObjects % objectsToSpawn.Length;
        for (int i = 0; i < remainingObjects; i++)
        {
            GameObject obj = objectsToSpawn[i];
            SetEnemyProperties(obj); // Düþmanýn özelliklerini ayarla
            SpawnObject(obj);
        }
    }

    public void SpawnObject(GameObject obj)
    {
        Vector2 spawnPosition = new Vector2(
            Random.Range(spawnAreaMin.x, spawnAreaMax.x),
            Random.Range(spawnAreaMin.y, spawnAreaMax.y)
        );
        GameObject spawnedObj = Instantiate(obj, spawnPosition, Quaternion.identity);

        // Düþmanýn hareket hýzýný rastgele belirle
        EnemyAI enemyAI = spawnedObj.GetComponent<EnemyAI>();
        if (enemyAI != null)
        {
            enemyAI.speed = Random.Range(minMoveSpeed, maxMoveSpeed);
        }
        else
        {
            Debug.LogWarning("EnemyAI component not found on enemy prefab or its children!");
        }

        // Düþmanýn mermi hýzýný rastgele belirle
        EnemyBullet[] bullets = spawnedObj.GetComponentsInChildren<EnemyBullet>();
        foreach (EnemyBullet bullet in bullets)
        {
            bullet.speed = Random.Range(minBulletSpeed, maxBulletSpeed);
        }

        // EnemyHealth bileþeninde prefab bilgisini ayarla
        EnemyHealth enemyHealth = spawnedObj.GetComponent<EnemyHealth>();
        if (enemyHealth != null)
        {
            enemyHealth.enemyPrefab = obj;
        }
        else
        {
            Debug.LogWarning("EnemyHealth component not found on enemy prefab!");
        }

        spawnedObjects.Add(spawnedObj);
    }

    void SetEnemyProperties(GameObject enemy)
    {
        // EnemyHealth bileþenini al
        EnemyHealth healthComponent = enemy.GetComponent<EnemyHealth>();
        if (healthComponent != null)
        {
            // Rastgele can deðeri atamasý yap
            healthComponent.maxHealth = Random.Range(minHealth, maxHealth + 1);
            healthComponent.currentHealth = healthComponent.maxHealth;
            // Saldýrý hýzý zaten EnemyHealth içinde rastgele atanýyor, burada tekrar atamaya gerek yok
        }
        else
        {
            Debug.LogError("EnemyHealth component not found on enemy prefab!");
        }
    }

    public void RespawnObject(GameObject prefab)
    {
        StartCoroutine(RespawnCoroutine(prefab));
    }

    private IEnumerator RespawnCoroutine(GameObject prefab)
    {
        yield return new WaitForSeconds(1f); // 1 saniye bekle, sonra nesneyi tekrar spawn et
        SpawnObject(prefab);
    }

    public void RemoveObject(GameObject obj, GameObject prefab)
    {
        spawnedObjects.Remove(obj);
        Destroy(obj);

        if (spawnedObjects.Count < totalObjects)
        {
            RespawnObject(prefab);
        }
    }
}
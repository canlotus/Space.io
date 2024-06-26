using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] objectsToSpawn; // Spawnlanacak d��man nesneleri
    public Vector2 spawnAreaMin; // Spawn alan�n�n sol alt k��esi
    public Vector2 spawnAreaMax; // Spawn alan�n�n sa� �st k��esi
    public int totalObjects = 10; // Toplam spawnlanacak d��man say�s�
    public int minHealth = 1000; // Minimum can de�eri
    public int maxHealth = 2000; // Maximum can de�eri
    public float minAttackSpeed = 7f; // Minimum sald�r� h�z�
    public float maxAttackSpeed = 20f; // Maximum sald�r� h�z�
    public float minBulletSpeed = 7f; // Minimum mermi h�z�
    public float maxBulletSpeed = 100f; // Maximum mermi h�z�
    public float minMoveSpeed = 5f; // Minimum hareket h�z�
    public float maxMoveSpeed = 8f; // Maximum hareket h�z�
    private List<GameObject> spawnedObjects = new List<GameObject>();

    void Start()
    {
        // Her nesne t�r� i�in ka� adet spawnlanaca��n� hesapla
        int objectsPerType = totalObjects / objectsToSpawn.Length;

        for (int i = 0; i < objectsToSpawn.Length; i++)
        {
            for (int j = 0; j < objectsPerType; j++)
            {
                GameObject obj = objectsToSpawn[i];
                SetEnemyProperties(obj); // D��man�n �zelliklerini ayarla
                SpawnObject(obj);
            }
        }

        // E�er toplam nesne say�s� e�it olarak da��t�lam�yorsa, kalan nesneleri de ekleyin
        int remainingObjects = totalObjects % objectsToSpawn.Length;
        for (int i = 0; i < remainingObjects; i++)
        {
            GameObject obj = objectsToSpawn[i];
            SetEnemyProperties(obj); // D��man�n �zelliklerini ayarla
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

        // D��man�n hareket h�z�n� rastgele belirle
        EnemyAI enemyAI = spawnedObj.GetComponent<EnemyAI>();
        if (enemyAI != null)
        {
            enemyAI.speed = Random.Range(minMoveSpeed, maxMoveSpeed);
        }
        else
        {
            Debug.LogWarning("EnemyAI component not found on enemy prefab or its children!");
        }

        // D��man�n mermi h�z�n� rastgele belirle
        EnemyBullet[] bullets = spawnedObj.GetComponentsInChildren<EnemyBullet>();
        foreach (EnemyBullet bullet in bullets)
        {
            bullet.speed = Random.Range(minBulletSpeed, maxBulletSpeed);
        }

        // EnemyHealth bile�eninde prefab bilgisini ayarla
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
        // EnemyHealth bile�enini al
        EnemyHealth healthComponent = enemy.GetComponent<EnemyHealth>();
        if (healthComponent != null)
        {
            // Rastgele can de�eri atamas� yap
            healthComponent.maxHealth = Random.Range(minHealth, maxHealth + 1);
            healthComponent.currentHealth = healthComponent.maxHealth;
            // Sald�r� h�z� zaten EnemyHealth i�inde rastgele atan�yor, burada tekrar atamaya gerek yok
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
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] initialObjectsToSpawn; // Ba�lang��ta spawn edilecek nesneler
    public GameObject[] replacementObjects; // Yok oldu�unda spawn edilecek yeni nesneler
    public Vector2 spawnAreaMin; // Spawn alan�n�n sol alt k��esi
    public Vector2 spawnAreaMax; // Spawn alan�n�n sa� �st k��esi
    public int totalObjects = 10; // Toplam spawnlanacak nesne say�s�
    private List<GameObject> spawnedObjects = new List<GameObject>();

    void Start()
    {
        // Ba�lang��ta nesneleri spawn et
        foreach (GameObject obj in initialObjectsToSpawn)
        {
            SpawnObject(obj);
        }
    }

    public void SpawnObject(GameObject obj)
    {
        Vector2 spawnPosition = GetRandomSpawnPosition();
        GameObject spawnedObj = Instantiate(obj, spawnPosition, Quaternion.identity);
        spawnedObjects.Add(spawnedObj);
    }

    Vector2 GetRandomSpawnPosition()
    {
        Vector2 spawnPosition = new Vector2(
            Random.Range(spawnAreaMin.x, spawnAreaMax.x),
            Random.Range(spawnAreaMin.y, spawnAreaMax.y)
        );
        return spawnPosition;
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

    public void RemoveObject(GameObject obj)
    {
        spawnedObjects.Remove(obj);
        Destroy(obj);

        if (spawnedObjects.Count < totalObjects)
        {
            // Yerine rastgele bir nesne spawn etmek i�in replacementObjects dizisinden bir prefab se�in
            GameObject replacementPrefab = replacementObjects[Random.Range(0, replacementObjects.Length)];
            SpawnObject(replacementPrefab);
        }
    }

    void Update()
    {
        // Spawnlanan nesne say�s�n� kontrol et ve eksik olan nesneleri spawn et
        while (spawnedObjects.Count < totalObjects)
        {
            GameObject replacementPrefab = replacementObjects[Random.Range(0, replacementObjects.Length)];
            SpawnObject(replacementPrefab);
        }
    }
}
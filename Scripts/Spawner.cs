using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] initialObjectsToSpawn; // Baþlangýçta spawn edilecek nesneler
    public GameObject[] replacementObjects; // Yok olduðunda spawn edilecek yeni nesneler
    public Vector2 spawnAreaMin; // Spawn alanýnýn sol alt köþesi
    public Vector2 spawnAreaMax; // Spawn alanýnýn sað üst köþesi
    public int totalObjects = 10; // Toplam spawnlanacak nesne sayýsý
    private List<GameObject> spawnedObjects = new List<GameObject>();

    void Start()
    {
        // Baþlangýçta nesneleri spawn et
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
            // Yerine rastgele bir nesne spawn etmek için replacementObjects dizisinden bir prefab seçin
            GameObject replacementPrefab = replacementObjects[Random.Range(0, replacementObjects.Length)];
            SpawnObject(replacementPrefab);
        }
    }

    void Update()
    {
        // Spawnlanan nesne sayýsýný kontrol et ve eksik olan nesneleri spawn et
        while (spawnedObjects.Count < totalObjects)
        {
            GameObject replacementPrefab = replacementObjects[Random.Range(0, replacementObjects.Length)];
            SpawnObject(replacementPrefab);
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSpawn : MonoBehaviour
{
    public GameObject objectToSpawn; // Spawnlanacak obje
    public Vector2 spawnAreaMin; // Spawn alan�n�n sol alt k��esi
    public Vector2 spawnAreaMax; // Spawn alan�n�n sa� �st k��esi
    public int totalObjects = 10; // Toplam spawnlanacak obje say�s�
    private List<GameObject> spawnedObjects = new List<GameObject>();

    void Start()
    {
        for (int i = 0; i < totalObjects; i++)
        {
            SpawnObject();
        }
    }

    public void SpawnObject()
    {
        Vector2 spawnPosition = new Vector2(
            Random.Range(spawnAreaMin.x, spawnAreaMax.x),
            Random.Range(spawnAreaMin.y, spawnAreaMax.y)
        );
        GameObject spawnedObj = Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);
        HealthObj healthComponent = spawnedObj.GetComponent<HealthObj>();
        if (healthComponent != null)
        {
            healthComponent.spawner = this; // HealthObj'deki spawner referans�n� ayarla
        }
        spawnedObjects.Add(spawnedObj);
    }

    public void RemoveObject(GameObject obj)
    {
        spawnedObjects.Remove(obj);
        Destroy(obj);

        if (spawnedObjects.Count < totalObjects)
        {
            StartCoroutine(RespawnObject());
        }
    }

    private IEnumerator RespawnObject()
    {
        yield return new WaitForSeconds(1f); // 1 saniye bekle, sonra nesneyi tekrar spawn et
        SpawnObject();
    }
}
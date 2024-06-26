using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject[] bulletPrefabs; // Farkl� silah prefablar�
    public Transform[] firePoints; // Farkl� ate� noktalar�
    public float bulletSpeed = 10f; // Kur�unun h�z�

    private int currentWeaponIndex = 0; // Ba�lang��ta kullan�lacak silah�n index'i

    public void Fire()
    {
        // E�er aktif de�ilse ate� etme
        if (!gameObject.activeInHierarchy) return;

        foreach (Transform firePoint in firePoints)
        {
            GameObject bullet = Instantiate(bulletPrefabs[currentWeaponIndex], firePoint.position, firePoint.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.velocity = firePoint.up * bulletSpeed;
        }
    }

    public void SwitchWeapon(int index)
    {
        if (index >= 0 && index < bulletPrefabs.Length)
        {
            currentWeaponIndex = index;
        }
        else
        {
            Debug.LogWarning("Invalid weapon index!");
        }
    }
}
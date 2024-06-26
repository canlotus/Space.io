using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject[] bulletPrefabs; // Farklý silah prefablarý
    public Transform[] firePoints; // Farklý ateþ noktalarý
    public float bulletSpeed = 10f; // Kurþunun hýzý

    private int currentWeaponIndex = 0; // Baþlangýçta kullanýlacak silahýn index'i

    public void Fire()
    {
        // Eðer aktif deðilse ateþ etme
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
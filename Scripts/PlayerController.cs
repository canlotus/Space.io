using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    public Weapon[] weapons; // T�m silahlar� bar�nd�ran dizi
    public float fireRate = 0.2f; // Ate� etme s�kl��� (saniyede bir ate�)

    private int currentWeaponIndex = 0; // Ba�lang��ta kullan�lacak silah�n index'i
    private Weapon currentWeapon;

    Vector2 moveDirection;
    Vector2 mousePosition;

    void Start()
    {
        // Ba�lang��ta sadece ilk silah� aktif yap
        SwitchWeapon(0);
        StartCoroutine(AutoFire());
    }

    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector2(moveX, moveY).normalized;
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Ge�ici test i�in silah de�i�tirme tu�lar�
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwitchWeapon(11);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SwitchWeapon(12);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SwitchWeapon(10);
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
        Vector2 aimDirection = mousePosition - rb.position;
        float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = aimAngle;

        // S�n�rlar� kontrol et
        ClampPosition();
    }

    private void ClampPosition()
    {
        Vector2 clampedPosition = rb.position;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, -100f, 100f);
        clampedPosition.y = Mathf.Clamp(clampedPosition.y, -100f, 100f);
        rb.position = clampedPosition;
    }

    private IEnumerator AutoFire()
    {
        while (true)
        {
            if (currentWeapon != null && currentWeapon.gameObject.activeInHierarchy)
            {
                currentWeapon.Fire();
            }
            yield return new WaitForSeconds(fireRate);
        }
    }

    public void SwitchWeapon(int index)
    {
        if (index >= 0 && index < weapons.Length)
        {
            // T�m silahlar� devre d��� b�rak
            foreach (Weapon weapon in weapons)
            {
                weapon.gameObject.SetActive(false);
            }

            // Yeni silah� aktif yap
            currentWeaponIndex = index;
            currentWeapon = weapons[currentWeaponIndex];
            currentWeapon.gameObject.SetActive(true);
        }
        else
        {
            Debug.LogWarning("Invalid weapon index!");
        }
    }
}
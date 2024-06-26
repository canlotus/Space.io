using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    public Weapon[] weapons; // Tüm silahları barındıran dizi
    public float fireRate = 0.2f; // Ateş etme sıklığı (saniyede bir ateş)

    private int currentWeaponIndex = 0; // Başlangıçta kullanılacak silahın index'i
    private Weapon currentWeapon;

    Vector2 moveDirection;
    Vector2 mousePosition;

    void Start()
    {
        // Başlangıçta sadece ilk silahı aktif yap
        SwitchWeapon(0);
        StartCoroutine(AutoFire());
    }

    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector2(moveX, moveY).normalized;
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Geçici test için silah değiştirme tuşları
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

        // Sınırları kontrol et
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
            // Tüm silahları devre dışı bırak
            foreach (Weapon weapon in weapons)
            {
                weapon.gameObject.SetActive(false);
            }

            // Yeni silahı aktif yap
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
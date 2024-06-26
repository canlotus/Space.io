using UnityEngine;
using UnityEngine.UI;

public class WeaponSelectionUI : MonoBehaviour
{
    public GameObject[] weaponsLevel2; // Seviye 2 i�in silahlar
    public GameObject[] weaponsLevel3; // Seviye 3 i�in silahlar
    public GameObject[] weaponsLevel4; // Seviye 4 i�in silahlar
    public GameObject[] weaponsLevel5; // Seviye 5 i�in silahlar

    public GameObject weaponSelectionPanel; // Silah se�im aray�z� paneli
    public Button[] weaponButtons; // Seviye 2-5 i�in silah butonlar�

    private PlayerController playerController;

    void Start()
    {
        playerController = GameObject.FindObjectOfType<PlayerController>();
    }

    public void ShowWeaponSelection(int level)
    {
        weaponSelectionPanel.SetActive(true); // Silah se�im panelini aktif et

        switch (level)
        {
            case 2:
                SetWeaponButtons(weaponsLevel2);
                break;
            case 3:
                SetWeaponButtons(weaponsLevel3);
                break;
            case 4:
                SetWeaponButtons(weaponsLevel4);
                break;
            case 5:
                SetWeaponButtons(weaponsLevel5);
                break;
            default:
                Debug.LogWarning("Invalid level for weapon selection!");
                break;
        }
    }

    private void SetWeaponButtons(GameObject[] weapons)
    {
        for (int i = 0; i < weaponButtons.Length; i++)
        {
            if (i < weapons.Length)
            {
                // Butonu aktif hale getir ve �zerine silah ad�n� veya mini resmi koy
                weaponButtons[i].gameObject.SetActive(true);
                // �rne�in, buton �zerine silah ad�n� veya mini resmi koyabilirsiniz
                // buttons[i].GetComponent<Image>().sprite = weapons[i].GetComponent<Weapon>().weaponIcon;
                // Butona t�klama eventi atay�n
                int weaponIndex = GetWeaponIndex(weapons[i]);
                weaponButtons[i].onClick.RemoveAllListeners(); // Eski eventleri temizle
                weaponButtons[i].onClick.AddListener(() => SelectWeapon(weaponIndex));
            }
            else
            {
                // E�er silah se�ene�i yoksa butonu devre d��� b�rak
                weaponButtons[i].gameObject.SetActive(false);
            }
        }
    }

    private int GetWeaponIndex(GameObject weapon)
    {
        for (int i = 0; i < playerController.weapons.Length; i++)
        {
            if (playerController.weapons[i].gameObject == weapon)
            {
                return i;
            }
        }
        return -1;
    }

    public void SelectWeapon(int weaponIndex)
    {
        if (weaponIndex >= 0)
        {
            // Se�ilen silah� aktif hale getir
            playerController.SwitchWeapon(weaponIndex);
            weaponSelectionPanel.SetActive(false); // Silah se�im panelini kapat
        }
        else
        {
            Debug.LogWarning("Invalid weapon index selected!");
        }
    }
}
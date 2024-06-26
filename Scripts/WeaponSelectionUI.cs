using UnityEngine;
using UnityEngine.UI;

public class WeaponSelectionUI : MonoBehaviour
{
    public GameObject[] weaponsLevel2; // Seviye 2 için silahlar
    public GameObject[] weaponsLevel3; // Seviye 3 için silahlar
    public GameObject[] weaponsLevel4; // Seviye 4 için silahlar
    public GameObject[] weaponsLevel5; // Seviye 5 için silahlar

    public GameObject weaponSelectionPanel; // Silah seçim arayüzü paneli
    public Button[] weaponButtons; // Seviye 2-5 için silah butonlarý

    private PlayerController playerController;

    void Start()
    {
        playerController = GameObject.FindObjectOfType<PlayerController>();
    }

    public void ShowWeaponSelection(int level)
    {
        weaponSelectionPanel.SetActive(true); // Silah seçim panelini aktif et

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
                // Butonu aktif hale getir ve üzerine silah adýný veya mini resmi koy
                weaponButtons[i].gameObject.SetActive(true);
                // Örneðin, buton üzerine silah adýný veya mini resmi koyabilirsiniz
                // buttons[i].GetComponent<Image>().sprite = weapons[i].GetComponent<Weapon>().weaponIcon;
                // Butona týklama eventi atayýn
                int weaponIndex = GetWeaponIndex(weapons[i]);
                weaponButtons[i].onClick.RemoveAllListeners(); // Eski eventleri temizle
                weaponButtons[i].onClick.AddListener(() => SelectWeapon(weaponIndex));
            }
            else
            {
                // Eðer silah seçeneði yoksa butonu devre dýþý býrak
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
            // Seçilen silahý aktif hale getir
            playerController.SwitchWeapon(weaponIndex);
            weaponSelectionPanel.SetActive(false); // Silah seçim panelini kapat
        }
        else
        {
            Debug.LogWarning("Invalid weapon index selected!");
        }
    }
}
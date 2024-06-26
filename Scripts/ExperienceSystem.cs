using UnityEngine;
using UnityEngine.UI;

public class ExperienceSystem : MonoBehaviour
{
    public int currentLevel = 1; // Baþlangýç seviyesi
    public int currentXP = 0; // Baþlangýç deneyim puaný

    public int[] xpToLevelUp; // Seviye atlamak için gereken toplam deneyim puaný miktarlarý

    public int maxHealthIncreasePerLevel = 200; // Seviye baþýna maksimum saðlýk artýþý
    public PlayerHealth playerHealth;
    public Text levelText; // UI üzerinde seviye göstermek için Text objesi
    public Slider expSlider; // UI üzerinde XP ilerlemesini göstermek için Slider objesi
    public WeaponSelectionUI weaponSelectionUI; // WeaponSelectionUI referansý

    void Start()
    {
        // PlayerHealth sýnýfýndan referansý al
        playerHealth = GameObject.FindObjectOfType<PlayerHealth>();

        // Seviye atlamak için gereken deneyim puaný miktarlarýný tanýmla
        xpToLevelUp = new int[] { 0, 20, 40, 80, 160 }; // Örnek: Level 1 -> 0 XP, Level 2 -> 50 XP, Level 3 -> 100 XP, vb.

        // UI elementlerini baðla
        levelText = GameObject.Find("LevelText").GetComponent<Text>(); // UI'deki LevelText objesini bul
        expSlider = GameObject.Find("ExpSlider").GetComponent<Slider>(); // UI'deki ExpSlider objesini bul

        UpdateUI(); // Baþlangýçta UI'yi güncelle
    }

    // XP kazanma iþlemi
    public void GainXP(int amount)
    {
        currentXP += amount;

        // Seviye atlamak için gerekli XP'yi kontrol et
        while (currentLevel < xpToLevelUp.Length && currentXP >= xpToLevelUp[currentLevel])
        {
            LevelUp();
        }

        // UI güncellemeleri
        UpdateUI();
    }

    // Seviye atlamak
    void LevelUp()
    {
        currentLevel++;
        currentXP = 0; // Seviye atladýðýmýz için deneyim puanýný sýfýrla

        // Seviye atladýðýmýzda maksimum saðlýðý artýr
        if (playerHealth != null)
        {
            playerHealth.maxHealth += maxHealthIncreasePerLevel;
        }

        // Silah seçim panelini göster
        if (weaponSelectionUI != null)
        {
            weaponSelectionUI.ShowWeaponSelection(currentLevel);
        }

        // UI güncellemesi
        UpdateUI();
    }

    // UI güncellemesi
    void UpdateUI()
    {
        // Level Text güncelle
        if (levelText != null)
        {
            levelText.text = "Level: " + currentLevel.ToString();
        }

        // XP Slider güncelle
        if (expSlider != null)
        {
            expSlider.maxValue = GetNextLevelXP();
            expSlider.value = currentXP;
        }
    }

    // Seviye atlamak için gerekli XP miktarýný al
    public int GetNextLevelXP()
    {
        if (currentLevel < xpToLevelUp.Length)
        {
            return xpToLevelUp[currentLevel];
        }
        else
        {
            // Son seviyeye ulaþýldýysa, sonsuz bir deðer döndürmek yerine bir sýnýrlama yapýlabilir
            return int.MaxValue;
        }
    }

    // Seviye atladýðýmýzda oyuncu özelliklerini güncellemek için kullanýlabilir
    void UpdatePlayerStats()
    {
        // Burada oyuncunun özelliklerini güncelleyebilirsiniz
        // Örneðin, maksimum saðlýðý arttýrabilir veya yeni yetenekler verebilirsiniz
    }
}
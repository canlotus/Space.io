using UnityEngine;
using UnityEngine.UI;

public class ExperienceSystem : MonoBehaviour
{
    public int currentLevel = 1; // Ba�lang�� seviyesi
    public int currentXP = 0; // Ba�lang�� deneyim puan�

    public int[] xpToLevelUp; // Seviye atlamak i�in gereken toplam deneyim puan� miktarlar�

    public int maxHealthIncreasePerLevel = 200; // Seviye ba��na maksimum sa�l�k art���
    public PlayerHealth playerHealth;
    public Text levelText; // UI �zerinde seviye g�stermek i�in Text objesi
    public Slider expSlider; // UI �zerinde XP ilerlemesini g�stermek i�in Slider objesi
    public WeaponSelectionUI weaponSelectionUI; // WeaponSelectionUI referans�

    void Start()
    {
        // PlayerHealth s�n�f�ndan referans� al
        playerHealth = GameObject.FindObjectOfType<PlayerHealth>();

        // Seviye atlamak i�in gereken deneyim puan� miktarlar�n� tan�mla
        xpToLevelUp = new int[] { 0, 20, 40, 80, 160 }; // �rnek: Level 1 -> 0 XP, Level 2 -> 50 XP, Level 3 -> 100 XP, vb.

        // UI elementlerini ba�la
        levelText = GameObject.Find("LevelText").GetComponent<Text>(); // UI'deki LevelText objesini bul
        expSlider = GameObject.Find("ExpSlider").GetComponent<Slider>(); // UI'deki ExpSlider objesini bul

        UpdateUI(); // Ba�lang��ta UI'yi g�ncelle
    }

    // XP kazanma i�lemi
    public void GainXP(int amount)
    {
        currentXP += amount;

        // Seviye atlamak i�in gerekli XP'yi kontrol et
        while (currentLevel < xpToLevelUp.Length && currentXP >= xpToLevelUp[currentLevel])
        {
            LevelUp();
        }

        // UI g�ncellemeleri
        UpdateUI();
    }

    // Seviye atlamak
    void LevelUp()
    {
        currentLevel++;
        currentXP = 0; // Seviye atlad���m�z i�in deneyim puan�n� s�f�rla

        // Seviye atlad���m�zda maksimum sa�l��� art�r
        if (playerHealth != null)
        {
            playerHealth.maxHealth += maxHealthIncreasePerLevel;
        }

        // Silah se�im panelini g�ster
        if (weaponSelectionUI != null)
        {
            weaponSelectionUI.ShowWeaponSelection(currentLevel);
        }

        // UI g�ncellemesi
        UpdateUI();
    }

    // UI g�ncellemesi
    void UpdateUI()
    {
        // Level Text g�ncelle
        if (levelText != null)
        {
            levelText.text = "Level: " + currentLevel.ToString();
        }

        // XP Slider g�ncelle
        if (expSlider != null)
        {
            expSlider.maxValue = GetNextLevelXP();
            expSlider.value = currentXP;
        }
    }

    // Seviye atlamak i�in gerekli XP miktar�n� al
    public int GetNextLevelXP()
    {
        if (currentLevel < xpToLevelUp.Length)
        {
            return xpToLevelUp[currentLevel];
        }
        else
        {
            // Son seviyeye ula��ld�ysa, sonsuz bir de�er d�nd�rmek yerine bir s�n�rlama yap�labilir
            return int.MaxValue;
        }
    }

    // Seviye atlad���m�zda oyuncu �zelliklerini g�ncellemek i�in kullan�labilir
    void UpdatePlayerStats()
    {
        // Burada oyuncunun �zelliklerini g�ncelleyebilirsiniz
        // �rne�in, maksimum sa�l��� artt�rabilir veya yeni yetenekler verebilirsiniz
    }
}
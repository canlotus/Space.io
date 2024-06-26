using UnityEngine;
using UnityEngine.UI;
using System;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public Image healthBarForeground;
    public Transform healthBarCanvas;

    public event Action OnPlayerDeath; // Oyuncu ölümü olayý

    public ExperienceSystem experienceSystem;
    public Text levelText; // Level Text
    public Slider expSlider; // XP Slider

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();

        if (experienceSystem == null)
        {
            experienceSystem = GetComponent<ExperienceSystem>();
            if (experienceSystem == null)
            {
                Debug.LogError("ExperienceSystem component not found!");
            }
        }

        UpdateLevelAndXPUI();
    }

    void Update()
    {
        if (healthBarCanvas != null)
        {
            Vector3 healthBarPosition = transform.position + new Vector3(0, 1, 0);
            healthBarCanvas.position = healthBarPosition;
            healthBarCanvas.rotation = Quaternion.identity;
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHealthBar();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Oyuncu ölümü iþlemleri
        OnPlayerDeath?.Invoke(); // Oyuncu ölümü olayýný tetikle
        GameManager.Instance.OnPlayerDied(); // GameManager üzerinden ölümü bildir
        gameObject.SetActive(false); // Oyuncu nesnesini devre dýþý býrak (opsiyonel)
    }

    public void RestoreHealth(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHealthBar();
    }

    public void UpdateHealthBar()
    {
        if (healthBarForeground != null)
        {
            healthBarForeground.fillAmount = (float)currentHealth / maxHealth;
        }
    }

    public void GainExperience(int amount)
    {
        if (experienceSystem != null)
        {
            experienceSystem.GainXP(amount);
            UpdateLevelAndXPUI();
        }
    }

    void UpdateLevelAndXPUI()
    {
        if (levelText != null)
        {
            levelText.text = "Level: " + experienceSystem.currentLevel;
        }
        if (expSlider != null && experienceSystem != null)
        {
            int currentXP = experienceSystem.currentXP;
            int nextLevelXP = experienceSystem.GetNextLevelXP();
            expSlider.value = (float)currentXP / nextLevelXP; // Deneyim puaný çubuðunun doluluk oranýný ayarla
        }
    }
}
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    bool gameStarted = false;

    void Awake()
    {
        // Singleton deseni: GameManager tek bir instance olarak çalýþsýn
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        // Oyun baþlatýldýktan sonra Space tuþuna basýldýðýnda RestartGame metodunu çaðýr
        if (gameStarted && Input.GetKeyDown(KeyCode.Space))
        {
            RestartGame();
        }
    }

    public void StartGame()
    {
        gameStarted = true;
        SceneManager.LoadScene("GameScene"); // Oyun sahnesini yükle
    }

    public void RestartGame()
    {
        gameStarted = false; // Oyun baþlatýldý flag'ini sýfýrla
        SceneManager.LoadScene("StartScene"); // Start sahnesine geri dön
    }

    public void OnPlayerDied()
    {
        // Oyuncu öldüðünde yapýlacak iþlemler
        RestartGame();
    }
}
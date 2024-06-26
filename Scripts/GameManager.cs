using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    bool gameStarted = false;

    void Awake()
    {
        // Singleton deseni: GameManager tek bir instance olarak �al��s�n
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
        // Oyun ba�lat�ld�ktan sonra Space tu�una bas�ld���nda RestartGame metodunu �a��r
        if (gameStarted && Input.GetKeyDown(KeyCode.Space))
        {
            RestartGame();
        }
    }

    public void StartGame()
    {
        gameStarted = true;
        SceneManager.LoadScene("GameScene"); // Oyun sahnesini y�kle
    }

    public void RestartGame()
    {
        gameStarted = false; // Oyun ba�lat�ld� flag'ini s�f�rla
        SceneManager.LoadScene("StartScene"); // Start sahnesine geri d�n
    }

    public void OnPlayerDied()
    {
        // Oyuncu �ld���nde yap�lacak i�lemler
        RestartGame();
    }
}
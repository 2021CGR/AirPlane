using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static bool isGameOver = false;
    public static int enemyKillCount = 0;

    public GameObject gameOverPanel;
    public GameObject gameClearPanel;

    void Update()
    {
        if (!isGameOver && enemyKillCount >= 5)
        {
            ShowGameClear();
        }
    }

    public void ShowGameOver()
    {
        isGameOver = true;

        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }
        else
        {
            Debug.LogWarning("GameOverPanel이 연결되지 않았습니다!");
        }
    }

    public void ShowGameClear()
    {
        isGameOver = true;

        Debug.Log("게임 클리어 조건 만족!"); // ← 이게 Console에 뜨는지 확인!

        if (gameClearPanel != null)
        {
            gameClearPanel.SetActive(true);
        }
        else
        {
            Debug.LogWarning("GameClearPanel이 연결되지 않았습니다!");
        }
    }

    public void RestartGame()
    {
        isGameOver = false;
        enemyKillCount = 0;
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GoToMainMenu()
    {
        isGameOver = false;
        enemyKillCount = 0;
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadNextScene()
    {
        isGameOver = false;
        enemyKillCount = 0;
        Time.timeScale = 1f;
        SceneManager.LoadScene("Map2"); // 다음 씬 이름에 맞게 수정 가능
    }
}

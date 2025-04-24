using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static bool isGameOver = false;
    public static int totalEnemyCount = 0;     // 생성된 적 수
    public static int enemyKillCount = 0;      // 처치된 적 수

    [Header("📺 UI 패널")]
    public GameObject gameOverPanel;
    public GameObject gameClearPanel;
    public GameObject pausePanel;

    private bool isPaused = false;

    void Start()
    {
        SetCursorGameplay(); // ✅ 게임 시작 시 커서 상태 확정
    }

    void Update()
    {
        // 클리어 조건: 생성된 적만큼 처치
        if (!isGameOver && totalEnemyCount > 0 && enemyKillCount >= totalEnemyCount)
        {
            ShowGameClear();
        }

        // ESC 키로 일시정지 토글
        if (Input.GetKeyDown(KeyCode.Escape) && !isGameOver)
        {
            TogglePause();
        }
    }

    public void ShowGameOver()
    {
        isGameOver = true;
        Time.timeScale = 0f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);
    }

    public void ShowGameClear()
    {
        isGameOver = true;
        Time.timeScale = 0f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        if (gameClearPanel != null)
            gameClearPanel.SetActive(true);
    }

    public void TogglePause()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0f : 1f;

        if (isPaused)
        {
            // 일시정지 시 커서 보이기
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            // 게임 재개 시 커서 숨기기
            SetCursorGameplay(); // 게임 상태로 돌아갈 때는 커서 초기화
        }

        if (pausePanel != null)
            pausePanel.SetActive(isPaused);
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene("MainMenu"); // 실제 씬 이름으로 변경 필요
    }

    // 게임 내에서 커서를 숨기고 잠금 상태로 설정
    void SetCursorGameplay()
    {
        // 게임 중에는 커서 숨기기, 마우스 화면 밖으로 못 나가게 설정
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
    }
}






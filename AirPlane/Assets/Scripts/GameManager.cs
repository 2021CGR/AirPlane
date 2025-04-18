using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static bool isGameOver = false;         // 게임 오버 상태 플래그
    public static int enemyKillCount = 0;          // 적 처치 수 카운트

    public GameObject gameOverPanel;               // 게임 오버 UI 패널
    public GameObject gameClearPanel;              // 게임 클리어 UI 패널
    public GameObject pausePanel;                  // 일시정지 UI 패널

    private bool isPaused = false;                 // 일시정지 상태 플래그

    void Start()
    {
        // 게임 시작 시 마우스 커서 숨기고 잠금
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // 적 5마리 이상 잡으면 게임 클리어
        if (!isGameOver && enemyKillCount >= 5)
        {
            ShowGameClear();
        }

        // ESC 키 누르면 일시정지 토글 (단, 게임 오버 상태가 아닐 때만)
        if (Input.GetKeyDown(KeyCode.Escape) && !isGameOver)
        {
            TogglePause();
        }
    }

    // 게임 오버 처리 함수
    public void ShowGameOver()
    {
        isGameOver = true;

        // 마우스 커서 보이게 설정
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true); // 게임 오버 UI 활성화
        }
        else
        {
            Debug.LogWarning("GameOverPanel이 연결되지 않았습니다!");
        }
    }

    // 게임 클리어 처리 함수
    public void ShowGameClear()
    {
        isGameOver = true;

        // 마우스 커서 보이게 설정
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        Debug.Log("게임 클리어 조건 만족!");

        if (gameClearPanel != null)
        {
            gameClearPanel.SetActive(true); // 게임 클리어 UI 활성화
        }
        else
        {
            Debug.LogWarning("GameClearPanel이 연결되지 않았습니다!");
        }
    }

    // 일시정지 토글 함수 (ESC 키로 실행)
    public void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Time.timeScale = 0f;                     // 게임 멈춤
            pausePanel.SetActive(true);              // 일시정지 UI 보임
            Cursor.visible = true;                   // 마우스 커서 보임
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Time.timeScale = 1f;                     // 게임 재개
            pausePanel.SetActive(false);             // 일시정지 UI 숨김
            Cursor.visible = false;                  // 마우스 커서 숨김
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    // "계속하기" 버튼에서 호출되는 함수
    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f;
        pausePanel.SetActive(false);

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // "다시 시작" 버튼에서 호출
    public void RestartGame()
    {
        isGameOver = false;
        isPaused = false;
        enemyKillCount = 0;
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // "메인 메뉴로" 버튼에서 호출
    public void GoToMainMenu()
    {
        isGameOver = false;
        isPaused = false;
        enemyKillCount = 0;
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu"); // 메인 메뉴 씬 이름
    }

    // "다음 스테이지" 버튼에서 호출
    public void LoadNextScene()
    {
        isGameOver = false;
        isPaused = false;
        enemyKillCount = 0;
        Time.timeScale = 1f;
        SceneManager.LoadScene("Map2"); // 다음 씬 이름에 맞게 수정
    }
}

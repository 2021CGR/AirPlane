using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // 게임 시작 버튼 동작
    public void StartGame()
    {
        SceneManager.LoadScene("Map1"); // "Map1" 이름의 씬을 로드
    }

    // 게임 종료 버튼 동작
    public void QuitGame()
    {
        Debug.Log("게임 종료");
        Application.Quit(); // 애플리케이션 종료
    }
}

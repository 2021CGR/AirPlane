using UnityEngine;
using TMPro; // TextMeshPro 텍스트 UI 사용을 위한 네임스페이스

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 5;             // 플레이어의 최대 체력
    private int currentHealth;            // 현재 체력 값

    public TMP_Text healthText;           // 화면에 표시할 체력 숫자 텍스트 (UI)

    void Start()
    {
        // 게임 시작 시 최대 체력으로 초기화
        currentHealth = maxHealth;

        // 체력 텍스트 초기화
        UpdateHealthText();
    }

    public void TakeDamage(int damage)
    {
        // 데미지만큼 체력 감소
        currentHealth -= damage;

        // 체력을 0 ~ maxHealth 사이로 제한 (음수 방지)
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        Debug.Log("플레이어 체력: " + currentHealth);

        // 체력 표시 텍스트 업데이트
        UpdateHealthText();

        // 체력이 0이 되면 사망 처리
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // 체력 숫자 텍스트 업데이트 및 색상 변경
    void UpdateHealthText()
    {
        if (healthText != null)
        {
            healthText.text = "HP: " + currentHealth;

            // 체력이 2 이하이면 빨간색, 아니면 흰색
            if (currentHealth <= 2)
                healthText.color = Color.red;
            else
                healthText.color = Color.white;
        }
    }

    // 플레이어 사망 처리
    void Die()
    {
        Debug.Log("플레이어가 사망했습니다!");

        // 전체 게임 상태를 Game Over로 설정
        GameManager.isGameOver = true;

        // GameManager 오브젝트를 찾아서 ShowGameOver() 호출
        GameObject gameManagerObject = GameObject.Find("GameManager");
        if (gameManagerObject != null)
        {
            GameManager gm = gameManagerObject.GetComponent<GameManager>();
            if (gm != null)
            {
                gm.ShowGameOver(); // 게임 오버 UI 띄우기
            }
            else
            {
                Debug.LogWarning("GameManager 스크립트가 없음!");
            }
        }
        else
        {
            Debug.LogWarning("GameManager 오브젝트를 찾을 수 없음!");
        }

        // 플레이어 비활성화 (사망 상태)
        gameObject.SetActive(false);
    }
}

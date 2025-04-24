using UnityEngine;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 10;               // 최대 체력
    private int currentHealth;               // 현재 체력

    public TMP_Text healthText;              // 체력 표시 UI (TextMeshPro 사용)

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthText();
    }

    public void TakeDamage(int damage)
    {
        // 체력 감소
        currentHealth -= damage;

        // 체력이 0 아래로 내려가지 않게 제한
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        Debug.Log($"💥 플레이어 데미지 {damage} / 현재 체력: {currentHealth}");

        UpdateHealthText();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void UpdateHealthText()
    {
        if (healthText != null)
        {
            healthText.text = "HP: " + currentHealth;

            // 체력이 낮을 경우 색상 경고
            healthText.color = currentHealth <= 2 ? Color.red : Color.white;
        }
    }

    void Die()
    {
        Debug.Log("☠️ 플레이어 사망!");
        GameManager.isGameOver = true;

        GameObject gmObj = GameObject.Find("GameManager");
        if (gmObj != null)
        {
            GameManager gm = gmObj.GetComponent<GameManager>();
            if (gm != null)
            {
                gm.ShowGameOver();
            }
        }

        gameObject.SetActive(false); // 플레이어 비활성화
    }
}


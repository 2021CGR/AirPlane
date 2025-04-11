using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 5;   // 플레이어의 최대 체력
    private int currentHealth; // 현재 체력

    void Start()
    {
        currentHealth = maxHealth; // 게임 시작 시 최대 체력으로 설정
    }

    // 데미지를 받는 함수 (총알에 맞았을 때 호출)
    public void TakeDamage(int damage)
    {
        currentHealth -= damage; // 데미지만큼 체력을 감소
        Debug.Log("플레이어 체력: " + currentHealth);

        // 체력이 0 이하이면 플레이어 사망 처리
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("플레이어가 사망했습니다!");

        // 플레이어 오브젝트 삭제
        Destroy(gameObject);

        // 여기에서 추가적으로 게임 오버 이벤트를 트리거할 수 있습니다.
    }
}

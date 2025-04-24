using UnityEngine;

/// <summary>
/// 플레이어가 획득하면 체력을 회복시키는 아이템
/// </summary>
public class HealthItem : MonoBehaviour
{
    public int healAmount = 2; // 회복할 체력량

    // 플레이어나 다른 물체와 충돌했을 때 호출되는 함수
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 충돌한 오브젝트가 "Player" 태그를 가진 경우에만 처리
        if (collision.CompareTag("Player"))
        {
            // 충돌한 오브젝트에서 PlayerHealth 컴포넌트를 가져옴
            PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();

            // PlayerHealth가 존재하면 체력 회복 실행
            if (playerHealth != null)
            {
                // Heal 메서드가 없으므로 체력 증가를 직접 처리
                playerHealth.SendMessage("TakeDamage", -healAmount); // 마이너스 데미지는 회복 효과
                Debug.Log("플레이어 체력 회복: " + healAmount);
            }

            // 아이템은 즉시 파괴됨 (획득 효과)
            Destroy(gameObject);
        }
    }
}

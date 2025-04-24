using UnityEngine;

/// <summary>
/// 적의 총알이 플레이어에 닿으면 데미지를 입히고 총알을 파괴하는 스크립트
/// 중복 충돌을 방지하여 한 번만 데미지를 주도록 처리
/// </summary>
public class EnemyBullet : MonoBehaviour
{
    public int damage = 1;              // 총알이 주는 데미지
    private bool hasHit = false;        // 충돌 중복 방지용 플래그

    void Start()
    {
        // 총알이 생성된 후 10초 뒤에 자동 파괴 (안 날아가면 사라지게)
        Destroy(gameObject, 10f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // 이미 충돌했으면 무시 (중복 방지)
        if (hasHit) return;
        hasHit = true;

        // 충돌한 대상이 Player 태그라면
        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();

            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }

            Destroy(gameObject); // 총알 제거
        }
    }
}


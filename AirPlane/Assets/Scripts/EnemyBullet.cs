using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public int damage = 1; // 총알이 가하는 데미지

    void Start()
    {
        // 총알이 생성된 후 10초 뒤에 자동으로 삭제
        Destroy(gameObject, 10f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // 충돌한 대상이 플레이어인지 확인
        if (other.CompareTag("Player"))
        {
            // 플레이어의 PlayerHealth 컴포넌트를 가져옴
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();

            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage); // 플레이어에게 데미지 부여
            }

            // 충돌 시 총알 제거
            Destroy(gameObject);
        }
    }
}

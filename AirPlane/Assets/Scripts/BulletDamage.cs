using UnityEngine;

/// <summary>
/// 플레이어의 총알이 적과 충돌했을 때 데미지를 주는 스크립트
/// </summary>
public class BulletDamage : MonoBehaviour
{
    public int damage = 1; // 총알의 데미지

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 충돌 대상이 적이면
        if (collision.CompareTag("Enemy"))
        {
            // Enemy 컴포넌트를 찾아서 데미지 적용
            Enemy enemy = collision.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }

            // 총알 제거 (한 번만 데미지를 주도록)
            Destroy(gameObject);
        }
    }
}




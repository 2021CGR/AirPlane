using UnityEngine;

public class BulletDamage : MonoBehaviour
{
    public int damage = 1; // 총알의 데미지

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage); // 딜 적용
            }

            Destroy(gameObject); // 충돌 후 총알 파괴
        }
    }
}



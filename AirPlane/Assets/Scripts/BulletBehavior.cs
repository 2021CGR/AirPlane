using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    public float bulletSpeed = 10.0f;   // 총알 이동 속도
    public int damage = 1;             // 총알이 적에게 가하는 피해

    void Update()
    {
        if (GameManager.isGameOver) return; // 게임 오버 시 이동 정지

        transform.Translate(Vector3.up * bulletSpeed * Time.deltaTime);

        if (transform.position.y > 10)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }

            Destroy(gameObject);
        }
    }
}

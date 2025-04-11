using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    public float bulletSpeed = 10.0f;   // 총알 이동 속도
    public int damage = 1;             // 총알이 적에게 가하는 피해

    void Update()
    {
        // 총알을 위쪽으로 이동
        transform.Translate(Vector3.up * bulletSpeed * Time.deltaTime);

        // 화면 밖으로 나가면 총알 제거
        if (transform.position.y > 10)
        {
            Destroy(gameObject);
        }
    }

    // 충돌 처리
    void OnTriggerEnter2D(Collider2D other)
    {
        // 적과 충돌했을 때
        if (other.CompareTag("Enemy"))
        {
            // 적 오브젝트의 Enemy 스크립트 호출
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage); // 적에게 데미지 전달
            }

            // 총알 제거
            Destroy(gameObject);
        }
    }
}

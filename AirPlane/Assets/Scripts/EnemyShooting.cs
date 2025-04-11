using System.Collections;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public GameObject bulletPrefab;  // 적의 총알 프리팹
    public Transform firePoint;     // 총알 발사 위치
    public float fireRate = 2.0f;   // 발사 최대 간격 (랜덤 범위 내에서 결정)
    public float bulletSpeed = 5.0f; // 총알 속도

    private void Start()
    {
        // ShootRoutine 코루틴 시작
        StartCoroutine(ShootRoutine());
    }

    IEnumerator ShootRoutine()
    {
        while (true) // 무한 반복: 적이 파괴되거나 게임이 종료될 때까지 실행
        {
            // 1초 ~ fireRate 간격으로 랜덤 대기
            yield return new WaitForSeconds(Random.Range(1.0f, fireRate));

            // 총알 발사
            Shoot();
        }
    }

    void Shoot()
    {
        // 총알 생성
        if (bulletPrefab != null && firePoint != null)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity); // 총알 생성
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

            if (rb != null)
            {
                rb.velocity = Vector2.down * bulletSpeed; // 총알을 아래 방향으로 발사 (Y축 음수 방향)
            }
        }
    }
}

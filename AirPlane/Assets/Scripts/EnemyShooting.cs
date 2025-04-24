using System.Collections;
using UnityEngine;

/// <summary>
/// 적이 다양한 탄막 패턴으로 총알을 발사하는 스크립트
/// Inspector에서 패턴 종류 및 설정값을 조절할 수 있으며,
/// 자동으로 플레이어 타겟을 찾는 기능도 포함됨
/// </summary>
public class EnemyShooting : MonoBehaviour
{
    public enum ShotType
    {
        Straight,
        Spread,
        Circle,
        Rotating,
        Burst,
        Zigzag,
        Homing
    }

    [Header("🎯 탄막 패턴 설정")]
    public ShotType shotType = ShotType.Straight;   // 현재 사용할 탄막 패턴

    [Header("🔫 기본 발사 설정")]
    public GameObject bulletPrefab;                 // 사용할 총알 프리팹
    public Transform firePoint;                     // 총알이 생성될 위치
    public float fireRate = 2f;                     // 발사 주기
    public float bulletSpeed = 5f;                  // 총알 속도
    public int bulletCount = 5;                     // Circle, Spread, Burst용 개수

    [Header("🌈 퍼짐/회전/지그재그")]
    public float angleStep = 15f;                   // Spread용 각도 간격
    public float rotateSpeed = 30f;                 // 회전 탄막용 속도
    public float zigzagFrequency = 5f;              // 지그재그 횟수
    public float zigzagMagnitude = 0.5f;            // 지그재그 진폭

    [Header("💥 버스트(연사) 설정")]
    public float burstDelay = 0.1f;                 // Burst용 탄 간 딜레이

    [Header("🎯 타겟 설정 (추적용)")]
    public Transform playerTarget;                  // 추적 탄막용 타겟 (플레이어)

    private float currentAngle = 0f;                // 회전용 현재 각도

    private void Start()
    {
        // ▶️ 플레이어가 수동으로 연결되지 않은 경우 자동 탐색
        if (playerTarget == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                playerTarget = player.transform;
                Debug.Log("✅ 자동으로 플레이어 타겟을 연결했습니다.");
            }
            else
            {
                Debug.LogWarning("❌ 'Player' 태그를 가진 오브젝트를 찾을 수 없습니다.");
            }
        }

        // 발사 루틴 시작
        StartCoroutine(ShootRoutine());
    }

    /// <summary>
    /// 일정 간격마다 선택된 패턴으로 총알을 발사
    /// </summary>
    IEnumerator ShootRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(fireRate);

            switch (shotType)
            {
                case ShotType.Straight: ShootStraight(); break;
                case ShotType.Spread: ShootSpread(); break;
                case ShotType.Circle: ShootCircle(); break;
                case ShotType.Rotating: ShootRotating(); break;
                case ShotType.Burst: StartCoroutine(ShootBurst()); break;
                case ShotType.Zigzag: ShootZigzag(); break;
                case ShotType.Homing: ShootHoming(); break;
            }
        }
    }

    void ShootStraight()
    {
        CreateBullet(Vector2.down);
    }

    void ShootSpread()
    {
        float startAngle = -angleStep * (bulletCount - 1) / 2f;

        for (int i = 0; i < bulletCount; i++)
        {
            float angle = startAngle + i * angleStep;
            Vector2 dir = Quaternion.Euler(0, 0, angle) * Vector2.down;
            CreateBullet(dir.normalized);
        }
    }

    void ShootCircle()
    {
        float anglePerBullet = 360f / bulletCount;

        for (int i = 0; i < bulletCount; i++)
        {
            float angle = i * anglePerBullet;
            Vector2 dir = Quaternion.Euler(0, 0, angle) * Vector2.down;
            CreateBullet(dir.normalized);
        }
    }

    void ShootRotating()
    {
        float startAngle = currentAngle;

        for (int i = 0; i < bulletCount; i++)
        {
            float angle = startAngle + i * angleStep;
            Vector2 dir = Quaternion.Euler(0, 0, angle) * Vector2.down;
            CreateBullet(dir.normalized);
        }

        currentAngle += rotateSpeed;
    }

    IEnumerator ShootBurst()
    {
        for (int i = 0; i < bulletCount; i++)
        {
            CreateBullet(Vector2.down);
            yield return new WaitForSeconds(burstDelay);
        }
    }

    void ShootZigzag()
    {
        GameObject bullet = CreateBullet(Vector2.down);
        BulletZigzag zig = bullet.GetComponent<BulletZigzag>();
        if (zig != null)
        {
            zig.frequency = zigzagFrequency;
            zig.magnitude = zigzagMagnitude;
            zig.speed = bulletSpeed;
        }
    }

    void ShootHoming()
    {
        if (playerTarget == null) return;

        Vector2 direction = (playerTarget.position - firePoint.position).normalized;
        CreateBullet(direction);
    }

    /// <summary>
    /// 총알을 생성하고 지정된 방향으로 속도 부여
    /// </summary>
    GameObject CreateBullet(Vector2 direction)
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = direction * bulletSpeed;
        }
        return bullet;
    }
}





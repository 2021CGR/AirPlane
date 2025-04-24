using UnityEngine;

/// <summary>
/// 플레이어의 총알 발사 및 탄환 강화 상태를 관리하는 스크립트
/// </summary>
public class PlayerShooting : MonoBehaviour
{
    [Header("🔫 기본 총알")]
    public GameObject normalBulletPrefab;           // 기본 총알 프리팹

    [Header("💥 강화 총알")]
    public GameObject poweredBulletPrefab;          // 아이템 먹었을 때 사용하는 강화 총알 프리팹
    public float powerupDuration = 10f;             // 강화 지속 시간 (초)

    [Header("📍 발사 설정")]
    public Transform firePoint;                     // 총알이 생성될 위치
    public float fireRate = 0.5f;                   // 발사 속도 (간격)
    private float nextFireTime = 0f;                // 다음 발사가 가능한 시간

    private GameObject currentBulletPrefab;         // 현재 사용할 총알 (기본 or 강화)
    private float powerupTimer = 0f;                // 강화 상태 남은 시간

    private void Start()
    {
        // 시작할 때는 기본 총알로 설정
        currentBulletPrefab = normalBulletPrefab;
    }

    private void Update()
    {
        // ␣ 스페이스바를 누르면 총알 발사 + 쿨타임 체크
        if (Input.GetKey(KeyCode.Space) && Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + fireRate;
            Shoot();
        }

        // 강화 상태 시간 체크 → 다 지나면 기본 총알로 복귀
        if (powerupTimer > 0f)
        {
            powerupTimer -= Time.deltaTime;

            if (powerupTimer <= 0f)
            {
                currentBulletPrefab = normalBulletPrefab;
                Debug.Log("🔚 강화 총알 종료, 기본 총알로 복귀");
            }
        }
    }

    /// <summary>
    /// 현재 설정된 총알 프리팹으로 발사
    /// </summary>
    void Shoot()
    {
        if (currentBulletPrefab != null && firePoint != null)
        {
            // 총알 생성
            GameObject bullet = Instantiate(currentBulletPrefab, firePoint.position, Quaternion.identity);

            // 🔺 강화 상태일 경우, 데미지를 2배로 처리
            BulletDamage bulletDamage = bullet.GetComponent<BulletDamage>();
            if (powerupTimer > 0f && bulletDamage != null)
            {
                bulletDamage.damage *= 2;
            }

            // 총알 발사 사운드
            AudioManager.Instance.Play("Shoot");
        }
    }

    /// <summary>
    /// 강화 아이템을 먹었을 때 호출하는 함수
    /// </summary>
    public void ActivateBulletPowerup(GameObject newBulletPrefab, float duration)
    {
        currentBulletPrefab = newBulletPrefab; // 강화 총알로 변경
        powerupTimer = duration;               // 타이머 설정
        Debug.Log("⚡ 강화 총알 적용됨! " + duration + "초");
    }
}

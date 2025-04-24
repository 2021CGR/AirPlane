using UnityEngine;

/// <summary>
/// 플레이어의 이동 및 총알 발사를 제어하는 컨트롤러 스크립트
/// </summary>
public class PlayerController : MonoBehaviour
{
    [Header("🔫 총알 설정")]
    public GameObject bulletPrefab;   // 발사할 총알 프리팹
    public Transform firePoint;       // 총알이 생성될 위치 (무기의 총구)
    public float fireRate = 0.5f;     // 총알 발사 간격(초) — 빠르게 쏘고 싶으면 값을 줄이기

    [Header("🎮 이동 설정")]
    public float moveSpeed = 5f;      // 플레이어의 이동 속도
    public Vector2 minBounds;         // 이동 가능한 최소 좌표 (왼쪽 아래 경계)
    public Vector2 maxBounds;         // 이동 가능한 최대 좌표 (오른쪽 위 경계)

    void Update()
    {
        // ⬅️➡️ 상하좌우 키 입력 받아 이동
        float horizontalInput = Input.GetAxis("Horizontal"); // 왼쪽/오른쪽 화살표 또는 A/D 키
        float verticalInput = Input.GetAxis("Vertical");     // 위/아래 화살표 또는 W/S 키

        // 입력값에 속도를 곱해서 실제 이동량 계산
        Vector3 movement = new Vector3(horizontalInput, verticalInput, 0) * moveSpeed * Time.deltaTime;

        // 현재 위치에 이동량 더하기
        transform.position += movement;

        // 플레이어가 맵 밖으로 못 나가게 범위 제한
        float clampedX = Mathf.Clamp(transform.position.x, minBounds.x, maxBounds.x); // X축 제한
        float clampedY = Mathf.Clamp(transform.position.y, minBounds.y, maxBounds.y); // Y축 제한
        transform.position = new Vector3(clampedX, clampedY, transform.position.z);   // 제한된 위치로 갱신
    }

    /// <summary>
    /// 총알을 생성하고 발사하는 함수
    /// </summary>
    void Shoot()
    {
        // 총알 프리팹이 설정되어 있으면 총알 생성
        if (bulletPrefab != null && firePoint != null)
        {
            Instantiate(bulletPrefab, firePoint.position, Quaternion.identity); // 총알 생성

            // 사운드 매니저를 통해 "Shoot" 이름의 사운드를 재생
            // AudioManager_HG는 싱글톤으로 관리되는 사운드 시스템
            AudioManager.Instance.Play("Shoot");
        }
    }
}

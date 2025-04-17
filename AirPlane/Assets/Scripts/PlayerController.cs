using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject bulletPrefab;  // 총알 프리팹
    public Transform firePoint;     // 총알 생성 위치
    public float fireRate = 0.5f;   // 발사 간격(초)
    private float nextFire = 0.0f;  // 다음 발사가 가능한 시간

    public float moveSpeed = 5f;      // 이동 속도
    public Vector2 minBounds;        // 이동 최소 경계
    public Vector2 maxBounds;        // 이동 최대 경계

    void Update()
    {
        // 이동 처리
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontalInput, verticalInput, 0) * moveSpeed * Time.deltaTime;
        transform.position += movement;

        // 이동 범위 제한
        float clampedX = Mathf.Clamp(transform.position.x, minBounds.x, maxBounds.x);
        float clampedY = Mathf.Clamp(transform.position.y, minBounds.y, maxBounds.y);
        transform.position = new Vector3(clampedX, clampedY, transform.position.z);

        // 총알 발사
        if (Input.GetKey(KeyCode.Space) && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate; // 발사 간격
            Shoot();
        }
    }

    void Shoot()
    {
        // 총알 생성
        Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
    }
}

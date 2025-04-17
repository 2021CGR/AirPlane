using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float moveSpeed = 3.0f;       // 적 이동 속도
    public Vector2 minBounds;           // 이동 제한 범위(최소값)
    public Vector2 maxBounds;           // 이동 제한 범위(최대값)
    private Vector2 movementDirection;  // 이동 방향

    void Start()
    {
        // 랜덤한 초기 이동 방향 설정
        movementDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }

    void Update()
    {
        if (GameManager.isGameOver) return;

        // 적 이동
        transform.Translate(movementDirection * moveSpeed * Time.deltaTime);

        // 위치 클램핑(범위 제한)
        float clampedX = Mathf.Clamp(transform.position.x, minBounds.x, maxBounds.x); // X축 제한
        float clampedY = Mathf.Clamp(transform.position.y, minBounds.y, maxBounds.y); // Y축 제한

        transform.position = new Vector3(clampedX, clampedY, transform.position.z);

        // 경계에 도달하면 방향 변경
        if (transform.position.x <= minBounds.x || transform.position.x >= maxBounds.x)
        {
            movementDirection.x *= -1; // X 방향 반전
        }
        if (transform.position.y <= minBounds.y || transform.position.y >= maxBounds.y)
        {
            movementDirection.y *= -1; // Y 방향 반전
        }
    }
}

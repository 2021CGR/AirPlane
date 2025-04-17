using UnityEngine;

public class VerticalBackgroundScroll : MonoBehaviour
{
    public float scrollSpeed = 0.1f;
    public float backgroundHeight = 20f;

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        if (GameManager.isGameOver) return; // ✅ 게임 오버 시 스크롤 멈춤

        float newPosition = Mathf.Repeat(Time.time * scrollSpeed, backgroundHeight);
        transform.position = startPosition + Vector3.down * newPosition;
    }
}
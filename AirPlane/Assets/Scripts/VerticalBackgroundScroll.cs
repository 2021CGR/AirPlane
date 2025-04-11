using UnityEngine;

public class VerticalBackgroundScroll : MonoBehaviour
{
    public float scrollSpeed = 2.0f; // 배경 스크롤 속도
    private Vector3 startPosition;
    private float backgroundHeight;  // 배경 높이 계산

    void Start()
    {
        startPosition = transform.position;
        backgroundHeight = GetComponent<SpriteRenderer>().bounds.size.y; // 배경의 세로 크기
    }

    void Update()
    {
        // 배경 스크롤 반복
        float newPosition = Mathf.Repeat(Time.time * scrollSpeed, backgroundHeight);
        transform.position = startPosition + Vector3.down * newPosition;
    }
}

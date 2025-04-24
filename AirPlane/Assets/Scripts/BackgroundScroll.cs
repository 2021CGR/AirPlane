using UnityEngine;

/// <summary>
/// 두 배경을 위에서 아래로 무한 스크롤되게 만들고,
/// 자동으로 정확히 이어지게 배치해주는 스크립트
/// </summary>
public class LoopingBackground : MonoBehaviour
{
    public GameObject background1; // 첫 번째 배경
    public GameObject background2; // 두 번째 배경
    public float scrollSpeed = 2f;

    private float backgroundHeight;

    void Start()
    {
        // 배경 이미지의 세로 길이를 가져옴
        backgroundHeight = background1.GetComponent<SpriteRenderer>().bounds.size.y;
        Debug.Log("📏 배경 높이: " + backgroundHeight);

        // 두 배경이 정확히 이어지도록 초기 위치 재설정
        background1.transform.position = new Vector3(0, 0, 0);
        background2.transform.position = new Vector3(0, backgroundHeight, 0);
    }

    void Update()
    {
        // 아래 방향으로 스크롤
        background1.transform.Translate(Vector3.down * scrollSpeed * Time.deltaTime);
        background2.transform.Translate(Vector3.down * scrollSpeed * Time.deltaTime);

        // 첫 번째 배경이 화면 아래로 완전히 내려가면 위로 재배치
        if (background1.transform.position.y <= -backgroundHeight)
        {
            background1.transform.position = new Vector3(0, background2.transform.position.y + backgroundHeight, 0);
        }

        // 두 번째 배경이 화면 아래로 완전히 내려가면 위로 재배치
        if (background2.transform.position.y <= -backgroundHeight)
        {
            background2.transform.position = new Vector3(0, background1.transform.position.y + backgroundHeight, 0);
        }
    }
}


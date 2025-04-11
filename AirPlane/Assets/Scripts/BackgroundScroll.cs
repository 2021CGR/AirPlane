using UnityEngine;

public class LoopingBackground : MonoBehaviour
{
    public GameObject background1;  // 첫 번째 배경
    public GameObject background2;  // 두 번째 배경
    public float scrollSpeed = 2.0f;  // 배경 스크롤 속도
    private float backgroundHeight;  // 배경 이미지의 높이(자동 계산)

    void Start()
    {
        // 배경 높이 자동 설정
        backgroundHeight = background1.GetComponent<SpriteRenderer>().bounds.size.y;
    }

    void Update()
    {
        // 배경 1 스크롤
        background1.transform.Translate(Vector3.down * scrollSpeed * Time.deltaTime);

        // 배경 2 스크롤
        background2.transform.Translate(Vector3.down * scrollSpeed * Time.deltaTime);

        // 배경 1이 화면 아래로 벗어나면 재배치
        if (background1.transform.position.y <= -backgroundHeight)
        {
            Vector3 resetPosition = new Vector3(0, background2.transform.position.y + backgroundHeight, 0);
            background1.transform.position = resetPosition;
        }

        // 배경 2가 화면 아래로 벗어나면 재배치
        if (background2.transform.position.y <= -backgroundHeight)
        {
            Vector3 resetPosition = new Vector3(0, background1.transform.position.y + backgroundHeight, 0);
            background2.transform.position = resetPosition;
        }
    }
}

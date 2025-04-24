using UnityEngine;

/// <summary>
/// 총알이 지그재그로 움직이도록 만드는 스크립트
/// </summary>
public class BulletZigzag : MonoBehaviour
{
    public float speed = 5f;
    public float frequency = 5f;
    public float magnitude = 0.5f;

    private Vector3 axis;
    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
        axis = transform.right;
    }

    void Update()
    {
        startPos += Vector3.down * speed * Time.deltaTime;
        transform.position = startPos + axis * Mathf.Sin(Time.time * frequency) * magnitude;
    }
}

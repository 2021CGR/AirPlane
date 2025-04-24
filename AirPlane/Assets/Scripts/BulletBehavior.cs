using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    public float bulletSpeed = 7f; // 총알 속도

    void Update()
    {
        // 총알을 위로 이동
        transform.Translate(Vector2.up * bulletSpeed * Time.deltaTime);
    }
}

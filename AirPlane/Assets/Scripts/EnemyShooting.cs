using System.Collections;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public enum ShotType { Straight, Spread }

    [Header("🎯 탄막 패턴 설정")]
    public ShotType shotType = ShotType.Straight;

    [Header("🔫 발사 설정")]
    public GameObject bulletPrefab;
    public Transform firePoint;

    [Header("⚙️ 세부 설정")]
    public int bulletCount = 3;
    public float fireRate = 1f;
    public float bulletSpeed = 5f;
    public float angleStep = 0f;
    public float rotateSpeed = 0f;
    public float burstDelay = 0.1f;
    public float zigzagFrequency = 0f;
    public float zigzagMagnitude = 0f;

    public Transform playerTarget;
    private float currentAngle = 0f;

    void Awake()
    {
        shotType = (ShotType)Random.Range(0, System.Enum.GetValues(typeof(ShotType)).Length);
        PatternSettings preset = GetDefaultSettings(shotType);

        bulletCount = preset.bulletCount;
        angleStep = preset.angleStep;
        fireRate = preset.fireRate;
        bulletSpeed = preset.bulletSpeed;
        rotateSpeed = preset.rotateSpeed;
        burstDelay = preset.burstDelay;
        zigzagFrequency = preset.zigzagFrequency;
        zigzagMagnitude = preset.zigzagMagnitude;
    }

    void Start()
    {
        if (playerTarget == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null) playerTarget = player.transform;
        }

        StartCoroutine(ShootRoutine());
    }

    public static PatternSettings GetDefaultSettings(ShotType type)
    {
        PatternSettings settings = new PatternSettings();

        switch (type)
        {
            case ShotType.Straight:
                settings.bulletCount = 3;
                settings.fireRate = 1f;
                break;
            case ShotType.Spread:
                settings.bulletCount = 3;
                settings.angleStep = 20;
                break;
        }

        return settings;
    }

    IEnumerator ShootRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(fireRate);

            switch (shotType)
            {
                case ShotType.Straight: ShootStraight(); break;
                case ShotType.Spread: ShootSpread(); break;
            }
        }
    }

    void ShootStraight() => CreateBullet(Vector2.down);

    void ShootSpread()
    {
        float startAngle = -angleStep * (bulletCount - 1) / 2f;
        for (int i = 0; i < bulletCount; i++)
        {
            float angle = startAngle + i * angleStep;
            Vector2 dir = Quaternion.Euler(0, 0, angle) * Vector2.down;
            CreateBullet(dir.normalized);
        }
    }

    GameObject CreateBullet(Vector2 direction)
    {
        Vector3 spawnPos = firePoint.position + (Vector3)(direction.normalized * 0.2f);
        GameObject bullet = Instantiate(bulletPrefab, spawnPos, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
            rb.velocity = direction * bulletSpeed;
        return bullet;
    }
}




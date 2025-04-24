using UnityEngine;
using System.Collections;

/// <summary>
/// 일정 시간마다 아이템을 랜덤하게 생성하는 스크립트
/// 확률에 따라 회복 아이템 또는 탄환 강화 아이템을 생성함
/// </summary>
public class ItemSpawner_ : MonoBehaviour
{
    [Header("🕒 생성 주기 설정")]
    public float spawnInterval = 8f;   // 몇 초마다 아이템 생성 시도

    [Header("🗺️ 생성 위치 범위")]
    public Vector2 spawnAreaMin = new Vector2(-3.4f, -1.8f);
    public Vector2 spawnAreaMax = new Vector2(3.4f, 1.8f);

    [Header("🎁 아이템 프리팹")]
    public GameObject healthItemPrefab;        // 체력 회복 아이템
    public GameObject bulletPowerupPrefab;     // 강화 탄환 아이템

    [Header("🎲 아이템 확률 (0~100)")]
    [Range(0, 100)] public int bulletPowerupChance = 30; // 강화 아이템이 나올 확률 (%)

    void Start()
    {
        // 아이템 생성 루틴 시작
        StartCoroutine(SpawnItemRoutine());
    }

    IEnumerator SpawnItemRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);

            if (GameManager.isGameOver) continue;

            // 랜덤 위치 생성
            Vector2 spawnPos = new Vector2(
                Random.Range(spawnAreaMin.x, spawnAreaMax.x),
                Random.Range(spawnAreaMin.y, spawnAreaMax.y)
            );

            // 0~100 사이에서 무작위 숫자를 뽑아 확률 체크
            int rand = Random.Range(0, 100);

            if (rand < bulletPowerupChance)
            {
                // 💥 탄환 강화 아이템 생성
                Instantiate(bulletPowerupPrefab, spawnPos, Quaternion.identity);
            }
            else
            {
                // 🍎 회복 아이템 생성
                Instantiate(healthItemPrefab, spawnPos, Quaternion.identity);
            }
        }
    }
}


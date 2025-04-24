using UnityEngine;
using System.Collections;

public class EnemyRespawn : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform[] spawnPoints;
    public float respawnDelay = 1f;
    public int maxEnemies = 6;

    void Start()
    {
        StartCoroutine(RespawnOnce());
    }

    IEnumerator RespawnOnce()
    {
        int spawnCount = Random.Range(3, maxEnemies + 1); // 3~6 랜덤 생성
        Debug.Log($"🎮 이번 게임의 적 수: {spawnCount}");

        for (int i = 0; i < spawnCount; i++)
        {
            int index = Random.Range(0, spawnPoints.Length);
            Transform point = spawnPoints[index];

            GameObject enemy = Instantiate(enemyPrefab, point.position, Quaternion.identity);

            GameManager.totalEnemyCount++; // ✅ 생성 수 기록

            Enemy enemyScript = enemy.GetComponent<Enemy>();
            if (enemyScript != null)
                enemyScript.enemyRespawnManager = this;

            EnemyShooting shooting = enemy.GetComponent<EnemyShooting>();
            if (shooting != null)
            {
                int patternCount = System.Enum.GetValues(typeof(EnemyShooting.ShotType)).Length;
                shooting.shotType = (EnemyShooting.ShotType)Random.Range(0, patternCount);

                PatternSettings preset = EnemyShooting.GetDefaultSettings(shooting.shotType);
                shooting.bulletCount = preset.bulletCount;
                shooting.angleStep = preset.angleStep;
                shooting.fireRate = preset.fireRate;
                shooting.bulletSpeed = preset.bulletSpeed;
                shooting.rotateSpeed = preset.rotateSpeed;
                shooting.burstDelay = preset.burstDelay;
                shooting.zigzagFrequency = preset.zigzagFrequency;
                shooting.zigzagMagnitude = preset.zigzagMagnitude;
            }

            yield return new WaitForSeconds(respawnDelay);
        }
    }
}












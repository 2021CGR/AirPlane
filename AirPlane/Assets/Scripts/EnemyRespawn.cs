using UnityEngine;
using System.Collections;

public class EnemyRespawn : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform respawnPoint;
    public float respawnDelay = 3f;
    public int maxEnemies = 5;

    private int currentEnemyCount = 0;

    void Start()
    {
        RespawnEnemy();
    }

    public void OnEnemyDeath()
    {
        currentEnemyCount = Mathf.Max(0, currentEnemyCount - 1);
        RespawnEnemy();
    }

    public void RespawnEnemy()
    {
        StartCoroutine(RespawnCoroutine());
    }

    IEnumerator RespawnCoroutine()
    {
        yield return new WaitForSeconds(respawnDelay);

        if (currentEnemyCount < maxEnemies)
        {
            GameObject enemy = Instantiate(enemyPrefab, respawnPoint.position, Quaternion.identity);
            currentEnemyCount++;

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
        }
    }
}




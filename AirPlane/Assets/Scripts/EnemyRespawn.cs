using UnityEngine;
using System.Collections;

public class EnemyRespawn : MonoBehaviour
{
    public GameObject enemyPrefab;      // 적 프리팹
    public Transform respawnPoint;      // 리스폰 위치
    public float respawnDelay = 3.0f;   // 리스폰 대기 시간
    public int maxEnemies = 5;          // 최대 적 개수
    private int currentEnemyCount = 0;  // 현재 적 개수

    private void Start()
    {
        // 초기 적 1마리 생성 (선택 사항)
        RespawnEnemy();
    }

    public void RespawnEnemy()
    {
        // 코루틴을 통해 일정 시간 후 리스폰
        StartCoroutine(RespawnCoroutine());
    }

    private IEnumerator RespawnCoroutine()
    {
        yield return new WaitForSeconds(respawnDelay);

        if (currentEnemyCount < maxEnemies)
        {
            GameObject enemy = Instantiate(enemyPrefab, respawnPoint.position, Quaternion.identity);

            // 적이 자신의 RespawnManager를 알 수 있게 전달
            Enemy enemyScript = enemy.GetComponent<Enemy>();
            if (enemyScript != null)
            {
                enemyScript.enemyRespawnManager = this;
            }

            currentEnemyCount++;
        }
    }

    // 적이 죽었을 때 호출되는 함수
    public void OnEnemyDeath()
    {
        currentEnemyCount = Mathf.Max(0, currentEnemyCount - 1);

        // 적이 죽었으니 새로 리스폰
        RespawnEnemy();
    }
}

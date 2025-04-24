using UnityEngine;
using System.Collections;

/// <summary>
/// 적을 일정 시간마다 리스폰시키는 매니저 스크립트
/// 리스폰된 적에게 랜덤한 탄막 패턴을 설정함
/// </summary>
public class EnemyRespawn : MonoBehaviour
{
    [Header("🎮 리스폰 설정")]
    public GameObject enemyPrefab;      // 생성할 적 프리팹 (EnemyShooting_HG 포함)
    public Transform respawnPoint;      // 생성 위치
    public float respawnDelay = 3f;     // 리스폰 대기 시간
    public int maxEnemies = 5;          // 동시에 존재할 수 있는 적 수

    private int currentEnemyCount = 0;  // 현재 존재 중인 적 수

    private void Start()
    {
        // 시작 시 첫 적 리스폰 (옵션)
        RespawnEnemy();
    }

    /// <summary>
    /// 외부에서 적이 죽었을 때 호출 → 새 적을 리스폰 예약
    /// </summary>
    public void OnEnemyDeath()
    {
        currentEnemyCount = Mathf.Max(0, currentEnemyCount - 1);
        RespawnEnemy();
    }

    /// <summary>
    /// 리스폰을 시작하는 함수 (코루틴 사용)
    /// </summary>
    public void RespawnEnemy()
    {
        StartCoroutine(RespawnCoroutine());
    }

    /// <summary>
    /// 리스폰 딜레이 후 적을 생성하고, 탄막 패턴을 랜덤으로 설정
    /// </summary>
    IEnumerator RespawnCoroutine()
    {
        yield return new WaitForSeconds(respawnDelay);

        // 현재 적 수가 제한보다 작을 때만 생성
        if (currentEnemyCount < maxEnemies)
        {
            // 적 생성
            GameObject enemy = Instantiate(enemyPrefab, respawnPoint.position, Quaternion.identity);

            // Enemy.cs에 리스폰 매니저 연결
            Enemy enemyScript = enemy.GetComponent<Enemy>();
            if (enemyScript != null)
            {
                enemyScript.enemyRespawnManager = this;
            }

            // 🔁 탄막 패턴을 랜덤으로 설정 (7종 모두 포함)
            EnemyShooting shootingScript = enemy.GetComponent<EnemyShooting>();
            if (shootingScript != null)
            {
                int patternCount = System.Enum.GetValues(typeof(EnemyShooting.ShotType)).Length;
                shootingScript.shotType = (EnemyShooting.ShotType)Random.Range(0, patternCount);

                Debug.Log($"💥 리스폰된 적의 탄막 타입: {shootingScript.shotType}");
            }

            // 적 수 증가
            currentEnemyCount++;
        }
    }
}


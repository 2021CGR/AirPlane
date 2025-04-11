using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 3;
    public EnemyRespawn enemyRespawnManager;

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("적 데미지: " + damage + ", 현재 체력: " + health);

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("적이 죽었습니다!");

        if (enemyRespawnManager != null)
        {
            enemyRespawnManager.OnEnemyDeath();
        }

        Destroy(gameObject);
    }
}

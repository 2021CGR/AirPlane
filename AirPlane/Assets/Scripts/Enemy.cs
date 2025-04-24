using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 3;
    public EnemyRespawn enemyRespawnManager;

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("💥 적 사망");
        GameManager.enemyKillCount++; // ✅ 적 사망 시 카운트 증가

        Destroy(gameObject);
    }
}


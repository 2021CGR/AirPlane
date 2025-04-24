using UnityEngine;

/// <summary>
/// 플레이어가 먹으면 10초 동안 총알이 강화되는 아이템
/// </summary>
public class BulletPowerup : MonoBehaviour
{
    public GameObject poweredBulletPrefab; // 강화된 총알 프리팹
    public float powerupDuration = 10f;    // 지속 시간

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerShooting shooter = collision.GetComponent<PlayerShooting>();
            if (shooter != null)
            {
                shooter.ActivateBulletPowerup(poweredBulletPrefab, powerupDuration);
                Debug.Log("🔵 강화 총알 발동!");
            }

            Destroy(gameObject); // 아이템은 먹자마자 사라짐
        }
    }
}

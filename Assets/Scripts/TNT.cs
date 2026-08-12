using UnityEngine;

public class TNTLogic : MonoBehaviour
{
    [Header("TNT Targets")]
    [SerializeField] private GameObject[] targetEnemy;


    [SerializeField] private GameObject[] targetWalls;

    [Header("Settings")]
    [SerializeField] private string arrowTag = "Arrow";

    [Header("Explosion Effects (Optional)")]
    [SerializeField] private GameObject explosionEffectPrefab;
    [SerializeField] private float destroyDelay = 0.1f;

    private bool isExploded = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(arrowTag) && !isExploded)
        {
            Explode();
        }
    }

    private void Explode()
    {
        isExploded = true;


        if (targetEnemy != null)
        {
            foreach (GameObject enemy in targetEnemy)
            {
                if (enemy == null) continue;

                IDeath enemyDeath = enemy.GetComponent<IDeath>();
                if (enemyDeath != null)
                {
                    enemyDeath.Die();
                }
                else
                {
                    Destroy(enemy);
                }
            }
        }


        if (targetWalls != null)
        {
            foreach (GameObject wall in targetWalls)
            {
                if (wall != null)
                {
                    Destroy(wall);
                }
            }
        }


        if (explosionEffectPrefab != null)
        {
            Instantiate(explosionEffectPrefab, transform.position, Quaternion.identity);
        }


        Destroy(gameObject, destroyDelay);
    }
}
using UnityEngine;

public class TNTLogic : MonoBehaviour
{
    [Header("TNT Settings")]
    [Tooltip("العدو المباشر اللي عايزة القنبلة تدمره (اختياري)")]
    [SerializeField] private GameObject targetEnemy;

    [Tooltip("تاغ السهم اللي لما يلمس القنبلة تنفجر")]
    [SerializeField] private string arrowTag = "Arrow"; // غيري التاج على حسب مشروعك

    [Header("Explosion Effects (Optional)")]
    [SerializeField] private GameObject explosionEffectPrefab;
    [SerializeField] private float destroyDelay = 0.1f;

    private bool isExploded = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // التأكد إن اللي لمس القنبلة هو السهم وإنها مانفجرتش قبل كده
        if (collision.CompareTag(arrowTag) && !isExploded)
        {
            Explode();
        }
    }

    private void Explode()
    {
        isExploded = true;

        // 1. تدمير العدو المفيّد في الـ Inspector لو موجود
        if (targetEnemy != null)
        {
            // البحث عن كلاس العدو أو الإنترفيس IDeath وتفعيل دالة الموت
            IDeath enemyDeath = targetEnemy.GetComponent<IDeath>();
            if (enemyDeath != null)
            {
                enemyDeath.Die();
            }
            else
            {
                // لو العدو معندوش IDeath هيتم مسحه مباشرة
                Destroy(targetEnemy);
            }
        }

        // 2. إظهار تأثير الانفجار لو حاطة Prefab للانفجار
        if (explosionEffectPrefab != null)
        {
            Instantiate(explosionEffectPrefab, transform.position, Quaternion.identity);
        }

        // 3. تدمير القنبلة نفسها
        Destroy(gameObject, destroyDelay);
    }
}
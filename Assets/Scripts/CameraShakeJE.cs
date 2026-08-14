using UnityEngine;
using System.Collections;

public class CameraShakeJE : MonoBehaviour
{
    public static CameraShakeJE Instance { get; private set; }

    [SerializeField] private float shakeDuration = 0.2f;
    [SerializeField] private float shakeMagnitude = 0.15f;
    [SerializeField] private float dampingSpeed = 1.5f;

    private Vector3 originalPosition;
    private Coroutine shakeCoroutine;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        originalPosition = transform.localPosition;
    }

    public void Shake()
    {
        Shake(shakeDuration, shakeMagnitude);
    }

    public void Shake(float duration, float magnitude)
    {
        if (shakeCoroutine != null)
            StopCoroutine(shakeCoroutine);

        shakeCoroutine = StartCoroutine(ShakeCoroutine(duration, magnitude));
    }

  
    private IEnumerator ShakeCoroutine(float duration, float magnitude)
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float strength = Mathf.Lerp(magnitude, 0f, elapsed / duration);

            Vector2 offset = Random.insideUnitCircle * strength;

            transform.localPosition = originalPosition +
                                       new Vector3(offset.x, offset.y, 0f);

            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = originalPosition;
        shakeCoroutine = null;
    }

}

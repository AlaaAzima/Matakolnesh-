using UnityEngine;
using System.Collections.Generic;

public class ArrowPool : MonoBehaviour
{
    public static ArrowPool Instance { get; private set; }

    [SerializeField] private Arrow arrowPrefab;
    [SerializeField] private int initialPoolSize = 10;

    private readonly Queue<Arrow> availableArrows = new Queue<Arrow>();
    private Transform poolContainer;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        poolContainer = new GameObject("ArrowPool_Container").transform;
        poolContainer.SetParent(transform);

        for (int i = 0; i < initialPoolSize; i++)
        {
            Arrow arrow = CreateNewArrow();
            availableArrows.Enqueue(arrow);
        }
    }

    private Arrow CreateNewArrow()
    {
        Arrow arrow = Instantiate(arrowPrefab, poolContainer);
        arrow.gameObject.SetActive(false);
        return arrow;
    }

    public Arrow GetArrow(Vector3 position, Quaternion rotation)
    {
        Arrow arrow;

        if (availableArrows.Count > 0)
        {
            arrow = availableArrows.Dequeue();
        }
        else
        {
            Debug.LogWarning("[ArrowPool] Pool exhausted — growing pool by 1. " +
                              "Consider raising Initial Pool Size in the Inspector.");
            arrow = CreateNewArrow();
        }

        arrow.transform.SetParent(null);
        arrow.transform.SetPositionAndRotation(position, rotation);
        arrow.gameObject.SetActive(true);
        arrow.OnSpawnFromPool();

        return arrow;
    }

    public void ReturnArrow(Arrow arrow)
    {
        arrow.gameObject.SetActive(false);
        arrow.transform.SetParent(poolContainer);
        availableArrows.Enqueue(arrow);
    }
}
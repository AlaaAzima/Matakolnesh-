using UnityEngine;
using System;
public class StarRatingJE : MonoBehaviour
{
    [SerializeField] private UIHandlerJE uiHandler;
    public int arrowsUsed;
    public static event Action<int> OnArrowCountChanged;
    [SerializeField] int stars3 = 10;
    [SerializeField] int stars2 = 6;
    void Start()
    {
        uiHandler = FindFirstObjectByType<UIHandlerJE>();
    }
    public void ArrowShot()
    {
        arrowsUsed++;
        Debug.Log("Arrows Used: " + arrowsUsed);
        OnArrowCountChanged?.Invoke(arrowsUsed);
    }

    public int CalculateStars()
    {
        if (arrowsUsed <= stars3)
            return 3;

        if (arrowsUsed <= stars2)
            return 2;

        return 1;
    }

    public void ResetCounter()
    {
        arrowsUsed = 0;
        Debug.Log("Arrows Used Reset to: " + arrowsUsed);
        OnArrowCountChanged?.Invoke(arrowsUsed);
    }
}

using UnityEngine;

public class StarRatingJE : MonoBehaviour
{
    private int arrowsUsed;

    public void ArrowShot()
    {
        arrowsUsed++;
        Debug.Log("Arrows Used: " + arrowsUsed);
    }

    public int CalculateStars()
    {
        if (arrowsUsed <= 4)
            return 3;

        if (arrowsUsed <= 8)
            return 2;

        return 1;
    }

    public void ResetCounter()
    {
        arrowsUsed = 0;

        Debug.Log("Arrows Used Reset to: " + arrowsUsed);

    }
}

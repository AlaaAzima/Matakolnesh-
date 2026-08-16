using UnityEngine;
using System;
public class UnlockFiveLevels : MonoBehaviour
{
    [SerializeField] int neededStars;
    int playerStars;

    void Start()
    {
        if (GameManagerJE.Instance != null && GameManagerJE.Instance.gameData != null)
        {
            playerStars = 0;
            foreach (int stars in GameManagerJE.Instance.gameData.levelStars)
            {
                playerStars += stars;
            }
        }

        if (neededStars <= playerStars)
        {
            gameObject.SetActive(false);
        }

    }
}

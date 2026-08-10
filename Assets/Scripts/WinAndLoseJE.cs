using UnityEngine;

public class WinAndLoseJE : MonoBehaviour
{
    public bool CheckWin()

    {

        if (GameManagerJE.Instance.remainingEnemies <= 0 &&

            !GameManagerJE.Instance.isGameOver &&
            GameManagerJE.Instance.activeArrowCount <= 0)

        {

            Debug.Log("YOU WIN!");

            return true;

        }
        return false;
    }

    public bool CheckLose()

    {

        if (GameManagerJE.Instance.isGameOver)

        {

            Debug.Log("YOU LOSE!");

            return true;

        }

        return false;
    }


}

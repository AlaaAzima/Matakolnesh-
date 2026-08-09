using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIHandlerJE : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI arrowCounter;


    private void OnEnable()
{
    StarRatingJE.OnArrowCountChanged += ArrowCounterUpdateJE;
}

private void OnDisable()
{
    StarRatingJE.OnArrowCountChanged -= ArrowCounterUpdateJE;
}

private void ArrowCounterUpdateJE(int arrowCount)
{
    arrowCounter.text = "×" + arrowCount;
}

}

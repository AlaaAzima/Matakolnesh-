using UnityEngine;
using System.Collections;

public class StartInstructionUI : MonoBehaviour
{
    [SerializeField] private GameObject instructionPanel;
    [SerializeField] private float time ;

    private void Start()
    {
        instructionPanel.SetActive(true);
        StartCoroutine(HideInstructions());
    }

    private IEnumerator HideInstructions()
    {
        yield return new WaitForSecondsRealtime(time);

        instructionPanel.SetActive(false);
    }
}
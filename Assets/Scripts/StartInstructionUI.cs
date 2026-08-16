using UnityEngine;
using System.Collections;

public class StartInstructionUI : MonoBehaviour
{
    [SerializeField] private GameObject instructionPanel;

    private void Start()
    {
        instructionPanel.SetActive(true);
        StartCoroutine(HideInstructions());
    }

    private IEnumerator HideInstructions()
    {
        yield return new WaitForSecondsRealtime(2f);

        instructionPanel.SetActive(false);
    }
}
using UnityEngine;
using UnityEngine.UI;

public class ButtonSound : MonoBehaviour
{
    private void Awake()

    {

        GetComponent<Button>().onClick.AddListener(PlayButtonSound);

    }

    private void PlayButtonSound()

    {

        SoundManager.PlaySound(SoundType.PressButton);

    }

}

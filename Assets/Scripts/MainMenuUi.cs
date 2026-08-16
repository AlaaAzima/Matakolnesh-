using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button quitButton;

    [SerializeField] private Image musicIcon;

    [SerializeField] private Sprite musicOnSprite;

    [SerializeField] private Sprite musicOffSprite;

     [SerializeField] private Image sfxIcon;

    [SerializeField] private Sprite sfxOnSprite;

    [SerializeField] private Sprite sfxOffSprite;

    private void Awake()
    {
        playButton.onClick.AddListener(() =>
        {
            Loader.Load(Loader.Scene.StageMenu);

        });
        quitButton.onClick.AddListener(() =>
        {
            Application.Quit();
        });
        Time.timeScale = 1f;
    }


    private void Start()
    {
        UpdateMusicIcon();
         UpdateSFXicon();

    }

    public void ToggleMusic()

    {
        SoundManager.ToggleMusicMute();
        UpdateMusicIcon();
    }

    private void UpdateMusicIcon()

    {
        musicIcon.sprite = SoundManager.IsMusicMuted()
            ? musicOffSprite
            : musicOnSprite;

    }

       
    public void ToggleSFX()
    {
        SoundManager.ToggleSFXMute();
         UpdateSFXicon();
    }

    private void UpdateSFXicon()

    {

        sfxIcon.sprite = SoundManager.IsSFXMuted()

            ? sfxOffSprite

            : sfxOnSprite;

    }

}


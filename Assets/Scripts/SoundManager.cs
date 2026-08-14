using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public enum SoundType
{  
    PlayerShoot,     //done
    PlayerDealth,     //done
    SpoonCollison,  //done
    EnemyDeath,      //done
    ObstacaleInteraction,       
    TNT,   //done
    WinSound,       //done
    LoseSound,         //done       
    PressButton,
    stars        

}

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    [Header("Clips")]
    [SerializeField] private AudioClip[] soundList;

    [Header("Music Playlist")]
    [SerializeField] private AudioClip[] musicList;

    [Header("Audio Sources")]
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioSource musicSource;

    private int currentMusicIndex;
    private bool playlistActive;
    private bool musicStopped = false;


    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        StartPlaylist();
         //AddButtonSounds();
    }

    private void Update()
    {
        HandlePlaylist();
    }

    private void OnEnable()
{
    SceneManager.sceneLoaded += OnSceneLoaded;
}

private void OnDisable()
{
    SceneManager.sceneLoaded -= OnSceneLoaded;
}

private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
{

    if (scene.name == "MainMenu" || scene.name == "StageMenu")
    {
        if (!playlistActive) StartPlaylist();
    }
    else
    {
        StopMusic();
    }
}

// private void AddButtonSounds()
// {
//     Button[] buttons = FindObjectsByType<Button>(
//         FindObjectsInactive.Include,
//         FindObjectsSortMode.None
//     );

//     foreach (Button button in buttons)
//     {
//         button.onClick.AddListener(() =>
//         {
//             PlaySound(SoundType.PressButton);
//         });
//     }
// }

    /*  PLAYLIST  */

    private void StartPlaylist()
    {
        if (musicList.Length == 0)
            return;

        playlistActive = true;
        currentMusicIndex = Mathf.Clamp(currentMusicIndex, 0, musicList.Length - 1);

        PlayCurrentTrack();
    }

    private void PlayCurrentTrack()
    {
        musicSource.clip = musicList[currentMusicIndex];
        musicSource.loop = false;
        musicSource.Play();
    }

    private void HandlePlaylist()
    {
        if (!playlistActive || musicStopped)
            return;

        if (!musicSource.isPlaying && musicSource.time == 0f)
        {
            PlayNextTrack();
        }
    }

    private void PlayNextTrack()
    {
        currentMusicIndex = (currentMusicIndex + 1) % musicList.Length;
        PlayCurrentTrack();
    }

    /*  SFX  */

    public static void PlaySound(SoundType sound, float volume = 1f)
    {
        if (Instance == null) return;

        Instance.sfxSource.PlayOneShot(
            Instance.soundList[(int)sound],
            volume
        );
    }

    /*  VOLUME  */

    public static void SetMusicVolume(float volume)
    {
        if (Instance == null) return;
        Instance.musicSource.volume = volume;
    }

    public static void SetSFXVolume(float volume)
    {
        if (Instance == null) return;
        Instance.sfxSource.volume = volume;
    }


    public static void StopMusic()
    {
        if (Instance == null) return;

        Instance.musicStopped = true;
        Instance.playlistActive = false;

        Instance.musicSource.Stop();
    }

    public static void ResumeMusic()
    {
        if (Instance == null) return;

        Instance.musicStopped = false;
        Instance.playlistActive = true;
        Instance.PlayCurrentTrack();
    }



    public float InstanceMusicVolume => musicSource.volume;
    public float InstanceSFXVolume => sfxSource.volume;
}



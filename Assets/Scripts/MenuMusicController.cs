using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class MenuMusicController : MonoBehaviour
{
    [SerializeField] private string[] allowedScenes = { "MainMenu","LoadingScene", "StageMenu" };

    private AudioSource audioSource;
    private static MenuMusicController instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        audioSource = GetComponent<AudioSource>();
        audioSource.loop = true;          // <-- was missing: music now loops instead of playing once
        audioSource.playOnAwake = false;  // we control playback manually

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void Start()
    {
        // sceneLoaded only fires for scenes loaded AFTER we subscribe,
        // so the scene we booted into is missed unless we check it manually.
        OnSceneLoaded(SceneManager.GetActiveScene(), LoadSceneMode.Single);
    }

    private void OnDestroy()
    {
        if (instance == this)
            SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        bool shouldPlay = System.Array.IndexOf(allowedScenes, scene.name) >= 0;

        if (shouldPlay)
        {
            // Only (re)start if it isn't already playing - this is what keeps
            // the track continuous across MainMenu <-> StageMenu transitions
            // instead of restarting from 0 every scene load.
            if (!audioSource.isPlaying)
                audioSource.Play();
        }
        else
        {
            if (audioSource.isPlaying)
                audioSource.Stop();
        }
    }
}
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraFadeJZ : MonoBehaviour
{
    public static CameraFadeJZ Instance { get; private set; }
    public float speedScale = 1f;
    public Color fadeColor = Color.black;
    public AnimationCurve curve = new AnimationCurve(
        new Keyframe(0, 1),
        new Keyframe(0.5f, 0.5f, -1.5f, -1.5f),
        new Keyframe(1, 0)
    );

    [Header("Timing Configurations")]
    public float startDelayDuration = 2f; // <-- Time to wait BEFORE fading out
    public float holdDuration = 0.5f;     // Time to stay black before reload

    private float alpha = 0f;
    private Texture2D texture;
    private int direction = 0;
    private float time = 0f;
    private bool reloadPending = false;
    private float holdTimer = 0f;
    private float delayTimer = 0f;         // <-- Tracks the initial delay
    private bool isWaitingToFade = false;  // <-- State flag for the delay

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        texture = new Texture2D(1, 1);
        SetAlpha(1f);   // start fully black
        time = 0f;
        direction = 1;  // fade OUT on game start
        SoundManager.PlaySound(SoundType.LoseSound);

    }

    public void TriggerDeathSequence()
    {
        reloadPending = true;
        holdTimer = 0f;

        // Start the 2-second waiting period instead of fading immediately
        delayTimer = 0f;
        isWaitingToFade = true;
        direction = 0;
    }

    private void SetAlpha(float a)
    {
        alpha = a;
        texture.SetPixel(0, 0, new Color(fadeColor.r, fadeColor.g, fadeColor.b, alpha));
        texture.Apply();
    }

    public void OnGUI()
    {
        if (alpha > 0f) GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), texture);

        // Phase 1: Wait for 2 seconds before doing anything
        if (isWaitingToFade)
        {
            delayTimer += Time.unscaledDeltaTime;
            if (delayTimer >= startDelayDuration)
            {
                isWaitingToFade = false;
                time = 1f;
                direction = -1; // Now start fading IN to black
            }
            return;
        }

        // Phase 2: Handle the active fade transition
        if (direction != 0)
        {
            time += direction * Time.unscaledDeltaTime * speedScale;
            time = Mathf.Clamp01(time);
            SetAlpha(curve.Evaluate(time));

            if (direction == 1 && alpha <= 0f)
            {
                direction = 0;
            }
            else if (direction == -1 && alpha >= 1f)
            {
                direction = 0;
                if (reloadPending) holdTimer = 0f;
            }
        }
        // Phase 3: Screen is fully black, hold briefly then reload
        else if (reloadPending)
        {
            holdTimer += Time.unscaledDeltaTime;
            if (holdTimer >= holdDuration)
            {
                reloadPending = false;
                Time.timeScale = 1f;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
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
        SetAlpha(1f);
        time = 0f;
        direction = 1; // fade OUT completely on every scene load
        isWaitingToFade = false;
    }
}
using UnityEngine;
using System.Collections.Generic;

public class VFXManager : MonoBehaviour
{
    public static VFXManager Instance { get; private set; }

    [System.Serializable]
    public struct VFXMapping
    {
        public VFXType type;
        public ParticleSystem prefab;
    }

    [Header("VFX Prefabs")]
    [Tooltip("Map each VFXType to a ParticleSystem prefab here.")]
    [SerializeField] private VFXMapping[] vfxMappings;

    private Dictionary<VFXType, ParticleSystem> vfxDictionary;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            // DontDestroyOnLoad(gameObject); // Uncomment if manager is persistent across scenes
            InitializeDictionary();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void InitializeDictionary()
    {
        vfxDictionary = new Dictionary<VFXType, ParticleSystem>();
        foreach (var mapping in vfxMappings)
        {
            if (!vfxDictionary.ContainsKey(mapping.type))
            {
                vfxDictionary.Add(mapping.type, mapping.prefab);
            }
        }
    }

    private void OnEnable()
    {
        GameEvents.OnPlayVFX += HandlePlayVFX;
    }

    private void OnDisable()
    {
        GameEvents.OnPlayVFX -= HandlePlayVFX;
    }

    private void HandlePlayVFX(VFXType type, Vector3 position)
    {
        if (vfxDictionary != null && vfxDictionary.TryGetValue(type, out ParticleSystem prefab))
        {
            if (prefab != null)
            {
                // Note: For a massive scale game, we would use Object Pooling here (like ArrowPool).
                // For now, Instantiate is used, and the ParticleSystem must be set to "Stop Action -> Destroy" in Unity.
                ParticleSystem vfxInstance = Instantiate(prefab, position, Quaternion.identity);

                // Fallback in case "Stop Action -> Destroy" is not set in the inspector.
                // It destroys the GameObject after the duration of the particle system.
                var main = vfxInstance.main;
                Destroy(vfxInstance.gameObject, main.duration + main.startLifetime.constantMax);
            }
            else
            {
                Debug.LogWarning($"[VFXManager] Prefab for {type} is null! Please assign it in the Inspector.");
            }
        }
        else
        {
            Debug.LogWarning($"[VFXManager] Unhandled VFX Type: {type}. Add it to the VFXManager's array in the Inspector.");
        }
    }
}

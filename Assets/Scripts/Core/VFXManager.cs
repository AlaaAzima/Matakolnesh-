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
    [SerializeField] private VFXMapping[] vfxMappings;

    private Dictionary<VFXType, ParticleSystem> vfxDictionary;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
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
            if (mapping.prefab != null && !vfxDictionary.ContainsKey(mapping.type))
            {
                vfxDictionary.Add(mapping.type, mapping.prefab);
            }
        }
    }

    private void OnEnable()
    {

        GameEvents.OnPlayVFX -= HandlePlayVFX;
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

                ParticleSystem vfxInstance = Instantiate(prefab, position, Quaternion.identity);


                if (type == VFXType.StarAchieved || type == VFXType.GameWin || type == VFXType.GameLose)
                {
                    Canvas currentCanvas = FindObjectOfType<Canvas>();
                    if (currentCanvas != null)
                    {
                        vfxInstance.transform.SetParent(currentCanvas.transform, false);
                        vfxInstance.transform.position = position;
                    }
                }

                var main = vfxInstance.main;
                Destroy(vfxInstance.gameObject, main.duration + main.startLifetime.constantMax);
            }
        }
    }
}
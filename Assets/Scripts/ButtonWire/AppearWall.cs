using UnityEngine;

public class AppearWall : MonoBehaviour
{
    private ButtonJZ button;
    [SerializeField] GameObject[] wall;

    private void Awake()
    {
        button = GetComponent<ButtonJZ>();
    }
    private void OnEnable()
    {
        button.OnButtonClick += Appear;
    }

    private void OnDisable()
    {
        button.OnButtonClick -= Appear;
    }

    public void Appear()
    {
        for (int i = 0; i < wall.Length; i++)
        {
            wall[i].SetActive(true);
            GameEvents.TriggerPlayVFX(VFXType.WallAppear, wall[i].transform.position);
        }
    }
}

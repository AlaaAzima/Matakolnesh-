using UnityEngine;

public class DisappearWall : MonoBehaviour
{
    private ButtonJZ button;
    [SerializeField] GameObject[] wall;

    private void Awake()
    {
        button = GetComponent<ButtonJZ>();
    }
    private void OnEnable()
    {
        button.OnButtonClick += Disappear;
    }

    private void OnDisable()
    {
        button.OnButtonClick -= Disappear;
    }

    public void Disappear()
    {
        for (int i = 0; i < wall.Length; i++)
        {
            wall[i].SetActive(false);
        }
    }
}

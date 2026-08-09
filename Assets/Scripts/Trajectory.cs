using UnityEngine;

public class TrajectoryPredictor : MonoBehaviour
{
    [SerializeField] private GameObject pointPrefab;
    [SerializeField] private int numberOfPoints = 10;
    [SerializeField] private float spaceBetweenPoints = 0.1f;

    private GameObject[] points;

    void Start()
    {
        points = new GameObject[numberOfPoints];
        for (int i = 0; i < numberOfPoints; i++)
        {
            points[i] = Instantiate(pointPrefab, transform.position, Quaternion.identity);
            points[i].SetActive(false);
        }
    }

    public void ShowTrajectory(Transform shotPoint, float arrowSpeed)
    {
        SetTrajectoryVisible(true);
        for (int i = 0; i < numberOfPoints; i++)
        {
            float t = i * spaceBetweenPoints;
            points[i].transform.position = PointPosition(shotPoint, arrowSpeed, t);
        }
    }

    public void HideTrajectory()
    {
        SetTrajectoryVisible(false);
    }

    private void SetTrajectoryVisible(bool visible)
    {
        for (int i = 0; i < numberOfPoints; i++)
        {
            points[i].SetActive(visible);
        }
    }

    private Vector2 PointPosition(Transform shotPoint, float arrowSpeed, float t)
    {
        return (Vector2)shotPoint.position + ((Vector2)shotPoint.right * arrowSpeed * t);
    }
}
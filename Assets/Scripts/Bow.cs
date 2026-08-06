using UnityEngine;
public class Bow : MonoBehaviour
{
    //[SerializeField] private StarRatingJE starRatingJE;
    [SerializeField] private GameObject arrowPrefab; // Reference to the arrow prefab
    [SerializeField] private Transform shotPoint;   // Reference to the shot point
    [SerializeField] private float arrowSpeed = 20f; // Speed of the arrow
    //======================================
    [SerializeField] private GameObject point;
    private GameObject[] points;
    [SerializeField] private int numberOfPoints = 10;
    [SerializeField] private float spaceBetweenPoints = 0.1f;
    //======================================
    private void Start()
    {
        points = new GameObject[numberOfPoints];
        for (int i = 0; i < numberOfPoints; i++)
        {
            points[i] = Instantiate(point, shotPoint.position, Quaternion.identity);
        }
    }
    void SetTrajectoryVisible(bool visible)
    {
        for (int i = 0; i < numberOfPoints; i++)
        {
            points[i].SetActive(visible);
        }
    }
    void Update()
    {
        if (GameManagerJE.Instance.isGameOver || PauseSystemJZ.Instance.IsPaused) return;
        AimGun();
        if (Input.GetButton("Fire1"))
        {
            SetTrajectoryVisible(true);
            for (int i = 0; i < numberOfPoints; i++)
            {
                points[i].transform.position = PointPosition(i * spaceBetweenPoints);
            }
        }
        else
        {
            SetTrajectoryVisible(false);
        }
        if (Input.GetButtonUp("Fire1"))
        {
            Shoot();
        }
    }
    void AimGun()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        Vector3 direction = (mousePosition - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }
    void Shoot()
    {
        GameObject bullet = Instantiate(arrowPrefab, shotPoint.position, shotPoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        //starRatingJE.ArrowShot(); 
        GameManagerJE.Instance.ArrowShot();
        rb.linearVelocity = shotPoint.right * arrowSpeed;
        Destroy(bullet, 2f); // Destroy bullet after 2 seconds
    }
    Vector2 PointPosition(float t)
    {
        Vector2 position = (Vector2)shotPoint.position + ((Vector2)shotPoint.right * arrowSpeed * t);
        return position;
    }
}
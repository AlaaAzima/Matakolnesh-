using UnityEngine;

[RequireComponent(typeof(BowInput))]
[RequireComponent(typeof(TrajectoryPredictor))]
public class Bow : MonoBehaviour
{
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private Transform shotPoint;
    [SerializeField] private float arrowSpeed = 20f;

    private BowInput bowInput;
    private TrajectoryPredictor trajectoryPredictor;

    private void Awake()
    {
        bowInput = GetComponent<BowInput>();
        trajectoryPredictor = GetComponent<TrajectoryPredictor>();
    }

    private void OnEnable()
    {
        bowInput.OnFireHeld += HandleFireHeld;
        bowInput.OnFireReleased += Shoot;
    }

    private void OnDisable()
    {
        bowInput.OnFireHeld -= HandleFireHeld;
        bowInput.OnFireReleased -= Shoot;
    }

    void Update()
    {
        if (GameManagerJE.Instance.isGameOver || PauseSystemJZ.Instance.IsPaused) return;
        AimGun();
    }

    void AimGun()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        Vector3 direction = (mousePosition - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    void HandleFireHeld()
    {
        trajectoryPredictor.ShowTrajectory(shotPoint, arrowSpeed);
    }

    void Shoot()
    {
        trajectoryPredictor.HideTrajectory();

        GameObject arrow = Instantiate(arrowPrefab, shotPoint.position, shotPoint.rotation);
        SoundManager.PlaySound(SoundType.PlayerShoot);
        Rigidbody2D rb = arrow.GetComponent<Rigidbody2D>();

        GameManagerJE.Instance.ArrowShot();
        rb.linearVelocity = shotPoint.right * arrowSpeed;

        //Destroy(arrow, 2f);
    }
}
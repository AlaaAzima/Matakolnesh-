using UnityEngine;

public class ArcherAim : MonoBehaviour
{

    public Sprite[] bowSprites;


    public float minAngle = -45f;
    public float maxAngle = 45f;

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        AimAtMouse();
    }

    void AimAtMouse()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = transform.position.z;
        Vector2 direction = mousePos - transform.position;


        if (direction.x < 0)
        {
            spriteRenderer.flipX = true;
            direction.x = -direction.x;
        }
        else
        {
            spriteRenderer.flipX = false;
        }

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        float clampedAngle = Mathf.Clamp(angle, minAngle, maxAngle);
        float t = Mathf.InverseLerp(minAngle, maxAngle, clampedAngle);

        int index = Mathf.RoundToInt(t * (bowSprites.Length - 1));

        spriteRenderer.sprite = bowSprites[index];
       // Debug.Log("Angle: " + angle + " | Index: " + index);
    }
}
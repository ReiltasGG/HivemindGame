using UnityEngine;

public class LaserBeam : MonoBehaviour
{
    public Transform laserOrigin;
    public float laserMaxLength = 100f;
    private LineRenderer lineRenderer;
    public float timeBetweenDamage = .2f;
    public float currentTime = 0.0f;
    public int damage = 5;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
    }

    void Update()
    {
        Vector3 endPosition = laserOrigin.position + laserOrigin.up * laserMaxLength;
        lineRenderer.SetPosition(0, laserOrigin.position);

        RaycastHit2D hit = Physics2D.Raycast(laserOrigin.position, laserOrigin.up, laserMaxLength);

        if (hit.collider != null)
        {
            endPosition = hit.point;
            HandleCollision(hit.collider.gameObject);
        }

        lineRenderer.SetPosition(1, endPosition);
    }

    void HandleCollision(GameObject hitObject)
    {
        // Do something when a collision occurs, e.g., damage the hitObject or play an effect.
        // You can also destroy the laser if it hits an object, depending on your game logic.
        if(currentTime > timeBetweenDamage)
        {
        var attMan = hitObject.GetComponent<AttributesManager>();
        attMan.takeDamage(damage);
        currentTime = 0.0f;
        }
        currentTime += Time.deltaTime;
      

    }
}

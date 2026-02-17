using UnityEngine;

public class BossShooter : MonoBehaviour
{
    public Transform shootPoint;

    public GameObject projectilePrefab;
    public Sprite projectileSprite;
    public float projectileSpeed = 8f;
    public float shootInterval = 1.2f;

    private float timer;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= shootInterval)
        {
            timer = 0f;
            Shoot();
        }
    }

    void Shoot()
    {
        if (projectilePrefab == null || shootPoint == null) return;

        GameObject go = Instantiate(projectilePrefab, shootPoint.position, Quaternion.identity);

        Projectile p = go.GetComponent<Projectile>();

        if (p != null)
        {
            p.Init(Vector2.left, projectileSpeed, projectileSprite);
        }
    }
}

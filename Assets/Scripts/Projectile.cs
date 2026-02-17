using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Vector2 dir;
    private float speed;

    public void Init(Vector2 direction, float projectileSpeed, Sprite skin)
    {
        dir = direction.normalized;
        speed = projectileSpeed;

        var sr = GetComponent<SpriteRenderer>();
        if (sr != null && skin != null) sr.sprite = skin;
    }

    void Update()
    {
        transform.position += (Vector3)(dir * speed * Time.deltaTime);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}

using UnityEngine;

public class BossShooter : MonoBehaviour
{
    [Header("Ataque")]
    public Transform shootPoint;
    public GameObject projectilePrefab;
    public Sprite projectileSprite;
    public float projectileSpeed = 8f;
    public float shootInterval = 2f;

    private float timer;
    private Transform alvoDoTiro;

    void Start()
    {
        GameObject playerNaTela = GameObject.FindGameObjectWithTag("Player");
        if (playerNaTela != null) alvoDoTiro = playerNaTela.transform;
    }

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

        GameObject go = Instantiate(projectilePrefab, shootPoint.position, Quaternion.identity);
        Projectile p = go.GetComponent<Projectile>();

        if (p != null)
        {
            Vector2 direcao = Vector2.left; 
            if (alvoDoTiro != null) direcao = (alvoDoTiro.position - shootPoint.position).normalized;
            p.Init(direcao, projectileSpeed, projectileSprite);
        }
        else
        {
            Debug.LogWarning("ERRO: O Prefab atirado não tem o script 'Projectile' nele!");
        }
    }
}
// using UnityEngine;

// public class BossShooter : MonoBehaviour
// {
//     [Header("Ataque")]
//     public Transform shootPoint;
//     public GameObject projectilePrefab;
//     public Sprite projectileSprite;
//     public float projectileSpeed = 8f;
//     public float shootInterval = 2f;

//     private float timer;
//     private Transform alvoDoTiro;

//     void Start()
//     {
//         GameObject playerNaTela = GameObject.FindGameObjectWithTag("Player");
//         if (playerNaTela != null) alvoDoTiro = playerNaTela.transform;
//     }

//     void Update()
//     {
//         // Só atira, não anda mais!
//         timer += Time.deltaTime;
//         if (timer >= shootInterval)
//         {
//             timer = 0f;
//             Shoot();
//         }
//     }

//     void Shoot()
//     {
//         if (projectilePrefab == null || shootPoint == null) return;
//         GameObject go = Instantiate(projectilePrefab, shootPoint.position, Quaternion.identity);
//         Projectile p = go.GetComponent<Projectile>();

//         if (p != null)
//         {
//             Vector2 direcao = Vector2.left; 
//             if (alvoDoTiro != null) direcao = (alvoDoTiro.position - shootPoint.position).normalized;
//             p.Init(direcao, projectileSpeed, projectileSprite);
//         }
//     }
// }

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
        
        // Fofoca 1: O Boss avisa que nasceu e de quanto em quanto tempo ele vai atirar
        Debug.Log("Boss nasceu! Preparado para atirar a cada " + shootInterval + " segundos.");
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
        // Fofoca 2: Se não tiver munição, ele dá um aviso amarelo!
        if (projectilePrefab == null)
        {
            Debug.LogWarning("ERRO: O Boss está sem Munição (Projectile Prefab nulo)!");
            return;
        }
        
        // Fofoca 3: Se o ponto de tiro sumir, ele avisa!
        if (shootPoint == null)
        {
            Debug.LogWarning("ERRO: O Boss não sabe de onde atirar (Shoot Point nulo)!");
            return;
        }

        GameObject go = Instantiate(projectilePrefab, shootPoint.position, Quaternion.identity);
        Projectile p = go.GetComponent<Projectile>();

        if (p != null)
        {
            Vector2 direcao = Vector2.left; 
            if (alvoDoTiro != null) direcao = (alvoDoTiro.position - shootPoint.position).normalized;
            p.Init(direcao, projectileSpeed, projectileSprite);
            
            // Fofoca 4: Deu tudo certo!
            Debug.Log("Tiro disparado com sucesso!");
        }
        else
        {
            Debug.LogWarning("ERRO: O Prefab atirado não tem o script 'Projectile' nele!");
        }
    }
}
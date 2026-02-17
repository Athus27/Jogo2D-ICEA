using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public FaseConfig faseAtual;

    [Header("Onde spawnar coisas")]
    public Transform backgroundParent;
    public Transform bossParent;

    private GameObject backgroundInstance;
    private GameObject bossInstance;

    void Start()
    {
        LoadFase(faseAtual);
    }

    public void LoadFase(FaseConfig fase)
    {
        if (fase == null)
        {
            Debug.LogError("Nenhuma fase configurada!");
            return;
        }

        // Limpa fase anterior
        if (backgroundInstance != null) Destroy(backgroundInstance);
        if (bossInstance != null) Destroy(bossInstance);

        faseAtual = fase;

        // Instancia Background
        if (fase.backgroundPrefab != null)
        {
            backgroundInstance = Instantiate(
                fase.backgroundPrefab,
                backgroundParent.position,
                Quaternion.identity,
                backgroundParent
            );
        }

        // Instancia Boss
        if (fase.bossPrefab != null)
        {
            bossInstance = Instantiate(
                fase.bossPrefab,
                bossParent.position,
                Quaternion.identity,
                bossParent
            );

            // Configura o Boss
            BossShooter shooter = bossInstance.GetComponent<BossShooter>();

            if (shooter != null)
            {
                shooter.projectilePrefab = fase.projectilePrefab;
                shooter.projectileSprite = fase.projectileSprite;
                shooter.projectileSpeed = fase.projectileSpeed;
                shooter.shootInterval = fase.shootInterval;
            }
        }

        Debug.Log("Fase carregada: " + fase.nomeDaFase);
    }
}

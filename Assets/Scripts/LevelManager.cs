using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    // A "porta de entrada" que o Player estava procurando:
    public static LevelManager instance; 

    public FaseConfig faseAtual;

    private Player player; // Referência para pegar a vida
    private int pontuacaoAtual;

    [Header("Onde spawnar coisas")]
    public Transform backgroundParent;
    public Transform bossParent;

    [Header("Interface e Regras")]
    public float tempoDeFase = 60f; 
    public int pontuacao = 0;
    public TextMeshProUGUI textoTempo; 
    public TextMeshProUGUI textoPontuacao; 

    private GameObject backgroundInstance;
    private GameObject bossInstance;

    void Awake()
    {
        // Configura a porta de entrada assim que o jogo começa
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        LoadFase(faseAtual);
        AtualizarUI();
    }

    void Update()
    {
        tempoDeFase += Time.deltaTime;
        AtualizarUI();
    }

    public void AdicionarPontos(int pontos)
    {
        pontuacao += pontos;
        AtualizarUI();
    }

    void AtualizarUI()
    {
        if (textoTempo != null) textoTempo.text = "Tempo: " + Mathf.CeilToInt(tempoDeFase).ToString();
        if (textoPontuacao != null) textoPontuacao.text = "Pontos: " + pontuacao;
    }

    public void LoadFase(FaseConfig fase)
    {
        if (fase == null)
        {
            Debug.LogError("Nenhuma fase configurada!");
            return;
        }

        if (backgroundInstance != null) Destroy(backgroundInstance);
        if (bossInstance != null) Destroy(bossInstance);

        faseAtual = fase;

        if (fase.backgroundPrefab != null)
        {
            backgroundInstance = Instantiate(fase.backgroundPrefab, backgroundParent.position, Quaternion.identity, backgroundParent);
        }

        if (fase.bossPrefab != null)
        {
            bossInstance = Instantiate(fase.bossPrefab, bossParent.position, Quaternion.identity, bossParent);
            BossShooter shooter = bossInstance.GetComponent<BossShooter>();

            if (shooter != null)
            {
                shooter.projectilePrefab = fase.projectilePrefab;
                shooter.projectileSprite = fase.projectileSprite;
                shooter.projectileSpeed = fase.projectileSpeed;
                shooter.shootInterval = fase.shootInterval;
            }
        }
    }
}
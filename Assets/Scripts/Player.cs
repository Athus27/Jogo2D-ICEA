using UnityEngine;
using UnityEngine.UI; // Necessário para a barra de vida (Slider)

public class Player : MonoBehaviour
{
    public float speed = 5f;
    [Header("Configurações do Pulo")]
    public float maxHeight = 1f;
    public float timeToPeak = 0.4f;

    [Header("Sprites")]
    public Sprite parado;

    [Header("Sistema de Vida")]
    public int vidaMaxima = 100;
    public int vidaAtual;
    public Slider barraDeVida; // Arraste o Slider da tela (Canvas) aqui

    [Header("Sistema de Ataque")]
    public Transform pontoDeAtaque; // Objeto vazio na frente do jogador
    public float raioDeAtaque = 1.8f;
    public LayerMask camadaProjetil; // Identifica o que o ataque pode acertar

    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Animator anim;
    private bool isGrounded;
    private float gravity;
    private float jumpSpeed;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        gravity = (2 * maxHeight) / Mathf.Pow(timeToPeak, 2);
        jumpSpeed = gravity * timeToPeak;
        rb.gravityScale = gravity / 9.81f;

        // Inicia a vida cheia
        vidaAtual = vidaMaxima;
        AtualizarBarraDeVida();
    }

    void Update()
    {
        MovePlayer();

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpSpeed);
        }

        // Lógica de Ataque (Botão J ou clique esquerdo)
        if (Input.GetKeyDown(KeyCode.J) || Input.GetButtonDown("Fire1"))
        {
            Atacar();
        }
    }

    void Atacar()
    {
        if (pontoDeAtaque == null) return;

        // Desenha um círculo invisível que pega tudo que está na camada do projétil
        Collider2D[] projeteisAtingidos = Physics2D.OverlapCircleAll(pontoDeAtaque.position, raioDeAtaque, camadaProjetil);

        foreach (Collider2D projetil in projeteisAtingidos)
        {
            Destroy(projetil.gameObject); // Destrói o projétil inimigo
            
            // Avisa o LevelManager para aumentar a pontuação
            if (LevelManager.instance != null)
            {
                LevelManager.instance.AdicionarPontos(10);
                Debug.Log("Projétil destruído! +10 Pontos");
            }
        }
    }

    // Desenha uma linha vermelha no Editor para ver o tamanho do ataque
    private void OnDrawGizmosSelected()
    {
        if (pontoDeAtaque != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(pontoDeAtaque.position, raioDeAtaque);
        }
    }

    void MovePlayer()
    {
        float moveX = 0;

        if (Input.GetKey(KeyCode.A))
        {
            moveX = -1;
            sr.flipX = false; 
        }
        else if (Input.GetKey(KeyCode.D))
        {
            moveX = 1;
            sr.flipX = true; 
        }

        if (moveX != 0) anim.enabled = true;
        else
        {
            anim.enabled = false;
            sr.sprite = parado;
        }

        rb.linearVelocity = new Vector2(moveX * speed, rb.linearVelocity.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Chao"))
        {
            isGrounded = true;
        }
        
        // Se bater no projétil (e não tiver defendido antes)
        if (collision.gameObject.CompareTag("Projetil"))
        {
            Destroy(collision.gameObject);  
            ReceberDano(20); // Perde 20 de vida
        }
    }

    public void ReceberDano(int dano)
    {
        vidaAtual -= dano;
        if (vidaAtual < 0) vidaAtual = 0;
        
        AtualizarBarraDeVida();
        Debug.Log("Player tomou dano! Vida atual: " + vidaAtual);

        if (vidaAtual <= 0)
        {
            Debug.Log("Game Over!");
            gameObject.SetActive(false); // Esconde o jogador (morreu)
        }
    }

    void AtualizarBarraDeVida()
    {
        if (barraDeVida != null)
        {
            barraDeVida.maxValue = vidaMaxima;
            barraDeVida.value = vidaAtual;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Chao"))
        {
            isGrounded = false;
        }
    }
}
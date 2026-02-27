using UnityEngine;
using UnityEngine.UI; 

public class Player : MonoBehaviour
{
    public float speed = 5f;
    [Header("Nova Velocidade Automática")]
    public float velocidadeFase = 3f; // Faz o personagem andar com a câmera!

    [Header("Configurações do Pulo")]
    public float maxHeight = 1f;
    public float timeToPeak = 0.4f;

    [Header("Sprites e Animação")]
    public Sprite parado;
    public Sprite[] framesAtaque; 
    public float tempoPorFrame = 0.08f; 

    [Header("Sistema de Vida")]
    public int vidaMaxima = 100;
    public int vidaAtual;
    public Slider barraDeVida; 

    [Header("Sistema de Ataque")]
    public Transform pontoDeAtaque; 
    public float raioDeAtaque = 0.8f;
    public LayerMask camadaProjetil; 

    [Header("Sons")]
    public AudioSource audioSource;
    public AudioClip somAtaque;

    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Animator anim;
    private bool isGrounded;
    private float gravity;
    private float jumpSpeed;

    private bool isAttacking = false;
    private int frameAtualAtaque = 0;
    private float attackTimer = 0f;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        gravity = (2 * maxHeight) / Mathf.Pow(timeToPeak, 2);
        jumpSpeed = gravity * timeToPeak;
        rb.gravityScale = gravity / 9.81f;

        vidaAtual = vidaMaxima;
        AtualizarBarraDeVida();
    }

    void Update()
    {
        if (isAttacking)
        {
            attackTimer -= Time.deltaTime;
            if (attackTimer <= 0)
            {
                frameAtualAtaque++; 
                if (frameAtualAtaque < framesAtaque.Length)
                {
                    sr.sprite = framesAtaque[frameAtualAtaque]; 
                    attackTimer = tempoPorFrame; 
                }
                else isAttacking = false; 
            }
        }

        MovePlayer();

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpSpeed);
        }

        if ((Input.GetKeyDown(KeyCode.J) || Input.GetButtonDown("Fire1")) && !isAttacking)
        {
            Atacar();
        }
    }

    void Atacar()
    {
        if (framesAtaque == null || framesAtaque.Length == 0) return;

        isAttacking = true;
        frameAtualAtaque = 0;
        attackTimer = tempoPorFrame;

        anim.enabled = false;
        sr.sprite = framesAtaque[0]; 

        if (pontoDeAtaque == null) return;

        Collider2D[] projeteisAtingidos = Physics2D.OverlapCircleAll(pontoDeAtaque.position, raioDeAtaque, camadaProjetil);

        if (audioSource != null && somAtaque != null)
        {
            // Usamos PlayOneShot porque ele permite tocar vários sons por cima 
            // uns dos outros (se você apertar J muito rápido, o som não corta o anterior)
            audioSource.PlayOneShot(somAtaque); 
        }

        foreach (Collider2D projetil in projeteisAtingidos)
        {
            Destroy(projetil.gameObject); 
            if (LevelManager.instance != null) LevelManager.instance.AdicionarPontos(10); 
        }
    }

    void MovePlayer()
    {
        float moveX = 0;

        if (Input.GetKey(KeyCode.A))
        {
            moveX = -1;
            sr.flipX = false; 
            if (pontoDeAtaque != null) 
                pontoDeAtaque.localPosition = new Vector3(-Mathf.Abs(pontoDeAtaque.localPosition.x), pontoDeAtaque.localPosition.y, 0);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            moveX = 1;
            sr.flipX = true; 
            if (pontoDeAtaque != null) 
                pontoDeAtaque.localPosition = new Vector3(Mathf.Abs(pontoDeAtaque.localPosition.x), pontoDeAtaque.localPosition.y, 0);
        }

        if (!isAttacking)
        {
            if (moveX != 0) anim.enabled = true;
            else
            {
                anim.enabled = false;
                sr.sprite = parado;
            }
        }

        // Adiciona a velocidade da tela no jogador!
        rb.linearVelocity = new Vector2((moveX * speed) + velocidadeFase, rb.linearVelocity.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Chao")) isGrounded = true;
        if (collision.gameObject.CompareTag("Projetil"))
        {
            Destroy(collision.gameObject);  
            ReceberDano(20); 
        }
    }

    public void ReceberDano(int dano)
    {
        vidaAtual -= dano;
        if (vidaAtual < 0) vidaAtual = 0;
        AtualizarBarraDeVida();
        if (vidaAtual <= 0) gameObject.SetActive(false); 
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
        if (collision.gameObject.CompareTag("Chao")) isGrounded = false;
    }
}
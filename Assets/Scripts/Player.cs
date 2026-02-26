using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 5f;
    [Header("Configurações do Pulo")]
    public float maxHeight = 1f;
    public float timeToPeak = 0.4f;
    [Header("Spawn Position")]
    // public Vector2 spawnPosition = new Vector2(0, 0); // Posição inicial

    [Header("Sprites")]
    public Sprite parado; // ARRASTE O FRAME_0 AQUI NO INSPECTOR

    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Animator anim; // Adicionado para controlar o play/stop
    private bool isGrounded;
    private float gravity;
    private float jumpSpeed;

    void Start()
    {
        // transform.position = spawnPosition;


        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>(); // Pega o Animator automaticamente

        gravity = (2 * maxHeight) / Mathf.Pow(timeToPeak, 2);
        jumpSpeed = gravity * timeToPeak;
        rb.gravityScale = gravity / 9.81f;
    }

    void Update()
    {
        MovePlayer();

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpSpeed);
        }
    }


    void MovePlayer()
    {
        float moveX = 0;

        // INVERTI A LÓGICA DO FLIPX AQUI
        if (Input.GetKey(KeyCode.A)) // Esquerda
        {
            moveX = -1;
            sr.flipX = false; 
        }
        else if (Input.GetKey(KeyCode.D)) // Direita
        {
            moveX = 1;
            sr.flipX = true; 
        }

        // LÓGICA DA ANIMAÇÃO vs PARADO
        if (moveX != 0)
        {
            anim.enabled = true; // Liga a animação correndo
        }
        else
        {
            anim.enabled = false; // Desliga a animação
            sr.sprite = parado;   // Força o sprite parado (Frame 0)
        }

        rb.linearVelocity = new Vector2(moveX * speed, rb.linearVelocity.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
        {
            // Só ativa o pulo se tocar em algo com a etiqueta "Chao"
            if (collision.gameObject.CompareTag("Chao"))
            {
                isGrounded = true;
            }
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            // Só desativa o pulo se a coisa que você largou for o "Chao"
            if (collision.gameObject.CompareTag("Chao"))
            {
                isGrounded = false;
            }
        }
}
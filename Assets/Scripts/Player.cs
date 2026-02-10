using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 5f;

    public Sprite frente;
    public Sprite costas;
    public Sprite lado;


    private SpriteRenderer sr;


    // O comando Start é chamado antes da primeira atualização do frame.
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = frente;
    }

    // A atualização é chamada uma vez por quadro
    void Update()
    {
        Vector2 position = transform.position;

        if (Input.GetKey(KeyCode.A))
        {
            position.x -= speed * Time.deltaTime;
            sr.sprite = lado;
            sr.flipX = true;
            // moving = true;
        }

        if (Input.GetKey(KeyCode.D))
        {
            position.x += speed * Time.deltaTime;
            sr.sprite = lado;
            sr.flipX = false;
            // moving = true;
        }

        if (Input.GetKey(KeyCode.W))
        {
            position.y += speed * Time.deltaTime;
            sr.sprite = costas;
            // moving = true;
        }

        if (Input.GetKey(KeyCode.S))
        {
            position.y -= speed * Time.deltaTime;
            sr.sprite = frente;
            // moving = true;
        }

        transform.position = position;
    }
}

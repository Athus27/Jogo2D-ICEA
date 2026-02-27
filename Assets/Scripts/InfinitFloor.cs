using UnityEngine;

public class FundoInfinito : MonoBehaviour
{
    private float larguraImagem;

    void Start()
    {
        // A Unity calcula automaticamente o tamanho exato da sua imagem
        larguraImagem = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void Update()
    {
        // Mede qual a distância entre a Câmera e esta imagem
        float distanciaDaCamera = Camera.main.transform.position.x - transform.position.x;

        // Se a imagem ficou totalmente para trás da visão da câmera...
        if (distanciaDaCamera > larguraImagem * 1.5f) 
        {
            //Adiciona a proxima imagem do loop
            transform.position += new Vector3(larguraImagem * 3f, 0, 0);
        }
    }
}
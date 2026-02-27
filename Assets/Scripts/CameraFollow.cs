using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Configuração de Velocidade")]
    public float velocidadeFase = 3f; // A velocidade que a tela vai se mover para a direita

    void Update()
    {
        // A câmera anda para a direita sozinha infinitamente
        transform.position += Vector3.right * velocidadeFase * Time.deltaTime;
    }
}
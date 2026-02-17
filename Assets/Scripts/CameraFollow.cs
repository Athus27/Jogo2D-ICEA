using UnityEngine;


public class CameraFollow : MonoBehaviour
{
    public Transform Player;
    public float minX, maxX;
    public float minY, maxY;


    void Start()
    {
        LateUpdate();
    }

    void LateUpdate()
    {
        if (Player == null) return;

        Vector3 novaPosicao = transform.position;

        //eixo x 
        novaPosicao.x = Player.position.x;
        novaPosicao.x = Mathf.Clamp(novaPosicao.x, minX, maxX);

        //eixo y
        novaPosicao.y = Player.position.y +1f; 
        novaPosicao.y = Mathf.Clamp(novaPosicao.y, minY, maxY);

        transform.position = novaPosicao;
    }

}



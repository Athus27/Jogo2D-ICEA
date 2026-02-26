using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform Player;
    public SpriteRenderer cenario;

    private float minX, maxX, minY, maxY;
    private float camHalfWidth, camHalfHeight;

    void Start()
    {
        if (cenario != null)
        {
            Bounds limites = cenario.bounds;

            minX = limites.min.x;
            maxX = limites.max.x;
            minY = limites.min.y;
            maxY = limites.max.y;
        }

        Camera cam = GetComponent<Camera>();

        camHalfHeight = cam.orthographicSize;
        camHalfWidth = camHalfHeight * cam.aspect;
    }

    void LateUpdate()
    {
        if (Player == null) return;

        Vector3 novaPosicao = transform.position;

        // X
        float limiteMinX = minX + camHalfWidth;
        float limiteMaxX = maxX - camHalfWidth;
        novaPosicao.x = Mathf.Clamp(Player.position.x, limiteMinX, limiteMaxX);

        // Y
        float limiteMinY = minY + camHalfHeight;
        float limiteMaxY = maxY - camHalfHeight;
        novaPosicao.y = Mathf.Clamp(Player.position.y + 1f, limiteMinY, limiteMaxY);

        transform.position = novaPosicao;
    }
}
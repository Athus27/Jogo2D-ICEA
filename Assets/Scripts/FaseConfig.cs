using UnityEngine;

[CreateAssetMenu(menuName = "Jogo/Fase Config", fileName = "Fase_")]
public class FaseConfig : ScriptableObject
{
    [Header("Visual da fase")]
    public GameObject backgroundPrefab;   // ou Sprite se preferir simples
    public Transform spawnBackground;      // opcional (pode ser nulo)

    [Header("Boss e ataque")]
    public GameObject bossPrefab;
    public GameObject projectilePrefab;

    [Header("Config do projétil (skin + velocidade)")]
    public Sprite projectileSprite;
    public float projectileSpeed = 8f;

    [Header("Ritmo do boss")]
    public float shootInterval = 1.2f;

    [Header("Dificuldade / tema")]
    public string nomeDaFase = "Fase";
}

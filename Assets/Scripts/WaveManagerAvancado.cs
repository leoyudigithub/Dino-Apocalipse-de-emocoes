using System.Collections;
using UnityEngine;

public class WaveManagerAvançado : MonoBehaviour
{
    [Header("Configuração de Inimigos")]
    public GameObject inimigoBasicoPrefab; // Prefab do inimigo comum
    public GameObject inimigoChefePrefab;  // Prefab do chefe
    public Transform[] pontosSpawn;        // Locais de onde os inimigos surgem

    [Header("Configuração de Ondas")]
    public int[] inimigosPorOnda;          // Ex: [3, 5, 7, 10]
    public int[] ondasComChefe;            // Ex: [3, 6, 9] — nas ondas 3, 6 e 9 aparece um chefe
    public float tempoEntreOndas = 5f;

    [Header("Escalonamento de Dificuldade")]
    public float aumentoVelocidade = 0.2f; // quanto aumenta a velocidade a cada onda
    public float aumentoVida = 10f;        // quanto aumenta a vida a cada onda
    public float aumentoDano = 2f;         // quanto aumenta o dano a cada onda

    private int ondaAtual = 0;

    void Start()
    {
        StartCoroutine(ControlarOndas());
    }

    private IEnumerator ControlarOndas()
    {
        // Enquanto ainda há ondas definidas no array
        while (ondaAtual < inimigosPorOnda.Length)
        {
            Debug.Log($"🌊 Iniciando Onda {ondaAtual + 1}");

            // Gera a onda atual
            yield return StartCoroutine(GerarOnda(ondaAtual));

            Debug.Log($"✅ Onda {ondaAtual + 1} concluída!");

            // Espera um tempo antes da próxima
            yield return new WaitForSeconds(tempoEntreOndas);

            // Passa para a próxima onda
            ondaAtual++;
        }

        Debug.Log("🏁 Todas as ondas foram concluídas!");
    }

    private IEnumerator GerarOnda(int indiceOnda)
    {
        int quantidade = inimigosPorOnda[indiceOnda];
        bool temChefe = System.Array.Exists(ondasComChefe, o => o == indiceOnda + 1);

        // Gera inimigos normais
        for (int i = 0; i < quantidade; i++)
        {
            CriarInimigo(inimigoBasicoPrefab, indiceOnda);
            yield return new WaitForSeconds(0.5f); // pequeno intervalo entre spawns
        }

        // Se for uma onda de chefe, gera o chefe
        if (temChefe)
        {
            Debug.Log("⚔️ Chefe entrou na arena!");
            CriarInimigo(inimigoChefePrefab, indiceOnda);
        }

        yield return null;
    }

    private void CriarInimigo(GameObject prefab, int indiceOnda)
    {
        // Escolhe posição de spawn aleatória
        Transform ponto = pontosSpawn[Random.Range(0, pontosSpawn.Length)];

        // Instancia o inimigo
        GameObject inimigo = Instantiate(prefab, ponto.position, Quaternion.identity);

        // Escala a dificuldade dinamicamente
        var vida = inimigo.GetComponent<Vida>();
        var ia = inimigo.GetComponent<InimigoIA>();

        if (vida != null)
            vida.vidaMaxima += aumentoVida * indiceOnda;

        if (ia != null)
        {
            ia.velocidade += aumentoVelocidade * indiceOnda;
            ia.dano += aumentoDano * indiceOnda;
        }
    }
}

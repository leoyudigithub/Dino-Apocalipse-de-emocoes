using System.Collections;
using UnityEngine;

public class WaveManagerAvançado : MonoBehaviour
{
    [Header("Referência ao Player")]
    public Transform playerTransform; // 👈 ADICIONE ESTA LINHA

    [Header("Configuração de Inimigos")]
    public GameObject inimigoBasicoPrefab;
    public GameObject inimigoChefePrefab;
    public Transform[] pontosSpawn;

    [Header("Configuração de Ondas")]
    public int[] inimigosPorOnda;
    public int[] ondasComChefe;
    public float tempoEntreOndas = 5f;

    [Header("Escalonamento de Dificuldade")]
    public float aumentoVelocidade = 0.2f;
    public float aumentoVida = 10f;
    public float aumentoDano = 2f;

    private int ondaAtual = 0;

    void Start()
    {
        StartCoroutine(ControlarOndas());
    }

    private IEnumerator ControlarOndas()
    {
        while (ondaAtual < inimigosPorOnda.Length)
        {
            Debug.Log($"🌊 Iniciando Onda {ondaAtual + 1}");
            yield return StartCoroutine(GerarOnda(ondaAtual));
            Debug.Log($"✅ Onda {ondaAtual + 1} concluída!");
            yield return new WaitForSeconds(tempoEntreOndas);
            ondaAtual++;
        }

        Debug.Log("🏁 Todas as ondas foram concluídas!");
    }

    private IEnumerator GerarOnda(int indiceOnda)
    {
        int quantidade = inimigosPorOnda[indiceOnda];
        bool temChefe = System.Array.Exists(ondasComChefe, o => o == indiceOnda + 1);

        for (int i = 0; i < quantidade; i++)
        {
            CriarInimigo(inimigoBasicoPrefab, indiceOnda);
            yield return new WaitForSeconds(2.5f);
        }

        if (temChefe)
        {
            Debug.Log("⚔️ Chefe entrou na arena!");
            CriarInimigo(inimigoChefePrefab, indiceOnda);
        }

        yield return null;
    }

    private void CriarInimigo(GameObject prefab, int indiceOnda)
    {
        Transform ponto = pontosSpawn[Random.Range(0, pontosSpawn.Length)];
        GameObject inimigo = Instantiate(prefab, ponto.position, Quaternion.identity);

        // Atribui referência ao Player (🔥 ESSENCIAL)
        var ia = inimigo.GetComponent<InimigoIA>();
        if (ia != null)
        {
            ia.player = playerTransform;
            ia.velocidade += aumentoVelocidade * indiceOnda;
            ia.dano += aumentoDano * indiceOnda;
        }

        var vida = inimigo.GetComponent<Vida>();
        if (vida != null)
            vida.vidaMaxima += aumentoVida * indiceOnda;
    }
}

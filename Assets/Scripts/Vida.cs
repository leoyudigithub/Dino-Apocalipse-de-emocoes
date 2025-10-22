using UnityEngine;

public class Vida : MonoBehaviour
{
    public float vidaMaxima = 100f; // Valor total de vida
    private float vidaAtual; // Vida restante

    void Start()
    {
        // Inicializa a vida com o valor máximo
        vidaAtual = vidaMaxima;
    }

    // Método público chamado por ataques para aplicar dano
    public void ReceberDano(float dano)
    {
        // Subtrai o dano da vida atual
        vidaAtual -= dano;

        Debug.Log($"{gameObject.name} recebeu {dano} de dano. Vida restante: {vidaAtual}");

        // Se a vida chegou a zero ou menos, morre
        if (vidaAtual <= 0)
            Morrer();
    }

    void Morrer()
    {
        // Aqui você pode adicionar animação de morte, efeitos, etc.
        Debug.Log($"{gameObject.name} morreu!");
        Destroy(gameObject); // Remove o objeto da cena
    }
}

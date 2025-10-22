using UnityEngine;

public class InimigoIA : MonoBehaviour
{
    public Transform player;      // Referência ao jogador
    public float velocidade = 3f; // Velocidade de movimento
    public float tempoEntreAtaques = 5f; // Tempo mínimo entre ataques
    private float tempoProximoAtaque;  // Tempo em que o inimigo pode atacar novamente
    public float dano = 10f;       // dano causado ao jogador ao colidir
    public float distanciaMinima = 1f; // Distância mínima para parar

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (player == null) return;

        // Calcula a distância horizontal até o jogador
        float distanciaX = player.position.x - transform.position.x;
        float distanciaY = player.position.y - transform.position.y;

        // Só persegue se estiver longe o suficiente
        if (Mathf.Abs(distanciaX) > distanciaMinima && Mathf.Abs(distanciaY) > distanciaMinima)
        {
            // Define a direção (1 para direita, -1 para esquerda)
            float direcaoX = Mathf.Sign(distanciaX);
            float direcaoY = Mathf.Sign(distanciaY);


            // Move apenas no eixo X, mantém o Y original
            rb.linearVelocity = new Vector2(direcaoX * velocidade, direcaoY * velocidade);

            // Espelha o sprite de acordo com a direção
            transform.localScale = new Vector3(direcaoX, direcaoY, 1);
        }
        else if (Time.time >= tempoProximoAtaque)
        {
            // Para de se mover quando está perto o suficiente
            rb.linearVelocity = new Vector2(0, 0);
            player.GetComponent<Vida>()?.ReceberDano(dano);
            tempoProximoAtaque = Time.time + tempoEntreAtaques;
        }
    }
}

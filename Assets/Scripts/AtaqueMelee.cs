using UnityEngine;

public class AtaqueMelee : MonoBehaviour
{
    public float dano = 10f; // Quanto dano o ataque causa
    public float alcance = 1f; // Raio da área de ataque
    public Transform pontoAtaque; // Posição de onde o ataque parte
    public LayerMask inimigoLayer; // Quais objetos podem ser atingidos

    void Update()
    {
        // Quando o jogador aperta Q, executa o ataque
        if (Input.GetKeyDown(KeyCode.Q))
            Atacar();
    }

    void Atacar()
    {
        // Verifica todos os inimigos dentro de um círculo (área de ataque)
        Collider2D[] inimigos = Physics2D.OverlapCircleAll(pontoAtaque.position, alcance, inimigoLayer);

        // Aplica dano a todos os inimigos atingidos
        foreach (Collider2D inimigo in inimigos)
        {
            // Chama o método ReceberDano no script Vida do inimigo
            inimigo.GetComponent<Vida>()?.ReceberDano(dano);
        }

        Debug.Log("Ataque Melee executado!");
    }

    // Desenha o alcance no editor (útil para ajustar o ataque visualmente)
    void OnDrawGizmosSelected()
    {
        if (pontoAtaque == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(pontoAtaque.position, alcance);
    }
}

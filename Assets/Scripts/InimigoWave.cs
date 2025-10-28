using UnityEngine;

public class InimigoWave : MonoBehaviour
{
    public float velocidade = 2f;
    public float dano = 10f;
    private Transform player;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    void Update()
    {
        if (player == null) return;

        // Move na direção do jogador
        Vector2 direcao = (player.position - transform.position).normalized;
        rb.linearVelocity = new Vector2(direcao.x * velocidade, rb.linearVelocity.y);

        // Espelha sprite conforme o lado
        // transform.localScale = new Vector3(player.position.x > transform.position.x ? 1 : -1, 1, 1);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            col.gameObject.GetComponent<Vida>()?.ReceberDano(dano);
        }
    }

    public void Morrer()
    {
        Destroy(gameObject);
    }
}

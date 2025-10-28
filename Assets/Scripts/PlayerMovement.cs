using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private float moveH, moveV;
    public float speed = 2f;
    public Transform pontoAtaque;

    private bool olhandoDireita = true;
    private Vector3 pontoAtaqueLocalOriginal;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (pontoAtaque != null)
            pontoAtaqueLocalOriginal = pontoAtaque.localPosition;
    }

    private void FixedUpdate()
    {
        moveH = Input.GetAxisRaw("Horizontal") * speed;
        moveV = Input.GetAxisRaw("Vertical") * speed;

        rb.linearVelocity = new Vector2(moveH, moveV);

        if (moveH > 0 && !olhandoDireita)
            Virar();
        else if (moveH < 0 && olhandoDireita)
            Virar();
    }

    private void Virar()
    {
        olhandoDireita = !olhandoDireita;

        // Inverte o personagem inteiro (isso faz o AttackPoint acompanhar automaticamente)
        Vector3 escala = transform.localScale;
        escala.x *= -1;
        transform.localScale = escala;

        // Corrige o ponto de ataque, se quiser manter uma distância lateral fixa
        if (pontoAtaque != null)
        {
            pontoAtaque.localPosition = new Vector3(
                pontoAtaqueLocalOriginal.x * (olhandoDireita ? 1 : -1),
                pontoAtaqueLocalOriginal.y,
                pontoAtaqueLocalOriginal.z
            );
        }
    }
}

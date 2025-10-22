using UnityEngine;

public class AtaqueProjetil : MonoBehaviour
{
    public GameObject projetilPrefab;  // Prefab do projétil (bala, magia, etc.)
    public Transform pontoDisparo;     // Local de onde o projétil é disparado
    public float velocidadeProjetil = 10f; // Velocidade do disparo

    void Update()
    {
        // Clique do mouse esquerdo dispara um projétil
        if (Input.GetMouseButtonDown(0))
            Disparar();
    }

    void Disparar()
    {
        // Cria o projétil na cena
        GameObject projetil = Instantiate(projetilPrefab, pontoDisparo.position, pontoDisparo.rotation);

        // Faz o projétil se mover na direção que o jogador está virado
        Rigidbody2D rb = projetil.GetComponent<Rigidbody2D>();
        rb.linearVelocity = pontoDisparo.right * velocidadeProjetil;
    }
}

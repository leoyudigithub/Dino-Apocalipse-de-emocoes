using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private float moveH, moveV;
    //public Animator animator;
    public float speed = 2f;
    

    private void Start()
    {
        //Ao iniciar o jogo, acessa o Rigidbody2D do personagem
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {


        moveH = Input.GetAxis("Horizontal") * speed;
        moveV = Input.GetAxis("Vertical") * speed;

        rb.linearVelocity = new Vector2(moveH, moveV);
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();

        //flip o sprite
        if( moveH > 0 )
        {
            
            sprite.flipX = true;
            
            
        }
        else if( moveH < 0 )
        {
            sprite.flipX = false;
        }
        
        

     
    }
}
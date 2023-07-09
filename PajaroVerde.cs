using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PajaroVerde : MonoBehaviour
{
    public float jumpForce = 10f; // Fuerza de salto
    public float moveSpeed = 5f; // Velocidad de movimiento horizontal
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true; // Desactiva la rotación del objeto al aplicar fuerzas
    }

    private void Update()
    {
        // Salto con la flecha hacia arriba
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            rb.velocity = Vector2.zero; // Detener el movimiento actual
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse); // Aplicar una fuerza de salto hacia arriba
        }

        // Movimiento horizontal
        float moveInput = Input.GetAxis("Horizontal"); // Obtener el valor de entrada horizontal (teclas de flecha izquierda y derecha o A/D)
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y); // Aplicar una velocidad horizontal al Rigidbody
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("escena"))
        {
            rb.velocity = Vector2.zero; // Detener el movimiento al colisionar con el suelo
        }
    }
}

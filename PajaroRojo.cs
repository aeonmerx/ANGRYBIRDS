using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class PajaroRojo : MonoBehaviour
{
    public Transform pivot; // Punto de pivote alrededor del cual se arrastra el pájaro
    public float springRange; // Rango máximo de movimiento del pájaro al ser arrastrado
    public float maxVel;       // Velocidad máxima del pájaro al ser lanzado

    Rigidbody2D rb;

    private bool canDrag = true; // Variable que indica si el pájaro puede ser arrastrado
    private Vector3 dis; // Distancia entre la posición del mouse y el punto de pivote
    private Vector3 initialPosition; // Posición inicial del pájaro
    private Quaternion initialRotation; // Rotación inicial del pájaro

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic; // Configurar el tipo de cuerpo del Rigidbody como kinemático
        initialPosition = transform.position; // Guardar la posición inicial del pájaro
        initialRotation = transform.rotation; // Guardar la rotación inicial del pájaro
        transform.position = initialPosition; // Asignar la posición inicial al inicio del juego
    }

    private void OnMouseDrag()
    {
        if (!canDrag)
            return; // Si no se puede arrastrar el pájaro, salir del método

        var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition); // Obtener la posición del mouse en el mundo
        dis = pos - pivot.position; // Calcular la distancia entre la posición del mouse y el punto de pivote
        dis.z = 0; // Asegurarse de que la posición en el eje Z sea cero (en 2D)

        if (dis.magnitude > springRange)
        {
            dis = dis.normalized * springRange; // Limitar la distancia al rango máximo si es necesario
        }

        transform.position = dis + pivot.position; // Establecer la posición del objeto según la distancia y el punto de pivote
    }

    private void OnMouseUp()
    {
        if (!canDrag)
            return; // Si no se puede arrastrar el pájaro, salir del método

        canDrag = false; // Desactivar el arrastre del pájaro
        rb.bodyType = RigidbodyType2D.Dynamic; // Cambiar el tipo de cuerpo del Rigidbody a dinámico
        rb.velocity = -dis.normalized * maxVel * dis.magnitude / springRange; // Aplicar una velocidad al Rigidbody en la dirección opuesta a la distancia del arrastre
    }

    // Método para restablecer el pájaro a su posición inicial y permitir otro lanzamiento
    public void ResetBird()
    {
        canDrag = true; // Activar el arrastre del pájaro
        rb.bodyType = RigidbodyType2D.Kinematic; // Cambiar el tipo de cuerpo del Rigidbody a kinemático
        rb.velocity = Vector2.zero; // Establecer la velocidad del Rigidbody a cero
        transform.position = initialPosition; // Restablecer la posición del pájaro a la posición inicial
        transform.rotation = initialRotation; // Restablecer la rotación del pájaro a la rotación inicial
        rb.angularVelocity = 0f; // Establecer la velocidad de rotación angular del Rigidbody a cero
    }
}

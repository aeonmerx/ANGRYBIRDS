using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class PajaroRojo : MonoBehaviour
{
    public Transform pivot; // Punto de pivote alrededor del cual se arrastra el p�jaro
    public float springRange; // Rango m�ximo de movimiento del p�jaro al ser arrastrado
    public float maxVel;       // Velocidad m�xima del p�jaro al ser lanzado

    Rigidbody2D rb;

    private bool canDrag = true; // Variable que indica si el p�jaro puede ser arrastrado
    private Vector3 dis; // Distancia entre la posici�n del mouse y el punto de pivote
    private Vector3 initialPosition; // Posici�n inicial del p�jaro
    private Quaternion initialRotation; // Rotaci�n inicial del p�jaro

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic; // Configurar el tipo de cuerpo del Rigidbody como kinem�tico
        initialPosition = transform.position; // Guardar la posici�n inicial del p�jaro
        initialRotation = transform.rotation; // Guardar la rotaci�n inicial del p�jaro
        transform.position = initialPosition; // Asignar la posici�n inicial al inicio del juego
    }

    private void OnMouseDrag()
    {
        if (!canDrag)
            return; // Si no se puede arrastrar el p�jaro, salir del m�todo

        var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition); // Obtener la posici�n del mouse en el mundo
        dis = pos - pivot.position; // Calcular la distancia entre la posici�n del mouse y el punto de pivote
        dis.z = 0; // Asegurarse de que la posici�n en el eje Z sea cero (en 2D)

        if (dis.magnitude > springRange)
        {
            dis = dis.normalized * springRange; // Limitar la distancia al rango m�ximo si es necesario
        }

        transform.position = dis + pivot.position; // Establecer la posici�n del objeto seg�n la distancia y el punto de pivote
    }

    private void OnMouseUp()
    {
        if (!canDrag)
            return; // Si no se puede arrastrar el p�jaro, salir del m�todo

        canDrag = false; // Desactivar el arrastre del p�jaro
        rb.bodyType = RigidbodyType2D.Dynamic; // Cambiar el tipo de cuerpo del Rigidbody a din�mico
        rb.velocity = -dis.normalized * maxVel * dis.magnitude / springRange; // Aplicar una velocidad al Rigidbody en la direcci�n opuesta a la distancia del arrastre
    }

    // M�todo para restablecer el p�jaro a su posici�n inicial y permitir otro lanzamiento
    public void ResetBird()
    {
        canDrag = true; // Activar el arrastre del p�jaro
        rb.bodyType = RigidbodyType2D.Kinematic; // Cambiar el tipo de cuerpo del Rigidbody a kinem�tico
        rb.velocity = Vector2.zero; // Establecer la velocidad del Rigidbody a cero
        transform.position = initialPosition; // Restablecer la posici�n del p�jaro a la posici�n inicial
        transform.rotation = initialRotation; // Restablecer la rotaci�n del p�jaro a la rotaci�n inicial
        rb.angularVelocity = 0f; // Establecer la velocidad de rotaci�n angular del Rigidbody a cero
    }
}

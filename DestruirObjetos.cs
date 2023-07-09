using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestruirObjetos : MonoBehaviour
{
    public float resistance; // Resistencia mínima requerida para destruir el objeto
    public GameObject explosionPrefab; // Prefabricado de la explosión

    private bool hasExploded = false; // Variable para controlar si la explosión ya ha sido creada y destruida

    private PajaroRojo pajaroRojo; // Referencia al script del pájaro rojo
    private Collider2D objectCollider; // Referencia al Collider del objeto

    private void Start()
    {
        pajaroRojo = FindObjectOfType<PajaroRojo>(); // Buscar el objeto con el script PajaroRojo en la escena
        objectCollider = GetComponent<Collider2D>(); // Obtener el componente Collider2D del objeto
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (hasExploded) // Verificar si la explosión ya ha sido creada y destruida
            return; // Si ha explotado, salir del método

        if (col.relativeVelocity.magnitude > resistance) // Verificar si la magnitud de la velocidad relativa de la colisión es mayor que la resistencia mínima
        {
            if (col.gameObject == pajaroRojo.gameObject) // Verificar si el objeto colisionado es el pájaro rojo
            {
                if (explosionPrefab != null) // Verificar si se ha asignado un prefabricado de explosión
                {
                    var go = Instantiate(explosionPrefab, transform.position, Quaternion.identity); // Crear una instancia del prefabricado de explosión en la posición del objeto
                    Destroy(go, 3); // Destruir la instancia de la explosión después de 3 segundos
                }

                Destroy(gameObject, 0.1f); // Destruir el objeto después de 0.1 segundos

                // Restablecer el pájaro rojo para otro lanzamiento
                pajaroRojo.ResetBird();

                hasExploded = true; // Marcar la explosión como creada y destruida

                objectCollider.enabled = false; // Desactivar el Collider del objeto para evitar colisiones adicionales
            }
        }
        else
        {
            resistance -= col.relativeVelocity.magnitude; // Reducir la resistencia por la magnitud de la velocidad relativa de la colisión
        }
    }
}

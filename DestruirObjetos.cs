using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestruirObjetos : MonoBehaviour
{
    public float resistance; // Resistencia m�nima requerida para destruir el objeto
    public GameObject explosionPrefab; // Prefabricado de la explosi�n

    private bool hasExploded = false; // Variable para controlar si la explosi�n ya ha sido creada y destruida

    private PajaroRojo pajaroRojo; // Referencia al script del p�jaro rojo
    private Collider2D objectCollider; // Referencia al Collider del objeto

    private void Start()
    {
        pajaroRojo = FindObjectOfType<PajaroRojo>(); // Buscar el objeto con el script PajaroRojo en la escena
        objectCollider = GetComponent<Collider2D>(); // Obtener el componente Collider2D del objeto
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (hasExploded) // Verificar si la explosi�n ya ha sido creada y destruida
            return; // Si ha explotado, salir del m�todo

        if (col.relativeVelocity.magnitude > resistance) // Verificar si la magnitud de la velocidad relativa de la colisi�n es mayor que la resistencia m�nima
        {
            if (col.gameObject == pajaroRojo.gameObject) // Verificar si el objeto colisionado es el p�jaro rojo
            {
                if (explosionPrefab != null) // Verificar si se ha asignado un prefabricado de explosi�n
                {
                    var go = Instantiate(explosionPrefab, transform.position, Quaternion.identity); // Crear una instancia del prefabricado de explosi�n en la posici�n del objeto
                    Destroy(go, 3); // Destruir la instancia de la explosi�n despu�s de 3 segundos
                }

                Destroy(gameObject, 0.1f); // Destruir el objeto despu�s de 0.1 segundos

                // Restablecer el p�jaro rojo para otro lanzamiento
                pajaroRojo.ResetBird();

                hasExploded = true; // Marcar la explosi�n como creada y destruida

                objectCollider.enabled = false; // Desactivar el Collider del objeto para evitar colisiones adicionales
            }
        }
        else
        {
            resistance -= col.relativeVelocity.magnitude; // Reducir la resistencia por la magnitud de la velocidad relativa de la colisi�n
        }
    }
}

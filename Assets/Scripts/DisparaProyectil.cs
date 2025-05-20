using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisparaProyectil : MonoBehaviour
{
    [SerializeField] private float velocidad = 8.0f;

    void FixedUpdate()
    {
        // Movimiento según la dirección
        if (CAD.dirDisparo == 2) // Abajo
        {
            transform.position += new Vector3(0, -1, 0) * Time.deltaTime * velocidad;
        }
        else if (CAD.dirDisparo == 1) // Arriba
        {
            transform.position += new Vector3(0, 1, 0) * Time.deltaTime * velocidad;
        }
        else if (CAD.dirDisparo == 3) // Izquierda
        {
            transform.position += new Vector3(-1, 0, 0) * Time.deltaTime * velocidad;
        }
        else if (CAD.dirDisparo == 4) // Derecha
        {
            transform.position += new Vector3(1, 0, 0) * Time.deltaTime * velocidad;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Limites")
        {
            Destroy(this.gameObject);  // Destruir el proyectil al tocar los límites
        }

        if (collision.gameObject.tag == "Enemigos")
        {
            // Asegúrate de que el objeto que recibe el daño tenga el script Enemigo
            collision.transform.GetComponent<Enemigo>().TomarDaño(1); // Aplicar daño al enemigo
            Destroy(this.gameObject);  // Destruir el proyectil al impactar con el enemigo
        }

        if (collision.gameObject.tag == "Boss")
        {
            // Asegúrate de que el objeto que recibe el daño tenga el script Boss
            collision.transform.GetComponent<Boss>().TomarDaño(1); // Aplicar daño al boss
            Destroy(this.gameObject);  // Destruir el proyectil al impactar con el boss
        }
    }

}

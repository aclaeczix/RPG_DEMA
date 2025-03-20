using UnityEngine;

public class MovCamara : MonoBehaviour
{
    public Camera camara;
    public Transform target;  // Referencia al jugador
    public float suavizado = 0.1f; // Controla la suavidad del seguimiento

    private bool isTeleporting = false;
    private Vector3 velocidad = Vector3.zero;

    // Variables para configurar los límites de la cámara en cada portal
    private Rect cameraBounds;  // Aquí se guardan los límites de la cámara

    private void Update()
    {
        if (!isTeleporting && target != null)
        {
            // La posición de la cámara seguirá al jugador en X e Y, pero se limita por los valores min y max
            Vector3 posicionObjetivo = new Vector3(target.position.x, target.position.y, -6);

            // Aplicar los límites de la cámara para no salir del área definida
            posicionObjetivo.x = Mathf.Clamp(posicionObjetivo.x, cameraBounds.xMin, cameraBounds.xMax);  // Límite en X
            posicionObjetivo.y = Mathf.Clamp(posicionObjetivo.y, cameraBounds.yMin, cameraBounds.yMax);  // Límite en Y

            // Suavizar el movimiento de la cámara hacia la posición objetivo
            camara.transform.position = Vector3.SmoothDamp(camara.transform.position, posicionObjetivo, ref velocidad, suavizado);
        }
    }

    private void OnTriggerEnter2D(Collider2D obj)
    {
        if (isTeleporting) return;  // Evita múltiples activaciones seguidas

        Vector3 posicionCamara = camara.transform.position;
        Vector3 posicionPlayer = target.position;

        // Definir las posiciones según los portales y sus límites
        if (obj.gameObject.CompareTag("portal1"))
        {
            posicionCamara = new Vector3(-5.28f, -13.93f, -6);
            posicionPlayer = new Vector3(-7.276f, -13.23f, 0);
            // Establecer los límites específicos para este portal
            cameraBounds = new Rect(-4.5f, -15f, 10f, 5f);  // (X min, Y min, ancho, altura)
        }
        if (obj.gameObject.CompareTag("portal2"))
        {
            posicionCamara = new Vector3(0.7750003f, 3.510999f, -6);
            posicionPlayer = new Vector3(0.775f, 3.511f, 0);
            cameraBounds = new Rect(-3f, -1f, 6f, 5f);
        }
        if (obj.gameObject.CompareTag("portal3"))
        {
            posicionCamara = new Vector3(14.1f, -0.85f, -6);
            posicionPlayer = new Vector3(12.764f, -0.528f, 0);
            cameraBounds = new Rect(10f, -5f, 8f, 5f);
        }
        if (obj.gameObject.CompareTag("portal4"))
        {
            posicionCamara = new Vector3(-1.322001f, -8.352998f, -6);
            posicionPlayer = new Vector3(-1.422f, -8.553f, 0);
            cameraBounds = new Rect(-5f, -12f, 6f, 6f);
        }
        if (obj.gameObject.CompareTag("portal5"))
        {
            posicionCamara = new Vector3(-5.28f, 9.61f, -6);
            posicionPlayer = new Vector3(-6.69f, 9.49f, 0);
            cameraBounds = new Rect(-4.5f, 5f, 6f, 5f);
        }
        if (obj.gameObject.CompareTag("portal6"))
        {
            posicionCamara = new Vector3(19.54f, 4.65f, -6);
            posicionPlayer = new Vector3(19.53f, 3.27f, 0);
            cameraBounds = new Rect(15f, 2f, 8f, 5f);
        }
        if (obj.gameObject.CompareTag("portal7"))
        {
            posicionCamara = new Vector3(-24.92f, 0.3f, -6);
            posicionPlayer = new Vector3(-26.65f, -0.54f, 0);
            cameraBounds = new Rect(-30f, -3f, 10f, 5f);
        }
        if (obj.gameObject.CompareTag("portal8"))
        {
            posicionCamara = new Vector3(5.978003f, 11.105f, -6);
            posicionPlayer = new Vector3(6.678f, 11.505f, 0);
            cameraBounds = new Rect(2f, 9f, 6f, 5f);
        }
        if (obj.gameObject.CompareTag("portal9"))
        {
            posicionCamara = new Vector3(1.790559f, 3.673067f, -6);
            posicionPlayer = new Vector3(0.775f, 3.511f, 0);
            cameraBounds = new Rect(-1f, 1f, 3f, 5f);
        }

        // Mover la cámara y al jugador a la nueva posición
        MoverCamaraYJugador(posicionCamara, posicionPlayer);
    }

    private void MoverCamaraYJugador(Vector3 posicionCamara, Vector3 posicionPlayer)
    {
        isTeleporting = true;
        camara.transform.position = posicionCamara;
        target.position = posicionPlayer;

        // Reactivar el seguimiento después de un pequeño retraso
        Invoke(nameof(ReactivarSeguimiento), 0.2f);
    }

    private void ReactivarSeguimiento()
    {
        isTeleporting = false;
    }
}

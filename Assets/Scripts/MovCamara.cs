using UnityEngine;

public class MovCamara : MonoBehaviour
{
    public Camera camara; // Arrástrala en el Inspector

    private void OnTriggerEnter2D(Collider2D obj)
    {
        if (obj.gameObject.CompareTag("portal1"))
        {
            Vector3 posicionCamara = new Vector3(-3.5f, -13, -10); // -10 para respetar la profundidad de la cámara en 2D
            camara.transform.position = posicionCamara;

            Vector3 posicionPlayer = new Vector3(-8.7f, -13.24f, 0); // Z=0 para mantenerlo en 2D
            this.transform.position = posicionPlayer;
        }
    }
}


using UnityEngine;

public class SonidoColeccionable : MonoBehaviour
{
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogWarning("Falta AudioSource en " + gameObject.name);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (audioSource != null && audioSource.clip != null)
            {
                AudioSource.PlayClipAtPoint(audioSource.clip, transform.position);
            }

            // Aquí puedes poner cualquier otra lógica que quieras que haga el coleccionable al ser recogido
            // Ejemplo: Destroy(gameObject);
        }
    }
}


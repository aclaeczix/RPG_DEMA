using UnityEngine;
using System.Collections.Generic;

public class AudioController : MonoBehaviour
{
    [System.Serializable]
    public class ZonaAudio
    {
        public Rect bounds;
        public AudioSource audioSource;
    }

    public List<ZonaAudio> zonasAudio;

    private AudioSource audioActivo = null;

    // Método que actualiza qué audio debe sonar según la posición del jugador
    public void ActualizarAudioPorPosicion(Vector3 posicionJugador)
    {
        foreach (var zona in zonasAudio)
        {
            if (zona.bounds.Contains(new Vector2(posicionJugador.x, posicionJugador.y)))
            {
                if (audioActivo != zona.audioSource)
                {
                    if (audioActivo != null)
                        audioActivo.Stop();

                    audioActivo = zona.audioSource;
                    if (!audioActivo.isPlaying)
                        audioActivo.Play();
                }
                return;
            }
        }

        // Si no encuentra zona, apaga audio activo
        if (audioActivo != null)
        {
            audioActivo.Stop();
            audioActivo = null;
        }
    }
}


using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VidasPlayer : MonoBehaviour
{
    public Image vidaPlayer;
    private float anchoVidasPlayer;
    public static int vida;
    private bool haMuerto;
    public GameObject gameOver;
    private const int vidasINI = 5;
    public static int puedePerderVida = 1;
    public CanvasGroup panelNegro;

    private MovPlayer movimientoJugador;

    // 🔊 Sonido de Game Over
    public AudioClip sonidoGameOver;
    private AudioSource audioSource;

    void Start()
    {
        anchoVidasPlayer = vidaPlayer.GetComponent<RectTransform>().sizeDelta.x;
        haMuerto = false;
        vida = vidasINI;
        gameOver.SetActive(false);

        // 🎮 Obtener el movimiento del jugador
        GameObject player = GameObject.Find("player");
        if (player != null)
            movimientoJugador = player.GetComponent<MovPlayer>();

        // 🔊 Obtener el componente AudioSource (agregado al mismo objeto que este script)
        audioSource = GetComponent<AudioSource>();
    }

    public void TomarDaño(int daño)
    {
        if (vida > 0 && puedePerderVida == 1)
        {
            puedePerderVida = 0;
            vida -= daño;
            DibujaVida(vida);
        }

        if (vida <= 0 && !haMuerto)
        {
            haMuerto = true;
            StartCoroutine(EjecutaMuerte());
        }
    }

    public void DibujaVida(int vida)
    {
        if (vida <= vidasINI)
        {
            RectTransform transformaImagen = vidaPlayer.GetComponent<RectTransform>();
            transformaImagen.sizeDelta = new Vector2(
                anchoVidasPlayer * (float)vida / (float)vidasINI,
                transformaImagen.sizeDelta.y
            );
        }
    }

    IEnumerator EjecutaMuerte()
    {
        // 🚫 Desactiva movimiento
        if (movimientoJugador != null)
            movimientoJugador.enabled = false;

        yield return new WaitForSeconds(0.2f);

        // 🎬 Fade rápido a negro
        yield return StartCoroutine(FadePanelNegro());

        // 🔇 Detener todos los sonidos activos en la escena
        AudioSource[] todosLosAudios = UnityEngine.Object.FindObjectsByType<AudioSource>(FindObjectsSortMode.None);
        foreach (AudioSource audio in todosLosAudios)
        {
            if (audio != null)
            {
                audio.Stop();
                audio.volume = 1f;
            }
        }

        // 🔊 Reproducir sonido de Game Over
        if (sonidoGameOver != null && audioSource != null)
            audioSource.PlayOneShot(sonidoGameOver);

        // Mostrar pantalla Game Over
        gameOver.SetActive(true);

        // 🎬 Fade lento para revelar la pantalla Game Over
        yield return StartCoroutine(FadePanelNegroOut());
    }

    IEnumerator FadePanelNegro()
    {
        float tiempo = 0f;
        float duracion = 0.1f;

        while (tiempo < duracion)
        {
            tiempo += Time.deltaTime;
            float alpha = Mathf.Lerp(0f, 1f, tiempo / duracion);
            panelNegro.alpha = alpha;
            yield return null;
        }

        panelNegro.alpha = 1f;
    }

    IEnumerator FadePanelNegroOut()
    {
        float tiempo = 0f;
        float duracion = 3f;

        while (tiempo < duracion)
        {
            tiempo += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, tiempo / duracion);
            panelNegro.alpha = alpha;
            yield return null;
        }

        panelNegro.alpha = 0f;
    }
}






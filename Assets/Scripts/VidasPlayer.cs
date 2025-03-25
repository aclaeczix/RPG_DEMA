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

    void Start()
    {
        anchoVidasPlayer = vidaPlayer.GetComponent<RectTransform>().sizeDelta.x;
        haMuerto = false;
        vida = vidasINI;
        gameOver.SetActive(false);
    }

    public void TomarDa�o(int da�o)
    {
        if (vida > 0 && puedePerderVida == 1)
        {
            puedePerderVida = 0;
            vida -= da�o;
            DibujaVida(vida);
        }
        if (vida <= 0 && !haMuerto)
        {
            haMuerto = true;
            StartCoroutine(EjecutaMuerte());
        }
    }

    private void DibujaVida(int vida)
    {
        RectTransform transformaImagen = vidaPlayer.GetComponent<RectTransform>();
        transformaImagen.sizeDelta = new Vector2(anchoVidasPlayer * (float)vida / (float)vidasINI, transformaImagen.sizeDelta.y);
    }

    IEnumerator EjecutaMuerte()
    {
        yield return new WaitForSeconds(2.1f);
        gameOver.SetActive(true);
    }
}

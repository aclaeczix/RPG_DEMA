using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo : MonoBehaviour
{
    public static int vidaEnemigo = 1;
    private float frecAtaque = 2.5f, tiempoSigAtaque = 0, iniciaConteo;



    void Start()
    {
        vidaEnemigo = 1;
    }

    
    void Update()
    {
        if (tiempoSigAtaque > 0)
        {
            tiempoSigAtaque = frecAtaque + iniciaConteo - Time.time;
        }else
        {
            tiempoSigAtaque = 0;
            VidasPlayer.puedePerderVida = 1;
        }
    }

    private void OnTriggerEnter2D(Collider2D obj)
    {
        if (obj.tag == "Player")
        {
            tiempoSigAtaque = frecAtaque;
            iniciaConteo = Time.time;
            obj.transform.GetComponentInChildren<VidasPlayer>().TomarDa�o(1);
        }
    }

    public void TomarDa�o(int da�o)
    {
        vidaEnemigo -= da�o;
        if (vidaEnemigo <= 0)
        {
            Destroy(gameObject);
        }
    }

}

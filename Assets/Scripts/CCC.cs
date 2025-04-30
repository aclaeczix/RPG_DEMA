using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCC : MonoBehaviour
{
    public Transform controladorGolpe;
    public float radioGolpe;
    public int dañoGolpe;
    public float tiempoEntreAtaques;
    public float tiempoSigAtaque;
    private Animator anim;

    public static bool atacando;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (tiempoSigAtaque < 0.05f && tiempoEntreAtaques > 0)
        {
            atacando = false;
        }

        if (tiempoSigAtaque > 0)
        {
            tiempoSigAtaque -= Time.deltaTime;
        }

        if (Input.GetButtonDown("Fire1") && tiempoSigAtaque <= 0)
        {
            atacando = true;
            activaCapaCCC("Ataque");
            Golpe();
            tiempoSigAtaque = tiempoEntreAtaques;
        }
    }
    private void Golpe()
    {
        if (MovPlayer.dirAtaque == 1)
        {
            anim.SetTrigger("AtaqueArriba");
        }
        else if (MovPlayer.dirAtaque == 2)
        {
            anim.SetTrigger("AtaqueAbajo");
        }
        else if (MovPlayer.dirAtaque == 3)
        {
            anim.SetTrigger("AtaqueIzquierda");
        }
        else if (MovPlayer.dirAtaque == 4)
        {
            anim.SetTrigger("Ataquederecha");
        }
    }

    private void VerificaGolpe()
    {
        Collider2D[] objs = Physics2D.OverlapCircleAll(controladorGolpe.position, radioGolpe); //verifica que golpeó
        foreach (Collider2D colisionador in objs)
        {
            if (colisionador.CompareTag("Enemigos"))
            {
                colisionador.transform.GetComponent<Enemigo>().TomarDaño(dañoGolpe);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(controladorGolpe.position, radioGolpe);
    }

    private void activaCapaCCC(string nombre)
    {
        for (int i = 0; i < anim.layerCount; i++)
        {
            anim.SetLayerWeight(anim.GetLayerIndex(nombre), 1);
        }
    }
}




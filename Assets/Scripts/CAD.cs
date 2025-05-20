using UnityEngine;

public class CAD : MonoBehaviour
{
    [SerializeField] private GameObject proyectil;
    public float tiempoSigAtaque;
    public float tiempoEntreAtaques;
    public Transform puntoEmision;
    private Animator anim;
    public static int dirDisparo = 0;
    public static bool disparando = false;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (tiempoSigAtaque < 0.05f && tiempoEntreAtaques > 0)
        {
            disparando = false;
        }

        if (tiempoSigAtaque > 0)
        {
            tiempoSigAtaque -= Time.deltaTime;
        }

        if (Input.GetButtonDown("Fire2") && tiempoSigAtaque <= 0)
        {
            disparando = true;
            activaCapaCAD("Ataque");
            Dispara();
            tiempoSigAtaque = tiempoEntreAtaques;
        }
        void Dispara()
        {
            if (MovPlayer.dirAtaque == 1)
            {
                anim.SetTrigger("GolpeArriba");
            }
            else if (MovPlayer.dirAtaque == 2)
            {
                anim.SetTrigger("GolpeAbajo");
            }
            else if (MovPlayer.dirAtaque == 3)
            {
                anim.SetTrigger("Golpeizquierda");
            }
            else if (MovPlayer.dirAtaque == 4)
            {
                anim.SetTrigger("Golpederecha");
            }
        }
    }

    private void EmiteProyectil()
    {
        dirDisparo = MovPlayer.dirAtaque;
        GameObject proyectilInstanciado = Instantiate(proyectil, puntoEmision.position, transform.rotation);

        // Obtén el Animator del proyectil instanciado
        Animator proyectilAnimator = proyectilInstanciado.GetComponent<Animator>();

        // Asignar la dirección de disparo al proyectil
        if (dirDisparo == 1)  // Arriba
        {
            proyectilAnimator.SetFloat("MovX", 0);   // Cambiado a MovX
            proyectilAnimator.SetFloat("MovY", 1);   // Cambiado a MovY
        }
        else if (dirDisparo == 2)  // Abajo
        {
            proyectilAnimator.SetFloat("MovX", 0);   // Cambiado a MovX
            proyectilAnimator.SetFloat("MovY", -1);  // Cambiado a MovY
        }
        else if (dirDisparo == 3)  // Izquierda
        {
            proyectilAnimator.SetFloat("MovX", -1);  // Cambiado a MovX
            proyectilAnimator.SetFloat("MovY", 0);   // Cambiado a MovY
        }
        else if (dirDisparo == 4)  // Derecha
        {
            proyectilAnimator.SetFloat("MovX", 1);   // Cambiado a MovX
            proyectilAnimator.SetFloat("MovY", 0);   // Cambiado a MovY
        }
    }


    private void activaCapaCAD(string nombre)
    {
        for (int i = 0; i < anim.layerCount; i++)
        {
            anim.SetLayerWeight(i, 0); //Ambos layers con weight en 0
        }
        anim.SetLayerWeight(anim.GetLayerIndex(nombre), 1);
    }
}


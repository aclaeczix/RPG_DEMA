using UnityEngine;

public class MovPlayer : MonoBehaviour
{
    private Vector2 dirMov;
    public float velMov;
    public Rigidbody2D rb;
    public Animator anim;


    private string capaIdle = "Idle";
    private string capaCaminar = "Caminar";
    private bool PlayerMoviendose = false;
    private float ultimoMovX, ultimoMovY;

    public static int dirAtaque = 0;

    void FixedUpdate()
    {
        Movimiento();
        if (CCC.atacando == false && CAD.disparando == false)
        {
            Animacionesplayer();
        }
    }
    private void Movimiento()
    {
        float movX = Input.GetAxisRaw("Horizontal");
        float movY = Input.GetAxisRaw("Vertical");
        Debug.Log($"movX: {movX}, movY: {movY}"); // Ver valores en la consola

        dirMov = new Vector2(movX, movY).normalized;
        rb.linearVelocity = new Vector2(dirMov.x * velMov, dirMov.y * velMov); // Usar rb.velocity en lugar de rb.linearVelocity

        // Actualizar dirAtaque basado en la dirección horizontal (movX)
        if (movX == -1)
        {
            dirAtaque = 3;  // Mover hacia la izquierda
        }
        else if (movX == 1)
        {
            dirAtaque = 4;  // Mover hacia la derecha
        }

        // Aquí también puedes manejar dirAtaque para los movimientos verticales si es necesario
        if (movY == 1)
        {
            dirAtaque = 1;  // Mover hacia arriba
        }
        else if (movY == -1)
        {
            dirAtaque = 2;  // Mover hacia abajo
        }

        Debug.Log("dirAtaque actualizado a: " + dirAtaque);

        // Comprobamos si el jugador está en reposo (Idle) o moviéndose
        if (movX == 0 && movY == 0)
        {
            PlayerMoviendose = false;
        }
        else
        {
            PlayerMoviendose = true;
            ultimoMovX = movX;
            ultimoMovY = movY;
        }

        ActualizarCapa();
    }



    private void Animacionesplayer()
    {
        anim.SetFloat("movX", ultimoMovX);
        anim.SetFloat("movY", ultimoMovY);
    }

    private void ActualizarCapa()
    {

        if (CCC.atacando == false && CAD.disparando == false)
        {
            if (PlayerMoviendose)
            {
                activaCapa("Caminar");
                Debug.Log("Caminando");
            }
            else
            {
                activaCapa("Idle");
                Debug.Log("Idle");
            }
        }
        else
        {
            activaCapa("Ataque");
            Debug.Log("Ataque");
        }
    }

    private void activaCapa(string nombre)
    {
        for (int i = 0; i < anim.layerCount; i++)
        {
            anim.SetLayerWeight(i, 0); //Ambos layes con weight en 0
        }
        anim.SetLayerWeight(anim.GetLayerIndex(nombre), 1);
    }

}
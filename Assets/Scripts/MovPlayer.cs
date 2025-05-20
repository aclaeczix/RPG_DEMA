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

    // NUEVO: AudioSource para sonido de caminar
    public AudioSource audioCaminar;

    void Start()
    {
        if (audioCaminar == null)
            audioCaminar = GetComponent<AudioSource>();
    }

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
        Debug.Log($"movX: {movX}, movY: {movY}");

        dirMov = new Vector2(movX, movY).normalized;
        rb.linearVelocity = new Vector2(dirMov.x * velMov, dirMov.y * velMov);

        if (movX == -1) dirAtaque = 3;
        else if (movX == 1) dirAtaque = 4;

        if (movY == 1) dirAtaque = 1;
        else if (movY == -1) dirAtaque = 2;

        Debug.Log("dirAtaque actualizado a: " + dirAtaque);

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

        // Aquí controlamos el audio
        if (PlayerMoviendose)
        {
            if (!audioCaminar.isPlaying)
                audioCaminar.Play();
        }
        else
        {
            if (audioCaminar.isPlaying)
                audioCaminar.Stop();
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
            anim.SetLayerWeight(i, 0);
        }
        anim.SetLayerWeight(anim.GetLayerIndex(nombre), 1);
    }
}

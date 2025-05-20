using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Boss : MonoBehaviour
{
    public int vidaEnemigo = 15;
    private float freqAtaque = 2.5f, tiempoSigAtaque = 0, iniciaConteo;

    public Transform personaje;
    private NavMeshAgent agente;
    public Transform[] puntosRuta;
    private int indiceRuta = 0;
    private bool playerEnRango = false;
    [SerializeField] private float distanciaDeteccionPlayer;
    private SpriteRenderer spriteEnemigo;
    private Transform mirarHacia;

    private Animator animator;

    public GameObject winScreen;           // Pantalla de victoria (GameObject)
    public CanvasGroup panelNegro;         // Panel negro para fade
    public AudioClip sonidoWin;             // Clip de sonido de victoria
    private AudioSource audioSource;

    private void Awake()
    {
        agente = GetComponent<NavMeshAgent>();
        spriteEnemigo = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        vidaEnemigo = 15;
        agente.updateRotation = false;
        agente.updateUpAxis = false;
        if (winScreen != null)
            winScreen.SetActive(false);    // Asegura que la pantalla de Win esté apagada al inicio
        if (panelNegro != null)
            panelNegro.alpha = 0f;          // Panel negro transparente al inicio
    }

    void Update()
    {
        this.transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        float distancia = Vector3.Distance(personaje.position, this.transform.position);

        if (this.transform.position == puntosRuta[indiceRuta].position)
        {
            if (indiceRuta < puntosRuta.Length - 1)
            {
                indiceRuta++;
            }
            else if (indiceRuta == puntosRuta.Length - 1)
            {
                indiceRuta = 0;
            }
        }

        playerEnRango = distancia < distanciaDeteccionPlayer;

        if (tiempoSigAtaque > 0)
        {
            tiempoSigAtaque = freqAtaque + iniciaConteo - Time.time;
        }
        else
        {
            tiempoSigAtaque = 0;
            VidasPlayer.puedePerderVida = 1;
            SigueAlPlayer(playerEnRango);
            RotaEnemigo();
        }

        ActualizarAnimaciones();
    }

    private void SigueAlPlayer(bool playerEnRango)
    {
        if (playerEnRango)
        {
            agente.SetDestination(personaje.position);
            mirarHacia = personaje;
        }
        else
        {
            agente.SetDestination(puntosRuta[indiceRuta].position);
            mirarHacia = puntosRuta[indiceRuta];
        }
    }

    private void RotaEnemigo()
    {
        spriteEnemigo.flipX = (this.transform.position.x > mirarHacia.position.x);
    }

    private void ActualizarAnimaciones()
    {
        Vector3 movimiento = agente.velocity;
        animator.SetFloat("MovX", movimiento.x);
        animator.SetFloat("MovY", movimiento.y);
        animator.SetFloat("Speed", movimiento.magnitude);
    }

    private void OnTriggerEnter2D(Collider2D obj)
    {
        if (obj.tag == "Player")
        {
            tiempoSigAtaque = freqAtaque;
            iniciaConteo = Time.time;
            obj.transform.GetComponentInChildren<VidasPlayer>().TomarDaño(3);
        }
    }

    public void TomarDaño(int daño)
    {
        vidaEnemigo -= daño;
        if (vidaEnemigo <= 0)
        {
            StartCoroutine(ProcesoVictoria());
        }
    }

    private IEnumerator ProcesoVictoria()
    {
        // Fade a negro rápido
        if (panelNegro != null)
        {
            yield return StartCoroutine(FadePanelNegro());
        }

        // Detener todos los sonidos activos
        AudioSource[] todosLosAudios = UnityEngine.Object.FindObjectsByType<AudioSource>(FindObjectsSortMode.None);
        foreach (AudioSource audio in todosLosAudios)
        {
            if (audio != null)
            {
                audio.Stop();
                audio.volume = 1f;
            }
        }

        // Reproducir sonido de victoria
        if (sonidoWin != null && audioSource != null)
        {
            audioSource.PlayOneShot(sonidoWin);
        }

        // Mostrar pantalla de victoria
        if (winScreen != null)
            winScreen.SetActive(true);

        // Fade out del negro para revelar la pantalla
        if (panelNegro != null)
        {
            yield return StartCoroutine(FadePanelNegroOut());
        }

        // Destruir boss para limpiar escena
        Destroy(gameObject);
    }

    private IEnumerator FadePanelNegro()
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

    private IEnumerator FadePanelNegroOut()
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



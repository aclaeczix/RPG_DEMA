using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemigo : MonoBehaviour
{
    public int vidaEnemigo = 3;
    private float freqAtaque = 2.5f, tiempoSigAtaque = 0, iniciaConteo;

    public Transform personaje;
    private NavMeshAgent agente;
    public Transform[] puntosRuta;
    private int indiceRuta = 0;
    private bool playerEnRango = false;
    [SerializeField] private float distanciaDeteccionPlayer;
    private SpriteRenderer spriteEnemigo;
    private Transform mirarHacia;

    private Animator animator; // Agregado para controlar las animaciones

    private void Awake()
    {
        agente = GetComponent<NavMeshAgent>();
        spriteEnemigo = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>(); // Obtenemos el Animator
    }

    void Start()
    {
        vidaEnemigo = 2;
        agente.updateRotation = false;
        agente.updateUpAxis = false;
    }

    void Update()
    {
        this.transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        float distancia = Vector3.Distance(personaje.position, this.transform.position);

        // Movimiento entre puntos predeterminados
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

        // Detección del jugador
        if (distancia < distanciaDeteccionPlayer)
        {
            playerEnRango = true;
        }
        else
        {
            playerEnRango = false;
        }

        // Ataque si el tiempo ha pasado
        if (tiempoSigAtaque > 0)
        {
            tiempoSigAtaque = freqAtaque + iniciaConteo - Time.time;
        }
        else
        {
            tiempoSigAtaque = 0;
            VidasPlayer.puedePerderVida = 1;
            SigueAlPlayer(playerEnRango); // El enemigo sigue al jugador
            RotaEnemigo(); // El enemigo rota hacia el jugador
        }

        // Actualizar animaciones en función del movimiento
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
        // Rotar al enemigo hacia el jugador
        if (this.transform.position.x > mirarHacia.position.x)
        {
            spriteEnemigo.flipX = true; // Enemigo mirando hacia la izquierda
        }
        else
        {
            spriteEnemigo.flipX = false; // Enemigo mirando hacia la derecha
        }
    }

    private void ActualizarAnimaciones()
    {
        // Obtener la velocidad del NavMeshAgent
        Vector3 movimiento = agente.velocity;

        // Depurar los valores de MoveX y MoveY
        Debug.Log("MovX: " + movimiento.x + " | MovY: " + movimiento.y);

        // Actualizar los parámetros MoveX y MoveY en el Animator
        animator.SetFloat("MovX", movimiento.x);  // Movimiento horizontal (izquierda/derecha)
        animator.SetFloat("MovY", movimiento.y);  // Movimiento vertical (arriba/abajo)

        // Actualizar el parámetro Speed (si tienes un parámetro "Speed")
        animator.SetFloat("Speed", movimiento.magnitude);  // Velocidad total
    }

    private void OnTriggerEnter2D(Collider2D obj)
    {
        if (obj.tag == "Player")
        {
            tiempoSigAtaque = freqAtaque;
            iniciaConteo = Time.time;
            obj.transform.GetComponentInChildren<VidasPlayer>().TomarDaño(1);
        }
    }

    // Método para tomar daño
    public void TomarDaño(int daño)
    {
        vidaEnemigo -= daño;
        if (vidaEnemigo <= 0)
        {
            // Incrementar el contador de enemigos muertos
            ContadorMonedas contador = FindObjectOfType<ContadorMonedas>();
            if (contador != null)
            {
                contador.IncrementarEnemigos();
            }

            Destroy(gameObject);  // El enemigo muere
        }
    }
}

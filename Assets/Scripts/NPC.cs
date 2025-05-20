using UnityEngine;

public class NPC : MonoBehaviour
{
    public GameObject txtDialogo;
    public Sprite txt1, txt2;

    private int numVisitas;  // Se hace privado para que cada NPC tenga su propio contador

    void Start()
    {
        txtDialogo.SetActive(false);  // Asegurarse de que el diálogo esté oculto al inicio
        numVisitas = 0;  // Inicializamos el contador de visitas para este NPC
    }

    private void OnTriggerEnter2D(Collider2D obj)
    {
        // Si el jugador entra en el collider del NPC
        if (obj.CompareTag("Player"))
        {
            txtDialogo.SetActive(true);  // Mostrar el cuadro de diálogo
            EscribeDialogo();  // Cambiar el diálogo dependiendo de las visitas
            numVisitas++;  // Incrementar el contador de visitas
        }
    }

    private void OnTriggerExit2D(Collider2D obj)
    {
        // Si el jugador sale del área del NPC
        if (obj.CompareTag("Player"))
        {
            txtDialogo.SetActive(false);  // Ocultar el cuadro de diálogo
        }
    }

    private void EscribeDialogo()
    {
        // Cambiar el texto del cuadro de diálogo dependiendo de las visitas
        switch (numVisitas)
        {
            case 0:
                txtDialogo.GetComponent<SpriteRenderer>().sprite = txt1;  // Primer diálogo
                break;
            case 1:
                txtDialogo.GetComponent<SpriteRenderer>().sprite = txt2;  // Segundo diálogo
                break;
            default:
                txtDialogo.GetComponent<SpriteRenderer>().sprite = txt1;  // Vuelve al primer diálogo si las visitas son mayores a 1
                break;
        }
    }
}


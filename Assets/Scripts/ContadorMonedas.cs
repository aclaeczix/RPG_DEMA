using TMPro;
using UnityEngine;

public class ContadorMonedas : MonoBehaviour
{
    public TextMeshProUGUI textoDoradas;
    public TextMeshProUGUI textoPlateadas;
    public TextMeshProUGUI textoEnemigos;  // Nuevo campo para mostrar contador de enemigos

    private int cantidadDoradas = 0;
    private int cantidadPlateadas = 0;
    private int cantidadEnemigos = 0;      // Nuevo contador de enemigos

    public void SumarDoradas()
    {
        cantidadDoradas++;
        textoDoradas.text = " x " + cantidadDoradas;
    }

    public void SumarPlateadas()
    {
        cantidadPlateadas++;
        textoPlateadas.text = " x " + cantidadPlateadas;
    }

    public void IncrementarEnemigos()
    {
        cantidadEnemigos++;
        textoEnemigos.text = " x " + cantidadEnemigos;
    }
}

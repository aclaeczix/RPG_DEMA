using UnityEngine;

public class Coleccionables : MonoBehaviour
{
    private GameObject player;
    public static string objAColeccionar;
    private Inventario inventario;
    private ContadorMonedas contador;


    void Start()
    {
        player = GameObject.Find("player");
        objAColeccionar = "";

        inventario = GameObject.Find("InventarioManager").GetComponent<Inventario>();

        contador = GameObject.Find("ContadorManager").GetComponent<ContadorMonedas>();

    }

    private void OnTriggerEnter2D(Collider2D obj)
    {
        if (obj.tag == "Vida")
        {
            VidasPlayer.vida++;
            player.GetComponent<VidasPlayer>().DibujaVida(VidasPlayer.vida);
            Destroy(obj.gameObject);
        }

        if (obj.tag == "Mana")
        {
            Destroy(obj.gameObject);
        }

        if (obj.tag == "Pan")
        {
            AplicaCambios(obj);
        }

        if (obj.tag == "GoldenCoin")
        {
            ContarMonedas(obj);
        }

        if (obj.tag == "Llave")
        {
            AplicaCambios(obj);
        }
        if (obj.tag == "Totem")
        {
            AplicaCambios(obj);
        }

        if (obj.tag == "SilverCoin")
        {
            ContarMonedas(obj);
        }

        if (obj.tag == "Iron")
        {
            AplicaCambios(obj);
        }

        if (obj.tag == "Cloth")
        {
            AplicaCambios(obj);
        }

        if (obj.tag == "Ring")
        {
            AplicaCambios(obj);
        }

        if (obj.tag == "Hierbas")
        {
            AplicaCambios(obj);
        }

        if (obj.tag == "Lazo")
        {
            AplicaCambios(obj);
        }

        if (obj.tag == "Piel")
        {
            AplicaCambios(obj);
        }
    }

    private void AplicaCambios(Collider2D obj)
    {
        objAColeccionar = obj.tag;
        inventario.EscribeEnArreglo();
        Destroy(obj.gameObject);
    }

    private void ContarMonedas(Collider2D obj)
    {
        if (obj.tag == "GoldenCoin")
        {
            contador.SumarDoradas();
        }
        else if (obj.tag == "SilverCoin")
        {
            contador.SumarPlateadas();
        }

        Destroy(obj.gameObject);
    }

}



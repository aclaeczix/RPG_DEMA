using UnityEngine;
using UnityEngine.UI;

public class Inventario : MonoBehaviour
{
    private bool muestraInventario;
    public GameObject goInventario;
    [SerializeField] private string[] valoresInventario;
    private int  numIron, numTotem, numHierbas, numPan;
    Button boton; // Botones del inventario
    public Sprite Pan, GoldenCoin, Llave, Totem, SilverCoin, Iron, Cloth, Ring, Hierbas, Lazo, Piel, contenedor;

    public AudioSource audioCaminar; 

    void Start()
    {
        muestraInventario = false;
        BorrarArreglo();
        numIron = 0;       
        numTotem = 0;
        numHierbas = 0;
        numPan = 0;
    }

    public void StatusInventario()
    {
        if (muestraInventario)
        {
            muestraInventario = false;
            goInventario.SetActive(false);
            Time.timeScale = 1;

            if (audioCaminar != null)
                audioCaminar.UnPause();  // Reanuda el audio de caminar
        }
        else
        {
            muestraInventario = true;
            goInventario.SetActive(true);
            Time.timeScale = 0;

            if (audioCaminar != null)
                audioCaminar.Pause();    // Pausa el audio de caminar
        }
    }

    public void EscribeEnArreglo()
    {
        if (VerificaEnArreglo() == -1)
        {
            for (int i = 0; i < valoresInventario.Length; i++)
            {
                if (valoresInventario[i] == "")
                {
                    valoresInventario[i] = Coleccionables.objAColeccionar;
                    DibujaElementos(i);
                    break;
                }
            }
        }
        else
        {
            DibujaElementos(VerificaEnArreglo());
        }
    }

    private int VerificaEnArreglo()
    {
        int pos = -1;
        for (int i = 0; i < valoresInventario.Length; i++)
        {
            if (valoresInventario[i] == Coleccionables.objAColeccionar)
            {
                pos = i;
                break;
            }
        }
        return pos;
    }

    public void DibujaElementos(int pos)
    {
        StatusInventario();
        boton = GameObject.Find("Elemento (" + pos + ")").GetComponent<Button>();

        switch (Coleccionables.objAColeccionar)
        {
            case "Llave":
                contenedor = Llave;
                break;
            case "Cloth":
                contenedor = Cloth;
                break;
            case "Ring":
                contenedor = Ring;
                break;
            case "Lazo":
                contenedor = Lazo;
                break;
            case "Piel":
                contenedor = Piel;
                break;
            case "Iron":
                contenedor = Iron;
                numIron++;
                boton.GetComponentInChildren<Text>().text = "x" + numIron.ToString();
                break;
            case "Totem":
                contenedor = Totem;
                numTotem++;
                boton.GetComponentInChildren<Text>().text = "x" + numTotem.ToString();
                break;
            case "Hierbas":
                contenedor = Hierbas;
                numHierbas++;
                boton.GetComponentInChildren<Text>().text = "x" + numHierbas.ToString();
                break;
            case "Pan":
                contenedor = Pan;
                numPan++;
                boton.GetComponentInChildren<Text>().text = "x" + numPan.ToString();
                break;
        }
        boton.GetComponent<Image>().sprite = contenedor;
    }

    private void BorrarArreglo()
    {
        for (int i = 0; i < valoresInventario.Length; i++)
        {
            valoresInventario[i] = "";
        }
    }
}


using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public Button BtnJugar;
    public Button BtnOpciones;
    public Button BtnSalir;
    public Button BtnCerrarOpciones;      // Botón para cerrar el panel de opciones
    public Button BtnCreditos;            // Botón para mostrar los créditos
    public Button BtnCerrarCreditos;      // Botón para cerrar los créditos

    public GameObject panelOpciones;      // El panel que contiene el slider de volumen
    public GameObject panelCreditos;      // Nuevo panel para los créditos
    public Slider sliderVolumen;          // Slider con rango 0-100

    void Start()
    {
        BtnJugar.onClick.AddListener(Jugar);
        BtnOpciones.onClick.AddListener(AbrirOpciones);
        BtnSalir.onClick.AddListener(Salir);
        BtnCerrarOpciones.onClick.AddListener(CerrarOpciones);  // Asignar la función para cerrar las opciones
        BtnCreditos.onClick.AddListener(MostrarCreditos);        // Asignar la función para mostrar créditos
        BtnCerrarCreditos.onClick.AddListener(CerrarCreditos);    // Asignar la función para cerrar los créditos

        if (panelOpciones != null)
            panelOpciones.SetActive(false);

        if (panelCreditos != null)
            panelCreditos.SetActive(false);  // Asegurarse de que los créditos estén ocultos al inicio

        if (sliderVolumen != null)
        {
            // Carga volumen guardado (0.0-1.0) y ajusta slider (0-100)
            float volumenGuardado = PlayerPrefs.GetFloat("VolumenGeneral", 1f);
            sliderVolumen.value = volumenGuardado * 100f;
            sliderVolumen.onValueChanged.AddListener(CambiarVolumen);
        }

        AudioListener.volume = PlayerPrefs.GetFloat("VolumenGeneral", 1f);
    }

    public void Jugar()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("SampleScene");
    }

    public void AbrirOpciones()
    {
        if (panelOpciones != null)
        {
            bool estaActivo = panelOpciones.activeSelf;
            panelOpciones.SetActive(!estaActivo);

            if (!estaActivo)
            {
                // Al abrir el panel, poner volumen a 100 y actualizar volumen
                sliderVolumen.value = 100f;
                CambiarVolumen(100f);
            }
        }
        else
        {
            Debug.LogWarning("No has asignado el panel de opciones.");
        }
    }

    // Función para cerrar el panel de opciones
    public void CerrarOpciones()
    {
        panelOpciones.SetActive(false);  // Cierra el panel de opciones
        Time.timeScale = 1;              // Reanuda el juego
    }

    // Función para mostrar los créditos
    public void MostrarCreditos()
    {
        if (panelCreditos != null)
        {
            bool estaActivo = panelCreditos.activeSelf;
            panelCreditos.SetActive(!estaActivo);  // Alterna la visibilidad del panel de créditos
        }
    }

    // Función para cerrar el panel de créditos y regresar al menú
    public void CerrarCreditos()
    {
        panelCreditos.SetActive(false);  // Cierra el panel de créditos
        Time.timeScale = 1;              // Reanuda el juego
    }

    // Función para cambiar el volumen
    public void CambiarVolumen(float valor)
    {
        float volumenNormalizado = valor / 100f;  // Convertir de 0-100 a 0.0-1.0
        AudioListener.volume = volumenNormalizado;
        PlayerPrefs.SetFloat("VolumenGeneral", volumenNormalizado);  // Guarda el volumen
    }

    public void Salir()
    {
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}




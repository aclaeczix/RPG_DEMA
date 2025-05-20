using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Opciones : MonoBehaviour
{
    public Button BtnOpciones;        // El bot�n de opciones
    public Button BtnCerrar;          // El bot�n de cierre dentro del panel de opciones
    public Button BtnCreditos;        // Nuevo bot�n de cr�ditos
    public Button BtnCerrarCreditos;  // Bot�n para cerrar el panel de cr�ditos
    public Button BtnQuitar;
    public Button BtnMenu;
    public Button BtnReiniciar;

    public GameObject panelOpciones;  // El panel que contiene el slider de volumen
    public GameObject panelCreditos;  // El panel que contiene los cr�ditos
    public Slider sliderVolumen;      // Slider con rango de 0 a 100 para el volumen

    void Start()
    {
        // Asigna la funci�n al bot�n de opciones
        BtnOpciones.onClick.AddListener(AbrirOpciones);

        // Asigna la funci�n al bot�n de cerrar dentro del panel de opciones
        BtnCerrar.onClick.AddListener(CerrarOpciones);

        // Asigna la funci�n al bot�n de cr�ditos
        BtnCreditos.onClick.AddListener(MostrarCreditos);

        // Asigna la funci�n al bot�n de cerrar dentro del panel de cr�ditos
        BtnCerrarCreditos.onClick.AddListener(CerrarCreditos);

        BtnQuitar.onClick.AddListener(Salir);

        BtnMenu.onClick.AddListener(Menu);

        BtnReiniciar.onClick.AddListener(Reiniciar);

        // Inicializa el panel de opciones como desactivado
        if (panelOpciones != null)
            panelOpciones.SetActive(false);

        // Inicializa el panel de cr�ditos como desactivado
        if (panelCreditos != null)
            panelCreditos.SetActive(false);

        // Inicializa el slider con el valor guardado o por defecto
        if (sliderVolumen != null)
        {
            float volumenGuardado = PlayerPrefs.GetFloat("VolumenGeneral", 1f);
            sliderVolumen.value = volumenGuardado * 100f;  // Convierte de 0-1 a 0-100 para el slider
            sliderVolumen.onValueChanged.AddListener(CambiarVolumen);
        }

        AudioListener.volume = PlayerPrefs.GetFloat("VolumenGeneral", 1f);  // Aplica el volumen al iniciar
    }

    // Funci�n para abrir y cerrar el panel de opciones
    public void AbrirOpciones()
    {
        if (panelOpciones != null)
        {
            bool estaActivo = panelOpciones.activeSelf;
            panelOpciones.SetActive(!estaActivo);

            if (!estaActivo)
            {
                sliderVolumen.value = 100f;  // Resetea el volumen a 100
                CambiarVolumen(100f);
                Time.timeScale = 0;          // Pausa el juego cuando se abre el panel
            }
        }
        else
        {
            Debug.LogWarning("No has asignado el panel de opciones.");
        }
    }

    // Funci�n para cerrar el panel de opciones
    public void CerrarOpciones()
    {
        panelOpciones.SetActive(false);  // Cierra el panel de opciones
        Time.timeScale = 1;              // Reanuda el juego cuando se cierra
    }

    // Funci�n para cambiar el volumen
    public void CambiarVolumen(float valor)
    {
        float volumenNormalizado = valor / 100f;  // Convierte de 0-100 a 0.0-1.0
        AudioListener.volume = volumenNormalizado;
        PlayerPrefs.SetFloat("VolumenGeneral", volumenNormalizado);  // Guarda el volumen
    }

    // Funci�n para mostrar los cr�ditos
    public void MostrarCreditos()
    {
        if (panelCreditos != null)
        {
            bool estaActivo = panelCreditos.activeSelf;
            panelCreditos.SetActive(!estaActivo);  // Alterna la visibilidad del panel de cr�ditos
        }
    }

    // Funci�n para cerrar el panel de cr�ditos y regresar al men�
    public void CerrarCreditos()
    {
        panelCreditos.SetActive(false);
        panelOpciones.SetActive(false); // Cierra el panel de cr�ditos
        Time.timeScale = 1;              // Reanuda el juego cuando se cierran los cr�ditos
    }
    public void Salir()
    {
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    public void Menu()
    {
        SceneManager.LoadScene("MenuInicio");
    }

    public void Reiniciar()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("SampleScene");
    }
}


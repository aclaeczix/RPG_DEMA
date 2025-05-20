using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public Button BtnJugar;
    public Button BtnSalir;
    public Button BtnMenu;

    void Start()
    {
        BtnJugar.onClick.AddListener(Jugar);
        BtnSalir.onClick.AddListener(Salir);
        BtnMenu.onClick.AddListener(Menu);
    }

    // Función para jugar nuevamente
    public void Jugar()
    {
        SceneManager.LoadScene("SampleScene");  // Asegúrate de que la escena esté bien configurada
    }

    // Función para ir al menú principal
    public void Menu()
    {
        SceneManager.LoadScene("MenuInicio");
    }

    // Función para salir del juego
    public void Salir()
    {
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}

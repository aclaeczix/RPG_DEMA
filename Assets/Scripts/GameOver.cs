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

    // Funci�n para jugar nuevamente
    public void Jugar()
    {
        SceneManager.LoadScene("SampleScene");  // Aseg�rate de que la escena est� bien configurada
    }

    // Funci�n para ir al men� principal
    public void Menu()
    {
        SceneManager.LoadScene("MenuInicio");
    }

    // Funci�n para salir del juego
    public void Salir()
    {
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}

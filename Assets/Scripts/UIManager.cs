using UnityEngine;
using UnityEngine.SceneManagement; 
public class UIManager : MonoBehaviour
{
    public void StartGame()
    {
        // Carrega a cena principal
        SceneManager.LoadScene("Main");
    }
    public void QuitGame()
    {
        Debug.Log("Saindo do jogo...");
        //Sai do jogo
        Application.Quit();
    }
}

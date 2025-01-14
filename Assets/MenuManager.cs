using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {
    // Referências aos painéis de UI.
    public GameObject startMenuPanel;
    public GameObject restartMenuPanel;

    void Start() {
        // Ativa o menu inicial e desativa o menu de reinício.
        startMenuPanel.SetActive(true);
        restartMenuPanel.SetActive(false);

        // Pausa o jogo até o jogador pressionar Start.
        Time.timeScale = 0;
    }

    // Chamado pelo botão Start.
    public void StartGame() {
        startMenuPanel.SetActive(false); // Oculta o menu inicial.
        Time.timeScale = 1;             // Retoma o jogo.
    }

    // Exibe o menu de reinício.
    public void ShowRestartMenu() {
        restartMenuPanel.SetActive(true);
        Time.timeScale = 0; // Pausa o jogo.
    }

    // Chamado pelo botão Restart.
    public void RestartGame() {
        // Reinicia a cena atual.
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1; // Retoma o jogo.
    }

    // Chamado pelo botão Quit.
    public void QuitGame() {
        // Fecha o jogo.
        Application.Quit();
    }
}

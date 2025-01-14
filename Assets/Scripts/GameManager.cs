using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    [Header("UI Panels")]
    public GameObject gameStartPanel;  // Painel de início do jogo.
    public GameObject gameEndPanel;   // Painel de fim de jogo.

    [Header("UI Elements")]
    public TMPro.TextMeshProUGUI scoreText;       // Referência ao texto da pontuação.
    public TMPro.TextMeshProUGUI endGameText;     // Referência ao texto de "You Win!" ou "Game Over!".

    [Header("Game State")]
    public bool isGameOver = false;   // Indica se o jogo terminou.

    void Start() {
        // Ativa o menu inicial e desativa o menu de fim de jogo.
        gameStartPanel.SetActive(true);
        gameEndPanel.SetActive(false);

        // Pausa o jogo até o jogador pressionar Start.
        Time.timeScale = 0;
    }

    // Chamado pelo botão Start.
    public void StartGame() {
        gameStartPanel.SetActive(false); // Oculta o menu inicial.
        Time.timeScale = 1;             // Retoma o jogo.
    }

    // Exibe o menu de fim de jogo (Game Over ou vitória) com o score.
    public void ShowEndGameMenu(bool didWin, int score) {
        isGameOver = true;              // Atualiza o estado do jogo.
        gameEndPanel.SetActive(true);   // Ativa o painel de fim de jogo.

        // Atualiza o texto de vitória ou derrota.
        endGameText.text = didWin ? "You Win!" : "Game Over!";

        // Atualiza o texto de pontuação.
        scoreText.text = "Your Score: " + score;

        // Pausa o jogo.
        Time.timeScale = 0;
    }

    // Chamado pelo botão Restart.
    public void RestartGame() {
        // Reinicia a cena atual.
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1; // Retoma o jogo.
    }

    // Chamado pelo botão Quit.
    public void QuitGame() {
        // Fecha o jogo (não funciona no editor da Unity).
        Application.Quit();
    }
}

using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour {
    private Rigidbody rb;
    private int count;
    private float movementX;
    private float movementY;

    [Header("Player Settings")]
    public float speed = 10f;
    public float fallThreshold = -10f; // Limite para queda do mapa.

    [Header("UI Elements")]
    public TextMeshProUGUI countText;

    [Header("Game Management")]
    public GameManager gameManager; // Referência ao GameManager.

    private const int WinConditionCount = 12;

    void Start() {
        rb = GetComponent<Rigidbody>();

        // Verifica se os elementos foram atribuídos corretamente.
        if (countText == null || gameManager == null) {
            Debug.LogError("UI elements or GameManager not assigned!");
            enabled = false;
            return;
        }

        count = 0;
        UpdateCountText();
    }

    void Update() {
        // Verifica se o jogador caiu do mapa.
        if (transform.position.y < fallThreshold) {
            HandleGameOver();
        }
    }

    void OnMove(InputValue movementValue) {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    private void FixedUpdate() {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        rb.AddForce(movement * speed);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("PickUp")) {
            other.gameObject.SetActive(false);
            count++;
            UpdateCountText();
        }
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Enemy")) {
            HandleGameOver();
        }
    }

    private void UpdateCountText() {
        countText.text = $"Count: {count}";

        if (count >= WinConditionCount) {
            DisplayWinText();
        }
    }

    private void DisplayWinText() {
        if (gameManager != null) {
            gameManager.ShowEndGameMenu(true, count); // Vitória com pontuação.
        }
    }

    private void HandleGameOver() {
        if (gameManager != null) {
            gameManager.ShowEndGameMenu(false, count); // Derrota com pontuação.
        }

        Destroy(gameObject); // Opcional: remove o jogador da cena.
    }
}

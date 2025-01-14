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
    public GameObject winTextObject;

    private const int WinConditionCount = 12;

    void Start() {
        rb = GetComponent<Rigidbody>();

        if (countText == null || winTextObject == null) {
            Debug.LogError("UI elements are not assigned in the Inspector!");
            enabled = false;
            return;
        }

        count = 0;
        UpdateCountText();
        winTextObject.SetActive(false);
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
        winTextObject.SetActive(true);
        winTextObject.GetComponent<TextMeshProUGUI>().text = "You Win!";
        Destroy(GameObject.FindGameObjectWithTag("Enemy"));
    }

    private void HandleGameOver() {
        Destroy(gameObject);
        winTextObject.SetActive(true);
        winTextObject.GetComponent<TextMeshProUGUI>().text = "Game Over!";
    }
}
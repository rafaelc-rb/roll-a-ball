using UnityEngine;

public class CameraController : MonoBehaviour {
    [Header("Target Settings")]
    public GameObject player; // Referência ao objeto do jogador.

    [Header("Camera Settings")]
    public float smoothSpeed = 5f; // Velocidade de suavização.
    public Vector3 offset;        // Offset ajustável no Inspector.

    // Inicialização
    void Start() {
        if (player == null) {
            Debug.LogError("Player não atribuído ao CameraController.");
            return;
        }

        // Calcula o offset inicial apenas se não for configurado manualmente.
        if (offset == Vector3.zero) {
            offset = transform.position - player.transform.position;
        }
    }

    // Atualização de câmera após o jogador se mover
    void LateUpdate() {
        if (player != null) {
            // Calcula a posição desejada.
            Vector3 desiredPosition = player.transform.position + offset;

            // Suaviza o movimento da câmera.
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

            // Atualiza a posição da câmera.
            transform.position = smoothedPosition;
        }
    }

    // Para debug e ajustes no Editor.
    private void OnDrawGizmosSelected() {
        if (player != null) {
            // Desenha uma linha para mostrar o offset.
            Gizmos.color = Color.green;
            Gizmos.DrawLine(player.transform.position, player.transform.position + offset);
        }
    }
}

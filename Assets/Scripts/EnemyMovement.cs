using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour {
    // Referência ao Transform do jogador.
    public Transform player;

    // Referência ao componente NavMeshAgent.
    private NavMeshAgent navMeshAgent;

    // Distância mínima para continuar perseguindo o jogador.
    public float chaseDistance = 20f;

    // Start é chamado antes do primeiro frame update.
    void Start() {
        // Tenta obter e armazenar o componente NavMeshAgent.
        navMeshAgent = GetComponent<NavMeshAgent>();

        // Verifica se o NavMeshAgent está configurado.
        if (navMeshAgent == null) {
            Debug.LogError("NavMeshAgent não encontrado no inimigo.");
        }
    }

    // Update é chamado a cada frame.
    void Update() {
        // Se há referência ao jogador e ao NavMeshAgent...
        if (player != null && navMeshAgent != null && navMeshAgent.isActiveAndEnabled) {
            // Calcula a distância ao jogador.
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            // Se estiver dentro da distância de perseguição...
            if (distanceToPlayer <= chaseDistance) {
                // Define o destino do NavMeshAgent como a posição atual do jogador.
                if (navMeshAgent.destination != player.position) {
                    navMeshAgent.SetDestination(player.position);
                }
            } else {
                // Para o movimento caso o jogador esteja fora do alcance.
                navMeshAgent.ResetPath();
            }
        }
    }
}

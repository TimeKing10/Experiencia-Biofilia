using UnityEngine;

public class IniciarAimyWaypoints : MonoBehaviour
{
    [Header("Movimiento")]
    public Transform[] waypoints;
    public float speed = 3f;
    public float reachThreshold = 0.2f;

    [Header("Animación")]
    [Tooltip("Nombre exacto del estado de animación que activa el movimiento")]
    public string walkingStateName = "Caminar";

    private int currentWaypointIndex = 0;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (IsInWalkingState())
        {
            MoveAlongWaypoints();
        }
    }

    bool IsInWalkingState()
    {
        if (animator == null || string.IsNullOrEmpty(walkingStateName)) return false;

        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        return stateInfo.IsName(walkingStateName);
    }

    void MoveAlongWaypoints()
    {
        if (waypoints.Length == 0 || currentWaypointIndex >= waypoints.Length) return;

        Transform targetWaypoint = waypoints[currentWaypointIndex];
        Vector3 direction = (targetWaypoint.position - transform.position).normalized;

        transform.position += direction * speed * Time.deltaTime;

        float distance = Vector3.Distance(transform.position, targetWaypoint.position);
        if (distance < reachThreshold)
        {
            currentWaypointIndex++;

            // Al llegar al final, detener movimiento
            if (currentWaypointIndex >= waypoints.Length)
            {
                // (Opcional) detener la animación o cambiar de estado
                // animator.SetTrigger("Detener"); // o cambiar a un booleano
            }
        }
    }
}


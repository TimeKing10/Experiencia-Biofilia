using UnityEngine;

public class WaypointFollower : MonoBehaviour
{
    public Transform[] waypoints;      // Lista de puntos a seguir
    public float speed = 3f;           // Velocidad del personaje
    public float reachThreshold = 0.2f; // Distancia mínima para cambiar de punto

    private int currentWaypointIndex = 0;

    void Update()
    {
        if (waypoints.Length == 0) return;

        Transform targetWaypoint = waypoints[currentWaypointIndex];
        Vector3 direction = (targetWaypoint.position - transform.position).normalized;

        // Movimiento hacia el waypoint
        transform.position += direction * speed * Time.deltaTime;

        // Rotar hacia el waypoint (opcional)
        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f);
        }

        // Si llegó al waypoint, pasa al siguiente
        float distance = Vector3.Distance(transform.position, targetWaypoint.position);
        if (distance < reachThreshold)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        }
    }
}

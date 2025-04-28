using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

[RequireComponent(typeof(NavMeshAgent))]
public class NavMeshRobot : MonoBehaviour
{
    public UnityEvent OnDestroyWallCube;

    [SerializeField]
    AudioClip collisionClip;

    public AudioClip GetCollisionClip() => collisionClip;

    NavMeshAgent agent;

    const string WALL_CUBE_STRING = "WallCube";
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    public void MoveAgent(Vector3 move)
    {
        agent.destination = agent.transform.position + move;
        //agent.SetDestination(agent.transform.position + move);
    }

    public void StopAgent()
    {
        //Stops agent from looking for a path
        agent.ResetPath();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag(WALL_CUBE_STRING))
        {
            Destroy(other.gameObject);
            OnDestroyWallCube?.Invoke();
        }
    }
}

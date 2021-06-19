using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Dood : MonoBehaviour
{
    public float moveSpeed = 500f;
    public bool move;
    Plane plane;
    GameController gc;
    NavMeshAgent agent;
    Transform target;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        move = false;
        plane = FindObjectOfType<Plane>();
        gc = FindObjectOfType<GameController>();
    }

    

    private void Update()
    {
        if (move)
        {
            //transform.position =  Vector3.MoveTowards(transform.position, 
            //    new Vector3(plane.transform.position.x -1 ,transform.position.y,plane.transform.position.z), 
            //    Time.deltaTime * moveSpeed);
            agent.SetDestination(new Vector3(target.position.x - 1, transform.position.y,target.position.z));

        }
      
    }

    public void ResetDestination()
    {
        agent.SetDestination( new Vector3(transform.position.x,transform.position.y, 20f));
    }


    public void PlaneGo()
    {
        move = true;
        target = plane.transform;
    }

    public void GoOutofPlane(Transform currentTarget)
    {
        move = true;
        target = currentTarget;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Plane>())
        {
            gc.AddPassenger();
            Destroy(gameObject);
        }
    }

    
}

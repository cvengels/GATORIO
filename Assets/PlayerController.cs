using System;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    public Camera cam;

    public NavMeshAgent agent;

    [SerializeField] private bool hasObjectInHand;
    [SerializeField] private GameObject objectInHand;

    private void Awake()
    {
        hasObjectInHand = false;
        objectInHand.SetActive(false);
    }
    
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            
            if (Physics.Raycast(ray, out hit))
            {
                agent.SetDestination(hit.point);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Resource"))
        {
            if (!hasObjectInHand)
            {
                hasObjectInHand = true;
                objectInHand.SetActive(true);
                objectInHand.GetComponent<MeshRenderer>().material =
                    collision.collider.GetComponent<MeshRenderer>().material;
            }
        }
        if (collision.collider.CompareTag("Resource Sink") && hasObjectInHand)
        {
            hasObjectInHand = false;
            objectInHand.SetActive(false);
        }
    }
}

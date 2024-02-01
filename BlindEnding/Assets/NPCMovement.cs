using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public class NPCMovement : MonoBehaviour
{

    
    [Header("Intentional Movement")]
    public float speed = 15f;
    public float acceleration = 4f;

    [Header("Avoiding")]
    public float avoidStrength;
    public ChunkManager transfromListManager;

    [HideInInspector]
    public Vector3 desiredPosition;

    private Transform[] avoidTransforms;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        GetAvoidTransforms();

        MoveTowardsDesiredPosition();
        MoveAwayFromOthers();
    }

    void MoveTowardsDesiredPosition(){
        Vector2 desiredVelocity = (desiredPosition - transform.position).normalized * speed;
        rb.velocity = Vector2.Lerp(rb.velocity, desiredVelocity, acceleration * Time.deltaTime);
    }

    void MoveAwayFromOthers () {
        foreach(Transform avoidTransform in avoidTransforms){
            Vector2 avoidDirection = (transform.position - avoidTransform.position).normalized;
            float avoidPower = 1/Mathf.Pow(Vector2.Distance(transform.position, avoidTransform.position),2) * avoidStrength;

            rb.position += avoidDirection * avoidPower * Time.deltaTime;
        }
    }

    void GetAvoidTransforms(){
        List<Transform> avoidTransformList = transfromListManager.GetChunk(transform.position);
        avoidTransformList.Remove(transform);

        avoidTransforms = avoidTransformList.ToArray();
    }
}

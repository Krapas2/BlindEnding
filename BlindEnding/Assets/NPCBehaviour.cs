using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(NPCMovement))]
public class NPCBehaviour : MonoBehaviour
{
    public Transform target;
    public enum State
    {
        Chasing,
    }

    [HideInInspector]
    public State state;

    private NPCMovement movement;
    
    void Start()
    {
        movement = GetComponent<NPCMovement>();

        state = State.Chasing;
    }

    void Update()
    {
        switch(state) 
        {
        case State.Chasing:
            Chase();
            break;
        }
    }

    void Chase(){
        movement.desiredPosition = target.position;
    }
}

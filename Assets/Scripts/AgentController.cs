using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentController : MonoBehaviour
{
    IInput input ; 
    AgentMovement movement;
    // Start is called before the first frame update
    // void Start()
    // {

    // }
    private void OnEnable() {
        input = GetComponent<IInput>();
        movement = GetComponent<AgentMovement>();
        input.OnMovementDirection+= movement.HandleMovementDirection;
        input.OnMovementInput +=movement.HandleMovement;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnDisable() {
        input.OnMovementDirection-= movement.HandleMovementDirection;
        input.OnMovementInput -=movement.HandleMovement;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player_Input : MonoBehaviour,IInput
{
    public Action<Vector2> OnMovementInput{get;set;}
    public Action<Vector3> OnMovementDirection{get;set;}

    // Start is called before the first frame update
    void Start()
    {
        //Lock cursor in the center of the screen
        Cursor.lockState = CursorLockMode.Locked;
        
    }

    // Update is called once per frame
    void Update()
    {
        getMovementInput();
        getMovementDirection();
    }
    void getMovementInput(){
        Vector2 input = new Vector2(Input.GetAxis("Horizontal"),Input.GetAxis("Vertical"));
        OnMovementInput?.Invoke(input);
        
    }
    void getMovementDirection(){
        var cameraDirection = Camera.main.transform.forward;
        Debug.DrawRay(Camera.main.transform.position,cameraDirection*10,Color.red);
        var directionToMoveIn = Vector3.Scale(cameraDirection,(Vector3.right+Vector3.forward));
        Debug.DrawRay(Camera.main.transform.position,directionToMoveIn*10,Color.blue);
        OnMovementDirection?.Invoke(directionToMoveIn);
    }
}

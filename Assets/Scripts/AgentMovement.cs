using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentMovement : MonoBehaviour
{
    CharacterController controller;
    public float rotationSpeed=20;
    public float movementSpeed=20;
    public float gravity = 20;
    Vector3 movementVector = Vector3.zero;
    private float desiredRotationAngle = 0;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        if(controller.isGrounded){
            if(movementVector.magnitude>0){
                var animationSpeedMultiplier = setCorrectAnimation();
                RotateAgent();
                movementVector*=animationSpeedMultiplier;
            }
        }
        movementVector.y-=gravity;
        controller.Move(movementVector*Time.deltaTime);
    }
    public void HandleMovement(Vector2 input){
        if(controller.isGrounded){
            if(input.y>0){
                movementVector = transform.forward * movementSpeed;
            }else{
                movementVector = Vector3.zero;
                animator.SetFloat("Move",0);
            }
        }
    }
    public void HandleMovementDirection(Vector3 direction){
        desiredRotationAngle = Vector3.Angle(transform.forward,direction);
        var crossProduct = Vector3.Cross(transform.forward,direction).y;
        if(crossProduct<0){
            desiredRotationAngle *=-1;
        }
    }
    private void RotateAgent(){
        if(desiredRotationAngle>10 || desiredRotationAngle<-10){
            transform.Rotate(Vector3.up*desiredRotationAngle*rotationSpeed*Time.deltaTime);
        }
    }
    private float setCorrectAnimation(){
        float currentAnimationSpeed = animator.GetFloat("Move");
        if(desiredRotationAngle>10 || desiredRotationAngle<-10){
            if(currentAnimationSpeed<0.2f){
                currentAnimationSpeed+=Time.deltaTime*2;
                currentAnimationSpeed = Mathf.Clamp(currentAnimationSpeed,0,0.2f);
            }
            animator.SetFloat("Move",currentAnimationSpeed);
        }
        else{
            if(currentAnimationSpeed<1){
                currentAnimationSpeed+=Time.deltaTime*2;
            }else{
                currentAnimationSpeed=1;
            }
            animator.SetFloat("Move",currentAnimationSpeed);
        }
        return currentAnimationSpeed;
    }
}

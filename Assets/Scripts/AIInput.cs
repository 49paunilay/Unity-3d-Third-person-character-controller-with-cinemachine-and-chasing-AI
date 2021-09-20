using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class AIInput : MonoBehaviour , IInput
{
    public Action<Vector2> OnMovementInput{get;set;}
    public Action<Vector3> OnMovementDirection{get;set;}
    bool playerDetectionResult = false;

    public Transform eyesTransform;
    public LayerMask playerLayer;
    public float stopingDistance ,visionDistance = 1.2f;
    public Transform playerTransform;

    private void OnDrawGizmos() {
        Gizmos.color=Color.blue;
        if(playerDetectionResult){
            Gizmos.color=Color.red;
        }
        Gizmos.DrawWireSphere(eyesTransform.position,visionDistance);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        playerDetectionResult = DetectPlayer();
        if(playerDetectionResult){
            var directionToPlayer = playerTransform.position - transform.position;
            directionToPlayer = Vector3.Scale(directionToPlayer,Vector3.forward+Vector3.right);
            if(directionToPlayer.magnitude>stopingDistance){
                directionToPlayer.Normalize();
                OnMovementInput?.Invoke(Vector2.up);
                OnMovementDirection?.Invoke(directionToPlayer);
                return;
            }
        }
        OnMovementInput?.Invoke(Vector2.zero);
        OnMovementDirection?.Invoke(transform.forward);
    }
    public bool DetectPlayer(){
        Collider[] hitCollider = Physics.OverlapSphere(eyesTransform.position,visionDistance,playerLayer);
        foreach (var collider in hitCollider)
        {
            playerTransform = collider.transform;
            return true;
        }
        playerTransform = null;
        return false;
    }
}

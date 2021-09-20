using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface IInput {
     Action<Vector2> OnMovementInput{get;set;}
     Action<Vector3> OnMovementDirection{get;set;}
}
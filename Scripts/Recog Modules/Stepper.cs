using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stepper : RecogModule
{
    public enum Axis
    {
        x, y, z
    }

    [SerializeField] private Axis axis = Axis.x; // [0, 1, 2]
    [SerializeField] private float stepSize = 1.0f;

    protected override void ActionOnRecognized()
    {
        switch(axis){
            case Axis.x:
                transform.position += transform.right * stepSize;
                break;
            case Axis.y:
                transform.position += transform.up * stepSize;
                break;
            case Axis.z:
                transform.position += transform.forward * stepSize;
                break;
        }
    }
}

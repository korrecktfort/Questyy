using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : RecogModule
{
    protected override void ActionOnRecognized()
    {
        Vector3 pos = Selector.Instance.GetRightPointAtPosition();

        if(pos == Vector3.zero)
            return;

        transform.LookAt(pos);
    }
}

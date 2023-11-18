using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ReferenceResource : MonoBehaviour
{
    public InputActionAsset inputAsset = null;
    public LayerMask selectionLayerMask = default;
    public float selectionDistance = 100f;

    public LayerMask pointAtLayerMask = default;
}

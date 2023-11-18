using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dubi.SingletonSpace;
using UnityEngine.InputSystem;
using System;
using UnityEngine.XR.Interaction.Toolkit;

public class Selector : Singleton<Selector>
{
    [SerializeField] InputActionAsset inputAsset = null;

    InputAction select = null;
    ReferenceResource referenceResource = null;

    Vector3 pos = Vector3.zero;
    Quaternion rot = Quaternion.identity;

    private void Awake()
    {        
        GameObject resource = Resources.Load<GameObject>("Prefabs/ReferenceResource");
        GameObject obj = Instantiate(resource);

        obj.name = "!ReferenceResource";
        DontDestroyOnLoad(obj);
        obj.transform.SetParent(transform);

        referenceResource = obj.GetComponent<ReferenceResource>();
        inputAsset = referenceResource.inputAsset;  
        select = inputAsset.FindActionMap("XRI RightHand Interaction").FindAction("Select");
        select.started += ctx => OnSelect();

        inputAsset.FindActionMap("XRI Head").FindAction("Position").started += ctx => pos = ctx.ReadValue<Vector2>();
        inputAsset.FindActionMap("XRI Head").FindAction("Rotation").started += ctx => rot = Quaternion.Euler(ctx.ReadValue<Vector2>());
    }

    private void OnEnable()
    {
        select?.Enable();
    }

    private void OnDisable()
    {
        select?.Disable();
    }

    private void OnSelect()
    {        
        float selectionDistance = referenceResource.selectionDistance;
        LayerMask layerMask = referenceResource.selectionLayerMask;

        if(Physics.Raycast(RightHandPointerRay(), out RaycastHit hit, selectionDistance, layerMask))
        {
            if (hit.collider == null)
                return;

            Recog.Instance.SetSelected(hit.collider.gameObject);       
        }
    }

    public Vector3 GetRightPointAtPosition()
    {
        float selectionDistance = Instance.referenceResource.selectionDistance;
        LayerMask layerMask = Instance.referenceResource.selectionLayerMask;

        if (Physics.Raycast(Instance.RightHandPointerRay(), out RaycastHit hit, selectionDistance, layerMask))
            return hit.point;    

        return Vector3.zero;
        
    }

    Ray RightHandPointerRay()
    {
        /// Get right hand device pointing direction
        Vector3 rayDir = Quaternion.LookRotation(rot * Vector3.forward) * Vector3.forward;
        return new Ray(pos, rayDir);
    }
}
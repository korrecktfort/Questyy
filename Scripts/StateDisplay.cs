using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateDisplay : MonoBehaviour, IOnSelected, IDeselectable
{
    Renderer rendererRef = null;

    [SerializeField] Material defaultMat = null;
    [SerializeField] Material selectedMat = null;

    private void Awake()
    {
        rendererRef = GetComponentInChildren<Renderer>();

        if (rendererRef == null)
            throw new System.Exception("No renderer found on " + gameObject.name);
    }

    public void Deselect()
    {
        rendererRef.material = defaultMat;
    }

    public void OnSelected()
    {
        rendererRef.material = selectedMat;
    }
}

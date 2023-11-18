using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RecogModule : MonoBehaviour

{
    protected Action action = default;

    [SerializeField] string keyword = default;
    public Action Action => ActionOnRecognized;
    public string Keyword => keyword;

    public void Select()
    {
        Recog.Instance.SetSelected(this.gameObject);
    }

    protected abstract void ActionOnRecognized();
}
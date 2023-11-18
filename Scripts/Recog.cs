using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;
using System;
using System.Linq;
using Dubi.SingletonSpace;

/// <summary>
/// Using UnityEngine.Windows.Speech to recognize speech
/// </summary>
public class Recog : Singleton<Recog>
{
    KeywordRecognizer keywordRecognizer = null;
    Dictionary<string, Action> currentDictionary = new Dictionary<string, Action>();

    GameObject currentSelected = null;
    
    public GameObject CurrentSelected => currentSelected; 
    
    private void Awake()
    {
        Selector.Instance.LoadSingletonObject();
    }

    void InitializeRecognizer()
    {
        keywordRecognizer = new KeywordRecognizer(currentDictionary.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += RecognizedSpeech;
        keywordRecognizer.Start();
    }

    private void RecognizedSpeech(PhraseRecognizedEventArgs args)
    {
        Action action;

        if(currentDictionary.TryGetValue(args.text, out action))
        {
            action.Invoke();
            Debug.Log("Recognized: " + args.text);
            return;
        }
    }

    public void SetSelected(GameObject obj)
    {
        ForceClearActive();

        if (obj == null)
            return;

        currentSelected = obj;

        foreach (IOnSelected selectable in obj.GetComponentsInChildren<IOnSelected>())
            selectable.OnSelected();

        RecogModule[] recogModules = obj.GetComponentsInChildren<RecogModule>();

        /// nothing new found, keep the current state
        if (recogModules == null || recogModules.Length == 0)
            return;

        foreach(RecogModule module in recogModules)
            currentDictionary.Add(module.Keyword, module.Action);

        InitializeRecognizer();
    }
    public void ForceClearActive()
    {
        currentDictionary.Clear();

        if (currentSelected == null)
            return;

        if (keywordRecognizer == null)
            return;

        keywordRecognizer.Stop();
        keywordRecognizer.Dispose();

        IDeselectable[] deselectables = currentSelected.GetComponentsInChildren<IDeselectable>();

        if (deselectables == null || deselectables.Length <= 0)
            return;

        foreach (IDeselectable deselectable in deselectables)
            deselectable.Deselect();
    }

    public void ClearActive(GameObject other)
    {
        if (other == currentSelected)
            ForceClearActive();
    }

}   
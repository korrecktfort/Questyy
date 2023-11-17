using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;
using System;
using System.Linq;

/// <summary>
/// Using UnityEngine.Windows.Speech to recognize speech
/// </summary>
public class Recog : MonoBehaviour
{
    KeywordRecognizer keywordRecognizer = null;
    Dictionary<string, Action> dictionary = new Dictionary<string, Action>();

    [SerializeField] private KeywordAction[] keywordActions = default;
    
    

    private void Start()
    {
        foreach(KeywordAction keywordAction in keywordActions)
            dictionary.Add(keywordAction.keyword, keywordAction.distribution.Action.Invoke);
        

        keywordRecognizer = new KeywordRecognizer(dictionary.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += RecognizedSpeech;

        keywordRecognizer.Start();
    }

    private void RecognizedSpeech(PhraseRecognizedEventArgs args)
    {
        Action action;
        if(dictionary.TryGetValue(args.text, out action))
        {
            action.Invoke();
        }
    }
}   

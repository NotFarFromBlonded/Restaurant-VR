using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;
using System.Linq;
using System;


public class voice_actions : MonoBehaviour
{   public bool ispressed;
    private KeywordRecognizer keywordRecognizer;
    private Dictionary<string, Action> actions =new Dictionary<string, Action> ();
    public RBGAmbience rbg;
    // Start is called before the first frame update
    void Start()
    {       ispressed=false;
                actions.Add("Two",Two);
        actions.Add("One", One);
        actions.Add("Three",Three);
        keywordRecognizer =new KeywordRecognizer(actions.Keys.ToArray());
        
        
        
    }
    private void RecognizedSpeech(PhraseRecognizedEventArgs speech)
    {
        Debug.Log(speech.text);
        actions[speech.text].Invoke();
    }
    public void One(){
        Debug.Log("ek");
    }
    public void Two(){
        Debug.Log("doo");
    }
    public void Three(){
        Debug.Log("theen");
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.M)){
            if (ispressed)
            {
                ispressed = false;
                rbg.rBGMA.setParameterByName("rAmb", 0);
            } else
            {
                ispressed = true;
                rbg.rBGMA.setParameterByName("rAmb", 1);
            }            
        }
        if (ispressed)
        {
            keywordRecognizer.Start();
            keywordRecognizer.OnPhraseRecognized += RecognizedSpeech;
        }
    }
}

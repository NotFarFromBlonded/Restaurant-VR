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
    public GameObject rm;
    GameObject currentGameObject;
    GameObject InstantiatedGameObject;
    public List<GameObject> food;
    public GameObject foodPos;
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
        //Debug.Log(speech.text);
        actions[speech.text].Invoke();
    }
    public void One(){
        InstantiatedGameObject = FoodInstantiation(food[0]);
        Debug.Log("ek");
        
    }
    public void Two(){
        InstantiatedGameObject = FoodInstantiation(food[1]);
        Debug.Log("doo");
        
    }
    public void Three(){
        InstantiatedGameObject = FoodInstantiation(food[2]);
        Debug.Log("theen");
        
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.M)){
            if (currentGameObject != null)
            {
                if (ispressed)
                {

                    ispressed = false;
                    InstantiatedGameObject.SetActive(true);
                    rbg.rBGMA.setParameterByName("rAmb", 0);
                    keywordRecognizer.Stop();

                }
                else
                {
                    ispressed = true;
                    InstantiatedGameObject.SetActive(false);
                    rbg.rBGMA.setParameterByName("rAmb", 1);
                    keywordRecognizer.Start();
                    keywordRecognizer.OnPhraseRecognized += RecognizedSpeech;
                }
            }
            else
            {
                if (ispressed)
                {

                    ispressed = false;
                    //currentGameObject.SetActive(true);
                    rbg.rBGMA.setParameterByName("rAmb", 0);
                    keywordRecognizer.Stop();

                }
                else
                {
                    ispressed = true;
                    //currentGameObject.SetActive(false);
                    rbg.rBGMA.setParameterByName("rAmb", 1);
                    keywordRecognizer.Start();
                    keywordRecognizer.OnPhraseRecognized += RecognizedSpeech;
                }
            }
                       
        }
        
    }

    GameObject FoodInstantiation(GameObject gameObject)
    {
        DestroyImmediate(InstantiatedGameObject, true);
        currentGameObject = gameObject;
        rbg.rBGMA.setParameterByName("rAmb", 1);
        rm.SetActive(false);
        return Instantiate(currentGameObject, foodPos.transform.position, Quaternion.identity);
        
    }
}

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
    public GameObject InstantiatedGameObject;
    public List<GameObject> food;
    public GameObject foodPos;
    movement mv;
    // Start is called before the first frame update
    void Start()
    {       ispressed=false;
                actions.Add("Two", Two);
        actions.Add("One", One);
        actions.Add("Three", Three);
        keywordRecognizer =new KeywordRecognizer(actions.Keys.ToArray());
        mv = GetComponent<movement>();
        
        
    }
    private void RecognizedSpeech(PhraseRecognizedEventArgs speech)
    {
        //Debug.Log(speech.text);
        actions[speech.text].Invoke();
    }
    public void One(){
        
        keywordRecognizer.Stop();
        ispressed = false;
        rbg.rBGMA.setParameterByName("rAmb", 0);
        StartCoroutine(fdi(food[0]));
        Debug.Log("ek");
        
    }
    public void Two(){
        keywordRecognizer.Stop();
        ispressed = false;
        rbg.rBGMA.setParameterByName("rAmb", 0);
        StartCoroutine(fdi(food[1])); 
        Debug.Log("doo");
    }
    public void Three(){
        keywordRecognizer.Stop();
        ispressed = false;
        rbg.rBGMA.setParameterByName("rAmb", 0);
        StartCoroutine(fdi(food[2]));
        Debug.Log("theen");
        
    }
    public void Four()
    {
        keywordRecognizer.Stop();
        ispressed = false;
        rbg.rBGMA.setParameterByName("rAmb", 0);
        StartCoroutine(fdi(food[3]));
        Debug.Log("chaar");

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
                    rm.SetActive(false);

                    rbg.rBGMA.setParameterByName("rAmb", 0);
                    if (keywordRecognizer.IsRunning)
                    {
                        keywordRecognizer.Stop();
                    }

                }
                else
                {
                    ispressed = true;
                    InstantiatedGameObject.SetActive(false);
                    rm.SetActive(true);
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
                    if (keywordRecognizer.IsRunning)
                    {
                        keywordRecognizer.Stop();
                    }
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
        return Instantiate(currentGameObject, foodPos.transform.position, Quaternion.identity);
    }

    IEnumerator fdi(GameObject gameObject)
    {
        /*yield return new WaitForSeconds(1);
        mv.pd.Play();*/
        StartCoroutine(mv.playFadeCutScene(0f));
        yield return new WaitForSeconds(2.5f);
        InstantiatedGameObject = FoodInstantiation(gameObject);
        rm.SetActive(false);
    }
}

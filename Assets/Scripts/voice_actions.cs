using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.Windows.Speech;
using System.Linq;
using System;


public class voice_actions : MonoBehaviour
{   public bool ispressed;
    //private KeywordRecognizer keywordRecognizer;
    private Dictionary<string, Action> actions =new Dictionary<string, Action> ();
    public RBGAmbience rbg;
    public GameObject rm;
    public GameObject currentGameObject;
    public GameObject InstantiatedGameObject;
    public List<GameObject> food;
    public GameObject foodPos;
    movement mv;
    public float tm;
    public bool timerIsRunning;
    // Start is called before the first frame update
    void Start()
    {       
        ispressed=false;
        //actions.Add("Two", Two);
        //actions.Add("One", One);
        //actions.Add("Three", Three);
        //keywordRecognizer =new KeywordRecognizer(actions.Keys.ToArray());
        mv = GetComponent<movement>();
        
        
    }
    /*private void RecognizedSpeech(PhraseRecognizedEventArgs speech)
    {
        //Debug.Log(speech.text);
        actions[speech.text].Invoke();
    }*/
    public void One(){
        
        //keywordRecognizer.Stop();
        ispressed = false;
        rbg.rBGMA.setParameterByName("rAmb", 0);
        StartCoroutine(fdi(food[0]));
        Debug.Log("ek");
        
    }
    public void Two(){
        //keywordRecognizer.Stop();
        ispressed = false;
        rbg.rBGMA.setParameterByName("rAmb", 0);
        StartCoroutine(fdi(food[1])); 
        Debug.Log("doo");
    }
    public void Three(){
        //keywordRecognizer.Stop();
        ispressed = false;
        rbg.rBGMA.setParameterByName("rAmb", 0);
        StartCoroutine(fdi(food[2]));
        Debug.Log("theen");
        
    }
    public void Four()
    {
        //keywordRecognizer.Stop();
        ispressed = false;
        rbg.rBGMA.setParameterByName("rAmb", 0);
        StartCoroutine(fdi(food[3]));
        Debug.Log("chaar");

    }
    // Update is called once per frame
    void Update()
    {
        if (timerIsRunning)
        {
            if (tm > 0f)
            {
                tm -= Time.deltaTime;
            }
            else
            {
                tm = 0f;
                timerIsRunning = false;
            }
        }
        


        if (mv.onseat && ispressed)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                
                tm = 4.5f;
                timerIsRunning = true;
                One();
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                
                tm = 4.5f;
                timerIsRunning = true;
                Two();
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                
                tm = 4.5f;
                timerIsRunning = true;
                Three();
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                
                tm = 4.5f;
                timerIsRunning = true;
                Four();
            }
        }

            if(timerIsRunning==true){
                rm.SetActive(false);
                InstantiatedGameObject.SetActive(true);
            }

            if (Input.GetKeyDown(KeyCode.M) && timerIsRunning == false && mv.onseat)
            {
                if (currentGameObject != null)
                {
                    if (ispressed)
                    {
                        ispressed = false;

                        InstantiatedGameObject.SetActive(true);
                        rm.SetActive(false);

                        rbg.rBGMA.setParameterByName("rAmb", 0);
                        /*if (keywordRecognizer.IsRunning)
                        {
                            keywordRecognizer.Stop();
                        }*/

                    }
                    else
                    {
                        ispressed = true;
                        InstantiatedGameObject.SetActive(false);
                        rm.SetActive(true);
                        rbg.rBGMA.setParameterByName("rAmb", 1);
                        
                       
                        //keywordRecognizer.Start();
                        //keywordRecognizer.OnPhraseRecognized += RecognizedSpeech;
                        
                    }
                }
                else
                {
                    if (ispressed)
                    {

                        ispressed = false;
                        //currentGameObject.SetActive(true);
                        rbg.rBGMA.setParameterByName("rAmb", 0);
                        /*if (keywordRecognizer.IsRunning)
                        {
                            keywordRecognizer.Stop();
                        }*/
                    }
                    else
                    {
                        ispressed = true;
                        //currentGameObject.SetActive(false);
                        rbg.rBGMA.setParameterByName("rAmb", 1);
                        //keywordRecognizer.Start();
                        //keywordRecognizer.OnPhraseRecognized += RecognizedSpeech;
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

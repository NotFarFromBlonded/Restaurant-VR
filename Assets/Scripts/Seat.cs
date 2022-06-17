using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seat : MonoBehaviour
{
    public GameObject waiter;
    public GameObject waiter1;
    public movement mv;
    public voice_actions va;
    public Vector3 waiterPos;
    //public GameObject waiter;
    // Start is called before the first frame update
    void Start()
    {
        waiter = GameObject.Find("Waiter");
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            mv.onseat = true;
            
            waiter.SetActive(false);
        }
        
    }

    private void OnTriggerExit()
    {
        mv.onseat = false;
        if (va.InstantiatedGameObject != null)
        {
            Destroy(va.InstantiatedGameObject);
            va.rm.SetActive(false);
            
        }
        waiter1.SetActive(true);
        //StartCoroutine(menuActivate());
    }

    IEnumerator menuActivate()
    {
        yield return new WaitForSeconds(2.5f);
        mv.wdg.SetActive(true);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seat : MonoBehaviour
{
    public GameObject waiter;
    public GameObject waiter1;
    public movement mv;
    public voice_actions va;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            waiter.SetActive(false);
            mv.onseat = true;
        }
        
    }

    private void OnTriggerExit()
    {
        waiter1.SetActive(true);
        mv.onseat = false;
        if (va.InstantiatedGameObject != null)
        {
            Destroy(va.InstantiatedGameObject);
            va.rm.SetActive(false);
        }
    }

}

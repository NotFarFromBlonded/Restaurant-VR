using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RBGAmbience : MonoBehaviour
{
    //public FMODUnity.StudioEventEmitter cBGMA;
    //public FMODUnity.StudioEventEmitter rBGMA;

    FMOD.Studio.EventInstance rBGMA;
    FMOD.Studio.EventInstance cBGMA;
    // Start is called before the first frame update
    void Awake()
    {
        cBGMA = FMODUnity.RuntimeManager.CreateInstance("event:/City Ambience");
        rBGMA = FMODUnity.RuntimeManager.CreateInstance("event:/Restaurant Ambience");
        cBGMA.start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            rBGMA.start();
            cBGMA.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        }
    }

    private void OnTriggerExit()
    {
        cBGMA.start();
        rBGMA.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }
}

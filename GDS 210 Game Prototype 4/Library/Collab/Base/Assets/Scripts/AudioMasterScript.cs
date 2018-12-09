using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioMasterScript : MonoBehaviour
{
    /*****************************************************************************************************
     *                      -= !!! ATTENTION CODERS OF PROTOTYPE 4 !!!=-                                 *     
     *                                                                                                   *
     *       with this script loaded, all you need to do to play a sound is to use the code below        *
     *   you can call this from any script on any object without requiring to reference anything else    *
     *                                                                                                   *
     *                     AudioMasterScript.Playsound("name of sound here");                            *
     *                                                                                                   *
     *****************************************************************************************************/

    public static FMOD.Studio.EventInstance sfxHit, sfxHit1, sfxHit2;

    // Keep music rolling between scenes
    static AudioMasterScript instance = null; // keep music rolling

    void Awake()
    {
        if (instance != null) //keep music rolling
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            GameObject.DontDestroyOnLoad(gameObject);
        }
    }

    // Use this for initialization
    void Start()
    {
        // load all possible sounds for the game
        sfxHit = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/Hit");
        sfxHit1 = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/Hit2");
        sfxHit2 = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/Hit3");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Below is the switch statement for all the possible sounds used in the game
    public static void Playsound(string clip) 
    {
        switch(clip)
        {
            case("sfxHit"):
                sfxHit.start();
                break;
        }

        switch(clip)
        {
            case("sfxHit1"):
                sfxHit1.start();
                break;
        }

        switch(clip)
        {
            case("sfxHit2"):
                sfxHit2.start();
                break;
        }
    }
}

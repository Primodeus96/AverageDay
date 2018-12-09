using UnityEngine;

public class AudioMasterScript : MonoBehaviour
{
    /****************************************************************************************************
     *                          -= !!! ATTENTION CODERS OF PROTOTYPE 4 !!!=-                            *     
     *                                                                                                  *
     *       with this script loaded, all you need to do to play a sound is to use the code below...    *
     *   you can call this from any script on any object without requiring to reference anything else   *
     *                                                                                                  *
     *                      AudioMasterScript.Playsound("NAME OF SOUND HERE");                          *
     *                                                                                                  *
     *              To change the intensity of the music use the following code below...                *
     *   you can call this from any script on any object without requiring to reference anything else   *
     *   the range is from [Least intense --> 0 - 6 <-- Most intense]. A value of 10 will fade to       *
     *   Death / Game Over music. Changing back to a value between 0 and 6 will fade back to game music *
     *   A float is required but please keep the values to whole numbers.                               *
     *                                                                                                  *
     *                      AudioMasterScript.intensity.setValue(VALUE OF FLOAT HERE);                  *
     *                                                                                                  *
     *   To change the volume of the heartbeat sound use the following code below...                    *
     *   you can call this from any script on any object without requiring to reference anything else   *
     *   Range is from [Silent --> 0 - 10 <-- Loudest]. After a volume of 9 the music track will get a  *
     *   cool lowpass filter effect applied.                                                            *
     *                                                                                                  *
     *                      AudioMasterScript.heartbeatVolume.setValue(VALUE OF FLOAT HERE);            *
     *                                                                                                  *
     *   To change the volume of the SawPad or Music Master Volume use the following code below...      *
     *   you can call this from any script on any object without requiring to reference anything else   *
     *   Range is from [Silent --> 0 - 10 <-- Loudest].                                                 *
     *                                                                                                  *
     *                      AudioMasterScript.sawPadVolume.setValue(VALUE OF FLOAT HERE);               *
     *                      AudioMasterScript.musicVol.setValue(VALUE OF FLOAT HERE);                   *
     *                                                                                                  *
     *                                                                                                  *
     *                                                                                                  *
     *   Keep a copy of the AudioMaster gameobject with this AudioMasterScript attached in every        *
     *   scene to get continuous music through out the game.        Enjoy, Roo :)                       *
     ****************************************************************************************************/

    // List all possible game sounds
    public static FMOD.Studio.EventInstance gameMusic; // main mussic session
    public static FMOD.Studio.EventInstance buttonPress, buttonSelect, itemPickup, itemUse, runLoop, walkLoop, fail, doorCreak, doorLatch, doorSlam, glassBig, glassSmall, hitSmall, swipe, pop; // SFX soundbank
    public static FMOD.Studio.EventInstance femaleAngry, femaleDisappointed, femaleExcited, femaleGeneric, femaleHappy, femaleNo, femaleQuestion, femaleYes, maleAngry, maleDisappointed, maleExcited, maleGeneric, maleHappy, maleNo, maleQuestion, maleYes; //dialogue soundbank
    public static FMOD.Studio.EventInstance bgBasement, bgOffice, bgOutdoor; // background soundbank

    // name the parameters for music variables
    public static FMOD.Studio.ParameterInstance intensity; // name the parameter for music intensity
    public static FMOD.Studio.ParameterInstance sawPadVolume; //name the parameter for the SawPad Volume ( 0 - 10 )
    public static FMOD.Studio.ParameterInstance heartbeatVolume; //name the parameter for the heartbeat volume ( 0 - 10 (Above 9 will cause music to have lowpass filter applied))
    public static FMOD.Studio.ParameterInstance musicVol; // master volume of music

    //name parameters for sound volumes
    public static FMOD.Studio.ParameterInstance walkVol; // walk volume
    public static FMOD.Studio.ParameterInstance runVol; // run voloume

    [Range(0.0f,10.0f)]
    [SerializeField]
    public float musicVolume; // For tweaking the starting music volume

    [Range(0.0f, 10.0f)]
    [SerializeField]
    public float maxWalkVolume; // For tweaking the walking volume in the inspector

    [Range(0.0f, 10.0f)]
    [SerializeField]
    public float maxRunVolume; // For tweaking the running volume in the inspector


    // will pass the above values into these static variable at start up
    private static float walkVolume; 
    private static float runVolume;

    // Keep music rolling between scenes. Will not destroy onload unless there is a double.
    static AudioMasterScript instance = null;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            GameObject.DontDestroyOnLoad(gameObject);
        }
    }

    public GameObject audioC; // used for audio canvas (desirable)
    private bool isCanvasShowing = true; // used for audio canvas (desirable)

    // Use this for initialization
    void Start()
    {

        // below we load all sounds for the game
        gameMusic = FMODUnity.RuntimeManager.CreateInstance("event:/Music");  // connect to music session for the whole game

        // SFX Soundbank
        buttonPress = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/buttonpress");
        buttonSelect = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/buttonSelect");
        itemPickup = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/itemPickup");
        itemUse = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/itemUse");
        runLoop = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/runLoop");
        walkLoop = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/walkLoop");
        fail = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/fail");
        doorCreak = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/doorCreak");
        doorLatch = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/doorLatch");
        doorSlam = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/doorSlam");
        glassBig = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/glassBig");
        glassSmall = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/glassSmall");
        hitSmall = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/hitSmall");
        swipe = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/swipe");
        pop = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/pop");

        // Dialogue Soundbank
        femaleAngry = FMODUnity.RuntimeManager.CreateInstance("event:/Dialogue/femaleAngry");
        femaleDisappointed = FMODUnity.RuntimeManager.CreateInstance("event:/Dialogue/femaleDisappointed");
        femaleExcited = FMODUnity.RuntimeManager.CreateInstance("event:/Dialogue/femaleExcited");
        femaleGeneric = FMODUnity.RuntimeManager.CreateInstance("event:/Dialogue/femaleGeneric");
        femaleHappy = FMODUnity.RuntimeManager.CreateInstance("event:/Dialogue/femaleHappy");
        femaleNo = FMODUnity.RuntimeManager.CreateInstance("event:/Dialogue/femaleNo");
        femaleQuestion = FMODUnity.RuntimeManager.CreateInstance("event:/Dialogue/femaleQuestion");
        femaleYes = FMODUnity.RuntimeManager.CreateInstance("event:/Dialogue/femaleYes");
        maleAngry = FMODUnity.RuntimeManager.CreateInstance("event:/Dialogue/maleAngry");
        maleDisappointed = FMODUnity.RuntimeManager.CreateInstance("event:/Dialogue/maleDisappointed");
        maleExcited = FMODUnity.RuntimeManager.CreateInstance("event:/Dialogue/maleExcited");
        maleGeneric = FMODUnity.RuntimeManager.CreateInstance("event:/Dialogue/maleGeneric");
        maleHappy = FMODUnity.RuntimeManager.CreateInstance("event:/Dialogue/maleHappy");
        maleNo = FMODUnity.RuntimeManager.CreateInstance("event:/Dialogue/maleNo");
        maleQuestion = FMODUnity.RuntimeManager.CreateInstance("event:/Dialogue/maleQuestion");
        maleYes = FMODUnity.RuntimeManager.CreateInstance("event:/Dialogue/maleYes");

        // Background Soundbank

        bgBasement = FMODUnity.RuntimeManager.CreateInstance("event:/BG/Basement");
        bgOffice = FMODUnity.RuntimeManager.CreateInstance("event:/BG/Office");
        bgOutdoor = FMODUnity.RuntimeManager.CreateInstance("event:/BG/Outdoor");

        // below we link to all the parameters
        gameMusic.getParameter("intensity", out intensity); // connect to intensity music parameter
        gameMusic.getParameter("sawPadVolume", out sawPadVolume); // connect to SawPad Volume parameter
        gameMusic.getParameter("heartbeatVolume", out heartbeatVolume); // connect to heartbeat Volume parameter
        gameMusic.getParameter("masterVolume", out musicVol); // connect to master volume of music track

        walkLoop.getParameter("walkVol", out walkVol); // connect to the walk Volume parameter
        runLoop.getParameter("runVol", out runVol); // connect to the walk Volume parameter

        gameMusic.start(); // start the music
        musicVol.setValue(musicVolume); // set the game music to the value of the slider in the inspector
        walkLoop.start(); //start walking sound (controlled by volume)
        runLoop.start(); //start running sound (controlled by volume)

        walkVolume = maxWalkVolume; // used to regulate maximum walk volume
        runVolume = maxRunVolume; // used to regulate maximum run volume
    }

    private void Update()
    {

        // used for toggling audio canvas (desirable) on and off
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (!isCanvasShowing)
            {
                isCanvasShowing = true;
                audioC.SetActive(true);
            }
            else
            {
                isCanvasShowing = false;
                audioC.SetActive(false);
            }
            
        }

    }


    // Below is the switch statement for all the possible sounds used in the game
    public static void Playsound(string clip) 
    {
        switch (clip) { case ("buttonPress"): buttonPress.start(); break; }
        switch (clip) { case ("buttonSelect"): buttonSelect.start(); break; }
        switch (clip) { case ("itemPickup"): itemPickup.start(); break; }
        switch (clip) { case ("itemUse"): itemUse.start(); break; }
        switch (clip) { case ("runStart"): runVol.setValue(runVolume); break; }
        switch (clip) { case ("runStop"): runVol.setValue(0); break; }
        switch (clip) { case ("walkStart"): walkVol.setValue(walkVolume); break; }
        switch (clip) { case ("walkStop"): walkVol.setValue(0); break; }
        switch (clip) { case ("fail"): fail.start(); break; }
        switch (clip) { case ("doorCreak"): doorCreak.start(); break; }
        switch (clip) { case ("doorLatch"): doorLatch.start(); break; }
        switch (clip) { case ("doorSlam"): doorSlam.start(); break; }
        switch (clip) { case ("glassBig"): glassBig.start(); break; }
        switch (clip) { case ("glassSmall"): glassSmall.start(); break; }
        switch (clip) { case ("hitSmall"): hitSmall.start(); break; }
        switch (clip) { case ("pop"): pop.start(); break; }
        switch (clip) { case ("swipe"): swipe.start(); break; }

        switch (clip) { case ("femaleAngry"): femaleAngry.start(); break; }
        switch (clip) { case ("femaleDisappointed"): femaleDisappointed.start(); break; }
        switch (clip) { case ("femaleExcited"): femaleExcited.start(); break; }
        switch (clip) { case ("femaleGeneric"): femaleGeneric.start(); break; }
        switch (clip) { case ("femaleHappy"): femaleHappy.start(); break; }
        switch (clip) { case ("femaleNo"): femaleNo.start(); break; }
        switch (clip) { case ("femaleQuestion"): femaleQuestion.start(); break; }
        switch (clip) { case ("femaleYes"): femaleYes.start(); break; }
        switch (clip) { case ("maleAngry"): maleAngry.start(); break; }
        switch (clip) { case ("maleDisappointed"): maleDisappointed.start(); break; }
        switch (clip) { case ("maleExcited"): maleExcited.start(); break; }
        switch (clip) { case ("maleGeneric"): maleGeneric.start(); break; }
        switch (clip) { case ("maleHappy"): maleHappy.start(); break; }
        switch (clip) { case ("maleNo"): maleNo.start(); break; }
        switch (clip) { case ("maleQuestion"): maleQuestion.start(); break; }
        switch (clip) { case ("maleYes"): maleYes.start(); break; }

        switch (clip) { case ("bgBasementStart"): bgBasement.start(); break; }
        switch (clip) { case ("bgBasementStop"): bgBasement.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT); break; }
        switch (clip) { case ("bgOfficeStart"): bgOffice.start(); break; }
        switch (clip) { case ("bgOfficeStop"): bgOffice.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT); break; }
        switch (clip) { case ("bgOutdoorStart"): bgOutdoor.start(); break; }
        switch (clip) { case ("bgOutdoorStop"): bgOutdoor.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT); break; }
    }

    /************************************************************************************************************
     *              INGORE BELOW - DUPLICATED NON STATIC SWITCH FUNCTION FOR AUDIO CANVAS                       *
     ************************************************************************************************************/

    // use this switch statement for Audio Demo canvas

    public void playButtonSounds(string clip)
    {
        switch (clip) { case ("buttonPress"): buttonPress.start(); break; }
        switch (clip) { case ("buttonSelect"): buttonSelect.start(); break; }
        switch (clip) { case ("itemPickup"): itemPickup.start(); break; }
        switch (clip) { case ("itemUse"): itemUse.start(); break; }
        switch (clip) { case ("runStart"): runVol.setValue(runVolume); break; }
        switch (clip) { case ("runStop"): runVol.setValue(0); break; }
        switch (clip) { case ("walkStart"): walkVol.setValue(walkVolume); break; }
        switch (clip) { case ("walkStop"): walkVol.setValue(0); break; }
        switch (clip) { case ("fail"): fail.start(); break; }
        switch (clip) { case ("doorCreak"): doorCreak.start(); break; }
        switch (clip) { case ("doorLatch"): doorLatch.start(); break; }
        switch (clip) { case ("doorSlam"): doorSlam.start(); break; }
        switch (clip) { case ("glassBig"): glassBig.start(); break; }
        switch (clip) { case ("glassSmall"): glassSmall.start(); break; }
        switch (clip) { case ("hitSmall"): hitSmall.start(); break; }
        switch (clip) { case ("pop"): pop.start(); break; }
        switch (clip) { case ("swipe"): swipe.start(); break; }

        switch (clip) { case ("femaleAngry"): femaleAngry.start(); break; }
        switch (clip) { case ("femaleDisappointed"): femaleDisappointed.start(); break; }
        switch (clip) { case ("femaleExcited"): femaleExcited.start(); break; }
        switch (clip) { case ("femaleGeneric"): femaleGeneric.start(); break; }
        switch (clip) { case ("femaleHappy"): femaleHappy.start(); break; }
        switch (clip) { case ("femaleNo"): femaleNo.start(); break; }
        switch (clip) { case ("femaleQuestion"): femaleQuestion.start(); break; }
        switch (clip) { case ("femaleYes"): femaleYes.start(); break; }
        switch (clip) { case ("maleAngry"): maleAngry.start(); break; }
        switch (clip) { case ("maleDisappointed"): maleDisappointed.start(); break; }
        switch (clip) { case ("maleExcited"): maleExcited.start(); break; }
        switch (clip) { case ("maleGeneric"): maleGeneric.start(); break; }
        switch (clip) { case ("maleHappy"): maleHappy.start(); break; }
        switch (clip) { case ("maleNo"): maleNo.start(); break; }
        switch (clip) { case ("maleQuestion"): maleQuestion.start(); break; }
        switch (clip) { case ("maleYes"): maleYes.start(); break; }

        switch (clip) { case ("bgBasementStart"): bgBasement.start(); break; }
        switch (clip) { case ("bgBasementStop"): bgBasement.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT); break; }
        switch (clip) { case ("bgOfficeStart"): bgOffice.start(); break; }
        switch (clip) { case ("bgOfficeStop"): bgOffice.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT); break; }
        switch (clip) { case ("bgOutdoorStart"): bgOutdoor.start(); break; }
        switch (clip) { case ("bgOutdoorStop"): bgOutdoor.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT); break; }

        switch (clip) { case ("musicVol.setValue(0f);"): musicVol.setValue(0f); ; break; }
        switch (clip) { case ("musicVol.setValue(5f);"): musicVol.setValue(5f); ; break; }
        switch (clip) { case ("musicVol.setValue(7.5f);"): musicVol.setValue(7.5f); ; break; }
        switch (clip) { case ("musicVol.setValue(10f);"): musicVol.setValue(10f); ; break; }

        switch (clip) { case ("sawPadVolume.setValue(0f)"): sawPadVolume.setValue(0f); break; }
        switch (clip) { case ("sawPadVolume.setValue(7f)"): sawPadVolume.setValue(7f); break; }
        switch (clip) { case ("sawPadVolume.setValue(10f)"): sawPadVolume.setValue(10f); break; }

        switch (clip) { case ("heartbeatVolume.setValue(0f)"): heartbeatVolume.setValue(0f); break; }
        switch (clip) { case ("heartbeatVolume.setValue(9f)"): heartbeatVolume.setValue(9f); break; }
        switch (clip) { case ("heartbeatVolume.setValue(10f)"): heartbeatVolume.setValue(10f); break; }

        switch (clip) { case ("intensity.setValue(0f)"): intensity.setValue(0f); break; }
        switch (clip) { case ("intensity.setValue(1f)"): intensity.setValue(1f); break; }
        switch (clip) { case ("intensity.setValue(2f)"): intensity.setValue(2f); break; }
        switch (clip) { case ("intensity.setValue(3f)"): intensity.setValue(3f); break; }
        switch (clip) { case ("intensity.setValue(4f)"): intensity.setValue(4f); break; }
        switch (clip) { case ("intensity.setValue(5f)"): intensity.setValue(5f); break; }
        switch (clip) { case ("intensity.setValue(6f)"): intensity.setValue(6f); break; }
        switch (clip) { case ("intensity.setValue(10f)"): intensity.setValue(10f); break; }
    }
}

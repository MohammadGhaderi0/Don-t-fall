using System.Collections;
using UnityEngine;

//Each AudioClip in the array will have its own AudioSource and GameObject
//instantiated when the BaseSoundController.cs script first runs. Think of each sound as hav-
//ing its own audio channel to avoid overlaps or too many different sounds playing on a single
//AudioSource. Internally, a class called SoundObject is used to store information about the
//audio sources and their gameObjects. When the main sound controller script needs to access
//each one, the references in SoundObject are used to avoid having to repeatedly look for them.


public class SoundObject
{
    public AudioSource source;
    public GameObject sourceGO;
    public Transform sourceTR;
    public AudioClip clip;
    public string name;

    public SoundObject(AudioClip aClip, string aName, float aVolume)
    {

        // in this (the constructor) we create a new audio source
        // and store the details of the sound itself

        sourceGO = new GameObject("AudioSource_" + aName);
        sourceTR = sourceGO.transform;
        source = sourceGO.AddComponent<AudioSource>();
        source.name = "AudioSource_" + aName;
        source.playOnAwake = false;
        source.clip = aClip;
        source.volume = aVolume;
        clip = aClip;
        name = aName;
    }

    public void PlaySound(Vector3 atPosition)
    {
        sourceTR.position = atPosition;
        source.PlayOneShot(clip);
    }
}

public class BaseSoundController : MonoBehaviour
{
    public static BaseSoundController Instance;
    public AudioClip[] GameSounds;
    private int totalSounds;
    private ArrayList soundObjectList;
    private SoundObject tempSoundObj;
    public float volume;
    public void Awake()
    {
        Instance= this;
    }
    void Start ()
    { 
        volume = PlayerPrefs.HasKey("volume") ? PlayerPrefs.GetFloat("volume") : 0.7f;        //  the volume is grabbed from PlayerPrefs when this script first starts
        soundObjectList=new ArrayList();                                                          // make sound objects for all of the sounds in GameSounds array
		
        foreach(AudioClip theSound in GameSounds)
        {
            
            tempSoundObj= new SoundObject(theSound,theSound.name, volume);
            soundObjectList.Add(tempSoundObj);
            totalSounds++;
        }
    }
    public void PlaySoundByIndex(int anIndexNumber, Vector3 aPosition)
    {
        // make sure we're not trying to play a sound indexed higher than exists in the array
        if(anIndexNumber > soundObjectList.Count)
        {
            Debug.LogWarning("BaseSoundController>Trying to do PlaySoundByIndex with invalid index number. Playing last sound in array, instead.");
            anIndexNumber= soundObjectList.Count-1;
        }
		
        tempSoundObj= (SoundObject)soundObjectList[anIndexNumber];
        tempSoundObj.PlaySound(aPosition);
    }

    public void SetVolume(float givenVolume)
    {
        foreach (SoundObject sfx in soundObjectList)
        {
            sfx.source.volume = givenVolume;
        }
    }
}

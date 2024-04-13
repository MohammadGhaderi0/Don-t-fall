using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        // we will grab the volume from PlayerPrefs when this script first starts
        volume = PlayerPrefs.HasKey("volume") ? PlayerPrefs.GetFloat("volume") : 0.7f;
        
        Debug.Log ("BaseSoundController gets volume from prefs"+"_SFXVol at "+volume);
        
        soundObjectList=new ArrayList();
		
        // make sound objects for all of the sounds in GameSounds array
		
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
        if(anIndexNumber>soundObjectList.Count)
        {
            Debug.LogWarning("BaseSoundController>Trying to do PlaySoundByIndex with invalid index number. Playing last sound in array, instead.");
            anIndexNumber= soundObjectList.Count-1;
        }
		
        tempSoundObj= (SoundObject)soundObjectList[anIndexNumber];
        // tempSoundObj.source.volume = 0;
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

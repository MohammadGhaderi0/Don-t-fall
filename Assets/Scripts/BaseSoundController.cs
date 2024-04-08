using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseSoundController : MonoBehaviour
{
    public static BaseSoundController Instance;
    public AudioClip[] GameSounds;
    private int totalSounds;
    private ArrayList soundObjectList;
    private SoundObject tempSoundObj;
    public float volume= 1;
    public void Awake()
    {
        Instance= this;
    }
    void Start ()
    { 
        // we will grab the volume from PlayerPrefs when this script first starts
        volume= PlayerPrefs.GetFloat("_SFXVol");
		
        Debug.Log ("BaseSoundController gets volume from prefs"+"_SFXVol at "+volume);
        soundObjectList=new ArrayList();
		
        // make sound objects for all of the sounds in GameSounds array
		
        foreach(AudioClip theSound in GameSounds)
        {
            
            tempSoundObj= new SoundObject(theSound,
                theSound.name, volume);
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
        tempSoundObj.PlaySound(aPosition);
    }
}

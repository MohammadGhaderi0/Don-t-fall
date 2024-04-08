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
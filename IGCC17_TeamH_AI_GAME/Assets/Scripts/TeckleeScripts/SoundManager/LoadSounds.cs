using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSounds : MonoBehaviour
{
	// Use this for initialization
	void Start () {
        PreloadSounds();
    }

    void PreloadSounds() {

        //SoundSystem.Instance.PlayClip(AUDIO_TYPE.BACKGROUND_MUSIC, AudioClipManager.GetInstance().GetAudioClip(""));
        AudioClipManager.GetInstance().GenerateAudioClip("filename","filepath");
        //Keep Adding on The same line with the filename and filepath replaced respectively for new sounds
    }
}

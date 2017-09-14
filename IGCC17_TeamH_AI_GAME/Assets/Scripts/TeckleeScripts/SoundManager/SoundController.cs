using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    public void PlayGenericSFX(string audioClipName)
    {
        SoundSystem.Instance.PlayClip(AUDIO_TYPE.SOUND_EFFECTS, AudioClipManager.GetInstance().GetAudioClip(audioClipName), false, "GenericGameSFX");
    }

    public void PlayPlayerSFX(string audioClipName)
    {
        SoundSystem.Instance.PlayClip(AUDIO_TYPE.SOUND_EFFECTS, AudioClipManager.GetInstance().GetAudioClip(audioClipName), false, "Player_SFX");
    }
    
    public void PlayAISFX(string audioClipName)
    {
        SoundSystem.Instance.PlayClip(AUDIO_TYPE.SOUND_EFFECTS, AudioClipManager.GetInstance().GetAudioClip(audioClipName), false, "AI_SFX");
    }
}

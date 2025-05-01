using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseVoiceController : MonoBehaviour
{
    public AudioSource voiceSource;
    public AudioClip scaredClip;
    public AudioClip victoryClip;

    public void PlayScared()
    {
        voiceSource.PlayOneShot(scaredClip);
        BGMController.Instance.DuckVolume(); // duck BGM for clarity
    }

    public void PlayVictory()
    {
        voiceSource.PlayOneShot(victoryClip);
        Invoke(nameof(RestoreBGM), 2.5f);
    }

    void RestoreBGM()
    {
        BGMController.Instance.RestoreVolume();
    }
}

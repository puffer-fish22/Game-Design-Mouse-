using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CatAudioController : MonoBehaviour
{
    [Header("Audio Sources")]
    public AudioSource roamingLoopSource;    // Looping footsteps/growl
    public AudioSource oneShotSource;        // One-shot stingers

    [Header("Audio Clips")]
    public AudioClip chaseStingerClip;
    public AudioClip escapeClip;
    public AudioClip caughtScreamClip;

    [Header("Pitch Settings")]
    public float patrolPitch = 1.0f;
    public float chasePitch = 1.5f;

    [Header("Volume Settings")]
    [Range(0f, 1f)] public float roamVolume = 0.4f;
    [Range(0f, 1f)] public float stingerVolume = 0.8f;
    [Range(0f, 1f)] public float escapeVolume = 0.6f;
    [Range(0f, 1f)] public float caughtVolume = 1.0f;

    private bool isChasing = false;
    private float fadeSpeed = 1.5f;

    void Start()
    {
        if (roamingLoopSource)
        {
            roamingLoopSource.loop = true;
            roamingLoopSource.pitch = patrolPitch;
            roamingLoopSource.volume = roamVolume;
            roamingLoopSource.Play();
        }
    }

    void Update()
    {
        // Smoothly restore BGM volume
        if (!isChasing)
        {
            BGMController.Instance.RestoreVolume();
        }
    }

    public void OnStartPatrol()
    {
        isChasing = false;

        if (roamingLoopSource)
        {
            roamingLoopSource.pitch = patrolPitch;
            roamingLoopSource.volume = roamVolume;

            if (!roamingLoopSource.isPlaying)
                roamingLoopSource.Play();
        }

        BGMController.Instance.RestoreVolume();
    }

    public void OnStartChase()
    {
        if (!isChasing)
        {
            isChasing = true;

            if (chaseStingerClip && oneShotSource)
                oneShotSource.PlayOneShot(chaseStingerClip, stingerVolume);

            if (roamingLoopSource)
            {
                roamingLoopSource.pitch = chasePitch;
                roamingLoopSource.volume = roamVolume;
            }

            BGMController.Instance.SetVolume(0f);
        }
    }

    public void OnEscaped()
    {
        isChasing = false;

        if (escapeClip && oneShotSource)
        {
            oneShotSource.PlayOneShot(escapeClip, escapeVolume);
        }

        // Restore chase audio settings
        if (roamingLoopSource)
        {
            roamingLoopSource.pitch = patrolPitch;
            roamingLoopSource.volume = roamVolume;
        }

    }

    public void OnCaught()
    {
        isChasing = false;

        if (roamingLoopSource && roamingLoopSource.isPlaying)
            roamingLoopSource.Stop();

        if (caughtScreamClip && oneShotSource)
        {
            oneShotSource.PlayOneShot(caughtScreamClip, caughtVolume);
        }

        BGMController.Instance.SetVolume(0f);
    }
}

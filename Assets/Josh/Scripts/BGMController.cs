using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BGMController : MonoBehaviour
{
    public static BGMController Instance;  // Singleton for global access

    public AudioSource bgmSource;
    [Range(0f, 1f)] public float normalVolume = 0.5f;
    [Range(0f, 1f)] public float duckVolume = 0.2f;
    public float fadeSpeed = 1.5f;

    private float targetVolume;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    void Start()
    {
        if (bgmSource)
        {
            bgmSource.loop = true;
            bgmSource.volume = normalVolume;
            bgmSource.Play();
        }

        targetVolume = normalVolume;
    }

    void Update()
    {
        if (bgmSource)
        {
            bgmSource.volume = Mathf.MoveTowards(bgmSource.volume, targetVolume, Time.deltaTime * fadeSpeed);
        }
    }

    public void DuckVolume() => targetVolume = duckVolume;
    public void RestoreVolume() => targetVolume = normalVolume;
    public void SetVolume(float custom) => targetVolume = Mathf.Clamp01(custom);
}

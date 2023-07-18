using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffect : MonoBehaviour
{
    [SerializeField] private UIandAudioManager audioManager;
    [SerializeField] private AudioSource ads;

    private void OnEnable()
    {
        PlaySound();
    }

    public void PlaySound()
    {
        ads.volume = audioManager.effectVolume;
        ads.Play();
    }
}

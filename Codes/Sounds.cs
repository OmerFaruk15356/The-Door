

using System.Collections.Generic;
using UnityEngine;

public class Sounds : MonoBehaviour
{
  [SerializeField]
  private AudioSource audioSource;
  [SerializeField]
  private AudioSource audioSource2;
  public List<AudioClip> clips;
  public List<AudioClip> musics;
  private int currentTrackIndex;

  private void Start() => this.PlayMusic();

  private void Update()
  {
    if (this.audioSource2.isPlaying)
      return;
    this.PlayMusic();
  }

  public void PlaySound(AudioClip audioClip)
  {
    this.audioSource.clip = audioClip;
    this.audioSource.Play();
  }

  public void PlayMusic()
  {
    this.audioSource2.clip = this.musics[this.currentTrackIndex];
    this.audioSource2.Play();
    this.currentTrackIndex = (this.currentTrackIndex + 1) % this.musics.Count;
  }
}

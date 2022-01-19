using System.Collections;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
  [SerializeField] AudioSource themeAudioSource;
  [SerializeField] AudioSource sfxSource;
  [SerializeField] AudioSource noiseSource;
  [SerializeField] AudioSource regularNoiseSource;
  [SerializeField] AudioClip mainTheme;
  [SerializeField] AudioClip preTheme;
  [SerializeField] AudioClip garageDoorSFX;
  [SerializeField] AudioClip lightBulbBurnSFX;

  [SerializeField] AudioClip[] noisesSFX;

  public void PlayPreTheme()
  {
    themeAudioSource.clip = preTheme;
    themeAudioSource.Play();
  }

  public void PlayMainTheme()
  {
    //themeAudioSource.clip = mainTheme;
    //themeAudioSource.loop = false;
    themeAudioSource.PlayOneShot(mainTheme);
  }

  public void PlayGarageDoorOpen()
  {
    sfxSource.PlayOneShot(garageDoorSFX);
    Invoke(nameof(PlayGarageNoisesLoop), 4);
  }

  public void PlayLightBurningLoop()
  {
    StartCoroutine(PlayLightBurningLoopRutine());
  }

  IEnumerator PlayLightBurningLoopRutine()
  {
    sfxSource.volume = 0.08f;
    sfxSource.pitch = 0.9f;
    yield return new WaitForSeconds(2);
    for (int i = 0; i < 5; i++)
    {
      sfxSource.PlayOneShot(lightBulbBurnSFX);
      yield return new WaitForSeconds(i+0.1f);
      sfxSource.volume += 0.018f;
      sfxSource.pitch += 0.015f;
    }
    sfxSource.volume = 1;
    sfxSource.pitch = 1;
  }

  public void PlayGarageNoisesLoop()
  {
    StartCoroutine(PlayGarageNoisesLoopRutine());
    InvokeRepeating(nameof(PlaysNormalNoise), 0.1f, 5);
  }

  IEnumerator PlayGarageNoisesLoopRutine()
  {
    while (true)
    {
      noiseSource.PlayOneShot(noisesSFX[Random.Range(0, 2)]);
      yield return new WaitForSeconds(Random.Range(3, 6));
    }
  }

  void PlaysNormalNoise()
  {
    regularNoiseSource.PlayOneShot(noisesSFX[2]);
  }
}

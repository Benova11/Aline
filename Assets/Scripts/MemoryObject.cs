using System.Collections;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class MemoryObject : MonoBehaviour
{
  GarageSceneManager garageSceneManager;

  bool isPlaced = false;

  [SerializeField] SpriteRenderer mainSpriteRenderer;
  [SerializeField] Transform sceneCenterPoint;
  [SerializeField] Transform moveTarget;
  [SerializeField] Vector3 sizeToBe;
  [SerializeField] Vector3 photoFramesizeToBe;

  [SerializeField] GameObject photoFrame;
  [SerializeField] SpriteRenderer frameContent;
  [SerializeField] GameObject memoryLight;

  [SerializeField] Light2D memoryLightComp;

  [SerializeField] ParticleSystem clueParticles;

  private void Awake()
  {
    clueParticles.Stop();
    garageSceneManager = FindObjectOfType<GarageSceneManager>();
    photoFrame.transform.localScale = new Vector3(0, 0, 0);
    frameContent.color = new Color(frameContent.color.r, frameContent.color.g, frameContent.color.b, 0);
  }

  public void ReadyToInteract()
  {
    clueParticles.Play();
  }

  void OnMouseDown()
  {
    if (garageSceneManager.IsGarageOpen && garageSceneManager.flashLight.activeInHierarchy && !isPlaced)
    {
      OnMemoryClicked();
    }
  }

  void OnMemoryClicked()
  {
    isPlaced = true;
    garageSceneManager.OnMemoryFound();
    clueParticles.Stop();
    StartCoroutine(AnimateMemoryRoutine());
  }

  IEnumerator AnimateMemoryRoutine()
  {
    memoryLight.SetActive(true);
    ChangeObjectAlpha(mainSpriteRenderer, 0.7f, 0);
    MoveToCenter();
    yield return new WaitForSeconds(4);
    AddToMemories();
    yield return new WaitForSeconds(3.5f);
    garageSceneManager.OnMemoryPlaced();
  }

  void MoveToCenter()
  {
    photoFrame.transform.position = sceneCenterPoint.position;
    //LeanTween.move(photoFrame, sceneCenterPoint, 1.5f).setEaseInOutSine();
    LeanTween.scale(photoFrame, photoFramesizeToBe, 1f).setEaseLinear();
    ChangeObjectAlpha(frameContent, 3, 1f, 1);
  }

  void AddToMemories()
  {
    LeanTween.move(photoFrame, moveTarget, 3.5f).setEaseInOutSine();
    LeanTween.value(memoryLightComp.intensity, 0.7f, 1f).setDelay(0.5f).setOnUpdate(SetMemoryLightIntensity);
    LeanTween.scale(photoFrame, new Vector3(0.25f, 0.25f, 0.25f), 1f).setEaseLinear();
    ChangeObjectAlpha(mainSpriteRenderer, 2, 0.3f, 0.5f);
  }

  void ChangeObjectAlpha(SpriteRenderer spriteRenderer, float timeToChange = 2f, float value = 0.3f, float delay = 0)
  {
    LeanTween.value(gameObject, spriteRenderer.color.a, value, timeToChange).setDelay(delay).setOnUpdate((float val) =>
    {
      Color c = spriteRenderer.color;
      c.a = val;
      spriteRenderer.color = c;
    });
  }


  void SetMemoryLightIntensity(float intensityValue)
  {
    memoryLightComp.intensity = intensityValue;
  }
}

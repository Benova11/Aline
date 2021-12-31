using System.Collections;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class MemoryObject : MonoBehaviour
{
  GarageSceneManager garageSceneManager;

  bool isPlaced = false;

  [SerializeField] Transform sceneCenterPoint;
  [SerializeField] Transform moveTarget;
  [SerializeField] Vector3 sizeToBe;
  [SerializeField] Vector3 photoFramesizeToBe;

  [SerializeField] GameObject photoFrame;
  [SerializeField] GameObject memoryLight;

  [SerializeField] Light2D memoryLightComp;

  private void Awake()
  {
    garageSceneManager = FindObjectOfType<GarageSceneManager>();
    photoFrame.transform.localScale = new Vector3(0, 0, 0);
  }

  void OnMouseDown()
  {
    if (garageSceneManager.IsGarageOpen && garageSceneManager.flashLight.activeInHierarchy && !isPlaced)
    {
      isPlaced = true;
      StartCoroutine(AnimateMemoryRoutine());
    }
  }

  IEnumerator AnimateMemoryRoutine()
  {
    garageSceneManager.OnMemoryFound();
    memoryLight.SetActive(true);
    MoveToCenter();
    yield return new WaitForSeconds(3);
    AddToMemories();
    yield return new WaitForSeconds(3.5f);
    garageSceneManager.OnMemoryPlaced();
  }

  void MoveToCenter()
  {
    LeanTween.move(gameObject, sceneCenterPoint, 2f).setEaseInOutSine();
    LeanTween.scale(gameObject, gameObject.transform.localScale * 1.1f, 2.5f).setEaseLinear();
    LeanTween.scale(photoFrame, photoFramesizeToBe, 2f).setDelay(1).setEaseLinear();
  }

  void AddToMemories()
  {
    LeanTween.move(gameObject, moveTarget, 3.5f).setEaseInOutSine();
    LeanTween.scale(gameObject, sizeToBe, 2.5f).setEaseLinear();
    LeanTween.value(memoryLightComp.intensity, 0.75f, 1f).setDelay(0.5f).setOnUpdate(SetMemoryLightIntensity);
  }


  void SetMemoryLightIntensity(float intensityValue)
  {
    memoryLightComp.intensity = intensityValue;
  }
}

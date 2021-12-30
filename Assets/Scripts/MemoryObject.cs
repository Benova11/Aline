using UnityEngine;

public class MemoryObject : MonoBehaviour
{
  GarageSceneManager garageSceneManager;

  bool isPlaced = false;

  [SerializeField] Transform moveTarget;
  [SerializeField] Vector3 sizeToBe;

  private void Awake()
  {
    garageSceneManager = FindObjectOfType<GarageSceneManager>();
  }

  void OnMouseDown()
  {
    if (garageSceneManager.IsGarageOpen && garageSceneManager.flashLight.activeInHierarchy && !isPlaced)
    {
      isPlaced = true;
      LeanTween.move(gameObject, moveTarget, 3.5f).setEaseInOutSine();
      LeanTween.scale(gameObject, sizeToBe, 2.5f).setEaseLinear();
    }
  }
}

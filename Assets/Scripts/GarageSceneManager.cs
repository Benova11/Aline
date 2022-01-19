using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using TMPro;
using System.Collections.Generic;

public class GarageSceneManager : MonoBehaviour
{
  [SerializeField] MusicManager musicManager;

  public GameObject flashLight;
  [SerializeField] GameObject ligtSwitch;
  [SerializeField] Light2D roomLight;

  [SerializeField] TextMeshProUGUI tutorialText;

  bool tutorialOn = true;
  bool canInteract = false;

  [SerializeField] GameObject garageDoor;
  [SerializeField] float openGarageDoorTime = 3f;
  bool isGarageOpen;
  public bool IsGarageOpen { get { return isGarageOpen; } }

  [SerializeField] List<MemoryObject> memoryObjects;

  void Awake()
  {
    roomLight.intensity = 0.1f; //0.12
  }

  void Update()
  {
    if (Input.GetMouseButtonDown(1) && canInteract)
    {
      if (tutorialOn)
      {
        tutorialOn = false;
        tutorialText.gameObject.SetActive(false);
        LeanTween.value(roomLight.intensity, 0.15f, 1f).setOnUpdate(SetRoomLightIntensity);
        InitMemories();
      }
      flashLight.SetActive(!flashLight.activeInHierarchy);
    }
  }

  void StartTutorial()
  {
    canInteract = true;
    ShowFlashLightTutorial();
  }

  void InitMemories()
  {
    foreach(MemoryObject memoryObject in memoryObjects)
    {
      memoryObject.ReadyToInteract();
    }
  }

  void ShowFlashLightTutorial()
  {
    tutorialText.gameObject.SetActive(true);
    LeanTween.alphaCanvas(tutorialText.GetComponent<CanvasGroup>(), 0.9f, 1).setLoopPingPong();
  }

  void SetRoomLightIntensity(float intensityValue)
  {
    roomLight.intensity = intensityValue;
  }

  public void TurnOnGarageLight()
  {
    ligtSwitch.SetActive(false);
    roomLight.intensity = 0.9f;
    AnimateRoomLight();
    musicManager.PlayMainTheme();
  }

  public void OpenGarage()
  {
    musicManager.PlayPreTheme();
    musicManager.PlayGarageDoorOpen();
    float target = garageDoor.transform.position.y + 7;
    LeanTween.moveY(garageDoor, target, openGarageDoorTime).setEaseOutBounce().setOnComplete(() => { isGarageOpen = true; });
  }

  void ShowObject(SpriteRenderer spriteRenderer, float delay = 0)
  {
    LeanTween.value(gameObject, 0, 1, 2).setDelay(delay).setOnUpdate((float val) =>
    {
      Color c = spriteRenderer.color;
      c.a = val;
      spriteRenderer.color = c;
    });
  }

  public void OnMemoryFound(bool turnOffFlash = false)
  {
    canInteract = false;
    flashLight.SetActive(turnOffFlash);
  }

  public void OnMemoryPlaced(bool turnOffFlash = true)
  {
    canInteract = true;
    flashLight.SetActive(turnOffFlash);
  }

  void AnimateRoomLight()
  {
    musicManager.PlayLightBurningLoop();
    var seq = LeanTween.sequence();
    seq.append(2);
    seq.append(LeanTween.value(roomLight.intensity, 0.7f, 0.14f).setOnUpdate(SetRoomLightIntensity));
    seq.append(0.2f);
    seq.append(LeanTween.value(roomLight.intensity, 0.2f, 0.14f).setOnUpdate(SetRoomLightIntensity));
    seq.append(0.14f);
    seq.append(LeanTween.value(roomLight.intensity, 0.4f, 0.14f).setOnUpdate(SetRoomLightIntensity));
    seq.append(0.2f);
    seq.append(LeanTween.value(roomLight.intensity, 0.7f, 0.14f).setOnUpdate(SetRoomLightIntensity));
    seq.append(2);
    seq.append(LeanTween.value(roomLight.intensity, 0.4f, 0.14f).setOnUpdate(SetRoomLightIntensity));
    seq.append(1);
    seq.append(LeanTween.value(roomLight.intensity, 0.3f, 0.14f).setOnUpdate(SetRoomLightIntensity));
    seq.append(1.141f);
    seq.append(LeanTween.value(roomLight.intensity, 0.5f, 0.2f).setOnUpdate(SetRoomLightIntensity));
    seq.append(0.21f);
    seq.append(LeanTween.value(roomLight.intensity, 0.25f, 0.1f).setOnUpdate(SetRoomLightIntensity));
    seq.append(0.61f);
    seq.append(LeanTween.value(roomLight.intensity, 0.15f, 0.1f).setOnUpdate(SetRoomLightIntensity));
    seq.append(0.11f);
    seq.append(LeanTween.value(roomLight.intensity, 0.3f, 0.07f).setOnUpdate(SetRoomLightIntensity));
    seq.append(0.21f);
    seq.append(LeanTween.value(roomLight.intensity, 0.5f, 0.1f).setOnUpdate(SetRoomLightIntensity));
    seq.append(0.11f);
    seq.append(LeanTween.value(roomLight.intensity, 0.25f, 0.05f).setOnUpdate(SetRoomLightIntensity));
    seq.append(0.11f);
    seq.append(LeanTween.value(roomLight.intensity, 0.05f, 0.08f).setOnUpdate(SetRoomLightIntensity).setOnComplete(() => { Invoke(nameof(StartTutorial), 4); }));
    //seq.append(1f);
    //seq.append(LeanTween.value(roomLight.intensity, 0.1f, 1f).setOnUpdate(SetRoomLightIntensity).setOnComplete(() => { Invoke(nameof(StartTutorial), 2); }));
  }
}

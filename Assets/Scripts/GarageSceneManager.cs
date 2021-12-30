using TMPro;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class GarageSceneManager : MonoBehaviour
{
  [SerializeField] CanvasGroup shelf;
  [SerializeField] GameObject shelfGo;
  [SerializeField] GameObject ron;
  [SerializeField] Transform openShelfpoint;

  [SerializeField] GameObject garageDoor;

  public GameObject flashLight;
  [SerializeField] GameObject ligtSwitch;
  [SerializeField] Light2D roomLight;

  [SerializeField] TextMeshProUGUI tutorialText;

  [SerializeField] float openGarageDoorTime = 3f;

  bool tutorialOn = true;
  bool canInteract = false;

  bool isGarageOpen;
  public bool IsGarageOpen { get { return isGarageOpen; } }

  void Awake()
  {
    roomLight.intensity = 0.12f;
  }

  void Start()
  {
    //ShowRon();
  }

  public void OpenShelf()
  {
    LeanTween.alphaCanvas(shelf, 1, 1);
    LeanTween.moveX(shelfGo, openShelfpoint.position.x, 2);
  }

  void ShowRon()
  {
    //LeanTween.alphaCanvas(ron, 1, 3);
    ShowObject(ron.GetComponent<SpriteRenderer>(), 3);
  }

  void ShowObject(SpriteRenderer spriteRenderer,float delay = 0)
  {
    //Image skipBtnImg = skipButton.gameObject.GetComponent<Image>();
    LeanTween.value(gameObject, 0, 1, 2).setDelay(delay).setOnUpdate((float val) =>
    {
      Color c = spriteRenderer.color;
      c.a = val;
      spriteRenderer.color = c;
    });
  }

  void AnimateRoomLight()
  {
    //roomLight.intensity
    var seq = LeanTween.sequence();
    seq.append(2);
    seq.append(LeanTween.value(roomLight.intensity, 0.3f, 0.14f).setOnUpdate(SetRoomLightIntensity));
    seq.append(0.141f);
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
    seq.append(LeanTween.value(roomLight.intensity, 0.12f, 0.08f).setOnUpdate(SetRoomLightIntensity).setOnComplete(()=> { Invoke(nameof(StartTutorial),2); }));
  }

  void StartTutorial()
  {
    canInteract = true;
    ShowFlashLightTutorial();
  }

  private void Update()
  {
    if (Input.GetMouseButtonDown(1) && canInteract)
    {
      if (tutorialOn)
      {
        tutorialOn = false;
        tutorialText.gameObject.SetActive(false);
      }
      flashLight.SetActive(!flashLight.activeInHierarchy);
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

  public void TurnOnLight()
  {
    ligtSwitch.SetActive(false);
    roomLight.intensity = 0.9f;
    AnimateRoomLight();
  }

  public void OpenGarage()
  {
    float target = garageDoor.transform.position.y + 9;
    LeanTween.moveY(garageDoor, target, openGarageDoorTime).setEaseOutBounce().setOnComplete(()=> { isGarageOpen = true; });
  }

}

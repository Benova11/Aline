using UnityEngine;

public class SceneManager : MonoBehaviour
{
  [SerializeField] CanvasGroup shelf;
  [SerializeField] GameObject shelfGo;
  [SerializeField] GameObject ron;
  [SerializeField] Transform openShelfpoint;


  void Awake()
  {
  }

  void Start()
  {
    ShowRon();
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

}

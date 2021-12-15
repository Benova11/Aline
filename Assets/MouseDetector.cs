
using UnityEngine;

public class MouseDetector : MonoBehaviour
{
  public float depth = 10.0f;
  // Start is called before the first frame update
  void Start()
    {
    Cursor.visible = false;

  }

  // Update is called once per frame
  void Update()
    {
    FollowMousePosition();
  }

  void FollowMousePosition()
  {
    Vector3 mousePos = Input.mousePosition;
    Vector3 wantedPos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, depth));
    transform.position = wantedPos;
  }
}
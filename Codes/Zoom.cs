

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Zoom : MonoBehaviour
{
  [SerializeField]
  private List<Button> buttons;
  [SerializeField]
  private List<GameObject> puzzles;
  [SerializeField]
  private GameObject cameraBounds;
  [SerializeField]
  private float zoomRatio = 0.5f;
  [SerializeField]
  private Vector3 puzzlesPos;
  [SerializeField]
  private GameObject inputField;
  public bool isZoom;
  private Rooms rooms;

  private void Awake() => this.rooms = Object.FindObjectOfType<Rooms>();

  public void ZoomCamera(Transform gbTransform)
  {
    Camera.main.orthographicSize *= this.zoomRatio;
    ((Component) Camera.main).transform.position = new Vector3(((Component) gbTransform).transform.position.x, ((Component) gbTransform).transform.position.y, ((Component) Camera.main).transform.position.z);
    ((Component) this.buttons[0]).gameObject.SetActive(false);
    ((Component) this.buttons[1]).gameObject.SetActive(false);
    ((Component) this.buttons[2]).gameObject.SetActive(true);
    this.isZoom = true;
    this.ConstrainCamera();
  }

  private void ConstrainCamera()
  {
    float orthographicSize = Camera.main.orthographicSize;
    float num = orthographicSize * Camera.main.aspect;
    if ((double) ((Component) Camera.main).transform.position.x + (double) num > (double) this.cameraBounds.transform.position.x + (double) this.cameraBounds.GetComponent<BoxCollider2D>().size.x / 2.0)
    {
      Transform transform = ((Component) Camera.main).transform;
      transform.position = Vector3.op_Addition(transform.position, new Vector3((float) ((double) this.cameraBounds.transform.position.x + (double) this.cameraBounds.GetComponent<BoxCollider2D>().size.x / 2.0 - ((double) ((Component) Camera.main).transform.position.x + (double) num)), 0.0f, 0.0f));
    }
    if ((double) ((Component) Camera.main).transform.position.x - (double) num < (double) this.cameraBounds.transform.position.x - (double) this.cameraBounds.GetComponent<BoxCollider2D>().size.x / 2.0)
    {
      Transform transform = ((Component) Camera.main).transform;
      transform.position = Vector3.op_Addition(transform.position, new Vector3((float) ((double) this.cameraBounds.transform.position.x - (double) this.cameraBounds.GetComponent<BoxCollider2D>().size.x / 2.0 - ((double) ((Component) Camera.main).transform.position.x - (double) num)), 0.0f, 0.0f));
    }
    if ((double) ((Component) Camera.main).transform.position.y + (double) orthographicSize > (double) this.cameraBounds.transform.position.y + (double) this.cameraBounds.GetComponent<BoxCollider2D>().size.y / 2.0)
    {
      Transform transform = ((Component) Camera.main).transform;
      transform.position = Vector3.op_Addition(transform.position, new Vector3(0.0f, (float) ((double) this.cameraBounds.transform.position.y + (double) this.cameraBounds.GetComponent<BoxCollider2D>().size.y / 2.0 - ((double) ((Component) Camera.main).transform.position.y + (double) orthographicSize)), 0.0f));
    }
    if ((double) ((Component) Camera.main).transform.position.y - (double) orthographicSize >= (double) this.cameraBounds.transform.position.y - (double) this.cameraBounds.GetComponent<BoxCollider2D>().size.y / 2.0)
      return;
    Transform transform1 = ((Component) Camera.main).transform;
    transform1.position = Vector3.op_Addition(transform1.position, new Vector3(0.0f, (float) ((double) this.cameraBounds.transform.position.y - (double) this.cameraBounds.GetComponent<BoxCollider2D>().size.y / 2.0 - ((double) ((Component) Camera.main).transform.position.y - (double) orthographicSize)), 0.0f));
  }

  public void ZoomOut()
  {
    Camera.main.orthographicSize /= this.zoomRatio;
    ((Component) Camera.main).transform.position = new Vector3(this.cameraBounds.transform.position.x, this.cameraBounds.transform.position.y, ((Component) Camera.main).transform.position.z);
    if (this.rooms.currentRoom != 0)
      ((Component) this.buttons[0]).gameObject.SetActive(true);
    if (this.rooms.currentRoom != 3)
      ((Component) this.buttons[1]).gameObject.SetActive(true);
    ((Component) this.buttons[2]).gameObject.SetActive(false);
    this.inputField.SetActive(false);
    this.checkPuzzle();
    this.isZoom = false;
  }

  public void zoomPuzzle(int value)
  {
    this.checkPuzzle();
    ((Component) Camera.main).transform.position = this.puzzlesPos;
    this.puzzles[value].gameObject.SetActive(true);
  }

  public void checkPuzzle()
  {
    foreach (GameObject puzzle in this.puzzles)
    {
      if (puzzle.gameObject.activeSelf)
        puzzle.gameObject.SetActive(false);
    }
  }
}



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
		Camera.main.transform.position = new Vector3(gbTransform.transform.position.x, gbTransform.transform.position.y, Camera.main.transform.position.z);
		this.buttons[0].gameObject.SetActive(false);
		this.buttons[1].gameObject.SetActive(false);
		this.buttons[2].gameObject.SetActive(true);
		this.isZoom = true;
		this.ConstrainCamera();
  }

  private void ConstrainCamera()
  {
		float orthographicSize = Camera.main.orthographicSize;
		float num = orthographicSize * Camera.main.aspect;
		if (Camera.main.transform.position.x + num > this.cameraBounds.transform.position.x + this.cameraBounds.GetComponent<BoxCollider2D>().size.x / 2f)
		{
			Camera.main.transform.position += new Vector3(this.cameraBounds.transform.position.x + this.cameraBounds.GetComponent<BoxCollider2D>().size.x / 2f - (Camera.main.transform.position.x + num), 0f, 0f);
		}
		if (Camera.main.transform.position.x - num < this.cameraBounds.transform.position.x - this.cameraBounds.GetComponent<BoxCollider2D>().size.x / 2f)
		{
			Camera.main.transform.position += new Vector3(this.cameraBounds.transform.position.x - this.cameraBounds.GetComponent<BoxCollider2D>().size.x / 2f - (Camera.main.transform.position.x - num), 0f, 0f);
		}
		if (Camera.main.transform.position.y + orthographicSize > this.cameraBounds.transform.position.y + this.cameraBounds.GetComponent<BoxCollider2D>().size.y / 2f)
		{
			Camera.main.transform.position += new Vector3(0f, this.cameraBounds.transform.position.y + this.cameraBounds.GetComponent<BoxCollider2D>().size.y / 2f - (Camera.main.transform.position.y + orthographicSize), 0f);
		}
		if (Camera.main.transform.position.y - orthographicSize < this.cameraBounds.transform.position.y - this.cameraBounds.GetComponent<BoxCollider2D>().size.y / 2f)
		{
			Camera.main.transform.position += new Vector3(0f, this.cameraBounds.transform.position.y - this.cameraBounds.GetComponent<BoxCollider2D>().size.y / 2f - (Camera.main.transform.position.y - orthographicSize), 0f);
		}
  }

  public void ZoomOut()
  {
		Camera.main.orthographicSize /= this.zoomRatio;
		Camera.main.transform.position = new Vector3(this.cameraBounds.transform.position.x, this.cameraBounds.transform.position.y, Camera.main.transform.position.z);
		if (this.rooms.currentRoom != 0)
		{
			this.buttons[0].gameObject.SetActive(true);
		}
		if (this.rooms.currentRoom != 3)
		{
			this.buttons[1].gameObject.SetActive(true);
		}
		this.buttons[2].gameObject.SetActive(false);
		this.inputField.SetActive(false);
		this.checkPuzzle();
		this.isZoom = false;
  }

  public void zoomPuzzle(int value)
  {
		this.checkPuzzle();
		Camera.main.transform.position = this.puzzlesPos;
		this.puzzles[value].gameObject.SetActive(true);
  }

  public void checkPuzzle()
  {
		foreach (GameObject gameObject in this.puzzles)
		{
			if (gameObject.gameObject.activeSelf)
			{
				gameObject.gameObject.SetActive(false);
			}
		}
  }
}

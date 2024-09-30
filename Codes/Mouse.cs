
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class Mouse : MonoBehaviour
{
  [SerializeField]
  private Button chat;
  [SerializeField]
  private List<Sprite> sprites;
  [SerializeField]
  private GameObject inputField;
  [SerializeField]
  private GameObject libary;
  [SerializeField]
  private GameObject hat;
  [SerializeField]
  private GameObject paper;
  [SerializeField]
  private GameObject door;
  private Zoom zoom;
  private InventoryManager inventoryManager;
  private Puzzles puzzles;
  private Sounds sounds;
  private Scene scene;
  public GameObject currentChessPiece;
  public GameObject prevChessPiece;
  private Vector3 chessPiecePrevPos;
  private int crosbarCount;

  private void Awake()
  {
    this.scene = Object.FindObjectOfType<Scene>();
    this.sounds = Object.FindObjectOfType<Sounds>();
    this.zoom = Object.FindObjectOfType<Zoom>();
    this.inventoryManager = Object.FindObjectOfType<InventoryManager>();
    this.puzzles = Object.FindObjectOfType<Puzzles>();
  }

  private void Update() => this.InteractObject();

public void InteractObject()
	{
		if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
		{
			Vector3 vector = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			RaycastHit2D raycastHit2D = Physics2D.Raycast(new Vector2(vector.x, vector.y), Vector2.zero);
			if (raycastHit2D.collider != null && raycastHit2D.collider.gameObject.tag == "Interactable")
			{
				if (raycastHit2D.collider.gameObject.name == "Door" && this.zoom.isZoom)
				{
					if (this.inventoryManager.selectedGameObject == null)
					{
						this.chat.gameObject.SetActive(true);
						this.chat.GetComponentInChildren<TextMeshProUGUI>().text = "The door is locked. We need to find key to open it";
						this.sounds.PlaySound(this.sounds.clips[15]);
						return;
					}
					if (this.inventoryManager.selectedGameObject.name == "SilverKey")
					{
						this.chat.gameObject.SetActive(true);
						this.chat.GetComponentInChildren<TextMeshProUGUI>().text = "This key doesn't open the door";
						this.sounds.PlaySound(this.sounds.clips[15]);
					}
					else if (this.inventoryManager.selectedGameObject.name != "GoldenKey")
					{
						this.chat.gameObject.SetActive(true);
						this.chat.GetComponentInChildren<TextMeshProUGUI>().text = "I can't open the door with this";
						this.sounds.PlaySound(this.sounds.clips[15]);
					}
					else if (this.inventoryManager.selectedGameObject.name == "GoldenKey")
					{
						this.door.GetComponent<SpriteRenderer>().sprite = this.sprites[8];
						this.door.name = "OpennedDoor";
						this.sounds.PlaySound(this.sounds.clips[16]);
						return;
					}
				}
				if (raycastHit2D.collider.gameObject.name == "OpennedDoor" && this.zoom.isZoom)
				{
					this.scene.FinishGame();
				}
				if (raycastHit2D.collider.gameObject.name == "Carpet" && this.zoom.isZoom)
				{
					raycastHit2D.collider.gameObject.transform.rotation = Quaternion.Euler(46.845f, -11.64f, -15.776f);
					Vector3 position = raycastHit2D.collider.gameObject.transform.parent.TransformPoint(new Vector3(-0.12f, -5.78f, -2f));
					raycastHit2D.collider.gameObject.transform.position = position;
					raycastHit2D.collider.gameObject.GetComponent<BoxCollider2D>().enabled = false;
					raycastHit2D.collider.gameObject.transform.GetChild(0).gameObject.SetActive(true);
					this.sounds.PlaySound(this.sounds.clips[2]);
				}
				if (raycastHit2D.collider.gameObject.name == "SilverKey" && this.zoom.isZoom)
				{
					this.chat.gameObject.SetActive(true);
					this.chat.GetComponentInChildren<TextMeshProUGUI>().text = "We can open something with this key";
					this.inventoryManager.AddItem(raycastHit2D.collider.gameObject);
					Object.Destroy(raycastHit2D.collider.gameObject);
					this.sounds.PlaySound(this.sounds.clips[9]);
				}
				if (raycastHit2D.collider.gameObject.name == "GoldenKey" && this.zoom.isZoom)
				{
					this.chat.gameObject.SetActive(true);
					this.chat.GetComponentInChildren<TextMeshProUGUI>().text = "Maybe we can open the door with this key";
					this.inventoryManager.AddItem(raycastHit2D.collider.gameObject);
					Object.Destroy(raycastHit2D.collider.gameObject);
					this.sounds.PlaySound(this.sounds.clips[9]);
				}
				if (raycastHit2D.collider.gameObject.name == "Paint1Part" && this.zoom.isZoom)
				{
					this.chat.gameObject.SetActive(true);
					this.chat.GetComponentInChildren<TextMeshProUGUI>().text = "Oh, there it is! One of the missing parts.";
					this.inventoryManager.AddItem(raycastHit2D.collider.gameObject);
					Object.Destroy(raycastHit2D.collider.gameObject);
					this.sounds.PlaySound(this.sounds.clips[13]);
				}
				if (raycastHit2D.collider.gameObject.name == "Paint2Part" && this.zoom.isZoom)
				{
					this.chat.gameObject.SetActive(true);
					this.chat.GetComponentInChildren<TextMeshProUGUI>().text = "Oh, there it is! One of the missing parts.";
					this.inventoryManager.AddItem(raycastHit2D.collider.gameObject);
					Object.Destroy(raycastHit2D.collider.gameObject);
					this.sounds.PlaySound(this.sounds.clips[13]);
				}
				if (raycastHit2D.collider.gameObject.name == "Crowbar" && this.zoom.isZoom)
				{
					raycastHit2D.collider.transform.parent.GetComponent<SpriteRenderer>().sprite = this.sprites[1];
					this.inventoryManager.AddItem(raycastHit2D.collider.gameObject);
					Object.Destroy(raycastHit2D.collider.gameObject);
					this.sounds.PlaySound(this.sounds.clips[6]);
				}
				if (raycastHit2D.collider.gameObject.name == "Word" && this.zoom.isZoom)
				{
					this.zoom.zoomPuzzle(0);
					this.inputField.SetActive(true);
				}
				if (raycastHit2D.collider.gameObject.name == "Number" && this.zoom.isZoom)
				{
					this.zoom.zoomPuzzle(1);
				}
				if (raycastHit2D.collider.gameObject.name == "Paper" && this.zoom.isZoom)
				{
					this.chat.gameObject.SetActive(true);
					this.chat.GetComponentInChildren<TextMeshProUGUI>().text = "It is just kind of code";
					this.zoom.zoomPuzzle(3);
					this.sounds.PlaySound(this.sounds.clips[12]);
				}
				if (raycastHit2D.collider.gameObject.name == "Symbol" && this.zoom.isZoom)
				{
					this.zoom.zoomPuzzle(2);
				}
				if (raycastHit2D.collider.gameObject.name == "KeyPuzzle" && this.zoom.isZoom)
				{
					this.zoom.zoomPuzzle(4);
				}
				if (raycastHit2D.collider.gameObject.name == "Chess" && this.zoom.isZoom)
				{
					this.chat.gameObject.SetActive(true);
					this.chat.GetComponentInChildren<TextMeshProUGUI>().text = "I wish I am good at chees";
					this.zoom.zoomPuzzle(5);
				}
				if (raycastHit2D.collider.gameObject.name == "Clock" && this.zoom.isZoom)
				{
					this.chat.gameObject.SetActive(true);
					this.chat.GetComponentInChildren<TextMeshProUGUI>().text = "It looks like the clock has stopped";
				}
				if (raycastHit2D.collider.gameObject.name == "ArrowSymbol" && this.zoom.isZoom)
				{
					this.chat.gameObject.SetActive(true);
					this.chat.GetComponentInChildren<TextMeshProUGUI>().text = "There is a symbol!";
				}
				if (raycastHit2D.collider.gameObject.name == "Flower" && this.zoom.isZoom)
				{
					this.chat.gameObject.SetActive(true);
					this.chat.GetComponentInChildren<TextMeshProUGUI>().text = "Someone needs to water the flower";
				}
				if (raycastHit2D.collider.gameObject.name == "Book" && this.zoom.isZoom)
				{
					this.chat.gameObject.SetActive(true);
					this.chat.GetComponentInChildren<TextMeshProUGUI>().text = "What will I do with this book?";
					this.inventoryManager.AddItem(raycastHit2D.collider.gameObject);
					Object.Destroy(raycastHit2D.collider.gameObject);
					this.sounds.PlaySound(this.sounds.clips[0]);
				}
				if (raycastHit2D.collider.gameObject.name == "Glove" && this.zoom.isZoom)
				{
					this.inventoryManager.AddItem(raycastHit2D.collider.gameObject);
					Object.Destroy(raycastHit2D.collider.gameObject);
					this.sounds.PlaySound(this.sounds.clips[8]);
				}
				if (raycastHit2D.collider.gameObject.name == "Hat" && this.zoom.isZoom)
				{
					this.chat.gameObject.SetActive(true);
					this.chat.GetComponentInChildren<TextMeshProUGUI>().text = "Nice hat!";
					this.inventoryManager.AddItem(raycastHit2D.collider.gameObject);
					raycastHit2D.collider.gameObject.SetActive(false);
					this.sounds.PlaySound(this.sounds.clips[8]);
				}
				if (raycastHit2D.collider.gameObject.name == "Hanger" && this.zoom.isZoom)
				{
					if (this.inventoryManager.selectedGameObject == null)
					{
						return;
					}
					if (this.inventoryManager.selectedGameObject.name == "Hat")
					{
						this.hat.gameObject.SetActive(true);
						this.hat.GetComponent<BoxCollider2D>().enabled = false;
						this.hat.transform.position = new Vector3(39.25f, 3.8f, -7f);
						this.hat.transform.rotation = Quaternion.Euler(0f, 0f, -22.92f);
						this.paper.SetActive(true);
						this.paper.GetComponent<Test>().StartDown();
						this.inventoryManager.DeleteItem(this.inventoryManager.selectedGameObject);
						this.inventoryManager.selectedGameObject = null;
					}
				}
				if (raycastHit2D.collider.gameObject.name == "Paint1" && this.zoom.isZoom)
				{
					if (this.inventoryManager.selectedGameObject == null || this.inventoryManager.selectedGameObject.name != "Paint1Part")
					{
						this.chat.gameObject.SetActive(true);
						this.chat.GetComponentInChildren<TextMeshProUGUI>().text = "I wonder where the missing piece is?";
						return;
					}
					if (this.inventoryManager.selectedGameObject.name == "Paint1Part")
					{
						raycastHit2D.collider.gameObject.transform.GetChild(0).gameObject.SetActive(true);
						raycastHit2D.collider.gameObject.GetComponent<BoxCollider2D>().enabled = false;
						this.inventoryManager.DeleteItem(this.inventoryManager.selectedGameObject);
						this.inventoryManager.selectedGameObject = null;
						this.puzzles.CheckPaints();
						this.sounds.PlaySound(this.sounds.clips[14]);
					}
				}
				if (raycastHit2D.collider.gameObject.name == "Paint2" && this.zoom.isZoom)
				{
					if (this.inventoryManager.selectedGameObject == null || this.inventoryManager.selectedGameObject.name != "Paint2Part")
					{
						this.chat.gameObject.SetActive(true);
						this.chat.GetComponentInChildren<TextMeshProUGUI>().text = "I wonder where the missing piece is?";
						return;
					}
					if (this.inventoryManager.selectedGameObject.name == "Paint2Part")
					{
						raycastHit2D.collider.gameObject.transform.GetChild(0).gameObject.SetActive(true);
						raycastHit2D.collider.gameObject.GetComponent<BoxCollider2D>().enabled = false;
						this.inventoryManager.DeleteItem(this.inventoryManager.selectedGameObject);
						this.inventoryManager.selectedGameObject = null;
						this.puzzles.CheckPaints();
						this.sounds.PlaySound(this.sounds.clips[14]);
					}
				}
				if (raycastHit2D.collider.gameObject.name == "Lamp" && this.zoom.isZoom)
				{
					if (this.inventoryManager.selectedGameObject == null)
					{
						return;
					}
					if (this.inventoryManager.selectedGameObject.name == "Lightbulb")
					{
						this.chat.gameObject.SetActive(true);
						this.chat.GetComponentInChildren<TextMeshProUGUI>().text = "There is a symbol!";
						raycastHit2D.collider.gameObject.GetComponent<SpriteRenderer>().sprite = this.sprites[7];
						raycastHit2D.collider.gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
						this.inventoryManager.DeleteItem(this.inventoryManager.selectedGameObject);
						this.inventoryManager.selectedGameObject = null;
						this.sounds.PlaySound(this.sounds.clips[11]);
					}
				}
				if (raycastHit2D.collider.gameObject.name == "Lantern" && this.zoom.isZoom)
				{
					if (this.inventoryManager.selectedGameObject == null || this.inventoryManager.selectedGameObject.name != "Glove")
					{
						this.chat.gameObject.SetActive(true);
						this.chat.GetComponentInChildren<TextMeshProUGUI>().text = "The lamp is too hot. I can't touch it";
						return;
					}
					if (this.inventoryManager.selectedGameObject.name == "Glove")
					{
						raycastHit2D.collider.gameObject.GetComponent<SpriteRenderer>().sprite = this.sprites[6];
						raycastHit2D.collider.gameObject.GetComponent<BoxCollider2D>().enabled = false;
						this.inventoryManager.DeleteItem(this.inventoryManager.selectedGameObject);
						this.inventoryManager.AddItem(raycastHit2D.collider.gameObject.transform.GetChild(0).gameObject);
						Object.Destroy(raycastHit2D.collider.gameObject.transform.GetChild(0).gameObject);
						this.inventoryManager.selectedGameObject = null;
						this.sounds.PlaySound(this.sounds.clips[11]);
					}
				}
				if (raycastHit2D.collider.gameObject.name == "BookPlace" && this.zoom.isZoom)
				{
					if (this.inventoryManager.selectedGameObject == null)
					{
						return;
					}
					if (this.inventoryManager.selectedGameObject.name == "Book")
					{
						raycastHit2D.collider.gameObject.GetComponent<SpriteRenderer>().sprite = this.sprites[4];
						raycastHit2D.collider.gameObject.transform.localScale = new Vector3(raycastHit2D.collider.gameObject.transform.localScale.x * 2f, raycastHit2D.collider.gameObject.transform.localScale.y * 2f, raycastHit2D.collider.gameObject.transform.localScale.z);
						raycastHit2D.collider.gameObject.GetComponent<BoxCollider2D>().enabled = false;
						this.inventoryManager.DeleteItem(this.inventoryManager.selectedGameObject);
						this.inventoryManager.selectedGameObject = null;
						this.libary.GetComponent<SpriteRenderer>().sprite = this.sprites[5];
						this.libary.GetComponent<BoxCollider2D>().enabled = false;
						this.libary.transform.GetChild(1).gameObject.SetActive(true);
						this.sounds.PlaySound(this.sounds.clips[0]);
					}
				}
				if (raycastHit2D.collider.gameObject.name == "Chest" && this.zoom.isZoom)
				{
					if (this.inventoryManager.selectedGameObject == null)
					{
						return;
					}
					if (this.inventoryManager.selectedGameObject.name == "SilverKey")
					{
						raycastHit2D.collider.gameObject.GetComponentInParent<SpriteRenderer>().sprite = this.sprites[0];
						raycastHit2D.collider.gameObject.GetComponent<BoxCollider2D>().enabled = false;
						raycastHit2D.collider.gameObject.transform.GetChild(0).gameObject.SetActive(true);
						this.inventoryManager.DeleteItem(this.inventoryManager.selectedGameObject);
						this.inventoryManager.selectedGameObject = null;
						this.sounds.PlaySound(this.sounds.clips[4]);
					}
				}
				if ((raycastHit2D.collider.gameObject.name == "Cupboard1" || raycastHit2D.collider.gameObject.name == "Cupboard2") && this.zoom.isZoom)
				{
					if (this.inventoryManager.selectedGameObject == null)
					{
						return;
					}
					if (this.inventoryManager.selectedGameObject.name == "Crowbar")
					{
						if (raycastHit2D.collider.gameObject.name == "Cupboard1")
						{
							raycastHit2D.collider.gameObject.GetComponentInParent<SpriteRenderer>().sprite = this.sprites[2];
						}
						else if (raycastHit2D.collider.gameObject.name == "Cupboard2")
						{
							raycastHit2D.collider.gameObject.GetComponentInParent<SpriteRenderer>().sprite = this.sprites[3];
							raycastHit2D.collider.gameObject.transform.localScale = new Vector3(raycastHit2D.collider.gameObject.transform.localScale.x * 1f, raycastHit2D.collider.gameObject.transform.localScale.y, raycastHit2D.collider.gameObject.transform.localScale.z);
						}
						raycastHit2D.collider.gameObject.GetComponent<BoxCollider2D>().enabled = false;
						raycastHit2D.collider.gameObject.transform.GetChild(0).gameObject.SetActive(true);
						this.crosbarCount++;
						if (this.crosbarCount == 2)
						{
							this.inventoryManager.DeleteItem(this.inventoryManager.selectedGameObject);
						}
						this.sounds.PlaySound(this.sounds.clips[7]);
					}
				}
				if (!this.zoom.isZoom)
				{
					this.zoom.ZoomCamera(raycastHit2D.collider.gameObject.transform);
					return;
				}
			}
			else if (raycastHit2D.collider != null && raycastHit2D.collider.gameObject.tag == "ChessPiece")
			{
				if (!this.puzzles.isOver && this.puzzles.chessTurn < 3)
				{
					this.currentChessPiece = raycastHit2D.collider.gameObject;
					this.chessPiecePrevPos = this.currentChessPiece.transform.position;
					if (this.currentChessPiece != this.prevChessPiece && this.prevChessPiece != null)
					{
						this.prevChessPiece.GetComponent<Chess>().CloseMoves();
					}
					this.currentChessPiece.GetComponent<Chess>().ActiveMoves();
					this.prevChessPiece = this.currentChessPiece;
					return;
				}
			}
			else if (raycastHit2D.collider != null && raycastHit2D.collider.gameObject.tag == "Moves")
			{
				if (!this.puzzles.isOver && this.puzzles.chessTurn < 3)
				{
					this.currentChessPiece.transform.position = new Vector3(raycastHit2D.collider.gameObject.transform.position.x, raycastHit2D.collider.gameObject.transform.position.y, this.currentChessPiece.transform.position.z);
					this.currentChessPiece.GetComponent<Chess>().CloseMoves();
					this.sounds.PlaySound(this.sounds.clips[3]);
					this.puzzles.ChessPuzzle(this.currentChessPiece, this.chessPiecePrevPos);
					return;
				}
			}
			else if (raycastHit2D.collider != null && raycastHit2D.collider.gameObject.name == "ChessBoard" && !this.puzzles.isOver && this.puzzles.chessTurn < 3 && this.currentChessPiece != null)
			{
				if (this.prevChessPiece != null)
				{
					this.prevChessPiece.GetComponent<Chess>().CloseMoves();
				}
				if (this.currentChessPiece != null)
				{
					this.puzzles.ChessPuzzle(this.currentChessPiece, this.chessPiecePrevPos);
					this.currentChessPiece.GetComponent<Chess>().isMove = false;
				}
			}
		}
	}
}

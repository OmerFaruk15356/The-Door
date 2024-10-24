

using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Puzzles : MonoBehaviour
{
  [SerializeField]
  private List<GameObject> puzzles;
  [SerializeField]
  private List<Sprite> items;
  [SerializeField]
  private List<TextMeshProUGUI> numbers;
  [SerializeField]
  private TextMeshProUGUI inputText;
  [SerializeField]
  private List<Sprite> symbols;
  [SerializeField]
  private List<Button> symbolButtons;
  [SerializeField]
  private List<TextMeshProUGUI> keyPuzzleText;
  [SerializeField]
  private GameObject keyPuzzle;
  [SerializeField]
  private GameObject paint1;
  [SerializeField]
  private GameObject paint2;
  [SerializeField]
  private GameObject goldenKey;
  [SerializeField]
  private List<GameObject> chessPieces;
  [SerializeField]
  private List<GameObject> trueMove;
  [SerializeField]
  private GameObject chessChoice;
  private Zoom zoom;
  private Sounds sounds;
  private Mouse mouse;
  private Vector3 pawnPrevPos;
  private GameObject pawn;
  public int chessTurn = 1;
  private int listNumber;
  private int keyTextNumber;
  public bool isOver;

  private void Awake()
  {
    this.mouse = Object.FindObjectOfType<Mouse>();
    this.sounds = Object.FindObjectOfType<Sounds>();
    this.zoom = Object.FindObjectOfType<Zoom>();
  }

  public void WordPuzzle()
  {
		string text = this.inputText.text.Trim('​');
		this.sounds.PlaySound(this.sounds.clips[10]);
		if (text.Equals("Time"))
		{
			this.puzzles[0].GetComponent<SpriteRenderer>().sprite = this.items[0];
			this.puzzles[0].name = "Book";
			this.puzzles[0].transform.position = new Vector3(45.5f, 4.7f, -2f);
			this.puzzles[0].transform.localScale = new Vector3(this.puzzles[0].transform.localScale.x * 2f, this.puzzles[0].transform.localScale.y * 2f, this.puzzles[0].transform.localScale.z);
			this.zoom.ZoomOut();
			return;
		}
		this.sounds.PlaySound(this.sounds.clips[17]);
  }

  public void NumberPuzzle(TextMeshProUGUI value)
  {
		int num = int.Parse(value.text);
		this.sounds.PlaySound(this.sounds.clips[1]);
		if (num < 9)
		{
			num++;
		}
		else
		{
			num = 0;
		}
		value.text = num.ToString();
		if (this.numbers[0].text == "1" && this.numbers[1].text == "2" && this.numbers[2].text == "1" && this.numbers[3].text == "5")
		{
			this.puzzles[1].GetComponent<SpriteRenderer>().sprite = this.items[1];
			this.puzzles[1].name = "Glove";
			this.puzzles[1].transform.position = new Vector3(48.25f, 4.7f, -2f);
			this.sounds.PlaySound(this.sounds.clips[10]);
			this.zoom.ZoomOut();
			this.zoom.checkPuzzle();
		}
  }

  public void SymbolPuzzle(Button button)
  {
		button.image.sprite = this.symbols[this.listNumber];
		this.sounds.PlaySound(this.sounds.clips[1]);
		if (this.listNumber < 8)
		{
			this.listNumber++;
		}
		else
		{
			this.listNumber = 0;
		}
		if (this.symbolButtons[0].image.sprite == this.symbols[0] && this.symbolButtons[1].image.sprite == this.symbols[5] && this.symbolButtons[2].image.sprite == this.symbols[1])
		{
			this.puzzles[2].GetComponent<SpriteRenderer>().sprite = this.items[2];
			this.puzzles[2].name = "Paint1Part";
			this.puzzles[2].transform.rotation = Quaternion.Euler(32.868f, -11.64f, 18.762f);
			this.puzzles[2].transform.position = new Vector3(35.475f, -0.705f, -15.2f);
			this.puzzles[2].transform.localScale = new Vector3(this.puzzles[2].transform.localScale.x * 0.5f, this.puzzles[2].transform.localScale.y * 0.5f, this.puzzles[2].transform.localScale.z);
			this.sounds.PlaySound(this.sounds.clips[10]);
			this.zoom.ZoomOut();
			this.zoom.checkPuzzle();
		}
  }

  public void KeyPuzzle(string key)
  {
		this.keyPuzzleText[this.keyTextNumber].text = key;
		this.sounds.PlaySound(this.sounds.clips[1]);
		this.keyTextNumber++;
		if (this.keyTextNumber == 5)
		{
			if (this.keyPuzzleText[0].text == "U" && this.keyPuzzleText[1].text == "L" && this.keyPuzzleText[2].text == "L" && this.keyPuzzleText[3].text == "R" && this.keyPuzzleText[4].text == "D")
			{
				this.keyPuzzle.GetComponent<BoxCollider2D>().enabled = false;
				this.puzzles[3].SetActive(true);
				this.puzzles[3].transform.rotation = Quaternion.Euler(58f, 0f, 0f);
				this.puzzles[3].transform.position = new Vector3(53.5f, 2.37f, -17f);
				this.sounds.PlaySound(this.sounds.clips[10]);
				this.zoom.ZoomOut();
				this.zoom.checkPuzzle();
				return;
			}
			foreach (TextMeshProUGUI textMeshProUGUI in this.keyPuzzleText)
			{
				textMeshProUGUI.text = " ";
			}
			this.sounds.PlaySound(this.sounds.clips[17]);
			this.keyTextNumber = 0;
		}
  }

  public void CheckPaints()
  {
		if (this.paint1.activeSelf && this.paint2.activeSelf)
		{
			this.goldenKey.GetComponent<Test>().StartDown();
		}
  }

  public void ChessPuzzle(GameObject chessPiece, Vector3 pos)
  {
		if (this.chessTurn != 1)
		{
			if (this.chessTurn == 2)
			{
				if (this.chessPieces[1].transform.position.x == this.trueMove[1].transform.position.x && this.chessPieces[1].transform.position.y == this.trueMove[1].transform.position.y)
				{
					this.chessTurn++;
					this.chessChoice.SetActive(true);
					this.pawn = chessPiece;
					this.pawnPrevPos = pos;
					this.mouse.currentChessPiece = null;
					return;
				}
				chessPiece.transform.position = pos;
			}
			return;
		}
		if (this.chessPieces[0].transform.position.x == this.trueMove[0].transform.position.x && this.chessPieces[0].transform.position.y == this.trueMove[0].transform.position.y)
		{
			this.chessTurn++;
			this.chessPieces[2].transform.position = new Vector3(this.chessPieces[2].transform.position.x, this.chessPieces[2].transform.position.y + 1.5f, this.chessPieces[2].transform.position.z);
			this.mouse.currentChessPiece = null;
			return;
		}
		chessPiece.transform.position = pos;
  }

  public void ChessChoice(bool state)
  {
		if (state)
		{
			this.isOver = true;
			this.puzzles[4].GetComponent<BoxCollider2D>().enabled = false;
			this.puzzles[5].SetActive(true);
			this.keyPuzzleText[5].gameObject.SetActive(true);
			this.keyPuzzleText[5].text = "YOU WIN!";
			this.pawn.GetComponent<SpriteRenderer>().sprite = this.items[3];
			this.chessChoice.SetActive(false);
			return;
		}
		this.chessChoice.SetActive(false);
		this.pawn.transform.position = this.pawnPrevPos;
  }
}

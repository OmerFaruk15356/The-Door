
using System.Collections.Generic;
using UnityEngine;


public class Chess : MonoBehaviour
{
  [SerializeField]
  private List<GameObject> Turn1;
  [SerializeField]
  private List<GameObject> Turn2;
  private Puzzles puzzles;
  public bool isMove;

  private void FixedUpdate()
  {
    if (!this.isMove)
      return;
    Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    base.gameObject.transform.position = new Vector3(vector.x, vector.y, base.gameObject.transform.position.z);
  }

  private void Awake() => this.puzzles = Object.FindObjectOfType<Puzzles>();

  public void ActiveMoves()
  {
    if (this.puzzles.chessTurn == 1)
    {
      foreach (GameObject gameObject in this.Turn1)
        gameObject.SetActive(true);
    }
    else if (this.puzzles.chessTurn == 2)
    {
      foreach (GameObject gameObject in this.Turn2)
        gameObject.SetActive(true);
    }
    this.isMove = true;
    base.gameObject.GetComponent<BoxCollider2D>().enabled = false;
  }

  public void CloseMoves()
  {
    if (this.puzzles.chessTurn == 1)
    {
      foreach (GameObject gameObject in this.Turn1)
      {
        if (gameObject.activeSelf)
          gameObject.SetActive(false);
      }
    }
    else if (this.puzzles.chessTurn == 2)
    {
      foreach (GameObject gameObject in this.Turn2)
      {
        if (gameObject.activeSelf)
          gameObject.SetActive(false);
      }
    }
    this.isMove = false;
    base.gameObject.GetComponent<BoxCollider2D>().enabled = true;
  }
}

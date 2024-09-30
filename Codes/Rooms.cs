

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Rooms : MonoBehaviour
{
  public int currentRoom = 1;
  [SerializeField]
  private List<Button> buttons;
  [SerializeField]
  private List<GameObject> rooms;
	public void ChangeRoom(int value)
	{
		foreach (GameObject gameObject in this.rooms)
		{
			gameObject.SetActive(false);
		}
		this.currentRoom += value;
		this.rooms[this.currentRoom].SetActive(true);
		if (this.currentRoom == 0)
		{
			this.buttons[0].gameObject.SetActive(false);
			return;
		}
		if (this.currentRoom == 3)
		{
			this.buttons[1].gameObject.SetActive(false);
			return;
		}
		this.buttons[0].gameObject.SetActive(true);
		this.buttons[1].gameObject.SetActive(true);
	}
}

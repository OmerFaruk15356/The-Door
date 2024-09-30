

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
    foreach (GameObject room in this.rooms)
      room.SetActive(false);
    this.currentRoom += value;
    this.rooms[this.currentRoom].SetActive(true);
    if (this.currentRoom == 0)
      ((Component) this.buttons[0]).gameObject.SetActive(false);
    else if (this.currentRoom == 3)
    {
      ((Component) this.buttons[1]).gameObject.SetActive(false);
    }
    else
    {
      ((Component) this.buttons[0]).gameObject.SetActive(true);
      ((Component) this.buttons[1]).gameObject.SetActive(true);
    }
  }
}

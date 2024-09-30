

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class InventoryManager : MonoBehaviour
{
  [SerializeField]
  private List<Button> items;
  public GameObject selectedGameObject;

  public void AddItem(GameObject gameObject)
  {
    foreach (Button button in this.items)
    {
      if (((Object) button).name == ((Object) gameObject).name)
        break;
      if (!((Component) button).gameObject.activeSelf)
      {
        ((Component) button).gameObject.SetActive(true);
        ((Selectable) button).image.sprite = gameObject.GetComponent<SpriteRenderer>().sprite;
        ((Object) button).name = ((Object) gameObject).name;
        break;
      }
    }
  }

  public void DeleteItem(GameObject gameObject)
  {
    foreach (Button button in this.items)
    {
      if (((Object) button).name == ((Object) gameObject).name)
      {
        ((Component) button).gameObject.SetActive(false);
        break;
      }
    }
  }

  public void SelectItem(GameObject gameObject) => this.selectedGameObject = gameObject;
}

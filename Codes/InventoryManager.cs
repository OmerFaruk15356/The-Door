

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
			if (button.name == gameObject.name)
			{
				break;
			}
			if (!button.gameObject.activeSelf)
			{
				button.gameObject.SetActive(true);
				button.image.sprite = gameObject.GetComponent<SpriteRenderer>().sprite;
				button.name = gameObject.name;
				break;
			}
		}
  }

  public void DeleteItem(GameObject gameObject)
  {
		foreach (Button button in this.items)
		{
			if (button.name == gameObject.name)
			{
				button.gameObject.SetActive(false);
				break;
			}
		}
  }

  public void SelectItem(GameObject gameObject) => this.selectedGameObject = gameObject;
}

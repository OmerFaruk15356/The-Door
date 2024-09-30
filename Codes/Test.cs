

using System.Collections;
using UnityEngine;


public class Test : MonoBehaviour
{
  public float initialVelocity = 0.1f;
  public float maxVelocity = 2f;
  public float acceleration = 0.01f;
  public float duration = 3f;

  public void StartDown() => this.StartCoroutine(this.FallCoroutine());

	private IEnumerator FallCoroutine()
	{
		float elapsedTime = 0f;
		float currentVelocity = this.initialVelocity;
		while (elapsedTime < this.duration)
		{
			currentVelocity += this.acceleration * Time.deltaTime;
			currentVelocity = Mathf.Clamp(currentVelocity, 0f, this.maxVelocity);
			base.transform.Translate(Vector3.down * currentVelocity * Time.deltaTime);
			elapsedTime += Time.deltaTime;
			yield return null;
		}
		yield break;
	}
}

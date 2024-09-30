

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
    Test test = this;
    float elapsedTime = 0.0f;
    float currentVelocity = test.initialVelocity;
    while ((double) elapsedTime < (double) test.duration)
    {
      currentVelocity += test.acceleration * Time.deltaTime;
      currentVelocity = Mathf.Clamp(currentVelocity, 0.0f, test.maxVelocity);
      ((Component) test).transform.Translate(Vector3.op_Multiply(Vector3.op_Multiply(Vector3.down, currentVelocity), Time.deltaTime));
      elapsedTime += Time.deltaTime;
      yield return (object) null;
    }
  }
}

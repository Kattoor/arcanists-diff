

using UnityEngine;

#nullable disable
public class HideInSeconds : MonoBehaviour
{
  public float seconds = 0.2f;
  private float cur;

  private void Update()
  {
    this.cur += Time.deltaTime;
    if ((double) this.cur <= (double) this.seconds)
      return;
    this.gameObject.SetActive(false);
    this.cur = 0.0f;
  }
}

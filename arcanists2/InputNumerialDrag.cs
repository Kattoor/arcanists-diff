

using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

#nullable disable
[RequireComponent(typeof (TMP_InputField))]
public class InputNumerialDrag : MonoBehaviour, IDragHandler, IEventSystemHandler, IBeginDragHandler
{
  private TMP_InputField input;
  public int max = (int) sbyte.MaxValue;
  public int min = (int) sbyte.MinValue;
  private int startValue;
  private Vector2 startPos;

  private void Awake() => this.input = this.GetComponent<TMP_InputField>();

  public void OnBeginDrag(PointerEventData d)
  {
    this.startPos = d.position;
    this.startValue = 0;
    int.TryParse(this.input.text, out this.startValue);
  }

  public void OnDrag(PointerEventData d)
  {
    int result = 0;
    if (!int.TryParse(this.input.text, out result))
      return;
    this.input.text = Mathf.Clamp((int) d.position.x - (int) this.startPos.x + this.startValue, this.min, this.max).ToString();
    this.input.onEndEdit?.Invoke(this.input.text);
  }
}

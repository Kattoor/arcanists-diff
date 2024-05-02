

using UnityEngine;

#nullable disable
public class UIAlwaysOnTop : MonoBehaviour
{
  private void Update()
  {
    if (this.transform.GetSiblingIndex() == this.transform.parent.childCount - 1)
      return;
    this.transform.SetAsLastSibling();
  }
}



using UnityEngine;

#nullable disable
internal class StandardBoard : IBoard
{
  public void SetText(string str) => GUIUtility.systemCopyBuffer = str;

  public string GetText() => GUIUtility.systemCopyBuffer;
}

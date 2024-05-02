

using UnityEngine;

#nullable disable
public class RectDimensionsChanged : MonoBehaviour
{
  private void OnRectTransformDimensionsChange()
  {
    RectTransform transform = (RectTransform) this.transform;
    RectTransform parent = (RectTransform) transform.parent;
    Vector3[] fourCornersArray1 = new Vector3[4];
    Vector3[] fourCornersArray2 = fourCornersArray1;
    parent.GetWorldCorners(fourCornersArray2);
    Vector3 vector3_1 = fourCornersArray1[0];
    Vector3 vector3_2 = fourCornersArray1[2];
    Vector3 vector3_3 = vector3_2 - vector3_1;
    transform.GetWorldCorners(fourCornersArray1);
    Vector3 vector3_4 = fourCornersArray1[0];
    Vector3 vector3_5 = fourCornersArray1[2];
    Vector3 vector3_6 = vector3_5 - vector3_4;
    Vector3 position = transform.position;
    Vector3 vector3_7 = position - vector3_4;
    Vector3 vector3_8 = vector3_5 - position;
    position.x = (double) vector3_6.x < (double) vector3_3.x ? Mathf.Clamp(position.x, vector3_1.x + vector3_7.x, vector3_2.x - vector3_8.x) : Mathf.Clamp(position.x, vector3_2.x - vector3_8.x, vector3_1.x + vector3_7.x);
    position.y = (double) vector3_6.y < (double) vector3_3.y ? Mathf.Clamp(position.y, vector3_1.y + vector3_7.y, vector3_2.y - vector3_8.y) : Mathf.Clamp(position.y, vector3_2.y - vector3_8.y, vector3_1.y + vector3_7.y);
    transform.position = position;
  }
}



using System;
using TMPro;
using UnityEngine;

#nullable disable
public class UIClanHeader : MonoBehaviour
{
  public TMP_Text txtName;
  public TMP_Text txtMemberCount;
  public TMP_Text txtDescription;
  public TMP_Text txtDate;

  public void Setup(Clan c)
  {
    this.txtName.text = c.name;
    this.txtMemberCount.text = string.Format("({0})", (object) c.clientMemberCount);
    this.txtDescription.text = c.description;
    this.txtDate.text = DateTime.FromBinary(c.creationDate).ToShortDateString();
  }
}

// Decompiled with JetBrains decompiler
// Type: UIClanHeader
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

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

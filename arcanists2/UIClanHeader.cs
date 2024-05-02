// Decompiled with JetBrains decompiler
// Type: UIClanHeader
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

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

﻿// Decompiled with JetBrains decompiler
// Type: Educative.CPanCamera
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

#nullable disable
namespace Educative
{
  public class CPanCamera : Command
  {
    public Point point_area = new Point();
    public bool bool_instant;
    public bool bool_interuptable = true;

    public CPanCamera() => this.type = Command.Type.PanCamera;
  }
}

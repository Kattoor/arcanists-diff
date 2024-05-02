// Decompiled with JetBrains decompiler
// Type: QuickCorrection
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

#nullable disable
public class QuickCorrection
{
  public static QuickCorrection Serialize(ZGame game, myBinaryWriter w)
  {
    QuickCorrection quickCorrection = new QuickCorrection();
    quickCorrection._Serialize(game, w);
    return quickCorrection;
  }

  public static QuickCorrection Deserialize(ZGame game, myBinaryReader r)
  {
    QuickCorrection quickCorrection = new QuickCorrection();
    quickCorrection._Deserialize(game, r);
    return quickCorrection;
  }

  private void _Serialize(ZGame game, myBinaryWriter w)
  {
  }

  private void _Deserialize(ZGame game, myBinaryReader r)
  {
  }

  public class Player
  {
    public MyLocation position;
    public int health;
    public MyLocation velocity;
  }

  public class Effector
  {
    public MyLocation position;
    public bool active;
  }
}

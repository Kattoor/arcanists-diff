// Decompiled with JetBrains decompiler
// Type: QuickCorrection
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

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

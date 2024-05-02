// Decompiled with JetBrains decompiler
// Type: Educative.CCreateIndicator
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

#nullable disable
namespace Educative
{
  public class CCreateIndicator : Command
  {
    public CCreateIndicator.Kinds kind;
    public float lifetime = -1f;
    public Point point_point = new Point();
    public int radius = 256;
    public float angle;
    public string hexColor = "#ffffffff";

    public CCreateIndicator() => this.type = Command.Type.CreateIndicator;

    public enum Kinds
    {
      Area,
      Point,
      MovingArrow,
      Arrow,
      Meter_Fill,
      Meter_Flash,
      Meter_Bolt,
      Meter_Target,
    }
  }
}

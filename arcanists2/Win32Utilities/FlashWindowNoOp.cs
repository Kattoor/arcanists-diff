// Decompiled with JetBrains decompiler
// Type: Win32Utilities.FlashWindowNoOp
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

#nullable disable
namespace Win32Utilities
{
  internal class FlashWindowNoOp : IFlashWindow
  {
    public bool Flash() => false;

    public bool Flash(uint count) => false;

    public bool Start() => false;

    public bool Stop() => false;
  }
}

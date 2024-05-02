// Decompiled with JetBrains decompiler
// Type: HardShellStudios.InputManager.hardKey
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
namespace HardShellStudios.InputManager
{
  public class hardKey : IComparable<hardKey>
  {
    public string keyName;
    public KeyCode keyInput;
    public KeyCode keyInput2;
    public int keyWheelState;
    public int keyWheelState2;
    public float keyValue;
    public bool saveKey;

    public hardKey(
      string keyNameGIVE,
      KeyCode inputKeyGIVE,
      KeyCode inputKey2GIVE,
      int keyWheelStateGIVE,
      int keyWheelState2GIVE,
      bool saveKeyGIVE)
    {
      this.keyName = keyNameGIVE;
      this.keyInput = inputKeyGIVE;
      this.keyInput2 = inputKey2GIVE;
      this.keyWheelState = keyWheelStateGIVE;
      this.keyWheelState2 = keyWheelState2GIVE;
      this.keyValue = 0.0f;
      this.saveKey = saveKeyGIVE;
    }

    public int CompareTo(hardKey other) => 1;

    public enum controllerMap
    {
      None,
      Square,
      Cross,
      Circle,
      Triangle,
      L1,
      R1,
      L2,
      R2,
      Share,
      Options,
      LeftStick,
      RightStick,
      PSHome,
      Trackpad,
      DPadUp,
      DPadDown,
      DPadLeft,
      DPadRight,
    }
  }
}

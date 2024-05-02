// Decompiled with JetBrains decompiler
// Type: Educative.Command
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
namespace Educative
{
  public class Command : IJason
  {
    public string Name = "-";
    public int identifier;
    public Command.Type type;
    public Command.TabIndex tabIndex;
    internal int state;

    public static Command From(Command.Type t)
    {
      switch (t)
      {
        case Command.Type.Info:
          return (Command) new CInfo();
        case Command.Type.Move:
          return (Command) new CMove();
        case Command.Type.CreateEntity:
          return (Command) new CCreateEntity();
        case Command.Type.AddSpell:
          return (Command) new CAddSpell();
        case Command.Type.LockSpell:
          return (Command) new CLockSpell();
        case Command.Type.CastSpell:
          return (Command) new CCastSpell();
        case Command.Type.EnableMovement:
          return (Command) new CEnableMovement();
        case Command.Type.WaitForDeath:
          return (Command) new CWaitForDeath();
        case Command.Type.WaitForSpellCast:
          return (Command) new CWaitForSpellCast();
        case Command.Type.WaitForMoveToArea:
          return (Command) new CWaitForMoveToArea();
        case Command.Type.WaitForSpellsToFinish:
          return (Command) new CWaitForSpellsToFinish();
        case Command.Type.WaitTime:
          return (Command) new CWaitTime();
        case Command.Type.PanCamera:
          return (Command) new CPanCamera();
        case Command.Type.Win:
          return (Command) new CWin();
        case Command.Type.IfGoTo:
          return (Command) new CIf();
        case Command.Type.SetGameVariable:
          return (Command) new CSetGameVariable();
        case Command.Type.CreateIndicator:
          return (Command) new CCreateIndicator();
        case Command.Type.ClearIndicators:
          return (Command) new CClearIndicators();
        case Command.Type.WaitWhileTrue:
          return (Command) new CWaitWhileTrue();
        case Command.Type.LoopWhileTrue:
          return (Command) new CLoopWhileTrue();
        case Command.Type.GoTo:
          return (Command) new CGoTo();
        case Command.Type.Armageddon:
          return (Command) new CArmageddon();
        case Command.Type.If:
          return (Command) new CIfThen();
        case Command.Type.IntitializeVariables:
          return (Command) new CIntitializeVarible();
        case Command.Type.Nothing:
          return (Command) new CNothing();
        case Command.Type.NextTurn:
          return (Command) new CNextTurn();
        case Command.Type.VariableMaths:
          return (Command) new CAddToVarible();
        case Command.Type.Script:
          return (Command) new CScript();
        default:
          Debug.LogError((object) ("Not implemented: " + (object) t));
          return (Command) null;
      }
    }

    public enum TabIndex
    {
      Zero,
      One,
      Two,
      Three,
      Four,
      Five,
    }

    public enum Type
    {
      Info,
      Move,
      CreateEntity,
      AddSpell,
      LockSpell,
      CastSpell,
      EnableMovement,
      WaitForDeath,
      WaitForSpellCast,
      WaitForMoveToArea,
      WaitForSpellsToFinish,
      WaitTime,
      PanCamera,
      Win,
      IfGoTo,
      SetGameVariable,
      CreateIndicator,
      ClearIndicators,
      WaitWhileTrue,
      LoopWhileTrue,
      GoTo,
      Armageddon,
      If,
      IntitializeVariables,
      Nothing,
      NextTurn,
      VariableMaths,
      Script,
    }
  }
}

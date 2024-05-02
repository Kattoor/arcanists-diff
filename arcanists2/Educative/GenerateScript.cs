// Decompiled with JetBrains decompiler
// Type: Educative.GenerateScript
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using MoonSharp.Interpreter;
using MyPolling;
using System;
using TMPro;
using UnityEngine;

#nullable disable
namespace Educative
{
  public class GenerateScript
  {
    public static Script GetScript(Tutorial t = null)
    {
      UserData.RegisterProxyType<ContainerGame, ZGame>((Func<ZGame, ContainerGame>) (r => new ContainerGame(r, t)));
      UserData.RegisterProxyType<ContainerPlayer, ZPerson>((Func<ZPerson, ContainerPlayer>) (r => new ContainerPlayer(r)));
      UserData.RegisterProxyType<ContainerCreature, ZCreature>((Func<ZCreature, ContainerCreature>) (r => new ContainerCreature(r)));
      UserData.RegisterProxyType<ContainerEffector, ZEffector>((Func<ZEffector, ContainerEffector>) (r => new ContainerEffector(r)));
      UserData.RegisterProxyType<ContainerIndicator, GameObject>((Func<GameObject, ContainerIndicator>) (r => new ContainerIndicator(r)));
      UserData.RegisterProxyType<ContainerSpell, SpellSlot>((Func<SpellSlot, ContainerSpell>) (r => new ContainerSpell(r)));
      UserData.RegisterProxyType<ContainerTower, ZTower>((Func<ZTower, ContainerTower>) (r => new ContainerTower(r)));
      Script.GlobalOptions.RethrowExceptionNested = true;
      UserData.RegisterType<Point>();
      UserData.RegisterType<Summon>();
      UserData.RegisterType<UIElement>();
      UserData.RegisterType<KeyCode>();
      UserData.RegisterType<SpellEnum>();
      UserData.RegisterType<CastType>();
      UserData.RegisterType<DamageType>();
      UserData.RegisterType<BookOf>();
      UserData.RegisterType<Anchor>();
      UserData.RegisterType<HorizontalAlignmentOptions>();
      UserData.RegisterType<VerticalAlignmentOptions>();
      UserData.RegisterType<IndicatorKind>();
      UserData.RegisterType<MapEnum>();
      UserData.RegisterType<EffectorType>();
      UserData.RegisterType<TowerType>();
      UserData.RegisterType<CreatureType>();
      UserData.RegisterType<CreatureRace>();
      UserData.RegisterType<ExplosionCutout>();
      UserData.RegisterType<MyPoll>();
      UserData.RegisterType<MyPoll.Item>();
      UserData.RegisterType<MyPoll.Answer>();
      UserData.RegisterType<LuaColor>();
      return new Script(CoreModules.Preset_SoftSandbox)
      {
        Globals = {
          [(object) "game"] = (object) new ContainerGame(Client.game, t),
          [(object) "point"] = (object) new Func<int, int, Point>(Point.construct),
          [(object) "color"] = (object) new Func<byte, byte, byte, byte, LuaColor>(LuaColor.construct),
          [(object) "colorHex"] = (object) new Func<string, LuaColor>(LuaColor.construct),
          [(object) "createUI"] = (object) new Func<Script, UIElement, UIElement>(UIElement.createUI),
          [(object) "createInput"] = (object) new Func<Script, UIElement, string, bool, bool, bool, UIElement>(UIElement.createInput),
          [(object) "summon"] = (object) new Func<string, Point, int, Table, bool, bool, string, Table, Table, BookOf, Summon>(Summon.construct),
          [(object) "spell"] = (object) new Func<string, ContainerSpell>(ContainerGame.getSpell),
          [(object) "createPoll"] = (object) new Func<string, MyPoll>(MyPoll.construct),
          [(object) "KeyCode"] = (object) UserData.CreateStatic<KeyCode>(),
          [(object) "SpellEnum"] = (object) UserData.CreateStatic<SpellEnum>(),
          [(object) "CastType"] = (object) UserData.CreateStatic<CastType>(),
          [(object) "DamageType"] = (object) UserData.CreateStatic<DamageType>(),
          [(object) "Book"] = (object) UserData.CreateStatic<BookOf>(),
          [(object) "Anchor"] = (object) UserData.CreateStatic<Anchor>(),
          [(object) "AlignHorizontal"] = (object) UserData.CreateStatic<HorizontalAlignmentOptions>(),
          [(object) "AlignVertical"] = (object) UserData.CreateStatic<VerticalAlignmentOptions>(),
          [(object) "IndicatorKind"] = (object) UserData.CreateStatic<IndicatorKind>(),
          [(object) "MapEnum"] = (object) UserData.CreateStatic<MapEnum>(),
          [(object) "EffectorType"] = (object) UserData.CreateStatic<EffectorType>(),
          [(object) "TowerType"] = (object) UserData.CreateStatic<TowerType>(),
          [(object) "CreatureType"] = (object) UserData.CreateStatic<CreatureType>(),
          [(object) "CreatureRace"] = (object) UserData.CreateStatic<CreatureRace>(),
          [(object) "ExplosionCutout"] = (object) UserData.CreateStatic<ExplosionCutout>()
        },
        Options = {
          CheckThreadAccess = false
        }
      };
    }
  }
}

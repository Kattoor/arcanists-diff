// Decompiled with JetBrains decompiler
// Type: Educative.CScript
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using MoonSharp;
using MoonSharp.Interpreter;
using MoonSharp.Interpreter.Loaders;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

#nullable disable
namespace Educative
{
  public class CScript : Command
  {
    public string code = "function main()\n\t--Some logic goes here\n\t--can use coroutine.yield(0) for multi-frame logic\nend";
    public bool bool_debug;

    public Script GetScript(Tutorial t)
    {
      Dictionary<string, string> scriptToCodeMap = new Dictionary<string, string>();
      foreach (TextAsset textAsset in Enumerable.OfType<TextAsset>(Resources.LoadAll("Lua", typeof (TextAsset))))
        scriptToCodeMap.Add(textAsset.name, textAsset.text);
      Script.DefaultOptions.ScriptLoader = (IScriptLoader) new UnityAssetsScriptLoader(scriptToCodeMap);
      Bridge.Initialize();
      Script script = GenerateScript.GetScript(t);
      script.Options.DebugPrint = !t.debugLog ? (Action<string>) (s => { }) : (Action<string>) (s =>
      {
        if ((UnityEngine.Object) ChatBox.Instance == (UnityEngine.Object) null)
          Controller.Instance.ShowChatBox(false);
        ChatBox.Instance?.NewChatMsg(s, (Color) ColorScheme.GetColor(Global.ColorSystem));
      });
      script.DoString(this.code);
      if (this.bool_debug)
      {
        int num = t.debugLog ? 1 : 0;
      }
      return script;
    }

    public CScript() => this.type = Command.Type.Script;
  }
}

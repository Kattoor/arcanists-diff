// Decompiled with JetBrains decompiler
// Type: SpellOverrides
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using Educative;
using Newtonsoft.Json;
using Newtonsoft.Json.Bson;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

#nullable disable
[Serializable]
public class SpellOverrides
{
  public SpellOverrides.AGlobal global = new SpellOverrides.AGlobal();
  public List<SpellOverrides.ASpell> listSpells = new List<SpellOverrides.ASpell>();
  public List<SpellOverrides.ACreature> listCreatures = new List<SpellOverrides.ACreature>();
  public static byte[] ServerBytes;
  public static SpellOverrides serverObj;
  public static SpellOverrides activeObj;

  public static string EXT => ".spellOverrides";

  public static string PATH
  {
    get
    {
      string persistentDataPath = Application.persistentDataPath;
      char directorySeparatorChar = Path.DirectorySeparatorChar;
      string str1 = directorySeparatorChar.ToString();
      directorySeparatorChar = Path.DirectorySeparatorChar;
      string str2 = directorySeparatorChar.ToString();
      return persistentDataPath + str1 + nameof (SpellOverrides) + str2;
    }
  }

  public static string SERVER_PATH => SpellOverrides.PATH + "_Server" + SpellOverrides.EXT;

  public static string OLD => SpellOverrides.PATH + "_outdated" + SpellOverrides.EXT;

  public void SaveToFile(string name)
  {
    if (name.StartsWith(SpellOverrides.PATH))
      name = name.Substring(SpellOverrides.PATH.Length);
    string path = SpellOverrides.PATH;
    Global.CheckDirectoryExists(path, name);
    File.WriteAllBytes(path + name + (name.EndsWith(SpellOverrides.EXT) ? "" : SpellOverrides.EXT), this.ToBytes());
  }

  public static SpellOverrides FromFile(string path)
  {
    try
    {
      if (File.Exists(path))
        return SpellOverrides.FromBytes(File.ReadAllBytes(path));
    }
    catch (Exception ex)
    {
      Debug.LogError((object) ex);
    }
    return (SpellOverrides) null;
  }

  public void SaveServer() => this.SaveToFile(SpellOverrides.SERVER_PATH);

  public static SpellOverrides LoadServer()
  {
    SpellOverrides spellOverrides = SpellOverrides.FromFile(SpellOverrides.SERVER_PATH);
    SpellOverrides.serverObj = spellOverrides;
    SpellOverrides.ServerBytes = spellOverrides == null ? (byte[]) null : spellOverrides.ToBytes();
    return spellOverrides;
  }

  public static SpellOverrides FromJson(JObject j)
  {
    return j.ToObject<SpellOverrides>(new JsonSerializer()
    {
      TypeNameHandling = TypeNameHandling.Auto,
      Binder = (SerializationBinder) new CustomJsonSerializationBinder("Educative")
    });
  }

  public JObject ToJson()
  {
    return JObject.FromObject((object) this, new JsonSerializer()
    {
      TypeNameHandling = TypeNameHandling.Auto
    });
  }

  public byte[] ToBytes()
  {
    using (MemoryStream memoryStream = new MemoryStream())
    {
      using (BsonWriter bsonWriter = new BsonWriter((Stream) memoryStream))
        new JsonSerializer()
        {
          TypeNameHandling = TypeNameHandling.Auto,
          Binder = ((SerializationBinder) new CustomJsonSerializationBinder("Educative"))
        }.Serialize((JsonWriter) bsonWriter, (object) this);
      return memoryStream.ToArray();
    }
  }

  public static SpellOverrides FromBytes(byte[] sent)
  {
    using (MemoryStream memoryStream = new MemoryStream(sent))
    {
      using (BsonReader reader = new BsonReader((Stream) memoryStream))
        return new JsonSerializer()
        {
          TypeNameHandling = TypeNameHandling.Auto,
          Binder = ((SerializationBinder) new CustomJsonSerializationBinder("Educative"))
        }.Deserialize<SpellOverrides>((JsonReader) reader);
    }
  }

  public static SpellOverrides HandleMsg(myBinaryReader r, bool setAsServerObj)
  {
    int num = r.ReadBoolean() ? 1 : 0;
    byte[] sent = (byte[]) null;
    if (num != 0)
      sent = r.ReadBytes();
    if (!setAsServerObj)
      return sent == null ? (SpellOverrides) null : SpellOverrides.FromBytes(sent);
    SpellOverrides.serverObj = sent != null ? SpellOverrides.FromBytes(sent) : (SpellOverrides) null;
    Inert.Instance?.Apply(SpellOverrides.serverObj);
    if (SpellOverrides.serverObj == null)
    {
      if (File.Exists(SpellOverrides.SERVER_PATH))
        File.Delete(SpellOverrides.SERVER_PATH);
    }
    else
      SpellOverrides.serverObj.SaveToFile(SpellOverrides.SERVER_PATH);
    return SpellOverrides.serverObj;
  }

  public static void SerializeAsServer(SpellOverrides s)
  {
    using (MemoryStream memoryStream = new MemoryStream())
    {
      using (myBinaryWriter myBinaryWriter = new myBinaryWriter((Stream) memoryStream))
      {
        myBinaryWriter.Write((byte) 99);
        myBinaryWriter.Write(s != null);
        if (s != null)
          myBinaryWriter.Write(s.ToBytes());
      }
      SpellOverrides.ServerBytes = memoryStream.ToArray();
      SpellOverrides.serverObj = s;
    }
  }

  public static SpellOverrides GenDefault()
  {
    SpellOverrides spellOverrides = new SpellOverrides();
    foreach (KeyValuePair<string, Spell> spell in Inert.Instance.spells)
      spellOverrides.listSpells.Add(new SpellOverrides.ASpell()
      {
        Name = spell.Key,
        stats = spell.Value.stats
      });
    foreach (Creature creature in Inert.Instance._creatures)
      spellOverrides.listCreatures.Add(new SpellOverrides.ACreature()
      {
        Name = creature.name,
        stats = creature.stats
      });
    return spellOverrides;
  }

  public void CompareDefault()
  {
    Controller.ShowPopup(CreditsMenu.Type.SpellOverrides, NewtonsoftExtensions.CompareObjects(this.ToJson(), SpellOverrides.GenDefault().ToJson(), true).ToString());
  }

  public static void Compare(SpellOverrides a, SpellOverrides b)
  {
    SpellOverrides spellOverrides1 = SpellOverrides.GenDefault();
    spellOverrides1.global = b.global;
    foreach (SpellOverrides.ASpell listSpell in b.listSpells)
    {
      SpellOverrides.ASpell x = listSpell;
      SpellOverrides.ASpell aspell = spellOverrides1.listSpells.Find((Predicate<SpellOverrides.ASpell>) (t => string.Equals(t.Name, x.Name)));
      if (aspell != null)
        aspell.stats = x.stats;
    }
    foreach (SpellOverrides.ACreature listCreature in b.listCreatures)
    {
      SpellOverrides.ACreature x = listCreature;
      SpellOverrides.ACreature acreature = spellOverrides1.listCreatures.Find((Predicate<SpellOverrides.ACreature>) (t => string.Equals(t.Name, x.Name)));
      if (acreature != null)
        acreature.stats = x.stats;
    }
    SpellOverrides spellOverrides2 = SpellOverrides.GenDefault();
    spellOverrides2.global = a.global;
    foreach (SpellOverrides.ASpell listSpell in a.listSpells)
    {
      SpellOverrides.ASpell x = listSpell;
      SpellOverrides.ASpell aspell = spellOverrides2.listSpells.Find((Predicate<SpellOverrides.ASpell>) (t => string.Equals(t.Name, x.Name)));
      if (aspell != null)
        aspell.stats = x.stats;
    }
    foreach (SpellOverrides.ACreature listCreature in a.listCreatures)
    {
      SpellOverrides.ACreature x = listCreature;
      SpellOverrides.ACreature acreature = spellOverrides2.listCreatures.Find((Predicate<SpellOverrides.ACreature>) (t => string.Equals(t.Name, x.Name)));
      if (acreature != null)
        acreature.stats = x.stats;
    }
    Controller.ShowPopup(CreditsMenu.Type.SpellOverrides, NewtonsoftExtensions.CompareObjects(spellOverrides2.ToJson(), spellOverrides1.ToJson(), true).ToString());
  }

  [Serializable]
  public class AGlobal
  {
    public string version = "v0.00";
    public bool wind = true;
    public bool bounce = true;
    public bool flyBounce = true;
    public string changelog = "";
  }

  [Serializable]
  public class ASpell
  {
    public string Name;
    public SpellStats stats;
  }

  [Serializable]
  public class ACreature
  {
    public string Name;
    public SpellSlot[] slot;
    public CreatureStats stats;
  }
}

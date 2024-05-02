// Decompiled with JetBrains decompiler
// Type: Content
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using Educative;
using UnityEngine;

#nullable disable
public static class Content
{
  public static object GetContent(myBinaryReader reader, ContentType contentType)
  {
    switch (contentType)
    {
      case ContentType.Outfit:
      case ContentType.SpellBook:
        SettingsPlayer content1 = new SettingsPlayer();
        content1.Deserialize(reader);
        return (object) content1;
      case ContentType.ArcTutorial:
        return (object) Tutorial.FromBytes(reader.ReadBytes());
      case ContentType.GameSettings:
        GameSettings content2 = new GameSettings();
        content2.Deserialize(reader);
        return (object) content2;
      case ContentType.ClanInvite:
        return (object) new ClanInvite()
        {
          from = reader.ReadString(),
          clan = reader.ReadString(),
          expire = (Time.realtimeSinceStartup + 29f)
        };
      case ContentType.MiniGameInvite:
        return (object) new MinigameInvite()
        {
          from = reader.ReadString(),
          miniGameType = reader.ReadInt32(),
          spectator = reader.ReadBoolean(),
          minigameID = reader.ReadInt32()
        };
      case ContentType.ColorScheme:
        return (object) ColorScheme.Deserialize(reader);
      case ContentType.RatedSettings:
        return (object) RatedFacts.Deserialize(3, reader);
      case ContentType.RatedContainer:
        return (object) RatedContainer.Deserialize(reader);
      default:
        return (object) null;
    }
  }

  public static void ToBytes(myBinaryWriter writer, object d, ContentType contentType)
  {
    switch (contentType)
    {
      case ContentType.Outfit:
      case ContentType.SpellBook:
        ((SettingsPlayer) d).Serialize(writer);
        break;
      case ContentType.ArcTutorial:
        Tutorial tutorial = (Tutorial) d;
        writer.Write(tutorial.ToBytes());
        break;
      case ContentType.GameSettings:
        ((GameSettings) d).Serialize(writer);
        break;
      case ContentType.ClanInvite:
        ClanInvite clanInvite = (ClanInvite) d;
        writer.Write(clanInvite.clan);
        break;
      case ContentType.MiniGameInvite:
        MinigameInvite minigameInvite = (MinigameInvite) d;
        writer.Write(minigameInvite.from);
        writer.Write(minigameInvite.miniGameType);
        writer.Write(minigameInvite.spectator);
        writer.Write(minigameInvite.minigameID);
        break;
      case ContentType.ColorScheme:
        ((ColorScheme) d).Serialize(writer);
        break;
      case ContentType.RatedSettings:
        ((RatedFacts) d).Share(writer);
        break;
      case ContentType.RatedContainer:
        ((RatedContainer) d).Serialize(writer);
        break;
    }
  }
}

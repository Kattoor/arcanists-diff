// Decompiled with JetBrains decompiler
// Type: EmojiInfo
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public static class EmojiInfo
{
  public static EmojiInfo.Cat allEmoji = new EmojiInfo.Cat();
  public static List<EmojiInfo.Cat> categories = new List<EmojiInfo.Cat>();
  public static EmojiInfo.Cat searchResults = new EmojiInfo.Cat();
  public static HashSet<string> resultsRealNames = new HashSet<string>();
  public static HashSet<string> IgnoredSpells = new HashSet<string>()
  {
    "Star Ball",
    "Starfire",
    "Starfire Shard",
    "Star Dust"
  };

  public static int GetIndex(string n)
  {
    for (int index = 0; index < EmojiInfo.allEmoji.list.Count; ++index)
    {
      if (string.Equals(EmojiInfo.allEmoji.list[index].realName, n))
        return index;
    }
    return 0;
  }

  public static Emoji FromIndex(int i)
  {
    return EmojiInfo.allEmoji.list[Mathf.Clamp(i, 0, EmojiInfo.allEmoji.list.Count - 1)];
  }

  public static bool Search(string n)
  {
    EmojiInfo.searchResults.list.Clear();
    EmojiInfo.resultsRealNames.Clear();
    if (string.IsNullOrEmpty(n))
      return false;
    foreach (Emoji emoji in EmojiInfo.allEmoji.list)
    {
      if (emoji.name.IndexOf(n, StringComparison.OrdinalIgnoreCase) >= 0 && !EmojiInfo.resultsRealNames.Contains(emoji.realName))
      {
        EmojiInfo.searchResults.list.Add(emoji);
        EmojiInfo.resultsRealNames.Add(emoji.realName);
      }
    }
    return EmojiInfo.searchResults.list.Count > 0;
  }

  public static Emoji FindByName(string n)
  {
    foreach (Emoji byName in EmojiInfo.allEmoji.list)
    {
      if (string.Equals(byName.name, n))
        return byName;
    }
    return (Emoji) null;
  }

  public static void Intitialize()
  {
    if (EmojiInfo.categories.Count > 0)
      return;
    foreach (object obj in Enum.GetValues(typeof (EmojiInfo.Category)))
      EmojiInfo.categories.Add(new EmojiInfo.Cat());
    foreach (Emoji emoji in EmojiRaw.importedData)
    {
      if (emoji.category != EmojiInfo.Category.Spells)
      {
        EmojiInfo.allEmoji.list.Add(emoji);
        EmojiInfo.categories[(int) emoji.category].list.Add(emoji);
      }
    }
    foreach (Sprite sprite in ClientResources.Instance._discordEmoji)
    {
      Emoji emoji = new Emoji()
      {
        name = sprite.name,
        realName = sprite.name,
        category = EmojiInfo.Category.Discord
      };
      EmojiInfo.allEmoji.list.Add(emoji);
      EmojiInfo.categories[(int) emoji.category].list.Add(emoji);
    }
    foreach (Sprite spellicon in ClientResources.Instance._spellicons)
    {
      if (!EmojiInfo.IgnoredSpells.Contains(spellicon.name))
      {
        Emoji emoji = new Emoji()
        {
          name = spellicon.name,
          realName = spellicon.name,
          category = EmojiInfo.Category.Spells
        };
        EmojiInfo.allEmoji.list.Add(emoji);
        EmojiInfo.categories[(int) emoji.category].list.Add(emoji);
      }
    }
  }

  public enum Category
  {
    None,
    Smileys_and_Emotion,
    People_and_Body,
    Animals_and_Nature,
    Food_and_Drink,
    Symbols,
    Travel_and_Places,
    Objects,
    Activities,
    Flags,
    Spells,
    Other_Arcanists,
    Favorites,
    Discord,
  }

  public class Cat
  {
    public List<Emoji> list = new List<Emoji>();
  }
}

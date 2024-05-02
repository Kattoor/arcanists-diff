// Decompiled with JetBrains decompiler
// Type: Prestige
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using Hazel;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

#nullable disable
public static class Prestige
{
  public const byte MsgInfo = 1;
  public const byte MsgUnlockBook = 2;
  public const byte MsgUnlockSpell = 3;
  public const byte MsgLevelup = 4;
  public const byte MsgLevelDown = 5;
  public const byte MsgWands = 6;
  public const byte MsgError = 7;
  public const byte MsgCompleteTutorial = 8;
  public const byte MsgTournamentCoins = 9;
  public const byte MsgBuyTournamentOutfit = 10;
  public static List<Prestige.TutorialInfo> tutorialInfo = new List<Prestige.TutorialInfo>()
  {
    new Prestige.TutorialInfo() { wands = 5 },
    new Prestige.TutorialInfo() { wands = 5 },
    new Prestige.TutorialInfo() { wands = 5 },
    new Prestige.TutorialInfo() { wands = 5 },
    new Prestige.TutorialInfo() { wands = 5 },
    new Prestige.TutorialInfo() { wands = 5 },
    new Prestige.TutorialInfo() { wands = 5 },
    new Prestige.TutorialInfo() { wands = 5 },
    new Prestige.TutorialInfo() { wands = 5 },
    new Prestige.TutorialInfo() { wands = 5 },
    new Prestige.TutorialInfo() { wands = 5 }
  };

  public static void ServerHandler(Connection c, myBinaryReader r)
  {
  }

  public static void OnLevelUp(Account a, int oldPrestige, int newPrestige, BookOf additionalBook)
  {
    if (additionalBook < BookOf.Stone || !Enum.IsDefined(typeof (BookOf), (object) additionalBook))
      additionalBook = BookOf.Stone;
    a.wands = newPrestige <= 2 ? 50 : 0;
    a.totalWands = a.wands;
    a.cosmetics.spells.ResetAll();
    Prestige.Unlock(a, BookOf.Arcane, true, true);
    Prestige.Unlock(a, BookOf.Flame, true);
    Prestige.Unlock(a, BookOf.Cogs, true, true);
    Prestige.Unlock(a, additionalBook, true, true);
    if (oldPrestige < 5 && newPrestige >= 5)
      Achievements.Set(Achievement.Lord_of_Wands, a, (ZGame) null);
    if (oldPrestige >= 10 || newPrestige < 10)
      return;
    Achievements.Set(Achievement.Wand_Hoarder, a, (ZGame) null);
  }

  public static void OnLevelDown(Account a, int oldPrestige, int newPrestige)
  {
    if (oldPrestige >= 5 && newPrestige < 5)
      Achievements.Remove(Achievement.Lord_of_Wands, a);
    if (oldPrestige >= 10 && newPrestige < 10)
      Achievements.Remove(Achievement.Wand_Hoarder, a);
    a.totalWands = Prestige.MaxWands(a);
    a.wands = 0;
    int num = a.prestige == (byte) 0 ? 9 : (int) (RandomExtensions.LastBook() + 1);
    a.cosmetics.spells.ResetAll();
    for (int book = 0; book < num; ++book)
      Prestige.Unlock(a, (BookOf) book, true, true);
  }

  public static void ClientHandler(myBinaryReader r)
  {
    switch (r.ReadByte())
    {
      case 2:
        BookOf book = (BookOf) r.ReadInt32();
        Prestige.Unlock(Client.MyAccount, book);
        Prestige.RefreshUI();
        break;
      case 3:
        int i = r.ReadInt32();
        Prestige.Unlock(Client.MyAccount, i);
        Prestige.RefreshUI();
        break;
      case 4:
      case 5:
        Client.MyAccount.prestige = r.ReadByte();
        Client.MyAccount.wands = r.ReadInt32();
        Client.MyAccount.totalWands = r.ReadInt32();
        Client.cosmetics = Cosmetics.Deserialize(r);
        Client.MyAccount.cosmetics = Client.cosmetics;
        Prestige.RefreshUI();
        break;
      case 6:
        Client.MyAccount.wands = r.ReadInt32();
        Client.MyAccount.totalWands = r.ReadInt32();
        Prestige.RefreshUI();
        break;
      case 9:
        Client.MyAccount.tournamentCoins = r.ReadInt32();
        CharacterCreation.Instance?.txtCoins.SetText(Client.MyAccount.tournamentCoins.ToString());
        break;
      case 10:
        int index1 = r.ReadInt32();
        int index2 = r.ReadInt32();
        Client.MyAccount.cosmetics.array[index1][index2] = true;
        CharacterCreation.Instance?.RefreshViewing();
        AudioManager.Play(AudioManager.instance.coins);
        break;
    }
  }

  public static void RefreshUI()
  {
    SpellLobbyChange.Instance?.Refresh(true);
    ChooseJsonDialog.Instance?.Refresh();
    PrestigeLobbyUI.Instance?.Refresh(false);
    LobbyMenu.instance?.RefreshNames();
    UnratedMenu.instance?.RefreshNames(false);
  }

  public static void Ask(byte msg, int info)
  {
    using (MemoryStream memoryStream = new MemoryStream())
    {
      using (myBinaryWriter myBinaryWriter = new myBinaryWriter((Stream) memoryStream))
      {
        myBinaryWriter.Write((byte) 96);
        myBinaryWriter.Write(msg);
        myBinaryWriter.Write(info);
      }
      Client.connection?.SendBytes(memoryStream.ToArray());
    }
  }

  public static void Ask(byte msg, int info, int info2)
  {
    using (MemoryStream memoryStream = new MemoryStream())
    {
      using (myBinaryWriter myBinaryWriter = new myBinaryWriter((Stream) memoryStream))
      {
        myBinaryWriter.Write((byte) 96);
        myBinaryWriter.Write(msg);
        myBinaryWriter.Write(info);
        myBinaryWriter.Write(info2);
      }
      Client.connection?.SendBytes(memoryStream.ToArray());
    }
  }

  public static void SendLevel(Connection c)
  {
    using (MemoryStream memoryStream = new MemoryStream())
    {
      using (myBinaryWriter w = new myBinaryWriter((Stream) memoryStream))
      {
        w.Write((byte) 96);
        w.Write((byte) 4);
        w.Write(c.player.account.prestige);
        w.Write(c.player.account.wands);
        w.Write(c.player.account.totalWands);
        c.player.account.cosmetics.Serialize(w);
      }
      c.SendBytes(memoryStream.ToArray());
    }
    Server.UpdateAccountInfo(c.player.account, false);
  }

  public static void Send(Connection c, byte msg, int info, int info2 = -1)
  {
    using (MemoryStream memoryStream = new MemoryStream())
    {
      using (myBinaryWriter myBinaryWriter = new myBinaryWriter((Stream) memoryStream))
      {
        myBinaryWriter.Write((byte) 96);
        myBinaryWriter.Write(msg);
        myBinaryWriter.Write(info);
        myBinaryWriter.Write(info2);
      }
      if (c.State != ConnectionState.Connected || c == null)
        return;
      c.SendBytes(memoryStream.ToArray());
    }
  }

  public static void AskUnlock(int i)
  {
    ClientResources.Instance.SpawnUIExplosion(i / 12, Input.mousePosition);
    Prestige.Ask((byte) 3, i);
  }

  public static void AskUnlock(BookOf i)
  {
    ClientResources.Instance.SpawnUIExplosion((int) i, Input.mousePosition);
    Prestige.Ask((byte) 2, (int) i);
  }

  public static bool AllUnlocked(Account acc)
  {
    int num = 108;
    for (int index = 0; index < num; ++index)
    {
      if (!acc.cosmetics.spells[index])
        return false;
    }
    return true;
  }

  public static void Unlock(Cosmetics cosmetics, BookOf book, bool full = false, bool lvl3s = false)
  {
    int num1 = (int) book * 12;
    if (full)
    {
      int num2 = lvl3s ? 12 : 10;
      for (int index = 0; index < num2; ++index)
        cosmetics.spells[num1 + index] = true;
    }
    else
    {
      for (int index = 0; index < 10; index += 2)
        cosmetics.spells[num1 + index] = true;
    }
  }

  public static void Unlock(Account acc, BookOf book, bool full = false, bool lvl3s = false)
  {
    int num1 = (int) book;
    int index1 = num1 * 12;
    if (num1 < 0 || (BookOf) num1 > RandomExtensions.LastBook())
      return;
    if (full)
    {
      int num2 = lvl3s ? 12 : 10;
      for (int index2 = 0; index2 < num2; ++index2)
        acc.cosmetics.spells[index1 + index2] = true;
    }
    else
    {
      if (acc.cosmetics.spells[index1])
        return;
      acc.wands -= 5;
      for (int index3 = 0; index3 < 10; index3 += 2)
        acc.cosmetics.spells[index1 + index3] = true;
    }
  }

  public static void Unlock(Account acc, int i)
  {
    int num = i % 12;
    acc.cosmetics.spells[i] = true;
    if (num >= 10)
      acc.wands -= 3;
    else
      acc.wands -= 2;
  }

  public static string BookErrorCode(int i)
  {
    switch (i)
    {
      case 0:
        return "<Error>";
      case 1:
        return "Requires 5 Wands to unlock.";
      case 2:
        return "Index out of range.";
      case 3:
        return "Already Bought.";
      case 4:
        return "Can only unlock after the first prestige.";
      default:
        return "...";
    }
  }

  public static string SpellErrorCode(int i)
  {
    switch (i)
    {
      case 0:
        return "<Error>";
      case 1:
        return "Unlock the book first!";
      case 2:
        return "Already Unlocked.";
      case 3:
        return "Requires 3 Wands to unlock.";
      case 4:
        return "Requires 2 Wands to unlock.";
      default:
        return "...";
    }
  }

  public static int CanUnlock(Account acc, BookOf book)
  {
    int num = (int) book;
    int index = num * 12;
    if (acc.wands < 5)
      return 1;
    if (num < 0 || (BookOf) num > RandomExtensions.LastBook())
      return 2;
    if (book >= BookOf.Cogs && acc.prestige == (byte) 0)
      return 4;
    return !acc.cosmetics.spells[index] ? 0 : 3;
  }

  public static int CanUnlock(Account acc, int i)
  {
    int num1 = i % 12;
    int num2 = i / 12;
    if (i >= (int) (RandomExtensions.LastBook() + 1) * 12 || !acc.cosmetics.spells[i - num1])
      return 1;
    if (acc.cosmetics.spells[i])
      return 2;
    if (num1 >= 10 && acc.wands < 3)
      return 3;
    return acc.wands < 2 ? 4 : 0;
  }

  public static bool IsUnlocked(Account acc, int i)
  {
    if (acc == null || acc.cosmetics == null)
      return true;
    return i >= 0 && i < (int) (RandomExtensions.LastBook() + 1) * 12 && acc.cosmetics.spells[i];
  }

  public static bool IsUnlocked(Account acc, BookOf book)
  {
    if (acc == null || acc.cosmetics == null)
      return true;
    int num = (int) book;
    int index = num * 12;
    return num >= 0 && (BookOf) num <= RandomExtensions.LastBook() && acc.cosmetics.spells[index];
  }

  public static bool ViewLocked(this ViewSpellLocks s)
  {
    return (s & (ViewSpellLocks.Locked | ViewSpellLocks.Locked_Rated_Restricted)) != 0;
  }

  public static bool ViewRestricted(this ViewSpellLocks s)
  {
    return (s & (ViewSpellLocks.Locked_Rated_Restricted | ViewSpellLocks.Rated_Restricted)) != 0;
  }

  public static int MaxWands(Account acc) => acc.prestige < (byte) 1 ? 153 : 237;

  public static bool ReadyToPrestige(Account acc) => acc.totalWands >= Prestige.MaxWands(acc);

  public static bool AboveRating(Account acc, int other = 0)
  {
    int num = Prestige.RequiredRating(acc);
    return (int) acc.HighestRating >= num || other >= num;
  }

  public static int RequiredRating(Account acc)
  {
    return acc.prestige >= (byte) 2 ? Mathf.Clamp(1000 + (int) acc.prestige * 100, 1000, 2000) : 1000;
  }

  public static bool VerifySpells(Account acc, byte[] spells, ref byte fullBook)
  {
    bool flag = false;
    for (int index = 0; index < spells.Length; ++index)
    {
      if (spells[index] < byte.MaxValue && !acc.cosmetics.spells[(int) spells[index]])
      {
        flag = true;
        spells[index] = byte.MaxValue;
      }
    }
    if (fullBook != (byte) 0)
    {
      int book = (int) fullBook - 1;
      int num = (int) RandomExtensions.LastBook();
      if (book < 0 || book > num)
        book = 0;
      if (!Prestige.CanUseElemental(acc, book))
        fullBook = (byte) 0;
      else if (book == 0 && fullBook > (byte) 1)
        fullBook = (byte) book;
    }
    return !flag;
  }

  public static bool CanUseElemental(Account acc, int book)
  {
    if (acc == null || acc.cosmetics == null)
      return true;
    int num = book * 12;
    if (book < 0)
      return true;
    if ((BookOf) book > RandomExtensions.LastBook())
      return false;
    for (int index = 0; index < 12; ++index)
    {
      if (!acc.cosmetics.spells[num + index])
        return false;
    }
    return true;
  }

  public class TutorialInfo
  {
    public int wands;
  }
}

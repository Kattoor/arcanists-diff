

using System;
using System.Text;

#nullable disable
namespace Hazel
{
  public static class AccountExtension
  {
    public static AccountType[] _IconOrder = new AccountType[31]
    {
      AccountType.Owner,
      AccountType.Developer,
      AccountType.Game_Director,
      AccountType.Web_Developer,
      AccountType.Community_Admin,
      AccountType.Mod,
      AccountType.GameMod,
      AccountType.Perm_Muted,
      AccountType.Muted,
      AccountType.Contributor,
      AccountType.Imp,
      AccountType.Press_Account,
      AccountType.Art_Lead,
      AccountType.Asset_Creator,
      AccountType.Head_Tournament_Official,
      AccountType.Tournament_Official,
      AccountType.Audio_Wizard,
      AccountType.Lifetime,
      AccountType.Arch_Donator,
      AccountType.Donator,
      AccountType.Booster,
      AccountType.Arcane_Monster,
      AccountType.Wiki_Staff,
      AccountType.Twitch_Streamer,
      AccountType.Youtube_Creator,
      AccountType.Robotics_Division,
      AccountType.First_Place,
      AccountType.Second_Place,
      AccountType.Third_Place,
      AccountType.Tourny_Participate,
      AccountType.None
    };

    public static byte GetIndex(this AccountType a)
    {
      int num = (int) a;
      for (int index = 0; index < 32; ++index)
      {
        if ((num & 1 << index) != 0)
          return (byte) index;
      }
      return 0;
    }

    public static string ToSpacelessString(this AccountType a) => a.ToString().Replace('_', ' ');

    public static string ToExtendedString(this AccountType a)
    {
      StringBuilder stringBuilder = new StringBuilder();
      foreach (AccountType accountType in (AccountType[]) Enum.GetValues(typeof (AccountType)))
      {
        if ((a & accountType) != AccountType.None)
        {
          if (stringBuilder.Length > 0)
            stringBuilder.Append(", ");
          stringBuilder.Append(accountType.ToString());
        }
      }
      return stringBuilder.Length == 0 ? "None" : stringBuilder.ToString();
    }

    public static bool isMod(this AccountType a)
    {
      return (a & (AccountType.Mod | AccountType.Community_Admin | AccountType.GameMod)) != 0;
    }

    public static bool isDev(this AccountType a)
    {
      return (a & (AccountType.Developer | AccountType.Game_Director | AccountType.Owner)) != 0;
    }

    public static bool isDevOrHeadMod(this AccountType a)
    {
      return (a & (AccountType.Developer | AccountType.Community_Admin | AccountType.Game_Director | AccountType.Owner)) != 0;
    }

    public static bool IsModPlus(this AccountType a)
    {
      return (a & (AccountType.Mod | AccountType.Developer | AccountType.Community_Admin | AccountType.GameMod | AccountType.Game_Director | AccountType.Owner)) != 0;
    }

    public static bool IsModPlusTOParticipate(this AccountType a)
    {
      return (a & (AccountType.Mod | AccountType.Developer | AccountType.Community_Admin | AccountType.GameMod | AccountType.Tournament_Official | AccountType.Game_Director | AccountType.Owner | AccountType.Head_Tournament_Official | AccountType.Tourny_Participate)) != 0;
    }

    public static bool IsModPlusTO(this AccountType a)
    {
      return (a & (AccountType.Mod | AccountType.Developer | AccountType.Community_Admin | AccountType.GameMod | AccountType.Tournament_Official | AccountType.Game_Director | AccountType.Owner | AccountType.Head_Tournament_Official)) != 0;
    }

    public static bool Heightened(this AccountType a)
    {
      return (a & (AccountType.Mod | AccountType.Developer | AccountType.Community_Admin | AccountType.GameMod | AccountType.Contributor | AccountType.Imp | AccountType.Wiki_Staff | AccountType.Twitch_Streamer | AccountType.Youtube_Creator | AccountType.Asset_Creator | AccountType.Audio_Wizard | AccountType.Game_Director | AccountType.Owner)) != 0;
    }
  }
}

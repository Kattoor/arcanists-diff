

using System.Collections.Generic;
using System.Linq;

#nullable disable
public static class CopyExtensions
{
  public static void Copy(this Account a, Account b)
  {
    if (!a.key.Equals(b.key))
      a.key = b.key;
    if (!a.name.Equals(b.name))
      a.name = b.name;
    if (!a.dateCreated.Equals(b.dateCreated))
      a.dateCreated = b.dateCreated;
    if (!a.email.Equals(b.email))
      a.email = b.email;
    if (a.permBanned != b.permBanned)
      a.permBanned = b.permBanned;
    if (!((IEnumerable<byte>) a.hash).SequenceEqual<byte>((IEnumerable<byte>) b.hash))
      a.hash.Copy(b.hash);
    if (!((IEnumerable<byte>) a.salt).SequenceEqual<byte>((IEnumerable<byte>) b.salt))
      a.salt.Copy(b.salt);
    if (!a.ispLock.Equals(b.ispLock))
      a.ispLock = b.ispLock;
    if (!a.extraInfo.Equals(b.extraInfo))
      a.extraInfo = b.extraInfo;
    if (!a.steamKey.Equals(b.steamKey))
      a.steamKey = b.steamKey;
    if (a.tournamentCoins != b.tournamentCoins)
      a.tournamentCoins = b.tournamentCoins;
    if (a.totalUnratedGames != b.totalUnratedGames)
      a.totalUnratedGames = b.totalUnratedGames;
    if ((int) a.Rating != (int) b.Rating)
      a.Rating = b.Rating;
    if (a.RatedGamesWon != b.RatedGamesWon)
      a.RatedGamesWon = b.RatedGamesWon;
    if (a.TotalRatedGames != b.TotalRatedGames)
      a.TotalRatedGames = b.TotalRatedGames;
    if (a.CurrentWinningStreak != b.CurrentWinningStreak)
      a.CurrentWinningStreak = b.CurrentWinningStreak;
    if (a.LongestWinningStreak != b.LongestWinningStreak)
      a.LongestWinningStreak = b.LongestWinningStreak;
    if (a.DamageDealt != b.DamageDealt)
      a.DamageDealt = b.DamageDealt;
    if (a.GamesSurvived != b.GamesSurvived)
      a.GamesSurvived = b.GamesSurvived;
    if (a.Kills != b.Kills)
      a.Kills = b.Kills;
    if (a.GamesWentFirst != b.GamesWentFirst)
      a.GamesWentFirst = b.GamesWentFirst;
    if (a.GamesWentFirstWon != b.GamesWentFirstWon)
      a.GamesWentFirstWon = b.GamesWentFirstWon;
    if (a.Draws != b.Draws)
      a.Draws = b.Draws;
    if (a.LastGamePlayed != b.LastGamePlayed)
      a.LastGamePlayed = b.LastGamePlayed;
    if (a.TeamGames != b.TeamGames)
      a.TeamGames = b.TeamGames;
    if ((int) a.Rating1 != (int) b.Rating1)
      a.Rating1 = b.Rating1;
    if (a.RatedGamesWon1 != b.RatedGamesWon1)
      a.RatedGamesWon1 = b.RatedGamesWon1;
    if (a.TotalRatedGames1 != b.TotalRatedGames1)
      a.TotalRatedGames1 = b.TotalRatedGames1;
    if (a.CurrentWinningStreak1 != b.CurrentWinningStreak1)
      a.CurrentWinningStreak1 = b.CurrentWinningStreak1;
    if (a.LongestWinningStreak1 != b.LongestWinningStreak1)
      a.LongestWinningStreak1 = b.LongestWinningStreak1;
    if (a.DamageDealt1 != b.DamageDealt1)
      a.DamageDealt1 = b.DamageDealt1;
    if (a.GamesSurvived1 != b.GamesSurvived1)
      a.GamesSurvived1 = b.GamesSurvived1;
    if (a.Kills1 != b.Kills1)
      a.Kills1 = b.Kills1;
    if (a.GamesWentFirst1 != b.GamesWentFirst1)
      a.GamesWentFirst1 = b.GamesWentFirst1;
    if (a.GamesWentFirstWon1 != b.GamesWentFirstWon1)
      a.GamesWentFirstWon1 = b.GamesWentFirstWon1;
    if (a.Draws1 != b.Draws1)
      a.Draws1 = b.Draws1;
    if (a.LastGamePlayed1 != b.LastGamePlayed1)
      a.LastGamePlayed1 = b.LastGamePlayed1;
    if (a.TeamGames1 != b.TeamGames1)
      a.TeamGames1 = b.TeamGames1;
    if ((int) a.Rating2 != (int) b.Rating2)
      a.Rating2 = b.Rating2;
    if (a.RatedGamesWon2 != b.RatedGamesWon2)
      a.RatedGamesWon2 = b.RatedGamesWon2;
    if (a.TotalRatedGames2 != b.TotalRatedGames2)
      a.TotalRatedGames2 = b.TotalRatedGames2;
    if (a.CurrentWinningStreak2 != b.CurrentWinningStreak2)
      a.CurrentWinningStreak2 = b.CurrentWinningStreak2;
    if (a.LongestWinningStreak2 != b.LongestWinningStreak2)
      a.LongestWinningStreak2 = b.LongestWinningStreak2;
    if (a.DamageDealt2 != b.DamageDealt2)
      a.DamageDealt2 = b.DamageDealt2;
    if (a.GamesSurvived2 != b.GamesSurvived2)
      a.GamesSurvived2 = b.GamesSurvived2;
    if (a.Kills2 != b.Kills2)
      a.Kills2 = b.Kills2;
    if (a.GamesWentFirst2 != b.GamesWentFirst2)
      a.GamesWentFirst2 = b.GamesWentFirst2;
    if (a.GamesWentFirstWon2 != b.GamesWentFirstWon2)
      a.GamesWentFirstWon2 = b.GamesWentFirstWon2;
    if (a.Draws2 != b.Draws2)
      a.Draws2 = b.Draws2;
    if (a.LastGamePlayed2 != b.LastGamePlayed2)
      a.LastGamePlayed2 = b.LastGamePlayed2;
    if (a.TeamGames2 != b.TeamGames2)
      a.TeamGames2 = b.TeamGames2;
    if (!a.OLD_GAMES.Equals(b.OLD_GAMES))
      a.OLD_GAMES = b.OLD_GAMES;
    if (!a.oldName.Equals(b.oldName))
      a.oldName = b.oldName;
    if (!a.extraStuff.Equals((object) b.extraStuff))
      a.extraStuff.Copy(b.extraStuff);
    if (a.accountType != b.accountType)
      a.accountType = b.accountType;
    if ((int) a.experience != (int) b.experience)
      a.experience = b.experience;
    if ((int) a.prestige != (int) b.prestige)
      a.prestige = b.prestige;
    if ((long) a.discord != (long) b.discord)
      a.discord = b.discord;
    if (a.displayedIcon != b.displayedIcon)
      a.displayedIcon = b.displayedIcon;
    if ((int) a.displayClanPrefix != (int) b.displayClanPrefix)
      a.displayClanPrefix = b.displayClanPrefix;
    if (a.points != b.points)
      a.points = b.points;
    if (a.lastLogin != b.lastLogin)
      a.lastLogin = b.lastLogin;
    if (a.dust != b.dust)
      a.dust = b.dust;
    if (a.poll != b.poll)
      a.poll = b.poll;
    if ((int) a.version != (int) b.version)
      a.version = b.version;
    if (!a.tutorials.Equals((object) b.tutorials))
      a.tutorials.Copy(b.tutorials);
    if (a.wands != b.wands)
      a.wands = b.wands;
    if (a.totalWands != b.totalWands)
      a.totalWands = b.totalWands;
    if (a.lobbyChat != b.lobbyChat)
      a.lobbyChat = b.lobbyChat;
    if (a.gameChat != b.gameChat)
      a.gameChat = b.gameChat;
    if (a.friendChat != b.friendChat)
      a.friendChat = b.friendChat;
    if (a.inviteChat != b.inviteChat)
      a.inviteChat = b.inviteChat;
    if (a.spectatorChat != b.spectatorChat)
      a.spectatorChat = b.spectatorChat;
    if (a.clanChat != b.clanChat)
      a.clanChat = b.clanChat;
    if (a.teamChat != b.teamChat)
      a.teamChat = b.teamChat;
    if (a.miniGameChat != b.miniGameChat)
      a.miniGameChat = b.miniGameChat;
    if (!a.friends.SequenceEqual<string>((IEnumerable<string>) b.friends))
      a.friends.Copy(b.friends);
    if (!a.ignored.SequenceEqual<string>((IEnumerable<string>) b.ignored))
      a.ignored.Copy(b.ignored);
    if (!a.cosmetics.Equals((object) b.cosmetics))
      a.cosmetics.Copy(b.cosmetics);
    if ((double) a.bonusExperience != (double) b.bonusExperience)
      a.bonusExperience = b.bonusExperience;
    if (!a.clan.Equals(b.clan))
      a.clan = b.clan;
    if (a.clanRole != b.clanRole)
      a.clanRole = b.clanRole;
    if (a.badges.Equals((object) b.badges))
      return;
    a.badges.Copy(b.badges);
  }

  public static void Copy(this HashSet<string> a, HashSet<string> b)
  {
    a.Clear();
    foreach (string str in b)
      a.Add(str);
  }

  public static void Copy(this ExtraStuff a, ExtraStuff b)
  {
  }

  public static void Copy(this byte[] a, byte[] b)
  {
    if (a.Length != b.Length)
      a = new byte[b.Length];
    for (int index = 0; index < a.Length; ++index)
      a[index] = b[index];
  }

  public static void Copy(this BitBools a, BitBools b)
  {
    for (int index = 0; index < a.array.Length; ++index)
      a.array[index] = b.array[index];
  }

  public static void Copy(this Cosmetics a, Cosmetics b)
  {
    for (int index1 = 0; index1 < a.array.Length; ++index1)
    {
      for (int index2 = 0; index2 < a.array[index1].array.Length; ++index2)
        a.array[index1].array[index2] = b.array[index1].array[index2];
    }
  }
}

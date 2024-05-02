// Decompiled with JetBrains decompiler
// Type: WordFilter
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using mattmc3.dotmore.Collections.Generic;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

#nullable disable
public static class WordFilter
{
  internal static string BadWords = "\\bass\\b|\\bassnigger\\b|\\bbitch\\b|\\bchild fucker\\b|\\bchild-fucker\\b|\\bchinc\\b|\\bchincs\\b|\\bchink\\b|\\bchinky\\b|\\bcoon\\b|\\bcoonnass\\b|\\bcoons\\b|\\bdog-fucker\\b|\\bdyke\\b|\\bdykes\\b|\\bf4g\\b|\\bf4gg0t\\b|\\bfag\\b|\\bfagbag\\b|\\bfagfucker\\b|\\bfagg\\b|\\bfagg0t\\b|\\bfagged\\b|\\bfagging\\b|\\bfaggit\\b|\\bfaggitt\\b|\\bfaggot\\b|\\bfaggotcock\\b|\\bfaggots\\b|\\bfaggs\\b|\\bfagot\\b|\\bfagots\\b|\\bfags\\b|\\bfagtard\\b|\\bfaig\\b|\\bfaigt\\b|\\bfaqqet\\b|\\bfrigga\\b|\\bfuck\\b|\\bfuckingshitmotherfucker\\b|\\bfucknigger\\b|\\bgayass\\b|\\bgaytard\\b|\\bh0m0\\b|\\bh0mo\\b|\\bkill yourself\\b|\\bkkk\\b|\\bku klux klan\\b|\\bKu Klux Klan\\b|\\bky\\b|\\bmcfagget\\b|\\bmolest\\b|\\bn1gg@\\b|\\bn1gga\\b|\\bn1gger\\b|\\bnegro\\b|\\bnig nog\\b|\\bnigaboo\\b|\\bnigg\\b|\\bnigg3r\\b|\\bnigg4h\\b|\\bnigga\\b|\\bniggaa\\b|\\bniggah\\b|\\bniggas\\b|\\bniggaz\\b|\\bnigger\\b|\\bniggerfaggot\\b|\\bniggers\\b|\\bniggger\\b|\\bnigggger\\b|\\bniggle\\b|\\bniglet\\b|\\bnig-nog\\b|\\bnigro\\b|\\bnigros\\b|\\bpaki\\b|\\bporch monkey\\b|\\bporchmonkey\\b|\\bqueer\\b|\\bqueerbait\\b|\\bqueerhole\\b|\\bqueero\\b|\\bqueers\\b|\\braghead\\b|\\brape\\b|\\braped\\b|\\braper\\b|\\brapey\\b|\\braping\\b|\\brapist\\b|\\breetard\\b|\\bretard\\b|\\bretarded\\b|\\breturd\\b|\\britard\\b|\\brope\\b|\\bRP1\\b|\\brtard\\b|\\br-tard\\b|\\bsand nigger\\b|\\bsandnigger\\b|\\bsex\\b|\\bslanteye\\b|\\bspic\\b|\\bspick\\b|\\bspik\\b|\\bspiks\\b|\\btowelhead\\b|\\bwhitey\\b|\\bwigger\\b|nigger|niger|cunt|n1ger|n1gger|nlger|nlgger|nigr|n1gr|nlgr|nigg|n1gg|nlgg";
  private static string[] replacer = new string[27]
  {
    "",
    "*",
    "**",
    "***",
    "****",
    "*****",
    "******",
    "*******",
    "********",
    "*********",
    "**********",
    "***********",
    "************",
    "*************",
    "**************",
    "***************",
    "****************",
    "*****************",
    "******************",
    "*******************",
    "********************",
    "*********************",
    "**********************",
    "***********************",
    "************************",
    "*************************",
    "**************************"
  };
  public static string ForbiddenWords = "<size|<align|<cspace|<font|<indent|<line|<link|<margin|<mark|<mspace|<noparse|<page|<pos|<space|<style|<voffset|<width|<color";
  private static Regex wordFilter = new Regex(WordFilter.BadWords, RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);
  private static Regex forbiddenFilter = new Regex(WordFilter.ForbiddenWords, RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);
  private static Regex webFilter = new Regex("(https?|www\\.|\\.com|\\.c0m|\\.net)", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.CultureInvariant);
  private static Regex linkFilter = (Regex) null;
  internal static OrderedDictionary<string, string> linkreplacement = new OrderedDictionary<string, string>((IEqualityComparer<string>) StringComparer.InvariantCultureIgnoreCase)
  {
    {
      "#appealform",
      "<link=\"https://arc2-forms.vercel.app/punishment-appeal.html\"><u>Appeal Form</u></link>"
    },
    {
      "#website",
      "<link=\"" + Server.websiteLink + "\"><u>Arcanists 2</u></link>"
    },
    {
      "#wiki",
      "<link=\"" + Server.wikiLink + "/\"><u>Wiki</u></link>"
    },
    {
      "#discord",
      "<link=\"" + Server.discordLink + "/\"><u>Discord</u></link>"
    },
    {
      "#announcements",
      "<link=\"https://discord.com/channels/597846871041638417/611587838114267136\"><u>#Announcements</u></link>"
    },
    {
      "#rules",
      "<link=\"https://discord.com/channels/597846871041638417/610688551675363350\"><u>#Rules</u></link>"
    },
    {
      "#donations",
      "<link=\"https://discord.com/channels/597846871041638417/1047656266119524362\"><u>#How to donate</u></link>"
    },
    {
      "#getinvolved",
      "<link=\"https://discord.com/channels/597846871041638417/733108967139573881\"><u>#Get Involved</u></link>"
    },
    {
      "#sneak",
      "<link=\"https://discord.com/channels/597846871041638417/838896718418018314\"><u>#Sneak-Peaks</u></link>"
    },
    {
      "#general",
      "<link=\"https://discord.com/channels/597846871041638417/610873668188241922\"><u>#General</u></link>"
    },
    {
      "#meta",
      "<link=\"https://discord.com/channels/597846871041638417/651452417128267786\"><u>#Meta & Ideas</u></link>"
    },
    {
      "#matchmaking",
      "<link=\"https://discord.com/channels/597846871041638417/610892460708855817\"><u>#Matchmaking</u></link>"
    },
    {
      "#clip",
      "<link=\"https://discord.com/channels/597846871041638417/651452571789295654\"><u>#Clips & Screenshots</u></link>"
    },
    {
      "#submit",
      "<link=\"https://discord.com/channels/597846871041638417/928517811783999488\"><u>#Submit Suggestions</u></link>"
    },
    {
      "#suggestion",
      "<link=\"https://discord.com/channels/597846871041638417/598205579344871434\"><u>#Suggestions</u></link>"
    },
    {
      "#bug",
      "<link=\"https://discord.com/channels/597846871041638417/598202142293098507\"><u>#Bug Reports</u></link>"
    },
    {
      "#newcontent",
      "<link=\"https://discord.com/channels/597846871041638417/817250365078503474\"><u>#New Content Discussion</u></link>"
    },
    {
      "#tournamentchat",
      "<link=\"https://discord.com/channels/597846871041638417/720302856933998672\"><u>#Tournament discussion</u></link>"
    },
    {
      "#tournament",
      "<link=\"https://discord.com/channels/597846871041638417/741003701434712114\"><u>#Tournament Info</u></link>"
    },
    {
      "#voice",
      "<link=\"https://discord.com/channels/597846871041638417/610923321218957327\"><u>#Voice Channel</u></link>"
    },
    {
      "#weekend",
      "<link=\"https://discord.com/channels/597846871041638417/925646956360003604\"><u>#Weekend Tournament</u></link>"
    },
    {
      "#log",
      "<link=\"https://discord.com/channels/597846871041638417/760349009192943656\"><u>#Rated Game Logs</u></link>"
    },
    {
      "#video",
      "<link=\"https://discord.com/channels/597846871041638417/756414083615424543\"><u>#Video-Stream-Releases</u></link>"
    },
    {
      "#forums",
      "<link=\"https://discord.com/channels/597846871041638417/1020507868295860224\"><u>#Forums</u></link>"
    },
    {
      "#forum",
      "<link=\"https://discord.com/channels/597846871041638417/1020507868295860224\"><u>#Forums</u></link>"
    }
  };

  internal static void RefreshWords()
  {
    WordFilter.wordFilter = new Regex(WordFilter.BadWords, RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);
  }

  public static string FilterBadWords(string inputWords)
  {
    return WordFilter.wordFilter.Replace(inputWords, "");
  }

  public static bool HasWeb(string inputWords) => WordFilter.webFilter.IsMatch(inputWords);

  public static string FilterForbiddenWords(string inputWords)
  {
    return WordFilter.forbiddenFilter.Replace(inputWords, ";)");
  }

  public static bool HasBadWords(string inputWords) => WordFilter.wordFilter.IsMatch(inputWords);

  public static string ReplaceBadWords(string inputWords)
  {
    foreach (Match match in WordFilter.wordFilter.Matches(inputWords))
      inputWords = inputWords.Remove(match.Index, match.Length).Insert(match.Index, WordFilter.replacer[match.Value.Length]);
    return WordFilter.wordFilter.Replace(inputWords, "");
  }

  public static MatchCollection GetBadWords(string inputWords)
  {
    return WordFilter.wordFilter.Matches(inputWords);
  }

  public static void CheckReplacements(ref string inputWords)
  {
    if (WordFilter.linkFilter == null)
      WordFilter.RefreshLinks();
    MatchCollection matchCollection = WordFilter.linkFilter.Matches(inputWords);
    if (matchCollection.Count <= 0)
      return;
    for (int i = matchCollection.Count - 1; i >= 0; --i)
      inputWords = inputWords.Remove(matchCollection[i].Index, matchCollection[i].Length).Insert(matchCollection[i].Index, WordFilter.linkreplacement[matchCollection[i].Value]);
  }

  public static void RefreshLinks()
  {
    StringBuilder stringBuilder = new StringBuilder();
    int num = 0;
    foreach (KeyValuePair<string, string> keyValuePair in WordFilter.linkreplacement)
    {
      ++num;
      stringBuilder.Append("\\").Append(keyValuePair.Key);
      if (num < WordFilter.linkreplacement.Count)
        stringBuilder.Append("|");
    }
    WordFilter.linkFilter = new Regex(stringBuilder.ToString(), RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);
  }
}



using Educative;
using Hazel;
using Newtonsoft.Json;
using Newtonsoft.Json.Bson;
using SevenZip.Compression.LZMA;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.Networking;

#nullable disable
public static class Global
{
  public static MyLocation mountOffset = new MyLocation(-14, 18);
  public static MyLocation mountOffsetLeft = new MyLocation(14, 18);
  public static MyLocation mountOffsetReindeer = new MyLocation(-7, 21);
  public static MyLocation mountOffsetLeftReindeer = new MyLocation(7, 21);
  public static MyLocation mountOffsetSteamEngine = new MyLocation(0, -7);
  public static MyLocation mountOffsetLeftGoat = new MyLocation(7, 21);
  public static MyLocation mountOffsetGoat = new MyLocation(-7, 21);
  public static MyLocation mountOffsetWaterLord = new MyLocation(-9, 36);
  public static MyLocation mountOffsetLeftWaterLord = new MyLocation(9, 36);
  public const byte MsgAuth = 1;
  public const byte MsgGameFrame = 2;
  public const byte ErrNameExists = 3;
  public const byte MsgDestroyGame = 4;
  public const byte MsgPlayerJoinedLobby = 5;
  public const byte MsgGameInfo = 6;
  public const byte MsgJoin = 7;
  public const byte MsgPlayerLeftUnrated = 8;
  public const byte MsgSyncLobby = 10;
  public const byte MsgPlayerLeftLobby = 11;
  public const byte MsgStartGame = 12;
  public const byte MsgGameOver = 13;
  public const byte MsgChangeGameFacts = 14;
  public const byte MsgGameTime = 15;
  public const byte MsgMapPixels = 16;
  public const byte MsgReady = 17;
  public const byte MsgPlayersInfo = 18;
  public const byte ErrMultipleConnections = 19;
  public const byte MsgOpenMainMenu = 20;
  public const byte MsgOpenLobby = 21;
  public const byte MsgOpenUnrated = 22;
  public const byte MsgOpenRated = 23;
  public const byte MsgPlayerJoinedUnrated = 24;
  public const byte ErrToManyAlts = 25;
  public const byte MsgTryJoinUnrated = 26;
  public const byte MsgTryJoinRated = 27;
  public const byte MsgBackToMainMenu = 28;
  public const byte MsgCreateGame = 29;
  public const byte MsgGameStatus = 30;
  public const byte MsgPixelBlock = 31;
  public const byte MsgPixelBlockEND = 32;
  public const byte MsgServerMsg = 33;
  public const byte MsgKeepAlive = 34;
  public const byte MsgOldData = 35;
  public const byte MsgSpectate = 36;
  public const byte MsgAuthSpectator = 37;
  public const byte MsgCreateRatedLobby = 39;
  public const byte ErrSomethingWentWrong = 40;
  public const byte MsgInvite = 41;
  public const byte MsgUpdateAccountInfo = 42;
  public const byte MsgKick = 43;
  public const byte MsgChangeSpells = 44;
  public const byte MsgChangeOutfit = 45;
  public const byte MsgCreateAccount = 46;
  public const byte ErrInvalidNameOrPass = 47;
  public const byte MsgChangePassword = 48;
  public const byte DiscordToken = 49;
  public const byte MsgAddFriend = 50;
  public const byte MsgAchievement = 51;
  public const byte MsgSpectatorDisconnect = 52;
  public const byte MsgVPN = 53;
  public const byte DiscordTokenError = 54;
  public const byte MsgReportAbuse = 55;
  public const byte MsgRetreiveUserDiscordID = 56;
  public const byte MsgChangeChatSettings = 57;
  public const byte MsgRetreiveUserInfo = 58;
  public const byte MsgAskToJoinGame = 59;
  public const byte MsgChangeElemental = 60;
  public const byte MsgLogin = 61;
  public const byte ErrNetwork = 62;
  public const byte MsgNotification = 63;
  public const byte MsgOldDataEx = 64;
  public const byte MsgReturn = 65;
  public const byte MsgChangeRatedFacts = 66;
  public const byte MsgFindOpponents = 67;
  public const byte MsgRanks = 68;
  public const byte MsgPlayerStats = 69;
  public const byte MsgYourGameStarted = 70;
  public const byte MsgConnected = 71;
  public const byte MsgNotifyNameChange = 72;
  public const byte MsgUpdateCosmetics = 73;
  public const byte MsgGetCosmetics = 74;
  public const byte MsgChangeAccountIcon = 75;
  public const byte MsgTryJoinLobbyFromQue = 76;
  public const byte MsgShare = 77;
  public const byte MsgCreateBookClubGame = 78;
  public const byte MsgRestrictions = 79;
  public const byte MsgCopyGameSettings = 80;
  public const byte MsgUpdateRatedFacts = 81;
  public const byte MsgGameDescription = 82;
  public const byte MsgSpectatorLogic = 83;
  public const byte MsgClanUpdate = 84;
  public const byte MsgPlayerLocation = 85;
  public const byte MsgClientReplay = 86;
  public const byte MsgChess = 87;
  public const byte MsgCreateMinigame = 88;
  public const byte MsgJoinMiniGame = 89;
  public const byte MsgPoll = 90;
  public const byte MsgAutoLink = 91;
  public const byte MsgDebugTest = 92;
  public const byte MsgRejoinGame = 93;
  public const byte MsgMessageBox = 94;
  public const byte MsgRatedRestrictions = 95;
  public const byte MsgPrestigeStuff = 96;
  public const byte MsgUpdateAttribute = 97;
  public const byte MsgRemoveGame = 98;
  public const byte MsgSpellOverrides = 99;
  public const byte MsgCompleteSerialization = 100;
  public const byte MsgPreStartGame = 101;
  public const byte ErrNetwork2 = 102;
  public const byte MsgClanChat = 150;
  public const byte MsgPrivateChat = 151;
  public const byte MsgTeamChat = 152;
  public const byte MsgChat = 153;
  public const byte MsgMapPing = 154;
  public const byte MsgQuickChat = 155;
  public const byte MsgTest = 156;
  public const byte MsgEmoji = 157;
  public const byte MsgVisualResign = 191;
  public const byte MsgBid = 192;
  public const byte MsgTryJoinLobby = 193;
  public const byte MsgOfferDraw = 194;
  public const byte MsgResign = 195;
  public const byte MsgOfferRematch = 196;
  public const byte MsgGameOutfit = 197;
  public const byte MsgNextTurn = 198;
  public const byte MsgDelayNextTurn = 199;
  public const byte MsgAbovePlayer = 200;
  public const byte MsgFamiliar = 201;
  public const byte MsgMoveLeft = 202;
  public const byte MsgMoveRight = 203;
  public const byte MsgHighJump = 204;
  public const byte MsgLongJump = 205;
  public const byte MsgSkipTurn = 206;
  public const byte MsgRollBack = 207;
  public const byte MsgFlipFlip = 208;
  public const byte MsgDetower = 209;
  public const byte MsgControlledJump = 211;
  public const byte MsgFire = 220;
  public static OperatingSystem OS = OperatingSystem.Windows;
  public static HashSet<Sprite> _cachedSprites = new HashSet<Sprite>();
  public static char[] BadChars = new char[4]
  {
    '\uF700',
    '\uF703',
    '\uF702',
    '\uF701'
  };
  public const string prefName = "prefName";
  public const string prefGameFacts = "prefGameFacts";
  public const string prefSpells = "prefSpells";
  public const string prefChatHIdden = "prefChatHIdden";
  public const string prefShowSpellDescriptions = "prefshowdescript";
  public const string prefVolumeSound = "prefvolsound";
  public const string prefVolumeMusic = "prefvolmusic";
  public const string prefMapType = "prefMapType";
  public const string prefFollowTargets = "preffollowtargets";
  public const string prefReFollowCamera = "prefrefollowcamera";
  public const string prefFPS = "preffps";
  public const string preflimitFPS = "preflimitfps";
  public const string prefLockMeter = "preflockmeter";
  public const string prefBlackBg = "prefblackBg";
  public const string prefBlackFg = "prefblackFg";
  public const string prefReversedX = "prefreversedx";
  public const string prefReversedY = "prefreversedy";
  public const string prefScrollWheel = "prefScrollWheel";
  public const string prefSaveReplay = "prefsavereplay";
  public const string prefFlashChat = "prefflashchat";
  public const string prefFlashChatTurn = "prefflashchatturn";
  public const string prefFlashChatInvite = "prefflashchatinvite";
  public const string prefAcceptInvites = "prefacceptinvites";
  public const string prefClanInvites = "prefclaninvites";
  public const string prefShowStartedGames = "prefshowstartedgames";
  public const string prefShowCreatedGames = "prefshowcreatedgames";
  public const string prefZoom = "prefZoom";
  public const string prefZoomMouse = "prefZoomMouse";
  public const string prefHDBG = "prefhdbg";
  public const string prefSandboxMap = "prefsandboxmaps";
  public const string prefControls = "prefControls";
  public const string prefDeathMsg = "prefdeathmsg";
  public const string prefFileBrowser = "preffilebrowser";
  public const string prefPreset = "prefselectedpreset";
  public const string prefSkipIntro = "prefSkipIntro";
  public const string prefshowEmoji = "prefshowemoji";
  public const string prefHasClickedFamiliar = "prefhasclickedfamiliar";
  public const string prefMapPing = "prefmapping";
  public const string prefMapPingSound = "prefmappingsound";
  public const string prefTurnStartSound = "prefturnstartsound";
  public const string prefTurnStartVolume = "prefturnstartvolume";
  public const string prefForcedGraphics = "prefforcedgraphics";
  public const string prefOldLayout = "prefoldlayout";
  public const string prefChessAudio = "prefchessaudio";
  public const string prefSkipWarning = "prefskipwarning";
  public const string prefViewingLocked = "prefviewinglocked";
  public const string prefJumpDetower = "prefjumpdetower";
  public const string prefNativeKeyboard = "prefnativekeyboard";
  public const string prefAdvanced = "prefadvanced";
  public const string prefOverheadRender = "toggleOverheadRender";
  public const string prefOverheadSpectator = "toggleOverheadSpectator";
  public const string prefOverheadSound = "toggleOverheadSound";
  public const string publicKey = "<RSAKeyValue><Modulus>mJbQP4q78dSvBUsCKXJXMnPQVVBtwEoC6IZH/n+jVWwT4P/AUw3pIGY69LzcLSjVErJmx2cTFHOk61mlqyQp0+51fUOkHL7Z9Xz7A486O9Sb85AtcUBfpyRuTdS9mgOVuXUeLzxwzuxiTqRRhLv7zOWPBze5nZG97luHkIWQ3AkjIdlurCIqeS2KHlwtdPgqzrAev/Qt38z/dug1bYJkxq3zVUBJR67JHR/WjBP6g8ZvU8dNTNNROU/gaaACnBOgx4IQOtJTAF4x9AWt3TG13opXlo/Pr5HD2FtWIcmPYf+cKLqtGd6tbN+GZvTgDsEic2w+HH9c5bRTHlBVcCvr3w==</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";
  public static Color ColorLobbyText = (Color) new Color32(byte.MaxValue, (byte) 210, (byte) 83, byte.MaxValue);
  public static Color ColorGameText = (Color) new Color32((byte) 85, (byte) 223, (byte) 85, byte.MaxValue);
  public static Color ColorMiniGameText = (Color) new Color32((byte) 115, (byte) 180, (byte) 115, byte.MaxValue);
  public static Color ColorSentPrivate = (Color) new Color32((byte) 115, (byte) 100, byte.MaxValue, byte.MaxValue);
  public static Color ColorReceivedPrivate = (Color) new Color32((byte) 194, (byte) 80, (byte) 65, byte.MaxValue);
  public static Color ColorGrayChat = new Color(0.5f, 0.5f, 0.5f);
  public static Color ColorClanText = (Color) new Color32(byte.MaxValue, (byte) 142, (byte) 0, byte.MaxValue);
  public static Color ColorSystem = Color.red;
  public static Color ColorNotification = (Color) new Color32(byte.MaxValue, (byte) 0, (byte) 182, byte.MaxValue);
  public static Color ColorTeamText = Color.cyan;
  public static Color ColorAnnoucement = Color.yellow;
  public static Color ColorWhiteText = Color.white;

  public static MyLocation GetMountOffset(float x, CreatureType waterLord)
  {
    switch (waterLord)
    {
      case CreatureType.Water_Lord:
        return (double) x < 0.0 ? Global.mountOffsetLeftWaterLord : Global.mountOffsetWaterLord;
      case CreatureType.Reindeer:
        return (double) x < 0.0 ? Global.mountOffsetLeftReindeer : Global.mountOffsetReindeer;
      case CreatureType.Goat:
        return (double) x < 0.0 ? Global.mountOffsetLeftReindeer : Global.mountOffsetReindeer;
      case CreatureType.Cogmobile:
        return Global.mountOffsetSteamEngine;
      default:
        return (double) x < 0.0 ? Global.mountOffsetLeft : Global.mountOffset;
    }
  }

  public static bool IsGameMsg(byte x) => x >= (byte) 175;

  public static float Divide(float a, float b) => (double) b == 0.0 ? 0.0f : a / b;

  public static int Divide(int a, int b) => b == 0 ? 0 : a / b;

  public static bool has(this AccountType a, AccountType b) => (a & b) != 0;

  public static bool IsMuted(this AccountType a)
  {
    return (a & (AccountType.Muted | AccountType.Perm_Muted)) != 0;
  }

  public static bool IsPermMuted(this AccountType a) => (a & AccountType.Perm_Muted) != 0;

  public static bool IsChatMsg(byte b)
  {
    return b == (byte) 153 || b == (byte) 152 || b == (byte) 33 || b == (byte) 151 || b == (byte) 150 || b == (byte) 51 || b == (byte) 73 || b == (byte) 97 || b == (byte) 10 || b == (byte) 58 || b == (byte) 56 || b == (byte) 155 || b == (byte) 157;
  }

  public static bool NoPlayerID(byte b)
  {
    return Global.IsChatMsg(b) || b == (byte) 42 || b == (byte) 50 || b == (byte) 57 || b == (byte) 55 || b == (byte) 1 || b == (byte) 37 || b == (byte) 63 || b == (byte) 72;
  }

  public static bool HasPlayerID(byte b) => b >= (byte) 175;

  public static Sprite AddSprite(Sprite s)
  {
    if ((UnityEngine.Object) s != (UnityEngine.Object) null)
      Global._cachedSprites.Add(s);
    return s;
  }

  public static void DestroySprite(Sprite s)
  {
    if (!((UnityEngine.Object) s != (UnityEngine.Object) null) || !Global._cachedSprites.Contains(s))
      return;
    Global._cachedSprites.Remove(s);
    UnityEngine.Object.Destroy((UnityEngine.Object) s.texture);
    UnityEngine.Object.Destroy((UnityEngine.Object) s);
  }

  public static string systemCopyBuffer
  {
    get => UniClipboard.GetText();
    set => UniClipboard.SetText(value);
  }

  public static bool IsMouseOverGameWindow
  {
    get
    {
      return 0.0 <= (double) Input.mousePosition.x && 0.0 <= (double) Input.mousePosition.y && (double) Screen.width >= (double) Input.mousePosition.x && (double) Screen.height >= (double) Input.mousePosition.y;
    }
  }

  public static int loopi(int lastPort, int v1, int v2)
  {
    ++lastPort;
    if (lastPort > v2 || lastPort < v1)
      lastPort = v1;
    return lastPort;
  }

  public static string ToJson(object o) => JsonConvert.SerializeObject(o);

  public static T FromJson<T>(string j) => JsonConvert.DeserializeObject<T>(j);

  public static void SaveCCJ(string name, byte[] sent)
  {
    string getTutorialPath = Global.GetTutorialPath;
    Global.CheckDirectoryExists(getTutorialPath, name);
    File.WriteAllText(getTutorialPath + name + ".arcTutorial", JsonConvert.SerializeObject((object) Tutorial.FromBytes(sent)));
  }

  public static string GetTutorialPath
  {
    get
    {
      string persistentDataPath = Application.persistentDataPath;
      char directorySeparatorChar = Path.DirectorySeparatorChar;
      string str1 = directorySeparatorChar.ToString();
      directorySeparatorChar = Path.DirectorySeparatorChar;
      string str2 = directorySeparatorChar.ToString();
      return persistentDataPath + str1 + "Tutorials" + str2;
    }
  }

  public static void SaveTutorial(string name, string sent)
  {
    string getTutorialPath = Global.GetTutorialPath;
    Global.CheckDirectoryExists(getTutorialPath, name);
    if (name.EndsWith(".arcTutorial", StringComparison.OrdinalIgnoreCase))
      File.WriteAllText(getTutorialPath + name, sent);
    else
      File.WriteAllText(getTutorialPath + name + ".arcTutorial", sent);
  }

  public static void SaveTutorialCode(string name, string sent, bool openExternal = true)
  {
    string getTutorialPath = Global.GetTutorialPath;
    Global.CheckDirectoryExists(getTutorialPath, name);
    string str = !name.EndsWith(".arcTutorial2", StringComparison.OrdinalIgnoreCase) ? (!name.EndsWith(".arcTutorial", StringComparison.OrdinalIgnoreCase) ? getTutorialPath + name + ".arcTutorial2" : getTutorialPath + name + "2") : getTutorialPath + name;
    try
    {
      if (File.Exists(str))
        File.WriteAllText(Global.GetTutorialPath + "_bak.arcTutorial2", File.ReadAllText(str));
    }
    catch (Exception ex)
    {
      UnityEngine.Debug.LogError((object) ex.ToString());
    }
    File.WriteAllText(str, sent);
    if (!openExternal)
      return;
    Process.Start(str);
  }

  public static Tutorial OpenTutorial(string name)
  {
    try
    {
      return name.EndsWith(".arcTutorial2", StringComparison.OrdinalIgnoreCase) ? Tutorial.FromCodeOnly(File.ReadAllText(name)) : Tutorial.FromJson(File.ReadAllText(name));
    }
    catch (Exception ex)
    {
      UnityEngine.Debug.LogError((object) ex.ToString());
      return new Tutorial();
    }
  }

  public static string InputString
  {
    get
    {
      if (KeyBoard.IsActive)
        return "";
      foreach (char ch in Input.inputString)
      {
        for (int index = 0; index < Global.BadChars.Length; ++index)
        {
          if ((int) ch == (int) Global.BadChars[index])
            return "";
        }
      }
      return Input.inputString;
    }
  }

  public static void SetPrefBool(string s, bool b) => PlayerPrefs.SetInt(s, b ? 1 : 0);

  public static bool GetPrefBool(string s, bool b) => PlayerPrefs.GetInt(s, b ? 1 : 0) == 1;

  public static void SetPrefColor(string s, Color b)
  {
    PlayerPrefs.SetString(s, Global.ColorToString(b));
  }

  public static Color32 GetPrefColor(string s, Color b)
  {
    return (Color32) Global.ColorFromString(PlayerPrefs.GetString(s, Global.ColorToString(b)));
  }

  public static Color ColorFromString(string s)
  {
    Color color;
    return ColorUtility.TryParseHtmlString("#" + s, out color) ? color : Color.black;
  }

  public static string ColorToString(Color c) => ColorUtility.ToHtmlStringRGB(c);

  public static float ParseTime(string t, float def, bool defMinutes = true)
  {
    float result = 0.0f;
    if (t.EndsWith("s"))
    {
      t = t.Substring(0, t.Length - 1).Trim();
      if (float.TryParse(t, out result))
        return result;
    }
    else if (t.EndsWith("h"))
    {
      t = t.Substring(0, t.Length - 1).Trim();
      if (float.TryParse(t, out result))
        return result * 3600f;
    }
    else if (t.EndsWith("m"))
    {
      t = t.Substring(0, t.Length - 1).Trim();
      if (float.TryParse(t, out result))
        return result * 60f;
    }
    else if (float.TryParse(t, out result))
      return result * (defMinutes ? 60f : 1f);
    return def;
  }

  public static bool CompareByteArrays(byte[] a, byte[] b)
  {
    if (a.Length != b.Length)
      return false;
    for (int index = 0; index < a.Length; ++index)
    {
      if ((int) a[index] != (int) b[index])
        return false;
    }
    return true;
  }

  public static bool Sign(FixedInt f) => f > 0;

  public static MyLocation Velocity(FixedInt angle)
  {
    FixedInt x = (angle + 90) * FixedInt.Deg2Rad;
    return new MyLocation(FixedInt.Cos(x), FixedInt.Sin(x));
  }

  public static MyLocation Velocity(FixedInt angle, FixedInt force)
  {
    FixedInt x = (angle + 90) * FixedInt.Deg2Rad;
    return new MyLocation(FixedInt.Cos(x) * force, FixedInt.Sin(x) * force);
  }

  public static MyLocation VelocityRight(FixedInt angle, FixedInt force)
  {
    FixedInt x = angle * FixedInt.Deg2Rad;
    return new MyLocation(FixedInt.Cos(x) * force, FixedInt.Sin(x) * force);
  }

  public static bool CompareColors(Color32 a, Color32 b)
  {
    return Mathf.Abs((int) a.r - (int) b.r) <= 1 && Mathf.Abs((int) a.b - (int) b.b) <= 1 && Mathf.Abs((int) a.g - (int) b.g) <= 1;
  }

  public static Color32 CombineColors(Color a, Color b)
  {
    Color black = Color.black with
    {
      a = (1f - b.a) * a.a + b.a
    };
    black.r = (float) ((1.0 - (double) b.a) * (double) a.a * (double) a.r + (double) b.a * (double) b.r) / black.a;
    black.g = (float) ((1.0 - (double) b.a) * (double) a.a * (double) a.g + (double) b.a * (double) b.g) / black.a;
    black.b = (float) ((1.0 - (double) b.a) * (double) a.a * (double) a.b + (double) b.a * (double) b.b) / black.a;
    return (Color32) black;
  }

  public static bool combareByteArrays(byte[] a, byte[] b)
  {
    if (a.Length != b.Length)
      return false;
    for (int index = 0; index < a.Length; ++index)
    {
      if ((int) a[index] != (int) b[index])
        return false;
    }
    return true;
  }

  public static byte[] ToBytes<T>(T t)
  {
    using (MemoryStream memoryStream = new MemoryStream())
    {
      using (BsonWriter bsonWriter = new BsonWriter((Stream) memoryStream))
        new JsonSerializer()
        {
          TypeNameHandling = TypeNameHandling.Auto
        }.Serialize((JsonWriter) bsonWriter, (object) t);
      return memoryStream.ToArray();
    }
  }

  public static T FromBytes<T>(byte[] sent)
  {
    using (MemoryStream memoryStream = new MemoryStream(sent))
    {
      using (BsonReader reader = new BsonReader((Stream) memoryStream))
        return new JsonSerializer()
        {
          TypeNameHandling = TypeNameHandling.Auto
        }.Deserialize<T>((JsonReader) reader);
    }
  }

  public static void OpenInMacFileBrowser(string path)
  {
    bool flag = false;
    string path1 = path.Replace("\\", "/");
    if (Directory.Exists(path1))
      flag = true;
    if (!path1.StartsWith("\""))
      path1 = "\"" + path1;
    if (!path1.EndsWith("\""))
      path1 += "\"";
    string arguments = (flag ? "" : "-R ") + path1;
    try
    {
      Process.Start("open", arguments);
    }
    catch (Win32Exception ex)
    {
      ex.HelpLink = "";
    }
  }

  public static void OpenInWinFileBrowser(string path)
  {
    bool flag = false;
    string path1 = path.Replace("/", "\\");
    if (Directory.Exists(path1))
      flag = true;
    try
    {
      Process.Start("explorer.exe", (flag ? "/root," : "/select,") + path1);
    }
    catch (Win32Exception ex)
    {
      ex.HelpLink = "";
    }
  }

  public static void OpenFileLocation(string path)
  {
    string persistentDataPath = Application.persistentDataPath;
    char directorySeparatorChar = Path.DirectorySeparatorChar;
    string str1 = directorySeparatorChar.ToString();
    string str2 = path;
    directorySeparatorChar = Path.DirectorySeparatorChar;
    string str3 = directorySeparatorChar.ToString();
    path = persistentDataPath + str1 + str2 + str3;
    Global.OpenInMacFileBrowser(path);
    Application.OpenURL(path);
  }

  public static void OpenURL(string path)
  {
    try
    {
      if (Application.platform == RuntimePlatform.OSXPlayer)
      {
        Process.Start("open", "\"" + UnityWebRequest.UnEscapeURL(path) + "\"");
        return;
      }
      if (Application.platform == RuntimePlatform.LinuxPlayer)
      {
        Application.OpenURL(path);
        return;
      }
      Application.OpenURL(path);
      return;
    }
    catch (Exception ex)
    {
    }
    Application.OpenURL(path);
  }

  public static DateTime GetNearestHour()
  {
    DateTime now = DateTime.Now;
    return new DateTime(now.Year, now.Month, now.Day, now.Hour, 0, 0);
  }

  public static string IntToTime(int ts, int space = 10)
  {
    ts = Mathf.Abs(ts);
    int num1 = ts % 60;
    int num2 = ts / 60;
    return "<mspace=" + (object) space + ">" + (object) num2 + "</mspace>:<mspace=" + (object) space + ">" + (num1 < 10 ? (object) ("0" + num1.ToString()) : (object) num1.ToString()) + "</mspace>";
  }

  public static T Copy<T>(T source)
  {
    if ((object) source == null)
      return default (T);
    XmlSerializer xmlSerializer = new XmlSerializer(typeof (T));
    using (Stream stream = (Stream) new MemoryStream())
    {
      xmlSerializer.Serialize(stream, (object) source);
      stream.Seek(0L, SeekOrigin.Begin);
      return (T) xmlSerializer.Deserialize(stream);
    }
  }

  public static string MakeSafeForCode(string str)
  {
    str = Regex.Replace(str, "[^a-zA-Z0-9_]", "_", RegexOptions.Compiled);
    if (char.IsDigit(str[0]))
      str = "_" + str;
    return str;
  }

  public static void RedPngMap(string file)
  {
    try
    {
      byte[] data = File.ReadAllBytes(file);
      Texture2D tex = new Texture2D(2, 2);
      if (tex.LoadImage(data))
        Client.map.SetMapSprite(Client.game, tex.GetPixels32(), tex.height, tex.width);
      else
        Client.map.SetMapSprite(Client.game, Global.GetSandBoxSprite());
    }
    catch (Exception ex)
    {
      UnityEngine.Debug.LogError((object) ex);
      Client.map.SetMapSprite(Client.game, Global.GetSandBoxSprite());
    }
  }

  public static void ReadArcMap(string file)
  {
    try
    {
      using (FileStream fileStream = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.None))
      {
        using (myBinaryReader myBinaryReader1 = new myBinaryReader((Stream) fileStream))
        {
          int width = myBinaryReader1.ReadInt32();
          int height = myBinaryReader1.ReadInt32();
          byte[] numArray = myBinaryReader1.ReadBytes();
          using (MemoryStream memoryStream = new MemoryStream(SevenZipHelper.Decompress(numArray)))
          {
            using (myBinaryReader myBinaryReader2 = new myBinaryReader((Stream) memoryStream))
            {
              int length = myBinaryReader2.ReadInt32();
              Client.map.deserializedPixels = new Color32[length];
              for (int index = 0; index < length; ++index)
                Client.map.deserializedPixels[index] = myBinaryReader2.ReadColor32();
            }
          }
          using (MemoryStream memoryStream = new MemoryStream())
          {
            using (myBinaryWriter myBinaryWriter = new myBinaryWriter((Stream) memoryStream))
            {
              myBinaryWriter.Write((byte) 16);
              myBinaryWriter.Write(width);
              myBinaryWriter.Write(height);
              myBinaryWriter.Write(true);
              myBinaryWriter.Write(numArray);
            }
            Client.map.serializedMap = memoryStream.ToArray();
          }
          Client.map.SetMapSprite(Client.game, Client.map.deserializedPixels, height, width);
          MapEnum result;
          if (Enum.TryParse<MapEnum>(Path.GetFileNameWithoutExtension(file), out result))
          {
            if (result == MapEnum.Random || result == MapEnum.Dont_Mind)
              result = MapEnum.Grassy_Hills;
            Client.game.gameFacts.SetMapMode(result);
          }
          else
            Client.game.gameFacts.SetMapMode(MapEnum.Grassy_Hills);
        }
      }
    }
    catch (Exception ex)
    {
      UnityEngine.Debug.LogError((object) ex);
      Client.map.SetMapSprite(Client.game, Global.GetSandBoxSprite());
      Client.game.gameFacts.SetMapMode(MapEnum.Grassy_Hills);
    }
  }

  public static bool ApproximatelyEqualEpsilon(float a, float b, float epsilon)
  {
    float num1 = Math.Abs(a);
    float num2 = Math.Abs(b);
    float num3 = Math.Abs(a - b);
    if ((double) a == (double) b)
      return true;
    return (double) a == 0.0 || (double) b == 0.0 || (double) num3 < 1.1754943508222875E-38 ? (double) num3 < (double) epsilon * 1.1754943508222875E-38 : (double) num3 / (double) Math.Min(num1 + num2, float.MaxValue) < (double) epsilon;
  }

  private static Sprite GetSandBoxSprite() => ClientResources.Instance?._maps[0];

  public static void CheckDirectoryExists(string path, string name)
  {
    if (!Directory.Exists(path))
      Directory.CreateDirectory(path);
    string[] strArray = name.Split('/', '\\', Path.DirectorySeparatorChar);
    if (strArray.Length <= 1)
      return;
    for (int index = 0; index < strArray.Length - 1; ++index)
    {
      path = path + strArray[index] + Path.DirectorySeparatorChar.ToString();
      if (!Directory.Exists(path))
        Directory.CreateDirectory(path);
    }
  }
}

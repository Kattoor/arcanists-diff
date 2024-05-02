// Decompiled with JetBrains decompiler
// Type: ReplayMenu
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using Crosstales.Common.Util;
using Crosstales.FB;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using TMPro;
using UnityEngine;

#nullable disable
public class ReplayMenu : Catalogue
{
  public GameObject buttonOpenFileExplorer;
  public UIOnHover watchButton;
  public UIOnHover saveButton;
  public TMP_Text loading_txt;
  public TMP_Text txt_nameOfGame;
  public TMP_Text txt_version;
  public TMP_Text txt_map;
  public TMP_Text txt_server;
  public TMP_Text txt_name;
  public TMP_Text txt_GameOver;
  public GameObject panelGameOver;
  internal List<ReplayMenu.CleanReplayName> games = new List<ReplayMenu.CleanReplayName>();
  internal string currentGame = "";
  internal ButtonReplayFile selectedGameObject;
  public static string nameWhoRecordedReplay = "";
  public static bool isSpectator = false;
  public UIOnHover buttonDelete;
  public UIOnHover buttonRename;
  public RecycledScrollViewGeneric recycled;
  private string lastClicked = "";

  public static ReplayMenu Instance { get; private set; }

  public void ClickDelete()
  {
    if ((UnityEngine.Object) this.selectedGameObject == (UnityEngine.Object) null)
      return;
    File.Delete(this.currentGame);
    this.selectedGameObject.gameObject.SetActive(false);
    this.selectedGameObject = (ButtonReplayFile) null;
    this.buttonDelete.Interactable(false);
    this.watchButton.Interactable(false);
    this.ToggleButtons(false);
    int index = ReplayMenu.Instance.games.FindIndex((Predicate<ReplayMenu.CleanReplayName>) (xx => string.Equals(xx.path, this.currentGame)));
    if (index <= -1)
      return;
    ReplayMenu.Instance.games.RemoveAt(index);
  }

  private void ToggleButtons(bool v)
  {
    this.saveButton.Interactable(v);
    this.buttonDelete.Interactable(v);
    this.buttonRename.Interactable(v);
  }

  public void ClickRename()
  {
    if ((UnityEngine.Object) this.selectedGameObject == (UnityEngine.Object) null)
      return;
    MyContextMenu myContextMenu = MyContextMenu.Show();
    ButtonReplayFile selectedGameObject = this.selectedGameObject;
    Action<string> a = (Action<string>) (s =>
    {
      if (string.IsNullOrEmpty(s))
        return;
      string str = this.currentGame.Substring(0, this.currentGame.Length - Path.GetFileName(this.currentGame).Length) + s + Path.GetExtension(this.currentGame);
      File.Move(this.currentGame, str);
      int index = this.games.FindIndex((Predicate<ReplayMenu.CleanReplayName>) (xx => string.Equals(xx.path, this.currentGame)));
      if (index > -1)
        this.games[index] = new ReplayMenu.CleanReplayName(s, str);
      this.selectedGameObject.name = str;
      this.selectedGameObject.text.text = s;
      ReplayMenu.Instance.ViewGame(str);
    });
    myContextMenu.AddInput(a);
  }

  public static string GetName(string n)
  {
    if (string.Equals("Strelizia", n))
      return "Mind Flayer";
    return string.Equals("Cyclops", n) ? "Sakuna" : n;
  }

  public void ViewGame(string s)
  {
    this.lastClicked = s;
    string a = "";
    if (!File.Exists(s))
    {
      this.watchButton.Interactable(false);
      this.ToggleButtons(false);
      this.txt_nameOfGame.text = "No File Found";
      this.txt_version.text = "Error";
      this.txt_map.text = "Error";
      this.txt_server.text = "Error";
      this.txt_name.text = "Error";
      this.txt_GameOver.text = "";
      this.panelGameOver.SetActive(false);
    }
    else
    {
      try
      {
        using (FileStream fileStream = new FileStream(s, FileMode.Open, FileAccess.Read, FileShare.None))
        {
          using (myBinaryReader reader = new myBinaryReader((Stream) fileStream))
          {
            a = reader.ReadString();
            ReplayMenu.isSpectator = !reader.ReadBoolean();
            ReplayMenu.nameWhoRecordedReplay = reader.ReadString();
            GameFacts gf = new GameFacts();
            gf.ManualDeserialize(reader, true);
            Client.game = gf.game;
            StringBuilder stringBuilder = new StringBuilder("Players: ");
            for (int index = 0; index < gf.players.Count; ++index)
            {
              stringBuilder.Append(gf.players[index]);
              if (index < gf.players.Count - 1)
                stringBuilder.Append(", ");
            }
            this.txt_nameOfGame.text = Path.GetFileNameWithoutExtension(s);
            this.txt_version.text = "Version: " + a;
            this.txt_map.text = "Map: " + GameFacts.MapName(gf.GetMapMode());
            this.txt_server.text = stringBuilder.ToString();
            this.txt_name.text = "Recorded by: " + ReplayMenu.nameWhoRecordedReplay;
            int num1 = reader.ReadInt32();
            bool flag = false;
            for (int index1 = 0; index1 < num1; ++index1)
            {
              byte[] buffer = reader.ReadBytes();
              if (buffer.Length != 0 && buffer[0] == (byte) 13)
              {
                using (MemoryStream memoryStream = new MemoryStream(buffer))
                {
                  using (myBinaryReader myBinaryReader = new myBinaryReader((Stream) memoryStream))
                  {
                    int num2 = (int) myBinaryReader.ReadByte();
                    bool[] flagArray = new bool[gf.players.Count];
                    for (int index2 = 0; index2 < gf.players.Count; ++index2)
                      flagArray[index2] = myBinaryReader.ReadBoolean();
                    this.txt_GameOver.text = myBinaryReader.ReadString();
                    this.panelGameOver.SetActive(true);
                  }
                }
                flag = true;
                break;
              }
            }
            if (!flag)
            {
              this.txt_GameOver.text = "Did not finish";
              this.panelGameOver.SetActive(true);
            }
          }
        }
        this.currentGame = s;
        this.watchButton.Interactable(true);
        this.ToggleButtons(true);
        this.buttonDelete.Interactable(true);
        this.buttonRename.Interactable(true);
      }
      catch (Exception ex)
      {
        Debug.LogError((object) ex);
        this.watchButton.Interactable(false);
        this.ToggleButtons(false);
        this.txt_nameOfGame.text = "Error Loading Game";
        this.txt_version.text = string.Equals(a, Inert.Version) ? "Error" : "Version: " + a + " mismatches " + Inert.Version;
        this.txt_map.text = "Error";
        this.txt_server.text = "Error";
        this.txt_name.text = "Error";
        this.txt_GameOver.text = "";
        this.panelGameOver.SetActive(false);
      }
    }
  }

  private void Awake()
  {
    ReplayMenu.Instance = this;
    GameObject openFileExplorer = this.buttonOpenFileExplorer;
    if (openFileExplorer == null)
      return;
    openFileExplorer.HideIfWebGL();
  }

  private void OnDestroy()
  {
    if (!((UnityEngine.Object) ReplayMenu.Instance == (UnityEngine.Object) this))
      return;
    ReplayMenu.Instance = (ReplayMenu) null;
  }

  private void Start() => this.Populate();

  private void Populate()
  {
    this.watchButton.Interactable(false);
    this.ToggleButtons(false);
    this.txt_nameOfGame.text = "";
    string persistentDataPath = Application.persistentDataPath;
    char directorySeparatorChar = Path.DirectorySeparatorChar;
    string str1 = directorySeparatorChar.ToString();
    directorySeparatorChar = Path.DirectorySeparatorChar;
    string str2 = directorySeparatorChar.ToString();
    string path1 = persistentDataPath + str1 + "SavedReplays" + str2;
    if (!Directory.Exists(path1))
      Directory.CreateDirectory(path1);
    List<string> list = ((IEnumerable<string>) Directory.GetFiles(path1, "*.arcreplay")).ToList<string>();
    try
    {
      string downloadFolderPath = KnownFolders.GetDownloadFolderPath();
      if (Directory.Exists(downloadFolderPath))
      {
        foreach (string file in Directory.GetFiles(downloadFolderPath, "*.arcreplay"))
          list.Add(file);
      }
    }
    catch (Exception ex)
    {
      Debug.LogError((object) ex.ToString());
    }
    foreach (string path2 in list.OrderByDescending<string, string>((Func<string, string>) (f => f)).ToList<string>())
      this.games.Add(new ReplayMenu.CleanReplayName(Path.GetFileNameWithoutExtension(path2), path2));
    this.recycled.Clear();
    foreach (object game in this.games)
      this.recycled.Add(game);
    if (this.games.Count <= 0)
    {
      this.loading_txt.text = "No Game Data\nPlay some games\nwith recording enabled";
      this.loading_txt.gameObject.SetActive(true);
    }
    this.recycled.Scroll(1f);
    this.recycled._chatScrollbar.normalizedPosition = new Vector2(0.0f, 1f);
  }

  public void press_deleteOld()
  {
    for (int index = this.games.Count - 1; index >= 0; --index)
    {
      if (File.Exists(this.games[index].path))
      {
        using (MemoryStream memoryStream = new MemoryStream(File.ReadAllBytes(this.games[index].path)))
        {
          using (myBinaryReader myBinaryReader = new myBinaryReader((Stream) memoryStream))
          {
            if (!string.Equals(myBinaryReader.ReadString(), Inert.Version, StringComparison.OrdinalIgnoreCase))
            {
              FileOperationAPIWrapper.MoveToRecycleBin(this.games[index].path);
              this.games.RemoveAt(index);
            }
          }
        }
      }
      else
        this.games.RemoveAt(index);
    }
    this.Populate();
  }

  public void press_mainmenu() => Controller.Instance.OpenMenu(Controller.Instance.MenuMain, false);

  public void press_WatchReplay()
  {
    ZGame.replayShowStartPanel = true;
    ZGame.targetTimelineFrame = -1;
    ZGame.init_Replay(this.currentGame);
  }

  public void PressSaveAs()
  {
    if (string.IsNullOrEmpty(this.lastClicked) || !File.Exists(this.lastClicked))
      return;
    string[] strArray = new string[1]{ "arcReplay" };
    string str = Singleton<FileBrowser>.Instance.SaveFile(Path.GetFileNameWithoutExtension(this.lastClicked), Application.persistentDataPath + Path.DirectorySeparatorChar.ToString() + "SavedReplays" + Path.DirectorySeparatorChar.ToString(), "", strArray);
    if (string.IsNullOrEmpty(str) || string.Equals(str, this.lastClicked))
      return;
    File.WriteAllBytes(str, File.ReadAllBytes(this.lastClicked));
  }

  public void PressOpenFrom()
  {
    string str = Singleton<FileBrowser>.Instance.OpenSingleFile("Open Arc2 Replay", Client.DownloadsFolder, "", "arcReplay");
    if (string.IsNullOrEmpty(str) || string.Equals(str, this.lastClicked))
      return;
    this.ViewGame(str);
  }

  public void OpenFileLocation() => Global.OpenFileLocation("SavedReplays");

  public class OldTeams
  {
    public int low;
    public int high;
    public int party;
  }

  public class CleanReplayName
  {
    public string id;
    public string date;
    public string time;
    public string map;
    public string modes;
    public string pCount;
    public string players;
    public string path;

    public CleanReplayName(string s, string path)
    {
      string[] strArray = s.Split(new char[1]{ '_' }, StringSplitOptions.RemoveEmptyEntries);
      if (strArray.Length < 7)
      {
        this.id = s;
        this.path = path;
      }
      else
      {
        long result1 = 0;
        long.TryParse(strArray[0], out result1);
        this.id = result1.ToString();
        DateTime result2;
        this.date = DateTime.TryParseExact(strArray[1], "MM-dd-tt-hh-mm", (IFormatProvider) null, DateTimeStyles.None, out result2) || DateTime.TryParse(strArray[1], out result2) ? result2.ToString("MM/dd") : strArray[1];
        this.time = !strArray[2].EndsWith(nameof (s)) || strArray[2].Length <= 1 ? strArray[2] : strArray[2].Substring(0, strArray[2].Length - 1);
        this.map = strArray[3];
        this.modes = strArray[4];
        this.pCount = strArray[5];
        this.players = strArray[6];
        for (int index = 7; index < strArray.Length; ++index)
          this.players += strArray[index];
        this.path = path;
      }
    }
  }
}

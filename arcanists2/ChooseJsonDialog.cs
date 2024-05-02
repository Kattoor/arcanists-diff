// Decompiled with JetBrains decompiler
// Type: ChooseJsonDialog
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using Educative;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

#nullable disable
public class ChooseJsonDialog : MonoBehaviour
{
  public GameObject buttonOpenFileExplorer;
  public InputFieldPlus input;
  public RectTransform container;
  public GameObject pfabBook;
  public UIOnHover buttonSelect;
  private string selectedBook = "";
  internal string selectedFilePath = "";
  private int selectedDefault = -1;
  private GameObject selectedGameObject;
  public UIOnHover buttonDelete;
  public UIOnHover buttonRename;
  [Header("Hide")]
  public List<GameObject> hideCustom;
  public UIOnHover buttonDefault;
  public UIOnHover buttonCustom;
  public UIOnHover buttonPublic;
  public GameObject buttonNew;
  [Header("Selected")]
  public TMP_Text header;
  public TMP_Text description;
  [Header("Folders")]
  public RectTransform containerFolder;
  public GameObject pfabFolder;
  public static ChooseJsonDialog Instance = (ChooseJsonDialog) null;
  private float lastClick;
  internal ChooseJsonDialog.Viewing custom;
  private Action<string, Tutorial, int> onEnd;
  private Action<string, string, int> onEnd2;
  private static string prefCustom = "prefcustomtut2";
  private string subDir = "";

  public static void Create(
    bool saving,
    ChooseJsonDialog.Viewing custom,
    Action<string, Tutorial, int> onEnd,
    bool allowEmpty = false,
    Action<string, string, int> onEnd2 = null)
  {
    if (!((UnityEngine.Object) ChooseJsonDialog.Instance == (UnityEngine.Object) null))
      return;
    ChooseJsonDialog andApply = Controller.Instance.CreateAndApply<ChooseJsonDialog>(Controller.Instance.dialogChooseJson, Controller.Instance.transform);
    Controller.Instance.Push(andApply.gameObject);
    andApply.onEnd = onEnd;
    andApply.onEnd2 = onEnd2;
    if (custom == ChooseJsonDialog.Viewing.Default)
      custom = (ChooseJsonDialog.Viewing) PlayerPrefs.GetInt(ChooseJsonDialog.prefCustom, 0);
    andApply.custom = custom;
    if (saving)
    {
      andApply.input.onEnd.AddListener(new UnityAction<string>(andApply.OnEndEdit));
      andApply.input.gameObject.SetActive(true);
      andApply.input.SetAsActive();
    }
    else
    {
      andApply.buttonDefault.gameObject.SetActive(true);
      andApply.buttonCustom.gameObject.SetActive(true);
      andApply.buttonPublic.gameObject.SetActive(true);
    }
    if (allowEmpty)
      andApply.buttonNew.SetActive(true);
    andApply.UpdateCustomButtons();
  }

  public void Refresh()
  {
    if (this.custom != ChooseJsonDialog.Viewing.Default || this.selectedDefault < 0)
      return;
    int selectedDefault = this.selectedDefault;
    this.Load();
    this.selectedDefault = -1;
    this.PreviewDefaultBook(selectedDefault);
  }

  public static bool HasCustomTutorials()
  {
    return Directory.Exists(ChooseJsonDialog.GetPath) && Directory.GetFiles(ChooseJsonDialog.GetPath, "*", SearchOption.AllDirectories).Length != 0;
  }

  public void UpdateCustomButtons()
  {
    foreach (GameObject gameObject in this.hideCustom)
      gameObject.SetActive(this.custom != 0);
    GameObject openFileExplorer = this.buttonOpenFileExplorer;
    if (openFileExplorer != null)
      openFileExplorer.HideIfWebGL();
    if (!this.buttonDefault.gameObject.activeSelf)
      return;
    this.buttonDefault.AlwaysOn = this.custom == ChooseJsonDialog.Viewing.Default;
    this.buttonCustom.AlwaysOn = this.custom == ChooseJsonDialog.Viewing.Custom;
    this.buttonPublic.AlwaysOn = this.custom == ChooseJsonDialog.Viewing.Public;
    PlayerPrefs.SetInt(ChooseJsonDialog.prefCustom, (int) this.custom);
  }

  public void ToggleCustom(bool v)
  {
    this.custom = v ? ChooseJsonDialog.Viewing.Custom : ChooseJsonDialog.Viewing.Default;
    this.FinishToggle();
  }

  public void TogglePublic()
  {
    this.custom = ChooseJsonDialog.Viewing.Public;
    this.FinishToggle();
  }

  private void FinishToggle()
  {
    this.header.text = "";
    this.description.text = "";
    this.selectedDefault = -1;
    this.selectedBook = "";
    this.selectedFilePath = "";
    this.buttonSelect.Interactable(false);
    this.UpdateCustomButtons();
    this.Load();
  }

  public void Awake()
  {
    if ((UnityEngine.Object) ChooseJsonDialog.Instance != (UnityEngine.Object) null)
      UnityEngine.Object.Destroy((UnityEngine.Object) ChooseJsonDialog.Instance.gameObject);
    ChooseJsonDialog.Instance = this;
    GameObject openFileExplorer = this.buttonOpenFileExplorer;
    if (openFileExplorer == null)
      return;
    openFileExplorer.HideIfWebGL();
  }

  private void Start() => this.Load();

  private void OnDestroy()
  {
    if (!((UnityEngine.Object) ChooseJsonDialog.Instance == (UnityEngine.Object) this))
      return;
    ChooseJsonDialog.Instance = (ChooseJsonDialog) null;
  }

  private void OnEndEdit(string s)
  {
    if (!this.input.gameObject.activeSelf)
      return;
    s = s.Trim();
    if (string.IsNullOrEmpty(s))
      return;
    this.input.gameObject.SetActive(false);
    this.Save(s);
    UnityEngine.Object.Destroy((UnityEngine.Object) this.gameObject);
  }

  public void ClickDelete()
  {
    if ((UnityEngine.Object) this.selectedGameObject == (UnityEngine.Object) null || this.custom != ChooseJsonDialog.Viewing.Custom)
      return;
    File.Delete(this.selectedFilePath);
    UnityEngine.Object.Destroy((UnityEngine.Object) this.selectedGameObject);
    this.buttonDelete.Interactable(false);
    this.buttonSelect.Interactable(false);
  }

  public void ClickRename()
  {
    if ((UnityEngine.Object) this.selectedGameObject == (UnityEngine.Object) null || this.custom != ChooseJsonDialog.Viewing.Custom)
      return;
    MyContextMenu myContextMenu = MyContextMenu.Show();
    string s = this.selectedFilePath;
    GameObject gameObject = this.selectedGameObject;
    Action<string> a = (Action<string>) (ss =>
    {
      if (string.IsNullOrEmpty(ss))
        return;
      string dir = s.Substring(0, s.Length - Path.GetFileName(s).Length) + ss + Path.GetExtension(s);
      File.Move(s, dir);
      this.selectedFilePath = dir;
      gameObject.GetComponent<UIOnHover>().onRightClick.RemoveAllListeners();
      gameObject.name = dir;
      gameObject.GetComponent<TextMeshProUGUI>().text = ss;
      gameObject.GetComponent<UIOnHover>().onRightClick.AddListener((UnityAction) (() => this.OnRightClick(dir, gameObject)));
    });
    myContextMenu.AddInput(a);
  }

  public void OnRightClick(string s, GameObject gameObject)
  {
    if (!File.Exists(s))
      return;
    MyContextMenu myContextMenu = MyContextMenu.Show();
    myContextMenu.AddSeperator();
    myContextMenu.AddItem("Rename", (Action) (() => MyContextMenu.Show().AddInput((Action<string>) (ss =>
    {
      if (string.IsNullOrEmpty(ss))
        return;
      string dir = s.Substring(0, s.Length - Path.GetFileName(s).Length) + ss + Path.GetExtension(s);
      File.Move(s, dir);
      this.selectedFilePath = dir;
      gameObject.GetComponent<UIOnHover>().onRightClick.RemoveAllListeners();
      gameObject.name = dir;
      gameObject.GetComponent<TextMeshProUGUI>().text = ss;
      gameObject.GetComponent<UIOnHover>().onRightClick.AddListener((UnityAction) (() => this.OnRightClick(dir, gameObject)));
    }))), (Color) ColorScheme.GetColor(MyContextMenu.ColorYellow));
    myContextMenu.AddItem("Delete " + Path.GetFileNameWithoutExtension(s), (Action) (() =>
    {
      File.Delete(s);
      UnityEngine.Object.Destroy((UnityEngine.Object) gameObject);
    }), (Color) ColorScheme.GetColor(MyContextMenu.ColorRed));
    myContextMenu.Rebuild();
  }

  private static string GetPath
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

  private void Load(string subFolder = "")
  {
    if (this.custom == ChooseJsonDialog.Viewing.Public)
    {
      this.PopulatePublic();
    }
    else
    {
      this.subDir = subFolder;
      for (int index = this.container.childCount - 1; index > 0; --index)
        UnityEngine.Object.Destroy((UnityEngine.Object) this.container.GetChild(index).gameObject);
      for (int index = this.containerFolder.childCount - 1; index > 0; --index)
        UnityEngine.Object.Destroy((UnityEngine.Object) this.containerFolder.GetChild(index).gameObject);
      float y = 0.0f;
      if (this.custom == ChooseJsonDialog.Viewing.Default)
      {
        int index = 0;
        foreach (ClientResources.TutorialData tutorial in ClientResources.Instance._tutorials)
        {
          GameObject g = UnityEngine.Object.Instantiate<GameObject>(this.pfabBook, (Transform) this.container);
          RectTransform transform = (RectTransform) g.transform;
          transform.localScale = new Vector3(1f, 1f, 1f);
          transform.anchoredPosition = new Vector2(0.0f, y);
          string s = tutorial.name;
          int tempIndex = index;
          g.name = s;
          if (Client.MyAccount.tutorials[index])
            g.GetComponent<TextMeshProUGUI>().text = "• " + s + " •";
          else
            g.GetComponent<TextMeshProUGUI>().text = s;
          g.GetComponent<UIOnHover>().onClick.AddListener((UnityAction) (() =>
          {
            this.selectedGameObject = g;
            this.PreviewDefaultBook(tempIndex);
          }));
          g.GetComponent<UIOnHover>().onRightClick.AddListener((UnityAction) (() => this.OnRightClick(s, g)));
          y -= transform.sizeDelta.y * transform.localScale.y;
          g.SetActive(true);
          ++index;
        }
        this.container.sizeDelta = new Vector2(this.container.sizeDelta.x, (float) (-(double) y + 20.0));
      }
      else
      {
        string getPath = ChooseJsonDialog.GetPath;
        int length = getPath.Length;
        if (!Directory.Exists(getPath))
          Directory.CreateDirectory(getPath);
        if (!string.IsNullOrEmpty(subFolder))
        {
          getPath += subFolder;
          if (!Directory.Exists(getPath))
            getPath = ChooseJsonDialog.GetPath;
        }
        int num = 0;
        char directorySeparatorChar;
        if (getPath.Length > length)
        {
          num = 64;
          GameObject gameObject1 = UnityEngine.Object.Instantiate<GameObject>(this.pfabFolder, (Transform) this.containerFolder);
          RectTransform transform = (RectTransform) gameObject1.transform;
          transform.anchoredPosition = (Vector2) new Vector3(5f, 0.0f);
          transform.localScale = new Vector3(1f, 1f, 1f);
          transform.GetComponent<TMP_Text>().text = "Top Folder";
          transform.name = "";
          UIOnHover component1 = gameObject1.GetComponent<UIOnHover>();
          component1.textNormalColor = Color.green;
          gameObject1.GetComponent<TMP_Text>().color = Color.green;
          component1.onClick.AddListener((UnityAction) (() => this.Load()));
          gameObject1.SetActive(true);
          GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(this.pfabFolder, (Transform) this.containerFolder);
          RectTransform rect = (RectTransform) gameObject2.transform;
          rect.anchoredPosition = (Vector2) new Vector3(0.0f, -32f);
          rect.localScale = new Vector3(1f, 1f, 1f);
          rect.GetComponent<TMP_Text>().text = "Up One Folder";
          string fullName = Directory.GetParent(Directory.GetParent(getPath).FullName).FullName;
          RectTransform rectTransform = rect;
          string str1;
          if (fullName.Length <= length)
          {
            str1 = "";
          }
          else
          {
            string str2 = fullName.Substring(length);
            directorySeparatorChar = Path.DirectorySeparatorChar;
            string str3 = directorySeparatorChar.ToString();
            str1 = str2 + str3;
          }
          rectTransform.name = str1;
          UIOnHover component2 = gameObject2.GetComponent<UIOnHover>();
          component2.textNormalColor = Color.green;
          gameObject2.GetComponent<TMP_Text>().color = Color.green;
          component2.onClick.AddListener((UnityAction) (() => this.Load(rect.name)));
          gameObject2.SetActive(true);
        }
        string[] directories = Directory.GetDirectories(getPath);
        for (int index = 0; index < directories.Length; ++index)
        {
          GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.pfabFolder, (Transform) this.containerFolder);
          RectTransform rect = (RectTransform) gameObject.transform;
          rect.anchoredPosition = (Vector2) new Vector3(0.0f, (float) (index * -32 - num));
          rect.localScale = new Vector3(1f, 1f, 1f);
          rect.GetComponent<TMP_Text>().text = directories[index].Substring(getPath.Length);
          RectTransform rectTransform = rect;
          string str4 = directories[index].Substring(length);
          directorySeparatorChar = Path.DirectorySeparatorChar;
          string str5 = directorySeparatorChar.ToString();
          string str6 = str4 + str5;
          rectTransform.name = str6;
          gameObject.GetComponent<UIOnHover>().onClick.AddListener((UnityAction) (() => this.Load(rect.name)));
          gameObject.SetActive(true);
        }
        this.containerFolder.sizeDelta = new Vector2(this.containerFolder.sizeDelta.x, (float) (num + directories.Length * 32));
        foreach (string str in ((IEnumerable<string>) Directory.GetFiles(getPath, "*")).Where<string>((Func<string, bool>) (s => s.EndsWith(".arcTutorial", StringComparison.OrdinalIgnoreCase) || s.EndsWith(".arcTutorial2", StringComparison.OrdinalIgnoreCase))).OrderBy<string, string>((Func<string, string>) (f => f)).ToList<string>())
        {
          GameObject g = UnityEngine.Object.Instantiate<GameObject>(this.pfabBook, (Transform) this.container);
          RectTransform transform = (RectTransform) g.transform;
          transform.localScale = new Vector3(1f, 1f, 1f);
          transform.anchoredPosition = new Vector2(0.0f, y);
          string s = str;
          g.name = str;
          g.GetComponent<TextMeshProUGUI>().text = Path.GetFileNameWithoutExtension(g.name);
          g.GetComponent<UIOnHover>().onClick.AddListener((UnityAction) (() =>
          {
            this.selectedGameObject = g;
            this.PreviewBook(s);
          }));
          g.GetComponent<UIOnHover>().onRightClick.AddListener((UnityAction) (() => this.OnRightClick(s, g)));
          y -= transform.sizeDelta.y * transform.localScale.y;
          g.SetActive(true);
        }
        this.container.sizeDelta = new Vector2(this.container.sizeDelta.x, (float) (-(double) y + 20.0));
      }
    }
  }

  internal void PopulatePublic()
  {
    for (int index = this.container.childCount - 1; index > 0; --index)
      UnityEngine.Object.Destroy((UnityEngine.Object) this.container.GetChild(index).gameObject);
    for (int index = this.containerFolder.childCount - 1; index > 0; --index)
      UnityEngine.Object.Destroy((UnityEngine.Object) this.containerFolder.GetChild(index).gameObject);
    TutorialFileRetriever.Get();
    float y = 0.0f;
    foreach (TutorialFileRetriever.Tuts tuts in TutorialFileRetriever.List)
    {
      TutorialFileRetriever.Tuts x = tuts;
      GameObject g = UnityEngine.Object.Instantiate<GameObject>(this.pfabBook, (Transform) this.container);
      RectTransform transform = (RectTransform) g.transform;
      transform.localScale = new Vector3(1f, 1f, 1f);
      transform.anchoredPosition = new Vector2(0.0f, y);
      string str = x.name + " (" + x.version + ")";
      g.name = x.url;
      g.GetComponent<TextMeshProUGUI>().text = str;
      g.GetComponent<UIOnHover>().onClick.AddListener((UnityAction) (() =>
      {
        this.selectedGameObject = g;
        this.PreviewPublicBook(x);
      }));
      y -= transform.sizeDelta.y * transform.localScale.y;
      g.SetActive(true);
    }
    this.container.sizeDelta = new Vector2(this.container.sizeDelta.x, (float) (-(double) y + 20.0));
  }

  public void ClickNew()
  {
    Action<string, Tutorial, int> onEnd = this.onEnd;
    if (onEnd != null)
      onEnd("", new Tutorial(), -1);
    Action<string, string, int> onEnd2 = this.onEnd2;
    if (onEnd2 != null)
      onEnd2("", "--NAME = Unnamed\r\n--DESCRIPTION = No Description\r\n--MAP = 0\r\n--SPELLS = {}\r\n--DEBUG = TRUE\r\n\r\n--CODE--\r\n\r\nfunction main()\r\n    while true do\r\n        --Some lua logic goes here\r\n        --Open the API github page for basic documentation and usage\r\n        coroutine.yield(0)\r\n    end\r\nend\r\n", -1);
    this.Cancel();
  }

  public void Select()
  {
    if (this.custom == ChooseJsonDialog.Viewing.Default)
    {
      int selectedDefault = this.selectedDefault;
      Tutorial tutorial = Tutorial.FromCodeOnly(ClientResources.Instance._tutorials[this.selectedDefault].data.text);
      Action<string, Tutorial, int> onEnd = this.onEnd;
      if (onEnd != null)
        onEnd(Global.GetTutorialPath + ClientResources.Instance._tutorials[this.selectedDefault].name + ".arcTutorial2", tutorial, selectedDefault);
      Action<string, string, int> onEnd2 = this.onEnd2;
      if (onEnd2 != null)
        onEnd2(Global.GetTutorialPath + ClientResources.Instance._tutorials[this.selectedDefault].name + ".arcTutorial2", tutorial.ToCodeOnly(), -1);
      this.Cancel();
    }
    else
    {
      if (string.IsNullOrEmpty(this.selectedBook) || !File.Exists(this.selectedBook))
        return;
      Action<string, Tutorial, int> onEnd = this.onEnd;
      if (onEnd != null)
        onEnd(this.selectedBook, Global.OpenTutorial(this.selectedBook), -1);
      Action<string, string, int> onEnd2 = this.onEnd2;
      if (onEnd2 != null)
        onEnd2(this.selectedBook, File.ReadAllText(this.selectedBook), -1);
      this.Cancel();
    }
  }

  public void Cancel()
  {
    Client.sharingWith = "";
    UnityEngine.Object.Destroy((UnityEngine.Object) this.gameObject);
  }

  public void PreviewDefaultBook(int i)
  {
    if (this.selectedDefault == i && (double) this.lastClick > (double) Time.realtimeSinceStartup)
    {
      this.Select();
    }
    else
    {
      this.selectedDefault = i;
      this.buttonDelete.Interactable(false);
      this.buttonRename.Interactable(false);
      this.lastClick = Time.realtimeSinceStartup + 0.5f;
      try
      {
        int index = i;
        Tutorial tutorial = Tutorial.FromCodeOnly(ClientResources.Instance._tutorials[i].data.text);
        this.header.text = tutorial.Name;
        this.description.text = tutorial.message;
        if (index < Prestige.tutorialInfo.Count)
        {
          if (Client.MyAccount.tutorials[index])
            this.description.text += "\n*<color=red>Rewards already received</color>*";
          else if (Client.MyAccount.prestige != (byte) 0)
            this.description.text += "\n*<color=red>Should have completed this tutorial before prestiging for some easy wands!</color>*";
          else if (Client.offlineMode)
            this.description.text += string.Format("\n*<color=red>Completing this tutorial while logged in will give {0} wands as a reward</color>*", (object) Prestige.tutorialInfo[index].wands);
          else
            this.description.text += string.Format("\n*<color=green>Completing this tutorial will reward you with {0} wands</color>*", (object) Prestige.tutorialInfo[index].wands);
        }
      }
      catch (Exception ex)
      {
      }
      this.buttonSelect.Interactable(true);
    }
  }

  public void PreviewPublicBook(TutorialFileRetriever.Tuts x, bool fromInput = true)
  {
    if (((!(this.selectedBook == x.FileName) ? 0 : ((double) this.lastClick > (double) Time.realtimeSinceStartup ? 1 : 0)) & (fromInput ? 1 : 0)) != 0)
    {
      this.Select();
    }
    else
    {
      this.buttonDelete.Interactable(false);
      this.buttonRename.Interactable(false);
      this.lastClick = Time.realtimeSinceStartup + 0.5f;
      this.selectedBook = x.FileName;
      this.selectedFilePath = x.url;
      if (!File.Exists(this.selectedBook))
      {
        this.header.text = "<color=red>" + x.name + x.version + "</color>";
        this.description.text = "<color=red>Downloading from:</color> <color=yellow>" + x.url + "</color>";
        TutorialFileRetriever.DownloadBook(x);
        this.buttonSelect.Interactable(false);
      }
      else
      {
        try
        {
          Tutorial tutorial = Tutorial.FromCodeOnly(File.ReadAllText(this.selectedBook));
          this.header.text = tutorial.Name;
          this.description.text = tutorial.message;
        }
        catch (Exception ex)
        {
          Debug.LogError((object) ex);
        }
        this.buttonSelect.Interactable(true);
      }
    }
  }

  public void PreviewBook(string file)
  {
    if (this.selectedBook == file && (double) this.lastClick > (double) Time.realtimeSinceStartup)
    {
      this.Select();
    }
    else
    {
      this.buttonDelete.Interactable(true);
      this.buttonRename.Interactable(true);
      this.lastClick = Time.realtimeSinceStartup + 0.5f;
      this.selectedBook = file;
      this.selectedFilePath = file;
      try
      {
        Tutorial tutorial = this.selectedBook.EndsWith(".arcTutorial2", StringComparison.OrdinalIgnoreCase) ? Tutorial.FromCodeOnly(File.ReadAllText(this.selectedBook)) : Tutorial.FromJson(File.ReadAllText(this.selectedBook));
        this.header.text = tutorial.Name;
        this.description.text = tutorial.message;
      }
      catch (Exception ex)
      {
        Debug.LogError((object) ex);
      }
      this.buttonSelect.Interactable(true);
    }
  }

  public void ClickSave()
  {
    string s = this.input.inputText.text.Trim();
    if (string.IsNullOrEmpty(s))
      return;
    this.Save(s);
  }

  private void Save(string s)
  {
    if (string.IsNullOrEmpty(s))
      return;
    Action<string, Tutorial, int> onEnd = this.onEnd;
    if (onEnd != null)
      onEnd(this.subDir + s, (Tutorial) null, -1);
    this.Cancel();
  }

  public void OpenFileLocation() => Global.OpenFileLocation("Tutorials");

  public void OnTouch()
  {
  }

  public enum Viewing
  {
    Default,
    Custom,
    Public,
  }
}

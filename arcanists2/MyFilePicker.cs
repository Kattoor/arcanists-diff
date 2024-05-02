// Decompiled with JetBrains decompiler
// Type: MyFilePicker
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

#nullable disable
public class MyFilePicker : MonoBehaviour
{
  public GameObject buttonOpenFileExplorer;
  public InputFieldPlus input;
  public RectTransform container;
  public GameObject pfabBook;
  public RectTransform[] column;
  public UIOnHover buttonSelect;
  public TMP_Text txtSelected;
  public TMP_Text txtHeader;
  private string selectedBook = "";
  private string selectedFilePath = "";
  private GameObject selectedGameObject;
  public UIOnHover buttonDelete;
  public UIOnHover buttonRename;
  public static MyFilePicker Instance;
  private float lastClick;
  private string folder = "";
  private string ext = "";
  [Header("Folders")]
  public RectTransform containerFolder;
  public GameObject pfabFolder;
  [Header("Share")]
  public UIOnHover buttonShare;
  private Action<string> onEnd;
  private ContentType contentType;
  private string subDir = "";

  public static void Create(
    string header,
    string folder,
    string ext,
    bool saving,
    Action<string> onEnd = null,
    ContentType content = ContentType.GameSettings,
    bool shareable = false)
  {
    Client.sharingWith = "";
    if (!((UnityEngine.Object) MyFilePicker.Instance == (UnityEngine.Object) null))
      return;
    MyFilePicker andApply = Controller.Instance.CreateAndApply<MyFilePicker>(Controller.Instance.dialogFilePicker, Controller.Instance.transform);
    Controller.Instance.Push(andApply.gameObject);
    andApply.txtHeader.text = header;
    andApply.folder = folder;
    andApply.ext = ext;
    andApply.onEnd = onEnd;
    andApply.contentType = content;
    if (saving)
    {
      andApply.input.onEnd.AddListener(new UnityAction<string>(andApply.OnEndEdit));
      andApply.input.gameObject.SetActive(true);
      andApply.input.SetAsActive();
    }
    if ((((UnityEngine.Object) LobbyMenu.instance != (UnityEngine.Object) null || (UnityEngine.Object) UnratedMenu.instance != (UnityEngine.Object) null ? 1 : (!((UnityEngine.Object) RatedMenu.instance != (UnityEngine.Object) null) ? 0 : (!string.IsNullOrEmpty(Client.sharingWith) ? 1 : 0))) & (shareable ? 1 : 0)) != 0)
      andApply.buttonShare.gameObject.SetActive(true);
    andApply.Load();
  }

  public void ClickShare()
  {
    Client.AskToShare(Path.GetFileNameWithoutExtension(this.selectedBook), this.contentType, (object) File.ReadAllBytes(this.selectedBook), true);
    this.Cancel();
  }

  public void Awake()
  {
    if ((UnityEngine.Object) MyFilePicker.Instance != (UnityEngine.Object) null)
      UnityEngine.Object.Destroy((UnityEngine.Object) MyFilePicker.Instance.gameObject);
    MyFilePicker.Instance = this;
    GameObject openFileExplorer = this.buttonOpenFileExplorer;
    if (openFileExplorer == null)
      return;
    openFileExplorer.HideIfWebGL();
  }

  private void OnDestroy()
  {
    if (!((UnityEngine.Object) MyFilePicker.Instance == (UnityEngine.Object) this))
      return;
    MyFilePicker.Instance = (MyFilePicker) null;
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
    if ((UnityEngine.Object) this.selectedGameObject == (UnityEngine.Object) null)
      return;
    File.Delete(this.selectedFilePath);
    UnityEngine.Object.Destroy((UnityEngine.Object) this.selectedGameObject);
    this.selectedGameObject = (GameObject) null;
    this.buttonDelete.Interactable(false);
    this.buttonSelect.Interactable(false);
    this.buttonShare.Interactable(false);
  }

  public void ClickRename()
  {
    if ((UnityEngine.Object) this.selectedGameObject == (UnityEngine.Object) null)
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

  private void Load(string subFolder = "")
  {
    this.subDir = subFolder;
    for (int index = this.container.childCount - 1; index > 3; --index)
      UnityEngine.Object.Destroy((UnityEngine.Object) this.container.GetChild(index).gameObject);
    for (int index = this.containerFolder.childCount - 1; index > 0; --index)
      UnityEngine.Object.Destroy((UnityEngine.Object) this.containerFolder.GetChild(index).gameObject);
    string persistentDataPath1 = Application.persistentDataPath;
    char directorySeparatorChar = Path.DirectorySeparatorChar;
    string str1 = directorySeparatorChar.ToString();
    string folder1 = this.folder;
    directorySeparatorChar = Path.DirectorySeparatorChar;
    string str2 = directorySeparatorChar.ToString();
    string path = persistentDataPath1 + str1 + folder1 + str2;
    int length = path.Length;
    if (!Directory.Exists(path))
      Directory.CreateDirectory(path);
    if (!string.IsNullOrEmpty(subFolder))
    {
      path += subFolder;
      if (!Directory.Exists(path))
      {
        string persistentDataPath2 = Application.persistentDataPath;
        directorySeparatorChar = Path.DirectorySeparatorChar;
        string str3 = directorySeparatorChar.ToString();
        string folder2 = this.folder;
        directorySeparatorChar = Path.DirectorySeparatorChar;
        string str4 = directorySeparatorChar.ToString();
        path = persistentDataPath2 + str3 + folder2 + str4;
      }
    }
    int num1 = 0;
    if (path.Length > length)
    {
      num1 = 64;
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
      string fullName = Directory.GetParent(Directory.GetParent(path).FullName).FullName;
      RectTransform rectTransform = rect;
      string str5;
      if (fullName.Length <= length)
      {
        str5 = "";
      }
      else
      {
        string str6 = fullName.Substring(length);
        directorySeparatorChar = Path.DirectorySeparatorChar;
        string str7 = directorySeparatorChar.ToString();
        str5 = str6 + str7;
      }
      rectTransform.name = str5;
      UIOnHover component2 = gameObject2.GetComponent<UIOnHover>();
      component2.textNormalColor = Color.green;
      gameObject2.GetComponent<TMP_Text>().color = Color.green;
      component2.onClick.AddListener((UnityAction) (() => this.Load(rect.name)));
      gameObject2.SetActive(true);
    }
    string[] directories = Directory.GetDirectories(path);
    for (int index = 0; index < directories.Length; ++index)
    {
      GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.pfabFolder, (Transform) this.containerFolder);
      RectTransform rect = (RectTransform) gameObject.transform;
      rect.anchoredPosition = (Vector2) new Vector3(0.0f, (float) (index * -32 - num1));
      rect.localScale = new Vector3(1f, 1f, 1f);
      rect.GetComponent<TMP_Text>().text = directories[index].Substring(path.Length);
      RectTransform rectTransform = rect;
      string str8 = directories[index].Substring(length);
      directorySeparatorChar = Path.DirectorySeparatorChar;
      string str9 = directorySeparatorChar.ToString();
      string str10 = str8 + str9;
      rectTransform.name = str10;
      gameObject.GetComponent<UIOnHover>().onClick.AddListener((UnityAction) (() => this.Load(rect.name)));
      gameObject.SetActive(true);
    }
    this.containerFolder.sizeDelta = new Vector2(this.containerFolder.sizeDelta.x, (float) (num1 + directories.Length * 32));
    List<string> list = ((IEnumerable<string>) Directory.GetFiles(path, "*" + this.ext)).OrderBy<string, string>((Func<string, string>) (f => f)).ToList<string>();
    float[] numArray = new float[this.column.Length];
    for (int index = 0; index < this.column.Length; ++index)
      numArray[index] = this.column[index].anchoredPosition.y;
    int index1 = 0;
    float num2 = 0.0f;
    foreach (string str11 in list)
    {
      GameObject g = UnityEngine.Object.Instantiate<GameObject>(this.pfabBook, (Transform) this.container);
      RectTransform transform = (RectTransform) g.transform;
      transform.localScale = new Vector3(1f, 1f, 1f);
      transform.anchoredPosition = new Vector2(this.column[index1].anchoredPosition.x, numArray[index1]);
      string s = str11;
      g.name = s;
      g.GetComponent<TextMeshProUGUI>().text = Path.GetFileNameWithoutExtension(g.name);
      g.GetComponent<UIOnHover>().onClick.AddListener((UnityAction) (() =>
      {
        this.selectedGameObject = g;
        this.PreviewBook(g.name);
      }));
      g.GetComponent<UIOnHover>().onRightClick.AddListener((UnityAction) (() => this.OnRightClick(s, g)));
      numArray[index1] -= transform.sizeDelta.y * transform.localScale.y;
      if ((double) numArray[index1] < (double) num2)
        num2 = numArray[index1];
      ++index1;
      if (index1 >= this.column.Length)
        index1 = 0;
      g.SetActive(true);
    }
    this.container.sizeDelta = new Vector2(this.container.sizeDelta.x, (float) (-(double) num2 + 20.0));
  }

  public void Select()
  {
    if (this.onEnd != null)
    {
      this.onEnd(this.selectedBook);
      if (!string.IsNullOrEmpty(Client.sharingWith))
        this.ClickShare();
    }
    this.Cancel();
  }

  public void Cancel()
  {
    Client.sharingWith = "";
    UnityEngine.Object.Destroy((UnityEngine.Object) this.gameObject);
  }

  public void PreviewBook(string file)
  {
    if (this.selectedBook == file && (double) this.lastClick > (double) Time.realtimeSinceStartup)
    {
      this.Select();
    }
    else
    {
      this.lastClick = Time.realtimeSinceStartup + 0.5f;
      this.selectedBook = file;
      this.selectedFilePath = file;
      if (this.input.gameObject.activeInHierarchy)
      {
        this.input.SetText(Path.GetFileNameWithoutExtension(file));
      }
      else
      {
        this.buttonDelete.Interactable(true);
        this.buttonRename.Interactable(true);
        this.buttonSelect.Interactable(true);
        this.buttonShare.Interactable(true);
        this.txtSelected.text = Path.GetFileNameWithoutExtension(file);
      }
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
    string persistentDataPath = Application.persistentDataPath;
    char directorySeparatorChar = Path.DirectorySeparatorChar;
    string str1 = directorySeparatorChar.ToString();
    string folder = this.folder;
    directorySeparatorChar = Path.DirectorySeparatorChar;
    string str2 = directorySeparatorChar.ToString();
    string path1 = persistentDataPath + str1 + folder + str2;
    Global.CheckDirectoryExists(path1, this.subDir + s);
    string path2 = path1 + this.subDir + s + this.ext;
    if (string.IsNullOrEmpty(Path.GetFileNameWithoutExtension(path2)))
      return;
    if (this.onEnd != null)
      this.onEnd(path2);
    this.Cancel();
  }

  public void OpenFileLocation() => Global.OpenFileLocation(this.folder);

  public void OnTouch()
  {
  }
}

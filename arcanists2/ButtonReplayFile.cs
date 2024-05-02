

using System;
using System.IO;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

#nullable disable
public class ButtonReplayFile : MonoBehaviour, IRecycledScrollViewGenericItem
{
  public TMP_Text text;
  public TMP_Text txtDate;
  public TMP_Text txtTime;
  public TMP_Text txtMap;
  public TMP_Text txtModes;
  public TMP_Text txtPlayers;

  public void Setup(string s)
  {
    this.name = s;
    this.text.text = Path.GetFileNameWithoutExtension(s);
  }

  public void OnClick()
  {
    ReplayMenu.Instance.selectedGameObject = this;
    ReplayMenu.Instance.ViewGame(this.name);
  }

  public void OnRightClick()
  {
    if (!File.Exists(this.name))
      return;
    MyContextMenu myContextMenu = MyContextMenu.Show();
    myContextMenu.AddSeperator();
    myContextMenu.AddItem("Rename", (Action) (() => MyContextMenu.Show().AddInput((Action<string>) (s =>
    {
      if (string.IsNullOrEmpty(s))
        return;
      string old = this.name;
      string str = this.name.Substring(0, this.name.Length - Path.GetFileName(this.name).Length) + s + Path.GetExtension(this.name);
      File.Move(this.name, str);
      this.name = str;
      this.text.text = s;
      ReplayMenu.Instance.ViewGame(this.name);
      int index = ReplayMenu.Instance.games.FindIndex((Predicate<ReplayMenu.CleanReplayName>) (xx => string.Equals(xx.path, old)));
      if (index <= -1)
        return;
      ReplayMenu.Instance.games[index] = new ReplayMenu.CleanReplayName(Path.GetFileNameWithoutExtension(str), str);
    }))), (Color) ColorScheme.GetColor(MyContextMenu.ColorYellow));
    myContextMenu.AddItem("Delete " + Path.GetFileNameWithoutExtension(this.name), (Action) (() =>
    {
      File.Delete(this.name);
      this.gameObject.SetActive(false);
      int index = ReplayMenu.Instance.games.FindIndex((Predicate<ReplayMenu.CleanReplayName>) (xx => string.Equals(xx.path, this.gameObject.name)));
      if (index > -1)
        ReplayMenu.Instance.games.RemoveAt(index);
      ReplayMenu.Instance.watchButton.Interactable(false);
    }), (Color) ColorScheme.GetColor(MyContextMenu.ColorRed));
    myContextMenu.Rebuild();
  }

  [ContextMenu("hi")]
  private void hi()
  {
    this.GetComponent<UIOnHover>().onClick.AddListener(new UnityAction(this.OnClick));
    this.GetComponent<UIOnHover>().onRightClick.AddListener(new UnityAction(this.OnRightClick));
  }

  public void Init(object obj, int index)
  {
    if (obj.GetType() == typeof (ReplayMenu.CleanReplayName))
    {
      ReplayMenu.CleanReplayName cleanReplayName = (ReplayMenu.CleanReplayName) obj;
      this.name = cleanReplayName.path;
      this.text.text = cleanReplayName.id;
      this.txtDate.text = cleanReplayName.date;
      this.txtTime.text = cleanReplayName.time;
      this.txtMap.text = cleanReplayName.map;
      this.txtModes.text = cleanReplayName.modes;
      this.txtPlayers.text = cleanReplayName.players;
    }
    else
      this.Setup((string) obj);
  }

  [SpecialName]
  GameObject IRecycledScrollViewGenericItem.get_gameObject() => this.gameObject;
}

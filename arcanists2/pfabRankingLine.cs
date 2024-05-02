

using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

#nullable disable
public class pfabRankingLine : MonoBehaviour, IRecycledScrollViewGenericItem
{
  public TMP_Text txtName;
  public TMP_Text txtRating;
  public TMP_Text txtPlayed;
  public TMP_Text txtWon;
  public TMP_Text txtLost;
  public TMP_Text txtDraw;
  public TMP_Text txtDamage;
  public TMP_Text txtKills;
  public TMP_Text txtWinStreak;
  public TMP_Text txtCurWinStreak;
  public TMP_Text txtSurvived;
  public TMP_Text txtFirstTurn;
  public TMP_Text txtFTW;
  public TMP_Text txtTeam;
  public const string pref = "prefshowrankings";
  public const int MaxItems = 7;

  public void Init(object obj, int index)
  {
    IJsonGames c = (IJsonGames) obj;
    if (string.Equals(c.name, Client.Name))
      this.txtName.color = Color.red;
    else
      this.txtName.color = Color.white;
    RatingsMenu.Instance?.Set(this, c);
  }

  public void ToggleItems(int x)
  {
    for (int i = 0; i <= 7; ++i)
      this.IndexToItem(i).gameObject.SetActive((x & 1 << i) != 0);
  }

  private void OnChanged(int i, bool v)
  {
    int num1 = PlayerPrefs.GetInt("prefshowrankings", 0);
    int num2 = !v ? num1 & ~(1 << i) : num1 | 1 << i;
    this.IndexToItem(i).gameObject.SetActive((num2 & 1 << i) != 0);
    RatingsMenu.Instance.spacers.IndexToItem(i).gameObject.SetActive((num2 & 1 << i) != 0);
    RatingsMenu.Instance.headers.IndexToItem(i).gameObject.SetActive((num2 & 1 << i) != 0);
    foreach (IRecycledScrollViewGenericItem getPfab in RatingsMenu.Instance.recycled.GetPfabs)
    {
      pfabRankingLine pfabRankingLine = getPfab as pfabRankingLine;
      if ((Object) pfabRankingLine != (Object) null)
        pfabRankingLine.IndexToItem(i).gameObject.SetActive((num2 & 1 << i) != 0);
    }
    PlayerPrefs.SetInt("prefshowrankings", num2);
    RatingsMenu.Instance.ResizeContainer();
  }

  public void ShowContextMenu()
  {
    int num = PlayerPrefs.GetInt("prefshowrankings", 0);
    MyContextMenu myContextMenu = MyContextMenu.Show();
    for (int i = 0; i <= 7; ++i)
    {
      int z = i;
      myContextMenu.AddToggle((UnityAction<bool>) (v => this.OnChanged(z, v)), this.IndexToItem(i).gameObject.name, (num & 1 << i) != 0, (Color) ColorScheme.GetColor(MyContextMenu.ColorCream));
    }
    myContextMenu.Rebuild();
  }

  public TMP_Text IndexToItem(int i)
  {
    switch (i)
    {
      case 0:
        return this.txtDamage;
      case 1:
        return this.txtKills;
      case 2:
        return this.txtWinStreak;
      case 3:
        return this.txtCurWinStreak;
      case 4:
        return this.txtSurvived;
      case 5:
        return this.txtFirstTurn;
      case 6:
        return this.txtFTW;
      case 7:
        return this.txtTeam;
      default:
        return (TMP_Text) null;
    }
  }

  public int ItemIndex(TMP_Text txt)
  {
    if ((Object) txt == (Object) this.txtDamage)
      return 0;
    if ((Object) txt == (Object) this.txtKills)
      return 1;
    if ((Object) txt == (Object) this.txtWinStreak)
      return 2;
    if ((Object) txt == (Object) this.txtCurWinStreak)
      return 3;
    if ((Object) txt == (Object) this.txtSurvived)
      return 4;
    if ((Object) txt == (Object) this.txtFirstTurn)
      return 5;
    if ((Object) txt == (Object) this.txtFTW)
      return 6;
    return (Object) txt == (Object) this.txtTeam ? 7 : -1;
  }

  [SpecialName]
  GameObject IRecycledScrollViewGenericItem.get_gameObject() => this.gameObject;
}

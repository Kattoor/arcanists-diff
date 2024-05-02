

using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

#nullable disable
public class ClanMenu : MonoBehaviour
{
  public RectTransform container;
  public UIClanHeader pfab;
  public TMP_Text headerWhich;
  public GameObject loading;
  public static List<Clan> clans = new List<Clan>();
  public static float lastGet = 0.0f;
  public ClanMenu.Sorter sorter;

  public static ClanMenu Instance { get; private set; }

  private void Awake()
  {
    ClanMenu.Instance = this;
    DiscordIntergration.Instance?.UpdateNoUpdate();
  }

  private void OnDestroy()
  {
    if ((UnityEngine.Object) ClanMenu.Instance == (UnityEngine.Object) this)
      ClanMenu.Instance = (ClanMenu) null;
    DiscordIntergration.Instance?.UpdateNoUpdate();
  }

  private void Start()
  {
    if ((double) ClanMenu.lastGet > (double) Time.realtimeSinceStartup)
    {
      this.Populate();
    }
    else
    {
      ClanMenu.lastGet = Time.realtimeSinceStartup + 60f;
      Client.Ask((byte) 84, (byte) 8);
    }
  }

  public void ClickBack() => UnityEngine.Object.Destroy((UnityEngine.Object) this.gameObject);

  [EnumAction(typeof (ClanMenu.Sorter))]
  public void ClickSorter(int i)
  {
    this.sorter = (ClanMenu.Sorter) i;
    this.Populate();
  }

  private void Sort()
  {
    switch (this.sorter)
    {
      case ClanMenu.Sorter.DateCreated:
        this.headerWhich.text = "(Sorted by Date Created)";
        ClanMenu.clans.Sort((Comparison<Clan>) ((a, b) => a.creationDate.CompareTo(b.creationDate)));
        break;
      case ClanMenu.Sorter.Members:
        this.headerWhich.text = "(Sorted by Member Count)";
        ClanMenu.clans.Sort((Comparison<Clan>) ((a, b) => b.clientMemberCount - a.clientMemberCount));
        break;
      case ClanMenu.Sorter.Alphanumeric:
        this.headerWhich.text = "(Sorted Alphanumerically)";
        ClanMenu.clans.Sort((Comparison<Clan>) ((a, b) => a.name.CompareTo(b.name)));
        break;
    }
  }

  public void Populate()
  {
    this.loading.SetActive(false);
    this.container.sizeDelta = new Vector2(this.container.sizeDelta.x, (float) (53 * ClanMenu.clans.Count));
    this.container.DestroyChildern();
    this.Sort();
    for (int index = 0; index < ClanMenu.clans.Count; ++index)
    {
      UIClanHeader uiClanHeader = UnityEngine.Object.Instantiate<UIClanHeader>(this.pfab, (Transform) this.container);
      ((RectTransform) uiClanHeader.transform).anchoredPosition = new Vector2(0.0f, (float) (index * -53));
      uiClanHeader.Setup(ClanMenu.clans[index]);
      uiClanHeader.gameObject.SetActive(true);
    }
  }

  public enum Sorter
  {
    DateCreated,
    Members,
    Alphanumeric,
  }
}

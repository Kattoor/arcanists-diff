// Decompiled with JetBrains decompiler
// Type: PrestigeMenu
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

#nullable disable
public class PrestigeMenu : MonoBehaviour
{
  public RectTransform bgrect;
  public GameObject[] panels;
  public UIOnHover[] buttons;
  public TMP_Text txtMinionSwitch;
  public TMP_Text txtPrestigeRequirments;
  public TMP_Text txtExperience;
  [Header("Spell Types")]
  public Image[] pfabSpellTypes;
  private bool spellIconsInit;
  [Header("Map")]
  public UIOnHover pfabMapButton;
  public Image mapImage;
  public Image armageddonImage;
  public TMP_Text mapDescription;
  private List<UIOnHover> mapButtons = new List<UIOnHover>();
  private int viewing = -1;
  private List<string> mapLore = new List<string>()
  {
    "These green slopes are where all young Arcanists train to become great mages. All that can be found here is a clean battleground in which to test your skills.",
    "Steep slopes and rugged terrain. Stay here too long and the volcanoes will erupt!",
    "These living islands offer much in the way of places to hide, but watch out, as the ancient magics guarding the area will start raining comets down!",
    "The dank caves of the goblins are a common battleground. Intense mining has disturbed the deep and the ground is liable to erupt from under your feet!",
    "Amid the witches' huts and rotten trees, Arcanists battle to control the arcane sources of magic within the swamp. Keep hidden, as the high concentration of power draws storms in from afar.",
    "The gentle slopes of the graveyard are covered with the remains of past Arcanists who have failed in this arena. Evil spirits haunt this place, sending bolts of pure chaos down onto anyone who disturbs them.",
    "The angels of the sky have opened their heavenly gates to allow Arcanists to engage in battle between their castles in the clouds. Choose your tower and allies carefully to achieve victory in this realm.",
    "Make your way to the islands of Mos Le'Harmless to get your hands on some 'rum', but watch out for the monkeys - they might just rain on your parade.",
    "This map will provide a very different style of gameplay; imps will surely find their home there...",
    "It's Christmas time in the grassy hills where young arcanists train! Keep an eye out for presents falling from the sky. These presents may only hurt a little, but grants the victim 2 additional random spells!",
    "Ever wondered where those drowned arcanists end up at? The Ocean Floor is where their sunken corpses are laid rest at.  Beware of any occasional brine storms and falling clams approaching the seabed!",
    "Random...",
    "The demons have opened their hellish gateways to allow Arcanists to engage in battle atop of their towering fortress which is slowly enveloped by lava.",
    "A polluted island created by a band of Cog Arcanists, which is the home of their tinkering around. The sheer pollution of the place is so unhealthy for the environment that the clouds in the sky occasionally rain acid!",
    "Ancient ruins teeming with arcane energy. Once a home to the mightiest mages. Stay long enough and you might witness a small portion of their arcane prowess.",
    "Foliage and overgrowth covers these ancient and consecrated grounds. They say the wood they use from the trees of the Kharazi Jungle is full of magic, but the bark is not what you need to watch out for, for there is a greater threat that lies within.",
    "Organic landscapes of a distant planet, with many places to hide, who knows what lays inside. Be careful of shooting stars on either side!",
    "These ethereal halls were once a bastion of great magical power, now haunted by restless spirits. Eerie whispers and flickering apparitions fill the air as the echoes of past enchantments linger."
  };

  public static PrestigeMenu Instance { get; private set; }

  private void Awake()
  {
    PrestigeMenu.Instance = this;
    if (!((UnityEngine.Object) LobbyMenu.instance == (UnityEngine.Object) null) || !((UnityEngine.Object) UnratedMenu.instance == (UnityEngine.Object) null) || !((UnityEngine.Object) RatedMenu.instance == (UnityEngine.Object) null))
      return;
    this.bgrect.anchoredPosition = Vector2.zero;
  }

  private void Start() => this.TogglePanel(0);

  private void OnDestroy()
  {
    if (!((UnityEngine.Object) PrestigeMenu.Instance == (UnityEngine.Object) this))
      return;
    PrestigeMenu.Instance = (PrestigeMenu) null;
  }

  public void TogglePanel(int e)
  {
    for (int index = 0; index < this.panels.Length; ++index)
    {
      this.panels[index].SetActive(index == e);
      this.buttons[index].AlwaysOn = index == e;
    }
    switch (e)
    {
      case 0:
        if (Client.MyAccount.prestige >= (byte) 10)
        {
          this.txtPrestigeRequirments.text = string.Format(this.txtPrestigeRequirments.text, (object) "<color=green>Congratulations, You've reached the highest prestige!");
          break;
        }
        if (Prestige.ReadyToPrestige(Client.MyAccount) && Prestige.AboveRating(Client.MyAccount))
        {
          this.txtPrestigeRequirments.text = string.Format(this.txtPrestigeRequirments.text, (object) ("<color=green>Requirements for the</color> " + Account.numberplus((int) Client.MyAccount.prestige + 1) + "<color=green> prestige =</color> " + (object) Prestige.RequiredRating(Client.MyAccount) + "<color=green> rating and</color> " + (object) Prestige.MaxWands(Client.MyAccount) + "<color=green> wands.</color>"));
          break;
        }
        this.txtPrestigeRequirments.text = string.Format(this.txtPrestigeRequirments.text, (object) ("<color=red>Requirements for the</color> " + Account.numberplus((int) Client.MyAccount.prestige + 1) + "<color=red> prestige =</color> " + (object) Prestige.RequiredRating(Client.MyAccount) + "<color=red> rating and</color> " + (object) Prestige.MaxWands(Client.MyAccount) + "<color=red> wands.</color>"));
        break;
      case 1:
        this.txtExperience.text = string.Format(this.txtExperience.text, (object) ("<color=red>You are level</color> " + (object) Client.MyAccount.experience + " <color=red>and have</color> " + (object) (int) Client.MyAccount.bonusExperience + "/" + (object) Client.MyAccount.RequiredPoints() + " <color=red>experience. With</color> " + (object) Achievements.CalculatePoints(Client.cosmetics) + "/" + (object) Client.MyAccount.RequiredAchievements() + " <color=red>achievement points.</color>"));
        break;
      case 2:
        this.txtMinionSwitch.text = string.Format(this.txtMinionSwitch.text, (object) hardInput.GetKeyName("Minions", false), (object) hardInput.GetKeyName("Minions", true));
        break;
      case 3:
        if (this.spellIconsInit)
          break;
        this.spellIconsInit = true;
        this.CreateSpellIcons();
        break;
      case 5:
        this.SetupMaps();
        break;
    }
  }

  private void CreateSpellIcons()
  {
    foreach (Spell spell in Inert.Instance._spells)
    {
      switch (spell.spellType)
      {
        case SpellType.None:
          Debug.Log((object) ("None -" + spell.name));
          break;
        case SpellType.Ball:
          this.CreateSpellIcon(this.pfabSpellTypes[0], spell);
          break;
        case SpellType.Bomb:
          this.CreateSpellIcon(this.pfabSpellTypes[1], spell);
          break;
        case SpellType.Aura:
          this.CreateSpellIcon(this.pfabSpellTypes[5], spell);
          break;
        case SpellType.Arrow:
          this.CreateSpellIcon(this.pfabSpellTypes[2], spell);
          break;
        case SpellType.Tower:
          this.CreateSpellIcon(this.pfabSpellTypes[6], spell);
          break;
        case SpellType.Target:
          this.CreateSpellIcon(this.pfabSpellTypes[5], spell);
          break;
        case SpellType.Bolt:
          this.CreateSpellIcon(this.pfabSpellTypes[3], spell);
          break;
        case SpellType.Summon:
          this.CreateSpellIcon(this.pfabSpellTypes[7], spell);
          break;
        case SpellType.Blit:
          this.CreateSpellIcon(this.pfabSpellTypes[5], spell);
          break;
        case SpellType.Self:
          this.CreateSpellIcon(this.pfabSpellTypes[4], spell);
          break;
        case SpellType.Flash:
          this.CreateSpellIcon(this.pfabSpellTypes[4], spell);
          break;
        case SpellType.NonAttack:
          this.CreateSpellIcon(this.pfabSpellTypes[4], spell);
          break;
      }
    }
  }

  private void CreateSpellIcon(Image pfab, Spell s)
  {
    if (pfab.transform.parent.childCount >= 19 || string.Equals(pfab.transform.parent.parent.GetChild(3).GetComponent<Image>().sprite.name, s.name))
      return;
    Image image = UnityEngine.Object.Instantiate<Image>(pfab, pfab.transform.parent);
    image.sprite = ClientResources.Instance.icons[s.name];
    image.gameObject.SetActive(true);
  }

  private void SetupMaps()
  {
    if (this.pfabMapButton.transform.parent.childCount > 1)
      return;
    int num = 0;
    List<MapEnum> list = ((IEnumerable<MapEnum>) Enum.GetValues(typeof (MapEnum))).ToList<MapEnum>();
    list.Sort((Comparison<MapEnum>) ((a, b) => GameFacts.GetMapIndex(a) - GameFacts.GetMapIndex(b)));
    foreach (MapEnum e in list)
    {
      switch (e)
      {
        case MapEnum.Dont_Mind:
        case MapEnum.Random:
          continue;
        default:
          MapEnum index = e;
          int button = num;
          UIOnHover uiOnHover = UnityEngine.Object.Instantiate<UIOnHover>(this.pfabMapButton, this.pfabMapButton.transform.parent);
          uiOnHover.onClick.AddListener((UnityAction) (() => this.ClickMap(index, button)));
          uiOnHover.GetComponent<TMP_Text>().text = GameFacts.MapName(e);
          uiOnHover.gameObject.SetActive(true);
          this.mapButtons.Add(uiOnHover);
          ++num;
          continue;
      }
    }
    this.ClickMap(MapEnum.Grassy_Hills, 0);
  }

  private void ClickMap(MapEnum x, int button)
  {
    int mapIndex = GameFacts.GetMapIndex(x);
    this.mapDescription.text = "Map: " + GameFacts.MapName(x) + "\nArmageddon: " + GameFacts.ArmageddonName(x, GameStyle.No_Movement) + "\n\n" + this.mapLore[mapIndex];
    this.mapImage.sprite = ClientResources.Instance.mapBgBig[mapIndex];
    this.armageddonImage.sprite = Inert.Instance.ArmageddonIcons[mapIndex];
    for (int index = 0; index < this.mapButtons.Count; ++index)
      this.mapButtons[index].AlwaysOn = index == button;
  }

  public void ClickClose() => UnityEngine.Object.Destroy((UnityEngine.Object) this.gameObject);
}

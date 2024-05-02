

using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

#nullable disable
public class ContextMenuSpells : MonoBehaviour
{
  public Image p;
  public Transform g;

  private void Start()
  {
  }

  public void Setup(UnityAction<SpellEnum> a) => this.StartCoroutine(this.DelayMake(a));

  private void Update()
  {
  }

  private IEnumerator DelayMake(UnityAction<SpellEnum> a)
  {
    HashSet<SpellEnum> spellEnumSet = new HashSet<SpellEnum>();
    List<KeyValuePair<string, Spell>> spell = new List<KeyValuePair<string, Spell>>();
    Stopwatch sw = new Stopwatch();
    foreach (KeyValuePair<string, Spell> spell1 in Inert.Instance.spells)
    {
      if (spellEnumSet.Add(spell1.Value.spellEnum) && (spell1.Value.level <= 3 || GameFacts.AllowCustomArmageddon(spell1.Value.spellEnum)))
        spell.Add(spell1);
    }
    sw.Start();
    for (int i = 0; i < spell.Count; ++i)
    {
      KeyValuePair<string, Spell> x = spell[i];
      SpellEnum e = x.Value.spellEnum;
      Image image = Object.Instantiate<Image>(this.p, this.g);
      image.sprite = ClientResources.Instance.GetSpellIcon(x.Key);
      UIOnHover component = image.gameObject.GetComponent<UIOnHover>();
      component.onClick.AddListener((UnityAction) (() =>
      {
        a(e);
        MyContextMenu.CloseInstance();
      }));
      component.onEnter.AddListener((UnityAction) (() => MyToolTip.Show(x.Key)));
      component.onExit.AddListener((UnityAction) (() => MyToolTip.Close()));
      image.gameObject.SetActive(true);
      if (sw.ElapsedMilliseconds > 16L)
      {
        sw.Reset();
        yield return (object) null;
        sw.Start();
      }
    }
    sw.Stop();
  }
}

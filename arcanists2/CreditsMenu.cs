

using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
public class CreditsMenu : MonoBehaviour
{
  public static CreditsMenu Instance;
  public CreditsMenu.Type type;
  public TMP_Text txt;
  public ScrollRect scroller;
  public TMP_Text headerTxt;
  private const string txtRules = "\r\n\r\n<sprite name=\"Emoji2_153\"> <#FFFFFF><b><u>Prohibited Language & Behavior (Discord & In-Game):</b></u></color>\r\n• Disrespectful, insulting, accusatory and inflammatory language or behavior in server channels and in-game.*\r\n• No NSFW/sexually suggestive content\r\n• Defamation\r\n• Impersonation\r\n• Discriminatory language/behavior of any kind (racism, sexism, homophobia, etc.)\r\n• Political discussion principally regarding race\r\n• Espousing of ideas that are considered to be both generally offensive and reasonably fall clearly outside of the Overton window (defined as, \"the range of policies politically acceptable to the mainstream population at a given time\")\r\n• Malicious threats of any kind(violence, DDoS, DoX, etc...)\r\n• Encouragement of self-harm\r\n• Targeted and attempted targeted harassment\r\n• Please refrain from advertising our Discord Server or Website in other servers where advertising is prohibited.\r\n\r\n<sprite name=\"Emoji2_204\"> <#FFFFFF><b><u>No Cheating/Bug Abuse (In-Game):</b></u></color>\r\n• Including but not limited to any form of bug abuse, use of client mods or aim-bot usage. Use of such may be grounds for an automatic ban. [Exceptions to this include inside of the sandbox.]\r\n• The act of xferring (transferring rating, or an act of rating manipulation) is strictly prohibited in order to maintain a balanced and fun game experience. This includes wand drawing for Prestige Hats, as well as participating in the act of teamkilling. This includes gaining levels for Prestige Hats. This is monitored and we will review suspicious activity on a case-by-case basis. <b>Please also avoid playing rated games with friends/family members in the same household (unrated games are fine). Giving this as a reason will not change the outcome if we deem you guilty. Anyone found guilty of xfering may have their rating/prestige/achievements reset.</b>\r\n\r\n<b>Note:</b> In the case of bug abuse or if such cheating is used unfairly against you, please report abuse with info relating to the injustice. We may be able to refund your rating if this occurs.\r\n\r\n<sprite name=\"Emoji2_255\"> <#FFFFFF><b><u>Alt Account Policy (Discord & In-Game):</b></u></color>\r\n• A player may have no more than 3 in-game accounts total.\r\n• Account sharing is prohibited.\r\n• There should not be any alts in the Discord server. This is to protect against ban and punishment evasion.\r\n\r\n<sprite name=\"Emoji2_306\"> <#FFFFFF><b><u>Misuse of the Report Abuse function is prohibited</b></u></color>\r\n\r\n<sprite name=\"Emoji2_357\"> <#FFFFFF><b><u>Try to be respectful to everyone (Discord & In-Game):</b></u></color>\r\n• Acting as a general community nuisance will not be tolerated. This includes excessive or repeated outright disrespectful treatment of the staff (even outside of the server itself ie. DMs in relation to the server/game).\r\n• We are a growing community and it is important to be mindful that particularly negative behavior can impact on this.\r\n\r\n\r\n\r\n";
  private const string txtCredits = "\r\n\r\n(In-game names unless otherwise stated)\r\n\r\n<#00FFFF>Programming/Development:</color>\r\npur3 extreme\r\n\r\n<#00FFFF>Project Lead:</color>\r\nSiggy\r\n\r\n<#995533>Technical Assistant:</color>\r\nkosmos_prime (Discord)\r\n\r\n<#FF0000>Spellbook Art:</color>\r\nQuinn\r\nRain\r\nStrelizia\r\n\r\n\r\n<#FFFF00>Map Art:</color>\r\nRain\r\nStrelizia\r\ngoblinking#9185 (Discord)\r\nKaoe\r\nSpeedymatt Z\r\n\r\n<#00FF00>Character Art:</color>\r\nQuinn\r\nKaoe\r\nMoin the Mine\r\nFurious\r\nDon Rufus\r\nHarvicus\r\nSyndicate\r\nclint\r\ngrapplepi\r\nNev\r\npur3 extreme\r\nSiggy\r\nSkittlez\r\nRain\r\nSpeedymatt Z\r\nStrelizia\r\nancient_sage\r\nrbd\r\nlmaaoooo#1847 (Discord)\r\nMagmar#6885 (Discord)\r\npzUH - <u><link=https://opengameart.org/users/pzuh>OGA</link></u> - License - <u><link=https://creativecommons.org/publicdomain/zero/1.0/>CCO</link></u>\r\nKenney - <u><link=http://www.kenney.nl/>kenney.nl</link></u> - License - <u><link=https://creativecommons.org/publicdomain/zero/1.0/>CCO</link></u>\r\n\r\n<#FF00FF>Lobby Player Icons:</color>\r\nQuinn\r\nKaoe\r\n[Mod] Cruel\r\nRain\r\n\r\n<#8866FF>UI/Other Art:</color>\r\n[Mod] Cruel\r\nrbd\r\ngrapplepi\r\nFurious\r\nancient_sage\r\nnetgfx - <u><link=https://opengameart.org/users/netgfx>OGA</link></u> License - <u><link=https://creativecommons.org/licenses/by/3.0/>CC-BY 3.0</link></u>\r\nKokoroReflections - <u><link=https://opengameart.org/users/kokororeflections>OGA</link></u> - License - <u><link=https://creativecommons.org/licenses/by/4.0/>CC-BY 4.0</link></u>\r\nKenney - <u><link=http://www.kenney.nl/>kenney.nl</link></u> - License - <u><link=https://creativecommons.org/publicdomain/zero/1.0/>CCO</link></u>\r\n\r\n<#FF7744>Sound Effects:</color>\r\nSkulls\r\n\r\n<#99FF55>Music:</color>\r\nSkulls\r\n\r\n<#3333FF>Tutorials:</color>\r\npur3 extreme\r\nQuinn\r\ngrapplepi\r\n\r\n<#229999>Website:</color>\r\nSiggy\r\nFrog\r\nclint\r\n\r\n<#663366>Chess UI:</color>\r\nFurious\r\nStrelizia\r\nJohnPablok's chess set <u><link=https://opengameart.org/content/chess-pieces-and-board-squares>OGA</link></u> - License - <u><link=https://creativecommons.org/licenses/by-sa/3.0/>CC-BY-SA 3.0</link></u>\r\nScreaming Brain Studios <u><link=https://opengameart.org/content/2d-chess-pack>OGA</link></u> - License - <u><link=https://creativecommons.org/publicdomain/zero/1.0/>CCO</link></u>\r\n\r\n<u>Original game assets made by Jagex</u>\r\n        \r\n\r\n\r\n";

  private void Awake() => CreditsMenu.Instance = this;

  private void OnDestroy()
  {
    if ((Object) CreditsMenu.Instance == (Object) this)
      CreditsMenu.Instance = (CreditsMenu) null;
    DiscordIntergration.Instance?.UpdateNoUpdate();
    AudioManager.ResumeMusic();
  }

  public void Init(CreditsMenu.Type t, string ztxt = null)
  {
    this.type = t;
    AudioManager.PlayMusic(AudioManager.instance?.musicCredits);
    this.txt.text = this.type != CreditsMenu.Type.Credits ? (this.type != CreditsMenu.Type.Rules ? ztxt : "\r\n\r\n<sprite name=\"Emoji2_153\"> <#FFFFFF><b><u>Prohibited Language & Behavior (Discord & In-Game):</b></u></color>\r\n• Disrespectful, insulting, accusatory and inflammatory language or behavior in server channels and in-game.*\r\n• No NSFW/sexually suggestive content\r\n• Defamation\r\n• Impersonation\r\n• Discriminatory language/behavior of any kind (racism, sexism, homophobia, etc.)\r\n• Political discussion principally regarding race\r\n• Espousing of ideas that are considered to be both generally offensive and reasonably fall clearly outside of the Overton window (defined as, \"the range of policies politically acceptable to the mainstream population at a given time\")\r\n• Malicious threats of any kind(violence, DDoS, DoX, etc...)\r\n• Encouragement of self-harm\r\n• Targeted and attempted targeted harassment\r\n• Please refrain from advertising our Discord Server or Website in other servers where advertising is prohibited.\r\n\r\n<sprite name=\"Emoji2_204\"> <#FFFFFF><b><u>No Cheating/Bug Abuse (In-Game):</b></u></color>\r\n• Including but not limited to any form of bug abuse, use of client mods or aim-bot usage. Use of such may be grounds for an automatic ban. [Exceptions to this include inside of the sandbox.]\r\n• The act of xferring (transferring rating, or an act of rating manipulation) is strictly prohibited in order to maintain a balanced and fun game experience. This includes wand drawing for Prestige Hats, as well as participating in the act of teamkilling. This includes gaining levels for Prestige Hats. This is monitored and we will review suspicious activity on a case-by-case basis. <b>Please also avoid playing rated games with friends/family members in the same household (unrated games are fine). Giving this as a reason will not change the outcome if we deem you guilty. Anyone found guilty of xfering may have their rating/prestige/achievements reset.</b>\r\n\r\n<b>Note:</b> In the case of bug abuse or if such cheating is used unfairly against you, please report abuse with info relating to the injustice. We may be able to refund your rating if this occurs.\r\n\r\n<sprite name=\"Emoji2_255\"> <#FFFFFF><b><u>Alt Account Policy (Discord & In-Game):</b></u></color>\r\n• A player may have no more than 3 in-game accounts total.\r\n• Account sharing is prohibited.\r\n• There should not be any alts in the Discord server. This is to protect against ban and punishment evasion.\r\n\r\n<sprite name=\"Emoji2_306\"> <#FFFFFF><b><u>Misuse of the Report Abuse function is prohibited</b></u></color>\r\n\r\n<sprite name=\"Emoji2_357\"> <#FFFFFF><b><u>Try to be respectful to everyone (Discord & In-Game):</b></u></color>\r\n• Acting as a general community nuisance will not be tolerated. This includes excessive or repeated outright disrespectful treatment of the staff (even outside of the server itself ie. DMs in relation to the server/game).\r\n• We are a growing community and it is important to be mindful that particularly negative behavior can impact on this.\r\n\r\n\r\n\r\n") : "\r\n\r\n(In-game names unless otherwise stated)\r\n\r\n<#00FFFF>Programming/Development:</color>\r\npur3 extreme\r\n\r\n<#00FFFF>Project Lead:</color>\r\nSiggy\r\n\r\n<#995533>Technical Assistant:</color>\r\nkosmos_prime (Discord)\r\n\r\n<#FF0000>Spellbook Art:</color>\r\nQuinn\r\nRain\r\nStrelizia\r\n\r\n\r\n<#FFFF00>Map Art:</color>\r\nRain\r\nStrelizia\r\ngoblinking#9185 (Discord)\r\nKaoe\r\nSpeedymatt Z\r\n\r\n<#00FF00>Character Art:</color>\r\nQuinn\r\nKaoe\r\nMoin the Mine\r\nFurious\r\nDon Rufus\r\nHarvicus\r\nSyndicate\r\nclint\r\ngrapplepi\r\nNev\r\npur3 extreme\r\nSiggy\r\nSkittlez\r\nRain\r\nSpeedymatt Z\r\nStrelizia\r\nancient_sage\r\nrbd\r\nlmaaoooo#1847 (Discord)\r\nMagmar#6885 (Discord)\r\npzUH - <u><link=https://opengameart.org/users/pzuh>OGA</link></u> - License - <u><link=https://creativecommons.org/publicdomain/zero/1.0/>CCO</link></u>\r\nKenney - <u><link=http://www.kenney.nl/>kenney.nl</link></u> - License - <u><link=https://creativecommons.org/publicdomain/zero/1.0/>CCO</link></u>\r\n\r\n<#FF00FF>Lobby Player Icons:</color>\r\nQuinn\r\nKaoe\r\n[Mod] Cruel\r\nRain\r\n\r\n<#8866FF>UI/Other Art:</color>\r\n[Mod] Cruel\r\nrbd\r\ngrapplepi\r\nFurious\r\nancient_sage\r\nnetgfx - <u><link=https://opengameart.org/users/netgfx>OGA</link></u> License - <u><link=https://creativecommons.org/licenses/by/3.0/>CC-BY 3.0</link></u>\r\nKokoroReflections - <u><link=https://opengameart.org/users/kokororeflections>OGA</link></u> - License - <u><link=https://creativecommons.org/licenses/by/4.0/>CC-BY 4.0</link></u>\r\nKenney - <u><link=http://www.kenney.nl/>kenney.nl</link></u> - License - <u><link=https://creativecommons.org/publicdomain/zero/1.0/>CCO</link></u>\r\n\r\n<#FF7744>Sound Effects:</color>\r\nSkulls\r\n\r\n<#99FF55>Music:</color>\r\nSkulls\r\n\r\n<#3333FF>Tutorials:</color>\r\npur3 extreme\r\nQuinn\r\ngrapplepi\r\n\r\n<#229999>Website:</color>\r\nSiggy\r\nFrog\r\nclint\r\n\r\n<#663366>Chess UI:</color>\r\nFurious\r\nStrelizia\r\nJohnPablok's chess set <u><link=https://opengameart.org/content/chess-pieces-and-board-squares>OGA</link></u> - License - <u><link=https://creativecommons.org/licenses/by-sa/3.0/>CC-BY-SA 3.0</link></u>\r\nScreaming Brain Studios <u><link=https://opengameart.org/content/2d-chess-pack>OGA</link></u> - License - <u><link=https://creativecommons.org/publicdomain/zero/1.0/>CCO</link></u>\r\n\r\n<u>Original game assets made by Jagex</u>\r\n        \r\n\r\n\r\n";
    this.headerTxt.text = t.ToString();
    DiscordIntergration.Instance?.UpdateNoUpdate();
  }

  private void Start()
  {
    if (this.type != CreditsMenu.Type.Credits)
      return;
    this.StartCoroutine(this.delay());
  }

  private void Update()
  {
    if (Input.anyKeyDown)
      this.StopAllCoroutines();
    if (!Input.GetKeyDown(KeyCode.F12))
      return;
    Global.systemCopyBuffer = this.txt.text;
    Debug.Log((object) "Copied to Clipboard");
  }

  private IEnumerator delay()
  {
    yield return (object) new WaitForSeconds(2f);
    bool down = true;
    Vector2 pos = this.scroller.normalizedPosition;
    while (true)
    {
      if (down)
      {
        pos.y -= Time.deltaTime / 15f;
        if ((double) pos.y <= 0.0)
        {
          pos.y = 0.0f;
          down = false;
        }
      }
      else
      {
        pos.y += Time.deltaTime / 15f;
        if ((double) pos.y >= 1.0)
        {
          pos.y = 1f;
          down = true;
        }
      }
      this.scroller.normalizedPosition = pos;
      yield return (object) new WaitForEndOfFrame();
    }
  }

  public void ClickBack() => Object.Destroy((Object) this.gameObject);

  public enum Type
  {
    Credits,
    Rules,
    SpellOverrides,
  }
}

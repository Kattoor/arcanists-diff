// Decompiled with JetBrains decompiler
// Type: BoatSpectators
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using Hazel;
using mattmc3.dotmore.Collections.Generic;
using Priority_Queue;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

#nullable disable
public class BoatSpectators : MonoBehaviour
{
  public List<Sprite> _flagPole;
  public List<Sprite> _postBack;
  public List<Sprite> _postFront;
  public List<Sprite> _postMiddle;
  public List<Sprite> _railing;
  public List<Sprite> _whole;
  public SpriteRenderer flagPole;
  public SpriteRenderer postBack;
  public SpriteRenderer postFront;
  public SpriteRenderer postMiddle;
  public SpriteRenderer railing;
  public SpriteRenderer whole;
  public AudioClip clipCreate;
  public MyCollider myCollider;
  public Dictionary<int, BoatSpectators.Character> spectators = new Dictionary<int, BoatSpectators.Character>();
  public Dictionary<string, BoatSpectators.Character> byName = new Dictionary<string, BoatSpectators.Character>();
  private BoatSpectators.PathFinding pathfinding = new BoatSpectators.PathFinding();
  public bool isSpectator;
  private BoatSpectators.Character cur;
  public static Vector3 maxStart = new Vector3(138f, -127f);
  public static Vector3 minStart = new Vector3(-180f, -146f);
  private bool showNames;
  private float lastMove = -1f;

  public static BoatSpectators Instance { get; private set; }

  private void OnDestroy()
  {
    if (!((Object) BoatSpectators.Instance == (Object) this))
      return;
    BoatSpectators.Instance = (BoatSpectators) null;
  }

  public static BoatSpectators Create(string name)
  {
    if ((Object) BoatSpectators.Instance != (Object) null)
      return (BoatSpectators) null;
    bool flag = BoatSpectators.ShouldBeVisible(name);
    BoatSpectators boatSpectators = Object.Instantiate<BoatSpectators>(ClientResources.Instance.boatSpectators, new Vector3(-1300f, 210f, 0.0f), Quaternion.identity, Client.game.GetMapTransform());
    BoatSpectators.Instance = boatSpectators;
    AudioManager.Play(boatSpectators.clipCreate);
    ZMyCollider collider = new ZMyCollider();
    collider.Copy(boatSpectators.myCollider);
    boatSpectators.pathfinding.InitGrid(collider);
    int num;
    switch (Client.game.gameFacts.realMap)
    {
      case MapEnum.Ocean_Floor:
      case MapEnum.Dark_Fortress:
      case MapEnum.Goblin_Caves:
      case MapEnum.Murky_Swamp:
      case MapEnum.Graveyard:
      case MapEnum.Mos_LeHarmless:
        num = 1;
        break;
      case MapEnum.Grassy_Hills:
        num = Client.game.gameFacts.seed % (boatSpectators._flagPole.Count + 1);
        break;
      case MapEnum.Alien_World:
      case MapEnum.Ghostly_Halls:
      case MapEnum.Space_Nexus:
        num = 2;
        break;
      default:
        num = 0;
        break;
    }
    int index = num - 1;
    if (index >= 0)
    {
      boatSpectators.flagPole.sprite = boatSpectators._flagPole[index];
      boatSpectators.postBack.sprite = boatSpectators._postBack[index];
      boatSpectators.postFront.sprite = boatSpectators._postFront[index];
      boatSpectators.postMiddle.sprite = boatSpectators._postMiddle[index];
      boatSpectators.railing.sprite = boatSpectators._railing[index];
      boatSpectators.whole.sprite = boatSpectators._whole[index];
    }
    if (Client.game.isSandbox)
    {
      boatSpectators.isSpectator = true;
      boatSpectators.CreateCharacter(0, Client.Name, (Vector3) BoatSpectators.GetRandomStartPos(), Client.settingsPlayer);
    }
    boatSpectators.gameObject.SetActive(flag);
    boatSpectators.OnActivation();
    return boatSpectators;
  }

  private void OnEnable()
  {
    if ((double) this.transform.position.x >= -660.0)
      return;
    this.StartCoroutine(this.MoveToPosition(this.transform.position.x));
  }

  public static Vector2 GetRandomStartPos()
  {
    return (Vector2) new Vector3(Random.Range(BoatSpectators.minStart.x, BoatSpectators.maxStart.x), Random.Range(BoatSpectators.minStart.y, BoatSpectators.maxStart.y));
  }

  private void OnMouseDown() => this.OnClick();

  private void Update()
  {
    if (!Input.GetKeyDown(KeyCode.LeftAlt) && !Input.GetKeyDown(KeyCode.RightAlt))
      return;
    this.showNames = !this.showNames;
    foreach (KeyValuePair<int, BoatSpectators.Character> spectator in this.spectators)
      spectator.Value.overheadCanvas.gameObject.SetActive(this.showNames);
  }

  public void OnClick()
  {
    if (!this.isSpectator || Player.IsPointerOverGameObject())
      return;
    Vector3 vector3 = Camera.main.ScreenToWorldPoint(Input.mousePosition) - this.transform.position;
    vector3.x -= (float) this.myCollider.GetRectMinX();
    vector3.y -= (float) this.myCollider.GetRectMinY();
    if (!Client.game.isServer)
    {
      if ((double) this.lastMove > (double) Time.realtimeSinceStartup)
        return;
      this.lastMove = Time.realtimeSinceStartup + 0.2f;
      Spectator.AskToMove((int) vector3.x, (int) vector3.y);
    }
    else
      this.MoveCharacter(0, (int) vector3.x, (int) vector3.y);
  }

  public void MoveCharacter(int id, int x, int y)
  {
    BoatSpectators.PathFinding.Tile closestOpen = this.pathfinding.GetClosestOpen(x, y);
    this.MoveCharacter(id, closestOpen);
  }

  public void OnEmoji(int id, int emoji)
  {
    if (!this.spectators.TryGetValue(id, out this.cur))
      return;
    this.cur.obj.GetComponent<Creature>()?.OnEmoji(emoji, true);
  }

  public void MoveCharacter(int id, BoatSpectators.PathFinding.Tile t, bool force = false)
  {
    if (!this.spectators.TryGetValue(id, out this.cur) || t == null)
      return;
    if (Client.game.resyncing)
    {
      this.cur.obj.transform.localPosition = new Vector3((float) t.xPos, (float) t.yPos, 0.0f);
    }
    else
    {
      BoatSpectators.PathFinding.Tile closestOpen = this.pathfinding.GetClosestOpen((int) this.cur.obj.transform.localPosition.x - this.myCollider.GetRectMinX(), (int) this.cur.obj.transform.localPosition.y - this.myCollider.GetRectMinY());
      if (closestOpen == null)
        return;
      OrderedDictionary<BoatSpectators.PathFinding.Tile, float> t1 = this.pathfinding.A_Star(closestOpen, t);
      if (t1 == null || t1.Count < 2 && (!force || t1 == null))
        return;
      if (this.cur.Walking != null)
        this.StopCoroutine(this.cur.Walking);
      this.cur.Walking = this.StartCoroutine(this.Walk(this.cur, t1));
    }
  }

  public Vector3 GetValidPosition(int x, int y)
  {
    BoatSpectators.PathFinding.Tile closestOpen = this.pathfinding.GetClosestOpen(x, y);
    return closestOpen == null ? Vector3.zero : new Vector3((float) closestOpen.xPos, (float) closestOpen.yPos, 0.0f);
  }

  public void Remove(int id)
  {
    BoatSpectators.Character character;
    if (this.spectators.TryGetValue(id, out character))
    {
      if (character.Walking != null)
        this.StopCoroutine(character.Walking);
      if ((Object) character.obj != (Object) null)
        Object.Destroy((Object) character.obj);
      this.spectators.Remove(id);
      this.byName.Remove(character.name);
      if (string.Equals(character.name, Client.Name))
      {
        this.isSpectator = false;
        if ((Object) HUD.instance != (Object) null)
        {
          HUD.instance.buttonJoinBoat.SetActive(true);
          HUD.instance.buttonOverheadEmoji.SetActive(false);
        }
        Connection boatConnection = Spectator.GetBoatConnection();
        if (boatConnection != null)
          boatConnection.player.inBoat = false;
      }
      foreach (KeyValuePair<int, BoatSpectators.Character> spectator in this.spectators)
      {
        if (spectator.Value.obj.activeSelf)
          return;
      }
      this.gameObject.SetActive(false);
    }
    this.OnActivation();
  }

  public void OnActivation()
  {
    if (!((Object) HUD.instance != (Object) null))
      return;
    if (this.spectators.Count > 0 && this.gameObject.activeInHierarchy)
      HUD.instance.txtGameID.text = "Game ID: #" + Client._gameFacts.id.ToString() + "\nSpectators: " + (object) this.spectators.Count;
    else
      HUD.instance.txtGameID.text = "Game ID: #" + Client._gameFacts.id.ToString();
  }

  public void CreateCharacter(int id, string name, Vector3 pos, SettingsPlayer sp)
  {
    if (this.spectators.ContainsKey(id))
      return;
    if (string.Equals(name, Client.Name) && !Client.game.resyncing)
    {
      this.isSpectator = true;
      HUD.instance.buttonJoinBoat.SetActive(false);
      HUD.instance.buttonOverheadEmoji.SetActive(true);
      Connection boatConnection = Spectator.GetBoatConnection();
      if (boatConnection != null)
      {
        boatConnection.player.inBoat = true;
        boatConnection.player.id = id;
      }
    }
    if (this.byName.ContainsKey(name))
      this.Remove(this.byName[name].id);
    GameObject gameObject = Object.Instantiate<GameObject>(Inert.Instance.Player_Character_GO, pos + this.transform.position, Quaternion.identity, this.transform.GetChild(0).GetChild(1));
    BoatSpectators.Character character = new BoatSpectators.Character()
    {
      id = id,
      name = name,
      sp = sp,
      obj = gameObject,
      color = ClientResources.Instance.colorsRegular.ffa_colors[id % ClientResources.Instance.colorsRegular.ffa_colors.Length]
    };
    this.spectators.Add(id, character);
    this.byName.Add(name, character);
    Creature component1 = gameObject.GetComponent<Creature>();
    component1.runTimeStats = Inert.Instance.Player_Character_GO.GetComponent<Creature>().runTimeStats;
    ConfigurePlayer component2 = component1.GetComponent<ConfigurePlayer>();
    character.animator = gameObject.GetComponent<IAnimator>();
    component1.animator = character.animator;
    string name1 = name;
    SettingsPlayer settingsPlayer = sp;
    component2.EquipAll(name1, settingsPlayer);
    ClientResources.CheckNotScaled(sp, component1);
    character.overheadCanvas = Object.Instantiate<Canvas>(ClientResources.Instance.overheadCanvasSpectator, new Vector3(0.0f, (float) (component1.radius + 15), 0.0f) + gameObject.transform.position, Quaternion.identity, gameObject.transform);
    character.overheadCanvas.gameObject.SetActive(this.showNames);
    component1.overheadCanvas = character.overheadCanvas;
    TMP_Text component3 = character.overheadCanvas.transform.GetChild(0).GetComponent<TMP_Text>();
    component3.text = name;
    component3.color = character.color;
    foreach (Creature.NotScaled notScaled in component1.notScaled)
      character.notScaled.Add(notScaled);
    Object.Destroy((Object) component1.bg.gameObject);
    Object.Destroy((Object) component1.transform.GetChild(0).GetChild(1).GetComponent<SpriteRenderer>());
    character.sprites = ((IEnumerable<SpriteRenderer>) component1.GetComponentsInChildren<SpriteRenderer>()).ToList<SpriteRenderer>();
    BoatSpectators.PathFinding.Tile closestOpen = this.pathfinding.GetClosestOpen((int) gameObject.transform.localPosition.x - this.myCollider.GetRectMinX(), (int) gameObject.transform.localPosition.y - this.myCollider.GetRectMinY());
    if (closestOpen != null)
    {
      character.curRenderOffset = (closestOpen.y * this.pathfinding.width + closestOpen.x) * 9;
      foreach (SpriteRenderer sprite in character.sprites)
        sprite.sortingOrder -= character.curRenderOffset;
    }
    if (!BoatSpectators.ShouldBeVisible(name))
      gameObject.SetActive(false);
    else if (!this.gameObject.activeSelf)
      this.gameObject.SetActive(true);
    this.OnActivation();
    if (closestOpen != null)
    {
      gameObject.transform.localPosition = new Vector3((float) closestOpen.xPos, (float) (closestOpen.yPos + 11), 0.0f);
      this.Render(closestOpen, character);
    }
    if (!((Object) HUD.instance != (Object) null))
      return;
    HUD.instance.txtGameID.text = "Game ID: #" + Client._gameFacts.id.ToString() + "\nSpectators: " + (object) this.spectators.Count;
  }

  public void RefreshVisible()
  {
    bool flag1 = false;
    foreach (KeyValuePair<int, BoatSpectators.Character> spectator in this.spectators)
    {
      bool flag2 = BoatSpectators.ShouldBeVisible(spectator.Value.name);
      spectator.Value.obj.SetActive(flag2);
      if (flag2)
        flag1 = true;
    }
    this.gameObject.SetActive(flag1);
    this.OnActivation();
  }

  private static bool ShouldBeVisible(string n)
  {
    switch (Client.spectatorChat)
    {
      case ChatSetting.On:
        return true;
      case ChatSetting.Friends:
        if (Client.HasFriend(n))
          goto case ChatSetting.On;
        else
          break;
      case ChatSetting.Off:
        return false;
    }
    return string.Equals(n, Client.Name);
  }

  private void Scale(BoatSpectators.Character character, Vector3 oneScale)
  {
    character.obj.transform.localScale = oneScale;
    character.overheadCanvas.transform.localScale = oneScale;
    for (int index = character.notScaled.Count - 1; index >= 0; --index)
    {
      if (character.notScaled[index] == null)
        character.notScaled.RemoveAt(index);
      else
        character.notScaled[index].Scale(oneScale);
    }
  }

  public IEnumerator Walk(
    BoatSpectators.Character character,
    OrderedDictionary<BoatSpectators.PathFinding.Tile, float> t)
  {
    if (t.Count > 1)
      t.RemoveAt(t.Count - 1);
    else
      t.Add((BoatSpectators.PathFinding.Tile) null, t.GetItem(0).Value);
    while (t.Count > 1)
    {
      BoatSpectators.PathFinding.Tile x = t.GetItem(t.Count - 2).Key;
      t.RemoveAt(t.Count - 1);
      Vector3 v = new Vector3((float) x.xPos, (float) (x.yPos + 11), 0.0f);
      Vector3 vector3 = v - character.obj.transform.localPosition;
      if ((double) v.x > (double) character.obj.transform.localPosition.x)
      {
        if ((double) character.obj.transform.localScale.x < 0.0)
          this.Scale(character, new Vector3(1f, 1f, 1f));
      }
      else if ((double) v.x < (double) character.obj.transform.localPosition.x && (double) character.obj.transform.localScale.x > 0.0)
        this.Scale(character, new Vector3(-1f, 1f, 1f));
      Vector3 localPosition = character.obj.transform.localPosition;
      while ((double) Vector3.Distance(character.obj.transform.localPosition, v) > 7.5500001907348633)
      {
        character.obj.transform.localPosition += (v - character.obj.transform.localPosition).normalized * 6f * 10f * Mathf.Min(Time.deltaTime, 0.0333f);
        character.animator?.Play(AnimateState.Walk, 0.05f, this.isSpectator);
        yield return (object) new WaitForEndOfFrame();
      }
      this.Render(x, character);
      x = (BoatSpectators.PathFinding.Tile) null;
      v = new Vector3();
    }
    character.animator?.Play(AnimateState.Stop);
    character.Walking = (Coroutine) null;
  }

  private void Render(BoatSpectators.PathFinding.Tile x, BoatSpectators.Character character)
  {
    int num = (x.y * this.pathfinding.width + x.x) * 9;
    foreach (SpriteRenderer sprite in character.sprites)
    {
      if ((Object) sprite != (Object) null)
        sprite.sortingOrder -= num - character.curRenderOffset;
    }
    character.curRenderOffset = num;
  }

  private IEnumerator MoveToPosition(float start)
  {
    BoatSpectators boatSpectators = this;
    float target = -659f;
    float cur = 0.0f;
    while (true)
    {
      cur += Time.deltaTime * 0.2f;
      if ((double) cur < 1.0)
      {
        boatSpectators.transform.position = new Vector3(Mathf.SmoothStep(start, target, cur), boatSpectators.transform.position.y, 0.0f);
        yield return (object) new WaitForEndOfFrame();
      }
      else
        break;
    }
    boatSpectators.transform.position = new Vector3(target, boatSpectators.transform.position.y, 0.0f);
  }

  private void InitCollider()
  {
  }

  public class Character
  {
    public int id;
    public string name;
    public SettingsPlayer sp;
    public GameObject obj;
    public IAnimator animator;
    public Coroutine Walking;
    public List<Creature.NotScaled> notScaled = new List<Creature.NotScaled>();
    public List<SpriteRenderer> sprites = new List<SpriteRenderer>();
    public Canvas overheadCanvas;
    public int curRenderOffset;
    public Color color;
  }

  public class PathFinding
  {
    private BoatSpectators.PathFinding.Tile[] tiles;
    internal int width;
    internal int height;
    private SimplePriorityQueue<BoatSpectators.PathFinding.Tile, float> openSet = new SimplePriorityQueue<BoatSpectators.PathFinding.Tile, float>();
    private HashSet<BoatSpectators.PathFinding.Tile> closedSet = new HashSet<BoatSpectators.PathFinding.Tile>();
    private Dictionary<BoatSpectators.PathFinding.Tile, BoatSpectators.PathFinding.Tile> cameFrom = new Dictionary<BoatSpectators.PathFinding.Tile, BoatSpectators.PathFinding.Tile>();
    private SimplePriorityQueue<BoatSpectators.PathFinding.Tile, float> gScore = new SimplePriorityQueue<BoatSpectators.PathFinding.Tile, float>();

    public void InitGrid(ZMyCollider collider)
    {
      collider.ArbritaryInit();
      MyCollider.Bounds bounds = collider.GetBounds();
      this.width = bounds.x2 - bounds.x1;
      this.height = bounds.y2 - bounds.y1;
      this.width /= 10;
      this.height /= 10;
      this.tiles = new BoatSpectators.PathFinding.Tile[this.width * this.height];
      int index1 = 0;
      for (int index2 = 0; index2 < this.height; ++index2)
      {
        int num = 0;
        while (num < this.width)
        {
          BoatSpectators.PathFinding.Tile tile = new BoatSpectators.PathFinding.Tile()
          {
            index = index1,
            xPos = num * 10 + bounds.x1,
            yPos = index2 * 10 + bounds.y1,
            x = num,
            y = index2
          };
          tile.blocked = !collider.OverlapPoint(num * 10 + bounds.x1, index2 * 10 + bounds.y1);
          this.tiles[index1] = tile;
          ++num;
          ++index1;
        }
      }
      this.tiles[371].blocked = true;
      this.tiles[432].blocked = true;
      this.tiles[204].blocked = true;
      this.tiles[205].blocked = true;
      this.tiles[265].blocked = true;
      this.tiles[401].blocked = true;
      this.tiles[402].blocked = true;
      this.tiles[462].blocked = true;
      this.tiles[479].blocked = true;
      this.tiles[543].blocked = true;
      this.tiles[604].blocked = false;
      this.tiles[607].blocked = true;
      this.tiles[668].blocked = false;
    }

    public BoatSpectators.PathFinding.Tile GetClosestOpen(int x, int y)
    {
      x /= 10;
      y /= 10;
      if (x < 0)
        x = 0;
      if (y < 0)
        y = 0;
      if (x >= this.width)
        x = this.width - 1;
      if (y >= this.height)
        y = this.height - 1;
      BoatSpectators.PathFinding.Tile tile1 = this.tiles[y * this.width + x];
      if (!tile1.blocked)
        return tile1;
      int num1 = 0;
      while (num1 < 100)
      {
        ++num1;
        int num2 = Mathf.Min(this.width, x + num1);
        int num3 = Mathf.Min(this.height, y + num1);
        for (int index1 = Mathf.Max(0, y - num1); index1 < num3; ++index1)
        {
          for (int index2 = Mathf.Max(0, x - num1); index2 < num2; ++index2)
          {
            BoatSpectators.PathFinding.Tile tile2 = this.tiles[index1 * this.width + index2];
            if (!tile2.blocked)
              return tile2;
          }
        }
      }
      return (BoatSpectators.PathFinding.Tile) null;
    }

    public OrderedDictionary<BoatSpectators.PathFinding.Tile, float> A_Star(
      BoatSpectators.PathFinding.Tile start,
      BoatSpectators.PathFinding.Tile end)
    {
      this.openSet.Clear();
      this.closedSet.Clear();
      this.gScore.Clear();
      this.cameFrom.Clear();
      this.gScore.Enqueue(start, 0.0f);
      this.openSet.Enqueue(start, 0.0f);
      bool flag = false;
      while (this.openSet.Count > 0)
      {
        double priority1 = (double) this.openSet.GetPriority(this.openSet.First);
        BoatSpectators.PathFinding.Tile tile1 = this.openSet.Dequeue();
        if (tile1 == end)
        {
          if ((double) this.gScore.GetPriority(end) > 1000.0)
          {
            this.closedSet.Add(tile1);
            flag = true;
          }
          else
          {
            BoatSpectators.PathFinding.Tile key = end;
            OrderedDictionary<BoatSpectators.PathFinding.Tile, float> orderedDictionary = new OrderedDictionary<BoatSpectators.PathFinding.Tile, float>();
            orderedDictionary.Add(end, this.gScore.GetPriority(end));
            while (this.cameFrom.ContainsKey(key))
            {
              key = this.cameFrom[key];
              orderedDictionary.Add(key, this.gScore.GetPriority(key));
            }
            return orderedDictionary;
          }
        }
        else
        {
          this.closedSet.Add(tile1);
          int num1 = tile1.index % this.width;
          int num2 = tile1.index / this.width;
          for (int index = 0; index < 8; ++index)
          {
            BoatSpectators.PathFinding.Tile tile2 = (BoatSpectators.PathFinding.Tile) null;
            if (index == 0)
            {
              if (num1 + 1 < this.width)
                tile2 = this.tiles[tile1.index + 1];
            }
            else if (index == 1)
            {
              if (num1 - 1 >= 0)
                tile2 = this.tiles[tile1.index - 1];
            }
            else if (index == 2)
            {
              if (num2 + 1 < this.height)
                tile2 = this.tiles[tile1.index + this.width];
            }
            else if (index == 3)
            {
              if (num2 - 1 >= 0)
                tile2 = this.tiles[tile1.index - this.width];
            }
            else if (index == 4)
            {
              int num3 = num2 - 1;
              int num4 = num1 + 1;
              if (num3 >= 0 && num4 < this.width)
                tile2 = this.tiles[tile1.index - this.width + 1];
            }
            else if (index == 5)
            {
              int num5 = num1 + 1;
              int num6 = num2 + 1;
              int width = this.width;
              if (num5 < width && num6 < this.height)
                tile2 = this.tiles[tile1.index + 1 + this.width];
            }
            else if (index == 6)
            {
              int num7 = num1 - 1;
              int num8 = num2 - 1;
              if (num7 >= 0 && num8 >= 0)
                tile2 = this.tiles[tile1.index - 1 - this.width];
            }
            else
            {
              int num9 = num2 + 1;
              int num10 = num1 - 1;
              int height = this.height;
              if (num9 < height && num10 >= 0)
                tile2 = this.tiles[tile1.index + this.width - 1];
            }
            if (tile2 != null && !tile2.blocked)
            {
              float num11 = (float) BoatSpectators.PathFinding.distance(end, tile2);
              float priority2 = this.gScore.GetPriority(tile1) + num11;
              if (this.closedSet.Contains(tile2))
              {
                if ((double) priority2 < (double) this.gScore.GetPriority(tile2))
                {
                  this.gScore.UpdatePriority(tile2, priority2);
                  if (!this.openSet.Contains(tile2))
                    this.openSet.Enqueue(tile2, priority2);
                  this.cameFrom[tile2] = tile1;
                }
              }
              else
              {
                if (!this.openSet.Contains(tile2))
                  this.openSet.Enqueue(tile2, priority2);
                else if ((double) priority2 >= (double) this.gScore.GetPriority(tile2))
                  continue;
                this.cameFrom[tile2] = tile1;
                if (this.gScore.Contains(tile2))
                  this.gScore.UpdatePriority(tile2, priority2);
                else
                  this.gScore.Enqueue(tile2, priority2);
              }
            }
          }
        }
      }
      if (!flag)
        return (OrderedDictionary<BoatSpectators.PathFinding.Tile, float>) null;
      BoatSpectators.PathFinding.Tile key1 = end;
      OrderedDictionary<BoatSpectators.PathFinding.Tile, float> orderedDictionary1 = new OrderedDictionary<BoatSpectators.PathFinding.Tile, float>();
      orderedDictionary1.Add(end, this.gScore.GetPriority(end));
      while (this.cameFrom.ContainsKey(key1))
      {
        key1 = this.cameFrom[key1];
        orderedDictionary1.Add(key1, this.gScore.GetPriority(key1));
      }
      return orderedDictionary1;
    }

    public static int distance(BoatSpectators.PathFinding.Tile a, BoatSpectators.PathFinding.Tile b)
    {
      return Mathf.Abs(a.x - b.x) + Mathf.Abs(a.y - b.y);
    }

    public class Tile
    {
      public int index;
      public bool blocked;
      public int x;
      public int y;
      public int xPos;
      public int yPos;
    }
  }
}

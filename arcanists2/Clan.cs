

using Hazel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

#nullable disable
public class Clan
{
  private const byte Version = 1;
  public string name = "";
  public string description = "";
  public List<string> log = new List<string>();
  public long creationDate;
  public Dictionary<string, Clan.Roles> members = new Dictionary<string, Clan.Roles>();
  public ClanOutfit outfit;
  public const byte MsgCreatedClan = 1;
  public const byte MsgMemberJoin = 2;
  public const byte MsgMemberLeft = 3;
  public const byte MsgMemberRole = 4;
  public const byte MsgAllClanOutfits = 5;
  public const byte MsgClanOutfit = 6;
  public const byte MsgInvite = 7;
  public const byte MsgAllClanInfo = 8;
  public const byte MsgAcceptInvite = 9;
  public const byte MsgKick = 10;
  public const byte MsgMemberNameChanged = 11;
  public const byte MsgLog = 12;
  [JsonIgnore]
  [NonSerialized]
  public Dictionary<string, Connection> activeConnections = new Dictionary<string, Connection>();
  [JsonIgnore]
  [NonSerialized]
  public Dictionary<string, (float time, string who)> activeInvites = new Dictionary<string, (float, string)>((IEqualityComparer<string>) Server._caseInsensitiveComparer);
  [JsonIgnore]
  [NonSerialized]
  private static List<string> outdated = new List<string>();
  [JsonIgnore]
  [NonSerialized]
  internal int clientMemberCount;

  public string key => this.name.ToLower();

  public void TryToJoin(Connection c)
  {
  }

  public void Invite(string n, string who)
  {
  }

  public void SendInvite(string inviter, Connection c)
  {
  }

  public void OnJoined(Account acc)
  {
  }

  public void MemberNameChanged(string oldName, string newName, Clan.Roles role)
  {
  }

  public void SendClanMsg(byte msg, string member, byte additional = 0)
  {
  }

  public void ClanChat(byte[] msg, bool serverMesh = true)
  {
  }

  public void Send(Connection notThis, byte[] msg)
  {
  }

  private async Task<bool> PromoteLeader(Clan.Roles toCheck) => false;

  public async Task RemoveMember(Account acc, bool update = true, bool allowDisband = false, string reason = null)
  {
  }

  public static async Task Disband(Clan clan, string reason)
  {
  }

  public bool AddMember(Account acc, Clan.Roles role, string inviter) => false;

  public void AddLog(string s)
  {
    this.log.Add(s);
    if (this.log.Count <= 200)
      return;
    this.log.RemoveAt(0);
  }

  public void ChangeRank(Account acc, Clan.Roles role, string whodidit)
  {
  }

  public static void LoadAllClans()
  {
  }

  public void ToBasic(myBinaryWriter w)
  {
    w.Write(this.name);
    w.Write(this.description);
    w.Write(this.members.Count);
    w.Write(this.creationDate);
  }

  public static Clan FromBasic(myBinaryReader r)
  {
    return new Clan()
    {
      name = r.ReadString(),
      description = r.ReadString(),
      clientMemberCount = r.ReadInt32(),
      creationDate = r.ReadInt64()
    };
  }

  public void SerializeToFile()
  {
  }

  public void Serialize(myBinaryWriter w)
  {
    w.Write((byte) 1);
    w.Write(this.name);
    w.Write(this.creationDate);
    w.Write(this.description);
    w.Write(this.members.Count);
    foreach (KeyValuePair<string, Clan.Roles> member in this.members)
    {
      w.Write(member.Key);
      w.Write((byte) member.Value);
    }
    if (this.outfit != null && this.outfit.outfits != null)
    {
      w.Write((byte) 1);
      w.Write(this.outfit.outfits.Length);
      for (int index = 0; index < this.outfit.outfits.Length; ++index)
      {
        ClanOutfit.Meta outfit = this.outfit.outfits[index];
        if (outfit == null)
        {
          w.Write((byte) 0);
        }
        else
        {
          w.Write((byte) 1);
          outfit.Serialize(w);
        }
      }
    }
    else
      w.Write((byte) 0);
    if (this.log.Count > 200)
      this.log.RemoveAt(0);
    w.Write(this.log.Count);
    for (int index = 0; index < this.log.Count; ++index)
      w.Write(this.log[index]);
  }

  public static Clan DeserializeFromFile(string file)
  {
    using (MemoryStream memoryStream = new MemoryStream(File.ReadAllBytes(file)))
    {
      using (myBinaryReader r = new myBinaryReader((Stream) memoryStream))
        return Clan.Deserialize(r);
    }
  }

  public static Clan Deserialize(myBinaryReader r)
  {
    Clan clan = new Clan();
    clan._Deserialize(r);
    return clan;
  }

  private void _Deserialize(myBinaryReader r)
  {
    int num1 = (int) r.ReadByte();
    this.name = r.ReadString();
    this.creationDate = r.ReadInt64();
    this.description = r.ReadString();
    int num2 = r.ReadInt32();
    for (int index = 0; index < num2; ++index)
      this.members.Add(r.ReadString(), (Clan.Roles) r.ReadByte());
    if (r.ReadByte() == (byte) 1)
    {
      int length = r.ReadInt32();
      this.outfit = new ClanOutfit();
      this.outfit.outfits = new ClanOutfit.Meta[length];
      for (int index = 0; index < length; ++index)
      {
        if (r.ReadByte() == (byte) 1)
        {
          ClanOutfit.Meta meta = new ClanOutfit.Meta();
          meta.Deserialize(r);
          this.outfit.outfits[index] = meta;
        }
      }
    }
    if (r.BaseStream.Position >= r.BaseStream.Length)
      return;
    int num3 = r.ReadInt32();
    for (int index = 0; index < num3; ++index)
      this.log.Add(r.ReadString());
  }

  public enum Roles : byte
  {
    Inactive,
    Member,
    Veteran,
    Officer,
    Advisor,
    Leader,
  }

  public class MemberX
  {
    public string name;
    public Clan.Roles role;
    public Account acc;
  }
}

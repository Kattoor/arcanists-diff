

using Hazel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;

#nullable disable
public class Clan
{
  private const byte Version = 1;
  public string name = "";
  public string description = "";
  public List<string> log = new List<string>();
  public long creationDate;
  public Dictionary<string, Clan.Member> members = new Dictionary<string, Clan.Member>();
  public ClanOufit outfit;
  public Dictionary<string, (float time, string who)> activeInvites = new Dictionary<string, (float, string)>((IEqualityComparer<string>) Server._caseInsensitiveComparer);
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
  private static List<string> outdated = new List<string>();
  internal int clientMemberCount;

  public string key => this.name.ToLower();

  public void TryToJoin(Connection c)
  {
  }

  public void Invite(string n, string who)
  {
    Clan.outdated.Clear();
    foreach (KeyValuePair<string, (float time, string who)> activeInvite in this.activeInvites)
    {
      if ((double) Time.realtimeSinceStartup > (double) activeInvite.Value.time)
        Clan.outdated.Add(activeInvite.Key);
    }
    foreach (string key in Clan.outdated)
      this.activeInvites.Remove(key);
    this.activeInvites[n] = (Time.realtimeSinceStartup + 30f, who);
  }

  public void SendInvite(string inviter, Connection c)
  {
    using (MemoryStream memoryStream = new MemoryStream())
    {
      using (myBinaryWriter myBinaryWriter = new myBinaryWriter((Stream) memoryStream))
      {
        myBinaryWriter.Write((byte) 84);
        myBinaryWriter.Write((byte) 7);
        myBinaryWriter.Write(inviter);
        myBinaryWriter.Write(this.name);
      }
      if (c.State != ConnectionState.Connected)
        return;
      c.SendBytes(memoryStream.ToArray());
    }
  }

  public void OnJoined(Account acc)
  {
    Connection connection;
    if (!Server._names.TryGetValue(acc.name, out connection) || connection.State != ConnectionState.Connected)
      return;
    using (MemoryStream memoryStream = new MemoryStream())
    {
      using (myBinaryWriter w = new myBinaryWriter((Stream) memoryStream))
      {
        w.Write((byte) 84);
        w.Write((byte) 1);
        this.Serialize(w);
      }
      if (connection.State != ConnectionState.Connected)
        return;
      connection.SendBytes(memoryStream.ToArray());
    }
  }

  public void MemberNameChanged(string oldName, string newName, Clan.Roles role)
  {
    this.members.Remove(oldName);
    this.members[newName] = new Clan.Member()
    {
      name = newName,
      role = role
    };
    using (MemoryStream memoryStream = new MemoryStream())
    {
      using (myBinaryWriter myBinaryWriter = new myBinaryWriter((Stream) memoryStream))
      {
        myBinaryWriter.Write((byte) 84);
        myBinaryWriter.Write((byte) 11);
        myBinaryWriter.Write(oldName);
        myBinaryWriter.Write(newName);
        myBinaryWriter.Write((byte) role);
      }
      this.ClanChat(memoryStream.ToArray());
    }
    this.log.Add("[Name Changed] " + Server.GetTime() + oldName + " [To] " + newName);
    this.SerializeToFile();
  }

  public void SendClanMsg(byte msg, string member, byte additional = 0)
  {
    using (MemoryStream memoryStream = new MemoryStream())
    {
      using (myBinaryWriter myBinaryWriter = new myBinaryWriter((Stream) memoryStream))
      {
        myBinaryWriter.Write((byte) 84);
        myBinaryWriter.Write(msg);
        myBinaryWriter.Write(member);
        myBinaryWriter.Write(additional);
      }
      this.ClanChat(memoryStream.ToArray());
    }
  }

  public void ClanChat(byte[] msg)
  {
    foreach (KeyValuePair<string, Clan.Member> member in this.members)
    {
      Connection connection;
      if (Server._names.TryGetValue(member.Key, out connection) && connection.State == ConnectionState.Connected)
        connection.SendBytes(msg);
    }
  }

  public void Send(Connection notThis, byte[] msg)
  {
    foreach (KeyValuePair<string, Clan.Member> member in this.members)
    {
      Connection connection;
      if (Server._names.TryGetValue(member.Key, out connection) && connection.State == ConnectionState.Connected && connection != notThis)
        connection.SendBytes(msg);
    }
  }

  private async Task<bool> PromoteLeader(Clan.Roles toCheck) => false;

  public async Task RemoveMember(Account acc, bool update = true, bool allowDisband = false, string reason = null)
  {
    Clan clan = this;
    if (acc == null)
      return;
    if (string.Equals(acc.clan, clan.name, StringComparison.OrdinalIgnoreCase))
    {
      if (acc.clanRole == Clan.Roles.Leader && clan.members.Count > 3)
      {
        int num = 0;
        foreach (KeyValuePair<string, Clan.Member> member in clan.members)
        {
          if (member.Value.role == Clan.Roles.Leader)
            ++num;
        }
        if (num == 1)
        {
          if (!await clan.PromoteLeader(Clan.Roles.Advisor))
          {
            if (!await clan.PromoteLeader(Clan.Roles.Officer))
            {
              if (!await clan.PromoteLeader(Clan.Roles.Veteran))
              {
                if (!await clan.PromoteLeader(Clan.Roles.Member))
                {
                  if (!await clan.PromoteLeader(Clan.Roles.Inactive))
                    Server.Instance?.communicator?.SendCommandSpam("Clan [" + clan.name + "] could not find a suitable leader when the leader [" + acc.name + "] left it.");
                }
              }
            }
          }
        }
      }
      acc.clan = "";
      acc.clanRole = Clan.Roles.Inactive;
      clan.SendClanMsg((byte) 3, acc.name);
    }
    clan.members.Remove(acc.name);
    if (update)
      Server.UpdateAccountInfo(acc);
    clan.log.Add("[Left Clan] " + Server.GetTime() + acc.name + (reason == null ? "" : " [Reason] " + reason));
    clan.SerializeToFile();
    MyLog.MainLog(acc.name + " [Left Clan " + clan.name + "]");
    if (!allowDisband || clan.members.Count >= 3)
      return;
    Clan.Disband(clan, "Due to insufficient members");
  }

  public static async Task Disband(Clan clan, string reason)
  {
  }

  public bool AddMember(Account acc, Clan.Roles role, string inviter)
  {
    if (acc == null || this.members.ContainsKey(acc.name))
      return false;
    Clan clan;
    if (!string.IsNullOrEmpty(acc.clan) && Server._clans.TryGetValue(acc.clan, out clan))
      clan.RemoveMember(acc, false, true, "Joined another clan");
    if (this.members.Count >= 100)
      return false;
    this.members.Add(acc.name, new Clan.Member()
    {
      name = acc.name,
      role = role
    });
    acc.clan = this.name;
    acc.clanRole = role;
    Server.UpdateAccountInfo(acc);
    MyLog.MainLog(acc.name + " [Joined Clan " + this.name + "]");
    this.OnJoined(acc);
    this.log.Add("[Joined] " + Server.GetTime() + acc.name + " [Invited by] " + inviter);
    this.SerializeToFile();
    this.SendClanMsg((byte) 2, acc.name, (byte) role);
    return true;
  }

  public void ChangeRank(Account acc, Clan.Roles role, string whodidit)
  {
    Clan.Member member;
    if (!this.members.TryGetValue(acc.name, out member))
      return;
    Clan.Roles role1 = member.role;
    member.role = role;
    acc.clanRole = role;
    Server.UpdateAccountInfo(acc);
    this.SendClanMsg((byte) 4, acc.name, (byte) role);
    List<string> log = this.log;
    string[] strArray = new string[6]
    {
      "[Rank] ",
      Server.GetTime(),
      acc.name,
      null,
      null,
      null
    };
    string str1;
    if (role <= role1)
      str1 = " Demoted from " + (object) role1 + " to " + (object) role;
    else
      str1 = " Promoted from " + (object) role1 + " to " + (object) role;
    strArray[3] = str1;
    strArray[4] = " by ";
    strArray[5] = whodidit;
    string str2 = string.Concat(strArray);
    log.Add(str2);
    this.SerializeToFile();
  }

  public static void LoadAllClans()
  {
    string path = Application.persistentDataPath + "/Clans/";
    if (!Directory.Exists(path))
      return;
    foreach (string file in Directory.GetFiles(path))
    {
      try
      {
        Clan clan = Clan.DeserializeFromFile(file);
        Server._clans.Add(clan.name, clan);
      }
      catch (Exception ex)
      {
        Debug.LogError((object) ex);
      }
    }
    ClientResources.ServerCompressOutfits();
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
    string path1 = Application.persistentDataPath + "/Clans/";
    if (!Directory.Exists(path1))
      Directory.CreateDirectory(path1);
    string path2 = path1 + this.name + ".clan";
    using (MemoryStream memoryStream = new MemoryStream())
    {
      using (myBinaryWriter w = new myBinaryWriter((Stream) memoryStream))
        this.Serialize(w);
      File.WriteAllBytes(path2, memoryStream.ToArray());
    }
  }

  public void Serialize(myBinaryWriter w)
  {
    w.Write((byte) 1);
    w.Write(this.name);
    w.Write(this.creationDate);
    w.Write(this.description);
    w.Write(this.members.Count);
    foreach (KeyValuePair<string, Clan.Member> member in this.members)
    {
      w.Write(member.Key);
      w.Write((byte) member.Value.role);
    }
    if (this.outfit != null && this.outfit.outfits != null)
    {
      w.Write((byte) 1);
      w.Write(this.outfit.outfits.Length);
      for (int index = 0; index < this.outfit.outfits.Length; ++index)
      {
        ClanOufit.Meta outfit = this.outfit.outfits[index];
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
    {
      Clan.Member member = new Clan.Member();
      member.name = r.ReadString();
      member.role = (Clan.Roles) r.ReadByte();
      this.members.Add(member.name, member);
    }
    if (r.ReadByte() == (byte) 1)
    {
      int length = r.ReadInt32();
      this.outfit = new ClanOufit();
      this.outfit.outfits = new ClanOufit.Meta[length];
      for (int index = 0; index < length; ++index)
      {
        if (r.ReadByte() == (byte) 1)
        {
          ClanOufit.Meta meta = new ClanOufit.Meta();
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

  public class Member
  {
    public string name;
    public Clan.Roles role;
  }

  public class MemberX
  {
    public string name;
    public Clan.Roles role;
    public Account acc;
  }
}

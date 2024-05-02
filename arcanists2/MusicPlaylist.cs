// Decompiled with JetBrains decompiler
// Type: MusicPlaylist
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using mattmc3.dotmore.Collections.Generic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

#nullable disable
public class MusicPlaylist
{
  private static string PATH;
  private OrderedDictionary<string, MusicPlaylist.Track> AllMusic = new OrderedDictionary<string, MusicPlaylist.Track>();
  private int customHeader;
  private int additionalHeader;

  public void Initialize()
  {
    List<MusicPlaylist.Track> trackList = new List<MusicPlaylist.Track>();
    AudioManager instance = AudioManager.instance;
    trackList.Add(new MusicPlaylist.Track()
    {
      name = instance.musicMainMenu.name,
      custom = false,
      clip = instance.musicMainMenu
    });
    foreach (AudioClip audioClip in instance.musicGame)
      trackList.Add(new MusicPlaylist.Track()
      {
        name = audioClip.name,
        custom = false,
        clip = audioClip
      });
    trackList.Add(new MusicPlaylist.Track()
    {
      name = instance.musicCredits.name,
      custom = false,
      clip = instance.musicCredits
    });
    this.additionalHeader = trackList.Count;
    foreach (AudioClip audioClip in instance.additionalMusic)
      trackList.Add(new MusicPlaylist.Track()
      {
        name = audioClip.name,
        custom = false,
        clip = audioClip
      });
    this.customHeader = trackList.Count;
    foreach (string path in ((IEnumerable<string>) Directory.GetFiles(MusicPlaylist.PATH)).Where<string>((Func<string, bool>) (file => Regex.IsMatch(Path.GetExtension(file).ToLower(), "\\.(wav|mp3|ogg)"))))
      trackList.Add(new MusicPlaylist.Track()
      {
        name = Path.GetFileNameWithoutExtension(path),
        path = path,
        custom = true
      });
  }

  static MusicPlaylist()
  {
    string persistentDataPath = Application.persistentDataPath;
    char directorySeparatorChar = Path.DirectorySeparatorChar;
    string str1 = directorySeparatorChar.ToString();
    directorySeparatorChar = Path.DirectorySeparatorChar;
    string str2 = directorySeparatorChar.ToString();
    MusicPlaylist.PATH = persistentDataPath + str1 + "Music" + str2;
  }

  public class Track
  {
    public string name;
    public string path;
    public bool custom;
    public AudioClip clip;
  }
}

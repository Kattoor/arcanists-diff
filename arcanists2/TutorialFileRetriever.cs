// Decompiled with JetBrains decompiler
// Type: TutorialFileRetriever
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

#nullable disable
public class TutorialFileRetriever
{
  private readonly string _url;
  public System.Collections.Generic.List<TutorialFileRetriever.Tuts> list = new System.Collections.Generic.List<TutorialFileRetriever.Tuts>();
  private float _lastRetrieve;

  public TutorialFileRetriever(string url) => this._url = url;

  public static System.Collections.Generic.List<TutorialFileRetriever.Tuts> List
  {
    get => ClientResources.Instance.tutorialFileRetriever.list;
  }

  public static void Get(bool force = false)
  {
    ChooseJsonDialog.Instance?.StartCoroutine(ClientResources.Instance.tutorialFileRetriever.GetHtmlContent(force));
  }

  public static void DownloadBook(TutorialFileRetriever.Tuts t)
  {
    ChooseJsonDialog.Instance?.StartCoroutine(ClientResources.Instance.tutorialFileRetriever._DownloadBook(t));
  }

  public IEnumerator GetHtmlContent(bool force)
  {
    if (force || (double) this._lastRetrieve < (double) Time.realtimeSinceStartup)
    {
      using (UnityWebRequest request = UnityWebRequest.Get(this._url))
      {
        this._lastRetrieve = Time.realtimeSinceStartup + 3600f;
        Debug.Log((object) "Retrieving...");
        yield return (object) request.SendWebRequest();
        if (request.isHttpError || request.isNetworkError)
          Debug.LogError((object) ("Error retrieving Tutorial index: " + request.error));
        else
          this.ReadIndex(request.downloadHandler.text);
      }
    }
  }

  private IEnumerator _DownloadBook(TutorialFileRetriever.Tuts t)
  {
    using (UnityWebRequest request = UnityWebRequest.Get(t.url))
    {
      yield return (object) request.SendWebRequest();
      if (request.isHttpError || request.isNetworkError)
        Debug.LogError((object) ("Error retrieving Tutorial: " + this._url + " >>> " + request.error));
      else
        this.OnDownload(t, request.downloadHandler.text);
    }
  }

  public void OnDownload(TutorialFileRetriever.Tuts t, string content)
  {
    File.WriteAllText(t.FileName, content);
    if (!((UnityEngine.Object) ChooseJsonDialog.Instance != (UnityEngine.Object) null) || !string.Equals(ChooseJsonDialog.Instance.selectedFilePath, t.url))
      return;
    ChooseJsonDialog.Instance.PreviewPublicBook(t, false);
  }

  public void ReadIndex(string index)
  {
    Global.CheckDirectoryExists(Global.GetTutorialPath, "PublicRepo" + Path.DirectorySeparatorChar.ToString());
    this.list.Clear();
    string str1 = index;
    char[] separator = new char[1]{ '\n' };
    foreach (string str2 in str1.Split(separator, StringSplitOptions.RemoveEmptyEntries))
    {
      char[] chArray = new char[1]{ ',' };
      string[] strArray = str2.Split(chArray);
      if (strArray.Length >= 2)
        this.list.Add(new TutorialFileRetriever.Tuts()
        {
          name = strArray[0].Trim(),
          version = strArray[1].Trim()
        });
    }
    this.list.Sort((Comparison<TutorialFileRetriever.Tuts>) ((a, b) => a.name.CompareTo(b.name)));
    HashSet<string> stringSet = new HashSet<string>();
    foreach (TutorialFileRetriever.Tuts tuts in this.list)
      stringSet.Add(tuts.FileName);
    try
    {
      string[] files = Directory.GetFiles(Global.GetTutorialPath + "PublicRepo" + Path.DirectorySeparatorChar.ToString());
      for (int index1 = 0; index1 < files.Length; ++index1)
      {
        if (!stringSet.Contains(files[index1]))
        {
          File.Delete(files[index1]);
          Debug.Log((object) ("Deleted: " + files[index1]));
        }
      }
    }
    catch (Exception ex)
    {
      Debug.LogError((object) ex);
    }
    ChooseJsonDialog instance = ChooseJsonDialog.Instance;
    if ((instance != null ? (instance.custom == ChooseJsonDialog.Viewing.Public ? 1 : 0) : 0) == 0)
      return;
    ChooseJsonDialog.Instance?.PopulatePublic();
  }

  public class Tuts
  {
    public string name;
    public string version;

    public string url
    {
      get => "pur3extreme.github.io/ArcanistsTutorials/Tutorials/" + this.name + ".arcTutorial2";
    }

    public string FileName
    {
      get
      {
        return Global.GetTutorialPath + "PublicRepo" + Path.DirectorySeparatorChar.ToString() + Global.MakeSafeForCode(this.name + " (" + this.version + ")") + ".arcTutorial2";
      }
    }
  }
}

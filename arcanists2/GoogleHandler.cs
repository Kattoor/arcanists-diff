

using MovementEffects;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityGoogleDrive;
using UnityGoogleDrive.Data;

#nullable disable
public class GoogleHandler
{
  private GoogleDriveFiles.ListRequest request;
  private string query = string.Empty;
  public int ResultsPerPage = 100;
  private string nextPageToken = "";
  private const string name = "Arc2DriveStoredData";
  private static DateTime? creationDate;
  public static Dictionary<string, PrefType> prefs = new Dictionary<string, PrefType>();
  public static float nextSave = 0.0f;
  private static IEnumerator<float> saveIE = (IEnumerator<float>) null;

  public void hi()
  {
    this.request = GoogleDriveFiles.List();
    this.request.Fields = new List<string>()
    {
      "nextPageToken, files(id, name, size, createdTime, mimeType)"
    };
    this.request.PageSize = new int?(this.ResultsPerPage);
    this.request.Q = "name = 'Arc2DriveStoredData' and mimeType = 'application/zip' and trashed = false";
    if (!string.IsNullOrEmpty(this.query))
      this.request.Q = string.Format("name contains '{0}'", (object) this.query);
    if (!string.IsNullOrEmpty(this.nextPageToken))
      this.request.PageToken = this.nextPageToken;
    this.request.Send().OnDone += new Action<FileList>(this.BuildResults);
  }

  public static void Save()
  {
    if (GoogleHandler.saveIE != null)
      return;
    GoogleHandler.saveIE = Timing.RunCoroutine(GoogleHandler.IESave());
  }

  public static IEnumerator<float> IESave()
  {
    while ((double) Time.realtimeSinceStartup > (double) GoogleHandler.nextSave)
      yield return 0.0f;
    GoogleHandler.saveIE = (IEnumerator<float>) null;
    GoogleHandler.nextSave = Time.realtimeSinceStartup + 300f;
    GoogleHandler.Upload();
  }

  private void BuildResults(FileList fileList)
  {
    foreach (UnityGoogleDrive.Data.File file in fileList.Files)
    {
      Debug.Log((object) (file.Name + " = type: " + file.MimeType));
      if (string.Equals(file.Name, "Arc2DriveStoredData"))
      {
        Debug.Log((object) "Success!!!");
        GoogleHandler.creationDate = file.CreatedTime;
        return;
      }
    }
    Debug.Log((object) ("Next Page? " + this.nextPageToken));
  }

  private bool NextPageExists()
  {
    return this.request != null && this.request.ResponseData != null && !string.IsNullOrEmpty(this.request.ResponseData.NextPageToken);
  }

  private static void Upload()
  {
    GoogleDriveFiles.CreateRequest createRequest = GoogleDriveFiles.Create(new UnityGoogleDrive.Data.File()
    {
      Name = "Arc2DriveStoredData",
      Content = GoogleHandler.CreatePersistantData(),
      MimeType = "application/zip"
    });
    createRequest.Fields = new List<string>()
    {
      "id",
      "name",
      "size",
      "createdTime"
    };
    createRequest.Send().OnDone += new Action<UnityGoogleDrive.Data.File>(GoogleHandler.PrintResult);
  }

  private static void PrintResult(UnityGoogleDrive.Data.File file)
  {
  }

  public static byte[] CreatePersistantData()
  {
    using (MemoryStream memoryStream = new MemoryStream())
    {
      using (myBinaryWriter w = new myBinaryWriter((Stream) memoryStream))
        GoogleHandler.Serialize(w, new DirectoryInfo(Application.persistentDataPath));
      return CLZF2.Compress(memoryStream.ToArray());
    }
  }

  private static void Serialize(myBinaryWriter w, DirectoryInfo d)
  {
    w.Write(d.Name);
    FileInfo[] files = d.GetFiles();
    w.Write(files.Length);
    foreach (FileInfo fileInfo in files)
    {
      w.Write(fileInfo.Name);
      w.Write(System.IO.File.ReadAllBytes(fileInfo.FullName));
    }
    DirectoryInfo[] directories = d.GetDirectories();
    w.Write(directories.Length);
    foreach (DirectoryInfo d1 in directories)
      GoogleHandler.Serialize(w, d1);
    w.Write(GoogleHandler.prefs.Count);
    foreach (KeyValuePair<string, PrefType> pref in GoogleHandler.prefs)
      pref.Value.Serialize(w);
  }

  private static void Deserialize(byte[] b)
  {
    using (MemoryStream memoryStream = new MemoryStream(b))
    {
      using (myBinaryReader r = new myBinaryReader((Stream) memoryStream))
      {
        GoogleHandler._Deserialize(Application.persistentDataPath + Path.DirectorySeparatorChar.ToString(), r);
        int num = r.ReadInt32();
        for (int index = 0; index < num; ++index)
        {
          PrefType prefType = PrefType.Deseriralize(r);
          GoogleHandler.prefs[prefType.name] = prefType;
        }
      }
    }
  }

  private static void _Deserialize(string dir, myBinaryReader r)
  {
    string str1 = r.ReadString();
    string str2 = dir + str1 + Path.DirectorySeparatorChar.ToString();
    if (!Directory.Exists(str2))
      Directory.CreateDirectory(str2);
    int num1 = r.ReadInt32();
    for (int index = 0; index < num1; ++index)
    {
      string str3 = r.ReadString();
      byte[] bytes = r.ReadBytes();
      string str4 = str2 + str3;
      if (System.IO.File.Exists(str4))
      {
        DateTime creationTimeUtc = new FileInfo(str4).CreationTimeUtc;
        DateTime? creationDate = GoogleHandler.creationDate;
        if ((creationDate.HasValue ? (creationTimeUtc < creationDate.GetValueOrDefault() ? 1 : 0) : 0) == 0)
          continue;
      }
      System.IO.File.WriteAllBytes(str4, bytes);
    }
    int num2 = r.ReadInt32();
    for (int index = 0; index < num2; ++index)
      GoogleHandler._Deserialize(str2, r);
  }
}

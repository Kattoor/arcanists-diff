

using System;
using System.IO;
using UnityEngine;

#nullable disable
public class Report
{
  public int id;
  public string reporter;
  public string reported;
  public int offense;
  public string extraInfo;
  public string time;

  public void Serialize(myBinaryWriter writer)
  {
    writer.Write(this.offense);
    writer.Write(this.reported);
    writer.Write(this.extraInfo);
    writer.Write(this.id);
    writer.Write(this.reporter);
    writer.Write(this.time);
  }

  public static int GetID()
  {
    int id = PlayerPrefs.GetInt("reportcount", 0);
    PlayerPrefs.SetInt("reportcount", id + 1);
    return id;
  }

  public static Report RequestReport(string reporter, string reported, string command, Report org = null)
  {
    return new Report()
    {
      reporter = reporter,
      reported = reported,
      extraInfo = command + " " + (org != null ? "Author: " + org.reporter + ": " + org.extraInfo : ">>> Did not originate from a report  <<<"),
      id = org == null ? Report.GetID() : org.id,
      time = DateTime.Now.ToString("yyyy-MM-dd tt hh-mm")
    };
  }

  public static Report Deserialize(myBinaryReader r, bool server, string reporter)
  {
    Report report = new Report();
    report.offense = r.ReadInt32();
    report.reported = r.ReadString();
    report.extraInfo = r.ReadString();
    if (server)
    {
      int id = Report.GetID();
      report.id = id;
      report.reporter = reporter;
      report.time = DateTime.Now.ToString("yyyy-MM-dd tt hh-mm");
    }
    else
    {
      report.id = r.ReadInt32();
      report.reporter = r.ReadString();
      report.time = r.ReadString();
    }
    return report;
  }

  public void ToFile(StreamWriter w)
  {
    w.WriteLine(Server.reportableOffenses[this.offense]);
    w.WriteLine(this.reported);
    w.WriteLine(this.extraInfo);
    w.WriteLine(this.id);
    w.WriteLine(this.reporter);
    w.WriteLine(this.time);
    w.WriteLine();
  }
}

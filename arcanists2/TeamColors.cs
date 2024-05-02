// Decompiled with JetBrains decompiler
// Type: TeamColors
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

#nullable disable
[Serializable]
public class TeamColors
{
  public Color[] ffa_colors;
  public Color[] team_colors;
  public Color[] team2Colors;
  public Color[] team3Colors;
  public Color[] team4Colors;
  public Color[] team5Colors;
  public Color[] team6Colors;
  public Color[] team7Colors;
  public Color[] team8Colors;
  public Color[] team9Colors;
  public Color[] team10Colors;
  public Color[] team11Colors;
  public Color[] team12Colors;

  public IEnumerable<Color> GetAll()
  {
    return ((IEnumerable<Color>) this.ffa_colors).Concat<Color>((IEnumerable<Color>) this.team_colors).Concat<Color>((IEnumerable<Color>) this.team2Colors).Concat<Color>((IEnumerable<Color>) this.team3Colors).Concat<Color>((IEnumerable<Color>) this.team4Colors).Concat<Color>((IEnumerable<Color>) this.team5Colors).Concat<Color>((IEnumerable<Color>) this.team6Colors).Concat<Color>((IEnumerable<Color>) this.team7Colors).Concat<Color>((IEnumerable<Color>) this.team8Colors).Concat<Color>((IEnumerable<Color>) this.team9Colors).Concat<Color>((IEnumerable<Color>) this.team10Colors).Concat<Color>((IEnumerable<Color>) this.team11Colors).Concat<Color>((IEnumerable<Color>) this.team12Colors);
  }

  public static TeamColors GetBlindness(int i) => ClientResources.Instance.colorsRegular;

  public static Color GetColor(int i)
  {
    return TeamColors.GetBlindness(ClientResources.ColorBlindness)._GetColor(i);
  }

  public Color _GetColor(int i)
  {
    if (Client.game.isTeam)
    {
      switch (i / Client._gameFacts.GetNumberPlayersPerTeam())
      {
        case 0:
          return this.team_colors[i];
        case 1:
          return this.team2Colors[i - Client._gameFacts.GetNumberPlayersPerTeam()];
        case 2:
          return this.team3Colors[i - (Client._gameFacts.GetNumberPlayersPerTeam() << 1)];
        case 3:
          return this.team4Colors[i - Client._gameFacts.GetNumberPlayersPerTeam() * 3];
        case 4:
          return this.team5Colors[i - Client._gameFacts.GetNumberPlayersPerTeam() * 4];
        case 5:
          return this.team6Colors[i - Client._gameFacts.GetNumberPlayersPerTeam() * 5];
        case 6:
          return this.team7Colors[i - Client._gameFacts.GetNumberPlayersPerTeam() * 6];
        case 7:
          return this.team8Colors[i - Client._gameFacts.GetNumberPlayersPerTeam() * 7];
        case 8:
          return this.team9Colors[i - Client._gameFacts.GetNumberPlayersPerTeam() * 8];
        case 9:
          return this.team10Colors[i - Client._gameFacts.GetNumberPlayersPerTeam() * 9];
        case 10:
          return this.team11Colors[i - Client._gameFacts.GetNumberPlayersPerTeam() * 10];
        case 11:
          return this.team12Colors[i - Client._gameFacts.GetNumberPlayersPerTeam() * 11];
      }
    }
    return this.ffa_colors[i];
  }

  internal void Set(int i, Color32 c)
  {
    if (i < this.ffa_colors.Length)
    {
      this.ffa_colors[i] = (Color) c;
    }
    else
    {
      i -= this.ffa_colors.Length;
      if (i < this.team_colors.Length)
      {
        this.team_colors[i] = (Color) c;
      }
      else
      {
        i -= this.team_colors.Length;
        if (i < this.team2Colors.Length)
        {
          this.team2Colors[i] = (Color) c;
        }
        else
        {
          i -= this.team2Colors.Length;
          if (i < this.team3Colors.Length)
          {
            this.team3Colors[i] = (Color) c;
          }
          else
          {
            i -= this.team3Colors.Length;
            if (i < this.team4Colors.Length)
            {
              this.team4Colors[i] = (Color) c;
            }
            else
            {
              i -= this.team4Colors.Length;
              if (i < this.team5Colors.Length)
              {
                this.team5Colors[i] = (Color) c;
              }
              else
              {
                i -= this.team5Colors.Length;
                if (i < this.team6Colors.Length)
                  this.team6Colors[i] = (Color) c;
                else
                  Debug.Log((object) ("Unknown index: " + (object) i + " " + (object) c));
              }
            }
          }
        }
      }
    }
  }
}

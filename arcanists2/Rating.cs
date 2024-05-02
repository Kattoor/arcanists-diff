

using UnityEngine;

#nullable disable
public static class Rating
{
  public static int Winner1v1(int winner, int loser)
  {
    return winner > loser ? Mathf.Clamp(50 - (winner - loser) / 10, 1, 100) : Mathf.Clamp(50 + (winner - loser) / 10, 1, 100);
  }

  public static int Loser1v1(int winner, int loser)
  {
    return winner > loser ? Mathf.Clamp(50 + (winner - loser) / 10, 1, 100) : Mathf.Clamp(50 - (winner - loser) / 10, 1, 100);
  }

  public static int WinnerFFA(int winner, params int[] others)
  {
    int num1 = 0;
    for (int index = 0; index < others.Length; ++index)
      num1 += others[index];
    int num2 = (num1 - winner) / (others.Length - 1);
    int num3;
    switch (others.Length)
    {
      case 3:
        num3 = 25;
        break;
      case 4:
        num3 = 16;
        break;
      case 5:
        num3 = 12;
        break;
      case 6:
        num3 = 10;
        break;
      default:
        num3 = 10 - (6 - (12 - others.Length));
        break;
    }
    return Mathf.Clamp(num3 - (winner - num2) / 10 + 50 - (winner - num2), 1, 100);
  }

  public static int SecondFFA(int Second, params int[] others)
  {
    int num1 = 0;
    for (int index = 0; index < others.Length; ++index)
      num1 += others[index];
    int num2 = (num1 - Second) / (others.Length - 1);
    int num3;
    switch (others.Length)
    {
      case 3:
        num3 = 25;
        break;
      case 4:
        num3 = 16;
        break;
      case 5:
        num3 = 12;
        break;
      case 6:
        num3 = 10;
        break;
      default:
        num3 = 10 - (6 - (12 - others.Length));
        break;
    }
    return Mathf.Clamp(num3 - (Second - num2) / 10, 1, 100);
  }

  public static int LastFFA(int last, params int[] others)
  {
    int num1 = 0;
    for (int index = 0; index < others.Length; ++index)
      num1 += others[index];
    int num2 = (num1 - last) / (others.Length - 1);
    return Mathf.Clamp(50 - (last - num2), 1, 100);
  }

  public static int TeamPoints(int avgWinner, int avgLoser)
  {
    return Mathf.Clamp(50 - (avgWinner - avgLoser), 1, 100);
  }
}

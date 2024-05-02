// Decompiled with JetBrains decompiler
// Type: Quiz
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;

#nullable disable
public class Quiz
{
  public GameSeason target;
  public GameSeason guess;

  public void Yes()
  {
    if (this.target == this.guess)
    {
      PlayerPrefs.SetInt("quizcorrectcount", PlayerPrefs.GetInt("quizcorrectcount") + 1);
      int num1 = PlayerPrefs.GetInt("quizcorrectcount");
      int num2 = PlayerPrefs.GetInt("quiz_max", 10);
      ChatBox.Instance?.NewChatMsg("Correct. You got a score of 100%! " + (num1 >= num2 ? "You've completed all the quizzes!" : (num2 - num1 < 100 ? "Only " + (object) (num2 - num1) + " quizzes left to pass before mastering the seasons." : "")), (Color) ColorScheme.GetColor(Global.ColorWhiteText));
    }
    else
      ChatBox.Instance?.NewChatMsg("Wrong. Keep at it and you'll eventually be able to tell the difference between " + (object) this.target + " and " + (object) this.guess + ".", (Color) ColorScheme.GetColor(Global.ColorWhiteText));
  }

  public void No()
  {
    if (this.target != this.guess)
    {
      PlayerPrefs.SetInt("quizcorrectcount", PlayerPrefs.GetInt("quizcorrectcount") + 1);
      int num1 = PlayerPrefs.GetInt("quizcorrectcount");
      int num2 = PlayerPrefs.GetInt("quiz_max", 10);
      ChatBox.Instance?.NewChatMsg("Correct. You've Aced the quiz! " + (num1 >= num2 ? "You've completed all the quizzes!" : (num2 - num1 < 100 ? "Only " + (object) (num2 - num1) + " quizzes left to pass before mastering the seasons." : "")), (Color) ColorScheme.GetColor(Global.ColorWhiteText));
    }
    else
      ChatBox.Instance?.NewChatMsg("Wrong. Should have used the elusive not-so-common sense.", (Color) ColorScheme.GetColor(Global.ColorWhiteText));
  }

  public void Randomize(TMP_Text t)
  {
    this.target = (GameSeason) Random.Range(1, 5);
    this.guess = (GameSeason) Random.Range(1, 5);
    string str = "";
    int num = Random.Range(0, 3);
    switch (this.target)
    {
      case GameSeason.Summer:
        switch (num)
        {
          case 1:
            str = string.Format("English Summer appears in {0}.", (object) this.guess);
            break;
          case 2:
            str = string.Format("{0} is the rainy season.", (object) this.guess);
            break;
          default:
            str = string.Format("Rain clouds generally form in {0}.", (object) this.guess);
            break;
        }
        break;
      case GameSeason.Autumn:
        switch (num)
        {
          case 1:
            str = string.Format("{0} is the most likely season for leaves to fall.", (object) this.guess);
            break;
          case 2:
            str = string.Format("Leaves tend to fall in {0}.", (object) this.guess);
            break;
          default:
            while (this.guess == GameSeason.Winter)
              this.guess = (GameSeason) Random.Range(1, 5);
            str = string.Format("{0} is right before Winter.", (object) this.guess);
            break;
        }
        break;
      case GameSeason.Winter:
        switch (num)
        {
          case 1:
            str = string.Format("Going out in {0} is a good way to see a Blizzard.", (object) this.guess);
            break;
          case 2:
            str = string.Format("{0} is the best time to build a snowman.", (object) this.guess);
            break;
          default:
            str = string.Format("Snow tend to appear {0}.", (object) this.guess);
            break;
        }
        break;
      case GameSeason.Spring:
        switch (num)
        {
          case 1:
            str = string.Format("{0} is the stormy season.", (object) this.guess);
            break;
          case 2:
            str = string.Format("Thunder is a good sign its {0}.", (object) this.guess);
            break;
          default:
            while (this.guess == GameSeason.Summer)
              this.guess = (GameSeason) Random.Range(1, 5);
            str = string.Format("{0} is right before Summer.", (object) this.guess);
            break;
        }
        break;
    }
    t.text = str;
  }
}

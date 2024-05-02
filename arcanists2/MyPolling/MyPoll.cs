

using Educative;
using Hazel;
using MoonSharp.Interpreter;
using Newtonsoft.Json;
using Newtonsoft.Json.Bson;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;

#nullable disable
namespace MyPolling
{
  [Serializable]
  public class MyPoll
  {
    public string name = "";
    public int id;
    public List<MyPoll.Item> items = new List<MyPoll.Item>();
    [MoonSharpHidden]
    [JsonIgnore]
    public bool onGoing = true;
    [MoonSharpHidden]
    [JsonIgnore]
    public int totalSubmissions;
    public bool isPublic;
    public const byte MsgRetrieve = 1;
    public const byte MsgSubmit = 2;
    public const byte MsgCreate = 3;
    public const byte MsgResults = 4;
    private static Regex sanitize = new Regex("[\\s|\\0|\\00]", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.CultureInvariant);

    [MoonSharpHidden]
    public void Intitialize()
    {
      foreach (MyPoll.Item obj in this.items)
        obj.Intitialize();
    }

    public string GetQuestionsAsString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      for (int index = 0; index < this.items.Count; ++index)
      {
        if (stringBuilder.Length > 0)
          stringBuilder.Append("\n");
        stringBuilder.Append("**").Append(index + 1).Append(") ").Append(this.items[index].question).Append("** ");
        if (this.items[index].multipleSelection)
          stringBuilder.Append("[Multiple Selection]");
        stringBuilder.Append("\n");
        foreach (MyPoll.Answer answer in this.items[index].answers)
          stringBuilder.Append(answer.text).Append("\n");
      }
      return stringBuilder.ToString();
    }

    public void SanitizeResponses()
    {
      for (int index1 = 0; index1 < this.items.Count; ++index1)
      {
        for (int index2 = 0; index2 < this.items[index1].answers.Count; ++index2)
        {
          if (this.items[index1].answers[index2].allowUserInput && !string.IsNullOrEmpty(this.items[index1].answers[index2].userInput))
          {
            this.items[index1].answers[index2].userInput = WordFilter.FilterBadWords(this.items[index1].answers[index2].userInput);
            this.items[index1].answers[index2].userInput = WordFilter.FilterForbiddenWords(this.items[index1].answers[index2].userInput);
            this.items[index1].answers[index2].userInput = MyPoll.sanitize.Replace(this.items[index1].answers[index2].userInput, " ");
          }
        }
      }
    }

    [MoonSharpHidden]
    public string GetAnswersAsString(Connection c, MyPoll p)
    {
      if (p.items.Count != this.items.Count)
        return (string) null;
      StringBuilder stringBuilder = new StringBuilder();
      for (int index1 = 0; index1 < this.items.Count; ++index1)
      {
        stringBuilder.Append("**").Append(index1 + 1).Append(") ").Append(this.items[index1].question).Append("**\n ");
        MyPoll.Item obj = this.items[index1];
        if (obj.multipleSelection)
        {
          bool flag = false;
          for (int index2 = 0; index2 < obj.answers.Count; ++index2)
          {
            MyPoll.Answer answer1 = obj.answers[index2];
            MyPoll.Answer answer2 = p.items[index1].answers[index2];
            if (answer1.checkable && answer2.isChecked)
            {
              if (flag)
                stringBuilder.Append("\n");
              if (answer1.allowUserInput && !string.IsNullOrWhiteSpace(answer2.userInput))
                stringBuilder.Append(answer1.text).Append(": ").Append(answer2.userInput);
              else
                stringBuilder.Append(answer1.text);
              flag = true;
            }
            else if (answer1.allowUserInput && !string.IsNullOrWhiteSpace(answer2.userInput))
            {
              if (flag)
                stringBuilder.Append("\n");
              stringBuilder.Append(answer1.text).Append(": ").Append(answer2.userInput);
              flag = true;
            }
          }
        }
        else
        {
          for (int index3 = 0; index3 < obj.answers.Count; ++index3)
          {
            MyPoll.Answer answer3 = obj.answers[index3];
            MyPoll.Answer answer4 = p.items[index1].answers[index3];
            if (answer3.checkable && answer4.isChecked)
            {
              if (answer3.allowUserInput && !string.IsNullOrWhiteSpace(answer4.userInput))
                stringBuilder.Append(answer3.text).Append(": ").Append(answer4.userInput);
              else
                stringBuilder.Append(answer3.text);
              stringBuilder.Append("\n");
              break;
            }
            if (answer3.allowUserInput && !string.IsNullOrWhiteSpace(answer4.userInput))
            {
              stringBuilder.Append(answer3.text).Append(": ").Append(answer4.userInput);
              stringBuilder.Append("\n");
              break;
            }
          }
        }
        stringBuilder.Append("\n");
      }
      return stringBuilder.ToString();
    }

    [MoonSharpHidden]
    public void AddResponses(MyPoll p)
    {
      if (p.items.Count != this.items.Count || p.Verify() != -1)
        return;
      for (int index1 = 0; index1 < this.items.Count; ++index1)
      {
        MyPoll.Item obj = this.items[index1];
        if (obj.multipleSelection)
        {
          for (int index2 = 0; index2 < obj.answers.Count; ++index2)
          {
            MyPoll.Answer answer1 = obj.answers[index2];
            MyPoll.Answer answer2 = p.items[index1].answers[index2];
            if (answer1.checkable && answer2.isChecked)
            {
              if (answer1.allowUserInput && !string.IsNullOrWhiteSpace(answer2.userInput))
                obj.AddUserData(answer1.text + " • " + answer2.userInput);
              obj.AddResponse(index2);
            }
            else if (answer1.allowUserInput && !string.IsNullOrWhiteSpace(answer2.userInput))
              obj.AddUserData(answer1.text + " • " + answer2.userInput);
          }
        }
        else
        {
          for (int index3 = 0; index3 < obj.answers.Count; ++index3)
          {
            MyPoll.Answer answer3 = obj.answers[index3];
            MyPoll.Answer answer4 = p.items[index1].answers[index3];
            if (answer3.checkable && answer4.isChecked)
            {
              if (answer3.allowUserInput && !string.IsNullOrWhiteSpace(answer4.userInput))
                obj.AddUserData(answer3.text + " • " + answer4.userInput);
              obj.AddResponse(index3);
              break;
            }
            if (answer3.allowUserInput && !string.IsNullOrWhiteSpace(answer4.userInput))
            {
              obj.AddUserData(answer3.text + " • " + answer4.userInput);
              break;
            }
          }
        }
      }
    }

    [MoonSharpHidden]
    public int Verify()
    {
      int num1 = 0;
      foreach (MyPoll.Item obj in this.items)
      {
        if (obj.requiresAnswer)
        {
          if (obj.multipleSelection)
          {
            bool flag = false;
            foreach (MyPoll.Answer answer in obj.answers)
            {
              if (answer.isChecked)
              {
                flag = true;
                break;
              }
              if (!answer.checkable && answer.allowUserInput && answer.userInput.Length < 501 && !string.IsNullOrWhiteSpace(answer.userInput))
              {
                flag = true;
                break;
              }
            }
            if (!flag)
              return num1;
          }
          else
          {
            int num2 = 0;
            foreach (MyPoll.Answer answer in obj.answers)
            {
              if (answer.checkable && answer.isChecked)
                ++num2;
              else if (!answer.checkable && answer.allowUserInput && answer.userInput.Length < 501 && !string.IsNullOrWhiteSpace(answer.userInput) && num2 == 0)
                ++num2;
            }
            if (num2 != 1)
              return num1;
          }
          ++num1;
        }
      }
      return -1;
    }

    public MyPoll.Item addItem(string question, bool multipleSelection = false, bool requireAnswer = true)
    {
      MyPoll.Item obj = new MyPoll.Item()
      {
        question = question,
        multipleSelection = multipleSelection,
        requiresAnswer = requireAnswer
      };
      this.items.Add(obj);
      return obj;
    }

    public static MyPoll construct(string name)
    {
      return new MyPoll() { name = name };
    }

    public override string ToString() => JsonUtility.ToJson((object) this);

    public static MyPoll fromString(string s) => JsonUtility.FromJson<MyPoll>(s);

    public void sendToServer()
    {
      if (string.IsNullOrWhiteSpace(this.name))
      {
        Tutorial.Print("Tutorial name cannot be empty");
      }
      else
      {
        MyContextMenu myContextMenu = MyContextMenu.Show();
        myContextMenu.AddSeperator("---Send to Server?---");
        myContextMenu.AddItem("Confirm Send Poll [" + this.name + "] to the server", (Action) (() =>
        {
          Client.PollMake(this);
          Tutorial.Print("Sent to server...");
        }), (Color) ColorScheme.GetColor(MyContextMenu.ColorGreen));
        myContextMenu.Rebuild();
      }
    }

    public static void closePoll()
    {
      MyContextMenu myContextMenu = MyContextMenu.Show();
      myContextMenu.AddSeperator("---Close poll on Server?---");
      myContextMenu.AddItem("Confirm", (Action) (() =>
      {
        Client.PollMake((MyPoll) null);
        Tutorial.Print("Closed....");
      }), (Color) ColorScheme.GetColor(MyContextMenu.ColorGreen));
      myContextMenu.Rebuild();
    }

    public void test() => MyPollUI.Create(this, true);

    [MoonSharpHidden]
    public static MyPoll FromFile()
    {
      string path = Application.persistentDataPath + "/activepoll.arcPoll";
      if (File.Exists(path))
      {
        try
        {
          return MyPoll.FromBytesWithResponses<MyPoll>(File.ReadAllBytes(path));
        }
        catch (Exception ex)
        {
          Debug.LogError((object) ex);
        }
      }
      return (MyPoll) null;
    }

    [MoonSharpHidden]
    public void ToFile()
    {
      File.WriteAllBytes(Application.persistentDataPath + "/activepoll.arcPoll", this.ToBytesWithResponses());
    }

    [MoonSharpHidden]
    public static byte[] ToBytes<T>(T t)
    {
      using (MemoryStream memoryStream = new MemoryStream())
      {
        using (BsonWriter bsonWriter = new BsonWriter((Stream) memoryStream))
          new JsonSerializer()
          {
            TypeNameHandling = TypeNameHandling.Auto
          }.Serialize((JsonWriter) bsonWriter, (object) t);
        return memoryStream.ToArray();
      }
    }

    [MoonSharpHidden]
    public static T FromBytes<T>(byte[] sent)
    {
      using (MemoryStream memoryStream = new MemoryStream(sent))
      {
        using (BsonReader reader = new BsonReader((Stream) memoryStream))
          return new JsonSerializer()
          {
            TypeNameHandling = TypeNameHandling.Auto,
            Binder = ((SerializationBinder) new PollCustomJsonSerializationBinder("MyPolling"))
          }.Deserialize<T>((JsonReader) reader);
      }
    }

    [MoonSharpHidden]
    public byte[] ToBytesWithResponses()
    {
      using (MemoryStream memoryStream = new MemoryStream())
      {
        using (BsonWriter bsonWriter = new BsonWriter((Stream) memoryStream))
          new JsonSerializer()
          {
            ContractResolver = ((IContractResolver) new IgnoreJsonAttributesResolver()),
            TypeNameHandling = TypeNameHandling.Auto
          }.Serialize((JsonWriter) bsonWriter, (object) this);
        return memoryStream.ToArray();
      }
    }

    [MoonSharpHidden]
    public static T FromBytesWithResponses<T>(byte[] sent)
    {
      using (MemoryStream memoryStream = new MemoryStream(sent))
      {
        using (BsonReader reader = new BsonReader((Stream) memoryStream))
          return new JsonSerializer()
          {
            ContractResolver = ((IContractResolver) new IgnoreJsonAttributesResolver()),
            TypeNameHandling = TypeNameHandling.Auto,
            Binder = ((SerializationBinder) new PollCustomJsonSerializationBinder("MyPolling"))
          }.Deserialize<T>((JsonReader) reader);
      }
    }

    [Serializable]
    public class Item
    {
      public string question = "";
      public bool multipleSelection;
      public bool requiresAnswer = true;
      public List<MyPoll.Answer> answers = new List<MyPoll.Answer>();
      [JsonIgnore]
      [MoonSharpHidden]
      public int[] responses;
      [JsonIgnore]
      [MoonSharpHidden]
      public List<string> userData = new List<string>();

      public MyPoll.Answer addAnswer(string answer, bool checkable = true, bool allowUserInput = false)
      {
        MyPoll.Answer answer1 = new MyPoll.Answer()
        {
          text = answer,
          checkable = checkable,
          allowUserInput = allowUserInput
        };
        this.answers.Add(answer1);
        return answer1;
      }

      [MoonSharpHidden]
      public void Intitialize()
      {
        this.responses = new int[this.answers.Count];
        for (int index = 0; index < this.responses.Length; ++index)
          this.responses[index] = 0;
      }

      [MoonSharpHidden]
      public void AddResponse(int key) => ++this.responses[key];

      [MoonSharpHidden]
      public void AddUserData(string d) => this.userData.Add(d);
    }

    [Serializable]
    public class Answer
    {
      public string text = "";
      public bool allowUserInput;
      public bool checkable = true;
      public string userInput = "";
      public bool isChecked;
    }
  }
}

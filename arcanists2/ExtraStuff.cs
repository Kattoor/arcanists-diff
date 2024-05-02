

using Newtonsoft.Json;
using System;

#nullable disable
[Serializable]
public class ExtraStuff
{
  [JsonIgnore]
  private byte[] _lastSpellBook = new byte[16]
  {
    byte.MaxValue,
    byte.MaxValue,
    byte.MaxValue,
    byte.MaxValue,
    byte.MaxValue,
    byte.MaxValue,
    byte.MaxValue,
    byte.MaxValue,
    byte.MaxValue,
    byte.MaxValue,
    byte.MaxValue,
    byte.MaxValue,
    byte.MaxValue,
    byte.MaxValue,
    byte.MaxValue,
    byte.MaxValue
  };

  public override bool Equals(object obj)
  {
    if (!(obj is ExtraStuff))
      return base.Equals(obj);
    ExtraStuff extraStuff = obj as ExtraStuff;
    return this.spellbookWinningStreak_Maps == extraStuff.spellbookWinningStreak_Maps && this.spellbookWinningStreak == extraStuff.spellbookWinningStreak && Global.CompareByteArrays(this.lastSpellBook, extraStuff.lastSpellBook);
  }

  public override int GetHashCode() => base.GetHashCode();

  [JsonProperty("a")]
  public int spellbookWinningStreak { get; set; }

  [JsonProperty("b")]
  public int spellbookWinningStreak_Maps { get; set; }

  [JsonProperty("c")]
  public byte[] lastSpellBook
  {
    get
    {
      if (this._lastSpellBook == null || this._lastSpellBook.Length != 16)
        this._lastSpellBook = new byte[16]
        {
          byte.MaxValue,
          byte.MaxValue,
          byte.MaxValue,
          byte.MaxValue,
          byte.MaxValue,
          byte.MaxValue,
          byte.MaxValue,
          byte.MaxValue,
          byte.MaxValue,
          byte.MaxValue,
          byte.MaxValue,
          byte.MaxValue,
          byte.MaxValue,
          byte.MaxValue,
          byte.MaxValue,
          byte.MaxValue
        };
      return this._lastSpellBook;
    }
    set
    {
      if (value == null || value.Length != 16)
        this._lastSpellBook = new byte[16]
        {
          byte.MaxValue,
          byte.MaxValue,
          byte.MaxValue,
          byte.MaxValue,
          byte.MaxValue,
          byte.MaxValue,
          byte.MaxValue,
          byte.MaxValue,
          byte.MaxValue,
          byte.MaxValue,
          byte.MaxValue,
          byte.MaxValue,
          byte.MaxValue,
          byte.MaxValue,
          byte.MaxValue,
          byte.MaxValue
        };
      else
        this._lastSpellBook = value;
    }
  }
}

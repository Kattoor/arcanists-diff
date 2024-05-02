

#nullable disable
namespace ChessConsole
{
  public struct ChessMove
  {
    public byte to;
    public byte combined;
    public float time;

    public int from => (int) this.combined & 63;

    public PromoteOptions promotion => (PromoteOptions) (((int) this.combined & 192) >> 6);

    public void Serialize(myBinaryWriter w)
    {
      w.Write(this.combined);
      w.Write(this.to);
      w.Write(this.time);
    }

    public static ChessMove Deserialize(myBinaryReader r)
    {
      return new ChessMove()
      {
        combined = r.ReadByte(),
        to = r.ReadByte(),
        time = r.ReadSingle()
      };
    }

    public bool Same(ChessMove b) => (int) this.combined == (int) b.combined;
  }
}

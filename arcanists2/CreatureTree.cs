

using UnityEngine;

#nullable disable
public class CreatureTree : Creature
{
  public Texture2D texture;
  public ExplosionCutout cutoutTexture;
  public bool isButterflyJar;

  public override ZCreature Get()
  {
    return (ZCreature) new ZCreatureTree()
    {
      baseTree = this
    };
  }

  [ContextMenu("Find Pixels Top")]
  private void FindpixelsTop()
  {
    Texture2D texture = this.GetComponent<SpriteRenderer>().sprite.texture;
    Color32[] pixels32 = texture.GetPixels32();
    new Coords[1][0] = new Coords(1, 1);
    string str = "Coords[] c = { ";
    int num1 = 0;
    int num2 = texture.width / 2;
    int num3 = texture.height / 2;
    for (int index1 = 0; index1 < texture.width; ++index1)
    {
      for (int index2 = texture.height - 1; index2 > 0; --index2)
      {
        if ((index2 == 0 || pixels32[index1 + index2 * texture.width].a == (byte) 0) && pixels32[index1 + (index2 - 1) * texture.width].a != (byte) 0)
        {
          str = str + "new Coords(" + (object) (index1 - num2) + ", " + (object) (index2 - num3) + "), ";
          ++num1;
        }
      }
    }
    string message = str + " };";
    Debug.Log((object) ("Found: " + (object) num1));
    Debug.Log((object) message);
  }
}

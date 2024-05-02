

using System;
using System.Xml.Serialization;
using UnityEngine;

#nullable disable
namespace LibNoise
{
  public class Noise2D : IDisposable
  {
    public static readonly double South = -90.0;
    public static readonly double North = 90.0;
    public static readonly double West = -180.0;
    public static readonly double East = 180.0;
    public static readonly double AngleMin = -180.0;
    public static readonly double AngleMax = 180.0;
    public static readonly double Left = -1.0;
    public static readonly double Right = 1.0;
    public static readonly double Top = -1.0;
    public static readonly double Bottom = 1.0;
    private int _width;
    private int _height;
    private float[,] _data;
    private readonly int _ucWidth;
    private readonly int _ucHeight;
    private int _ucBorder = 1;
    private readonly float[,] _ucData;
    private float _borderValue = float.NaN;
    private ModuleBase _generator;
    [XmlIgnore]
    [NonSerialized]
    private bool _disposed;

    protected Noise2D()
    {
    }

    public Noise2D(int size)
      : this(size, size)
    {
    }

    public Noise2D(int size, ModuleBase generator)
      : this(size, size, generator)
    {
    }

    public Noise2D(int width, int height, ModuleBase generator = null)
    {
      this._generator = generator;
      this._width = width;
      this._height = height;
      this._data = new float[width, height];
      this._ucWidth = width + this._ucBorder * 2;
      this._ucHeight = height + this._ucBorder * 2;
      this._ucData = new float[width + this._ucBorder * 2, height + this._ucBorder * 2];
    }

    public float this[int x, int y, bool isCropped = true]
    {
      get
      {
        if (isCropped)
        {
          if (x < 0 && x >= this._width)
            throw new ArgumentOutOfRangeException("Invalid x position");
          if (y < 0 && y >= this._height)
            throw new ArgumentOutOfRangeException("Invalid y position");
          return this._data[x, y];
        }
        if (x < 0 && x >= this._ucWidth)
          throw new ArgumentOutOfRangeException("Invalid x position");
        if (y < 0 && y >= this._ucHeight)
          throw new ArgumentOutOfRangeException("Invalid y position");
        return this._ucData[x, y];
      }
      set
      {
        if (isCropped)
        {
          if (x < 0 && x >= this._width)
            throw new ArgumentOutOfRangeException("Invalid x position");
          if (y < 0 && y >= this._height)
            throw new ArgumentOutOfRangeException("Invalid y position");
          this._data[x, y] = value;
        }
        else
        {
          if (x < 0 && x >= this._ucWidth)
            throw new ArgumentOutOfRangeException("Invalid x position");
          if (y < 0 && y >= this._ucHeight)
            throw new ArgumentOutOfRangeException("Invalid y position");
          this._ucData[x, y] = value;
        }
      }
    }

    public float Border
    {
      get => this._borderValue;
      set => this._borderValue = value;
    }

    public ModuleBase Generator
    {
      get => this._generator;
      set => this._generator = value;
    }

    public int Height => this._height;

    public int Width => this._width;

    public float[,] GetNormalizedData(bool isCropped = true, int xCrop = 0, int yCrop = 0)
    {
      return this.GetData(isCropped, xCrop, yCrop, true);
    }

    public float[,] GetData(bool isCropped = true, int xCrop = 0, int yCrop = 0, bool isNormalized = false)
    {
      int num1;
      int num2;
      float[,] numArray;
      if (isCropped)
      {
        num1 = this._width;
        num2 = this._height;
        numArray = this._data;
      }
      else
      {
        num1 = this._ucWidth;
        num2 = this._ucHeight;
        numArray = this._ucData;
      }
      int length1 = num1 - xCrop;
      int length2 = num2 - yCrop;
      float[,] data = new float[length1, length2];
      for (int index1 = 0; index1 < length1; ++index1)
      {
        for (int index2 = 0; index2 < length2; ++index2)
        {
          float num3 = !isNormalized ? numArray[index1, index2] : (float) (((double) numArray[index1, index2] + 1.0) / 2.0);
          data[index1, index2] = num3;
        }
      }
      return data;
    }

    public void Clear(float value = 0.0f)
    {
      for (int index1 = 0; index1 < this._width; ++index1)
      {
        for (int index2 = 0; index2 < this._height; ++index2)
          this._data[index1, index2] = value;
      }
    }

    private double GeneratePlanar(double x, double y) => this._generator.GetValue(x, 0.0, y);

    public void GeneratePlanar(
      double left,
      double right,
      double top,
      double bottom,
      bool isSeamless = true)
    {
      if (right <= left || bottom <= top)
        throw new ArgumentException("Invalid right/left or bottom/top combination");
      if (this._generator == null)
        throw new ArgumentNullException("Generator is null");
      double num1 = right - left;
      double num2 = bottom - top;
      double num3 = num1 / ((double) this._width - (double) this._ucBorder);
      double num4 = num2 / ((double) this._height - (double) this._ucBorder);
      double x = left;
      for (int index1 = 0; index1 < this._ucWidth; ++index1)
      {
        double y = top;
        for (int index2 = 0; index2 < this._ucHeight; ++index2)
        {
          float num5;
          if (isSeamless)
          {
            num5 = (float) this.GeneratePlanar(x, y);
          }
          else
          {
            double planar1 = this.GeneratePlanar(x, y);
            double planar2 = this.GeneratePlanar(x + num1, y);
            double planar3 = this.GeneratePlanar(x, y + num2);
            double planar4 = this.GeneratePlanar(x + num1, y + num2);
            double position1 = 1.0 - (x - left) / num1;
            double position2 = 1.0 - (y - top) / num2;
            double b = planar2;
            double position3 = position1;
            num5 = (float) Utils.InterpolateLinear(Utils.InterpolateLinear(planar1, b, position3), Utils.InterpolateLinear(planar3, planar4, position1), position2);
          }
          this._ucData[index1, index2] = num5;
          if (index1 >= this._ucBorder && index2 >= this._ucBorder && index1 < this._width + this._ucBorder && index2 < this._height + this._ucBorder)
            this._data[index1 - this._ucBorder, index2 - this._ucBorder] = num5;
          y += num4;
        }
        x += num3;
      }
    }

    private double GenerateCylindrical(double angle, double height)
    {
      return this._generator.GetValue(Math.Cos(angle * (Math.PI / 180.0)), height, Math.Sin(angle * (Math.PI / 180.0)));
    }

    public void GenerateCylindrical(
      double angleMin,
      double angleMax,
      double heightMin,
      double heightMax)
    {
      if (angleMax <= angleMin || heightMax <= heightMin)
        throw new ArgumentException("Invalid angle or height parameters");
      if (this._generator == null)
        throw new ArgumentNullException("Generator is null");
      double num1 = angleMax - angleMin;
      double num2 = heightMax - heightMin;
      double num3 = (double) this._width - (double) this._ucBorder;
      double num4 = num1 / num3;
      double num5 = num2 / ((double) this._height - (double) this._ucBorder);
      double angle = angleMin;
      for (int index1 = 0; index1 < this._ucWidth; ++index1)
      {
        double height = heightMin;
        for (int index2 = 0; index2 < this._ucHeight; ++index2)
        {
          this._ucData[index1, index2] = (float) this.GenerateCylindrical(angle, height);
          if (index1 >= this._ucBorder && index2 >= this._ucBorder && index1 < this._width + this._ucBorder && index2 < this._height + this._ucBorder)
            this._data[index1 - this._ucBorder, index2 - this._ucBorder] = (float) this.GenerateCylindrical(angle, height);
          height += num5;
        }
        angle += num4;
      }
    }

    private double GenerateSpherical(double lat, double lon)
    {
      double num = Math.Cos(Math.PI / 180.0 * lat);
      return this._generator.GetValue(num * Math.Cos(Math.PI / 180.0 * lon), Math.Sin(Math.PI / 180.0 * lat), num * Math.Sin(Math.PI / 180.0 * lon));
    }

    public void GenerateSpherical(double south, double north, double west, double east)
    {
      if (east <= west || south <= north)
        throw new ArgumentException("Invalid east/west or north/south combination");
      if (this._generator == null)
        throw new ArgumentNullException("Generator is null");
      double num1 = east - west;
      double num2 = north - south;
      double num3 = (double) this._width - (double) this._ucBorder;
      double num4 = num1 / num3;
      double num5 = num2 / ((double) this._height - (double) this._ucBorder);
      double lon = west;
      for (int index1 = 0; index1 < this._ucWidth; ++index1)
      {
        double lat = south;
        for (int index2 = 0; index2 < this._ucHeight; ++index2)
        {
          this._ucData[index1, index2] = (float) this.GenerateSpherical(lat, lon);
          if (index1 >= this._ucBorder && index2 >= this._ucBorder && index1 < this._width + this._ucBorder && index2 < this._height + this._ucBorder)
            this._data[index1 - this._ucBorder, index2 - this._ucBorder] = (float) this.GenerateSpherical(lat, lon);
          lat += num5;
        }
        lon += num4;
      }
    }

    public Texture2D GetTexture() => this.GetTexture(GradientPresets.Grayscale);

    public Texture2D GetTexture(Gradient gradient)
    {
      Texture2D texture = new Texture2D(this._width, this._height);
      Color[] colors = new Color[this._width * this._height];
      for (int index1 = 0; index1 < this._width; ++index1)
      {
        for (int index2 = 0; index2 < this._height; ++index2)
        {
          float num = float.IsNaN(this._borderValue) || index1 != 0 && index1 != this._width - this._ucBorder && index2 != 0 && index2 != this._height - this._ucBorder ? this._data[index1, index2] : this._borderValue;
          colors[index1 + index2 * this._width] = gradient.Evaluate((float) (((double) num + 1.0) / 2.0));
        }
      }
      texture.SetPixels(colors);
      texture.wrapMode = TextureWrapMode.Clamp;
      texture.Apply();
      return texture;
    }

    public Texture2D GetNormalMap(float intensity)
    {
      Texture2D normalMap = new Texture2D(this._width, this._height);
      Color[] colors = new Color[this._width * this._height];
      for (int index1 = 0; index1 < this._ucWidth; ++index1)
      {
        for (int index2 = 0; index2 < this._ucHeight; ++index2)
        {
          float num1 = (float) (((double) this._ucData[Mathf.Max(0, index1 - this._ucBorder), index2] - (double) this._ucData[Mathf.Min(index1 + this._ucBorder, this._width + this._ucBorder), index2]) / 2.0);
          float num2 = (float) (((double) this._ucData[index1, Mathf.Max(0, index2 - this._ucBorder)] - (double) this._ucData[index1, Mathf.Min(index2 + this._ucBorder, this._height + this._ucBorder)]) / 2.0);
          Vector3 vector3 = new Vector3(num1 * intensity, 0.0f, 1f) + new Vector3(0.0f, num2 * intensity, 1f);
          vector3.Normalize();
          Vector3 zero = Vector3.zero with
          {
            x = (float) (((double) vector3.x + 1.0) / 2.0),
            y = (float) (((double) vector3.y + 1.0) / 2.0),
            z = (float) (((double) vector3.z + 1.0) / 2.0)
          };
          if (index1 >= this._ucBorder && index2 >= this._ucBorder && index1 < this._width + this._ucBorder && index2 < this._height + this._ucBorder)
            colors[index1 - this._ucBorder + (index2 - this._ucBorder) * this._width] = new Color(zero.x, zero.y, zero.z);
        }
      }
      normalMap.SetPixels(colors);
      normalMap.wrapMode = TextureWrapMode.Clamp;
      normalMap.Apply();
      return normalMap;
    }

    public bool IsDisposed => this._disposed;

    public void Dispose()
    {
      if (!this._disposed)
        this._disposed = this.Disposing();
      GC.SuppressFinalize((object) this);
    }

    protected virtual bool Disposing()
    {
      this._data = (float[,]) null;
      this._width = 0;
      this._height = 0;
      return true;
    }
  }
}

// Decompiled with JetBrains decompiler
// Type: MyPerlin
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using System.Runtime.CompilerServices;

#nullable disable
public class MyPerlin
{
  private const short INLINE = 256;
  private const short OPTIMISE = 512;
  private int mSeed = 1337;
  private FixedInt mFrequency = (FixedInt) 10485L;
  private MyPerlin.NoiseType mNoiseType;
  private MyPerlin.RotationType3D mRotationType3D;
  private MyPerlin.TransformType3D mTransformType3D = MyPerlin.TransformType3D.DefaultOpenSimplex2;
  private MyPerlin.FractalType mFractalType;
  private int mOctaves = 3;
  private FixedInt mLacunarity = (FixedInt) 2097152L;
  private FixedInt mGain = (FixedInt) 524288L;
  private FixedInt mWeightedStrength = (FixedInt) 0;
  private FixedInt mPingPongStength = (FixedInt) 2097152L;
  private FixedInt mFractalBounding = (FixedInt) 1 / (FixedInt) 1835008L;
  private MyPerlin.CellularDistanceFunction mCellularDistanceFunction = MyPerlin.CellularDistanceFunction.EuclideanSq;
  private MyPerlin.CellularReturnType mCellularReturnType = MyPerlin.CellularReturnType.Distance;
  private FixedInt mCellularJitterModifier = (FixedInt) 1;
  private MyPerlin.DomainWarpType mDomainWarpType;
  private MyPerlin.TransformType3D mWarpTransformType3D = MyPerlin.TransformType3D.DefaultOpenSimplex2;
  private FixedInt mDomainWarpAmp = (FixedInt) 1;
  private static readonly FixedInt[] Gradients2D = new FixedInt[256]
  {
    (FixedInt) 136866L,
    (FixedInt) 1039605L,
    (FixedInt) 401272L,
    (FixedInt) 968757L,
    (FixedInt) 638332L,
    (FixedInt) 831891L,
    (FixedInt) 831891L,
    (FixedInt) 638332L,
    (FixedInt) 968757L,
    (FixedInt) 401272L,
    (FixedInt) 1039605L,
    (FixedInt) 136866L,
    (FixedInt) 1039605L,
    (FixedInt) -136866L,
    (FixedInt) 968757L,
    (FixedInt) -401272L,
    (FixedInt) 831891L,
    (FixedInt) -638332L,
    (FixedInt) 638332L,
    (FixedInt) -831891L,
    (FixedInt) 401272L,
    (FixedInt) -968757L,
    (FixedInt) 136866L,
    (FixedInt) -1039605L,
    (FixedInt) -136866L,
    (FixedInt) -1039605L,
    (FixedInt) -401272L,
    (FixedInt) -968757L,
    (FixedInt) -638332L,
    (FixedInt) -831891L,
    (FixedInt) -831891L,
    (FixedInt) -638332L,
    (FixedInt) -968757L,
    (FixedInt) -401272L,
    (FixedInt) -1039605L,
    (FixedInt) -136866L,
    (FixedInt) -1039605L,
    (FixedInt) 136866L,
    (FixedInt) -968757L,
    (FixedInt) 401272L,
    (FixedInt) -831891L,
    (FixedInt) 638332L,
    (FixedInt) -638332L,
    (FixedInt) 831891L,
    (FixedInt) -401272L,
    (FixedInt) 968757L,
    (FixedInt) -136866L,
    (FixedInt) 1039605L,
    (FixedInt) 136866L,
    (FixedInt) 1039605L,
    (FixedInt) 401272L,
    (FixedInt) 968757L,
    (FixedInt) 638332L,
    (FixedInt) 831891L,
    (FixedInt) 831891L,
    (FixedInt) 638332L,
    (FixedInt) 968757L,
    (FixedInt) 401272L,
    (FixedInt) 1039605L,
    (FixedInt) 136866L,
    (FixedInt) 1039605L,
    (FixedInt) -136866L,
    (FixedInt) 968757L,
    (FixedInt) -401272L,
    (FixedInt) 831891L,
    (FixedInt) -638332L,
    (FixedInt) 638332L,
    (FixedInt) -831891L,
    (FixedInt) 401272L,
    (FixedInt) -968757L,
    (FixedInt) 136866L,
    (FixedInt) -1039605L,
    (FixedInt) -136866L,
    (FixedInt) -1039605L,
    (FixedInt) -401272L,
    (FixedInt) -968757L,
    (FixedInt) -638332L,
    (FixedInt) -831891L,
    (FixedInt) -831891L,
    (FixedInt) -638332L,
    (FixedInt) -968757L,
    (FixedInt) -401272L,
    (FixedInt) -1039605L,
    (FixedInt) -136866L,
    (FixedInt) -1039605L,
    (FixedInt) 136866L,
    (FixedInt) -968757L,
    (FixedInt) 401272L,
    (FixedInt) -831891L,
    (FixedInt) 638332L,
    (FixedInt) -638332L,
    (FixedInt) 831891L,
    (FixedInt) -401272L,
    (FixedInt) 968757L,
    (FixedInt) -136866L,
    (FixedInt) 1039605L,
    (FixedInt) 136866L,
    (FixedInt) 1039605L,
    (FixedInt) 401272L,
    (FixedInt) 968757L,
    (FixedInt) 638332L,
    (FixedInt) 831891L,
    (FixedInt) 831891L,
    (FixedInt) 638332L,
    (FixedInt) 968757L,
    (FixedInt) 401272L,
    (FixedInt) 1039605L,
    (FixedInt) 136866L,
    (FixedInt) 1039605L,
    (FixedInt) -136866L,
    (FixedInt) 968757L,
    (FixedInt) -401272L,
    (FixedInt) 831891L,
    (FixedInt) -638332L,
    (FixedInt) 638332L,
    (FixedInt) -831891L,
    (FixedInt) 401272L,
    (FixedInt) -968757L,
    (FixedInt) 136866L,
    (FixedInt) -1039605L,
    (FixedInt) -136866L,
    (FixedInt) -1039605L,
    (FixedInt) -401272L,
    (FixedInt) -968757L,
    (FixedInt) -638332L,
    (FixedInt) -831891L,
    (FixedInt) -831891L,
    (FixedInt) -638332L,
    (FixedInt) -968757L,
    (FixedInt) -401272L,
    (FixedInt) -1039605L,
    (FixedInt) -136866L,
    (FixedInt) -1039605L,
    (FixedInt) 136866L,
    (FixedInt) -968757L,
    (FixedInt) 401272L,
    (FixedInt) -831891L,
    (FixedInt) 638332L,
    (FixedInt) -638332L,
    (FixedInt) 831891L,
    (FixedInt) -401272L,
    (FixedInt) 968757L,
    (FixedInt) -136866L,
    (FixedInt) 1039605L,
    (FixedInt) 136866L,
    (FixedInt) 1039605L,
    (FixedInt) 401272L,
    (FixedInt) 968757L,
    (FixedInt) 638332L,
    (FixedInt) 831891L,
    (FixedInt) 831891L,
    (FixedInt) 638332L,
    (FixedInt) 968757L,
    (FixedInt) 401272L,
    (FixedInt) 1039605L,
    (FixedInt) 136866L,
    (FixedInt) 1039605L,
    (FixedInt) -136866L,
    (FixedInt) 968757L,
    (FixedInt) -401272L,
    (FixedInt) 831891L,
    (FixedInt) -638332L,
    (FixedInt) 638332L,
    (FixedInt) -831891L,
    (FixedInt) 401272L,
    (FixedInt) -968757L,
    (FixedInt) 136866L,
    (FixedInt) -1039605L,
    (FixedInt) -136866L,
    (FixedInt) -1039605L,
    (FixedInt) -401272L,
    (FixedInt) -968757L,
    (FixedInt) -638332L,
    (FixedInt) -831891L,
    (FixedInt) -831891L,
    (FixedInt) -638332L,
    (FixedInt) -968757L,
    (FixedInt) -401272L,
    (FixedInt) -1039605L,
    (FixedInt) -136866L,
    (FixedInt) -1039605L,
    (FixedInt) 136866L,
    (FixedInt) -968757L,
    (FixedInt) 401272L,
    (FixedInt) -831891L,
    (FixedInt) 638332L,
    (FixedInt) -638332L,
    (FixedInt) 831891L,
    (FixedInt) -401272L,
    (FixedInt) 968757L,
    (FixedInt) -136866L,
    (FixedInt) 1039605L,
    (FixedInt) 136866L,
    (FixedInt) 1039605L,
    (FixedInt) 401272L,
    (FixedInt) 968757L,
    (FixedInt) 638332L,
    (FixedInt) 831891L,
    (FixedInt) 831891L,
    (FixedInt) 638332L,
    (FixedInt) 968757L,
    (FixedInt) 401272L,
    (FixedInt) 1039605L,
    (FixedInt) 136866L,
    (FixedInt) 1039605L,
    (FixedInt) -136866L,
    (FixedInt) 968757L,
    (FixedInt) -401272L,
    (FixedInt) 831891L,
    (FixedInt) -638332L,
    (FixedInt) 638332L,
    (FixedInt) -831891L,
    (FixedInt) 401272L,
    (FixedInt) -968757L,
    (FixedInt) 136866L,
    (FixedInt) -1039605L,
    (FixedInt) -136866L,
    (FixedInt) -1039605L,
    (FixedInt) -401272L,
    (FixedInt) -968757L,
    (FixedInt) -638332L,
    (FixedInt) -831891L,
    (FixedInt) -831891L,
    (FixedInt) -638332L,
    (FixedInt) -968757L,
    (FixedInt) -401272L,
    (FixedInt) -1039605L,
    (FixedInt) -136866L,
    (FixedInt) -1039605L,
    (FixedInt) 136866L,
    (FixedInt) -968757L,
    (FixedInt) 401272L,
    (FixedInt) -831891L,
    (FixedInt) 638332L,
    (FixedInt) -638332L,
    (FixedInt) 831891L,
    (FixedInt) -401272L,
    (FixedInt) 968757L,
    (FixedInt) -136866L,
    (FixedInt) 1039605L,
    (FixedInt) 401272L,
    (FixedInt) 968757L,
    (FixedInt) 968757L,
    (FixedInt) 401272L,
    (FixedInt) 968757L,
    (FixedInt) -401272L,
    (FixedInt) 401272L,
    (FixedInt) -968757L,
    (FixedInt) -401272L,
    (FixedInt) -968757L,
    (FixedInt) -968757L,
    (FixedInt) -401272L,
    (FixedInt) -968757L,
    (FixedInt) 401272L,
    (FixedInt) -401272L,
    (FixedInt) 968757L
  };
  private static readonly FixedInt[] RandVecs2D = new FixedInt[512]
  {
    (FixedInt) -283138L,
    (FixedInt) -1009625L,
    (FixedInt) 405074L,
    (FixedInt) -967174L,
    (FixedInt) 46607L,
    (FixedInt) -1047539L,
    (FixedInt) -628361L,
    (FixedInt) -839448L,
    (FixedInt) -819910L,
    (FixedInt) 653649L,
    (FixedInt) 992442L,
    (FixedInt) 338480L,
    (FixedInt) -683057L,
    (FixedInt) -795577L,
    (FixedInt) 983404L,
    (FixedInt) 363906L,
    (FixedInt) -891066L,
    (FixedInt) -552730L,
    (FixedInt) -921742L,
    (FixedInt) 499901L,
    (FixedInt) -935644L,
    (FixedInt) -473371L,
    (FixedInt) -398295L,
    (FixedInt) -969985L,
    (FixedInt) -1043506L,
    (FixedInt) 102987L,
    (FixedInt) 809961L,
    (FixedInt) -665938L,
    (FixedInt) 794116L,
    (FixedInt) -684756L,
    (FixedInt) -1041026L,
    (FixedInt) -125598L,
    (FixedInt) -55854L,
    (FixedInt) 1047087L,
    (FixedInt) 1022807L,
    (FixedInt) -231032L,
    (FixedInt) -803735L,
    (FixedInt) 673439L,
    (FixedInt) 1039806L,
    (FixedInt) 135329L,
    (FixedInt) -1043015L,
    (FixedInt) 107846L,
    (FixedInt) -564050L,
    (FixedInt) -883944L,
    (FixedInt) 526680L,
    (FixedInt) -906707L,
    (FixedInt) 478131L,
    (FixedInt) -933221L,
    (FixedInt) -907975L,
    (FixedInt) -524491L,
    (FixedInt) 92217L,
    (FixedInt) -1044513L,
    (FixedInt) -529707L,
    (FixedInt) 904942L,
    (FixedInt) 812980L,
    (FixedInt) -662249L,
    (FixedInt) -725818L,
    (FixedInt) 756768L,
    (FixedInt) -544384L,
    (FixedInt) -896190L,
    (FixedInt) 941476L,
    (FixedInt) -461663L,
    (FixedInt) -178968L,
    (FixedInt) 1033190L,
    (FixedInt) -980778L,
    (FixedInt) -370925L,
    (FixedInt) -1047779L,
    (FixedInt) 40860L,
    (FixedInt) -302206L,
    (FixedInt) -1004083L,
    (FixedInt) -1013324L,
    (FixedInt) 269603L,
    (FixedInt) -918522L,
    (FixedInt) -505794L,
    (FixedInt) -870645L,
    (FixedInt) -584369L,
    (FixedInt) 53583L,
    (FixedInt) -1047206L,
    (FixedInt) -897410L,
    (FixedInt) -542370L,
    (FixedInt) 103672L,
    (FixedInt) 1043438L,
    (FixedInt) 963538L,
    (FixedInt) 413649L,
    (FixedInt) -255787L,
    (FixedInt) -1016899L,
    (FixedInt) -851591L,
    (FixedInt) -611803L,
    (FixedInt) -1039184L,
    (FixedInt) 140029L,
    (FixedInt) 890495L,
    (FixedInt) -553651L,
    (FixedInt) -1018989L,
    (FixedInt) -247330L,
    (FixedInt) 1043276L,
    (FixedInt) 105291L,
    (FixedInt) 654423L,
    (FixedInt) -819293L,
    (FixedInt) 695111L,
    (FixedInt) 785067L,
    (FixedInt) -754703L,
    (FixedInt) 727965L,
    (FixedInt) -853894L,
    (FixedInt) -608585L,
    (FixedInt) 109598L,
    (FixedInt) -1042832L,
    (FixedInt) -111770L,
    (FixedInt) -1042601L,
    (FixedInt) 467454L,
    (FixedInt) -938614L,
    (FixedInt) 110674L,
    (FixedInt) 1042718L,
    (FixedInt) -1041016L,
    (FixedInt) 125686L,
    (FixedInt) -873921L,
    (FixedInt) 579458L,
    (FixedInt) 955835L,
    (FixedInt) -431148L,
    (FixedInt) 868802L,
    (FixedInt) -587106L,
    (FixedInt) 756767L,
    (FixedInt) -725819L,
    (FixedInt) 518048L,
    (FixedInt) -911667L,
    (FixedInt) -382973L,
    (FixedInt) -976136L,
    (FixedInt) -1016762L,
    (FixedInt) 256329L,
    (FixedInt) 93590L,
    (FixedInt) -1044390L,
    (FixedInt) 561415L,
    (FixedInt) -885621L,
    (FixedInt) -110475L,
    (FixedInt) 1042740L,
    (FixedInt) -1037071L,
    (FixedInt) 154900L,
    (FixedInt) 5091L,
    (FixedInt) 1048563L,
    (FixedInt) 1036580L,
    (FixedInt) 158155L,
    (FixedInt) 973721L,
    (FixedInt) -389073L,
    (FixedInt) -611570L,
    (FixedInt) -851758L,
    (FixedInt) 316167L,
    (FixedInt) 999774L,
    (FixedInt) -1004023L,
    (FixedInt) 302405L,
    (FixedInt) 1018775L,
    (FixedInt) -248208L,
    (FixedInt) 241153L,
    (FixedInt) 1020468L,
    (FixedInt) 1002190L,
    (FixedInt) -308423L,
    (FixedInt) 776948L,
    (FixedInt) 704174L,
    (FixedInt) -1045589L,
    (FixedInt) -79090L,
    (FixedInt) 724116L,
    (FixedInt) -758397L,
    (FixedInt) -304835L,
    (FixedInt) -1003288L,
    (FixedInt) 619999L,
    (FixedInt) -845642L,
    (FixedInt) -991385L,
    (FixedInt) -341563L,
    (FixedInt) 698818L,
    (FixedInt) 781769L,
    (FixedInt) 653906L,
    (FixedInt) 819706L,
    (FixedInt) 957034L,
    (FixedInt) -428481L,
    (FixedInt) -858968L,
    (FixedInt) 601402L,
    (FixedInt) -924083L,
    (FixedInt) -495561L,
    (FixedInt) 1043680L,
    (FixedInt) 101205L,
    (FixedInt) 1033439L,
    (FixedInt) -177520L,
    (FixedInt) -890868L,
    (FixedInt) 553051L,
    (FixedInt) 647480L,
    (FixedInt) -824791L,
    (FixedInt) 892144L,
    (FixedInt) 550989L,
    (FixedInt) 1047006L,
    (FixedInt) -57349L,
    (FixedInt) 206713L,
    (FixedInt) -1027998L,
    (FixedInt) 692883L,
    (FixedInt) -787034L,
    (FixedInt) -32479L,
    (FixedInt) 1048072L,
    (FixedInt) -705865L,
    (FixedInt) 775412L,
    (FixedInt) -754452L,
    (FixedInt) -728226L,
    (FixedInt) 1020003L,
    (FixedInt) 243114L,
    (FixedInt) 1048267L,
    (FixedInt) -25428L,
    (FixedInt) 463658L,
    (FixedInt) -940496L,
    (FixedInt) 1046620L,
    (FixedInt) -64008L,
    (FixedInt) -961928L,
    (FixedInt) -417380L,
    (FixedInt) -854595L,
    (FixedInt) -607600L,
    (FixedInt) -921628L,
    (FixedInt) 500113L,
    (FixedInt) 16631L,
    (FixedInt) 1048444L,
    (FixedInt) -848871L,
    (FixedInt) 615572L,
    (FixedInt) -961114L,
    (FixedInt) -419250L,
    (FixedInt) -841329L,
    (FixedInt) 625840L,
    (FixedInt) -542820L,
    (FixedInt) 897138L,
    (FixedInt) -855051L,
    (FixedInt) -606958L,
    (FixedInt) 421738L,
    (FixedInt) -960025L,
    (FixedInt) -949229L,
    (FixedInt) -445505L,
    (FixedInt) 767289L,
    (FixedInt) 714687L,
    (FixedInt) -592197L,
    (FixedInt) -865340L,
    (FixedInt) -881147L,
    (FixedInt) -568410L,
    (FixedInt) -976673L,
    (FixedInt) 381603L,
    (FixedInt) 549264L,
    (FixedInt) 893207L,
    (FixedInt) 779385L,
    (FixedInt) -701476L,
    (FixedInt) -1033237L,
    (FixedInt) -178698L,
    (FixedInt) 482498L,
    (FixedInt) 930970L,
    (FixedInt) 865972L,
    (FixedInt) 591273L,
    (FixedInt) 648268L,
    (FixedInt) 824172L,
    (FixedInt) 873621L,
    (FixedInt) -579911L,
    (FixedInt) 157318L,
    (FixedInt) 1036707L,
    (FixedInt) -694503L,
    (FixedInt) -785605L,
    (FixedInt) -701076L,
    (FixedInt) 779745L,
    (FixedInt) 736688L,
    (FixedInt) 746191L,
    (FixedInt) -568264L,
    (FixedInt) -881242L,
    (FixedInt) -355322L,
    (FixedInt) 986538L,
    (FixedInt) 873624L,
    (FixedInt) 579907L,
    (FixedInt) -313494L,
    (FixedInt) -1000616L,
    (FixedInt) 276669L,
    (FixedInt) 1011417L,
    (FixedInt) 130137L,
    (FixedInt) -1040469L,
    (FixedInt) -763641L,
    (FixedInt) -718584L,
    (FixedInt) 730071L,
    (FixedInt) 752667L,
    (FixedInt) -962963L,
    (FixedInt) 414985L,
    (FixedInt) -663339L,
    (FixedInt) -812090L,
    (FixedInt) -978519L,
    (FixedInt) -376843L,
    (FixedInt) -120982L,
    (FixedInt) -1041573L,
    (FixedInt) 997717L,
    (FixedInt) -322601L,
    (FixedInt) -94245L,
    (FixedInt) -1044331L,
    (FixedInt) 700291L,
    (FixedInt) 780451L,
    (FixedInt) 833869L,
    (FixedInt) -635746L,
    (FixedInt) -677590L,
    (FixedInt) -800239L,
    (FixedInt) -286638L,
    (FixedInt) 1008637L,
    (FixedInt) 1013930L,
    (FixedInt) -267315L,
    (FixedInt) -1026859L,
    (FixedInt) 212300L,
    (FixedInt) -563033L,
    (FixedInt) -884593L,
    (FixedInt) -283153L,
    (FixedInt) -1009621L,
    (FixedInt) -671117L,
    (FixedInt) 805675L,
    (FixedInt) -823607L,
    (FixedInt) -648985L,
    (FixedInt) 62976L,
    (FixedInt) -1046683L,
    (FixedInt) -25750L,
    (FixedInt) 1048259L,
    (FixedInt) -691888L,
    (FixedInt) 787909L,
    (FixedInt) -655768L,
    (FixedInt) -818217L,
    (FixedInt) -651208L,
    (FixedInt) -821851L,
    (FixedInt) 875444L,
    (FixedInt) 577155L,
    (FixedInt) -166962L,
    (FixedInt) 1035198L,
    (FixedInt) 877408L,
    (FixedInt) 574164L,
    (FixedInt) -909718L,
    (FixedInt) -521462L,
    (FixedInt) -212091L,
    (FixedInt) -1026902L,
    (FixedInt) 985576L,
    (FixedInt) 357981L,
    (FixedInt) 1035720L,
    (FixedInt) -163687L,
    (FixedInt) -947331L,
    (FixedInt) 449527L,
    (FixedInt) 133148L,
    (FixedInt) -1040087L,
    (FixedInt) -400514L,
    (FixedInt) 969071L,
    (FixedInt) 1022846L,
    (FixedInt) 230860L,
    (FixedInt) -335965L,
    (FixedInt) -993296L,
    (FixedInt) -1035443L,
    (FixedInt) 165432L,
    (FixedInt) 26585L,
    (FixedInt) -1048238L,
    (FixedInt) 507000L,
    (FixedInt) -917857L,
    (FixedInt) -298928L,
    (FixedInt) -1005063L,
    (FixedInt) -71361L,
    (FixedInt) -1046144L,
    (FixedInt) -826827L,
    (FixedInt) -644877L,
    (FixedInt) 334012L,
    (FixedInt) -993955L,
    (FixedInt) 931139L,
    (FixedInt) 482172L,
    (FixedInt) 679154L,
    (FixedInt) -798912L,
    (FixedInt) 1029726L,
    (FixedInt) 197924L,
    (FixedInt) 981181L,
    (FixedInt) -369857L,
    (FixedInt) -932697L,
    (FixedInt) 479152L,
    (FixedInt) 830764L,
    (FixedInt) 639797L,
    (FixedInt) 784735L,
    (FixedInt) 695487L,
    (FixedInt) -764299L,
    (FixedInt) -717884L,
    (FixedInt) 915305L,
    (FixedInt) -511593L,
    (FixedInt) 869096L,
    (FixedInt) 586671L,
    (FixedInt) 84667L,
    (FixedInt) 1045152L,
    (FixedInt) 1027515L,
    (FixedInt) -209103L,
    (FixedInt) -608940L,
    (FixedInt) -853641L,
    (FixedInt) -492835L,
    (FixedInt) -925540L,
    (FixedInt) 252653L,
    (FixedInt) 1017682L,
    (FixedInt) 989626L,
    (FixedInt) -346627L,
    (FixedInt) -936168L,
    (FixedInt) -472334L,
    (FixedInt) -846161L,
    (FixedInt) 619292L,
    (FixedInt) 65630L,
    (FixedInt) 1046520L,
    (FixedInt) -976496L,
    (FixedInt) 382054L,
    (FixedInt) 605809L,
    (FixedInt) 855865L,
    (FixedInt) -352331L,
    (FixedInt) -987610L,
    (FixedInt) 731834L,
    (FixedInt) -750952L,
    (FixedInt) -2105L,
    (FixedInt) -1048573L,
    (FixedInt) -191605L,
    (FixedInt) -1030921L,
    (FixedInt) -684081L,
    (FixedInt) 794697L,
    (FixedInt) -451163L,
    (FixedInt) -946553L,
    (FixedInt) -1047016L,
    (FixedInt) -57169L,
    (FixedInt) -10780L,
    (FixedInt) -1048520L,
    (FixedInt) -518633L,
    (FixedInt) 911334L,
    (FixedInt) -314504L,
    (FixedInt) 1000299L,
    (FixedInt) 856211L,
    (FixedInt) 605320L,
    (FixedInt) 282849L,
    (FixedInt) 1009706L,
    (FixedInt) -766119L,
    (FixedInt) -715941L,
    (FixedInt) -795969L,
    (FixedInt) -682601L,
    (FixedInt) -951114L,
    (FixedInt) 441465L,
    (FixedInt) -535283L,
    (FixedInt) -901655L,
    (FixedInt) 903175L,
    (FixedInt) 532715L,
    (FixedInt) 525114L,
    (FixedInt) -907615L,
    (FixedInt) -685934L,
    (FixedInt) 793098L,
    (FixedInt) -878995L,
    (FixedInt) -571732L,
    (FixedInt) 727719L,
    (FixedInt) 754941L,
    (FixedInt) 72885L,
    (FixedInt) 1046039L,
    (FixedInt) 178566L,
    (FixedInt) -1033259L,
    (FixedInt) 282693L,
    (FixedInt) 1009750L,
    (FixedInt) 578773L,
    (FixedInt) -874375L,
    (FixedInt) 236619L,
    (FixedInt) -1021529L,
    (FixedInt) 442002L,
    (FixedInt) -950865L,
    (FixedInt) 511901L,
    (FixedInt) -915132L,
    (FixedInt) -386280L,
    (FixedInt) -974832L,
    (FixedInt) -1030266L,
    (FixedInt) 195094L,
    (FixedInt) 852035L,
    (FixedInt) 611184L,
    (FixedInt) 335173L,
    (FixedInt) -993564L,
    (FixedInt) 1003583L,
    (FixedInt) 303862L,
    (FixedInt) -721069L,
    (FixedInt) -761295L,
    (FixedInt) -1047398L,
    (FixedInt) -49678L,
    (FixedInt) -131090L,
    (FixedInt) 1040349L,
    (FixedInt) -868234L,
    (FixedInt) 587945L,
    (FixedInt) 977782L,
    (FixedInt) -378750L,
    (FixedInt) 670527L,
    (FixedInt) 806166L,
    (FixedInt) -17027L,
    (FixedInt) -1048437L,
    (FixedInt) -1043858L,
    (FixedInt) -99348L,
    (FixedInt) -854099L,
    (FixedInt) 608296L,
    (FixedInt) 423344L,
    (FixedInt) -959317L,
    (FixedInt) 1042731L,
    (FixedInt) 110555L,
    (FixedInt) -170363L,
    (FixedInt) 1034643L,
    (FixedInt) -1043279L,
    (FixedInt) -105260L,
    (FixedInt) -733510L,
    (FixedInt) 749315L,
    (FixedInt) 551909L,
    (FixedInt) -891576L,
    (FixedInt) -565730L,
    (FixedInt) 882871L,
    (FixedInt) 689896L,
    (FixedInt) 789654L,
    (FixedInt) 14960L,
    (FixedInt) -1048469L,
    (FixedInt) -706151L,
    (FixedInt) 775152L,
    (FixedInt) 670472L,
    (FixedInt) -806212L,
    (FixedInt) 965903L,
    (FixedInt) 408096L,
    (FixedInt) -153760L,
    (FixedInt) -1037241L,
    (FixedInt) -820319L,
    (FixedInt) 653136L,
    (FixedInt) -528441L,
    (FixedInt) -905682L,
    (FixedInt) -811924L,
    (FixedInt) -663543L
  };
  private static readonly FixedInt[] Gradients3D = new FixedInt[256]
  {
    (FixedInt) 0,
    (FixedInt) 1,
    (FixedInt) 1,
    (FixedInt) 0,
    (FixedInt) 0,
    (FixedInt) -1,
    (FixedInt) 1,
    (FixedInt) 0,
    (FixedInt) 0,
    (FixedInt) 1,
    (FixedInt) -1,
    (FixedInt) 0,
    (FixedInt) 0,
    (FixedInt) -1,
    (FixedInt) -1,
    (FixedInt) 0,
    (FixedInt) 1,
    (FixedInt) 0,
    (FixedInt) 1,
    (FixedInt) 0,
    (FixedInt) -1,
    (FixedInt) 0,
    (FixedInt) 1,
    (FixedInt) 0,
    (FixedInt) 1,
    (FixedInt) 0,
    (FixedInt) -1,
    (FixedInt) 0,
    (FixedInt) -1,
    (FixedInt) 0,
    (FixedInt) -1,
    (FixedInt) 0,
    (FixedInt) 1,
    (FixedInt) 1,
    (FixedInt) 0,
    (FixedInt) 0,
    (FixedInt) -1,
    (FixedInt) 1,
    (FixedInt) 0,
    (FixedInt) 0,
    (FixedInt) 1,
    (FixedInt) -1,
    (FixedInt) 0,
    (FixedInt) 0,
    (FixedInt) -1,
    (FixedInt) -1,
    (FixedInt) 0,
    (FixedInt) 0,
    (FixedInt) 0,
    (FixedInt) 1,
    (FixedInt) 1,
    (FixedInt) 0,
    (FixedInt) 0,
    (FixedInt) -1,
    (FixedInt) 1,
    (FixedInt) 0,
    (FixedInt) 0,
    (FixedInt) 1,
    (FixedInt) -1,
    (FixedInt) 0,
    (FixedInt) 0,
    (FixedInt) -1,
    (FixedInt) -1,
    (FixedInt) 0,
    (FixedInt) 1,
    (FixedInt) 0,
    (FixedInt) 1,
    (FixedInt) 0,
    (FixedInt) -1,
    (FixedInt) 0,
    (FixedInt) 1,
    (FixedInt) 0,
    (FixedInt) 1,
    (FixedInt) 0,
    (FixedInt) -1,
    (FixedInt) 0,
    (FixedInt) -1,
    (FixedInt) 0,
    (FixedInt) -1,
    (FixedInt) 0,
    (FixedInt) 1,
    (FixedInt) 1,
    (FixedInt) 0,
    (FixedInt) 0,
    (FixedInt) -1,
    (FixedInt) 1,
    (FixedInt) 0,
    (FixedInt) 0,
    (FixedInt) 1,
    (FixedInt) -1,
    (FixedInt) 0,
    (FixedInt) 0,
    (FixedInt) -1,
    (FixedInt) -1,
    (FixedInt) 0,
    (FixedInt) 0,
    (FixedInt) 0,
    (FixedInt) 1,
    (FixedInt) 1,
    (FixedInt) 0,
    (FixedInt) 0,
    (FixedInt) -1,
    (FixedInt) 1,
    (FixedInt) 0,
    (FixedInt) 0,
    (FixedInt) 1,
    (FixedInt) -1,
    (FixedInt) 0,
    (FixedInt) 0,
    (FixedInt) -1,
    (FixedInt) -1,
    (FixedInt) 0,
    (FixedInt) 1,
    (FixedInt) 0,
    (FixedInt) 1,
    (FixedInt) 0,
    (FixedInt) -1,
    (FixedInt) 0,
    (FixedInt) 1,
    (FixedInt) 0,
    (FixedInt) 1,
    (FixedInt) 0,
    (FixedInt) -1,
    (FixedInt) 0,
    (FixedInt) -1,
    (FixedInt) 0,
    (FixedInt) -1,
    (FixedInt) 0,
    (FixedInt) 1,
    (FixedInt) 1,
    (FixedInt) 0,
    (FixedInt) 0,
    (FixedInt) -1,
    (FixedInt) 1,
    (FixedInt) 0,
    (FixedInt) 0,
    (FixedInt) 1,
    (FixedInt) -1,
    (FixedInt) 0,
    (FixedInt) 0,
    (FixedInt) -1,
    (FixedInt) -1,
    (FixedInt) 0,
    (FixedInt) 0,
    (FixedInt) 0,
    (FixedInt) 1,
    (FixedInt) 1,
    (FixedInt) 0,
    (FixedInt) 0,
    (FixedInt) -1,
    (FixedInt) 1,
    (FixedInt) 0,
    (FixedInt) 0,
    (FixedInt) 1,
    (FixedInt) -1,
    (FixedInt) 0,
    (FixedInt) 0,
    (FixedInt) -1,
    (FixedInt) -1,
    (FixedInt) 0,
    (FixedInt) 1,
    (FixedInt) 0,
    (FixedInt) 1,
    (FixedInt) 0,
    (FixedInt) -1,
    (FixedInt) 0,
    (FixedInt) 1,
    (FixedInt) 0,
    (FixedInt) 1,
    (FixedInt) 0,
    (FixedInt) -1,
    (FixedInt) 0,
    (FixedInt) -1,
    (FixedInt) 0,
    (FixedInt) -1,
    (FixedInt) 0,
    (FixedInt) 1,
    (FixedInt) 1,
    (FixedInt) 0,
    (FixedInt) 0,
    (FixedInt) -1,
    (FixedInt) 1,
    (FixedInt) 0,
    (FixedInt) 0,
    (FixedInt) 1,
    (FixedInt) -1,
    (FixedInt) 0,
    (FixedInt) 0,
    (FixedInt) -1,
    (FixedInt) -1,
    (FixedInt) 0,
    (FixedInt) 0,
    (FixedInt) 0,
    (FixedInt) 1,
    (FixedInt) 1,
    (FixedInt) 0,
    (FixedInt) 0,
    (FixedInt) -1,
    (FixedInt) 1,
    (FixedInt) 0,
    (FixedInt) 0,
    (FixedInt) 1,
    (FixedInt) -1,
    (FixedInt) 0,
    (FixedInt) 0,
    (FixedInt) -1,
    (FixedInt) -1,
    (FixedInt) 0,
    (FixedInt) 1,
    (FixedInt) 0,
    (FixedInt) 1,
    (FixedInt) 0,
    (FixedInt) -1,
    (FixedInt) 0,
    (FixedInt) 1,
    (FixedInt) 0,
    (FixedInt) 1,
    (FixedInt) 0,
    (FixedInt) -1,
    (FixedInt) 0,
    (FixedInt) -1,
    (FixedInt) 0,
    (FixedInt) -1,
    (FixedInt) 0,
    (FixedInt) 1,
    (FixedInt) 1,
    (FixedInt) 0,
    (FixedInt) 0,
    (FixedInt) -1,
    (FixedInt) 1,
    (FixedInt) 0,
    (FixedInt) 0,
    (FixedInt) 1,
    (FixedInt) -1,
    (FixedInt) 0,
    (FixedInt) 0,
    (FixedInt) -1,
    (FixedInt) -1,
    (FixedInt) 0,
    (FixedInt) 0,
    (FixedInt) 1,
    (FixedInt) 1,
    (FixedInt) 0,
    (FixedInt) 0,
    (FixedInt) 0,
    (FixedInt) -1,
    (FixedInt) 1,
    (FixedInt) 0,
    (FixedInt) -1,
    (FixedInt) 1,
    (FixedInt) 0,
    (FixedInt) 0,
    (FixedInt) 0,
    (FixedInt) -1,
    (FixedInt) -1,
    (FixedInt) 0
  };
  private static readonly FixedInt[] RandVecs3D = new FixedInt[1024]
  {
    (FixedInt) -764698L,
    (FixedInt) -693993L,
    (FixedInt) 181988L,
    (FixedInt) 0L,
    (FixedInt) 828681L,
    (FixedInt) -574712L,
    (FixedInt) -287235L,
    (FixedInt) 0L,
    (FixedInt) 756818L,
    (FixedInt) 652865L,
    (FixedInt) -317024L,
    (FixedInt) 0L,
    (FixedInt) 593161L,
    (FixedInt) -860702L,
    (FixedInt) -82837L,
    (FixedInt) 0L,
    (FixedInt) 796969L,
    (FixedInt) -582586L,
    (FixedInt) -353474L,
    (FixedInt) 0L,
    (FixedInt) 389435L,
    (FixedInt) 525469L,
    (FixedInt) 819593L,
    (FixedInt) 0L,
    (FixedInt) -133909L,
    (FixedInt) -446110L,
    (FixedInt) -939449L,
    (FixedInt) 0L,
    (FixedInt) -302153L,
    (FixedInt) -609834L,
    (FixedInt) 797694L,
    (FixedInt) 0L,
    (FixedInt) 613370L,
    (FixedInt) -695017L,
    (FixedInt) -490141L,
    (FixedInt) 0L,
    (FixedInt) 346782L,
    (FixedInt) 41067L,
    (FixedInt) 988720L,
    (FixedInt) 0L,
    (FixedInt) 913532L,
    (FixedInt) -431318L,
    (FixedInt) -280953L,
    (FixedInt) 0L,
    (FixedInt) 609202L,
    (FixedInt) 736301L,
    (FixedInt) 431560L,
    (FixedInt) 0L,
    (FixedInt) 528227L,
    (FixedInt) 663754L,
    (FixedInt) -616374L,
    (FixedInt) 0L,
    (FixedInt) 471199L,
    (FixedInt) 630603L,
    (FixedInt) 692691L,
    (FixedInt) 0L,
    (FixedInt) -721252L,
    (FixedInt) 94569L,
    (FixedInt) -755223L,
    (FixedInt) 0L,
    (FixedInt) -624841L,
    (FixedInt) -678360L,
    (FixedInt) 498910L,
    (FixedInt) 0L,
    (FixedInt) -537610L,
    (FixedInt) 204149L,
    (FixedInt) -876817L,
    (FixedInt) 0L,
    (FixedInt) -1039296L,
    (FixedInt) -56730L,
    (FixedInt) -127103L,
    (FixedInt) 0L,
    (FixedInt) -225414L,
    (FixedInt) 1019308L,
    (FixedInt) -98541L,
    (FixedInt) 0L,
    (FixedInt) -788387L,
    (FixedInt) -569173L,
    (FixedInt) 392426L,
    (FixedInt) 0L,
    (FixedInt) 549146L,
    (FixedInt) 893006L,
    (FixedInt) -22102L,
    (FixedInt) 0L,
    (FixedInt) 664116L,
    (FixedInt) 201973L,
    (FixedInt) -785918L,
    (FixedInt) 0L,
    (FixedInt) -71179L,
    (FixedInt) 419252L,
    (FixedInt) 958473L,
    (FixedInt) 0L,
    (FixedInt) -580767L,
    (FixedInt) -495965L,
    (FixedInt) -718497L,
    (FixedInt) 0L,
    (FixedInt) -761418L,
    (FixedInt) -619917L,
    (FixedInt) 368043L,
    (FixedInt) 0L,
    (FixedInt) -967759L,
    (FixedInt) -186941L,
    (FixedInt) 357779L,
    (FixedInt) 0L,
    (FixedInt) -730733L,
    (FixedInt) 682756L,
    (FixedInt) 315252L,
    (FixedInt) 0L,
    (FixedInt) 1007476L,
    (FixedInt) -220029L,
    (FixedInt) -189973L,
    (FixedInt) 0L,
    (FixedInt) 71482L,
    (FixedInt) -1021670L,
    (FixedInt) 224926L,
    (FixedInt) 0L,
    (FixedInt) -375105L,
    (FixedInt) -702240L,
    (FixedInt) -682397L,
    (FixedInt) 0L,
    (FixedInt) -195939L,
    (FixedInt) 802015L,
    (FixedInt) -646444L,
    (FixedInt) 0L,
    (FixedInt) -685946L,
    (FixedInt) 416066L,
    (FixedInt) 675187L,
    (FixedInt) 0L,
    (FixedInt) 733304L,
    (FixedInt) -646398L,
    (FixedInt) 379399L,
    (FixedInt) 0L,
    (FixedInt) -162179L,
    (FixedInt) 659688L,
    (FixedInt) 798761L,
    (FixedInt) 0L,
    (FixedInt) -717395L,
    (FixedInt) -270583L,
    (FixedInt) -715290L,
    (FixedInt) 0L,
    (FixedInt) 564551L,
    (FixedInt) 446552L,
    (FixedInt) 762485L,
    (FixedInt) 0L,
    (FixedInt) -527117L,
    (FixedInt) -832551L,
    (FixedInt) -358491L,
    (FixedInt) 0L,
    (FixedInt) 335855L,
    (FixedInt) 297209L,
    (FixedInt) 947828L,
    (FixedInt) 0L,
    (FixedInt) 910502L,
    (FixedInt) -394L,
    (FixedInt) -520093L,
    (FixedInt) 0L,
    (FixedInt) 829549L,
    (FixedInt) -89244L,
    (FixedInt) 635133L,
    (FixedInt) 0L,
    (FixedInt) -42058L,
    (FixedInt) -461084L,
    (FixedInt) 940820L,
    (FixedInt) 0L,
    (FixedInt) 958935L,
    (FixedInt) 375321L,
    (FixedInt) -197707L,
    (FixedInt) 0L,
    (FixedInt) -1007895L,
    (FixedInt) -289038L,
    (FixedInt) 10744L,
    (FixedInt) 0L,
    (FixedInt) 682660L,
    (FixedInt) -301759L,
    (FixedInt) -736496L,
    (FixedInt) 0L,
    (FixedInt) -214096L,
    (FixedInt) 772301L,
    (FixedInt) 676184L,
    (FixedInt) 0L,
    (FixedInt) -809318L,
    (FixedInt) 397476L,
    (FixedInt) 535282L,
    (FixedInt) 0L,
    (FixedInt) -320872L,
    (FixedInt) -806668L,
    (FixedInt) 588080L,
    (FixedInt) 0L,
    (FixedInt) 476061L,
    (FixedInt) -526892L,
    (FixedInt) 771531L,
    (FixedInt) 0L,
    (FixedInt) 505077L,
    (FixedInt) 631369L,
    (FixedInt) -667668L,
    (FixedInt) 0L,
    (FixedInt) 730016L,
    (FixedInt) -337871L,
    (FixedInt) 672629L,
    (FixedInt) 0L,
    (FixedInt) -684946L,
    (FixedInt) -711055L,
    (FixedInt) 353214L,
    (FixedInt) 0L,
    (FixedInt) 533651L,
    (FixedInt) -645363L,
    (FixedInt) -631057L,
    (FixedInt) 0L,
    (FixedInt) -171538L,
    (FixedInt) -957727L,
    (FixedInt) -390952L,
    (FixedInt) 0L,
    (FixedInt) 549537L,
    (FixedInt) -884753L,
    (FixedInt) 121373L,
    (FixedInt) 0L,
    (FixedInt) 618931L,
    (FixedInt) 522591L,
    (FixedInt) -665833L,
    (FixedInt) 0L,
    (FixedInt) 614804L,
    (FixedInt) 518798L,
    (FixedInt) 672588L,
    (FixedInt) 0L,
    (FixedInt) 710864L,
    (FixedInt) 245507L,
    (FixedInt) 730690L,
    (FixedInt) 0L,
    (FixedInt) 752568L,
    (FixedInt) -719216L,
    (FixedInt) 126016L,
    (FixedInt) 0L,
    (FixedInt) -558767L,
    (FixedInt) -545796L,
    (FixedInt) 699568L,
    (FixedInt) 0L,
    (FixedInt) -907529L,
    (FixedInt) -73476L,
    (FixedInt) -520099L,
    (FixedInt) 0L,
    (FixedInt) -300082L,
    (FixedInt) 833836L,
    (FixedInt) 560515L,
    (FixedInt) 0L,
    (FixedInt) -50851L,
    (FixedInt) 1028740L,
    (FixedInt) -196515L,
    (FixedInt) 0L,
    (FixedInt) -666739L,
    (FixedInt) 635263L,
    (FixedInt) 501408L,
    (FixedInt) 0L,
    (FixedInt) 655862L,
    (FixedInt) -300062L,
    (FixedInt) 761129L,
    (FixedInt) 0L,
    (FixedInt) -271084L,
    (FixedInt) 530783L,
    (FixedInt) -862724L,
    (FixedInt) 0L,
    (FixedInt) 22400L,
    (FixedInt) 531000L,
    (FixedInt) -903907L,
    (FixedInt) 0L,
    (FixedInt) 209832L,
    (FixedInt) 901698L,
    (FixedInt) 492364L,
    (FixedInt) 0L,
    (FixedInt) 497398L,
    (FixedInt) 630716L,
    (FixedInt) -674019L,
    (FixedInt) 0L,
    (FixedInt) 694471L,
    (FixedInt) -545519L,
    (FixedInt) -565358L,
    (FixedInt) 0L,
    (FixedInt) 84777L,
    (FixedInt) -685005L,
    (FixedInt) 789361L,
    (FixedInt) 0L,
    (FixedInt) -722855L,
    (FixedInt) 62165L,
    (FixedInt) 757051L,
    (FixedInt) 0L,
    (FixedInt) -117638L,
    (FixedInt) -1014306L,
    (FixedInt) 238441L,
    (FixedInt) 0L,
    (FixedInt) 770086L,
    (FixedInt) 627013L,
    (FixedInt) -336648L,
    (FixedInt) 0L,
    (FixedInt) 607061L,
    (FixedInt) -260974L,
    (FixedInt) 814174L,
    (FixedInt) 0L,
    (FixedInt) 732764L,
    (FixedInt) 372996L,
    (FixedInt) -650724L,
    (FixedInt) 0L,
    (FixedInt) -905638L,
    (FixedInt) -288229L,
    (FixedInt) -443005L,
    (FixedInt) 0L,
    (FixedInt) -445333L,
    (FixedInt) -486631L,
    (FixedInt) 815094L,
    (FixedInt) 0L,
    (FixedInt) 551312L,
    (FixedInt) -883636L,
    (FixedInt) 121459L,
    (FixedInt) 0L,
    (FixedInt) 979771L,
    (FixedInt) 331667L,
    (FixedInt) -171918L,
    (FixedInt) 0L,
    (FixedInt) -106622L,
    (FixedInt) -844869L,
    (FixedInt) -611832L,
    (FixedInt) 0L,
    (FixedInt) -684640L,
    (FixedInt) 530601L,
    (FixedInt) -590966L,
    (FixedInt) 0L,
    (FixedInt) -258503L,
    (FixedInt) -1013784L,
    (FixedInt) -70196L,
    (FixedInt) 0L,
    (FixedInt) -1025181L,
    (FixedInt) -220122L,
    (FixedInt) -7726L,
    (FixedInt) 0L,
    (FixedInt) 811272L,
    (FixedInt) 601279L,
    (FixedInt) 282511L,
    (FixedInt) 0L,
    (FixedInt) -639116L,
    (FixedInt) 523834L,
    (FixedInt) 645475L,
    (FixedInt) 0L,
    (FixedInt) 607601L,
    (FixedInt) 779568L,
    (FixedInt) 350150L,
    (FixedInt) 0L,
    (FixedInt) -862580L,
    (FixedInt) 85381L,
    (FixedInt) 590064L,
    (FixedInt) 0L,
    (FixedInt) -535177L,
    (FixedInt) 493215L,
    (FixedInt) 754874L,
    (FixedInt) 0L,
    (FixedInt) -604501L,
    (FixedInt) -75829L,
    (FixedInt) -853428L,
    (FixedInt) 0L,
    (FixedInt) 760283L,
    (FixedInt) 414184L,
    (FixedInt) -591550L,
    (FixedInt) 0L,
    (FixedInt) -159952L,
    (FixedInt) 509696L,
    (FixedInt) -902295L,
    (FixedInt) 0L,
    (FixedInt) -582062L,
    (FixedInt) -519865L,
    (FixedInt) 700325L,
    (FixedInt) 0L,
    (FixedInt) -197511L,
    (FixedInt) 959013L,
    (FixedInt) 375224L,
    (FixedInt) 0L,
    (FixedInt) 799597L,
    (FixedInt) -567741L,
    (FixedInt) -371247L,
    (FixedInt) 0L,
    (FixedInt) -615538L,
    (FixedInt) -338322L,
    (FixedInt) -778563L,
    (FixedInt) 0L,
    (FixedInt) 319933L,
    (FixedInt) 237244L,
    (FixedInt) -969983L,
    (FixedInt) 0L,
    (FixedInt) 668947L,
    (FixedInt) 605282L,
    (FixedInt) -534466L,
    (FixedInt) 0L,
    (FixedInt) -625661L,
    (FixedInt) 152552L,
    (FixedInt) -827518L,
    (FixedInt) 0L,
    (FixedInt) -690309L,
    (FixedInt) 687392L,
    (FixedInt) -387911L,
    (FixedInt) 0L,
    (FixedInt) 779605L,
    (FixedInt) 246529L,
    (FixedInt) 656468L,
    (FixedInt) 0L,
    (FixedInt) 583229L,
    (FixedInt) 866580L,
    (FixedInt) -91607L,
    (FixedInt) 0L,
    (FixedInt) -317607L,
    (FixedInt) -865235L,
    (FixedInt) 500004L,
    (FixedInt) 0L,
    (FixedInt) 118420L,
    (FixedInt) -1033778L,
    (FixedInt) -129573L,
    (FixedInt) 0L,
    (FixedInt) 622608L,
    (FixedInt) -618325L,
    (FixedInt) 574059L,
    (FixedInt) 0L,
    (FixedInt) 708623L,
    (FixedInt) -611923L,
    (FixedInt) -472136L,
    (FixedInt) 0L,
    (FixedInt) 759410L,
    (FixedInt) -120871L,
    (FixedInt) 712879L,
    (FixedInt) 0L,
    (FixedInt) -997396L,
    (FixedInt) 79023L,
    (FixedInt) -313794L,
    (FixedInt) 0L,
    (FixedInt) 266282L,
    (FixedInt) -197796L,
    (FixedInt) 994726L,
    (FixedInt) 0L,
    (FixedInt) 599191L,
    (FixedInt) -176103L,
    (FixedInt) -842299L,
    (FixedInt) 0L,
    (FixedInt) -71074L,
    (FixedInt) 417151L,
    (FixedInt) 959397L,
    (FixedInt) 0L,
    (FixedInt) 637007L,
    (FixedInt) 768669L,
    (FixedInt) -320751L,
    (FixedInt) 0L,
    (FixedInt) -569951L,
    (FixedInt) 175722L,
    (FixedInt) 862431L,
    (FixedInt) 0L,
    (FixedInt) -616214L,
    (FixedInt) -354423L,
    (FixedInt) -770827L,
    (FixedInt) 0L,
    (FixedInt) -835459L,
    (FixedInt) 42968L,
    (FixedInt) -632196L,
    (FixedInt) 0L,
    (FixedInt) -209332L,
    (FixedInt) 912921L,
    (FixedInt) 471451L,
    (FixedInt) 0L,
    (FixedInt) -29230L,
    (FixedInt) -954857L,
    (FixedInt) -432323L,
    (FixedInt) 0L,
    (FixedInt) -817640L,
    (FixedInt) -656160L,
    (FixedInt) 20717L,
    (FixedInt) 0L,
    (FixedInt) -546437L,
    (FixedInt) 776118L,
    (FixedInt) -445598L,
    (FixedInt) 0L,
    (FixedInt) 899198L,
    (FixedInt) 425016L,
    (FixedInt) -332136L,
    (FixedInt) 0L,
    (FixedInt) 109599L,
    (FixedInt) 879775L,
    (FixedInt) -559905L,
    (FixedInt) 0L,
    (FixedInt) 367192L,
    (FixedInt) 969148L,
    (FixedInt) -159472L,
    (FixedInt) 0L,
    (FixedInt) 208441L,
    (FixedInt) 80191L,
    (FixedInt) 1024516L,
    (FixedInt) 0L,
    (FixedInt) 822712L,
    (FixedInt) 636093L,
    (FixedInt) -134318L,
    (FixedInt) 0L,
    (FixedInt) 94442L,
    (FixedInt) -1022465L,
    (FixedInt) -212501L,
    (FixedInt) 0L,
    (FixedInt) -867627L,
    (FixedInt) -568642L,
    (FixedInt) 152903L,
    (FixedInt) 0L,
    (FixedInt) -365512L,
    (FixedInt) -436000L,
    (FixedInt) 880804L,
    (FixedInt) 0L,
    (FixedInt) -259184L,
    (FixedInt) -765965L,
    (FixedInt) -667556L,
    (FixedInt) 0L,
    (FixedInt) -387989L,
    (FixedInt) 899463L,
    (FixedInt) 374088L,
    (FixedInt) 0L,
    (FixedInt) 620064L,
    (FixedInt) -574946L,
    (FixedInt) -620054L,
    (FixedInt) 0L,
    (FixedInt) 126340L,
    (FixedInt) -799693L,
    (FixedInt) -666363L,
    (FixedInt) 0L,
    (FixedInt) 646928L,
    (FixedInt) 32292L,
    (FixedInt) 824592L,
    (FixedInt) 0L,
    (FixedInt) 131927L,
    (FixedInt) -696341L,
    (FixedInt) -772797L,
    (FixedInt) 0L,
    (FixedInt) -679221L,
    (FixedInt) -182467L,
    (FixedInt) -777736L,
    (FixedInt) 0L,
    (FixedInt) 651992L,
    (FixedInt) -818353L,
    (FixedInt) -68657L,
    (FixedInt) 0L,
    (FixedInt) 691005L,
    (FixedInt) -639315L,
    (FixedInt) 461842L,
    (FixedInt) 0L,
    (FixedInt) -282049L,
    (FixedInt) -705943L,
    (FixedInt) -722220L,
    (FixedInt) 0L,
    (FixedInt) -403678L,
    (FixedInt) 595228L,
    (FixedInt) 763058L,
    (FixedInt) 0L,
    (FixedInt) 603397L,
    (FixedInt) 850444L,
    (FixedInt) -110306L,
    (FixedInt) 0L,
    (FixedInt) 958565L,
    (FixedInt) 401913L,
    (FixedInt) 138307L,
    (FixedInt) 0L,
    (FixedInt) -113167L,
    (FixedInt) 969460L,
    (FixedInt) 383211L,
    (FixedInt) 0L,
    (FixedInt) 396337L,
    (FixedInt) 319097L,
    (FixedInt) 916845L,
    (FixedInt) 0L,
    (FixedInt) -224697L,
    (FixedInt) -866048L,
    (FixedInt) 546792L,
    (FixedInt) 0L,
    (FixedInt) 608440L,
    (FixedInt) 434959L,
    (FixedInt) -734929L,
    (FixedInt) 0L,
    (FixedInt) -207897L,
    (FixedInt) 898332L,
    (FixedInt) -499289L,
    (FixedInt) 0L,
    (FixedInt) -35458L,
    (FixedInt) 395646L,
    (FixedInt) -970421L,
    (FixedInt) 0L,
    (FixedInt) -720153L,
    (FixedInt) -697994L,
    (FixedInt) 306093L,
    (FixedInt) 0L,
    (FixedInt) 810732L,
    (FixedInt) -301548L,
    (FixedInt) -592700L,
    (FixedInt) 0L,
    (FixedInt) -101249L,
    (FixedInt) 964030L,
    (FixedInt) -399882L,
    (FixedInt) 0L,
    (FixedInt) 284761L,
    (FixedInt) -1004316L,
    (FixedInt) -98845L,
    (FixedInt) 0L,
    (FixedInt) 257007L,
    (FixedInt) -725404L,
    (FixedInt) -712212L,
    (FixedInt) 0L,
    (FixedInt) 1025193L,
    (FixedInt) -183905L,
    (FixedInt) 121114L,
    (FixedInt) 0L,
    (FixedInt) -547853L,
    (FixedInt) 893555L,
    (FixedInt) 30446L,
    (FixedInt) 0L,
    (FixedInt) -811060L,
    (FixedInt) -551686L,
    (FixedInt) 370585L,
    (FixedInt) 0L,
    (FixedInt) -748105L,
    (FixedInt) -282640L,
    (FixedInt) 678206L,
    (FixedInt) 0L,
    (FixedInt) 172389L,
    (FixedInt) 535386L,
    (FixedInt) -884960L,
    (FixedInt) 0L,
    (FixedInt) 681011L,
    (FixedInt) 58569L,
    (FixedInt) 795175L,
    (FixedInt) 0L,
    (FixedInt) -494085L,
    (FixedInt) 526100L,
    (FixedInt) -760663L,
    (FixedInt) 0L,
    (FixedInt) -664353L,
    (FixedInt) -249737L,
    (FixedInt) -771866L,
    (FixedInt) 0L,
    (FixedInt) -945976L,
    (FixedInt) -284109L,
    (FixedInt) -352026L,
    (FixedInt) 0L,
    (FixedInt) -397799L,
    (FixedInt) 914628L,
    (FixedInt) 323606L,
    (FixedInt) 0L,
    (FixedInt) -718861L,
    (FixedInt) -340802L,
    (FixedInt) 683083L,
    (FixedInt) 0L,
    (FixedInt) 304185L,
    (FixedInt) -817790L,
    (FixedInt) -581550L,
    (FixedInt) 0L,
    (FixedInt) -220024L,
    (FixedInt) 891678L,
    (FixedInt) 505974L,
    (FixedInt) 0L,
    (FixedInt) -481569L,
    (FixedInt) 691903L,
    (FixedInt) -623596L,
    (FixedInt) 0L,
    (FixedInt) 913933L,
    (FixedInt) 100834L,
    (FixedInt) -504053L,
    (FixedInt) 0L,
    (FixedInt) -710584L,
    (FixedInt) 746429L,
    (FixedInt) -193452L,
    (FixedInt) 0L,
    (FixedInt) 738656L,
    (FixedInt) 327604L,
    (FixedInt) 668261L,
    (FixedInt) 0L,
    (FixedInt) -739489L,
    (FixedInt) -251772L,
    (FixedInt) -699483L,
    (FixedInt) 0L,
    (FixedInt) 85900L,
    (FixedInt) -755743L,
    (FixedInt) -721792L,
    (FixedInt) 0L,
    (FixedInt) -733340L,
    (FixedInt) -616118L,
    (FixedInt) -426756L,
    (FixedInt) 0L,
    (FixedInt) -134370L,
    (FixedInt) 673174L,
    (FixedInt) 792648L,
    (FixedInt) 0L,
    (FixedInt) -664523L,
    (FixedInt) -711508L,
    (FixedInt) -389456L,
    (FixedInt) 0L,
    (FixedInt) 583537L,
    (FixedInt) -227424L,
    (FixedInt) -840995L,
    (FixedInt) 0L,
    (FixedInt) -607288L,
    (FixedInt) 759627L,
    (FixedInt) -392018L,
    (FixedInt) 0L,
    (FixedInt) 123289L,
    (FixedInt) -744116L,
    (FixedInt) 728424L,
    (FixedInt) 0L,
    (FixedInt) -643261L,
    (FixedInt) 138792L,
    (FixedInt) 816371L,
    (FixedInt) 0L,
    (FixedInt) 732392L,
    (FixedInt) -31252L,
    (FixedInt) -749757L,
    (FixedInt) 0L,
    (FixedInt) 872214L,
    (FixedInt) -412108L,
    (FixedInt) 410999L,
    (FixedInt) 0L,
    (FixedInt) 154096L,
    (FixedInt) 58108L,
    (FixedInt) -1035562L,
    (FixedInt) 0L,
    (FixedInt) 743302L,
    (FixedInt) -282119L,
    (FixedInt) 683682L,
    (FixedInt) 0L,
    (FixedInt) 285847L,
    (FixedInt) 706423L,
    (FixedInt) -720256L,
    (FixedInt) 0L,
    (FixedInt) -691147L,
    (FixedInt) 318290L,
    (FixedInt) -721469L,
    (FixedInt) 0L,
    (FixedInt) 504903L,
    (FixedInt) -789396L,
    (FixedInt) 470571L,
    (FixedInt) 0L,
    (FixedInt) 988808L,
    (FixedInt) 175704L,
    (FixedInt) -301492L,
    (FixedInt) 0L,
    (FixedInt) 455923L,
    (FixedInt) 806911L,
    (FixedInt) -490448L,
    (FixedInt) 0L,
    (FixedInt) 412299L,
    (FixedInt) 623350L,
    (FixedInt) 735495L,
    (FixedInt) 0L,
    (FixedInt) 760672L,
    (FixedInt) -633261L,
    (FixedInt) 346220L,
    (FixedInt) 0L,
    (FixedInt) 795893L,
    (FixedInt) -682212L,
    (FixedInt) 25515L,
    (FixedInt) 0L,
    (FixedInt) -896822L,
    (FixedInt) -359666L,
    (FixedInt) 407260L,
    (FixedInt) 0L,
    (FixedInt) -643799L,
    (FixedInt) 732086L,
    (FixedInt) 386112L,
    (FixedInt) 0L,
    (FixedInt) -782856L,
    (FixedInt) -603141L,
    (FixedInt) 350523L,
    (FixedInt) 0L,
    (FixedInt) 600840L,
    (FixedInt) 849929L,
    (FixedInt) -126973L,
    (FixedInt) 0L,
    (FixedInt) -967403L,
    (FixedInt) -364402L,
    (FixedInt) -175651L,
    (FixedInt) 0L,
    (FixedInt) -745098L,
    (FixedInt) -494895L,
    (FixedInt) -547190L,
    (FixedInt) 0L,
    (FixedInt) -89806L,
    (FixedInt) 375704L,
    (FixedInt) 974829L,
    (FixedInt) 0L,
    (FixedInt) -868189L,
    (FixedInt) -214240L,
    (FixedInt) 547594L,
    (FixedInt) 0L,
    (FixedInt) 448731L,
    (FixedInt) 291678L,
    (FixedInt) 901706L,
    (FixedInt) 0L,
    (FixedInt) 566134L,
    (FixedInt) -823878L,
    (FixedInt) -316586L,
    (FixedInt) 0L,
    (FixedInt) 595423L,
    (FixedInt) -576235L,
    (FixedInt) -642599L,
    (FixedInt) 0L,
    (FixedInt) -1037678L,
    (FixedInt) 143197L,
    (FixedInt) -47221L,
    (FixedInt) 0L,
    (FixedInt) -645329L,
    (FixedInt) -675374L,
    (FixedInt) 476371L,
    (FixedInt) 0L,
    (FixedInt) 112638L,
    (FixedInt) -833234L,
    (FixedInt) 626534L,
    (FixedInt) 0L,
    (FixedInt) -377010L,
    (FixedInt) -931715L,
    (FixedInt) 298799L,
    (FixedInt) 0L,
    (FixedInt) -228632L,
    (FixedInt) 160420L,
    (FixedInt) 1010694L,
    (FixedInt) 0L,
    (FixedInt) -763094L,
    (FixedInt) -646347L,
    (FixedInt) -315331L,
    (FixedInt) 0L,
    (FixedInt) 760189L,
    (FixedInt) -7022L,
    (FixedInt) 722201L,
    (FixedInt) 0L,
    (FixedInt) -582343L,
    (FixedInt) -559581L,
    (FixedInt) 668772L,
    (FixedInt) 0L,
    (FixedInt) 538712L,
    (FixedInt) 836366L,
    (FixedInt) -331350L,
    (FixedInt) 0L,
    (FixedInt) -397832L,
    (FixedInt) 969472L,
    (FixedInt) -36938L,
    (FixedInt) 0L,
    (FixedInt) 862899L,
    (FixedInt) 287872L,
    (FixedInt) -521580L,
    (FixedInt) 0L,
    (FixedInt) -566662L,
    (FixedInt) 638702L,
    (FixedInt) 608657L,
    (FixedInt) 0L,
    (FixedInt) 842696L,
    (FixedInt) -283433L,
    (FixedInt) 555913L,
    (FixedInt) 0L,
    (FixedInt) 633792L,
    (FixedInt) 716488L,
    (FixedInt) 429490L,
    (FixedInt) 0L,
    (FixedInt) 67003L,
    (FixedInt) 1012736L,
    (FixedInt) -263413L,
    (FixedInt) 0L,
    (FixedInt) 113992L,
    (FixedInt) 776205L,
    (FixedInt) -695717L,
    (FixedInt) 0L,
    (FixedInt) -748083L,
    (FixedInt) -726325L,
    (FixedInt) 111057L,
    (FixedInt) 0L,
    (FixedInt) 677264L,
    (FixedInt) -600262L,
    (FixedInt) -529631L,
    (FixedInt) 0L,
    (FixedInt) -687229L,
    (FixedInt) 774003L,
    (FixedInt) 167767L,
    (FixedInt) 0L,
    (FixedInt) 410093L,
    (FixedInt) 963522L,
    (FixedInt) -54387L,
    (FixedInt) 0L,
    (FixedInt) -511602L,
    (FixedInt) -619118L,
    (FixedInt) 674141L,
    (FixedInt) 0L,
    (FixedInt) 630696L,
    (FixedInt) 808183L,
    (FixedInt) -220391L,
    (FixedInt) 0L,
    (FixedInt) -595294L,
    (FixedInt) 787623L,
    (FixedInt) 353249L,
    (FixedInt) 0L,
    (FixedInt) 824031L,
    (FixedInt) 237685L,
    (FixedInt) 603315L,
    (FixedInt) 0L,
    (FixedInt) -473992L,
    (FixedInt) -633573L,
    (FixedInt) -688060L,
    (FixedInt) 0L,
    (FixedInt) 2382L,
    (FixedInt) 433360L,
    (FixedInt) -954832L,
    (FixedInt) 0L,
    (FixedInt) -609825L,
    (FixedInt) -541372L,
    (FixedInt) 659196L,
    (FixedInt) 0L,
    (FixedInt) -38836L,
    (FixedInt) 867569L,
    (FixedInt) 587645L,
    (FixedInt) 0L,
    (FixedInt) -536838L,
    (FixedInt) 833989L,
    (FixedInt) -340260L,
    (FixedInt) 0L,
    (FixedInt) -281271L,
    (FixedInt) -1003727L,
    (FixedInt) -113706L,
    (FixedInt) 0L,
    (FixedInt) -243529L,
    (FixedInt) -1014930L,
    (FixedInt) -100602L,
    (FixedInt) 0L,
    (FixedInt) 372698L,
    (FixedInt) -931293L,
    (FixedInt) 305450L,
    (FixedInt) 0L,
    (FixedInt) 770338L,
    (FixedInt) -458371L,
    (FixedInt) 544045L,
    (FixedInt) 0L,
    (FixedInt) 1047015L,
    (FixedInt) 48853L,
    (FixedInt) -29716L,
    (FixedInt) 0L,
    (FixedInt) -390876L,
    (FixedInt) -952367L,
    (FixedInt) 199308L,
    (FixedInt) 0L,
    (FixedInt) 961936L,
    (FixedInt) -365286L,
    (FixedInt) 201882L,
    (FixedInt) 0L,
    (FixedInt) 284679L,
    (FixedInt) 434900L,
    (FixedInt) -910676L,
    (FixedInt) 0L,
    (FixedInt) 538104L,
    (FixedInt) -746201L,
    (FixedInt) 503128L,
    (FixedInt) 0L,
    (FixedInt) -916177L,
    (FixedInt) 198044L,
    (FixedInt) -470008L,
    (FixedInt) 0L,
    (FixedInt) 887099L,
    (FixedInt) -390617L,
    (FixedInt) 399979L,
    (FixedInt) 0L,
    (FixedInt) 941487L,
    (FixedInt) -186668L,
    (FixedInt) -422216L,
    (FixedInt) 0L,
    (FixedInt) 228386L,
    (FixedInt) -1016942L,
    (FixedInt) -114797L,
    (FixedInt) 0L,
    (FixedInt) -159177L,
    (FixedInt) -816727L,
    (FixedInt) -638068L,
    (FixedInt) 0L,
    (FixedInt) -272670L,
    (FixedInt) -498639L,
    (FixedInt) -881204L,
    (FixedInt) 0L,
    (FixedInt) 600114L,
    (FixedInt) -783741L,
    (FixedInt) -353728L,
    (FixedInt) 0L,
    (FixedInt) -752263L,
    (FixedInt) 178154L,
    (FixedInt) -708429L,
    (FixedInt) 0L,
    (FixedInt) -717415L,
    (FixedInt) 22499L,
    (FixedInt) -764408L,
    (FixedInt) 0L,
    (FixedInt) -210496L,
    (FixedInt) 68740L,
    (FixedInt) -1024928L,
    (FixedInt) 0L,
    (FixedInt) -120460L,
    (FixedInt) -843567L,
    (FixedInt) 611060L,
    (FixedInt) 0L,
    (FixedInt) -825266L,
    (FixedInt) 36149L,
    (FixedInt) 645864L,
    (FixedInt) 0L,
    (FixedInt) -211350L,
    (FixedInt) 719309L,
    (FixedInt) 733100L,
    (FixedInt) 0L,
    (FixedInt) -89979L,
    (FixedInt) -114513L,
    (FixedInt) -1038413L,
    (FixedInt) 0L,
    (FixedInt) 580144L,
    (FixedInt) 768108L,
    (FixedInt) -415876L,
    (FixedInt) 0L,
    (FixedInt) -193198L,
    (FixedInt) -1025232L,
    (FixedInt) -105285L,
    (FixedInt) 0L,
    (FixedInt) 81314L,
    (FixedInt) -955410L,
    (FixedInt) 424370L,
    (FixedInt) 0L,
    (FixedInt) 146783L,
    (FixedInt) 797088L,
    (FixedInt) -665293L,
    (FixedInt) 0L,
    (FixedInt) 470225L,
    (FixedInt) -886349L,
    (FixedInt) 304603L,
    (FixedInt) 0L
  };
  private const int PrimeX = 501125321;
  private const int PrimeY = 1136930381;
  private const int PrimeZ = 1720413743;

  public MyPerlin(int seed = 1337) => this.SetSeed(seed);

  public void SetSeed(int seed) => this.mSeed = seed;

  public void SetFrequency(FixedInt frequency) => this.mFrequency = frequency;

  public void SetNoiseType(MyPerlin.NoiseType noiseType)
  {
    this.mNoiseType = noiseType;
    this.UpdateTransformType3D();
  }

  public void SetRotationType3D(MyPerlin.RotationType3D rotationType3D)
  {
    this.mRotationType3D = rotationType3D;
    this.UpdateTransformType3D();
    this.UpdateWarpTransformType3D();
  }

  public void SetFractalType(MyPerlin.FractalType fractalType) => this.mFractalType = fractalType;

  public void SetFractalOctaves(int octaves)
  {
    this.mOctaves = octaves;
    this.CalculateFractalBounding();
  }

  public void SetFractalLacunarity(FixedInt lacunarity) => this.mLacunarity = lacunarity;

  public void SetFractalGain(FixedInt gain)
  {
    this.mGain = gain;
    this.CalculateFractalBounding();
  }

  public void SetFractalWeightedStrength(FixedInt weightedStrength)
  {
    this.mWeightedStrength = weightedStrength;
  }

  public void SetFractalPingPongStrength(FixedInt pingPongStrength)
  {
    this.mPingPongStength = pingPongStrength;
  }

  public void SetCellularDistanceFunction(
    MyPerlin.CellularDistanceFunction cellularDistanceFunction)
  {
    this.mCellularDistanceFunction = cellularDistanceFunction;
  }

  public void SetCellularReturnType(MyPerlin.CellularReturnType cellularReturnType)
  {
    this.mCellularReturnType = cellularReturnType;
  }

  public void SetCellularJitter(FixedInt cellularJitter)
  {
    this.mCellularJitterModifier = cellularJitter;
  }

  public void SetDomainWarpType(MyPerlin.DomainWarpType domainWarpType)
  {
    this.mDomainWarpType = domainWarpType;
    this.UpdateWarpTransformType3D();
  }

  public void SetDomainWarpAmp(FixedInt domainWarpAmp) => this.mDomainWarpAmp = domainWarpAmp;

  [MethodImpl((MethodImplOptions) 512)]
  public FixedInt GetNoise(FixedInt x, FixedInt y)
  {
    this.TransformNoiseCoordinate(ref x, ref y);
    switch (this.mFractalType)
    {
      case MyPerlin.FractalType.FBm:
        return this.GenFractalFBm(x, y);
      case MyPerlin.FractalType.Ridged:
        return this.GenFractalRidged(x, y);
      case MyPerlin.FractalType.PingPong:
        return this.GenFractalPingPong(x, y);
      default:
        return this.GenNoiseSingle(this.mSeed, x, y);
    }
  }

  [MethodImpl((MethodImplOptions) 512)]
  public FixedInt GetNoise(FixedInt x, FixedInt y, FixedInt z)
  {
    this.TransformNoiseCoordinate(ref x, ref y, ref z);
    switch (this.mFractalType)
    {
      case MyPerlin.FractalType.FBm:
        return this.GenFractalFBm(x, y, z);
      case MyPerlin.FractalType.Ridged:
        return this.GenFractalRidged(x, y, z);
      case MyPerlin.FractalType.PingPong:
        return this.GenFractalPingPong(x, y, z);
      default:
        return this.GenNoiseSingle(this.mSeed, x, y, z);
    }
  }

  [MethodImpl((MethodImplOptions) 512)]
  public void DomainWarp(ref FixedInt x, ref FixedInt y)
  {
    switch (this.mFractalType)
    {
      case MyPerlin.FractalType.DomainWarpProgressive:
        this.DomainWarpFractalProgressive(ref x, ref y);
        break;
      case MyPerlin.FractalType.DomainWarpIndependent:
        this.DomainWarpFractalIndependent(ref x, ref y);
        break;
      default:
        this.DomainWarpSingle(ref x, ref y);
        break;
    }
  }

  [MethodImpl((MethodImplOptions) 512)]
  public void DomainWarp(ref FixedInt x, ref FixedInt y, ref FixedInt z)
  {
    switch (this.mFractalType)
    {
      case MyPerlin.FractalType.DomainWarpProgressive:
        this.DomainWarpFractalProgressive(ref x, ref y, ref z);
        break;
      case MyPerlin.FractalType.DomainWarpIndependent:
        this.DomainWarpFractalIndependent(ref x, ref y, ref z);
        break;
      default:
        this.DomainWarpSingle(ref x, ref y, ref z);
        break;
    }
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  private static FixedInt FastMin(FixedInt a, FixedInt b) => !(a < b) ? b : a;

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  private static FixedInt FastMax(FixedInt a, FixedInt b) => !(a > b) ? b : a;

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  private static FixedInt FastAbs(FixedInt f) => !(f < 0) ? f : -f;

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  private static FixedInt FastSqrt(FixedInt f) => Mathd.Sqrt(f);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  private static int FastFloor(FixedInt f) => !(f >= 0) ? (int) f - 1 : (int) f;

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  private static int FastRound(FixedInt f)
  {
    return !(f >= 0) ? (int) (f - (FixedInt) 524288L) : (int) (f + (FixedInt) 524288L);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  private static FixedInt Lerp(FixedInt a, FixedInt b, FixedInt t) => a + t * (b - a);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  private static FixedInt InterpHermite(FixedInt t) => t * t * ((FixedInt) 3 - (FixedInt) 2 * t);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  private static FixedInt InterpQuintic(FixedInt t) => t * t * t * (t * (t * 6 - 15) + 10);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  private static FixedInt CubicLerp(FixedInt a, FixedInt b, FixedInt c, FixedInt d, FixedInt t)
  {
    FixedInt fixedInt = d - c - (a - b);
    return t * t * t * fixedInt + t * t * (a - b - fixedInt) + t * (c - a) + b;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  private static FixedInt PingPong(FixedInt t)
  {
    t -= (int) (t * (FixedInt) 524288L) * 2;
    return !(t < 1) ? (FixedInt) 2 - t : t;
  }

  private void CalculateFractalBounding()
  {
    FixedInt fixedInt1 = MyPerlin.FastAbs(this.mGain);
    FixedInt fixedInt2 = fixedInt1;
    FixedInt fixedInt3 = (FixedInt) 1;
    for (int index = 1; index < this.mOctaves; ++index)
    {
      fixedInt3 += fixedInt2;
      fixedInt2 *= fixedInt1;
    }
    this.mFractalBounding = (FixedInt) 1 / fixedInt3;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  private static int Hash(int seed, int xPrimed, int yPrimed)
  {
    return (seed ^ xPrimed ^ yPrimed) * 668265261;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  private static int Hash(int seed, int xPrimed, int yPrimed, int zPrimed)
  {
    return (seed ^ xPrimed ^ yPrimed ^ zPrimed) * 668265261;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  private static FixedInt ValCoord(int seed, int xPrimed, int yPrimed)
  {
    int num1 = MyPerlin.Hash(seed, xPrimed, yPrimed);
    int num2 = num1 * num1;
    return (FixedInt) (num2 ^ num2 << 19) * ((FixedInt) 1 / FixedInt.MaxValue);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  private static FixedInt ValCoord(int seed, int xPrimed, int yPrimed, int zPrimed)
  {
    int num1 = MyPerlin.Hash(seed, xPrimed, yPrimed, zPrimed);
    int num2 = num1 * num1;
    return (FixedInt) (num2 ^ num2 << 19) * ((FixedInt) 1 / FixedInt.MaxValue);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  private static FixedInt GradCoord(int seed, int xPrimed, int yPrimed, FixedInt xd, FixedInt yd)
  {
    int num = MyPerlin.Hash(seed, xPrimed, yPrimed);
    int index = (num ^ num >> 15) & 254;
    FixedInt fixedInt1 = MyPerlin.Gradients2D[index];
    FixedInt fixedInt2 = MyPerlin.Gradients2D[index | 1];
    return xd * fixedInt1 + yd * fixedInt2;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  private static FixedInt GradCoord(
    int seed,
    int xPrimed,
    int yPrimed,
    int zPrimed,
    FixedInt xd,
    FixedInt yd,
    FixedInt zd)
  {
    int num = MyPerlin.Hash(seed, xPrimed, yPrimed, zPrimed);
    int index = (num ^ num >> 15) & 252;
    FixedInt fixedInt1 = MyPerlin.Gradients3D[index];
    FixedInt fixedInt2 = MyPerlin.Gradients3D[index | 1];
    FixedInt fixedInt3 = MyPerlin.Gradients3D[index | 2];
    return xd * fixedInt1 + yd * fixedInt2 + zd * fixedInt3;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  private static void GradCoordOut(
    int seed,
    int xPrimed,
    int yPrimed,
    out FixedInt xo,
    out FixedInt yo)
  {
    int index = MyPerlin.Hash(seed, xPrimed, yPrimed) & 510;
    xo = MyPerlin.RandVecs2D[index];
    yo = MyPerlin.RandVecs2D[index | 1];
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  private static void GradCoordOut(
    int seed,
    int xPrimed,
    int yPrimed,
    int zPrimed,
    out FixedInt xo,
    out FixedInt yo,
    out FixedInt zo)
  {
    int index = MyPerlin.Hash(seed, xPrimed, yPrimed, zPrimed) & 1020;
    xo = MyPerlin.RandVecs3D[index];
    yo = MyPerlin.RandVecs3D[index | 1];
    zo = MyPerlin.RandVecs3D[index | 2];
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  private static void GradCoordDual(
    int seed,
    int xPrimed,
    int yPrimed,
    FixedInt xd,
    FixedInt yd,
    out FixedInt xo,
    out FixedInt yo)
  {
    int num = MyPerlin.Hash(seed, xPrimed, yPrimed);
    int index1 = num & 254;
    int index2 = num >> 7 & 510;
    FixedInt fixedInt1 = MyPerlin.Gradients2D[index1];
    FixedInt fixedInt2 = MyPerlin.Gradients2D[index1 | 1];
    FixedInt fixedInt3 = xd * fixedInt1 + yd * fixedInt2;
    FixedInt fixedInt4 = MyPerlin.RandVecs2D[index2];
    FixedInt fixedInt5 = MyPerlin.RandVecs2D[index2 | 1];
    xo = fixedInt3 * fixedInt4;
    yo = fixedInt3 * fixedInt5;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  private static void GradCoordDual(
    int seed,
    int xPrimed,
    int yPrimed,
    int zPrimed,
    FixedInt xd,
    FixedInt yd,
    FixedInt zd,
    out FixedInt xo,
    out FixedInt yo,
    out FixedInt zo)
  {
    int num = MyPerlin.Hash(seed, xPrimed, yPrimed, zPrimed);
    int index1 = num & 252;
    int index2 = num >> 6 & 1020;
    FixedInt fixedInt1 = MyPerlin.Gradients3D[index1];
    FixedInt fixedInt2 = MyPerlin.Gradients3D[index1 | 1];
    FixedInt fixedInt3 = MyPerlin.Gradients3D[index1 | 2];
    FixedInt fixedInt4 = xd * fixedInt1 + yd * fixedInt2 + zd * fixedInt3;
    FixedInt fixedInt5 = MyPerlin.RandVecs3D[index2];
    FixedInt fixedInt6 = MyPerlin.RandVecs3D[index2 | 1];
    FixedInt fixedInt7 = MyPerlin.RandVecs3D[index2 | 2];
    xo = fixedInt4 * fixedInt5;
    yo = fixedInt4 * fixedInt6;
    zo = fixedInt4 * fixedInt7;
  }

  private FixedInt GenNoiseSingle(int seed, FixedInt x, FixedInt y)
  {
    switch (this.mNoiseType)
    {
      case MyPerlin.NoiseType.OpenSimplex2:
        return this.SingleSimplex(seed, x, y);
      case MyPerlin.NoiseType.OpenSimplex2S:
        return this.SingleOpenSimplex2S(seed, x, y);
      case MyPerlin.NoiseType.Cellular:
        return this.SingleCellular(seed, x, y);
      case MyPerlin.NoiseType.Perlin:
        return this.SinglePerlin(seed, x, y);
      case MyPerlin.NoiseType.ValueCubic:
        return this.SingleValueCubic(seed, x, y);
      case MyPerlin.NoiseType.Value:
        return this.SingleValue(seed, x, y);
      default:
        return (FixedInt) 0;
    }
  }

  private FixedInt GenNoiseSingle(int seed, FixedInt x, FixedInt y, FixedInt z)
  {
    switch (this.mNoiseType)
    {
      case MyPerlin.NoiseType.OpenSimplex2:
        return this.SingleOpenSimplex2(seed, x, y, z);
      case MyPerlin.NoiseType.OpenSimplex2S:
        return this.SingleOpenSimplex2S(seed, x, y, z);
      case MyPerlin.NoiseType.Cellular:
        return this.SingleCellular(seed, x, y, z);
      case MyPerlin.NoiseType.Perlin:
        return this.SinglePerlin(seed, x, y, z);
      case MyPerlin.NoiseType.ValueCubic:
        return this.SingleValueCubic(seed, x, y, z);
      case MyPerlin.NoiseType.Value:
        return this.SingleValue(seed, x, y, z);
      default:
        return (FixedInt) 0;
    }
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  private void TransformNoiseCoordinate(ref FixedInt x, ref FixedInt y)
  {
    x *= this.mFrequency;
    y *= this.mFrequency;
    switch (this.mNoiseType)
    {
      case MyPerlin.NoiseType.OpenSimplex2:
      case MyPerlin.NoiseType.OpenSimplex2S:
        FixedInt fixedInt1 = (FixedInt) 524288L * ((FixedInt) 1.73205078f - 1);
        FixedInt fixedInt2 = (x + y) * fixedInt1;
        x += fixedInt2;
        y += fixedInt2;
        break;
    }
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  private void TransformNoiseCoordinate(ref FixedInt x, ref FixedInt y, ref FixedInt z)
  {
    x *= this.mFrequency;
    y *= this.mFrequency;
    z *= this.mFrequency;
    switch (this.mTransformType3D)
    {
      case MyPerlin.TransformType3D.ImproveXYPlanes:
        FixedInt fixedInt1 = x + y;
        FixedInt fixedInt2 = fixedInt1 * -(FixedInt) 0.211324871f;
        z *= (FixedInt) 0.577350259f;
        x += fixedInt2 - z;
        y = y + fixedInt2 - z;
        z += fixedInt1 * (FixedInt) 0.577350259f;
        break;
      case MyPerlin.TransformType3D.ImproveXZPlanes:
        FixedInt fixedInt3 = x + z;
        FixedInt fixedInt4 = fixedInt3 * -(FixedInt) 0.211324871f;
        y *= (FixedInt) 0.577350259f;
        x += fixedInt4 - y;
        z += fixedInt4 - y;
        y += fixedInt3 * (FixedInt) 0.577350259f;
        break;
      case MyPerlin.TransformType3D.DefaultOpenSimplex2:
        FixedInt fixedInt5 = (FixedInt) 0.6666667f;
        FixedInt fixedInt6 = (x + y + z) * fixedInt5;
        x = fixedInt6 - x;
        y = fixedInt6 - y;
        z = fixedInt6 - z;
        break;
    }
  }

  private void UpdateTransformType3D()
  {
    switch (this.mRotationType3D)
    {
      case MyPerlin.RotationType3D.ImproveXYPlanes:
        this.mTransformType3D = MyPerlin.TransformType3D.ImproveXYPlanes;
        break;
      case MyPerlin.RotationType3D.ImproveXZPlanes:
        this.mTransformType3D = MyPerlin.TransformType3D.ImproveXZPlanes;
        break;
      default:
        switch (this.mNoiseType)
        {
          case MyPerlin.NoiseType.OpenSimplex2:
          case MyPerlin.NoiseType.OpenSimplex2S:
            this.mTransformType3D = MyPerlin.TransformType3D.DefaultOpenSimplex2;
            return;
          default:
            this.mTransformType3D = MyPerlin.TransformType3D.None;
            return;
        }
    }
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  private void TransformDomainWarpCoordinate(ref FixedInt x, ref FixedInt y)
  {
    switch (this.mDomainWarpType)
    {
      case MyPerlin.DomainWarpType.OpenSimplex2:
      case MyPerlin.DomainWarpType.OpenSimplex2Reduced:
        FixedInt fixedInt1 = (FixedInt) 524288L * ((FixedInt) 1.73205078f - 1);
        FixedInt fixedInt2 = (x + y) * fixedInt1;
        x += fixedInt2;
        y += fixedInt2;
        break;
    }
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  private void TransformDomainWarpCoordinate(ref FixedInt x, ref FixedInt y, ref FixedInt z)
  {
    switch (this.mWarpTransformType3D)
    {
      case MyPerlin.TransformType3D.ImproveXYPlanes:
        FixedInt fixedInt1 = x + y;
        FixedInt fixedInt2 = fixedInt1 * -(FixedInt) 0.211324871f;
        z *= (FixedInt) 0.577350259f;
        x += fixedInt2 - z;
        y = y + fixedInt2 - z;
        z += fixedInt1 * (FixedInt) 0.577350259f;
        break;
      case MyPerlin.TransformType3D.ImproveXZPlanes:
        FixedInt fixedInt3 = x + z;
        FixedInt fixedInt4 = fixedInt3 * -(FixedInt) 0.211324871f;
        y *= (FixedInt) 0.577350259f;
        x += fixedInt4 - y;
        z += fixedInt4 - y;
        y += fixedInt3 * (FixedInt) 0.577350259f;
        break;
      case MyPerlin.TransformType3D.DefaultOpenSimplex2:
        FixedInt fixedInt5 = (FixedInt) 0.6666667f;
        FixedInt fixedInt6 = (x + y + z) * fixedInt5;
        x = fixedInt6 - x;
        y = fixedInt6 - y;
        z = fixedInt6 - z;
        break;
    }
  }

  private void UpdateWarpTransformType3D()
  {
    switch (this.mRotationType3D)
    {
      case MyPerlin.RotationType3D.ImproveXYPlanes:
        this.mWarpTransformType3D = MyPerlin.TransformType3D.ImproveXYPlanes;
        break;
      case MyPerlin.RotationType3D.ImproveXZPlanes:
        this.mWarpTransformType3D = MyPerlin.TransformType3D.ImproveXZPlanes;
        break;
      default:
        switch (this.mDomainWarpType)
        {
          case MyPerlin.DomainWarpType.OpenSimplex2:
          case MyPerlin.DomainWarpType.OpenSimplex2Reduced:
            this.mWarpTransformType3D = MyPerlin.TransformType3D.DefaultOpenSimplex2;
            return;
          default:
            this.mWarpTransformType3D = MyPerlin.TransformType3D.None;
            return;
        }
    }
  }

  private FixedInt GenFractalFBm(FixedInt x, FixedInt y)
  {
    int mSeed = this.mSeed;
    FixedInt fixedInt1 = (FixedInt) 0;
    FixedInt fixedInt2 = this.mFractalBounding;
    for (int index = 0; index < this.mOctaves; ++index)
    {
      FixedInt fixedInt3 = this.GenNoiseSingle(mSeed++, x, y);
      fixedInt1 += fixedInt3 * fixedInt2;
      FixedInt fixedInt4 = fixedInt2 * MyPerlin.Lerp((FixedInt) 1, MyPerlin.FastMin(fixedInt3 + 1, (FixedInt) 2) * (FixedInt) 524288L, this.mWeightedStrength);
      x *= this.mLacunarity;
      y *= this.mLacunarity;
      fixedInt2 = fixedInt4 * this.mGain;
    }
    return fixedInt1;
  }

  private FixedInt GenFractalFBm(FixedInt x, FixedInt y, FixedInt z)
  {
    int mSeed = this.mSeed;
    FixedInt fixedInt1 = (FixedInt) 0;
    FixedInt fixedInt2 = this.mFractalBounding;
    for (int index = 0; index < this.mOctaves; ++index)
    {
      FixedInt fixedInt3 = this.GenNoiseSingle(mSeed++, x, y, z);
      fixedInt1 += fixedInt3 * fixedInt2;
      FixedInt fixedInt4 = fixedInt2 * MyPerlin.Lerp((FixedInt) 1, (fixedInt3 + 1) * (FixedInt) 524288L, this.mWeightedStrength);
      x *= this.mLacunarity;
      y *= this.mLacunarity;
      z *= this.mLacunarity;
      fixedInt2 = fixedInt4 * this.mGain;
    }
    return fixedInt1;
  }

  private FixedInt GenFractalRidged(FixedInt x, FixedInt y)
  {
    int mSeed = this.mSeed;
    FixedInt fixedInt1 = (FixedInt) 0;
    FixedInt fixedInt2 = this.mFractalBounding;
    for (int index = 0; index < this.mOctaves; ++index)
    {
      FixedInt fixedInt3 = MyPerlin.FastAbs(this.GenNoiseSingle(mSeed++, x, y));
      fixedInt1 += (fixedInt3 * -2 + 1) * fixedInt2;
      FixedInt fixedInt4 = fixedInt2 * MyPerlin.Lerp((FixedInt) 1, (FixedInt) 1 - fixedInt3, this.mWeightedStrength);
      x *= this.mLacunarity;
      y *= this.mLacunarity;
      fixedInt2 = fixedInt4 * this.mGain;
    }
    return fixedInt1;
  }

  private FixedInt GenFractalRidged(FixedInt x, FixedInt y, FixedInt z)
  {
    int mSeed = this.mSeed;
    FixedInt fixedInt1 = (FixedInt) 0;
    FixedInt fixedInt2 = this.mFractalBounding;
    for (int index = 0; index < this.mOctaves; ++index)
    {
      FixedInt fixedInt3 = MyPerlin.FastAbs(this.GenNoiseSingle(mSeed++, x, y, z));
      fixedInt1 += (fixedInt3 * -2 + 1) * fixedInt2;
      FixedInt fixedInt4 = fixedInt2 * MyPerlin.Lerp((FixedInt) 1, (FixedInt) 1 - fixedInt3, this.mWeightedStrength);
      x *= this.mLacunarity;
      y *= this.mLacunarity;
      z *= this.mLacunarity;
      fixedInt2 = fixedInt4 * this.mGain;
    }
    return fixedInt1;
  }

  private FixedInt GenFractalPingPong(FixedInt x, FixedInt y)
  {
    int mSeed = this.mSeed;
    FixedInt fixedInt1 = (FixedInt) 0;
    FixedInt fixedInt2 = this.mFractalBounding;
    for (int index = 0; index < this.mOctaves; ++index)
    {
      FixedInt b = MyPerlin.PingPong((this.GenNoiseSingle(mSeed++, x, y) + 1) * this.mPingPongStength);
      fixedInt1 += (b - (FixedInt) 524288L) * 2 * fixedInt2;
      FixedInt fixedInt3 = fixedInt2 * MyPerlin.Lerp((FixedInt) 1, b, this.mWeightedStrength);
      x *= this.mLacunarity;
      y *= this.mLacunarity;
      fixedInt2 = fixedInt3 * this.mGain;
    }
    return fixedInt1;
  }

  private FixedInt GenFractalPingPong(FixedInt x, FixedInt y, FixedInt z)
  {
    int mSeed = this.mSeed;
    FixedInt fixedInt1 = (FixedInt) 0;
    FixedInt fixedInt2 = this.mFractalBounding;
    for (int index = 0; index < this.mOctaves; ++index)
    {
      FixedInt b = MyPerlin.PingPong((this.GenNoiseSingle(mSeed++, x, y, z) + 1) * this.mPingPongStength);
      fixedInt1 += (b - (FixedInt) 524288L) * 2 * fixedInt2;
      FixedInt fixedInt3 = fixedInt2 * MyPerlin.Lerp((FixedInt) 1, b, this.mWeightedStrength);
      x *= this.mLacunarity;
      y *= this.mLacunarity;
      z *= this.mLacunarity;
      fixedInt2 = fixedInt3 * this.mGain;
    }
    return fixedInt1;
  }

  private FixedInt SingleSimplex(int seed, FixedInt x, FixedInt y)
  {
    FixedInt fixedInt1 = ((FixedInt) 3 - (FixedInt) 1816187L) / 6;
    int num1 = MyPerlin.FastFloor(x);
    int num2 = MyPerlin.FastFloor(y);
    FixedInt fixedInt2 = x - num1;
    FixedInt fixedInt3 = y - num2;
    FixedInt fixedInt4 = (fixedInt2 + fixedInt3) * fixedInt1;
    FixedInt xd1 = fixedInt2 - fixedInt4;
    FixedInt yd1 = fixedInt3 - fixedInt4;
    int xPrimed = num1 * 501125321;
    int yPrimed = num2 * 1136930381;
    FixedInt fixedInt5 = (FixedInt) 524288L - xd1 * xd1 - yd1 * yd1;
    FixedInt fixedInt6 = !(fixedInt5 <= 0) ? fixedInt5 * fixedInt5 * (fixedInt5 * fixedInt5) * MyPerlin.GradCoord(seed, xPrimed, yPrimed, xd1, yd1) : (FixedInt) 0;
    FixedInt fixedInt7 = (FixedInt) 2 * ((FixedInt) 1 - (FixedInt) 2 * fixedInt1) * ((FixedInt) 1 / fixedInt1 - 2) * fixedInt4 + ((FixedInt) -2 * ((FixedInt) 1 - (FixedInt) 2 * fixedInt1) * ((FixedInt) 1 - (FixedInt) 2 * fixedInt1) + fixedInt5);
    FixedInt fixedInt8;
    if (fixedInt7 <= 0)
    {
      fixedInt8 = (FixedInt) 0;
    }
    else
    {
      FixedInt xd2 = xd1 + ((FixedInt) 2 * fixedInt1 - 1);
      FixedInt yd2 = yd1 + ((FixedInt) 2 * fixedInt1 - 1);
      fixedInt8 = fixedInt7 * fixedInt7 * (fixedInt7 * fixedInt7) * MyPerlin.GradCoord(seed, xPrimed + 501125321, yPrimed + 1136930381, xd2, yd2);
    }
    FixedInt fixedInt9;
    if (yd1 > xd1)
    {
      FixedInt xd3 = xd1 + fixedInt1;
      FixedInt yd3 = yd1 + (fixedInt1 - 1);
      FixedInt fixedInt10 = (FixedInt) 524288L - xd3 * xd3 - yd3 * yd3;
      fixedInt9 = !(fixedInt10 <= 0) ? fixedInt10 * fixedInt10 * (fixedInt10 * fixedInt10) * MyPerlin.GradCoord(seed, xPrimed, yPrimed + 1136930381, xd3, yd3) : (FixedInt) 0;
    }
    else
    {
      FixedInt xd4 = xd1 + (fixedInt1 - 1);
      FixedInt yd4 = yd1 + fixedInt1;
      FixedInt fixedInt11 = (FixedInt) 524288L - xd4 * xd4 - yd4 * yd4;
      fixedInt9 = !(fixedInt11 <= 0) ? fixedInt11 * fixedInt11 * (fixedInt11 * fixedInt11) * MyPerlin.GradCoord(seed, xPrimed + 501125321, yPrimed, xd4, yd4) : (FixedInt) 0;
    }
    return (fixedInt6 + fixedInt9 + fixedInt8) * (FixedInt) 104686524L;
  }

  private FixedInt SingleOpenSimplex2(int seed, FixedInt x, FixedInt y, FixedInt z)
  {
    int num1 = MyPerlin.FastRound(x);
    int num2 = MyPerlin.FastRound(y);
    int num3 = MyPerlin.FastRound(z);
    FixedInt xd = x - num1;
    FixedInt yd = y - num2;
    FixedInt zd = z - num3;
    int num4 = (int) (-(FixedInt) 1 - xd) | 1;
    int num5 = (int) (-(FixedInt) 1 - yd) | 1;
    int num6 = (int) (-(FixedInt) 1 - zd) | 1;
    FixedInt fixedInt1 = (FixedInt) num4 * -xd;
    FixedInt fixedInt2 = (FixedInt) num5 * -yd;
    FixedInt fixedInt3 = (FixedInt) num6 * -zd;
    int xPrimed = num1 * 501125321;
    int yPrimed = num2 * 1136930381;
    int zPrimed = num3 * 1720413743;
    FixedInt fixedInt4 = (FixedInt) 0;
    FixedInt fixedInt5 = (FixedInt) 629145L - xd * xd - (yd * yd + zd * zd);
    int num7 = 0;
    while (true)
    {
      if (fixedInt5 > 0)
        fixedInt4 += fixedInt5 * fixedInt5 * (fixedInt5 * fixedInt5) * MyPerlin.GradCoord(seed, xPrimed, yPrimed, zPrimed, xd, yd, zd);
      if (fixedInt1 >= fixedInt2 && fixedInt1 >= fixedInt3)
      {
        FixedInt fixedInt6 = fixedInt5 + fixedInt1 + fixedInt1;
        if (fixedInt6 > 1)
        {
          FixedInt fixedInt7 = fixedInt6 - 1;
          fixedInt4 += fixedInt7 * fixedInt7 * (fixedInt7 * fixedInt7) * MyPerlin.GradCoord(seed, xPrimed - num4 * 501125321, yPrimed, zPrimed, xd + num4, yd, zd);
        }
      }
      else if (fixedInt2 > fixedInt1 && fixedInt2 >= fixedInt3)
      {
        FixedInt fixedInt8 = fixedInt5 + fixedInt2 + fixedInt2;
        if (fixedInt8 > 1)
        {
          FixedInt fixedInt9 = fixedInt8 - 1;
          fixedInt4 += fixedInt9 * fixedInt9 * (fixedInt9 * fixedInt9) * MyPerlin.GradCoord(seed, xPrimed, yPrimed - num5 * 1136930381, zPrimed, xd, yd + num5, zd);
        }
      }
      else
      {
        FixedInt fixedInt10 = fixedInt5 + fixedInt3 + fixedInt3;
        if (fixedInt10 > 1)
        {
          FixedInt fixedInt11 = fixedInt10 - 1;
          fixedInt4 += fixedInt11 * fixedInt11 * (fixedInt11 * fixedInt11) * MyPerlin.GradCoord(seed, xPrimed, yPrimed, zPrimed - num6 * 1720413743, xd, yd, zd + num6);
        }
      }
      if (num7 != 1)
      {
        fixedInt1 = (FixedInt) 524288L - fixedInt1;
        fixedInt2 = (FixedInt) 524288L - fixedInt2;
        fixedInt3 = (FixedInt) 524288L - fixedInt3;
        xd = (FixedInt) num4 * fixedInt1;
        yd = (FixedInt) num5 * fixedInt2;
        zd = (FixedInt) num6 * fixedInt3;
        fixedInt5 += (FixedInt) 786432L - fixedInt1 - (fixedInt2 + fixedInt3);
        xPrimed += num4 >> 1 & 501125321;
        yPrimed += num5 >> 1 & 1136930381;
        zPrimed += num6 >> 1 & 1720413743;
        num4 = -num4;
        num5 = -num5;
        num6 = -num6;
        seed = ~seed;
        ++num7;
      }
      else
        break;
    }
    return fixedInt4 * (FixedInt) 34282437L;
  }

  private FixedInt SingleOpenSimplex2S(int seed, FixedInt x, FixedInt y)
  {
    FixedInt fixedInt1 = ((FixedInt) 3 - (FixedInt) 1.73205078f) / 6;
    int num1 = MyPerlin.FastFloor(x);
    int num2 = MyPerlin.FastFloor(y);
    FixedInt fixedInt2 = x - num1;
    FixedInt fixedInt3 = y - num2;
    int xPrimed1 = num1 * 501125321;
    int yPrimed1 = num2 * 1136930381;
    int xPrimed2 = xPrimed1 + 501125321;
    int yPrimed2 = yPrimed1 + 1136930381;
    FixedInt fixedInt4 = (fixedInt2 + fixedInt3) * fixedInt1;
    FixedInt xd1 = fixedInt2 - fixedInt4;
    FixedInt yd1 = fixedInt3 - fixedInt4;
    FixedInt fixedInt5 = (FixedInt) 2097152L / (FixedInt) 3145728L - xd1 * xd1 - yd1 * yd1;
    FixedInt fixedInt6 = fixedInt5 * fixedInt5 * (fixedInt5 * fixedInt5) * MyPerlin.GradCoord(seed, xPrimed1, yPrimed1, xd1, yd1);
    FixedInt fixedInt7 = (FixedInt) 2 * ((FixedInt) 1 - (FixedInt) 2 * fixedInt1) * ((FixedInt) 1 / fixedInt1 - 2) * fixedInt4 + ((FixedInt) -2 * ((FixedInt) 1 - (FixedInt) 2 * fixedInt1) * ((FixedInt) 1 - (FixedInt) 2 * fixedInt1) + fixedInt5);
    FixedInt xd2 = xd1 - ((FixedInt) 1 - (FixedInt) 2 * fixedInt1);
    FixedInt yd2 = yd1 - ((FixedInt) 1 - (FixedInt) 2 * fixedInt1);
    FixedInt fixedInt8 = fixedInt6 + fixedInt7 * fixedInt7 * (fixedInt7 * fixedInt7) * MyPerlin.GradCoord(seed, xPrimed2, yPrimed2, xd2, yd2);
    FixedInt fixedInt9 = fixedInt2 - fixedInt3;
    if (fixedInt4 > fixedInt1)
    {
      if (fixedInt2 + fixedInt9 > 1)
      {
        FixedInt xd3 = xd1 + ((FixedInt) 3 * fixedInt1 - 2);
        FixedInt yd3 = yd1 + ((FixedInt) 3 * fixedInt1 - 1);
        FixedInt fixedInt10 = (FixedInt) 2097152L / (FixedInt) 3145728L - xd3 * xd3 - yd3 * yd3;
        if (fixedInt10 > 0)
          fixedInt8 += fixedInt10 * fixedInt10 * (fixedInt10 * fixedInt10) * MyPerlin.GradCoord(seed, xPrimed1 + 1002250642, yPrimed1 + 1136930381, xd3, yd3);
      }
      else
      {
        FixedInt xd4 = xd1 + fixedInt1;
        FixedInt yd4 = yd1 + (fixedInt1 - 1);
        FixedInt fixedInt11 = (FixedInt) 2097152L / (FixedInt) 3145728L - xd4 * xd4 - yd4 * yd4;
        if (fixedInt11 > 0)
          fixedInt8 += fixedInt11 * fixedInt11 * (fixedInt11 * fixedInt11) * MyPerlin.GradCoord(seed, xPrimed1, yPrimed1 + 1136930381, xd4, yd4);
      }
      if (fixedInt3 - fixedInt9 > 1)
      {
        FixedInt xd5 = xd1 + ((FixedInt) 3 * fixedInt1 - 1);
        FixedInt yd5 = yd1 + ((FixedInt) 3 * fixedInt1 - 2);
        FixedInt fixedInt12 = (FixedInt) 2097152L / (FixedInt) 3145728L - xd5 * xd5 - yd5 * yd5;
        if (fixedInt12 > 0)
          fixedInt8 += fixedInt12 * fixedInt12 * (fixedInt12 * fixedInt12) * MyPerlin.GradCoord(seed, xPrimed1 + 501125321, yPrimed1 - 2021106534, xd5, yd5);
      }
      else
      {
        FixedInt xd6 = xd1 + (fixedInt1 - 1);
        FixedInt yd6 = yd1 + fixedInt1;
        FixedInt fixedInt13 = (FixedInt) 2097152L / (FixedInt) 3145728L - xd6 * xd6 - yd6 * yd6;
        if (fixedInt13 > 0)
          fixedInt8 += fixedInt13 * fixedInt13 * (fixedInt13 * fixedInt13) * MyPerlin.GradCoord(seed, xPrimed1 + 501125321, yPrimed1, xd6, yd6);
      }
    }
    else
    {
      if (fixedInt2 + fixedInt9 < 0)
      {
        FixedInt xd7 = xd1 + ((FixedInt) 1 - fixedInt1);
        FixedInt yd7 = yd1 - fixedInt1;
        FixedInt fixedInt14 = (FixedInt) 2097152L / (FixedInt) 3145728L - xd7 * xd7 - yd7 * yd7;
        if (fixedInt14 > 0)
          fixedInt8 += fixedInt14 * fixedInt14 * (fixedInt14 * fixedInt14) * MyPerlin.GradCoord(seed, xPrimed1 - 501125321, yPrimed1, xd7, yd7);
      }
      else
      {
        FixedInt xd8 = xd1 + (fixedInt1 - 1);
        FixedInt yd8 = yd1 + fixedInt1;
        FixedInt fixedInt15 = (FixedInt) 2097152L / (FixedInt) 3145728L - xd8 * xd8 - yd8 * yd8;
        if (fixedInt15 > 0)
          fixedInt8 += fixedInt15 * fixedInt15 * (fixedInt15 * fixedInt15) * MyPerlin.GradCoord(seed, xPrimed1 + 501125321, yPrimed1, xd8, yd8);
      }
      if (fixedInt3 < fixedInt9)
      {
        FixedInt xd9 = xd1 - fixedInt1;
        FixedInt yd9 = yd1 - (fixedInt1 - 1);
        FixedInt fixedInt16 = (FixedInt) 2097152L / (FixedInt) 3145728L - xd9 * xd9 - yd9 * yd9;
        if (fixedInt16 > 0)
          fixedInt8 += fixedInt16 * fixedInt16 * (fixedInt16 * fixedInt16) * MyPerlin.GradCoord(seed, xPrimed1, yPrimed1 - 1136930381, xd9, yd9);
      }
      else
      {
        FixedInt xd10 = xd1 + fixedInt1;
        FixedInt yd10 = yd1 + (fixedInt1 - 1);
        FixedInt fixedInt17 = (FixedInt) 2097152L / (FixedInt) 3145728L - xd10 * xd10 - yd10 * yd10;
        if (fixedInt17 > 0)
          fixedInt8 += fixedInt17 * fixedInt17 * (fixedInt17 * fixedInt17) * MyPerlin.GradCoord(seed, xPrimed1, yPrimed1 + 1136930381, xd10, yd10);
      }
    }
    return fixedInt8 * (FixedInt) 19128081L;
  }

  private FixedInt SingleOpenSimplex2S(int seed, FixedInt x, FixedInt y, FixedInt z)
  {
    int num1 = MyPerlin.FastFloor(x);
    int num2 = MyPerlin.FastFloor(y);
    int num3 = MyPerlin.FastFloor(z);
    FixedInt fixedInt1 = x - num1;
    FixedInt fixedInt2 = y - num2;
    FixedInt fixedInt3 = z - num3;
    int num4 = num1 * 501125321;
    int num5 = num2 * 1136930381;
    int num6 = num3 * 1720413743;
    int seed1 = seed + 1293373;
    int num7 = (int) (-(FixedInt) 524288L - fixedInt1);
    int num8 = (int) (-(FixedInt) 524288L - fixedInt2);
    int num9 = (int) (-(FixedInt) 524288L - fixedInt3);
    FixedInt xd1 = fixedInt1 + num7;
    FixedInt yd1 = fixedInt2 + num8;
    FixedInt zd1 = fixedInt3 + num9;
    FixedInt fixedInt4 = (FixedInt) 786432L - xd1 * xd1 - yd1 * yd1 - zd1 * zd1;
    FixedInt fixedInt5 = fixedInt4 * fixedInt4 * (fixedInt4 * fixedInt4) * MyPerlin.GradCoord(seed, num4 + (num7 & 501125321), num5 + (num8 & 1136930381), num6 + (num9 & 1720413743), xd1, yd1, zd1);
    FixedInt xd2 = fixedInt1 - (FixedInt) 524288L;
    FixedInt yd2 = fixedInt2 - (FixedInt) 524288L;
    FixedInt zd2 = fixedInt3 - (FixedInt) 524288L;
    FixedInt fixedInt6 = (FixedInt) 786432L - xd2 * xd2 - yd2 * yd2 - zd2 * zd2;
    FixedInt fixedInt7 = fixedInt5 + fixedInt6 * fixedInt6 * (fixedInt6 * fixedInt6) * MyPerlin.GradCoord(seed1, num4 + 501125321, num5 + 1136930381, num6 + 1720413743, xd2, yd2, zd2);
    FixedInt fixedInt8 = (FixedInt) ((num7 | 1) << 1) * xd2;
    FixedInt fixedInt9 = (FixedInt) ((num8 | 1) << 1) * yd2;
    FixedInt fixedInt10 = (FixedInt) ((num9 | 1) << 1) * zd2;
    FixedInt fixedInt11 = (FixedInt) (-2 - (num7 << 2)) * xd2 - (FixedInt) 1;
    FixedInt fixedInt12 = (FixedInt) (-2 - (num8 << 2)) * yd2 - (FixedInt) 1;
    FixedInt fixedInt13 = (FixedInt) (-2 - (num9 << 2)) * zd2 - (FixedInt) 1;
    bool flag1 = false;
    FixedInt fixedInt14 = fixedInt8 + fixedInt4;
    if (fixedInt14 > 0)
    {
      FixedInt xd3 = xd1 - (num7 | 1);
      FixedInt yd3 = yd1;
      FixedInt zd3 = zd1;
      fixedInt7 += fixedInt14 * fixedInt14 * (fixedInt14 * fixedInt14) * MyPerlin.GradCoord(seed, num4 + (~num7 & 501125321), num5 + (num8 & 1136930381), num6 + (num9 & 1720413743), xd3, yd3, zd3);
    }
    else
    {
      FixedInt fixedInt15 = fixedInt9 + fixedInt10 + fixedInt4;
      if (fixedInt15 > 0)
      {
        FixedInt xd4 = xd1;
        FixedInt yd4 = yd1 - (num8 | 1);
        FixedInt zd4 = zd1 - (num9 | 1);
        fixedInt7 += fixedInt15 * fixedInt15 * (fixedInt15 * fixedInt15) * MyPerlin.GradCoord(seed, num4 + (num7 & 501125321), num5 + (~num8 & 1136930381), num6 + (~num9 & 1720413743), xd4, yd4, zd4);
      }
      FixedInt fixedInt16 = fixedInt11 + fixedInt6;
      if (fixedInt16 > 0)
      {
        FixedInt xd5 = (FixedInt) (num7 | 1) + xd2;
        FixedInt yd5 = yd2;
        FixedInt zd5 = zd2;
        fixedInt7 += fixedInt16 * fixedInt16 * (fixedInt16 * fixedInt16) * MyPerlin.GradCoord(seed1, num4 + (num7 & 1002250642), num5 + 1136930381, num6 + 1720413743, xd5, yd5, zd5);
        flag1 = true;
      }
    }
    bool flag2 = false;
    FixedInt fixedInt17 = fixedInt9 + fixedInt4;
    if (fixedInt17 > 0)
    {
      FixedInt xd6 = xd1;
      FixedInt yd6 = yd1 - (num8 | 1);
      FixedInt zd6 = zd1;
      fixedInt7 += fixedInt17 * fixedInt17 * (fixedInt17 * fixedInt17) * MyPerlin.GradCoord(seed, num4 + (num7 & 501125321), num5 + (~num8 & 1136930381), num6 + (num9 & 1720413743), xd6, yd6, zd6);
    }
    else
    {
      FixedInt fixedInt18 = fixedInt8 + fixedInt10 + fixedInt4;
      if (fixedInt18 > 0)
      {
        FixedInt xd7 = xd1 - (num7 | 1);
        FixedInt yd7 = yd1;
        FixedInt zd7 = zd1 - (num9 | 1);
        fixedInt7 += fixedInt18 * fixedInt18 * (fixedInt18 * fixedInt18) * MyPerlin.GradCoord(seed, num4 + (~num7 & 501125321), num5 + (num8 & 1136930381), num6 + (~num9 & 1720413743), xd7, yd7, zd7);
      }
      FixedInt fixedInt19 = fixedInt12 + fixedInt6;
      if (fixedInt19 > 0)
      {
        FixedInt xd8 = xd2;
        FixedInt yd8 = (FixedInt) (num8 | 1) + yd2;
        FixedInt zd8 = zd2;
        fixedInt7 += fixedInt19 * fixedInt19 * (fixedInt19 * fixedInt19) * MyPerlin.GradCoord(seed1, num4 + 501125321, num5 + (num8 & -2021106534), num6 + 1720413743, xd8, yd8, zd8);
        flag2 = true;
      }
    }
    bool flag3 = false;
    FixedInt fixedInt20 = fixedInt10 + fixedInt4;
    if (fixedInt20 > 0)
    {
      FixedInt xd9 = xd1;
      FixedInt yd9 = yd1;
      FixedInt zd9 = zd1 - (num9 | 1);
      fixedInt7 += fixedInt20 * fixedInt20 * (fixedInt20 * fixedInt20) * MyPerlin.GradCoord(seed, num4 + (num7 & 501125321), num5 + (num8 & 1136930381), num6 + (~num9 & 1720413743), xd9, yd9, zd9);
    }
    else
    {
      FixedInt fixedInt21 = fixedInt8 + fixedInt9 + fixedInt4;
      if (fixedInt21 > 0)
      {
        FixedInt xd10 = xd1 - (num7 | 1);
        FixedInt yd10 = yd1 - (num8 | 1);
        FixedInt zd10 = zd1;
        fixedInt7 += fixedInt21 * fixedInt21 * (fixedInt21 * fixedInt21) * MyPerlin.GradCoord(seed, num4 + (~num7 & 501125321), num5 + (~num8 & 1136930381), num6 + (num9 & 1720413743), xd10, yd10, zd10);
      }
      FixedInt fixedInt22 = fixedInt13 + fixedInt6;
      if (fixedInt22 > 0)
      {
        FixedInt xd11 = xd2;
        FixedInt yd11 = yd2;
        FixedInt zd11 = (FixedInt) (num9 | 1) + zd2;
        fixedInt7 += fixedInt22 * fixedInt22 * (fixedInt22 * fixedInt22) * MyPerlin.GradCoord(seed1, num4 + 501125321, num5 + 1136930381, num6 + (num9 & -854139810), xd11, yd11, zd11);
        flag3 = true;
      }
    }
    if (!flag1)
    {
      FixedInt fixedInt23 = fixedInt12 + fixedInt13 + fixedInt6;
      if (fixedInt23 > 0)
      {
        FixedInt xd12 = xd2;
        FixedInt yd12 = (FixedInt) (num8 | 1) + yd2;
        FixedInt zd12 = (FixedInt) (num9 | 1) + zd2;
        fixedInt7 += fixedInt23 * fixedInt23 * (fixedInt23 * fixedInt23) * MyPerlin.GradCoord(seed1, num4 + 501125321, num5 + (num8 & -2021106534), num6 + (num9 & -854139810), xd12, yd12, zd12);
      }
    }
    if (!flag2)
    {
      FixedInt fixedInt24 = fixedInt11 + fixedInt13 + fixedInt6;
      if (fixedInt24 > 0)
      {
        FixedInt xd13 = (FixedInt) (num7 | 1) + xd2;
        FixedInt yd13 = yd2;
        FixedInt zd13 = (FixedInt) (num9 | 1) + zd2;
        fixedInt7 += fixedInt24 * fixedInt24 * (fixedInt24 * fixedInt24) * MyPerlin.GradCoord(seed1, num4 + (num7 & 1002250642), num5 + 1136930381, num6 + (num9 & -854139810), xd13, yd13, zd13);
      }
    }
    if (!flag3)
    {
      FixedInt fixedInt25 = fixedInt11 + fixedInt12 + fixedInt6;
      if (fixedInt25 > 0)
      {
        FixedInt xd14 = (FixedInt) (num7 | 1) + xd2;
        FixedInt yd14 = (FixedInt) (num8 | 1) + yd2;
        FixedInt zd14 = zd2;
        fixedInt7 += fixedInt25 * fixedInt25 * (fixedInt25 * fixedInt25) * MyPerlin.GradCoord(seed1, num4 + (num7 & 1002250642), num5 + (num8 & -2021106534), num6 + 1720413743, xd14, yd14, zd14);
      }
    }
    return fixedInt7 * (FixedInt) 9485445L;
  }

  private FixedInt SingleCellular(int seed, FixedInt x, FixedInt y)
  {
    int num1 = MyPerlin.FastRound(x);
    int num2 = MyPerlin.FastRound(y);
    FixedInt fixedInt1 = FixedInt.MaxValue;
    FixedInt fixedInt2 = FixedInt.MaxValue;
    int num3 = 0;
    FixedInt fixedInt3 = (FixedInt) 458244L * this.mCellularJitterModifier;
    int xPrimed = (num1 - 1) * 501125321;
    int num4 = (num2 - 1) * 1136930381;
    switch (this.mCellularDistanceFunction)
    {
      case MyPerlin.CellularDistanceFunction.Manhattan:
        for (int index1 = num1 - 1; index1 <= num1 + 1; ++index1)
        {
          int yPrimed = num4;
          for (int index2 = num2 - 1; index2 <= num2 + 1; ++index2)
          {
            int num5 = MyPerlin.Hash(seed, xPrimed, yPrimed);
            int index3 = num5 & 510;
            FixedInt f1 = (FixedInt) index1 - x + MyPerlin.RandVecs2D[index3] * fixedInt3;
            FixedInt f2 = (FixedInt) index2 - y + MyPerlin.RandVecs2D[index3 | 1] * fixedInt3;
            FixedInt b = MyPerlin.FastAbs(f1) + MyPerlin.FastAbs(f2);
            fixedInt2 = MyPerlin.FastMax(MyPerlin.FastMin(fixedInt2, b), fixedInt1);
            if (b < fixedInt1)
            {
              fixedInt1 = b;
              num3 = num5;
            }
            yPrimed += 1136930381;
          }
          xPrimed += 501125321;
        }
        break;
      case MyPerlin.CellularDistanceFunction.Hybrid:
        for (int index4 = num1 - 1; index4 <= num1 + 1; ++index4)
        {
          int yPrimed = num4;
          for (int index5 = num2 - 1; index5 <= num2 + 1; ++index5)
          {
            int num6 = MyPerlin.Hash(seed, xPrimed, yPrimed);
            int index6 = num6 & 510;
            FixedInt f3 = (FixedInt) index4 - x + MyPerlin.RandVecs2D[index6] * fixedInt3;
            FixedInt f4 = (FixedInt) index5 - y + MyPerlin.RandVecs2D[index6 | 1] * fixedInt3;
            FixedInt b = MyPerlin.FastAbs(f3) + MyPerlin.FastAbs(f4) + (f3 * f3 + f4 * f4);
            fixedInt2 = MyPerlin.FastMax(MyPerlin.FastMin(fixedInt2, b), fixedInt1);
            if (b < fixedInt1)
            {
              fixedInt1 = b;
              num3 = num6;
            }
            yPrimed += 1136930381;
          }
          xPrimed += 501125321;
        }
        break;
      default:
        for (int index7 = num1 - 1; index7 <= num1 + 1; ++index7)
        {
          int yPrimed = num4;
          for (int index8 = num2 - 1; index8 <= num2 + 1; ++index8)
          {
            int num7 = MyPerlin.Hash(seed, xPrimed, yPrimed);
            int index9 = num7 & 510;
            FixedInt fixedInt4 = (FixedInt) index7 - x + MyPerlin.RandVecs2D[index9] * fixedInt3;
            FixedInt fixedInt5 = (FixedInt) index8 - y + MyPerlin.RandVecs2D[index9 | 1] * fixedInt3;
            FixedInt b = fixedInt4 * fixedInt4 + fixedInt5 * fixedInt5;
            fixedInt2 = MyPerlin.FastMax(MyPerlin.FastMin(fixedInt2, b), fixedInt1);
            if (b < fixedInt1)
            {
              fixedInt1 = b;
              num3 = num7;
            }
            yPrimed += 1136930381;
          }
          xPrimed += 501125321;
        }
        break;
    }
    if (this.mCellularDistanceFunction == MyPerlin.CellularDistanceFunction.Euclidean && this.mCellularReturnType >= MyPerlin.CellularReturnType.Distance)
    {
      fixedInt1 = MyPerlin.FastSqrt(fixedInt1);
      if (this.mCellularReturnType >= MyPerlin.CellularReturnType.Distance2)
        fixedInt2 = MyPerlin.FastSqrt(fixedInt2);
    }
    switch (this.mCellularReturnType)
    {
      case MyPerlin.CellularReturnType.CellValue:
        return (FixedInt) num3 * ((FixedInt) 1 / FixedInt.MaxValue);
      case MyPerlin.CellularReturnType.Distance:
        return fixedInt1 - 1;
      case MyPerlin.CellularReturnType.Distance2:
        return fixedInt2 - 1;
      case MyPerlin.CellularReturnType.Distance2Add:
        return (fixedInt2 + fixedInt1) * (FixedInt) 524288L - 1;
      case MyPerlin.CellularReturnType.Distance2Sub:
        return fixedInt2 - fixedInt1 - 1;
      case MyPerlin.CellularReturnType.Distance2Mul:
        return fixedInt2 * fixedInt1 * (FixedInt) 524288L - 1;
      case MyPerlin.CellularReturnType.Distance2Div:
        return fixedInt1 / fixedInt2 - 1;
      default:
        return (FixedInt) 0;
    }
  }

  private FixedInt SingleCellular(int seed, FixedInt x, FixedInt y, FixedInt z)
  {
    int num1 = MyPerlin.FastRound(x);
    int num2 = MyPerlin.FastRound(y);
    int num3 = MyPerlin.FastRound(z);
    FixedInt fixedInt1 = FixedInt.MaxValue;
    FixedInt fixedInt2 = FixedInt.MaxValue;
    int num4 = 0;
    FixedInt fixedInt3 = (FixedInt) 415386L * this.mCellularJitterModifier;
    int xPrimed = (num1 - 1) * 501125321;
    int num5 = (num2 - 1) * 1136930381;
    int num6 = (num3 - 1) * 1720413743;
    switch (this.mCellularDistanceFunction)
    {
      case MyPerlin.CellularDistanceFunction.Euclidean:
      case MyPerlin.CellularDistanceFunction.EuclideanSq:
        for (int index1 = num1 - 1; index1 <= num1 + 1; ++index1)
        {
          int yPrimed = num5;
          for (int index2 = num2 - 1; index2 <= num2 + 1; ++index2)
          {
            int zPrimed = num6;
            for (int index3 = num3 - 1; index3 <= num3 + 1; ++index3)
            {
              int num7 = MyPerlin.Hash(seed, xPrimed, yPrimed, zPrimed);
              int index4 = num7 & 1020;
              FixedInt fixedInt4 = (FixedInt) index1 - x + MyPerlin.RandVecs3D[index4] * fixedInt3;
              FixedInt fixedInt5 = (FixedInt) index2 - y + MyPerlin.RandVecs3D[index4 | 1] * fixedInt3;
              FixedInt fixedInt6 = (FixedInt) index3 - z + MyPerlin.RandVecs3D[index4 | 2] * fixedInt3;
              FixedInt b = fixedInt4 * fixedInt4 + fixedInt5 * fixedInt5 + fixedInt6 * fixedInt6;
              fixedInt2 = MyPerlin.FastMax(MyPerlin.FastMin(fixedInt2, b), fixedInt1);
              if (b < fixedInt1)
              {
                fixedInt1 = b;
                num4 = num7;
              }
              zPrimed += 1720413743;
            }
            yPrimed += 1136930381;
          }
          xPrimed += 501125321;
        }
        break;
      case MyPerlin.CellularDistanceFunction.Manhattan:
        for (int index5 = num1 - 1; index5 <= num1 + 1; ++index5)
        {
          int yPrimed = num5;
          for (int index6 = num2 - 1; index6 <= num2 + 1; ++index6)
          {
            int zPrimed = num6;
            for (int index7 = num3 - 1; index7 <= num3 + 1; ++index7)
            {
              int num8 = MyPerlin.Hash(seed, xPrimed, yPrimed, zPrimed);
              int index8 = num8 & 1020;
              FixedInt f1 = (FixedInt) index5 - x + MyPerlin.RandVecs3D[index8] * fixedInt3;
              FixedInt f2 = (FixedInt) index6 - y + MyPerlin.RandVecs3D[index8 | 1] * fixedInt3;
              FixedInt f3 = (FixedInt) index7 - z + MyPerlin.RandVecs3D[index8 | 2] * fixedInt3;
              FixedInt b = MyPerlin.FastAbs(f1) + MyPerlin.FastAbs(f2) + MyPerlin.FastAbs(f3);
              fixedInt2 = MyPerlin.FastMax(MyPerlin.FastMin(fixedInt2, b), fixedInt1);
              if (b < fixedInt1)
              {
                fixedInt1 = b;
                num4 = num8;
              }
              zPrimed += 1720413743;
            }
            yPrimed += 1136930381;
          }
          xPrimed += 501125321;
        }
        break;
      case MyPerlin.CellularDistanceFunction.Hybrid:
        for (int index9 = num1 - 1; index9 <= num1 + 1; ++index9)
        {
          int yPrimed = num5;
          for (int index10 = num2 - 1; index10 <= num2 + 1; ++index10)
          {
            int zPrimed = num6;
            for (int index11 = num3 - 1; index11 <= num3 + 1; ++index11)
            {
              int num9 = MyPerlin.Hash(seed, xPrimed, yPrimed, zPrimed);
              int index12 = num9 & 1020;
              FixedInt f4 = (FixedInt) index9 - x + MyPerlin.RandVecs3D[index12] * fixedInt3;
              FixedInt f5 = (FixedInt) index10 - y + MyPerlin.RandVecs3D[index12 | 1] * fixedInt3;
              FixedInt f6 = (FixedInt) index11 - z + MyPerlin.RandVecs3D[index12 | 2] * fixedInt3;
              FixedInt b = MyPerlin.FastAbs(f4) + MyPerlin.FastAbs(f5) + MyPerlin.FastAbs(f6) + (f4 * f4 + f5 * f5 + f6 * f6);
              fixedInt2 = MyPerlin.FastMax(MyPerlin.FastMin(fixedInt2, b), fixedInt1);
              if (b < fixedInt1)
              {
                fixedInt1 = b;
                num4 = num9;
              }
              zPrimed += 1720413743;
            }
            yPrimed += 1136930381;
          }
          xPrimed += 501125321;
        }
        break;
    }
    if (this.mCellularDistanceFunction == MyPerlin.CellularDistanceFunction.Euclidean && this.mCellularReturnType >= MyPerlin.CellularReturnType.Distance)
    {
      fixedInt1 = MyPerlin.FastSqrt(fixedInt1);
      if (this.mCellularReturnType >= MyPerlin.CellularReturnType.Distance2)
        fixedInt2 = MyPerlin.FastSqrt(fixedInt2);
    }
    switch (this.mCellularReturnType)
    {
      case MyPerlin.CellularReturnType.CellValue:
        return (FixedInt) num4 * ((FixedInt) 1 / FixedInt.MaxValue);
      case MyPerlin.CellularReturnType.Distance:
        return fixedInt1 - 1;
      case MyPerlin.CellularReturnType.Distance2:
        return fixedInt2 - 1;
      case MyPerlin.CellularReturnType.Distance2Add:
        return (fixedInt2 + fixedInt1) * (FixedInt) 524288L - 1;
      case MyPerlin.CellularReturnType.Distance2Sub:
        return fixedInt2 - fixedInt1 - 1;
      case MyPerlin.CellularReturnType.Distance2Mul:
        return fixedInt2 * fixedInt1 * (FixedInt) 524288L - 1;
      case MyPerlin.CellularReturnType.Distance2Div:
        return fixedInt1 / fixedInt2 - 1;
      default:
        return (FixedInt) 0;
    }
  }

  private FixedInt SinglePerlin(int seed, FixedInt x, FixedInt y)
  {
    int num1 = MyPerlin.FastFloor(x);
    int num2 = MyPerlin.FastFloor(y);
    FixedInt fixedInt1 = x - num1;
    FixedInt fixedInt2 = y - num2;
    FixedInt xd = fixedInt1 - 1;
    FixedInt yd = fixedInt2 - 1;
    FixedInt t1 = MyPerlin.InterpQuintic(fixedInt1);
    FixedInt t2 = MyPerlin.InterpQuintic(fixedInt2);
    int xPrimed1 = num1 * 501125321;
    int yPrimed1 = num2 * 1136930381;
    int xPrimed2 = xPrimed1 + 501125321;
    int yPrimed2 = yPrimed1 + 1136930381;
    return MyPerlin.Lerp(MyPerlin.Lerp(MyPerlin.GradCoord(seed, xPrimed1, yPrimed1, fixedInt1, fixedInt2), MyPerlin.GradCoord(seed, xPrimed2, yPrimed1, xd, fixedInt2), t1), MyPerlin.Lerp(MyPerlin.GradCoord(seed, xPrimed1, yPrimed2, fixedInt1, yd), MyPerlin.GradCoord(seed, xPrimed2, yPrimed2, xd, yd), t1), t2) * (FixedInt) 1493978L;
  }

  private FixedInt SinglePerlin(int seed, FixedInt x, FixedInt y, FixedInt z)
  {
    int num1 = MyPerlin.FastFloor(x);
    int num2 = MyPerlin.FastFloor(y);
    int num3 = MyPerlin.FastFloor(z);
    FixedInt fixedInt1 = x - num1;
    FixedInt fixedInt2 = y - num2;
    FixedInt fixedInt3 = z - num3;
    FixedInt xd = fixedInt1 - 1;
    FixedInt yd = fixedInt2 - 1;
    FixedInt zd = fixedInt3 - 1;
    FixedInt t1 = MyPerlin.InterpQuintic(fixedInt1);
    FixedInt t2 = MyPerlin.InterpQuintic(fixedInt2);
    FixedInt t3 = MyPerlin.InterpQuintic(fixedInt3);
    int xPrimed1 = num1 * 501125321;
    int yPrimed1 = num2 * 1136930381;
    int zPrimed1 = num3 * 1720413743;
    int xPrimed2 = xPrimed1 + 501125321;
    int yPrimed2 = yPrimed1 + 1136930381;
    int zPrimed2 = zPrimed1 + 1720413743;
    FixedInt a1 = MyPerlin.Lerp(MyPerlin.GradCoord(seed, xPrimed1, yPrimed1, zPrimed1, fixedInt1, fixedInt2, fixedInt3), MyPerlin.GradCoord(seed, xPrimed2, yPrimed1, zPrimed1, xd, fixedInt2, fixedInt3), t1);
    FixedInt fixedInt4 = MyPerlin.Lerp(MyPerlin.GradCoord(seed, xPrimed1, yPrimed2, zPrimed1, fixedInt1, yd, fixedInt3), MyPerlin.GradCoord(seed, xPrimed2, yPrimed2, zPrimed1, xd, yd, fixedInt3), t1);
    FixedInt a2 = MyPerlin.Lerp(MyPerlin.GradCoord(seed, xPrimed1, yPrimed1, zPrimed2, fixedInt1, fixedInt2, zd), MyPerlin.GradCoord(seed, xPrimed2, yPrimed1, zPrimed2, xd, fixedInt2, zd), t1);
    FixedInt b1 = MyPerlin.Lerp(MyPerlin.GradCoord(seed, xPrimed1, yPrimed2, zPrimed2, fixedInt1, yd, zd), MyPerlin.GradCoord(seed, xPrimed2, yPrimed2, zPrimed2, xd, yd, zd), t1);
    FixedInt b2 = fixedInt4;
    FixedInt t4 = t2;
    return MyPerlin.Lerp(MyPerlin.Lerp(a1, b2, t4), MyPerlin.Lerp(a2, b1, t2), t3) * (FixedInt) 1011793L;
  }

  private FixedInt SingleValueCubic(int seed, FixedInt x, FixedInt y)
  {
    int num1 = MyPerlin.FastFloor(x);
    int num2 = MyPerlin.FastFloor(y);
    FixedInt t1 = x - num1;
    FixedInt t2 = y - num2;
    int xPrimed1 = num1 * 501125321;
    int yPrimed1 = num2 * 1136930381;
    int xPrimed2 = xPrimed1 - 501125321;
    int yPrimed2 = yPrimed1 - 1136930381;
    int xPrimed3 = xPrimed1 + 501125321;
    int yPrimed3 = yPrimed1 + 1136930381;
    int xPrimed4 = xPrimed1 + 1002250642;
    int yPrimed4 = yPrimed1 - 2021106534;
    return MyPerlin.CubicLerp(MyPerlin.CubicLerp(MyPerlin.ValCoord(seed, xPrimed2, yPrimed2), MyPerlin.ValCoord(seed, xPrimed1, yPrimed2), MyPerlin.ValCoord(seed, xPrimed3, yPrimed2), MyPerlin.ValCoord(seed, xPrimed4, yPrimed2), t1), MyPerlin.CubicLerp(MyPerlin.ValCoord(seed, xPrimed2, yPrimed1), MyPerlin.ValCoord(seed, xPrimed1, yPrimed1), MyPerlin.ValCoord(seed, xPrimed3, yPrimed1), MyPerlin.ValCoord(seed, xPrimed4, yPrimed1), t1), MyPerlin.CubicLerp(MyPerlin.ValCoord(seed, xPrimed2, yPrimed3), MyPerlin.ValCoord(seed, xPrimed1, yPrimed3), MyPerlin.ValCoord(seed, xPrimed3, yPrimed3), MyPerlin.ValCoord(seed, xPrimed4, yPrimed3), t1), MyPerlin.CubicLerp(MyPerlin.ValCoord(seed, xPrimed2, yPrimed4), MyPerlin.ValCoord(seed, xPrimed1, yPrimed4), MyPerlin.ValCoord(seed, xPrimed3, yPrimed4), MyPerlin.ValCoord(seed, xPrimed4, yPrimed4), t1), t2) * ((FixedInt) 1 / ((FixedInt) 1572864L * (FixedInt) 1572864L));
  }

  private FixedInt SingleValueCubic(int seed, FixedInt x, FixedInt y, FixedInt z)
  {
    int num1 = MyPerlin.FastFloor(x);
    int num2 = MyPerlin.FastFloor(y);
    int num3 = MyPerlin.FastFloor(z);
    FixedInt t1 = x - num1;
    FixedInt t2 = y - num2;
    FixedInt t3 = z - num3;
    int xPrimed1 = num1 * 501125321;
    int yPrimed1 = num2 * 1136930381;
    int zPrimed1 = num3 * 1720413743;
    int xPrimed2 = xPrimed1 - 501125321;
    int yPrimed2 = yPrimed1 - 1136930381;
    int zPrimed2 = zPrimed1 - 1720413743;
    int xPrimed3 = xPrimed1 + 501125321;
    int yPrimed3 = yPrimed1 + 1136930381;
    int zPrimed3 = zPrimed1 + 1720413743;
    int xPrimed4 = xPrimed1 + 1002250642;
    int yPrimed4 = yPrimed1 - 2021106534;
    int zPrimed4 = zPrimed1 - 854139810;
    return MyPerlin.CubicLerp(MyPerlin.CubicLerp(MyPerlin.CubicLerp(MyPerlin.ValCoord(seed, xPrimed2, yPrimed2, zPrimed2), MyPerlin.ValCoord(seed, xPrimed1, yPrimed2, zPrimed2), MyPerlin.ValCoord(seed, xPrimed3, yPrimed2, zPrimed2), MyPerlin.ValCoord(seed, xPrimed4, yPrimed2, zPrimed2), t1), MyPerlin.CubicLerp(MyPerlin.ValCoord(seed, xPrimed2, yPrimed1, zPrimed2), MyPerlin.ValCoord(seed, xPrimed1, yPrimed1, zPrimed2), MyPerlin.ValCoord(seed, xPrimed3, yPrimed1, zPrimed2), MyPerlin.ValCoord(seed, xPrimed4, yPrimed1, zPrimed2), t1), MyPerlin.CubicLerp(MyPerlin.ValCoord(seed, xPrimed2, yPrimed3, zPrimed2), MyPerlin.ValCoord(seed, xPrimed1, yPrimed3, zPrimed2), MyPerlin.ValCoord(seed, xPrimed3, yPrimed3, zPrimed2), MyPerlin.ValCoord(seed, xPrimed4, yPrimed3, zPrimed2), t1), MyPerlin.CubicLerp(MyPerlin.ValCoord(seed, xPrimed2, yPrimed4, zPrimed2), MyPerlin.ValCoord(seed, xPrimed1, yPrimed4, zPrimed2), MyPerlin.ValCoord(seed, xPrimed3, yPrimed4, zPrimed2), MyPerlin.ValCoord(seed, xPrimed4, yPrimed4, zPrimed2), t1), t2), MyPerlin.CubicLerp(MyPerlin.CubicLerp(MyPerlin.ValCoord(seed, xPrimed2, yPrimed2, zPrimed1), MyPerlin.ValCoord(seed, xPrimed1, yPrimed2, zPrimed1), MyPerlin.ValCoord(seed, xPrimed3, yPrimed2, zPrimed1), MyPerlin.ValCoord(seed, xPrimed4, yPrimed2, zPrimed1), t1), MyPerlin.CubicLerp(MyPerlin.ValCoord(seed, xPrimed2, yPrimed1, zPrimed1), MyPerlin.ValCoord(seed, xPrimed1, yPrimed1, zPrimed1), MyPerlin.ValCoord(seed, xPrimed3, yPrimed1, zPrimed1), MyPerlin.ValCoord(seed, xPrimed4, yPrimed1, zPrimed1), t1), MyPerlin.CubicLerp(MyPerlin.ValCoord(seed, xPrimed2, yPrimed3, zPrimed1), MyPerlin.ValCoord(seed, xPrimed1, yPrimed3, zPrimed1), MyPerlin.ValCoord(seed, xPrimed3, yPrimed3, zPrimed1), MyPerlin.ValCoord(seed, xPrimed4, yPrimed3, zPrimed1), t1), MyPerlin.CubicLerp(MyPerlin.ValCoord(seed, xPrimed2, yPrimed4, zPrimed1), MyPerlin.ValCoord(seed, xPrimed1, yPrimed4, zPrimed1), MyPerlin.ValCoord(seed, xPrimed3, yPrimed4, zPrimed1), MyPerlin.ValCoord(seed, xPrimed4, yPrimed4, zPrimed1), t1), t2), MyPerlin.CubicLerp(MyPerlin.CubicLerp(MyPerlin.ValCoord(seed, xPrimed2, yPrimed2, zPrimed3), MyPerlin.ValCoord(seed, xPrimed1, yPrimed2, zPrimed3), MyPerlin.ValCoord(seed, xPrimed3, yPrimed2, zPrimed3), MyPerlin.ValCoord(seed, xPrimed4, yPrimed2, zPrimed3), t1), MyPerlin.CubicLerp(MyPerlin.ValCoord(seed, xPrimed2, yPrimed1, zPrimed3), MyPerlin.ValCoord(seed, xPrimed1, yPrimed1, zPrimed3), MyPerlin.ValCoord(seed, xPrimed3, yPrimed1, zPrimed3), MyPerlin.ValCoord(seed, xPrimed4, yPrimed1, zPrimed3), t1), MyPerlin.CubicLerp(MyPerlin.ValCoord(seed, xPrimed2, yPrimed3, zPrimed3), MyPerlin.ValCoord(seed, xPrimed1, yPrimed3, zPrimed3), MyPerlin.ValCoord(seed, xPrimed3, yPrimed3, zPrimed3), MyPerlin.ValCoord(seed, xPrimed4, yPrimed3, zPrimed3), t1), MyPerlin.CubicLerp(MyPerlin.ValCoord(seed, xPrimed2, yPrimed4, zPrimed3), MyPerlin.ValCoord(seed, xPrimed1, yPrimed4, zPrimed3), MyPerlin.ValCoord(seed, xPrimed3, yPrimed4, zPrimed3), MyPerlin.ValCoord(seed, xPrimed4, yPrimed4, zPrimed3), t1), t2), MyPerlin.CubicLerp(MyPerlin.CubicLerp(MyPerlin.ValCoord(seed, xPrimed2, yPrimed2, zPrimed4), MyPerlin.ValCoord(seed, xPrimed1, yPrimed2, zPrimed4), MyPerlin.ValCoord(seed, xPrimed3, yPrimed2, zPrimed4), MyPerlin.ValCoord(seed, xPrimed4, yPrimed2, zPrimed4), t1), MyPerlin.CubicLerp(MyPerlin.ValCoord(seed, xPrimed2, yPrimed1, zPrimed4), MyPerlin.ValCoord(seed, xPrimed1, yPrimed1, zPrimed4), MyPerlin.ValCoord(seed, xPrimed3, yPrimed1, zPrimed4), MyPerlin.ValCoord(seed, xPrimed4, yPrimed1, zPrimed4), t1), MyPerlin.CubicLerp(MyPerlin.ValCoord(seed, xPrimed2, yPrimed3, zPrimed4), MyPerlin.ValCoord(seed, xPrimed1, yPrimed3, zPrimed4), MyPerlin.ValCoord(seed, xPrimed3, yPrimed3, zPrimed4), MyPerlin.ValCoord(seed, xPrimed4, yPrimed3, zPrimed4), t1), MyPerlin.CubicLerp(MyPerlin.ValCoord(seed, xPrimed2, yPrimed4, zPrimed4), MyPerlin.ValCoord(seed, xPrimed1, yPrimed4, zPrimed4), MyPerlin.ValCoord(seed, xPrimed3, yPrimed4, zPrimed4), MyPerlin.ValCoord(seed, xPrimed4, yPrimed4, zPrimed4), t1), t2), t3) * ((FixedInt) 1 / ((FixedInt) 1572864L * (FixedInt) 1572864L * (FixedInt) 1572864L));
  }

  private FixedInt SingleValue(int seed, FixedInt x, FixedInt y)
  {
    int num1 = MyPerlin.FastFloor(x);
    int num2 = MyPerlin.FastFloor(y);
    FixedInt t1 = MyPerlin.InterpHermite(x - num1);
    FixedInt t2 = MyPerlin.InterpHermite(y - num2);
    int xPrimed1 = num1 * 501125321;
    int yPrimed1 = num2 * 1136930381;
    int xPrimed2 = xPrimed1 + 501125321;
    int yPrimed2 = yPrimed1 + 1136930381;
    return MyPerlin.Lerp(MyPerlin.Lerp(MyPerlin.ValCoord(seed, xPrimed1, yPrimed1), MyPerlin.ValCoord(seed, xPrimed2, yPrimed1), t1), MyPerlin.Lerp(MyPerlin.ValCoord(seed, xPrimed1, yPrimed2), MyPerlin.ValCoord(seed, xPrimed2, yPrimed2), t1), t2);
  }

  private FixedInt SingleValue(int seed, FixedInt x, FixedInt y, FixedInt z)
  {
    int num1 = MyPerlin.FastFloor(x);
    int num2 = MyPerlin.FastFloor(y);
    int num3 = MyPerlin.FastFloor(z);
    FixedInt t1 = MyPerlin.InterpHermite(x - num1);
    FixedInt t2 = MyPerlin.InterpHermite(y - num2);
    FixedInt t3 = MyPerlin.InterpHermite(z - num3);
    int xPrimed1 = num1 * 501125321;
    int yPrimed1 = num2 * 1136930381;
    int zPrimed1 = num3 * 1720413743;
    int xPrimed2 = xPrimed1 + 501125321;
    int yPrimed2 = yPrimed1 + 1136930381;
    int zPrimed2 = zPrimed1 + 1720413743;
    FixedInt a1 = MyPerlin.Lerp(MyPerlin.ValCoord(seed, xPrimed1, yPrimed1, zPrimed1), MyPerlin.ValCoord(seed, xPrimed2, yPrimed1, zPrimed1), t1);
    FixedInt fixedInt = MyPerlin.Lerp(MyPerlin.ValCoord(seed, xPrimed1, yPrimed2, zPrimed1), MyPerlin.ValCoord(seed, xPrimed2, yPrimed2, zPrimed1), t1);
    FixedInt a2 = MyPerlin.Lerp(MyPerlin.ValCoord(seed, xPrimed1, yPrimed1, zPrimed2), MyPerlin.ValCoord(seed, xPrimed2, yPrimed1, zPrimed2), t1);
    FixedInt b1 = MyPerlin.Lerp(MyPerlin.ValCoord(seed, xPrimed1, yPrimed2, zPrimed2), MyPerlin.ValCoord(seed, xPrimed2, yPrimed2, zPrimed2), t1);
    FixedInt b2 = fixedInt;
    FixedInt t4 = t2;
    return MyPerlin.Lerp(MyPerlin.Lerp(a1, b2, t4), MyPerlin.Lerp(a2, b1, t2), t3);
  }

  private void DoSingleDomainWarp(
    int seed,
    FixedInt amp,
    FixedInt freq,
    FixedInt x,
    FixedInt y,
    ref FixedInt xr,
    ref FixedInt yr)
  {
    switch (this.mDomainWarpType)
    {
      case MyPerlin.DomainWarpType.OpenSimplex2:
        this.SingleDomainWarpSimplexGradient(seed, amp * (FixedInt) 40143358L, freq, x, y, ref xr, ref yr, false);
        break;
      case MyPerlin.DomainWarpType.OpenSimplex2Reduced:
        this.SingleDomainWarpSimplexGradient(seed, amp * (FixedInt) 16777216L, freq, x, y, ref xr, ref yr, true);
        break;
      case MyPerlin.DomainWarpType.BasicGrid:
        this.SingleDomainWarpBasicGrid(seed, amp, freq, x, y, ref xr, ref yr);
        break;
    }
  }

  private void DoSingleDomainWarp(
    int seed,
    FixedInt amp,
    FixedInt freq,
    FixedInt x,
    FixedInt y,
    FixedInt z,
    ref FixedInt xr,
    ref FixedInt yr,
    ref FixedInt zr)
  {
    switch (this.mDomainWarpType)
    {
      case MyPerlin.DomainWarpType.OpenSimplex2:
        this.SingleDomainWarpOpenSimplex2Gradient(seed, amp * (FixedInt) 34282437L, freq, x, y, z, ref xr, ref yr, ref zr, false);
        break;
      case MyPerlin.DomainWarpType.OpenSimplex2Reduced:
        this.SingleDomainWarpOpenSimplex2Gradient(seed, amp * (FixedInt) 8090863L, freq, x, y, z, ref xr, ref yr, ref zr, true);
        break;
      case MyPerlin.DomainWarpType.BasicGrid:
        this.SingleDomainWarpBasicGrid(seed, amp, freq, x, y, z, ref xr, ref yr, ref zr);
        break;
    }
  }

  private void DomainWarpSingle(ref FixedInt x, ref FixedInt y)
  {
    int mSeed = this.mSeed;
    FixedInt amp = this.mDomainWarpAmp * this.mFractalBounding;
    FixedInt mFrequency = this.mFrequency;
    FixedInt x1 = x;
    FixedInt y1 = y;
    this.TransformDomainWarpCoordinate(ref x1, ref y1);
    this.DoSingleDomainWarp(mSeed, amp, mFrequency, x1, y1, ref x, ref y);
  }

  private void DomainWarpSingle(ref FixedInt x, ref FixedInt y, ref FixedInt z)
  {
    int mSeed = this.mSeed;
    FixedInt amp = this.mDomainWarpAmp * this.mFractalBounding;
    FixedInt mFrequency = this.mFrequency;
    FixedInt x1 = x;
    FixedInt y1 = y;
    FixedInt z1 = z;
    this.TransformDomainWarpCoordinate(ref x1, ref y1, ref z1);
    this.DoSingleDomainWarp(mSeed, amp, mFrequency, x1, y1, z1, ref x, ref y, ref z);
  }

  private void DomainWarpFractalProgressive(ref FixedInt x, ref FixedInt y)
  {
    int mSeed = this.mSeed;
    FixedInt amp = this.mDomainWarpAmp * this.mFractalBounding;
    FixedInt mFrequency = this.mFrequency;
    for (int index = 0; index < this.mOctaves; ++index)
    {
      FixedInt x1 = x;
      FixedInt y1 = y;
      this.TransformDomainWarpCoordinate(ref x1, ref y1);
      this.DoSingleDomainWarp(mSeed, amp, mFrequency, x1, y1, ref x, ref y);
      ++mSeed;
      amp *= this.mGain;
      mFrequency *= this.mLacunarity;
    }
  }

  private void DomainWarpFractalProgressive(ref FixedInt x, ref FixedInt y, ref FixedInt z)
  {
    int mSeed = this.mSeed;
    FixedInt amp = this.mDomainWarpAmp * this.mFractalBounding;
    FixedInt mFrequency = this.mFrequency;
    for (int index = 0; index < this.mOctaves; ++index)
    {
      FixedInt x1 = x;
      FixedInt y1 = y;
      FixedInt z1 = z;
      this.TransformDomainWarpCoordinate(ref x1, ref y1, ref z1);
      this.DoSingleDomainWarp(mSeed, amp, mFrequency, x1, y1, z1, ref x, ref y, ref z);
      ++mSeed;
      amp *= this.mGain;
      mFrequency *= this.mLacunarity;
    }
  }

  private void DomainWarpFractalIndependent(ref FixedInt x, ref FixedInt y)
  {
    FixedInt x1 = x;
    FixedInt y1 = y;
    this.TransformDomainWarpCoordinate(ref x1, ref y1);
    int mSeed = this.mSeed;
    FixedInt amp = this.mDomainWarpAmp * this.mFractalBounding;
    FixedInt mFrequency = this.mFrequency;
    for (int index = 0; index < this.mOctaves; ++index)
    {
      this.DoSingleDomainWarp(mSeed, amp, mFrequency, x1, y1, ref x, ref y);
      ++mSeed;
      amp *= this.mGain;
      mFrequency *= this.mLacunarity;
    }
  }

  private void DomainWarpFractalIndependent(ref FixedInt x, ref FixedInt y, ref FixedInt z)
  {
    FixedInt x1 = x;
    FixedInt y1 = y;
    FixedInt z1 = z;
    this.TransformDomainWarpCoordinate(ref x1, ref y1, ref z1);
    int mSeed = this.mSeed;
    FixedInt amp = this.mDomainWarpAmp * this.mFractalBounding;
    FixedInt mFrequency = this.mFrequency;
    for (int index = 0; index < this.mOctaves; ++index)
    {
      this.DoSingleDomainWarp(mSeed, amp, mFrequency, x1, y1, z1, ref x, ref y, ref z);
      ++mSeed;
      amp *= this.mGain;
      mFrequency *= this.mLacunarity;
    }
  }

  private void SingleDomainWarpBasicGrid(
    int seed,
    FixedInt warpAmp,
    FixedInt frequency,
    FixedInt x,
    FixedInt y,
    ref FixedInt xr,
    ref FixedInt yr)
  {
    FixedInt f1 = x * frequency;
    FixedInt f2 = y * frequency;
    int num1 = MyPerlin.FastFloor(f1);
    int num2 = MyPerlin.FastFloor(f2);
    FixedInt t1 = MyPerlin.InterpHermite(f1 - num1);
    FixedInt t2 = MyPerlin.InterpHermite(f2 - num2);
    int xPrimed1 = num1 * 501125321;
    int yPrimed1 = num2 * 1136930381;
    int xPrimed2 = xPrimed1 + 501125321;
    int yPrimed2 = yPrimed1 + 1136930381;
    int index1 = MyPerlin.Hash(seed, xPrimed1, yPrimed1) & 510;
    int index2 = MyPerlin.Hash(seed, xPrimed2, yPrimed1) & 510;
    FixedInt a1 = MyPerlin.Lerp(MyPerlin.RandVecs2D[index1], MyPerlin.RandVecs2D[index2], t1);
    FixedInt a2 = MyPerlin.Lerp(MyPerlin.RandVecs2D[index1 | 1], MyPerlin.RandVecs2D[index2 | 1], t1);
    int index3 = MyPerlin.Hash(seed, xPrimed1, yPrimed2) & 510;
    int index4 = MyPerlin.Hash(seed, xPrimed2, yPrimed2) & 510;
    FixedInt b1 = MyPerlin.Lerp(MyPerlin.RandVecs2D[index3], MyPerlin.RandVecs2D[index4], t1);
    FixedInt b2 = MyPerlin.Lerp(MyPerlin.RandVecs2D[index3 | 1], MyPerlin.RandVecs2D[index4 | 1], t1);
    xr += MyPerlin.Lerp(a1, b1, t2) * warpAmp;
    yr += MyPerlin.Lerp(a2, b2, t2) * warpAmp;
  }

  private void SingleDomainWarpBasicGrid(
    int seed,
    FixedInt warpAmp,
    FixedInt frequency,
    FixedInt x,
    FixedInt y,
    FixedInt z,
    ref FixedInt xr,
    ref FixedInt yr,
    ref FixedInt zr)
  {
    FixedInt f1 = x * frequency;
    FixedInt f2 = y * frequency;
    FixedInt f3 = z * frequency;
    int num1 = MyPerlin.FastFloor(f1);
    int num2 = MyPerlin.FastFloor(f2);
    int num3 = MyPerlin.FastFloor(f3);
    FixedInt t1 = MyPerlin.InterpHermite(f1 - num1);
    FixedInt t2 = MyPerlin.InterpHermite(f2 - num2);
    FixedInt t3 = MyPerlin.InterpHermite(f3 - num3);
    int xPrimed1 = num1 * 501125321;
    int yPrimed1 = num2 * 1136930381;
    int zPrimed1 = num3 * 1720413743;
    int xPrimed2 = xPrimed1 + 501125321;
    int yPrimed2 = yPrimed1 + 1136930381;
    int zPrimed2 = zPrimed1 + 1720413743;
    int index1 = MyPerlin.Hash(seed, xPrimed1, yPrimed1, zPrimed1) & 1020;
    int index2 = MyPerlin.Hash(seed, xPrimed2, yPrimed1, zPrimed1) & 1020;
    FixedInt a1 = MyPerlin.Lerp(MyPerlin.RandVecs3D[index1], MyPerlin.RandVecs3D[index2], t1);
    FixedInt a2 = MyPerlin.Lerp(MyPerlin.RandVecs3D[index1 | 1], MyPerlin.RandVecs3D[index2 | 1], t1);
    FixedInt a3 = MyPerlin.Lerp(MyPerlin.RandVecs3D[index1 | 2], MyPerlin.RandVecs3D[index2 | 2], t1);
    int index3 = MyPerlin.Hash(seed, xPrimed1, yPrimed2, zPrimed1) & 1020;
    int index4 = MyPerlin.Hash(seed, xPrimed2, yPrimed2, zPrimed1) & 1020;
    FixedInt b1 = MyPerlin.Lerp(MyPerlin.RandVecs3D[index3], MyPerlin.RandVecs3D[index4], t1);
    FixedInt b2 = MyPerlin.Lerp(MyPerlin.RandVecs3D[index3 | 1], MyPerlin.RandVecs3D[index4 | 1], t1);
    FixedInt b3 = MyPerlin.Lerp(MyPerlin.RandVecs3D[index3 | 2], MyPerlin.RandVecs3D[index4 | 2], t1);
    FixedInt a4 = MyPerlin.Lerp(a1, b1, t2);
    FixedInt a5 = MyPerlin.Lerp(a2, b2, t2);
    FixedInt a6 = MyPerlin.Lerp(a3, b3, t2);
    int index5 = MyPerlin.Hash(seed, xPrimed1, yPrimed1, zPrimed2) & 1020;
    int index6 = MyPerlin.Hash(seed, xPrimed2, yPrimed1, zPrimed2) & 1020;
    FixedInt a7 = MyPerlin.Lerp(MyPerlin.RandVecs3D[index5], MyPerlin.RandVecs3D[index6], t1);
    FixedInt a8 = MyPerlin.Lerp(MyPerlin.RandVecs3D[index5 | 1], MyPerlin.RandVecs3D[index6 | 1], t1);
    FixedInt a9 = MyPerlin.Lerp(MyPerlin.RandVecs3D[index5 | 2], MyPerlin.RandVecs3D[index6 | 2], t1);
    int index7 = MyPerlin.Hash(seed, xPrimed1, yPrimed2, zPrimed2) & 1020;
    int index8 = MyPerlin.Hash(seed, xPrimed2, yPrimed2, zPrimed2) & 1020;
    FixedInt b4 = MyPerlin.Lerp(MyPerlin.RandVecs3D[index7], MyPerlin.RandVecs3D[index8], t1);
    FixedInt b5 = MyPerlin.Lerp(MyPerlin.RandVecs3D[index7 | 1], MyPerlin.RandVecs3D[index8 | 1], t1);
    FixedInt b6 = MyPerlin.Lerp(MyPerlin.RandVecs3D[index7 | 2], MyPerlin.RandVecs3D[index8 | 2], t1);
    xr += MyPerlin.Lerp(a4, MyPerlin.Lerp(a7, b4, t2), t3) * warpAmp;
    yr += MyPerlin.Lerp(a5, MyPerlin.Lerp(a8, b5, t2), t3) * warpAmp;
    zr += MyPerlin.Lerp(a6, MyPerlin.Lerp(a9, b6, t2), t3) * warpAmp;
  }

  private void SingleDomainWarpSimplexGradient(
    int seed,
    FixedInt warpAmp,
    FixedInt frequency,
    FixedInt x,
    FixedInt y,
    ref FixedInt xr,
    ref FixedInt yr,
    bool outGradOnly)
  {
    FixedInt fixedInt1 = ((FixedInt) 3 - (FixedInt) 1816187L) / 6;
    x *= frequency;
    y *= frequency;
    int num1 = MyPerlin.FastFloor(x);
    int num2 = MyPerlin.FastFloor(y);
    FixedInt fixedInt2 = x - num1;
    FixedInt fixedInt3 = y - num2;
    FixedInt fixedInt4 = (fixedInt2 + fixedInt3) * fixedInt1;
    FixedInt xd1 = fixedInt2 - fixedInt4;
    FixedInt yd1 = fixedInt3 - fixedInt4;
    int xPrimed = num1 * 501125321;
    int yPrimed = num2 * 1136930381;
    FixedInt fixedInt5;
    FixedInt fixedInt6 = fixedInt5 = (FixedInt) 0;
    FixedInt fixedInt7 = (FixedInt) 524288L - xd1 * xd1 - yd1 * yd1;
    if (fixedInt7 > 0)
    {
      FixedInt fixedInt8 = fixedInt7 * fixedInt7 * (fixedInt7 * fixedInt7);
      FixedInt xo;
      FixedInt yo;
      if (outGradOnly)
        MyPerlin.GradCoordOut(seed, xPrimed, yPrimed, out xo, out yo);
      else
        MyPerlin.GradCoordDual(seed, xPrimed, yPrimed, xd1, yd1, out xo, out yo);
      fixedInt6 += fixedInt8 * xo;
      fixedInt5 += fixedInt8 * yo;
    }
    FixedInt fixedInt9 = (FixedInt) 2 * ((FixedInt) 1 - (FixedInt) 2 * fixedInt1) * ((FixedInt) 1 / fixedInt1 - 2) * fixedInt4 + ((FixedInt) -2 * ((FixedInt) 1 - (FixedInt) 2 * fixedInt1) * ((FixedInt) 1 - (FixedInt) 2 * fixedInt1) + fixedInt7);
    if (fixedInt9 > 0)
    {
      FixedInt xd2 = xd1 + ((FixedInt) 2 * fixedInt1 - 1);
      FixedInt yd2 = yd1 + ((FixedInt) 2 * fixedInt1 - 1);
      FixedInt fixedInt10 = fixedInt9 * fixedInt9 * (fixedInt9 * fixedInt9);
      FixedInt xo;
      FixedInt yo;
      if (outGradOnly)
        MyPerlin.GradCoordOut(seed, xPrimed + 501125321, yPrimed + 1136930381, out xo, out yo);
      else
        MyPerlin.GradCoordDual(seed, xPrimed + 501125321, yPrimed + 1136930381, xd2, yd2, out xo, out yo);
      fixedInt6 += fixedInt10 * xo;
      fixedInt5 += fixedInt10 * yo;
    }
    if (yd1 > xd1)
    {
      FixedInt xd3 = xd1 + fixedInt1;
      FixedInt yd3 = yd1 + (fixedInt1 - 1);
      FixedInt fixedInt11 = (FixedInt) 524288L - xd3 * xd3 - yd3 * yd3;
      if (fixedInt11 > 0)
      {
        FixedInt fixedInt12 = fixedInt11 * fixedInt11 * (fixedInt11 * fixedInt11);
        FixedInt xo;
        FixedInt yo;
        if (outGradOnly)
          MyPerlin.GradCoordOut(seed, xPrimed, yPrimed + 1136930381, out xo, out yo);
        else
          MyPerlin.GradCoordDual(seed, xPrimed, yPrimed + 1136930381, xd3, yd3, out xo, out yo);
        fixedInt6 += fixedInt12 * xo;
        fixedInt5 += fixedInt12 * yo;
      }
    }
    else
    {
      FixedInt xd4 = xd1 + (fixedInt1 - 1);
      FixedInt yd4 = yd1 + fixedInt1;
      FixedInt fixedInt13 = (FixedInt) 524288L - xd4 * xd4 - yd4 * yd4;
      if (fixedInt13 > 0)
      {
        FixedInt fixedInt14 = fixedInt13 * fixedInt13 * (fixedInt13 * fixedInt13);
        FixedInt xo;
        FixedInt yo;
        if (outGradOnly)
          MyPerlin.GradCoordOut(seed, xPrimed + 501125321, yPrimed, out xo, out yo);
        else
          MyPerlin.GradCoordDual(seed, xPrimed + 501125321, yPrimed, xd4, yd4, out xo, out yo);
        fixedInt6 += fixedInt14 * xo;
        fixedInt5 += fixedInt14 * yo;
      }
    }
    xr += fixedInt6 * warpAmp;
    yr += fixedInt5 * warpAmp;
  }

  private void SingleDomainWarpOpenSimplex2Gradient(
    int seed,
    FixedInt warpAmp,
    FixedInt frequency,
    FixedInt x,
    FixedInt y,
    FixedInt z,
    ref FixedInt xr,
    ref FixedInt yr,
    ref FixedInt zr,
    bool outGradOnly)
  {
    x *= frequency;
    y *= frequency;
    z *= frequency;
    int num1 = MyPerlin.FastRound(x);
    int num2 = MyPerlin.FastRound(y);
    int num3 = MyPerlin.FastRound(z);
    FixedInt xd1 = x - num1;
    FixedInt yd1 = y - num2;
    FixedInt zd1 = z - num3;
    int num4 = (int) (-xd1 - (FixedInt) 1) | 1;
    int num5 = (int) (-yd1 - (FixedInt) 1) | 1;
    int num6 = (int) (-zd1 - (FixedInt) 1) | 1;
    FixedInt fixedInt1 = (FixedInt) num4 * -xd1;
    FixedInt fixedInt2 = (FixedInt) num5 * -yd1;
    FixedInt fixedInt3 = (FixedInt) num6 * -zd1;
    int xPrimed1 = num1 * 501125321;
    int yPrimed1 = num2 * 1136930381;
    int zPrimed1 = num3 * 1720413743;
    FixedInt fixedInt4;
    FixedInt fixedInt5 = fixedInt4 = (FixedInt) 0;
    FixedInt fixedInt6 = fixedInt4;
    FixedInt fixedInt7 = fixedInt4;
    FixedInt fixedInt8 = (FixedInt) 629145L - xd1 * xd1 - (yd1 * yd1 + zd1 * zd1);
    int num7 = 0;
    while (true)
    {
      if (fixedInt8 > 0)
      {
        FixedInt fixedInt9 = fixedInt8 * fixedInt8 * (fixedInt8 * fixedInt8);
        FixedInt xo;
        FixedInt yo;
        FixedInt zo;
        if (outGradOnly)
          MyPerlin.GradCoordOut(seed, xPrimed1, yPrimed1, zPrimed1, out xo, out yo, out zo);
        else
          MyPerlin.GradCoordDual(seed, xPrimed1, yPrimed1, zPrimed1, xd1, yd1, zd1, out xo, out yo, out zo);
        fixedInt7 += fixedInt9 * xo;
        fixedInt6 += fixedInt9 * yo;
        fixedInt5 += fixedInt9 * zo;
      }
      FixedInt fixedInt10 = fixedInt8;
      int xPrimed2 = xPrimed1;
      int yPrimed2 = yPrimed1;
      int zPrimed2 = zPrimed1;
      FixedInt xd2 = xd1;
      FixedInt yd2 = yd1;
      FixedInt zd2 = zd1;
      FixedInt fixedInt11;
      if (fixedInt1 >= fixedInt2 && fixedInt1 >= fixedInt3)
      {
        xd2 += num4;
        fixedInt11 = fixedInt10 + fixedInt1 + fixedInt1;
        xPrimed2 -= num4 * 501125321;
      }
      else if (fixedInt2 > fixedInt1 && fixedInt2 >= fixedInt3)
      {
        yd2 += num5;
        fixedInt11 = fixedInt10 + fixedInt2 + fixedInt2;
        yPrimed2 -= num5 * 1136930381;
      }
      else
      {
        zd2 += num6;
        fixedInt11 = fixedInt10 + fixedInt3 + fixedInt3;
        zPrimed2 -= num6 * 1720413743;
      }
      if (fixedInt11 > 1)
      {
        FixedInt fixedInt12 = fixedInt11 - 1;
        FixedInt fixedInt13 = fixedInt12 * fixedInt12 * (fixedInt12 * fixedInt12);
        FixedInt xo;
        FixedInt yo;
        FixedInt zo;
        if (outGradOnly)
          MyPerlin.GradCoordOut(seed, xPrimed2, yPrimed2, zPrimed2, out xo, out yo, out zo);
        else
          MyPerlin.GradCoordDual(seed, xPrimed2, yPrimed2, zPrimed2, xd2, yd2, zd2, out xo, out yo, out zo);
        fixedInt7 += fixedInt13 * xo;
        fixedInt6 += fixedInt13 * yo;
        fixedInt5 += fixedInt13 * zo;
      }
      if (num7 != 1)
      {
        fixedInt1 = (FixedInt) 524288L - fixedInt1;
        fixedInt2 = (FixedInt) 524288L - fixedInt2;
        fixedInt3 = (FixedInt) 524288L - fixedInt3;
        xd1 = (FixedInt) num4 * fixedInt1;
        yd1 = (FixedInt) num5 * fixedInt2;
        zd1 = (FixedInt) num6 * fixedInt3;
        fixedInt8 += (FixedInt) 786432L - fixedInt1 - (fixedInt2 + fixedInt3);
        xPrimed1 += num4 >> 1 & 501125321;
        yPrimed1 += num5 >> 1 & 1136930381;
        zPrimed1 += num6 >> 1 & 1720413743;
        num4 = -num4;
        num5 = -num5;
        num6 = -num6;
        seed += 1293373;
        ++num7;
      }
      else
        break;
    }
    xr += fixedInt7 * warpAmp;
    yr += fixedInt6 * warpAmp;
    zr += fixedInt5 * warpAmp;
  }

  public enum NoiseType
  {
    OpenSimplex2,
    OpenSimplex2S,
    Cellular,
    Perlin,
    ValueCubic,
    Value,
  }

  public enum RotationType3D
  {
    None,
    ImproveXYPlanes,
    ImproveXZPlanes,
  }

  public enum FractalType
  {
    None,
    FBm,
    Ridged,
    PingPong,
    DomainWarpProgressive,
    DomainWarpIndependent,
  }

  public enum CellularDistanceFunction
  {
    Euclidean,
    EuclideanSq,
    Manhattan,
    Hybrid,
  }

  public enum CellularReturnType
  {
    CellValue,
    Distance,
    Distance2,
    Distance2Add,
    Distance2Sub,
    Distance2Mul,
    Distance2Div,
  }

  public enum DomainWarpType
  {
    OpenSimplex2,
    OpenSimplex2Reduced,
    BasicGrid,
  }

  private enum TransformType3D
  {
    None,
    ImproveXYPlanes,
    ImproveXZPlanes,
    DefaultOpenSimplex2,
  }
}

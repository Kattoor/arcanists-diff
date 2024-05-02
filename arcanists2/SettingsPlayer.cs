// Decompiled with JetBrains decompiler
// Type: SettingsPlayer
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using Hazel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using UnityEngine;

#nullable disable
[Serializable]
public class SettingsPlayer
{
  public const int NumOutfitParts = 11;
  public SpellsOnly _spells = new SpellsOnly();
  public const byte _Version = 4;
  public byte indexHead;
  public byte indexBeard;
  public byte indexBeard2;
  public byte indexBeard3;
  public byte indexHat;
  public byte indexBody;
  public byte indexLeftHand;
  public byte indexRightHand;
  public byte indexLeftFoot;
  public byte indexRightFoot;
  public byte indexMouth;
  public static readonly byte[] clanOutfit = new byte[9]
  {
    (byte) 100,
    (byte) 80,
    (byte) 106,
    (byte) 120,
    (byte) 112,
    (byte) 79,
    (byte) 100,
    (byte) 100,
    byte.MaxValue
  };
  public Recolor coloring = new Recolor();
  public byte custom;
  public string[] customPieces;
  public Sprite[] textures;
  public SettingsPlayer.CustomAnim[] animations;
  public SettingsPlayer.MetaData[] metaData;
  public static byte Achievement_GameOutfit = byte.MaxValue;
  public static ulong id_sno = 628590527973163009;
  public static int sno_body = 65;
  public static int sno_body2 = 67;
  public static int sno_body3 = 68;
  public static int sno_body4 = 69;
  public static int brine_body = 76;
  public static int dharok_body = 80;
  public static int strelizia_body = 125;
  public static int quinn_body2 = 140;
  public static int sno_head = 59;
  public static int sno_head2 = 61;
  public static int sno_head3 = 62;
  public static int sno_head4 = 63;
  public static int brine_head = 67;
  public static int sno_pHand = 63;
  public static int sno_pHand2 = 64;
  public static int sno_pHand3 = 65;
  public static int brine_hand = 73;
  public static int strelizia_pHand = 144;
  public static int godsword = 145;
  public static int olympic_torch = 146;
  public static int cosmos_helper = 154;
  public static int sno_mHand = 64;
  public static int sno_mHand2 = 65;
  public static int sno_mHand3 = 66;
  public static int strelizia_mHand = 118;
  public static int sno_hat = 74;
  public static int strelizia_hat = 142;
  public static int sno_beard = 57;
  public static int sno_beard2 = 59;
  public static int body_skate = 142;
  public static List<List<SettingsPlayer.ByDust>> LockedByDust = new List<List<SettingsPlayer.ByDust>>()
  {
    new List<SettingsPlayer.ByDust>(),
    new List<SettingsPlayer.ByDust>(),
    new List<SettingsPlayer.ByDust>(),
    new List<SettingsPlayer.ByDust>()
    {
      new SettingsPlayer.ByDust(97, 30, "Bronze Trophy"),
      new SettingsPlayer.ByDust(98, 50, "Silver Trophy"),
      new SettingsPlayer.ByDust(99, 101, "Gold Trophy")
    },
    new List<SettingsPlayer.ByDust>(),
    new List<SettingsPlayer.ByDust>()
    {
      new SettingsPlayer.ByDust(66, 20, "Olympic Bronze Medal"),
      new SettingsPlayer.ByDust(67, 50, "Olympic Silver Medal"),
      new SettingsPlayer.ByDust(68, 100, "Olympic Gold Medal")
    }
  };
  public static List<List<SettingsPlayer.ByReason>> LockedByReason = new List<List<SettingsPlayer.ByReason>>()
  {
    new List<SettingsPlayer.ByReason>()
    {
      new SettingsPlayer.ByReason(146, "Played before the rate split")
    },
    new List<SettingsPlayer.ByReason>(),
    new List<SettingsPlayer.ByReason>()
    {
      new SettingsPlayer.ByReason((int) sbyte.MaxValue, "Played before the rate split")
    },
    new List<SettingsPlayer.ByReason>(),
    new List<SettingsPlayer.ByReason>()
    {
      new SettingsPlayer.ByReason(165, "Played before the rate split")
    },
    new List<SettingsPlayer.ByReason>()
  };
  public static List<List<SettingsPlayer.ByAchievement>> LockedByAchievement = new List<List<SettingsPlayer.ByAchievement>>()
  {
    new List<SettingsPlayer.ByAchievement>()
    {
      new SettingsPlayer.ByAchievement(24, Achievement.High_Mage),
      new SettingsPlayer.ByAchievement(25, Achievement.High_Mage),
      new SettingsPlayer.ByAchievement(26, Achievement.High_Mage),
      new SettingsPlayer.ByAchievement(27, Achievement.Arch_Mage),
      new SettingsPlayer.ByAchievement(28, Achievement.Arch_Mage),
      new SettingsPlayer.ByAchievement(29, Achievement.Arch_Mage),
      new SettingsPlayer.ByAchievement(80, Achievement.Axing_for_Trouble),
      new SettingsPlayer.ByAchievement(86, Achievement.Just_a_Flesh_Wound),
      new SettingsPlayer.ByAchievement(71, Achievement.Arch_Mage),
      new SettingsPlayer.ByAchievement(77, Achievement.High_Mage),
      new SettingsPlayer.ByAchievement(87, Achievement.You_got_the_Touch),
      new SettingsPlayer.ByAchievement(88, Achievement.Kraken_Guard),
      new SettingsPlayer.ByAchievement(90, Achievement.Block_out_the_sun),
      new SettingsPlayer.ByAchievement(91, Achievement.Block_out_the_sun),
      new SettingsPlayer.ByAchievement(94, Achievement.Seas_Arcanist),
      new SettingsPlayer.ByAchievement(95, Achievement.Blood_Arcanist),
      new SettingsPlayer.ByAchievement(96, Achievement.Blood_Arcanist),
      new SettingsPlayer.ByAchievement(97, Achievement.Master_Team_Leader),
      new SettingsPlayer.ByAchievement(102, Achievement.Overlight_Arcanist),
      new SettingsPlayer.ByAchievement(106, Achievement.Soul_Drain),
      new SettingsPlayer.ByAchievement(107, Achievement.Nature_Arcanist),
      new SettingsPlayer.ByAchievement(108, Achievement.Claustrophobia),
      new SettingsPlayer.ByAchievement(111, Achievement.Master_of_Arcanists),
      new SettingsPlayer.ByAchievement(112, Achievement.Mage),
      new SettingsPlayer.ByAchievement(113, Achievement.Lord_of_Wands),
      new SettingsPlayer.ByAchievement(114, Achievement.Lord_of_Wands),
      new SettingsPlayer.ByAchievement(115, Achievement.Frost_Arcanist),
      new SettingsPlayer.ByAchievement(116, Achievement.Master_of_Seas),
      new SettingsPlayer.ByAchievement(117, Achievement.Master_of_Seas),
      new SettingsPlayer.ByAchievement(118, Achievement.Flame_Arcanist),
      new SettingsPlayer.ByAchievement(119, Achievement.Paper_Bag),
      new SettingsPlayer.ByAchievement(120, Achievement.Block_out_the_sun),
      new SettingsPlayer.ByAchievement(121, Achievement.Glider),
      new SettingsPlayer.ByAchievement(122, Achievement.Champion_of_Magic),
      new SettingsPlayer.ByAchievement(123, Achievement.Seasons_Arcanist),
      new SettingsPlayer.ByAchievement(124, Achievement.Master_of_Imps),
      new SettingsPlayer.ByAchievement(126, Achievement.The_Wilds_Arcanist),
      new SettingsPlayer.ByAchievement((int) sbyte.MaxValue, Achievement.Arch_Mage),
      new SettingsPlayer.ByAchievement(134, Achievement.Lord_of_Wands),
      new SettingsPlayer.ByAchievement(135, Achievement.Wand_Hoarder),
      new SettingsPlayer.ByAchievement(145, Achievement.Diplomat),
      new SettingsPlayer.ByAchievement(147, Achievement.High_Mage_Thinker),
      new SettingsPlayer.ByAchievement(148, Achievement.High_Mage_Blitzer),
      new SettingsPlayer.ByAchievement(149, Achievement.Swift_Arch_Mage),
      new SettingsPlayer.ByAchievement(150, Achievement.Swift_Arch_Mage),
      new SettingsPlayer.ByAchievement(151, Achievement.Methodical_Arch_Mage),
      new SettingsPlayer.ByAchievement(152, Achievement.Lively_Arch_Mage),
      new SettingsPlayer.ByAchievement(153, Achievement.Lively_Arch_Mage),
      new SettingsPlayer.ByAchievement(154, Achievement.Supreme_Arch_Mage),
      new SettingsPlayer.ByAchievement(155, Achievement.Supreme_Arch_Mage),
      new SettingsPlayer.ByAchievement(156, Achievement.Supreme_Arch_Mage),
      new SettingsPlayer.ByAchievement(157, Achievement.Supreme_Arch_Mage),
      new SettingsPlayer.ByAchievement(158, Achievement.It_s_Alive_),
      new SettingsPlayer.ByAchievement(159, Achievement.Ice_Hot),
      new SettingsPlayer.ByAchievement(160, Achievement.Multi_Dunk_),
      new SettingsPlayer.ByAchievement(162, Achievement.High_Mage_Partier),
      new SettingsPlayer.ByAchievement(163, Achievement.High_Mage_Partier)
    },
    new List<SettingsPlayer.ByAchievement>()
    {
      new SettingsPlayer.ByAchievement(73, Achievement.Kraken_Guard),
      new SettingsPlayer.ByAchievement(82, Achievement.Master_of_Seas),
      new SettingsPlayer.ByAchievement(83, Achievement.Master_of_Seas),
      new SettingsPlayer.ByAchievement(84, Achievement.Lord_of_Wands),
      new SettingsPlayer.ByAchievement(85, Achievement.Lord_of_Wands),
      new SettingsPlayer.ByAchievement(87, Achievement.Master_of_Minions),
      new SettingsPlayer.ByAchievement(88, Achievement.Master_of_Imps),
      new SettingsPlayer.ByAchievement(95, Achievement.Diplomat),
      new SettingsPlayer.ByAchievement(96, Achievement.High_Mage_Blitzer),
      new SettingsPlayer.ByAchievement(97, Achievement.High_Mage_Blitzer),
      new SettingsPlayer.ByAchievement(98, Achievement.Wrath_Wreaker),
      new SettingsPlayer.ByAchievement(99, Achievement.Swift_Arch_Mage)
    },
    new List<SettingsPlayer.ByAchievement>()
    {
      new SettingsPlayer.ByAchievement(24, Achievement.High_Mage),
      new SettingsPlayer.ByAchievement(25, Achievement.High_Mage),
      new SettingsPlayer.ByAchievement(26, Achievement.High_Mage),
      new SettingsPlayer.ByAchievement(27, Achievement.Swift_Arch_Mage),
      new SettingsPlayer.ByAchievement(28, Achievement.Arch_Mage),
      new SettingsPlayer.ByAchievement(29, Achievement.Methodical_Arch_Mage),
      new SettingsPlayer.ByAchievement(79, Achievement.Axing_for_Trouble),
      new SettingsPlayer.ByAchievement(85, Achievement.Mark_of_Fame),
      new SettingsPlayer.ByAchievement(84, Achievement.High_Mage),
      new SettingsPlayer.ByAchievement(89, Achievement.You_got_the_Touch),
      new SettingsPlayer.ByAchievement(90, Achievement.Kraken_Guard),
      new SettingsPlayer.ByAchievement(92, Achievement._3rd_Dimension),
      new SettingsPlayer.ByAchievement(93, Achievement.Glider),
      new SettingsPlayer.ByAchievement(94, Achievement.Explosives_Expert),
      new SettingsPlayer.ByAchievement(95, Achievement.Guardian_of_Souls),
      new SettingsPlayer.ByAchievement(96, Achievement.Page_Turner),
      new SettingsPlayer.ByAchievement(98, Achievement.Seas_Arcanist),
      new SettingsPlayer.ByAchievement(99, Achievement.Sniper),
      new SettingsPlayer.ByAchievement(108, Achievement.Frost_Arcanist),
      new SettingsPlayer.ByAchievement(111, Achievement.Master_of_Seas),
      new SettingsPlayer.ByAchievement(112, Achievement.Master_of_Arcanists),
      new SettingsPlayer.ByAchievement(113, Achievement.Master_of_Seas),
      new SettingsPlayer.ByAchievement(114, Achievement.Master_of_Nature),
      new SettingsPlayer.ByAchievement(115, Achievement.Master_of_Minions),
      new SettingsPlayer.ByAchievement(116, Achievement.Champion_of_Magic),
      new SettingsPlayer.ByAchievement(117, Achievement.Master_of_Imps),
      new SettingsPlayer.ByAchievement(119, Achievement.Cursed_Earth),
      new SettingsPlayer.ByAchievement(120, Achievement.The_Wilds_Arcanist),
      new SettingsPlayer.ByAchievement(121, Achievement.Arch_Mage),
      new SettingsPlayer.ByAchievement(128, Achievement.High_Mage_Blitzer),
      new SettingsPlayer.ByAchievement(129, Achievement.High_Mage_Partier),
      new SettingsPlayer.ByAchievement(130, Achievement.Lively_Arch_Mage),
      new SettingsPlayer.ByAchievement(131, Achievement.SMASH_),
      new SettingsPlayer.ByAchievement(132, Achievement.Whiter_than_White),
      new SettingsPlayer.ByAchievement(133, Achievement.It_s_Alive_),
      new SettingsPlayer.ByAchievement(134, Achievement.It_s_Alive_),
      new SettingsPlayer.ByAchievement(135, Achievement.Ice_Hot),
      new SettingsPlayer.ByAchievement(136, Achievement.Ice_Hot),
      new SettingsPlayer.ByAchievement(137, Achievement.Multi_Dunk_),
      new SettingsPlayer.ByAchievement(138, Achievement.Comet_Chucker),
      new SettingsPlayer.ByAchievement(139, Achievement.Volcanic_Volleyer),
      new SettingsPlayer.ByAchievement(140, Achievement.Fissure_Finisher),
      new SettingsPlayer.ByAchievement(143, Achievement.Lively_Arch_Mage)
    },
    new List<SettingsPlayer.ByAchievement>()
    {
      new SettingsPlayer.ByAchievement(24, Achievement.High_Mage),
      new SettingsPlayer.ByAchievement(25, Achievement.High_Mage),
      new SettingsPlayer.ByAchievement(26, Achievement.High_Mage),
      new SettingsPlayer.ByAchievement(27, Achievement.Swift_Arch_Mage),
      new SettingsPlayer.ByAchievement(28, Achievement.Arch_Mage),
      new SettingsPlayer.ByAchievement(29, Achievement.Methodical_Arch_Mage),
      new SettingsPlayer.ByAchievement(75, Achievement.High_Mage),
      new SettingsPlayer.ByAchievement(82, Achievement.Axing_for_Trouble),
      new SettingsPlayer.ByAchievement(85, Achievement.Master_of_Flame),
      new SettingsPlayer.ByAchievement(90, Achievement.Circle_of_Death),
      new SettingsPlayer.ByAchievement(91, Achievement.Mark_of_Fame),
      new SettingsPlayer.ByAchievement(87, Achievement.High_Mage),
      new SettingsPlayer.ByAchievement(94, Achievement.Master_of_Arcanists),
      new SettingsPlayer.ByAchievement(96, Achievement.Nature_Arcanist),
      new SettingsPlayer.ByAchievement(98, Achievement.Turing_Machine),
      new SettingsPlayer.ByAchievement(99, Achievement.Seas_Arcanist),
      new SettingsPlayer.ByAchievement(101, Achievement.You_got_the_Touch),
      new SettingsPlayer.ByAchievement(102, Achievement.Apprentice),
      new SettingsPlayer.ByAchievement(100, Achievement.Paper_Bag),
      new SettingsPlayer.ByAchievement(106, Achievement.Kraken_Guard),
      new SettingsPlayer.ByAchievement(109, Achievement.Axing_for_Trouble),
      new SettingsPlayer.ByAchievement(110, Achievement.Blade_Master),
      new SettingsPlayer.ByAchievement(112, Achievement.Celestial_Lord),
      new SettingsPlayer.ByAchievement(113, Achievement.Icarus),
      new SettingsPlayer.ByAchievement(114, Achievement.Master_of_Monkeys),
      new SettingsPlayer.ByAchievement(122, Achievement.Master_of_Elements),
      new SettingsPlayer.ByAchievement(124, Achievement.Overlight_Arcanist),
      new SettingsPlayer.ByAchievement(125, Achievement.Blood_Arcanist),
      new SettingsPlayer.ByAchievement(126, Achievement.Soul_Drain),
      new SettingsPlayer.ByAchievement((int) sbyte.MaxValue, Achievement.Nature_Arcanist),
      new SettingsPlayer.ByAchievement(129, Achievement.Master_of_Arcanists),
      new SettingsPlayer.ByAchievement(130, Achievement.Blade_Master),
      new SettingsPlayer.ByAchievement(131, Achievement.Lord_of_Wands),
      new SettingsPlayer.ByAchievement(132, Achievement.Frost_Arcanist),
      new SettingsPlayer.ByAchievement(134, Achievement.Flame_Arcanist),
      new SettingsPlayer.ByAchievement(135, Achievement.Master_of_Minions),
      new SettingsPlayer.ByAchievement(136, Achievement.Master_of_Darkness),
      new SettingsPlayer.ByAchievement(78, Achievement.Arch_Mage),
      new SettingsPlayer.ByAchievement(138, Achievement.Overlight_Arcanist),
      new SettingsPlayer.ByAchievement(139, Achievement.Cursed_Earth),
      new SettingsPlayer.ByAchievement(140, Achievement.Blood_Arcanist),
      new SettingsPlayer.ByAchievement(141, Achievement.Drop_Shot),
      new SettingsPlayer.ByAchievement(142, Achievement.Seasons_Arcanist),
      new SettingsPlayer.ByAchievement(143, Achievement.Champion_of_Magic),
      new SettingsPlayer.ByAchievement(137, Achievement.Master_of_Imps),
      new SettingsPlayer.ByAchievement(147, Achievement.The_Wilds_Arcanist),
      new SettingsPlayer.ByAchievement(167, Achievement.Natural_Spaghetti),
      new SettingsPlayer.ByAchievement(168, Achievement.Diplomat),
      new SettingsPlayer.ByAchievement(169, Achievement.Whiter_than_White),
      new SettingsPlayer.ByAchievement(170, Achievement.High_Mage_Blitzer),
      new SettingsPlayer.ByAchievement(171, Achievement.High_Mage_Thinker),
      new SettingsPlayer.ByAchievement(172, Achievement.High_Mage_Partier),
      new SettingsPlayer.ByAchievement(173, Achievement.Lively_Arch_Mage),
      new SettingsPlayer.ByAchievement(174, Achievement.Supreme_Arch_Mage),
      new SettingsPlayer.ByAchievement(175, Achievement.SMASH_),
      new SettingsPlayer.ByAchievement(176, Achievement.Ice_Hot),
      new SettingsPlayer.ByAchievement(177, Achievement.Ice_Hot),
      new SettingsPlayer.ByAchievement(178, Achievement.Multi_Dunk_),
      new SettingsPlayer.ByAchievement(179, Achievement.Crazy)
    },
    new List<SettingsPlayer.ByAchievement>()
    {
      new SettingsPlayer.ByAchievement(42, Achievement.Enchanter),
      new SettingsPlayer.ByAchievement(43, Achievement.Apprentice),
      new SettingsPlayer.ByAchievement(44, Achievement.Acolyte),
      new SettingsPlayer.ByAchievement(45, Achievement.Mage),
      new SettingsPlayer.ByAchievement(46, Achievement.High_Mage),
      new SettingsPlayer.ByAchievement(47, Achievement.Arch_Mage),
      new SettingsPlayer.ByAchievement(83, Achievement.Axing_for_Trouble),
      new SettingsPlayer.ByAchievement(77, Achievement.Mark_of_Fame),
      new SettingsPlayer.ByAchievement(101, Achievement.Didn_t_see_that_coming),
      new SettingsPlayer.ByAchievement(106, Achievement.You_got_the_Touch),
      new SettingsPlayer.ByAchievement(108, Achievement.Block_out_the_sun),
      new SettingsPlayer.ByAchievement(110, Achievement.Master_Team_Leader),
      new SettingsPlayer.ByAchievement(113, Achievement.Master_of_Seas),
      new SettingsPlayer.ByAchievement(114, Achievement.Master_of_Seas),
      new SettingsPlayer.ByAchievement(115, Achievement.Frost_Arcanist),
      new SettingsPlayer.ByAchievement(116, Achievement.Blood_Arcanist),
      new SettingsPlayer.ByAchievement(118, Achievement.Master_of_Nature),
      new SettingsPlayer.ByAchievement(120, Achievement.Panoramic_View),
      new SettingsPlayer.ByAchievement(121, Achievement.Panoramic_View),
      new SettingsPlayer.ByAchievement(125, Achievement.Master_of_Arcanists),
      new SettingsPlayer.ByAchievement(126, Achievement.Mage),
      new SettingsPlayer.ByAchievement((int) sbyte.MaxValue, Achievement.Flame_Arcanist),
      new SettingsPlayer.ByAchievement((int) sbyte.MaxValue, Achievement.Frost_Arcanist),
      new SettingsPlayer.ByAchievement((int) sbyte.MaxValue, Achievement.Nature_Arcanist),
      new SettingsPlayer.ByAchievement(128, Achievement.Master_of_Minions),
      new SettingsPlayer.ByAchievement(129, Achievement.Overlight_Arcanist),
      new SettingsPlayer.ByAchievement(130, Achievement.Soul_Drain),
      new SettingsPlayer.ByAchievement(131, Achievement.Axing_for_Trouble),
      new SettingsPlayer.ByAchievement(132, Achievement.Glider),
      new SettingsPlayer.ByAchievement(133, Achievement.Champion_of_Magic),
      new SettingsPlayer.ByAchievement(134, Achievement.Seasons_Arcanist),
      new SettingsPlayer.ByAchievement(135, Achievement.Block_out_the_sun),
      new SettingsPlayer.ByAchievement(136, Achievement.Master_of_Imps),
      new SettingsPlayer.ByAchievement(139, Achievement.Wand_Hoarder),
      new SettingsPlayer.ByAchievement(140, Achievement.Wand_Hoarder),
      new SettingsPlayer.ByAchievement(141, Achievement.Master_of_Darkness),
      new SettingsPlayer.ByAchievement(144, Achievement.Master_of_Illusion),
      new SettingsPlayer.ByAchievement(145, Achievement.The_Wilds_Arcanist),
      new SettingsPlayer.ByAchievement(146, Achievement.Arch_Mage),
      new SettingsPlayer.ByAchievement(151, Achievement.Cosmos_Arcanist),
      new SettingsPlayer.ByAchievement(152, Achievement.Wand_Hoarder),
      new SettingsPlayer.ByAchievement(154, Achievement.Seas_Arcanist),
      new SettingsPlayer.ByAchievement(161, Achievement.Seasons_Arcanist),
      new SettingsPlayer.ByAchievement(162, Achievement.Stone_Arcanist),
      new SettingsPlayer.ByAchievement(163, Achievement.Flame_Arcanist),
      new SettingsPlayer.ByAchievement(164, Achievement.Didn_t_see_that_coming),
      new SettingsPlayer.ByAchievement(166, Achievement.High_Mage_Blitzer),
      new SettingsPlayer.ByAchievement(167, Achievement.High_Mage_Thinker),
      new SettingsPlayer.ByAchievement(168, Achievement.High_Mage_Partier),
      new SettingsPlayer.ByAchievement(169, Achievement.Lively_Arch_Mage),
      new SettingsPlayer.ByAchievement(170, Achievement.Methodical_Arch_Mage),
      new SettingsPlayer.ByAchievement(171, Achievement.It_s_Alive_),
      new SettingsPlayer.ByAchievement(172, Achievement.Ice_Hot),
      new SettingsPlayer.ByAchievement(173, Achievement.Multi_Dunk_),
      new SettingsPlayer.ByAchievement(174, Achievement.Kraken_Guard),
      new SettingsPlayer.ByAchievement(175, Achievement.Crazy)
    },
    new List<SettingsPlayer.ByAchievement>()
    {
      new SettingsPlayer.ByAchievement(63, Achievement.Axing_for_Trouble),
      new SettingsPlayer.ByAchievement(71, Achievement.Master_Team_Leader),
      new SettingsPlayer.ByAchievement(72, Achievement.With_the_Fishies),
      new SettingsPlayer.ByAchievement(75, Achievement.Seas_Arcanist),
      new SettingsPlayer.ByAchievement(78, Achievement.Soul_Drain),
      new SettingsPlayer.ByAchievement(88, Achievement.Lord_of_Wands),
      new SettingsPlayer.ByAchievement(89, Achievement.Lord_of_Wands),
      new SettingsPlayer.ByAchievement(92, Achievement.Glider),
      new SettingsPlayer.ByAchievement(93, Achievement.Cosmos_Arcanist),
      new SettingsPlayer.ByAchievement(95, Achievement.Didn_t_see_that_coming),
      new SettingsPlayer.ByAchievement(96, Achievement.High_Mage_Blitzer),
      new SettingsPlayer.ByAchievement(97, Achievement.High_Mage_Partier),
      new SettingsPlayer.ByAchievement(98, Achievement.Supreme_Arch_Mage),
      new SettingsPlayer.ByAchievement(99, Achievement.Miasma)
    }
  };
  public static List<SettingsPlayer.Seasonal> seasonHalloween = new List<SettingsPlayer.Seasonal>()
  {
    new SettingsPlayer.Seasonal()
    {
      index = 76,
      outfit = Outfit.Beard
    },
    new SettingsPlayer.Seasonal()
    {
      index = 93,
      outfit = Outfit.Body
    },
    new SettingsPlayer.Seasonal()
    {
      index = 137,
      outfit = Outfit.Body
    },
    new SettingsPlayer.Seasonal()
    {
      index = 76,
      outfit = Outfit.Head
    },
    new SettingsPlayer.Seasonal()
    {
      index = 77,
      outfit = Outfit.Head
    },
    new SettingsPlayer.Seasonal()
    {
      index = 77,
      outfit = Outfit.Beard
    },
    new SettingsPlayer.Seasonal()
    {
      index = 92,
      outfit = Outfit.Head
    },
    new SettingsPlayer.Seasonal()
    {
      index = 116,
      outfit = Outfit.RightHand
    },
    new SettingsPlayer.Seasonal()
    {
      index = 117,
      outfit = Outfit.RightHand
    },
    new SettingsPlayer.Seasonal()
    {
      index = 118,
      outfit = Outfit.RightHand
    },
    new SettingsPlayer.Seasonal()
    {
      index = 123,
      outfit = Outfit.LeftHand
    },
    new SettingsPlayer.Seasonal()
    {
      index = 97,
      outfit = Outfit.LeftHand
    },
    new SettingsPlayer.Seasonal()
    {
      index = 111,
      outfit = Outfit.Hair
    },
    new SettingsPlayer.Seasonal()
    {
      index = 153,
      outfit = Outfit.Hair
    },
    new SettingsPlayer.Seasonal()
    {
      index = 166,
      outfit = Outfit.RightHand
    }
  };
  public static List<SettingsPlayer.Seasonal> seasonThanksgiving = new List<SettingsPlayer.Seasonal>()
  {
    new SettingsPlayer.Seasonal()
    {
      index = 99,
      outfit = Outfit.Body
    },
    new SettingsPlayer.Seasonal()
    {
      index = 99,
      outfit = Outfit.Hair
    },
    new SettingsPlayer.Seasonal()
    {
      index = 115,
      outfit = Outfit.RightHand
    }
  };
  public static List<SettingsPlayer.Seasonal> seasonEaster = new List<SettingsPlayer.Seasonal>()
  {
    new SettingsPlayer.Seasonal()
    {
      index = 138,
      outfit = Outfit.Body
    },
    new SettingsPlayer.Seasonal()
    {
      index = 139,
      outfit = Outfit.Body
    },
    new SettingsPlayer.Seasonal()
    {
      index = 89,
      outfit = Outfit.Head
    },
    new SettingsPlayer.Seasonal()
    {
      index = 122,
      outfit = Outfit.LeftHand
    },
    new SettingsPlayer.Seasonal()
    {
      index = 149,
      outfit = Outfit.RightHand
    },
    new SettingsPlayer.Seasonal()
    {
      index = 153,
      outfit = Outfit.RightHand
    },
    new SettingsPlayer.Seasonal()
    {
      index = 143,
      outfit = Outfit.Hair
    },
    new SettingsPlayer.Seasonal()
    {
      index = 150,
      outfit = Outfit.Hair
    }
  };
  public static List<SettingsPlayer.Seasonal> seasonChristmas = new List<SettingsPlayer.Seasonal>()
  {
    new SettingsPlayer.Seasonal()
    {
      index = 141,
      outfit = Outfit.Body
    },
    new SettingsPlayer.Seasonal()
    {
      index = 142,
      outfit = Outfit.Body
    },
    new SettingsPlayer.Seasonal()
    {
      index = 156,
      outfit = Outfit.Hair
    },
    new SettingsPlayer.Seasonal()
    {
      index = 157,
      outfit = Outfit.Hair
    }
  };
  public static List<List<SettingsPlayer.ByRole>> LockedByAccountType = new List<List<SettingsPlayer.ByRole>>()
  {
    new List<SettingsPlayer.ByRole>()
    {
      new SettingsPlayer.ByRole(89, AccountType.Arcane_Monster),
      new SettingsPlayer.ByRole(SettingsPlayer.brine_body, AccountType.Lifetime)
    },
    new List<SettingsPlayer.ByRole>()
    {
      new SettingsPlayer.ByRole(74, AccountType.Arcane_Monster),
      new SettingsPlayer.ByRole(75, AccountType.Arcane_Monster),
      new SettingsPlayer.ByRole(SettingsPlayer.brine_head, AccountType.Lifetime)
    },
    new List<SettingsPlayer.ByRole>()
    {
      new SettingsPlayer.ByRole(67, AccountType.Arch_Donator),
      new SettingsPlayer.ByRole(91, AccountType.Arcane_Monster),
      new SettingsPlayer.ByRole(100, AccountType.Wiki_Staff),
      new SettingsPlayer.ByRole(102, AccountType.Third_Place),
      new SettingsPlayer.ByRole(103, AccountType.Second_Place),
      new SettingsPlayer.ByRole(104, AccountType.First_Place),
      new SettingsPlayer.ByRole(118, AccountType.Arch_Donator),
      new SettingsPlayer.ByRole(125, AccountType.Audio_Wizard)
    },
    new List<SettingsPlayer.ByRole>()
    {
      new SettingsPlayer.ByRole(66, AccountType.Arch_Donator),
      new SettingsPlayer.ByRole(89, AccountType.Asset_Creator),
      new SettingsPlayer.ByRole(95, AccountType.Booster),
      new SettingsPlayer.ByRole(97, AccountType.Tournament_Official),
      new SettingsPlayer.ByRole(103, AccountType.Booster),
      new SettingsPlayer.ByRole(104, AccountType.Donator),
      new SettingsPlayer.ByRole(105, AccountType.Donator),
      new SettingsPlayer.ByRole(108, AccountType.Arcane_Monster),
      new SettingsPlayer.ByRole(111, AccountType.Asset_Creator),
      new SettingsPlayer.ByRole(155, AccountType.Audio_Wizard),
      new SettingsPlayer.ByRole(SettingsPlayer.brine_hand, AccountType.Lifetime)
    },
    new List<SettingsPlayer.ByRole>()
    {
      new SettingsPlayer.ByRole(78, AccountType.Owner),
      new SettingsPlayer.ByRole(103, AccountType.Contributor),
      new SettingsPlayer.ByRole(104, AccountType.Imp),
      new SettingsPlayer.ByRole(105, AccountType.Mod),
      new SettingsPlayer.ByRole(107, AccountType.Arcane_Monster),
      new SettingsPlayer.ByRole(155, AccountType.Audio_Wizard),
      new SettingsPlayer.ByRole(159, AccountType.Lifetime)
    },
    new List<SettingsPlayer.ByRole>()
    {
      new SettingsPlayer.ByRole(69, AccountType.Arcane_Monster)
    }
  };
  public static List<List<SettingsPlayer.ByPrestige>> LockedByPrestige = new List<List<SettingsPlayer.ByPrestige>>()
  {
    new List<SettingsPlayer.ByPrestige>()
    {
      new SettingsPlayer.ByPrestige(133, 3),
      new SettingsPlayer.ByPrestige(136, 4),
      new SettingsPlayer.ByPrestige(143, 8)
    },
    new List<SettingsPlayer.ByPrestige>(),
    new List<SettingsPlayer.ByPrestige>()
    {
      new SettingsPlayer.ByPrestige(124, 7)
    },
    new List<SettingsPlayer.ByPrestige>()
    {
      new SettingsPlayer.ByPrestige(156, 7)
    },
    new List<SettingsPlayer.ByPrestige>()
    {
      new SettingsPlayer.ByPrestige(137, 9),
      new SettingsPlayer.ByPrestige(138, 9)
    },
    new List<SettingsPlayer.ByPrestige>()
    {
      new SettingsPlayer.ByPrestige(94, 8)
    }
  };
  public static List<List<SettingsPlayer.ByTournament>> LockedByTournament = new List<List<SettingsPlayer.ByTournament>>()
  {
    new List<SettingsPlayer.ByTournament>()
    {
      new SettingsPlayer.ByTournament(161, 1000)
    },
    new List<SettingsPlayer.ByTournament>()
    {
      new SettingsPlayer.ByTournament(100, 1000)
    },
    new List<SettingsPlayer.ByTournament>()
    {
      new SettingsPlayer.ByTournament(141, 1000),
      new SettingsPlayer.ByTournament(142, 1000)
    },
    new List<SettingsPlayer.ByTournament>()
    {
      new SettingsPlayer.ByTournament(67, 1000),
      new SettingsPlayer.ByTournament(68, 1000),
      new SettingsPlayer.ByTournament(70, 1000),
      new SettingsPlayer.ByTournament(77, 1000),
      new SettingsPlayer.ByTournament(88, 1000),
      new SettingsPlayer.ByTournament(123, 1000),
      new SettingsPlayer.ByTournament(157, 1000),
      new SettingsPlayer.ByTournament(158, 1000),
      new SettingsPlayer.ByTournament(159, 1000),
      new SettingsPlayer.ByTournament(160, 1000),
      new SettingsPlayer.ByTournament(161, 1000),
      new SettingsPlayer.ByTournament(162, 1000),
      new SettingsPlayer.ByTournament(163, 1000),
      new SettingsPlayer.ByTournament(165, 1000),
      new SettingsPlayer.ByTournament(180, 1000)
    },
    new List<SettingsPlayer.ByTournament>()
    {
      new SettingsPlayer.ByTournament(99, 1000)
    },
    new List<SettingsPlayer.ByTournament>()
    {
      new SettingsPlayer.ByTournament(62, 1000),
      new SettingsPlayer.ByTournament(64, 1000)
    }
  };
  public static List<List<SettingsPlayer.ByPrestige>> LockedByExperience = new List<List<SettingsPlayer.ByPrestige>>()
  {
    new List<SettingsPlayer.ByPrestige>()
    {
      new SettingsPlayer.ByPrestige(128, 1),
      new SettingsPlayer.ByPrestige(129, 4),
      new SettingsPlayer.ByPrestige(130, 7),
      new SettingsPlayer.ByPrestige(131, 10),
      new SettingsPlayer.ByPrestige(132, 12)
    },
    new List<SettingsPlayer.ByPrestige>()
    {
      new SettingsPlayer.ByPrestige(93, 10)
    },
    new List<SettingsPlayer.ByPrestige>(),
    new List<SettingsPlayer.ByPrestige>()
    {
      new SettingsPlayer.ByPrestige(150, 4),
      new SettingsPlayer.ByPrestige(151, 7),
      new SettingsPlayer.ByPrestige(152, 12)
    },
    new List<SettingsPlayer.ByPrestige>()
    {
      new SettingsPlayer.ByPrestige(147, 4),
      new SettingsPlayer.ByPrestige(148, 7),
      new SettingsPlayer.ByPrestige(158, 10),
      new SettingsPlayer.ByPrestige(149, 12)
    },
    new List<SettingsPlayer.ByPrestige>()
  };
  public static List<List<byte>> AlwaysUnlocked = new List<List<byte>>()
  {
    new List<byte>(),
    new List<byte>(),
    new List<byte>(),
    new List<byte>(),
    new List<byte>(),
    new List<byte>()
  };
  private static Dictionary<string, SettingsPlayer.ByDust> buyables = new Dictionary<string, SettingsPlayer.ByDust>();
  private static AchievementCosmetic _unlock;

  public byte fullBook
  {
    get => this._spells.fullBook;
    set => this._spells.fullBook = value;
  }

  public byte extraInfo
  {
    get => this._spells.extraInfo;
    set => this._spells.extraInfo = value;
  }

  public BookOf Elemental => (BookOf) ((int) this.fullBook - 1);

  public byte GetOutfitIndex(Outfit o)
  {
    switch (o)
    {
      case Outfit.Body:
        return this.indexBody;
      case Outfit.Head:
        return this.indexHead;
      case Outfit.LeftHand:
        return this.indexLeftHand;
      case Outfit.RightHand:
        return this.indexRightHand;
      case Outfit.Hair:
        return this.indexHat;
      case Outfit.Beard:
        return this.indexBeard;
      case Outfit.LeftFoot:
        return this.indexLeftFoot;
      case Outfit.RightFoot:
        return this.indexRightFoot;
      case Outfit.Mouth:
        return this.indexMouth;
      case Outfit.Beard2:
        return this.indexBeard2;
      case Outfit.Beard3:
        return this.indexBeard3;
      default:
        return 0;
    }
  }

  public void SetOutfitIndex(Outfit o, byte i)
  {
    switch (o)
    {
      case Outfit.Body:
        this.indexBody = i;
        break;
      case Outfit.Head:
        this.indexHead = i;
        break;
      case Outfit.LeftHand:
        this.indexLeftHand = i;
        break;
      case Outfit.RightHand:
        this.indexRightHand = i;
        break;
      case Outfit.Hair:
        this.indexHat = i;
        break;
      case Outfit.Beard:
        this.indexBeard = i;
        break;
      case Outfit.LeftFoot:
        this.indexLeftFoot = i;
        break;
      case Outfit.RightFoot:
        this.indexRightFoot = i;
        break;
      case Outfit.Mouth:
        this.indexMouth = i;
        break;
      case Outfit.Beard2:
        this.indexBeard2 = i;
        break;
      case Outfit.Beard3:
        this.indexBeard3 = i;
        break;
    }
  }

  public byte[] spells
  {
    get => this._spells.spells;
    set => this._spells.spells = value;
  }

  internal void Export(string fileName)
  {
    using (MemoryStream memoryStream = new MemoryStream())
    {
      using (myBinaryWriter w = new myBinaryWriter((Stream) memoryStream))
      {
        for (int index = 0; index < 11; ++index)
        {
          if ((UnityEngine.Object) this.textures[index] != (UnityEngine.Object) null)
          {
            w.Write(1);
            w.Write(this.customPieces[index]);
            this.metaData[index].Serialize(w);
            w.Write(this.textures[index].texture.EncodeToPNG());
            if (this.animations[index] != null)
              w.Write(this.animations[index].sprites[0].texture.EncodeToPNG());
            else
              w.Write(0);
          }
          else
            w.Write(0);
        }
      }
      string path = Application.persistentDataPath + Path.DirectorySeparatorChar.ToString() + "OutfitPackages" + Path.DirectorySeparatorChar.ToString();
      if (!Directory.Exists(path))
        Directory.CreateDirectory(path);
      File.WriteAllBytes(path + fileName + ".outfitPKG", memoryStream.ToArray());
      Global.OpenFileLocation(path);
    }
  }

  internal void Import(string file)
  {
    using (MemoryStream memoryStream = new MemoryStream(File.ReadAllBytes(file)))
    {
      this.MakeSureIntitialized();
      string persistentDataPath = Application.persistentDataPath;
      char directorySeparatorChar = Path.DirectorySeparatorChar;
      string str1 = directorySeparatorChar.ToString();
      directorySeparatorChar = Path.DirectorySeparatorChar;
      string str2 = directorySeparatorChar.ToString();
      string path = persistentDataPath + str1 + "_Temporary" + str2;
      if (!Directory.Exists(path))
        Directory.CreateDirectory(path);
      using (myBinaryReader r = new myBinaryReader((Stream) memoryStream))
      {
        StringBuilder stringBuilder = new StringBuilder();
        for (int index = 0; index < 11 && r.BaseStream.Position != r.BaseStream.Length; ++index)
        {
          if (r.ReadInt32() != 0)
          {
            string str3 = Path.GetFileNameWithoutExtension(r.ReadString());
            if (!str3.StartsWith("temp_"))
              str3 = "temp_" + str3;
            this.customPieces[index] = path + str3 + ".png";
            this.metaData[index] = SettingsPlayer.MetaData.Deserialize(r).meta;
            byte[] numArray = r.ReadBytes();
            Texture2D texture2D = new Texture2D(2, 2);
            texture2D.LoadImage(numArray);
            this.textures[index] = Global.AddSprite(Sprite.Create(texture2D, new Rect(0.0f, 0.0f, (float) texture2D.width, (float) texture2D.height), this.metaData[index].pivot, 2f));
            File.WriteAllBytes(this.customPieces[index], numArray);
            this.metaData[index].SerializeToFile(this.customPieces[index]);
            int count = r.ReadInt32();
            if (count != 0)
            {
              byte[] bytes = r.ReadBytes(count);
              this.animations[index] = CharacterCreation.GenerateAnim(bytes, texture2D, this.metaData[index]);
              File.WriteAllBytes(path + str3 + "_effect.png", bytes);
              stringBuilder.Append(((Outfit) index).ToString()).Append(": ").Append((object) this.metaData[index].pivot).Append("\n");
            }
          }
        }
        File.WriteAllText(path + "meta.txt", stringBuilder.ToString());
      }
    }
  }

  internal bool CustomContainsSomething()
  {
    if (this.customPieces == null)
      return false;
    for (int index = 0; index < 11; ++index)
    {
      if (!string.IsNullOrWhiteSpace(this.customPieces[index]))
        return true;
    }
    return false;
  }

  internal void UpdatePivot(Outfit o, Vector2 p, SpriteRenderer sr)
  {
    int index1 = (int) o;
    Texture2D texture = this.textures[index1].texture;
    this.textures[index1] = Global.AddSprite(Sprite.Create(texture, new Rect(0.0f, 0.0f, (float) texture.width, (float) texture.height), p, 2f));
    sr.sprite = Global.AddSprite(Sprite.Create(sr.sprite.texture, new Rect(0.0f, 0.0f, (float) texture.width, (float) texture.height), p, 2f));
    this.metaData[index1].pivot = p;
    if (this.animations[index1] != null)
    {
      for (int index2 = 0; index2 < this.animations[index1].sprites.Count; ++index2)
        this.animations[index1].sprites[index2] = Global.AddSprite(Sprite.Create(this.animations[index1].sprites[index2].texture, this.animations[index1].sprites[index2].rect, p, 2f));
    }
    foreach (SpriteRenderer componentsInChild in sr.GetComponentsInChildren<SpriteRenderer>())
    {
      if ((UnityEngine.Object) componentsInChild != (UnityEngine.Object) sr)
      {
        AnimateRepeat component = componentsInChild.GetComponent<AnimateRepeat>();
        if ((UnityEngine.Object) component != (UnityEngine.Object) null)
        {
          for (int index3 = 0; index3 < component.sprites.Length; ++index3)
            component.sprites[index3] = Global.AddSprite(Sprite.Create(component.sprites[index3].texture, component.sprites[index3].rect, p, 2f));
        }
      }
    }
    this.metaData[index1].SerializeToFile(this.customPieces[index1]);
  }

  public void MakeSureIntitialized()
  {
    if (this.customPieces == null)
      this.customPieces = new string[11];
    if (this.textures != null)
      return;
    this.textures = new Sprite[11];
    this.animations = new SettingsPlayer.CustomAnim[11];
    this.metaData = new SettingsPlayer.MetaData[11];
    for (int index = 0; index < 11; ++index)
      this.metaData[index] = new SettingsPlayer.MetaData();
  }

  public void Reset(Outfit o)
  {
    switch (o)
    {
      case Outfit.Body:
        this.indexBody = (byte) 0;
        break;
      case Outfit.Head:
        this.indexHead = (byte) 0;
        break;
      case Outfit.LeftHand:
        this.indexLeftHand = (byte) 0;
        break;
      case Outfit.RightHand:
        this.indexRightHand = (byte) 0;
        break;
      case Outfit.Hair:
        this.indexHat = (byte) 0;
        break;
      case Outfit.Beard:
        this.indexBeard = (byte) 0;
        break;
      case Outfit.LeftFoot:
        this.indexLeftFoot = (byte) 0;
        break;
      case Outfit.RightFoot:
        this.indexRightFoot = (byte) 0;
        break;
      case Outfit.Beard2:
        this.indexBeard2 = (byte) 0;
        break;
      case Outfit.Beard3:
        this.indexBeard3 = (byte) 0;
        break;
    }
  }

  public void ResetAll()
  {
    this.indexMouth = (byte) 0;
    this.indexLeftFoot = (byte) 0;
    this.indexRightFoot = (byte) 0;
    for (int o = 0; o < 11; ++o)
      this.ResetCustom((Outfit) o);
  }

  public void ResetCustom(Outfit o)
  {
    int index = (int) o;
    if (this.customPieces == null)
      return;
    this.customPieces[index] = (string) null;
    if ((UnityEngine.Object) this.textures[index] != (UnityEngine.Object) null)
    {
      Global.DestroySprite(this.textures[index]);
      this.textures[index] = (Sprite) null;
    }
    this.ResetAnimation(o);
  }

  public void ResetAnimation(Outfit o)
  {
    int index = (int) o;
    if (this.animations[index] == null)
      return;
    foreach (Sprite sprite in this.animations[index].sprites)
      Global.DestroySprite(sprite);
    this.animations[index] = (SettingsPlayer.CustomAnim) null;
  }

  public static SettingsPlayer DefaultSettings()
  {
    SettingsPlayer settingsPlayer = new SettingsPlayer();
    settingsPlayer.coloring = new Recolor();
    for (int index = 0; index < settingsPlayer.spells.Length; ++index)
      settingsPlayer.spells[index] = byte.MaxValue;
    return settingsPlayer;
  }

  public static SettingsPlayer Load(string path)
  {
    SettingsPlayer settingsPlayer = new SettingsPlayer();
    if (File.Exists(path))
    {
      try
      {
        using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.None))
        {
          using (myBinaryReader r = new myBinaryReader((Stream) fileStream))
            settingsPlayer.Deserialize(r);
        }
      }
      catch (Exception ex)
      {
        settingsPlayer = new SettingsPlayer();
      }
    }
    return settingsPlayer;
  }

  public void RemoveSpellSlot(int i)
  {
    for (; i < 15 && this.spells[i + 1] < byte.MaxValue; ++i)
      this.spells[i] = this.spells[i + 1];
    this.spells[i] = byte.MaxValue;
  }

  public void Realign()
  {
    int index1 = 0;
    bool flag = false;
    for (int index2 = 0; index2 < 16; ++index2)
    {
      if (this.spells[index2] < byte.MaxValue)
      {
        if (flag)
        {
          this.spells[index1] = this.spells[index2];
          this.spells[index2] = byte.MaxValue;
          ++index1;
        }
        else
          ++index1;
      }
      else
        flag = true;
    }
  }

  public void VerifySpells()
  {
    HashSet<byte> byteSet = new HashSet<byte>();
    int[] numArray = new int[(int) (RandomExtensions.LastBook() + 1)];
    bool flag = false;
    for (int index1 = 0; index1 < 16; ++index1)
    {
      byte spell = this.spells[index1];
      int index2 = (int) spell / 12;
      if (index2 < 0 || index2 >= numArray.Length)
      {
        this.spells[index1] = byte.MaxValue;
        flag = true;
      }
      else
      {
        int num = (int) spell % 12;
        if (num < 10)
        {
          if (num % 2 == 1 && spell >= (byte) 12 && (index1 == 0 || (int) this.spells[index1 - 1] != (int) spell - 1))
          {
            this.spells[index1] = byte.MaxValue;
            flag = true;
            continue;
          }
        }
        else if (index2 > 0 && numArray[index2] < 5)
        {
          this.spells[index1] = byte.MaxValue;
          flag = true;
          continue;
        }
        if (byteSet.Contains(spell))
        {
          this.spells[index1] = byte.MaxValue;
          flag = true;
        }
        else
        {
          ++numArray[index2];
          byteSet.Add(spell);
        }
      }
    }
    if (!flag)
      return;
    this.Realign();
  }

  private void DefaultSpells()
  {
    this.spells = new byte[16]
    {
      (byte) 0,
      (byte) 1,
      (byte) 2,
      (byte) 3,
      (byte) 4,
      (byte) 5,
      (byte) 6,
      (byte) 7,
      (byte) 8,
      (byte) 9,
      (byte) 10,
      (byte) 11,
      (byte) 12,
      (byte) 13,
      (byte) 14,
      (byte) 15
    };
  }

  public bool CanEquipSpell(int index, byte spellID)
  {
    if (index < 0 || index >= 16 || Inert.Instance._spells.Length <= (int) spellID)
      return false;
    for (int index1 = 0; index1 < 16; ++index1)
    {
      if ((int) this.spells[index1] == (int) spellID && index != index1)
        return false;
    }
    KeyValuePair<string, Spell> keyValuePair = Inert.Instance.spells.GetItem((int) spellID);
    Spell spell = keyValuePair.Value;
    if (spell.level == 1)
      return true;
    if (spell.level == 2)
    {
      byte num = (byte) ((uint) spellID - 1U);
      for (int index2 = 0; index2 < 16; ++index2)
      {
        if ((int) this.spells[index2] == (int) num)
          return true;
      }
    }
    else if (spell.level == 3)
    {
      int num = 0;
      for (int index3 = 0; index3 < index; ++index3)
      {
        if ((int) this.spells[index3] < Inert.Instance._spells.Length)
        {
          keyValuePair = Inert.Instance.spells.GetItem((int) this.spells[index3]);
          if (keyValuePair.Value.bookOf == spell.bookOf)
          {
            keyValuePair = Inert.Instance.spells.GetItem((int) this.spells[index3]);
            if (keyValuePair.Value.level < 3)
            {
              ++num;
              if (num >= 5)
                return true;
            }
          }
        }
      }
    }
    else
      Debug.LogError((object) ("Unknown Spell Level: " + (object) spell.level));
    return false;
  }

  public void CopyOutfit(SettingsPlayer b)
  {
    this.indexHead = b.indexHead;
    this.indexBeard = b.indexBeard;
    this.indexBeard2 = b.indexBeard2;
    this.indexBeard3 = b.indexBeard3;
    this.indexHat = b.indexHat;
    this.indexBody = b.indexBody;
    this.indexLeftHand = b.indexLeftHand;
    this.indexRightHand = b.indexRightHand;
    for (int index = 0; index < this.coloring.colors.Length; ++index)
      this.coloring.colors[index].Copy(b.coloring.colors[index]);
    this.custom = b.CustomContainsSomething() ? (byte) 1 : (byte) 0;
    if (this.custom != (byte) 0)
    {
      this.indexMouth = b.indexMouth;
      this.indexLeftFoot = b.indexLeftFoot;
      this.indexRightFoot = b.indexRightFoot;
      this.MakeSureIntitialized();
      for (int index = 0; index < 11; ++index)
      {
        this.customPieces[index] = b.customPieces[index];
        this.textures[index] = b.textures[index];
        this.metaData[index] = b.metaData[index];
        this.animations[index] = b.animations[index];
      }
    }
    else
    {
      this.indexMouth = (byte) 0;
      this.indexLeftFoot = (byte) 0;
      this.indexRightFoot = (byte) 0;
      this.ResetAll();
    }
  }

  public void CopySpells(SpellsOnly b)
  {
    this.fullBook = b.fullBook;
    this.extraInfo = b.extraInfo;
    for (int index = 0; index < 16; ++index)
      this.spells[index] = b.spells[index];
  }

  public void CopySpells(SettingsPlayer b, bool force = false)
  {
    if (b.fullBook > (byte) 0 | force)
      this.fullBook = b.fullBook;
    this.extraInfo = b.extraInfo;
    for (int index = 0; index < 16; ++index)
      this.spells[index] = b.spells[index];
  }

  public void SaveAs(string s, bool spells)
  {
    string persistentDataPath = Application.persistentDataPath;
    char directorySeparatorChar = Path.DirectorySeparatorChar;
    string str1 = directorySeparatorChar.ToString();
    string str2 = spells ? "SavedSpells" : "SavedOutfits";
    directorySeparatorChar = Path.DirectorySeparatorChar;
    string str3 = directorySeparatorChar.ToString();
    string path1 = persistentDataPath + str1 + str2 + str3;
    Global.CheckDirectoryExists(path1, s);
    string path2 = path1 + s + (spells ? ".spellBook" : ".outfit");
    if (string.IsNullOrEmpty(Path.GetFileNameWithoutExtension(path2)))
      return;
    using (FileStream fileStream = new FileStream(path2, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None))
    {
      using (myBinaryWriter w = new myBinaryWriter((Stream) fileStream))
        this.Serialize(w);
    }
  }

  public void SerializeBasicOutfit(myBinaryWriter w)
  {
    w.Write(this.indexHead);
    w.Write(this.indexBeard);
    w.Write(this.indexBeard2);
    w.Write(this.indexBeard3);
    w.Write(this.indexHat);
    w.Write(this.indexBody);
    w.Write(this.indexLeftHand);
    w.Write(this.indexRightHand);
    this.coloring.Serialize(w);
  }

  public void DeserializeBasicOutfit(myBinaryReader r)
  {
    this.indexHead = r.ReadByte();
    this.indexBeard = r.ReadByte();
    this.indexBeard2 = r.ReadByte();
    this.indexBeard3 = r.ReadByte();
    this.indexHat = r.ReadByte();
    this.indexBody = r.ReadByte();
    this.indexLeftHand = r.ReadByte();
    this.indexRightHand = r.ReadByte();
    this.coloring.Deserialize(r);
  }

  public void Serialize(myBinaryWriter w)
  {
    w.Write((byte) 4);
    w.Write(this.indexHead);
    w.Write(this.indexBeard);
    w.Write(this.indexBeard2);
    w.Write(this.indexBeard3);
    w.Write(this.indexHat);
    w.Write(this.indexBody);
    w.Write(this.indexLeftHand);
    w.Write(this.indexRightHand);
    for (int index = 0; index < 16; ++index)
      w.Write(this.spells[index]);
    this.coloring.Serialize(w);
    w.Write(this.fullBook);
    w.Write(this.extraInfo);
    if (this.customPieces == null || this.customPieces.Length != 11 || !this.CustomContainsSomething())
      this.custom = (byte) 0;
    w.Write(this.custom);
    if (this.custom == (byte) 0)
      return;
    w.Write(this.indexMouth);
    w.Write(this.indexLeftFoot);
    w.Write(this.indexRightFoot);
    w.Write((byte) 11);
    for (int index = 0; index < 11; ++index)
      w.Write(this.customPieces[index] ?? "");
  }

  public void Deserialize(myBinaryReader r)
  {
    byte num1 = r.ReadByte();
    this.indexHead = r.ReadByte();
    this.indexBeard = r.ReadByte();
    if (num1 > (byte) 3)
    {
      this.indexBeard2 = r.ReadByte();
      this.indexBeard3 = r.ReadByte();
    }
    else
    {
      this.indexBeard2 = (byte) 0;
      this.indexBeard3 = (byte) 0;
    }
    this.indexHat = r.ReadByte();
    this.indexBody = r.ReadByte();
    this.indexLeftHand = r.ReadByte();
    this.indexRightHand = r.ReadByte();
    for (int index = 0; index < 16; ++index)
      this.spells[index] = r.ReadByte();
    if (num1 <= (byte) 1)
    {
      byte a1 = r.ReadByte();
      byte a2 = r.ReadByte();
      byte a3 = r.ReadByte();
      byte a4 = r.ReadByte();
      if ((UnityEngine.Object) ClientResources.Instance != (UnityEngine.Object) null)
      {
        this.coloring.colors[0].red = (Color32) ClientResources.Instance.colorsMain[Mathf.Min((int) a1, ClientResources.Instance.colorsMain.Length - 1)];
        this.coloring.colors[0].green = (Color32) ClientResources.Instance.colorsSecondary[Mathf.Min((int) a2, ClientResources.Instance.colorsSecondary.Length - 1)];
        this.coloring.colors[0].blue = (Color32) ClientResources.Instance.colorsHair[Mathf.Min((int) a3, ClientResources.Instance.colorsHair.Length - 1)];
        this.coloring.colors[0].gray = (Color32) ClientResources.Instance.colorSkin[Mathf.Min((int) a4, ClientResources.Instance.colorSkin.Length - 1)];
        for (int index = 1; index < this.coloring.colors.Length; ++index)
        {
          this.coloring.colors[index].red = this.coloring.colors[0].red;
          this.coloring.colors[index].green = this.coloring.colors[0].green;
          this.coloring.colors[index].blue = this.coloring.colors[0].blue;
          this.coloring.colors[index].gray = this.coloring.colors[0].gray;
        }
      }
      if (num1 > (byte) 0)
        this.fullBook = r.ReadByte();
      this.ResetAll();
    }
    else
    {
      this.coloring.Deserialize(r);
      this.fullBook = r.ReadByte();
      this.extraInfo = num1 <= (byte) 2 ? (byte) 0 : r.ReadByte();
      byte num2 = r.ReadByte();
      if (num2 != (byte) 0)
      {
        this.custom = num2;
        this.indexMouth = r.ReadByte();
        this.indexLeftFoot = r.ReadByte();
        this.indexRightFoot = r.ReadByte();
        this.MakeSureIntitialized();
        int num3 = (int) r.ReadByte();
        for (int index = 0; index < num3; ++index)
        {
          if (index < 11)
            this.customPieces[index] = r.ReadString();
        }
      }
      else
        this.ResetAll();
    }
  }

  public void ReseralizeSpells(Connection c)
  {
    using (MemoryStream memoryStream = new MemoryStream())
    {
      using (myBinaryWriter w = new myBinaryWriter((Stream) memoryStream))
      {
        w.Write((byte) 44);
        this.Serialize(w);
      }
      c.SendBytes(memoryStream.ToArray());
    }
  }

  public byte FootIndex()
  {
    if ((int) this.indexBody == SettingsPlayer.sno_body4)
      return 2;
    if ((int) this.indexBody == SettingsPlayer.brine_body)
      return 3;
    if ((int) this.indexBody == SettingsPlayer.dharok_body)
      return 4;
    if (this.indexBody == (byte) 46)
      return 5;
    if (this.indexBody == (byte) 98 || this.indexBody == (byte) 140)
      return 6;
    if (this.indexBody == (byte) 93)
      return 7;
    if (this.indexBody == (byte) 101)
      return 8;
    if (this.indexBody == (byte) 142)
      return 9;
    if (this.indexBody == (byte) 152 || this.indexBody == (byte) 153)
      return 5;
    return this.indexBody == (byte) 161 ? (byte) 10 : (byte) 0;
  }

  public static bool HeadNoMouth(string name, SettingsPlayer sp)
  {
    byte indexHead = sp.indexHead;
    if ((int) indexHead == (int) SettingsPlayer.clanOutfit[1])
    {
      ClanOufit clanOutfit = ClientResources.Instance.GetClanOutfit(Client.GetAccount(name).clan);
      if (clanOutfit != null && clanOutfit.outfits != null && clanOutfit.outfits[1] != null && clanOutfit.outfits[1].effect == (byte) 1)
        return true;
    }
    return SettingsPlayer.HeadNoMouth(sp.indexBeard) || SettingsPlayer.HeadNoMouth(sp.indexBeard2) || SettingsPlayer.HeadNoMouth(sp.indexBeard3) || (int) indexHead == SettingsPlayer.sno_head || (int) indexHead == SettingsPlayer.sno_head2 || (int) indexHead == SettingsPlayer.sno_head3 || (int) indexHead == SettingsPlayer.sno_head4 || indexHead == (byte) 76 || indexHead == (byte) 77 || indexHead == (byte) 78 || indexHead == (byte) 94 || (int) sp.indexHead == SettingsPlayer.brine_head || sp.indexHead == (byte) 69 || sp.indexHead == (byte) 71 || sp.indexHead == (byte) 81 || (int) sp.indexBody == SettingsPlayer.sno_body2 || indexHead == (byte) 73 || indexHead == (byte) 92 || indexHead == (byte) 99 || indexHead == (byte) 100;
  }

  private static bool HeadNoMouth(byte i)
  {
    return i == (byte) 55 || i == (byte) 72 || i == (byte) 77 || i == (byte) 53 || i == (byte) 65 || i == (byte) 69 || i == (byte) 94 || i == (byte) 99;
  }

  public void VerifyOutfit(Cosmetics cosmetics, Account a = null)
  {
    if (a == null)
      a = Client.GetAccount(Client.Name);
    if (!SettingsPlayer.VerifyOutfit(a, cosmetics, a.accountType, Outfit.Body, (int) this.indexBody))
      this.indexBody = (byte) 0;
    if (!SettingsPlayer.VerifyOutfit(a, cosmetics, a.accountType, Outfit.Head, (int) this.indexHead))
      this.indexHead = (byte) 0;
    if (!SettingsPlayer.VerifyOutfit(a, cosmetics, a.accountType, Outfit.LeftHand, (int) this.indexLeftHand))
      this.indexLeftHand = (byte) 0;
    if (!SettingsPlayer.VerifyOutfit(a, cosmetics, a.accountType, Outfit.RightHand, (int) this.indexRightHand))
      this.indexRightHand = (byte) 0;
    if (!SettingsPlayer.VerifyOutfit(a, cosmetics, a.accountType, Outfit.Hair, (int) this.indexHat))
      this.indexHat = (byte) 0;
    if (!SettingsPlayer.VerifyOutfit(a, cosmetics, a.accountType, Outfit.Beard, (int) this.indexBeard))
      this.indexBeard = (byte) 0;
    if (!a.accountType.has(AccountType.Donator | AccountType.Owner) || !SettingsPlayer.VerifyOutfit(a, cosmetics, a.accountType, Outfit.Beard, (int) this.indexBeard2))
      this.indexBeard2 = (byte) 0;
    if (a.accountType.has(AccountType.Arch_Donator | AccountType.Owner) && SettingsPlayer.VerifyOutfit(a, cosmetics, a.accountType, Outfit.Beard, (int) this.indexBeard3))
      return;
    this.indexBeard3 = (byte) 0;
  }

  public void VerifyColors()
  {
  }

  public static Sprite[] GetOutfits(int o)
  {
    switch (o)
    {
      case 0:
        return ClientResources.Instance._characterBody;
      case 1:
        return ClientResources.Instance._characterHeads;
      case 2:
        return ClientResources.Instance._characterLeftHand;
      case 3:
        return ClientResources.Instance._characterRightHand;
      case 4:
        return ClientResources.Instance._characterHats;
      case 5:
        return ClientResources.Instance._characterBeards;
      default:
        Debug.LogError((object) ("Invalid index " + (object) o));
        return ClientResources.Instance._characterBody;
    }
  }

  public static void InitUnlockables(Cosmetics def)
  {
    byte index1 = 0;
    foreach (List<SettingsPlayer.ByAchievement> byAchievementList in SettingsPlayer.LockedByAchievement)
    {
      foreach (SettingsPlayer.ByAchievement byAchievement in byAchievementList)
        def.array[(int) index1][byAchievement.index] = false;
      ++index1;
    }
    byte index2 = 0;
    foreach (List<SettingsPlayer.ByRole> byRoleList in SettingsPlayer.LockedByAccountType)
    {
      foreach (SettingsPlayer.ByRole byRole in byRoleList)
        def.array[(int) index2][byRole.index] = false;
      ++index2;
    }
    byte index3 = 0;
    foreach (List<SettingsPlayer.ByDust> byDustList in SettingsPlayer.LockedByDust)
    {
      foreach (SettingsPlayer.ByDust byDust in byDustList)
      {
        byDust.outfitType = (Outfit) index3;
        SettingsPlayer.buyables.Add(byDust.name, byDust);
        def.array[(int) index3][byDust.index] = false;
      }
      ++index3;
    }
    byte index4 = 0;
    foreach (List<SettingsPlayer.ByPrestige> byPrestigeList in SettingsPlayer.LockedByPrestige)
    {
      foreach (SettingsPlayer.ByPrestige byPrestige in byPrestigeList)
        def.array[(int) index4][byPrestige.index] = false;
      ++index4;
    }
    byte index5 = 0;
    foreach (List<SettingsPlayer.ByPrestige> byPrestigeList in SettingsPlayer.LockedByExperience)
    {
      foreach (SettingsPlayer.ByPrestige byPrestige in byPrestigeList)
        def.array[(int) index5][byPrestige.index] = false;
      ++index5;
    }
    byte index6 = 0;
    foreach (List<SettingsPlayer.ByTournament> byTournamentList in SettingsPlayer.LockedByTournament)
    {
      foreach (SettingsPlayer.ByTournament byTournament in byTournamentList)
        def.array[(int) index6][byTournament.index] = false;
      ++index6;
    }
    foreach (SettingsPlayer.Seasonal seasonal in SettingsPlayer.seasonHalloween)
      def.array[(int) seasonal.outfit][seasonal.index] = false;
    foreach (SettingsPlayer.Seasonal seasonal in SettingsPlayer.seasonThanksgiving)
      def.array[(int) seasonal.outfit][seasonal.index] = false;
    foreach (SettingsPlayer.Seasonal seasonal in SettingsPlayer.seasonEaster)
      def.array[(int) seasonal.outfit][seasonal.index] = false;
    foreach (SettingsPlayer.Seasonal seasonChristma in SettingsPlayer.seasonChristmas)
      def.array[(int) seasonChristma.outfit][seasonChristma.index] = false;
  }

  public static void Disable(Cosmetics def)
  {
    def.LockRest();
    byte index1 = 0;
    foreach (List<SettingsPlayer.ByAchievement> byAchievementList in SettingsPlayer.LockedByAchievement)
    {
      foreach (SettingsPlayer.ByAchievement byAchievement in byAchievementList)
        def.array[(int) index1][byAchievement.index] = false;
      ++index1;
    }
    byte index2 = 0;
    foreach (List<SettingsPlayer.ByRole> byRoleList in SettingsPlayer.LockedByAccountType)
    {
      foreach (SettingsPlayer.ByRole byRole in byRoleList)
        def.array[(int) index2][byRole.index] = false;
      ++index2;
    }
    byte index3 = 0;
    foreach (List<SettingsPlayer.ByTournament> byTournamentList in SettingsPlayer.LockedByTournament)
    {
      foreach (SettingsPlayer.ByTournament byTournament in byTournamentList)
        def.array[(int) index3][byTournament.index] = false;
      ++index3;
    }
    byte index4 = 0;
    foreach (List<SettingsPlayer.ByPrestige> byPrestigeList in SettingsPlayer.LockedByExperience)
    {
      foreach (SettingsPlayer.ByPrestige byPrestige in byPrestigeList)
        def.array[(int) index4][byPrestige.index] = false;
      ++index4;
    }
    byte index5 = 0;
    foreach (List<SettingsPlayer.ByPrestige> byPrestigeList in SettingsPlayer.LockedByPrestige)
    {
      foreach (SettingsPlayer.ByPrestige byPrestige in byPrestigeList)
        def.array[(int) index5][byPrestige.index] = false;
      ++index5;
    }
  }

  public static Achievement CheckAchievements(Outfit type, int index)
  {
    foreach (SettingsPlayer.ByAchievement byAchievement in SettingsPlayer.LockedByAchievement[(int) type])
    {
      if (byAchievement.index == index)
        return byAchievement.a;
    }
    return Achievement.None;
  }

  public static AccountType CheckAccountType(Outfit type, int index)
  {
    foreach (SettingsPlayer.ByRole byRole in SettingsPlayer.LockedByAccountType[(int) type])
    {
      if (byRole.index == index)
        return byRole.a;
    }
    return AccountType.None;
  }

  public static bool CheckAlwaysUnlocked(Outfit type, int index)
  {
    foreach (int num in SettingsPlayer.AlwaysUnlocked[(int) type])
    {
      if (num == index)
        return true;
    }
    return false;
  }

  public static int CheckPrestige(Outfit type, int index)
  {
    foreach (SettingsPlayer.ByPrestige byPrestige in SettingsPlayer.LockedByPrestige[(int) type])
    {
      if (byPrestige.index == index)
        return byPrestige.prestige;
    }
    return 0;
  }

  public static int CheckExperience(Outfit type, int index)
  {
    foreach (SettingsPlayer.ByPrestige byPrestige in SettingsPlayer.LockedByExperience[(int) type])
    {
      if (byPrestige.index == index)
        return byPrestige.prestige;
    }
    return 0;
  }

  public static int CheckTournament(Outfit type, int index)
  {
    foreach (SettingsPlayer.ByTournament byTournament in SettingsPlayer.LockedByTournament[(int) type])
    {
      if (byTournament.index == index)
        return byTournament.coins;
    }
    return 0;
  }

  public static int ClientHasAchievement(Outfit type, int index)
  {
    if (SettingsPlayer.CheckAlwaysUnlocked(type, index) || Client.MyAccount.accountType.has(AccountType.Owner | AccountType.Press_Account))
      return 1;
    Achievement index1 = SettingsPlayer.CheckAchievements(type, index);
    if (index1 != Achievement.None && Client.cosmetics.achievements[(int) index1])
      return 1;
    Account account = Client.GetAccount(Client.Name);
    AccountType accountType = SettingsPlayer.CheckAccountType(type, index);
    if ((account.accountType & accountType) != AccountType.None)
      return 1;
    int num1 = SettingsPlayer.CheckPrestige(type, index);
    if (num1 > 0)
      return (int) account.prestige < num1 ? 0 : 1;
    int num2 = SettingsPlayer.CheckExperience(type, index);
    return num2 > 0 ? ((int) account.experience < num2 ? 0 : 1) : (SettingsPlayer.CheckTournament(type, index) > 0 || SettingsPlayer.seasonHalloween.Find((Predicate<SettingsPlayer.Seasonal>) (z => z.outfit == type && z.index == index)) != null || SettingsPlayer.seasonThanksgiving.Find((Predicate<SettingsPlayer.Seasonal>) (z => z.outfit == type && z.index == index)) != null || SettingsPlayer.seasonEaster.Find((Predicate<SettingsPlayer.Seasonal>) (z => z.outfit == type && z.index == index)) != null || SettingsPlayer.seasonChristmas.Find((Predicate<SettingsPlayer.Seasonal>) (z => z.outfit == type && z.index == index)) != null || accountType != AccountType.None || index1 != Achievement.None ? 0 : -1);
  }

  public static bool IsSeasonal(Outfit type, int index)
  {
    return SettingsPlayer.seasonHalloween.Find((Predicate<SettingsPlayer.Seasonal>) (z => z.outfit == type && z.index == index)) != null || SettingsPlayer.seasonThanksgiving.Find((Predicate<SettingsPlayer.Seasonal>) (z => z.outfit == type && z.index == index)) != null || SettingsPlayer.seasonEaster.Find((Predicate<SettingsPlayer.Seasonal>) (z => z.outfit == type && z.index == index)) != null || SettingsPlayer.seasonChristmas.Find((Predicate<SettingsPlayer.Seasonal>) (z => z.outfit == type && z.index == index)) != null;
  }

  public static SettingsPlayer.ByDust GetBuyable(string name)
  {
    SettingsPlayer.ByDust buyable;
    SettingsPlayer.buyables.TryGetValue(name, out buyable);
    return buyable;
  }

  public static bool VerifyOutfit(
    Account acc,
    Cosmetics c,
    AccountType a,
    Outfit type,
    int index)
  {
    if (c.array[(int) type][index] || SettingsPlayer.CheckAlwaysUnlocked(type, index) || acc.accountType.has(AccountType.Owner | AccountType.Press_Account))
      return true;
    Achievement index1 = SettingsPlayer.CheckAchievements(type, index);
    if (index1 != Achievement.None && c.achievements[(int) index1] || (a & SettingsPlayer.CheckAccountType(type, index)) != AccountType.None)
      return true;
    int num1 = SettingsPlayer.CheckPrestige(type, index);
    if (num1 > 0 && (int) acc.prestige >= num1)
      return true;
    int num2 = SettingsPlayer.CheckExperience(type, index);
    return num2 > 0 && (int) acc.experience >= num2;
  }

  public class CustomAnim
  {
    public List<Sprite> sprites = new List<Sprite>();
  }

  public class MetaData
  {
    public const byte _version = 1;
    public float fps = 12f;
    public Vector2 pivot = new Vector2(0.5f, 0.5f);
    internal bool hasEffect;
    private static SemaphoreSlim semaphoreSlim = new SemaphoreSlim(1, 1);
    private static Dictionary<string, SettingsPlayer.MetaData> metaQueue = new Dictionary<string, SettingsPlayer.MetaData>();

    public static (SettingsPlayer.MetaData meta, bool exist) Deserialize(myBinaryReader r)
    {
      int num = (int) r.ReadByte();
      SettingsPlayer.MetaData metaData = new SettingsPlayer.MetaData();
      if (num == 0)
        return (metaData, false);
      metaData.fps = r.ReadSingle();
      metaData.pivot = r.ReadVector2();
      return (metaData, true);
    }

    public static (SettingsPlayer.MetaData meta, bool exist) Deserialize(string file)
    {
      string persistentDataPath = Application.persistentDataPath;
      char directorySeparatorChar = Path.DirectorySeparatorChar;
      string str1 = directorySeparatorChar.ToString();
      directorySeparatorChar = Path.DirectorySeparatorChar;
      string str2 = directorySeparatorChar.ToString();
      string path = persistentDataPath + str1 + "meta" + str2;
      if (!Directory.Exists(path))
        Directory.CreateDirectory(path);
      file = path + Path.GetFileName(file);
      if (File.Exists(file))
      {
        try
        {
          using (MemoryStream memoryStream = new MemoryStream(File.ReadAllBytes(file)))
          {
            using (myBinaryReader r = new myBinaryReader((Stream) memoryStream))
              return SettingsPlayer.MetaData.Deserialize(r);
          }
        }
        catch (Exception ex)
        {
        }
      }
      return (new SettingsPlayer.MetaData(), false);
    }

    public async void SerializeToFile(string file)
    {
      SettingsPlayer.MetaData metaData = this;
      string path = Application.persistentDataPath + Path.DirectorySeparatorChar.ToString() + "meta" + Path.DirectorySeparatorChar.ToString();
      if (!Directory.Exists(path))
        Directory.CreateDirectory(path);
      file = path + Path.GetFileName(file) + ".meta";
      SettingsPlayer.MetaData.metaQueue[file] = metaData;
      await SettingsPlayer.MetaData.semaphoreSlim.WaitAsync();
      try
      {
        if (!SettingsPlayer.MetaData.metaQueue.ContainsKey(file))
          return;
        SettingsPlayer.MetaData meta = SettingsPlayer.MetaData.metaQueue[file];
        SettingsPlayer.MetaData.metaQueue.Remove(file);
        using (MemoryStream m = new MemoryStream())
        {
          using (myBinaryWriter w = new myBinaryWriter((Stream) m))
            meta.Serialize(w);
          byte[] array = m.ToArray();
          using (FileStream f = new FileStream(file, FileMode.Create, FileAccess.Write, FileShare.ReadWrite, array.Length, true))
            await f.WriteAsync(array, 0, array.Length);
        }
      }
      finally
      {
        SettingsPlayer.MetaData.semaphoreSlim.Release();
      }
    }

    public void Serialize(myBinaryWriter w)
    {
      w.Write((byte) 1);
      w.Write(this.fps);
      w.Write(this.pivot);
    }
  }

  public class Seasonal
  {
    public int index;
    public Outfit outfit;
  }

  public class ByRole
  {
    public int index;
    public AccountType a;

    public ByRole(int index, AccountType a)
    {
      this.index = index;
      this.a = a;
    }
  }

  public class ByPrestige
  {
    public int index;
    public int prestige;

    public ByPrestige(int index, int a)
    {
      this.index = index;
      this.prestige = a;
    }
  }

  public class ByTournament
  {
    public int index;
    public int coins;

    public ByTournament(int index, int a)
    {
      this.index = index;
      this.coins = a;
    }
  }

  public class ByAchievement
  {
    public int index;
    public Achievement a;

    public ByAchievement(int index, Achievement a)
    {
      this.index = index;
      this.a = a;
    }
  }

  public class ByReason
  {
    public int index;
    public string a;

    public ByReason(int index, string a)
    {
      this.index = index;
      this.a = a;
    }
  }

  public class ByDust
  {
    public int index;
    public int cost;
    public string name;
    public Outfit outfitType;

    public ByDust(int index, int a, string n)
    {
      this.index = index;
      this.cost = a;
      this.name = n;
    }
  }
}

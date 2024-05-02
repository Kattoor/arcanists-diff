// Decompiled with JetBrains decompiler
// Type: Descriptions
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using Hazel;
using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public static class Descriptions
{
  private static Dictionary<string, string> bookHeader = new Dictionary<string, string>()
  {
    {
      "Arcane",
      "Arcane spells focus on useful utilities and basic summoning abilities"
    },
    {
      "Flame",
      "Flame spells focus on explosions and fire-based mayhem."
    },
    {
      "Stone",
      "Stone spells focus on the creation and destruction of terrain."
    },
    {
      "Storm",
      "Storm spells focus on direct attacks and powerful wind effects"
    },
    {
      "Frost",
      "Frost spells focus on high damage attacks and the summoning of powerful minions"
    },
    {
      "Underdark",
      "Underdark spells focus on stealing health and turning minions into zombies."
    },
    {
      "OverLight",
      "Overlight spells focus on healing and the defense of your forces."
    },
    {
      "Nature",
      "Nature spells focus on trapping enemies and controlling the battlefield."
    },
    {
      "Seas",
      "Sea spells focus on diverse attacks from above, below and summoning armies of minions"
    },
    {
      "Cogs",
      "Cog spells deal with changing the course of time and manipulating your opponents."
    },
    {
      "Holiday",
      "Holiday spells focus on bringing joy and happiness to those around you."
    },
    {
      "Seasons",
      "Seasons spells focus on area deniability and constant attacks from above."
    },
    {
      "Illusion",
      "Illusion spells focus on getting through terrain and attacking just out of reach."
    },
    {
      "Blood",
      "Blood spells focus on changing the flow of the battle and reaping the rewards."
    },
    {
      "Druidism",
      "Druidism spells focus on increasing damage and hunting down your opponents, while sustaining yourself."
    },
    {
      "Cosmos",
      "Cosmos spells focus on getting to your opponents no matter where in the universe they are."
    }
  };
  private static Dictionary<string, string> bookDesriptions = new Dictionary<string, string>()
  {
    {
      "Arcane",
      "Unstable Portal:\n(drains 20 health per cast)\n Every other level increases your max summons by 1. At level 1: Learn the spell Little Devil. At level 2: Learn the spell Arcane Mist. At level 3: Arcane Tower starts with 50hp. At level 5: Arcane Energiser is permanent (once used) and Arcane Tower starts at 75hp"
    },
    {
      "Flame",
      "Harpy Flame\n(drains 20 health per cast)\nAt maximum power, your flame spells will do 50% more damage.\nAt Level 5: Learn the spells Fire Wave and Meteor Shower."
    },
    {
      "Stone",
      "Pet Rock\n(drains 20 health per cast)\nProvides you and your minions with damage resistance. This damage resistance will only be applied to towers on their initial cast <sprite name=\"Resistance\"> (Subsequent casts of the same tower will not have it). Learn the spell Stepping Stone which has reduced cooldown per familiar level.\nAt Level 3: Learn the spell Summon Mountain Goat."
    },
    {
      "Storm",
      "Storm Cloud\n(drains 20 health per cast)\nAllows you to cast Storm faster, increases the power of Thunder Shock and increases the maximum damage potential of Conductor Rod.\nAt Level 5: Wind Shield has a 7 turn cooldown. Also learn the spells Electrostatic Charge and Whistling Winds."
    },
    {
      "Frost",
      "Frost Sprite\n(drains 20 health per cast)\nYour minions' jumping abilities are also buffed for each active level and lets you all skid on water. Each level adds an extra use of Ice Shield. Frost Arrow can deal up to 5 more damage per level. Holding space will make you jump further (minions always do). Learn the spell :\"Frost Leap\" which increases in distance per level allowing you to move quickly around the map."
    },
    {
      "Underdark",
      "Soul Jar\n(drains 20 health per cast)\nCreates a phylactery: an object to contain your soul. If you subsequently die, you'll be resurrected at the Soul Jar's location. The Soul Jar will fail to function if killed by Light damage or if it falls into the water.\nAt level 5: Rain of Chaos is less random."
    },
    {
      "OverLight",
      "Cherub\n(drains 20 health per cast)\nRemoves the 150 protection shield cap. For every charge the familiar has, you and your minions will gain 1 point of shielding up to a maximum of 50 each turn, Shining Bolt will do 2 extra damage and Sunder 5 extra damage.\nAt Level 5: Rising Star gets a 5 turn cooldown."
    },
    {
      "Nature",
      "Fungus\n(drains 20 health per cast)\nIncreases the effects of Vine and Thorn Bombs and also lets your Elves fire larger volleys of arrows and Vine bridge's max distance increases.\nAt level 5: Learn the spell Forestation - "
    },
    {
      "Seas",
      "Seahorse\n(drains 20 health per cast)\nAllows you and your minions to swim within the confines of the arena as well as reducing any water damage taken.\nAt level 5: Gain the ability to summon the mighty Kraken."
    },
    {
      "Cogs",
      "Time Boost:\n(drains 20 health per cast)\nEach level increases your turn time by 6 seconds (or 1/5th your base turn time, whichever is smaller) and grants a use of the spell Time Dilation. At maximum power, you gain 30 extra seconds for your turns (maxes at double your turn time, so only +10 in 10s games).\nAt level 5: Cog Fall has a 3 turn recharge - Clockwork Bomb is guaranteed to explode on your next turn."
    },
    {
      "Holiday",
      "Ho! Ho! Ho!\n(drains 20 health per cast)\nBecome Santa, Eat Cookies, Grow in size, weigh more, long jump further, and be merry....Increases all Holiday use spells Max uses. Snowmen get 5 more max health.\nAt level 5: Learn the spell Gift of Giving. Reindeer get 1 use of Santa's Magic (each). "
    },
    {
      "Seasons",
      "Tranquility\n(drains 20 health per cast)\nLearn the spell: Butterfly Jar. Each level of Harmony gives an extra cast of Life Dew, makes Breeze last longer, causes more Autumn Leaves to fall, The Four Seasons lasts an extra turn before needing to be cast again or forever at level 5, and Snowmen get 5 more max health per level."
    },
    {
      "Illusion",
      "Allusion\n(drains 20 health per cast)\nWho needs health, just give it away! (to a hurt ally/minion) Highest missing health takes priority. Learn the Spell: Blink. Makes you significantly smaller.\nAt level 5: Duplicates have no expiration and spawn with 75% health - Vortex is buffed."
    },
    {
      "Blood",
      "Lifeless\n(drains 20 health per cast)\nIn order of level required: Each Level applies a stack of bleed to all Ball/Bolt/Bomb blood spells (stacks do not stack with each other but can refresh the duration). Learn the spell: Blood Craze. Infections through contact last a minimum of your familiar level and cannot be spread to you. Resurrected minions start with more health. Level 3: Blood Mist. Level 5: Blood Pact - Barrage of Bones recharges one turn faster."
    },
    {
      "Druidism",
      "Raven\n(drains 20 health per cast)\nEach Level provides +2 minion health, +4 Enchanted Axes max bonus damage, and +15% base minion/Druidism cast range. Tiger gets the Compete! spell."
    },
    {
      "Cosmos",
      "Gravitation\n(drains 20 health per cast)\nAt level 1 Learn the spell: Gravity Well. Each level makes you weigh slightly more, making you more resistant to knockback. Lessens the affect of the Gravity debuff. Increases the digging capabilities of Collision Course and Shooting Stars. Increases Supernova's potential damage."
    }
  };
  private static Dictionary<string, string> deathMessages = new Dictionary<string, string>()
  {
    {
      "RisingStarHuge",
      "{0} has been struck by {1}'s rising star."
    },
    {
      "Arcane Meteor",
      "{0} collided with {1}'s arcane meteor."
    },
    {
      "Acid Rain",
      "{0} stood under {1}'s acid rain."
    },
    {
      "Arcane Arrow",
      "{0} has had their saucepan cleaned by {1}'s arcane arrows."
    },
    {
      "Arcane Bomb",
      "{0} exploded into magic from {1}'s bomb."
    },
    {
      "Arcane Tower",
      "{0} got washed away by {1}'s arcane fountain."
    },
    {
      "Arcane Energiser",
      ""
    },
    {
      "Arcane Gate",
      ""
    },
    {
      "Arcane Portal",
      ""
    },
    {
      "Summon Imps",
      "{0} got vaporized by {1}'s imp."
    },
    {
      "Imp Destruction",
      "{0} got their saucepan cleaned by {1}'s imp's destruction."
    },
    {
      "Arcane Glyph",
      "{0} touched one of {1}'s glyphs and died."
    },
    {
      "Arcane Sigil",
      "{0} touched one of {1}'s glyphs and died."
    },
    {
      "Arcane Flash",
      "{0} took a bright flash to the face from {1}."
    },
    {
      "Summon Dragon Egg",
      "{0} died to the cry of {1}'s young dragon."
    },
    {
      "Fire Ball",
      "{0} got cooked well done by {1}'s fireball."
    },
    {
      "Fire Arrow",
      "{0} got charred from {1}'s fire arrow."
    },
    {
      "Lava Bomb",
      "{0} blew up from {1}'s lava bomb."
    },
    {
      "Magma Bomb",
      "{0} turned into ashes from {1}'s magma bomb."
    },
    {
      "Flame Shield",
      ""
    },
    {
      "Flame Wall",
      "{0} collided with {1}'s fire wall."
    },
    {
      "Napalm",
      "{0} got smoked by {1}'s napalm attack."
    },
    {
      "Napalm Bomb",
      "{0} got turned into flame by {1}'s napalm bomb."
    },
    {
      "Rain of Fire",
      "{0} got bombarded by {1}'s rain of fire."
    },
    {
      "Rain of Arrows",
      "{0} was incinerated by {1}'s arrow rain."
    },
    {
      "Volcano",
      "{0} erupted into cinders from {1}'s volcano."
    },
    {
      "Summon Flame Dragon",
      ""
    },
    {
      "Pebble Shot",
      "{0} got perforated by {1}'s pebbles."
    },
    {
      "Scatter Rock",
      "{0} shattered from {1}'s scatter rock."
    },
    {
      "Quake",
      ""
    },
    {
      "Disruption",
      "{0} got caved in by {1}'s disruption."
    },
    {
      "Mud Ball",
      "{0} was buried by {1}'s mud ball."
    },
    {
      "Old Mud Ball",
      "{0} was buried by {1}'s good ol' mud ball."
    },
    {
      "Mega Boulder",
      "{0} got crushed by {1}'s mega boulder."
    },
    {
      "Rock Slab",
      ""
    },
    {
      "Fortress",
      ""
    },
    {
      "Summon Dwarf",
      ""
    },
    {
      "Summon Rock Golem",
      ""
    },
    {
      "Meteor",
      "{0} got decimated from {1}'s meteor."
    },
    {
      "Fissure",
      "{0} was smashed into pieces from {1}'s fissure."
    },
    {
      "Thunder Shock",
      "{0} was electrified by {1}'s thunder shock."
    },
    {
      "Chain Lightning",
      "{0} got electrocuted from {1}'s chain lightning."
    },
    {
      "Wind Shield",
      ""
    },
    {
      "Hurricane",
      ""
    },
    {
      "Shock Bomb",
      "{0} has been shocked from {1}'s shock bomb."
    },
    {
      "Storm Shield",
      "{0} couldn't contain the watts from {1}'s storm shield."
    },
    {
      "Summon Cyclops",
      ""
    },
    {
      "Conductor Rod",
      "{0} got short circuited by {1}'s conductor rod."
    },
    {
      "Summon Storm Spirit",
      ""
    },
    {
      "Flight",
      ""
    },
    {
      "Storm",
      "{0} got electrified from {1}'s storm."
    },
    {
      "Summon Storm Dragon",
      ""
    },
    {
      "Ice Ball",
      "{0} got cold feet from {1}'s ice ball."
    },
    {
      "Ice Bomb",
      "{0} froze solid from {1}'s icy bomb."
    },
    {
      "Frost Shards",
      "{0} was frostbitten by {1}'s frost shards."
    },
    {
      "Frost Arrow",
      "{0} got impaled from {1}'s frost arrow."
    },
    {
      "Snowball",
      "{0} was pelted by {1}'s dangerous snowball."
    },
    {
      "Snowbolt",
      "{0} was out matched in {1}'s snowbolt fight."
    },
    {
      "Blizzard",
      "{0} was frozen solid by {1}'s brutal blizzard."
    },
    {
      "Ice Shield",
      ""
    },
    {
      "Ice Castle",
      ""
    },
    {
      "Summon Sylph",
      ""
    },
    {
      "Summon Frost Giant",
      ""
    },
    {
      "Comet",
      "{0} has been crushed by {1}'s comet!"
    },
    {
      "Frost Dragon",
      ""
    },
    {
      "Den of Darkness",
      "{0} withered away from the presence of {1}'s den of darkness."
    },
    {
      "Rain of Chaos",
      "{0} suffers endlessly from {1}'s rain of chaos."
    },
    {
      "Drain Bolt",
      "{0} had their life force siphoned by {1}'s drain bolt."
    },
    {
      "Death Bomb",
      "{0} rotted away from {1}'s death bomb."
    },
    {
      "Summon Swarm",
      "{0} didn't have the vaccine to prevent rotting from {1}'s killer plague bees."
    },
    {
      "Summon Dark Knight",
      ""
    },
    {
      "Raise Dead",
      "{0} became a cold unliving husk from {1}'s gravely stone"
    },
    {
      "Summon Wraith",
      ""
    },
    {
      "Aura of Decay",
      "{0} rotted from {1}'s aura of decay."
    },
    {
      "Dark Defences",
      "If you're reading this, you're wasting as much time as I took writing this. - I read it all, also hi."
    },
    {
      "Swallowing Pit",
      "{0} got eaten alive from {1}'s swallowing pit."
    },
    {
      "Lichdom",
      "{0} rotted from {1}'s aura of decay."
    },
    {
      "Protection Shield",
      ""
    },
    {
      "Sky Ray",
      "{0} has been boiled by the light of {1}'s sky ray."
    },
    {
      "Shining Bolt",
      "{0} got bolted by {1}'s shining bolt."
    },
    {
      "Rising Star",
      "{0} has been struck by {1}'s rising star!"
    },
    {
      "Summon Pegasus",
      ""
    },
    {
      "Summon Paladin",
      ""
    },
    {
      "Forest Seed",
      ""
    },
    {
      "Summon Pixies",
      ""
    },
    {
      "Sphere of Healing",
      "{0} decayed from the light of {1}'s healing spheres."
    },
    {
      "Castle of Light",
      ""
    },
    {
      "Invulnerability Shield",
      ""
    },
    {
      "Shining Power",
      ""
    },
    {
      "Thorn Ball",
      "{0} was violently pricked by {1}'s thorn ball."
    },
    {
      "Thorn Bomb",
      "{0} exploded into thorns from {1}'s thorn bomb."
    },
    {
      "Vine Whip",
      "{0} got whipped by {1}'s vine whip."
    },
    {
      "Vine Bridge",
      ""
    },
    {
      "Entangle",
      "{0} fell into their doom thanks to {1}'s entangle."
    },
    {
      "Vine Bomb",
      "{0} blossomed into flowers from {1}'s vine bomb."
    },
    {
      "Summon Man-Trap",
      ""
    },
    {
      "Sanctuary",
      "{0} fell into their demise thanks to {1}'s sanctuary."
    },
    {
      "Summon Elves",
      ""
    },
    {
      "Flurry",
      "{0} became prey for {1}'s relentless elves."
    },
    {
      "Nature's Wrath",
      "{0} got rooted up from {1}'s lingering thorn balls."
    },
    {
      "Vine Bloom",
      "{0} has bloomed into their doom from {1}'s vine bloom."
    },
    {
      "Water Ball",
      "{0} took a splash from {1}'s water ball."
    },
    {
      "Maelstrom",
      "{0} drowned in {1}'s maelstrom."
    },
    {
      "Summon Water Trolls",
      "{0} got cannibalized by {1}'s water trolls."
    },
    {
      "Hydration",
      "{0} became overhydrated by {1}'s hydration ball."
    },
    {
      "Deluge",
      "{0} drowned to {1}'s deluge."
    },
    {
      "English Summer",
      "{0}'s hopes became a rainy day due to {1}'s english summer."
    },
    {
      "Brine Bolt",
      "{0} has over swollen to {1}'s brine bolts."
    },
    {
      "Brine Bomb",
      "{0}'s spirit feels salty after dying to {1}'s brine bomb."
    },
    {
      "Summon Brine Goblin",
      "{0} went pop to {1}'s brine goblin."
    },
    {
      "Rain of Clams",
      "{0} got snapped violently from {1}'s clams."
    },
    {
      "Ocean's Fury",
      "{0} got their tides of battle badly turned by {1}'s ocean fury."
    },
    {
      "WaterDropOceanFury",
      "{0} got their tides of battle badly turned by {1}'s ocean fury."
    },
    {
      "WaterOceanFury2",
      "{0} got their tides of battle badly turned by {1}'s ocean fury."
    },
    {
      "Summon Water Lord",
      ""
    },
    {
      "Static Ball",
      ""
    },
    {
      "Static Shield",
      ""
    },
    {
      "Mechanical Arrow",
      "{0} got blasted into time space from {1}'s rocket."
    },
    {
      "Cog Fall",
      "{0} took a heavy hit on the head because of {1}'s giant cog."
    },
    {
      "Recall Device",
      ""
    },
    {
      "Calling Bell",
      "{0} rings the death bell's toll due to {1}'s tricky calling bell."
    },
    {
      "Clock Tower",
      ""
    },
    {
      "Cuckoo Clock",
      "{0} got fractured by {1}'s cuckoo clock."
    },
    {
      "Blast From The Past",
      "{0} got prodded off from {1}'s blast from the past."
    },
    {
      "Clockwork Bomb",
      "{0}'s time of survival ran out because of {1}'s clockwork bomb."
    },
    {
      "Steam Dragon",
      "{0} was standing under {1}'s broken dragon."
    },
    {
      "Redo",
      ""
    },
    {
      "Flame Dragon Breath",
      "{0} got roasted by {1}'s fire dragon."
    },
    {
      "Banish",
      ""
    },
    {
      "Self Destruct",
      "{0} got vaporized by {1}'s imp."
    },
    {
      "Mine",
      "{0} had their internals excavated by {1}'s dwarf."
    },
    {
      "Kablam!",
      "{0} exploded due to {1}'s dwarven dynamite."
    },
    {
      "Wallop",
      "{0} got walloped by {1}'s rock golem."
    },
    {
      "Spirit Shield",
      ""
    },
    {
      "Storm Dragon Breath",
      "{0} got zapped by {1}'s storm dragon breath."
    },
    {
      "Cancel Flight",
      ""
    },
    {
      "Spirit Hurricane",
      ""
    },
    {
      "Shock Shield",
      "{0} couldn't contain the watts from {1}'s shock shield."
    },
    {
      "Smash",
      "{0} got smashed by {1}'s frost giant smash."
    },
    {
      "Frost Dragon Breath",
      "{0} became a living popsicle due to {1}'s pet frost dragon's breath."
    },
    {
      "Sylph Arrow",
      "{0} took an arrow to their knee from {1}'s sylph arrows."
    },
    {
      "Charge",
      "{0} got executed by {1}'s dark knight"
    },
    {
      "Zombie Breath",
      "{0} passed out from the stench of {1}'s pet zombie dragon's bad breath."
    },
    {
      "Sunder",
      "{0} has been torn asunder by {1}'s paladin's holy sword."
    },
    {
      "Fairy Ring",
      ""
    },
    {
      "Volley",
      "{0} got hunted down by {1}'s elves."
    },
    {
      "Dive",
      "{0} dived into the water thanks to {1}'s dive attack."
    },
    {
      "Water Lord Dive",
      "{0} dived into the water thanks to {1}'s dive attack."
    },
    {
      "Recall",
      ""
    },
    {
      "Rake",
      "{0} got raked by {1}'s cog dragon."
    },
    {
      "Stocking Stuffer",
      "{0} got sent to the naughty list by {1}'s stocking stuffer."
    },
    {
      "Presents!",
      "{0} received a gift of death kindly from {1}'s present."
    },
    {
      "New Years Rocket",
      "{0} exploded into confetti from {1}'s firework."
    },
    {
      "Firecrackers",
      "{0} fell into the line of fire of {1}'s firecrackers."
    },
    {
      "Christmas Tree",
      ""
    },
    {
      "Santa's Magic",
      ""
    },
    {
      "Summon Snowman",
      ""
    },
    {
      "Melted Snowman",
      "{0} refroze in {1}'s melted snowman."
    },
    {
      "Summon Reindeer",
      ""
    },
    {
      "Jingle Boom!",
      "{0} blew up all the way up to the festive moon from {1}'s jingle boom."
    },
    {
      "Thanksgiving Dinner",
      "{0} suffered unexpected food poisoning from {1}'s Thanksgiving dinner."
    },
    {
      "Snow Globe",
      "{0} got buried in {1}'s snow."
    },
    {
      "Erosion",
      "{0} deteriorated due to {1}'s erosion."
    },
    {
      "Life Dew",
      "{0} made payment on {1}'s life dew."
    },
    {
      "Acorn",
      "{0} was recycled by {1}'s acorn."
    },
    {
      "Breeze",
      "{0} couldn't hang due to {1}'s Breeze."
    },
    {
      "Morning Sun",
      "{0} fell asleep in light of {1}'s morning sun."
    },
    {
      "Exploding Pinecone",
      "{0} was pricked by {1}'s pinecone."
    },
    {
      "Autumn Leaves",
      "{0} raked in {1}'s autumn leaves."
    },
    {
      "The Four Seasons",
      "{0} was not prepared for {1}'s four seasons."
    },
    {
      "Flutter",
      "{0} head was bit off by {1}'s butterfly."
    },
    {
      "Seasonal",
      "{0} forgot to watch the forecast of {1}'s seasonal weather."
    },
    {
      "Chomp",
      "{0} was eaten by {1}'s dryad."
    },
    {
      "Arcane Dragon",
      ""
    },
    {
      "Arcane Mist",
      "{0} got mystified by {1}'s arcane mist."
    },
    {
      "Air Surge",
      "{0} got blown away by {1}'s air surge."
    },
    {
      "Apparition",
      ""
    },
    {
      "Color Spray",
      "{0} got rainbowlized by {1}'s color spray."
    },
    {
      "Duplication",
      ""
    },
    {
      "Floating Castle",
      ""
    },
    {
      "Glide",
      ""
    },
    {
      "Magical Barrier",
      ""
    },
    {
      "Social Distancing",
      ""
    },
    {
      "Summon Phantom",
      ""
    },
    {
      "Vortex",
      ""
    },
    {
      "Whisper Arrow",
      "{0} didn't see {1}'s whisper arrow coming."
    },
    {
      "Whisper Bomb",
      "{0} blew up into nothingness from {1}'s whisper bomb."
    },
    {
      "Blood Lust",
      "{0} got murdalized from the brutality of {1}'s blood lust."
    },
    {
      "Curse of Loneliness",
      ""
    },
    {
      "Blood Mist",
      "{0} has suffered blood poisoning from {1}'s blood mist."
    },
    {
      "Barrage of Bones",
      "{0} became rattled by {1}'s countless bones."
    },
    {
      "Flesh Wound",
      "{0} got clotted by {1}'s fleshy wound attack."
    },
    {
      "Blood Bath",
      "{0} took a bath from {1}'s blood bath."
    },
    {
      "Curse of Disabling",
      ""
    },
    {
      "Summon Blood Bank",
      ""
    },
    {
      "Summon Gargoyle",
      ""
    },
    {
      "Blood Craze",
      "{0} got fleshed out from {1}'s blood craze."
    },
    {
      "Bleed Out",
      "{0} got exsanguinated from {1}'s bleed out."
    },
    {
      "Infection",
      "{0} died of infection from {1}'s disease."
    },
    {
      "Call of the Void",
      "{0} was lost in {1}'s void."
    },
    {
      "Blood Pact",
      ""
    },
    {
      "Summon Dark Totem",
      ""
    },
    {
      "Resurrection",
      ""
    },
    {
      "Wraith Drain",
      "{0} had their life force siphoned by {1}'s wraith drain."
    },
    {
      "Frost Giant Ball",
      "{0} got cold feet from {1}'s frost giant ball."
    },
    {
      "Ent Whip",
      "{0} got whipped by {1}'s man trap."
    },
    {
      "Frost Familiar",
      ""
    },
    {
      "Hatch",
      ""
    },
    {
      "Rawr!",
      "{0} died to the cry of {1}'s young dragon."
    },
    {
      "Fire Cannons",
      "{0} became cannon fodder for {1}'s boat."
    },
    {
      "Summon Kraken",
      ""
    },
    {
      "From the Depths",
      ""
    },
    {
      "Latch",
      "{0} got dragged down to the depths of the abyss by {1}'s kraken."
    },
    {
      "Fire Wave",
      "{0} couldn't stand the heat of {1}'s fire wave."
    },
    {
      "Meteor Shower",
      "{0} got torched from {1}'s meteor shower."
    },
    {
      "Gift of Giving",
      ""
    },
    {
      "Forestation",
      ""
    },
    {
      "Electrostatic Charge",
      "{0} touched {1}'s electrostatic charge and got fried."
    },
    {
      "Summon Myth",
      "{0} was a myth, {1} was a legend."
    },
    {
      "Healing Spores",
      "{0} ate {1}'s poison mushrooms."
    },
    {
      "Harmony",
      ""
    },
    {
      "Enchanted Axes",
      "{0} got enchanted by {1}'s axes."
    },
    {
      "Summon Will o' the Wisp",
      ""
    },
    {
      "Verdant Javelin",
      "{0} was impaled by {1}'s javelin."
    },
    {
      "Prickly Barrier",
      "{0} ran into {1}'s barrier."
    },
    {
      "Bear Claw",
      "{0} died gloriously to {1}'s Bear Claw."
    },
    {
      "Faerie Jump",
      "{0} was leaps and bounds behind {1}."
    },
    {
      "Summon Boar",
      "{0} was gored by {1}'s boar."
    },
    {
      "Herd Mentality",
      "{0} learned a valuable lesson from {1}: Strength in numbers."
    },
    {
      "Summon Tiger",
      ""
    },
    {
      "Grove Renewal",
      ""
    },
    {
      "Bear Form",
      ""
    },
    {
      "Rampage",
      "{0} was torn apart in {1}'s rampage."
    },
    {
      "Bite",
      "{0} thought {1}'s bark was worse than their bite."
    },
    {
      "Large Bite",
      "{0} was so wrong about {1}'s bark being worse then their bite."
    },
    {
      "Spirit Link",
      ""
    },
    {
      "Wormhole",
      ""
    },
    {
      "Dark Matter Bomb",
      "{0} caved under the pressure of {1}'s dark matter bomb."
    },
    {
      "Collision Course",
      "{0} collided with {1}'s known trajectory."
    },
    {
      "MACAIR",
      ""
    },
    {
      "Drone Strike",
      "{0} was targeted by {1}'s drone strike."
    },
    {
      "Fusion",
      ""
    },
    {
      "Gravity Pulse",
      "{0} gravitated towards {1}'s gravity pulse."
    },
    {
      "Shooting Stars",
      "{0} was captivated by {1}'s shooting stars."
    },
    {
      "Star Bolt",
      "{0} didn't think twice upon {1}'s star bolt."
    },
    {
      "Cosmic Horror",
      ""
    },
    {
      "Black Hole",
      "{0} was in two places at once, {1}'s view, and their own."
    },
    {
      "Supernova",
      "{0} went nova to {1}'s supernova."
    },
    {
      "Summon Drone",
      ""
    },
    {
      "Tentacle Smash",
      "{0} got slapped by {1}'s cosmic horror."
    },
    {
      "Star Ball",
      "{0} got turned into star dust by {1}."
    },
    {
      "Star Dust",
      "{0} got turned into star dust by {1}."
    },
    {
      "Starfire Shard",
      "{0} got turned into star dust by {1}."
    },
    {
      "Gravity Well",
      "{0} sunk into {1}'s gravity well."
    }
  };
  private static Dictionary<string, string> spellDescriptions = new Dictionary<string, string>()
  {
    {
      "Cancel",
      ""
    },
    {
      "Arcane Arrow",
      "Arrow: 20-40 arcane damage\n{0}\nShoots four arrows of pure, bubbling, arcane energy. Good at digging through rock (and cleaning saucepans)."
    },
    {
      "Arcane Bomb",
      "Arrow/Bomb: up to 40 arcane damage\n{0}\nThrows a bomb over the terrain that explodes into three Arcane Arrows."
    },
    {
      "Arcane Tower",
      "Tower: 25-100 health\n{0}\nThis little tower can be of great use, especially if charged up with arcane energy. Will also move with you if you use Arcane Gate."
    },
    {
      "Arcane Energiser",
      "Personal:\n{0}\nSurrounds you with an aura of arcane power that lasts for five turns and causes all of your arcane attacks to deal extra damage."
    },
    {
      "Arcane Gate",
      "Targeted:\n{0}\nTeleports you to a specified location."
    },
    {
      "Arcane Portal",
      "Targeted:\n(1 active only per spell slot){0}\nCreates a two-way portal to allow travel between your current location and a selected destination, that will allow small objects to pass through once per turn. Shares a cooldown with Wormhole."
    },
    {
      "Summon Imps",
      "Arcane Minions: 3x with 10-250 health each\n{0}\nSummons a group of arcane imps to do your bidding. Imps can explode, releasing all the energy they have stored up. The imps can be powered up by hitting them with arcane spells."
    },
    {
      "Imp Destruction",
      "Targeted/Arena: arcane damage up to the equivalent of all imps' health\n{0}\nAll imps on the map explode, releasing clouds of Arcane Arrows at the selected target."
    },
    {
      "Arcane Glyph",
      "Personal: 5-10 arcane damage per glyph\n{0}\nCreates a ring of protective glyphs around you that hampers enemy movement and will stop you from being knocked away."
    },
    {
      "Arcane Sigil",
      "Targeted: up to 30 arcane damage\n{0}\nCreates a triangle of three Arcane Glyphs at the selected location, which can be useful for blocking holes or portals."
    },
    {
      "Arcane Flash",
      "Personal: 15 arcane damage\n{0}\nBlast your enemies out of the arena with this spell, which creates an intense ball of energy that knocks everyone but yourself far and wide."
    },
    {
      "Summon Dragon Egg",
      "Arcane Minion: 25-150 health\n{0}\nSummons a dragon egg that can hatch into different stages of a dragon depending on its current health. The egg can be powered up by hitting them with arcane spells. Gains 10 health a turn."
    },
    {
      "Fire Ball",
      "Ball: 25-50 damage\n{0}\nYour basic, everyday fireball. Deals decent damage (and useful for cooking while out camping)."
    },
    {
      "Fire Arrow",
      "Arrow: 25-50 damage\n{0}\nWhere a Fire Ball would be blocked by scenery, resort to the wondrous, homing Fire Arrow (also useful for campsite cooking)."
    },
    {
      "Lava Bomb",
      "Bomb: 25-50 damage\n{0}\nBounces everywhere a bomb should bounce and explodes after 5 seconds."
    },
    {
      "Magma Bomb",
      "Bomb: up to 125 damage\n{0}\nMore dangerous than the Lava Bomb, this spell explodes into multiple fragments after 5 seconds, dealing damage to a wider area."
    },
    {
      "Flame Shield",
      "Personal:\n{0}\nHave a problem with incoming Fire Balls? Flame Shield will deflect aerial attacks up, up and away, leaving you safely on terra firma."
    },
    {
      "Flame Wall",
      "Targeted: 50 damage\n{0}\nThis upgrade to Flame Shield creates a static defensive wall of flame, which both deflects attacks and damages anyone foolish enough to walk into it."
    },
    {
      "Napalm",
      "Bolt up to 60 napalm damage\n{0}\nKeep enemies at bay by covering the terrain with fire. If that doesn't work, use MORE fire! Napalm will stay on the terrain for the next 5 turns."
    },
    {
      "Napalm Bomb",
      "Bomb: up to 100 damage\n{0}\nPerfect to use against those Arcanists who hide in small holes, as it fills their previously safe holes with napalm fire! This usually results in said enemies leaving said holes in a hurry."
    },
    {
      "Rain of Fire",
      "Targeted: up to 125 damage\n{0}\nBombards the selected area with fire. Seeking cover is recommended."
    },
    {
      "Rain of Arrows",
      "Targeted: up to 75 damage\n{0}\nWhen a target is hiding under scenery, use this seeking spell to punch a hole through it and send them flying."
    },
    {
      "Volcano",
      "Targeted: up to 3,000 damage\n{0}\nCalls up a mighty volcano that lays waste to the whole arena!"
    },
    {
      "Summon Flame Dragon",
      "Minion: flying, mountable; 200 health\n(Immune to napalm damage; 1 use only)\nSome people are afraid of the dark, others of spiders, but what they SHOULD be afraid of is this flame dragon blasting them with its napalm breath!"
    },
    {
      "Pebble Shot",
      "Bolt: up to 50 damage\n{0}\nSticks may not break my bones, but these stones definitely will. Pebbles dig through terrain and deal heavy damage to all types of towers, making them perfect to use against Arcanists who like to hide."
    },
    {
      "Scatter Rock",
      "Ball: up to 65 damage\n{0}\nGreat for shattering towers that are further away and for causing general mayhem."
    },
    {
      "Quake",
      "Arena: up to 68 damage to towers (not Cogs)\n{0}\nCauses an earthquake that damages towers and knocks everyone around a bit."
    },
    {
      "Disruption",
      "Ball: up to 125 damage\n{0}\nCause chaos with this spell! Bury enemies and generally annoy everyone. This spell will do reduced damage (max 73) if used on the first turn."
    },
    {
      "Mud Ball",
      "Ball: 12-25 damage\n{0}\nThis spell doesn't deal much damage, but traps anyone it hits inside a giant ball of mud."
    },
    {
      "Old Mud Ball",
      "Ball: 12-25 damage\n{0}\nThis spell doesn't deal much damage, but traps anyone it hits inside a giant ball of mud."
    },
    {
      "Mega Boulder",
      "Ball: up to 128 damage\n{0}\nStarts small, but soon expands into a giant rock, which bounces around the arena leaving holes everywhere. This spell only reaches full power when dropped from a height."
    },
    {
      "Rock Slab",
      "Targeted:\n{0}\nWhen defense is a requirement or there's need to get over a hole, resort to Rock Slab to save the day."
    },
    {
      "Fortress",
      "Tower: 100 health\n{0}\nThis tower, the strongest in the game, will keep you safe within its cold, hard walls by blocking up to 5 damage per projectile that hits it (minimum 1 damage still gets through)."
    },
    {
      "Summon Dwarf",
      "Minion: 50 health\n{0}\nDwarves love only five things: gold, mining gold, mining rocks, making dynamite and blowing themselves up. They don't much like jumping."
    },
    {
      "Summon Rock Golem",
      "Arcane Minion: with damage resistance; 100 health\n{0}\nRock golems are tough, strong and stupid. They take hardly any damage from attacks and work wonderfully as shields."
    },
    {
      "Meteor",
      "Targeted: up to 276 damage\n{0}\nEnemy hiding in a hole with shields up to their eyeballs? Then drop a meteor on them and boom, problem gone...along with any scenery they were standing on."
    },
    {
      "Fissure",
      "Targeted: up to 2,625 damage\n{0}\nMay look wimpy when first cast, but wait one turn and see what happens. At this point, stand as far away as possible."
    },
    {
      "Thunder Shock",
      "Bolt: up to 50 storm damage\n{0}\nLightning laughs in the face of gravity. (Lightning also laughs in the face of accountants, but that is another story.)"
    },
    {
      "Cyclops Shock Attack",
      "Bolt: up to 50 storm damage\n{0}\nLightning laughs in the face of gravity. (Lightning also laughs in the face of accountants, but that is another story.)"
    },
    {
      "Chain Lightning",
      "Bolt: up to 200 storm damage\n{0}\nChain lightning bounces around, dealing damage all over the place. It is hard to control, though, so watch out."
    },
    {
      "Wind Shield",
      "Personal:\n{0}\nFor the next three turns, all incoming projectiles will be blown away. A very powerful spell, so don't waste it!"
    },
    {
      "Hurricane",
      "Personal:\n{0}\nGreat for dealing with situations that involve a large number of enemies in close proximity."
    },
    {
      "Shock Bomb",
      "Bomb: 40-80 storm damage\n{0}\nSmall explosion; massive damage. Used skillfully this bomb can obliterate enemies. Used badly, it will just singe the scenery."
    },
    {
      "Storm Shield",
      "Personal: up to 25 damage per turn\n{0}\nThis dangerous little shield lasts for five turns and is good to zap anyone who gets too close."
    },
    {
      "Summon Cyclops",
      "Minion: 75 health\n(Immune to storm damage; recharges over 5 turns)\nCyclopes are loyal servants of the Arcanists (except when they are rioting about pay, complaining about the noise or generally being disloyal)."
    },
    {
      "Conductor Rod",
      "Ball: up to 125 storm damage\n{0}\nLightning never strikes the same place twice...unless you are using a Conductor Rod to aim it with. The rod will attract more lightning the further across the arena it flies!"
    },
    {
      "Summon Storm Spirit",
      "Arcane Minion: 25 health\n{0}\nStorm elementals can protect themselves with wind or thunder for as long as you have the spells available."
    },
    {
      "Flight",
      "Personal:\n{0}\nThis spell will let you soar over the landscape and shoot spells down from on high, though is disrupted if you take damage."
    },
    {
      "Storm",
      "Targeted: up to 750 storm damage\n{0}\nAcross the hills, the sound of thunder rumbled. Bolts of searing hot lightning flashed, lighting up the sky above. This spell saturates the targeted area with lightning for three turns."
    },
    {
      "Summon Storm Dragon",
      "Minion: flying, mountable; 150 health\n(Immune to storm damage; 1 use only)\n The storm dragon loves nothing more than sniping its prey with perfectly accurate lightning. Only fools would come out of their holes to meet it in battle."
    },
    {
      "Ice Ball",
      "Ball: up to 75 damage\n{0}\nSkillful use of Ice Ball will result in the quick death of opponents. Poor use results in just a pretty pile of ice."
    },
    {
      "Ice Bomb",
      "Bomb: up to 250 damage\n{0}\nThe most destructive bomb spell. This spell will leave enemies chilled to the wand as it explodes in a shattering spray of ice."
    },
    {
      "Frost Shards",
      "Bolt: up to 75 damage\n{0}\nThe most damaging bolt spell. Anything that gets too close will be pierced by this hail of shards."
    },
    {
      "Frost Arrow",
      "Arrow: up to 75 damage\n{0}\nThe most dangerous arrow spell. A critical hit with this spell will cause enemies to flee in terror."
    },
    {
      "Snowball",
      "Ball: up to 100 snow damage\n{0}\nThis dangerous ball will detonate in midair sending a flurry of snow down onto your target."
    },
    {
      "Blizzard",
      "Targeted: up to 1,000 snow damage\n{0}\nTurns a section of the landscape into a winter wonderland for the next five turns."
    },
    {
      "Ice Shield",
      "Personal: does not end your turn\n{0}\nProtects you by encasing you in ice. The best part of this spell is that you get to do something else afterwards."
    },
    {
      "Ice Castle",
      "Tower: 75 health\n(Immune to snow damage; recharges over 5 turns)\nThis sturdy tower will shield you from any snow-based attacks."
    },
    {
      "Summon Sylph",
      "Minion: 50 health\n(Immune to snow damage; recharges over 5 turns)\nThe bouncy Sylphs can leap over tall mountains to loose their homing arrows into a chosen target."
    },
    {
      "Summon Frost Giant",
      "Minion: 100 health\n(Immune to snow damage; recharges over 5 turns)\nFrost Giants are loners, mostly because they have a permanent snowstorm above their heads."
    },
    {
      "Comet",
      "Targeted: up to 500 damage\n{0}\nSends forth a mighty comet to smash into the land, damaging anyone standing too close."
    },
    {
      "Frost Dragon",
      "Minion: flying, mountable; 150 health\n(Immune to snow damage; 1 use only)\nThe mighty frost dragon is capable of devastating attacks that can obliterate enemies."
    },
    {
      "Den of Darkness",
      "Tower: 75 health\n{0}\nThe evil Den of Darkness recovers 30 health each turn. It is also surrounded by an aura of decay for added protection."
    },
    {
      "Rain of Chaos",
      "Targeted: up to 140 damage\n{0}\nSkulls of chaos rain down from the sky, sending enemies flying. That is if they don't decide to fly off on their own..."
    },
    {
      "Drain Bolt",
      "Bolt: 15-30 damage\n{0}\nSteals the life force of a target and gives it to you. More effective when used on groups of enemies."
    },
    {
      "Death Bomb",
      "Bomb: 35-55 death damage\n{0}\nLike all death damage, this attack will turn anything it kills into a zombies under your control. It also leaves an aura of decay in its wake."
    },
    {
      "Summon Swarm",
      "Arcane Minion: flying; 25 health\n{0}\nThis swarm of evil bees generates a mobile Aura of Decay. This evil aura hurts anyone who is not undead, so make sure you keep your distance."
    },
    {
      "Summon Dark Knight",
      "Minion: 100 health\n{0}\nThe mighty dark knight can carve through solid rock with his axe, dealing insane amounts of damage on the way. He can also mount creatures, making up for this lack of jumping ability."
    },
    {
      "Raise Dead",
      "Bolt: 12-25 death damage\n{0}\n Like all death damage, this bolt will turn anything it kills into a zombie under your control."
    },
    {
      "Summon Wraith",
      "Undead Minion: flying; 50 health\n{0}\nThis undead wraith can cast Drain Bolt and will regain health if hit with death damage."
    },
    {
      "Aura of Decay",
      "Targeted: 15 damage per turn\n{0}\nLike all death damage, this attack will turn anything it kills into a zombie under your control. The aura will last for five turns before dissipating."
    },
    {
      "Dark Defences",
      "Personal:\n{0}\nActivating this powerful spell will heal you back up to full health by drawing life force from your minions. It lasts until all of your minions are drained of their life force."
    },
    {
      "Swallowing Pit",
      "Arena:\n{0}\nEvery minion within the arena will be instantly destroyed and replaced by an Aura of Decay. This is an incredibly powerful spell when used at the right moment, so save it for when it will do the most damage."
    },
    {
      "Lichdom",
      "Personal:\n(permanent) {0}\nEmbracing the power of Lichdom, your Arcanist will transform into a Lich within an Aura of Decay. Lich gain health for all the damage they deal. Restricts you to the Underdark and Arcane spellbooks."
    },
    {
      "Protection Shield",
      "Personal: 50 defense\n{0}\nSurrounds you with a shield of 50 health. Pegasus and Paladins are also able to cast this spell, without a cool down. Maximum 150 shield at any given time."
    },
    {
      "Sky Ray",
      "Targeted: 25-50 light damage\n{0}\nA perfectly accurate ray of boiling light shines down on the target. Like all light attacks this deals double damage to the undead."
    },
    {
      "Shining Bolt",
      "Bolt: 22-45 light damage\n{0}\nSends a bolt of shining light gracefully gliding over the landscape. Like all light attacks, this deals double damage to the undead."
    },
    {
      "Rising Star",
      "Targeted: up to 575 light damage\n{0}\n Stars of pure light fall from the sky, increasing in intensity over the next five turns. Like all light attacks, this deals double damage to the undead."
    },
    {
      "Summon Pegasus",
      "Minion: flying, mountable; 100 health\n{0}\nThe Pegasus is a powerful flying mount that can protect itself with a shield. Use it to transport yourself safely around the landscape."
    },
    {
      "Summon Paladin",
      "Minion: 100 health\n{0}\nThe noble Paladin sunders thy foes in twain. He also has the ability to call a Pegasus to his side, allowing for fast transportation."
    },
    {
      "Forest Seed",
      "Targeted:\n{0}\nCreates a magical 75 hitpoint tree at the targeted location, which blocks projectiles and absorbs damage. Loses 15 hitpoints at the start of each of your turns. The tree cannot be created where existing units stand. Can be used on the first turn if the time limit is less then 20 seconds."
    },
    {
      "Summon Pixies",
      "Minions: 3x flying minions; 25 health each\n{0}\nPixies have the powerful ability to teleport you and your minions to their location, sacrificing themselves in the process."
    },
    {
      "Sphere of Healing",
      "Bolt: 12-25 healing\n{0}\nCuts, bruises and scrapes begone with Sphere of Healing. This spell also hurts those smelly undead for 25-50 damage."
    },
    {
      "Castle of Light",
      "Tower: 75 health\n{0}\nThe Castle of Light contains a healing aura that slowly restores you back up to full health at a rate of 10 health per turn."
    },
    {
      "Invulnerability Shield",
      "Personal:\n{0}\nContains you within a shield of perfect protection that absorbs all damage. The shield will dissipate if you cast another spell, fall into water or after three turns."
    },
    {
      "Shining Power",
      "Personal\n(permanent) {0}\nSprouting angelic wings, you rise into the air gaining the power of flight and the ability to regenerate 10 health per turn. Restricts you to the Overlight and Arcane spellbooks."
    },
    {
      "Thorn Ball",
      "Ball: 25-50 damage\n{0}\nThe ball of thorns projected by this spell sticks to the landscape, exploding only when disturbed. Wonderful for setting traps."
    },
    {
      "Thorn Bomb",
      "Bomb: up to 1,000 damage\n{0}\nWhen this bomb explodes, it leaves a dangerous ring of thorns behind, which makes staying around a very dangerous proposition."
    },
    {
      "Vine Whip",
      "Bolt: up to 35 damage\n{0}\nSend your enemies flying with this handy whip that's also useful for uprooting weeds."
    },
    {
      "Vine Bridge",
      "Bolt: zero damage\n{0}\nShoots a vine that latches onto the landscape, creating a bridge that can be crossed. Also, this spell doesn't end your turn."
    },
    {
      "Entangle",
      "Ball: zero damage\n{0}\nAre flyers getting you down? Root them to the ground with this wonderful tangle of vines and also stops them from using Arcane Gate. The vine is destroyed if the entangled unit takes damage."
    },
    {
      "Vine Bomb",
      "Bomb: 15-30 damage\n{0}\nMake enemies shout in anger by covering them with vines and pretty flowers."
    },
    {
      "Summon Man-Trap",
      "Minion: 100 health\n{0}\nThis weed-like Man-Trap has the ability to cast all of your vine and thorn spells. It also looks pretty in a window box."
    },
    {
      "Sanctuary",
      "Tower: 75 health\n{0}\nDefend yourself within a fortress of nature that dissipates all nearby shields, glyphs, auras and flight spells."
    },
    {
      "Summon Elves",
      "Minions: 2x minions; 40 health each\n{0}\nElves have a weakness: if they see a target, they just cannot help shooting it. Just place their target on your enemy and watch them squirm!"
    },
    {
      "Flurry",
      "Targeted: up to 60 damage per elf\n{0}\nCauses every elf on the map to forget their current target and unleash a flurry of arrows upon the selected target. Prospective pincushions, beware!"
    },
    {
      "Nature's Wrath",
      "Targeted: up to 25 damage per thorn mine\n{0}\nCalls down a whirlwind of leaves that disrupts flying, jumping or shooting through it. Units on the ground should have no worries, though. It also detonates every thorn mine left from a Thorn Ball or Thorn Bomb."
    },
    {
      "Vine Bloom",
      "Personal: up to 5,000 damage\n{0}\nPrimes a Man-Trap for detonation, causing it to explode in a shower of Vine and Thorn Bombs. It's not a good idea to be standing anywhere nearby."
    },
    {
      "Water Ball",
      "Ball: up to 60 water damage\n{0}\nShoots a cohesive ball of water that explodes on impact, spreading a shower of dangerous dihydrogen-monoxide."
    },
    {
      "Maelstrom",
      "Targeted: up to 360 water damage\n{0}\nDuring the next three turns, the targeted area of water will churn and send up a spray of water to damage the land."
    },
    {
      "Summon Water Trolls",
      "Minions: 2x swimming; 40 health each\n{0}\nWater Trolls are rather crazy, attacking anyone who gets too close, including their friends."
    },
    {
      "Hydration",
      "Ball: 25-50 damage\n{0}\nBasic maths: Water Troll + Hydration = a bigger, friendlier, and tougher Water Troll."
    },
    {
      "Deluge",
      "Targeted: up to 90 water damage\n{0}\nSends a perfectly accurate downpour of water onto the target."
    },
    {
      "English Summer",
      "Targeted: up to 580 water damage\n{0}\nWhat begins as a cooling drizzle, over five turns, becomes a torrent of rain onto the target."
    },
    {
      "Brine Bolt",
      "Bolt: up to 60 damage\n{0}\nFires a scattered shot of sea salt at the target. If it misses, the salt will just pile up on the landscape."
    },
    {
      "Brine Bomb",
      "Bomb: up to 445 damage\n{0}\nMake enemies scream in pain and anger by covering them with spiky salt rocks that scatter sea salt all over the area."
    },
    {
      "Brine Burst",
      "Bomb: up to 130 damage\n{0}\nLike Brine Bomb, but less brine, creating more open space."
    },
    {
      "Summon Brine Goblin",
      "Minion: 25 health\n{0}\nBrine Goblins are happy fellows... that is, happy in the knowledge that when they happily die, they will explode in a happy spray of salt that will make their master happy."
    },
    {
      "Rain of Clams",
      "Targeted: up to 250 damage\n{0}\nFive bouncy clams fall from the sky and settle on a home... before their own internal conflicts cause them to explode."
    },
    {
      "Ocean's Fury",
      "Arena: over 5,000 damage\n{0}\nSummons the fury of the sea to wash away anyone foolish enough to remain on the surface of the landscape. Nothing can stand against this torrent of watery destruction."
    },
    {
      "Summon Water Lord",
      "Arcane Minion: swimming, mountable; 500 health\n{0}\nThe Water Lord commands the power of the sea and can even safely transport you underwater. The seas will tremble and the land will fall under his rule."
    },
    {
      "Static Ball",
      "Ball: zero damage\n{0}\nThe powerful static field that is released at impact causes all spell effects within to be halted for the next two turns."
    },
    {
      "Static Shield",
      "Personal:\n{0}\nGenerates a powerful static field around the caster that, for the next three turns, freezes in place all spells that come into contact with it."
    },
    {
      "Mechanical Arrow",
      "Arrow: up to 80 damage\n{0}\nThis badly constructed arrow of copper flies lazily towards its target, bits casually falling off it as it goes. After the first turn it will explode after 5 seconds of travel."
    },
    {
      "Cog Fall",
      "Targeted: up to 50 damage\n{0}\nDrops a heavy cog down from the sky that will smash the blazes out of anything beneath it."
    },
    {
      "Recall Device",
      "Minion: 50 health minion\n{0}\nThe Recall Device has the ability to sacrifice itself to quickly teleport you out of danger at no cost to your time."
    },
    {
      "Calling Bell",
      "Ball:\n{0}\nTeleports the target to a random Recall Device on the map. If there is no Recall Device, the Calling Bell will do nothing."
    },
    {
      "Clock Tower",
      "Tower: 75 health\n{0}\nThis mechanical tower can move around if you walk while within it. Remember that jumping will destroy the tower!"
    },
    {
      "Cuckoo Clock",
      "Ball: up to 100 damage\n{0}\nCuckoo clocks keep fallin' on my head. And just like the Arcanist whose hat is big for his head. Nothin' seems to fit. Those cuckoo clocks are fallin' on my head, they keep on fallin'."
    },
    {
      "Blast From The Past",
      "Bolt: 5 damage\n{0}\nShoots an insignificantly powerful blast that will lightly prod your opponents from across the map. Using this spell will not end your turn. Will not hurt anything at point blank range for safety reasons."
    },
    {
      "Clockwork Bomb",
      "Bomb: 100 damage\n{0}\nThrows a steaming bomb that will deal massive damage to the surrounding area, however knowing when it will detonate is not so easy. Deals extra damage to towers."
    },
    {
      "Steam Dragon",
      "Arcane Minion: flying, mountable; 100 health\n{0}\nThis massive, steam-powered, flying monstrosity repairs itself over time and has the ability to munch through pretty much anything that gets too close."
    },
    {
      "Redo",
      "Arena:\n(1 use only for all players combined)\nSend your foes flying through the eddies of time and space with this powerful spell, which resets all Arcanists' spellbooks to how they were at the start of the game. Time for the fun to begin again!"
    },
    {
      "Flame Dragon Breath",
      "Bolt: up to 100 damage\n{0}\nWhat more can be said? Don't stand between a Flame Dragon and its meal...or YOU'LL be its meal."
    },
    {
      "Banish",
      "Personal:\n{0}\nBanishes the Imp back into the ether. Using this ability does not end your turn."
    },
    {
      "Self Destruct",
      "Personal: 1-250 arcane damage\n(sacrifices the Imp)\n The Imp explodes in a blast of arcane damage equal to its current health."
    },
    {
      "Mine",
      "Targeted: up to 50 damage\n{0}\nThe Dwarf mines through the rock to create hidey-holes or to make a tunnel for you."
    },
    {
      "Kablam!",
      "Personal: 32-75 damage\n{0}\nDwarves give presents to their enemies... usually lit sticks of dynamite! Deals extra damage to towers."
    },
    {
      "Wallop",
      "Personal: 30 damage\n{0}\nWhen projectiles fail to work, just wallop the target!"
    },
    {
      "Spirit Shield",
      "Personal:\n{0}\nFor the next three turns, all incoming projectiles will be blown away."
    },
    {
      "Storm Dragon Breath",
      "Bolt: 37-75 damage\n{0}\nAccurate and devastating; Storm Dragons are always dangerous."
    },
    {
      "Cancel Flight",
      "Personal:\n{0}\nStops your Arcanist from flying. You cannot cast Flight again this turn."
    },
    {
      "Spirit Hurricane",
      "Personal:\n(sacrifices the Storm Spirit)\nGreat for dealing with situations that involve a large number of enemies in close proximity."
    },
    {
      "Shock Shield",
      "Personal:\n{0}\nIf anyone gets too close, zap them with this dangerous little shield."
    },
    {
      "Smash",
      "Personal: 30 damage\n{0}\nWhen projectiles fail to work, just smash your target!"
    },
    {
      "Frost Dragon Breath",
      "Bolt: up to 125 damage\n{0}\nPowerful and icy; Frost Dragons are not to be trifled with."
    },
    {
      "Sylph Arrow",
      "Arrow: up to 60 damage\n{0}\nThree homing arrows of ice fly towards the Sylph's target."
    },
    {
      "Charge",
      "Targeted: up to 250 damage\n{0}\nDark Knights are to be feared and avoided at all costs. Just look at the damage they can cause!"
    },
    {
      "Zombie Breath",
      "Bolt: up to 90 damage\n{0}\nThe zombie dragon's breath will steal the health of its victims, adding it to the dragon's."
    },
    {
      "Sunder",
      "Personal: 75 damage\n{0}\nSunder thy foes in twain with the Paladin's mighty blade."
    },
    {
      "Fairy Ring",
      "Targeted:\n(sacrifices the Pixie)\nTeleports any friendly units around the targeted location to the Pixie's position. Spread your Pixies out to ensure you always have a free gate available."
    },
    {
      "Volley",
      "Targeted: up to 20 damage per elf, per turn\nSets up a target that every elf in the arena will attempt to attack at the start of their turns."
    },
    {
      "Dive",
      "Targeted:\n(can only use in water)\nCauses the selected minion to dive underwater and resurface at a new location on the map. On resurfacing, the minion will create a small cave in which to take cover."
    },
    {
      "Water Lord Dive",
      "Targeted:\n(can only use in water)\nCauses the selected minion to dive underwater and resurface at a new location on the map. On resurfacing, the minion will create a small cave in which to take cover. If alt extended, the rider will be thrown off."
    },
    {
      "Recall",
      "Recall:\n(sacrifices the Recall Device)\nTeleports you to the Recall Device's location. This minion's ability doesn't end your turn unless you're in the water."
    },
    {
      "Rake",
      "Rake: 50 damage\n{0}\nDeal heavy damage at close range with mighty steam-powered jaws."
    },
    {
      "Stocking Stuffer",
      "Bolt/Ball: up to 60 damage\n{0}\nGive your enemies what they deserve, coal."
    },
    {
      "Presents!",
      "Ball: 10 damage\n{0}\nGive all creatures in the vicinity two random spells, which are destroyed when used, does not stack."
    },
    {
      "New Years Rocket",
      "Ball: up to 85 damage\n{0}\nCan detonate mid-air, when it explodes it releases 12 small projectiles."
    },
    {
      "Firecrackers",
      "Bolt: up to 90 damage\n{0}\nHard to use effectively, but can cause lots of mayhem."
    },
    {
      "Christmas Tree",
      "Targeted:\n{0}\nCreates a static Christmas Tree at the targeted location."
    },
    {
      "Santa's Magic",
      "Targeted:\n{0}\n{0}\nTeleports you to a specified location. This spell does not have a cooldown, however it can only protect against water damage once every 5 turns. It will also not use up a use if used while in the water."
    },
    {
      "Summon Reindeer",
      "Soulbound Minion: flying; 100 health\n{0}\nA flying reindeer."
    },
    {
      "Jingle Boom!",
      "Bomb: 55 damage\n{0}\nHigh area of effect, does not destroy much terrain."
    },
    {
      "Thanksgiving Dinner",
      "Ball: 50 healing\n{0}\nHeals creatures in close proximity. This spell also hurts those festivity hating undead for 100 damage."
    },
    {
      "Firework Show",
      "Targeted: up to 510 damage\n{0}\nDeals damage in a wide area."
    },
    {
      "Arcane Dragon",
      "Arcane Dragon: 250 health\n{0}\nHas the same arcane powers as you."
    },
    {
      "Arcane Mist",
      "Targeted: 15 arcane damage per turn\n{0}\nLike Aura of Decay, only Arcane. As such it will not harm the caster."
    },
    {
      "Summon King Monarch",
      "Minion: flying; 50 health\n{0}\nMaybe not as cute, especially if it were to turn undead. This spell shares a cooldown with Summon Monarchs."
    },
    {
      "Summon Monarchs",
      "Soulbound Minion: x2 flying minions; 40 health each\n{0}\nButterflies are beautiful little creatures, until they fly into your face and bite your head off!"
    },
    {
      "Erosion",
      "Bolt: up to 75 damage\n{0}\nExcels at removing that top soil, revealing those nasty vermin that dwell beneath the surface."
    },
    {
      "Life Dew",
      "Targeted: up to 40 damage/heal\n{0}\nIf the target is an ally they will be refreshed, however a foe will get a terrible surprise. Bee's can sacrifice themselves to increase the potency by 15 (max of 2 bees can do this per life dew)."
    },
    {
      "Acorn",
      "Ball: 25 damage\n{0}\nPlants an oak tree wherever it lands, which grows for the next three turns. Will spawn a beehive if you have an open minion slot, which subsequently heals itself 15 health and spawns bees each turn that are controlled by the hive."
    },
    {
      "Breeze",
      "Arena: No damage\n{0}\nPushes everyone in the direction the caster is facing, disrupting non-shielded inexperienced flight users in an attempt to make them crash land."
    },
    {
      "Morning Sun",
      "Targeted: up to 30 damage per turn\n{0}\nThe sun is able to break through the upper atmosphere with great intensity damaging any enemy, in its path with its three beams of pur3 awesomeness. Lasts five turns."
    },
    {
      "Exploding Pinecone",
      "Ball: up to 85 damage\n{0}\nNothing worse than a pinecone falling on your head. Hard to use effectively due to it detonating mid-air, but can cause lots of mayhem."
    },
    {
      "Autumn Leaves",
      "Targeted: up to 210 damage\n{0}\nLeaf piles are fun to play in, just don't play in someone else's. Extending the spell (alt) will instead create old dead leaves which will damage friend or foe and create terrain."
    },
    {
      "Summon Snowman",
      "Arcane Minion: 75 health\n{0}\nCan cast Snowbolt as well as Melt. Turns to slush on water."
    },
    {
      "Snow Globe",
      "Tower: 75 health\n{0}\nComes with its own snow flurry. Shake it up for some extra fun."
    },
    {
      "Tree House",
      "Tower: 75 health\n{0}\nCreates leaves every turn. Shake it up for some extra fun."
    },
    {
      "The Four Seasons",
      "Targeted:\n{0}\nCauses the Seasons to begin rotating (indefinitely) and unleash the power of the elements across the map for the next four turns depending on the active season, you'll be quizzed at the end of the game, so get your cue cards out."
    },
    {
      "Summon Dryad",
      "Arcane Minion: flying: 150 health\n{0}\nA peaceful creature who dissipates all nearby shields, glyphs, auras and flight spells."
    },
    {
      "Summer Weather",
      "Targeted: 20 avg damage\n(Changes with the Seasons)\nA weak English Summer which lasts but a single turn. Does not resolve until after your turn. Does not end your turn."
    },
    {
      "Autumn Weather",
      "Targeted: 20 avg damage\n(Changes with the Seasons)\nA weak Autumn Leaves. Does not resolve until after your turn. Does not end your turn."
    },
    {
      "Winter Weather",
      "Targeted: 20 avg damage\n(Changes with the Seasons)\nA weak Blizzard which lasts but a single turn. Does not resolve until after your turn. Does not end your turn."
    },
    {
      "Spring Weather",
      "Targeted: 20 avg damage\n(Changes with the Seasons)\nA weak Storm which lasts but a single turn. Does not resolve until after your turn. Does not end your turn."
    },
    {
      "Air Surge",
      "Blast: 50 damage\n{0}\nA medium range, sudden burst of air which knocks non-phantom casters back and travels in a straight line."
    },
    {
      "Apparition",
      "Personal:\n{0}\nBecome a phantom for four turns. Restricts you to the Illusion and Arcane spellbooks."
    },
    {
      "Color Spray",
      "Bolt: 60-132 damage\n{0}\nIncreases damage potential with every bounce, up to three times. Explodes on contact."
    },
    {
      "Duplication",
      "Targeted:\n{0}\nDuplicate any minion or player. Spawns with half of the original's health. Lasts four turns. Does not come with any spawned auras attached to it."
    },
    {
      "Floating Castle",
      "Tower: 75 health\n{0}\nUp, up, and away!"
    },
    {
      "Glide",
      "Personal:\n{0}\nStarts active (ends on your turn). Gives you the ability to glide for 1 turn (Hold space while jumping to suppress gliding). While gliding you will attempt to turn around while going out of bounds. This spell does not end your turn."
    },
    {
      "Magical Barrier",
      "Targeted:\n{0}\nStops up to 50 spells by forcing them to immediately explode upon entering the barrier. Lasts five turns."
    },
    {
      "Summon Phantom",
      "Arcane Minion: 30 health\n{0}\nIgnores terrain, cannot be hit directly as spells pass through it. The phantom will lose this ability until its next turn whenever it attacks or is summoned."
    },
    {
      "Vortex",
      "Targeted:\n{0}\nDisrupts spells going through it and confuses arrow spells. Facing right will pull the projectiles towards the center. Facing left will push them away."
    },
    {
      "Whisper Arrow",
      "Arrow: 60 damage\n{0}\nIgnores all collision for the first 0.166 seconds of flight (still affected by shields)."
    },
    {
      "Whisper Bomb",
      "Bomb: 45 damage\n{0}\nIgnores all collision for the first 0.166 seconds of being thrown (still affected by shields). The Illusion familiar gives this spell a damage over distance effect which scales per level."
    },
    {
      "Blood Lust",
      "Melee: 70 damage\n{0}\nYou're out for blood and have found your prey but you're so unaware of what's happening due to your lust for blood that you hurt yourself for 15 damage in the process and if you don't read this you probably wouldn't even notice."
    },
    {
      "Gargoyle Swipe",
      "Melee: 70 damage\n{0}\nTear your enemies limb from limb with this close range, blood thirsty attack."
    },
    {
      "Curse of Loneliness",
      "Targeted:\n{0}\nTarget cannot summon minions on their next turn. This spell does not end your turn, but you cannot summon minions this turn."
    },
    {
      "Blood Mist",
      "Targeted: 10 heal per turn\n{0}\n Be careful! Don't heal your enemies! Hurts undead. "
    },
    {
      "Barrage of Bones",
      "Targeted: up to 160 damage\n{0}\nBouncing bones, what fun."
    },
    {
      "Flesh Wound",
      "Ball: 10 damage\n{0}\nThe target takes 10 damage for the next five turns (boosted by familiar) and receives half healing from all sources (excluding Sphere of Healing) for the next three turns."
    },
    {
      "Blood Bath",
      "Bomb: 20-100 damage\n{0}\nSoak the terrain in blood and horrify you opponents, stunning them for the next turn."
    },
    {
      "Curse of Disabling",
      "Targeted:\n{0}\nDisables two of the target's spells for one turn."
    },
    {
      "Summon Blood Bank",
      "Arcane Minion: 25\n{0}\nA small fleshy minion that while alive all attacks from the player and their minions heal for 20%  of the damage dealt. Any damage the blood bank takes is given to the caster as health if not pacted."
    },
    {
      "Summon Gargoyle",
      "Minion: flying; 50 health\n{0}\nCan turn into stone without ending your turn: to regenerate/increase max health by 25 up to 100. Can cast Blood Lust."
    },
    {
      "Blood Craze",
      "Bolt: 50 damage\n{0}\nGoes straight for the heart, ignoring any protection shields the target has."
    },
    {
      "Bleed Out",
      "Ball: 50% health as damage\n{0}\nSuck the blood from your enemies. Target takes 50% of their health as damage, ignoring protection shields. Bonus 15 additional damage which increases by 15 every time it hits a player."
    },
    {
      "Infection",
      "Arrow: 15 damage per turn\n{0}\nInfects anything hit which spreads on contact. Players in towers will have prolonged symptoms, however it will no longer be infectious after 5 turns regardless. Full blood users cannot be infected through contact, however it can be used on them directly."
    },
    {
      "Blood Pact",
      "Targeted:\n{0}\nEqualize health with the target if you both have more then 25 health each. If this targets an ally any damage taken hence forth is shared equally between you. (Shields and towers do not count towards your hitpoints)."
    },
    {
      "Summon Dark Totem",
      "Arcane Minion: flying; 40 health\n{0}\nHas no offensive spells, but instead destroys terrain in close proximity."
    },
    {
      "Resurrection",
      "Personal:\n{0}\nPick from the most recent 20 minions to die to be brought back to life under your control at 50 percent health, more depending on your blood familiar level. Will not have any shields or auras that were attached to it and will not retain its undead status if it turned undead."
    },
    {
      "Sacrifice",
      "Personal: \n{0}\nDestroys its self and all minions inside of its aura - ally and enemy alike."
    },
    {
      "Vampire Bat",
      "Personal: until end of turn\n{0}\nFor half a turn this spell will let you soar over the landscape and shoot spells down from on high, though is disrupted if you take damage."
    },
    {
      "Summon Vampire",
      "Soulbound Minion: minion: 135 health\n{0}\nCan cast Vampire Bat, Blood Siphon, Blood Lust, Blood Bath, Flesh Wound, and Blood Craze. Takes 10 damage a turn if in direct sunlight. Heals 30% of the damage it deals if you have an active Blood Bank."
    },
    {
      "Wraith Drain",
      "Bolt: 15-30 damage\n{0}\nSteals the life force of a target and gives it to the Wraith. More effective when used on groups."
    },
    {
      "Frost Giant Ball",
      "Ball: 12-75 damage\n{0}\nSkillful use of Frost Giant Ball will result in the quick death of your opponents. Poor use results in just a pretty pile of ice."
    },
    {
      "Ent Whip",
      "Personal: up to 35 damage\n{0}\nSend your enemies flying with this handy whip that's also useful for uprooting weeds."
    },
    {
      "Frost Leap",
      "Ball:\n{0}\nLeap across the map. Max power is increased for each Frost familiar level you have. Aim it like every other ball spell. This spell does not end your turn."
    },
    {
      "Hatch",
      "Personal: Does not end your turn\n{0}\nHatch into a mountable arcane dragon adolescent with a close range attack. Waiting till the egg reaches 150hp will result in a random full grown dragon spawning instead. If it hatches with less then 25 health it instead comes as a dragon hatchling. Steam dragons are not \"hatched\"; they are made. (Which is totally possible)"
    },
    {
      "Dragon Hatchling",
      "Personal: Does not end your turn\n{0}\nHatch into a dragon hatchling which has the spells Arcane Flash and Arcane Glyphs. Waiting till the egg reaches 25hp will result in a dragon adolescent instead."
    },
    {
      "Rawr!",
      "Rake: 50 damage\n{0}\nWatch enemies cower in fear to your mighty roar!"
    },
    {
      "Fire Cannons",
      "Bolt: 10-750 damage\n{0}\nFires a spread of 3 cannonballs dealing massive damage to towers."
    },
    {
      "Summon Kraken",
      "Arcane Minion: swimming; 1000 health\n{0}\nThe mighty Kraken - Can only be summoned on water - An immovable force of nature that can hydrate its allies, dive, summon ships and bring its enemies to their knees. Weak to fire and is not compatible with Dark Defences."
    },
    {
      "From the Depths",
      "Arcane Minion: 50 health\n{0}\nA ship that can unleash its cannons on its foes."
    },
    {
      "Latch",
      "Ball: zero damage\n{0}\nRoot the target to the ground with this energy draining octopus which also stops them from using Arcane Gate. The octopus is destroyed if the latched unit takes damage."
    },
    {
      "Fire Wave",
      "Personal: 25 damage\n{0}\nBlast your enemies out of the arena with this spell, which creates an intense ball of fire that knocks everyone but yourself far and wide."
    },
    {
      "Meteor Shower",
      "Personal: 15 base damage per enemy\n{0}\nTargets all enemies from above and explodes into napalm for maximum mayhem."
    },
    {
      "Gift of Giving",
      "Personal:\n{0}\nTo give is better then to receive... So give yourself two things, that oughta make up for it - Does not end your turn."
    },
    {
      "Forestation",
      "Target:\n{0}\nPlants a forest in a large area."
    },
    {
      "Electrostatic Charge",
      "Ball: 25 storm damage\n{0}\nStays on the battlefield for 10 turns. Recharges every time anyone casts Thunder Shock/Chain Lightning and/or when a Storm goes off. Increases the potency of storm attacks and is super charged by nearby chain lightning."
    },
    {
      "The ol' swaparoo",
      "Target:\n{0}\nSwap places with a target or just  move yourself over a short distance."
    },
    {
      "Whistling Winds",
      "Personal: until end of turn\n{0}\nGain flight for half a turn."
    },
    {
      "Stepping Stone",
      "Targeted:\n{0}\nPlace a small stone - Does not end your turn."
    },
    {
      "Summon Mountain Goat",
      "Arcane Minion: mountable; 50 health\n{0}\nGreat for reaching high places and being annoying."
    },
    {
      "Corrupt Dragon",
      "mountable; 250 health\n{0]\nOnce an Arcane Dragon; now twisted and corrupt."
    },
    {
      "Morph",
      "Personal:\n{0}\nTransforms the gargoyle into a stone statue that cannot attack, but heals 25 health a turn, up to 100. If you are already stone, it turns you back into a gargoyle. This spell does not end your turn."
    },
    {
      "Little Devil",
      "Arcane Minion: 10 health\n{0}\nIf there weren't enough already here's another one - Cooldown is decreased by one for each Arcane familiar level you have. This spell does not end your turn."
    },
    {
      "Blink",
      "Targeted:\n{0}\nShort range teleport that does not end your turn... In the blink of an eye."
    },
    {
      "Pie",
      "Ball: Heal to full health\n{0}\nThere is always room for pie. Live, Laugh, Pie e..."
    },
    {
      "Nothing",
      "Hmm..Nothing?\nThat can't be!\nThere's got to be something!"
    },
    {
      "Flutter",
      "Personal:\n{0}\nKnocks the target back slightly in a small radius."
    },
    {
      "Melt",
      "Personal: health as damage\n{0}\nThe snowman melts into water droplets. Amount depends on its current health. This spell does not end your turn."
    },
    {
      "Snowbolt",
      "Bolt: up to 90 snow damage\n{0}\nPretty much a snowball in bolt form."
    },
    {
      "Cogmobile",
      "Arcane Minion: 100hp\n{0}\nThe climax of arcanist engineering - this drivable beast of thingy-a-bob can go where only certain other individuals have gone."
    },
    {
      "Arcane Meteor",
      "Targeted: up to 125 damage\n{0}\nDissipates all nearby shields, glyphs, auras and flight spells at the impact site and its randomly placed portal's location"
    },
    {
      "Chomp",
      "Personal: 50 damage\n{0}\nDon't worry it won't bite... too hard."
    },
    {
      "Blood Siphon",
      "Arena:\n{0}\nEvery minion within the arena will be dealt 25 damage, healing the caster by half that amount rounded down."
    },
    {
      "Watchtower",
      "Tower: 100 health\n{0}\nMakes for a great look out, giving you a chance before your opponent sees you."
    },
    {
      "Summon Myth",
      "Undead Minion: 50hp\n{0}\nSo they do exist after all."
    },
    {
      "Rock Blaster",
      "Bolt: 25 damage x2\n{0}\nShoots two independently aimed shots."
    },
    {
      "Miner Market",
      "Personal:\n{0}\nSpend the only currency you have (Hitpoints) on mining gear."
    },
    {
      "Mine Shaft",
      "Targeted:\n{0}\nBest way to get to the bottom of a mound of dirt."
    },
    {
      "Miner Map",
      "Targeted:\n{0}\nDwarfs loves maps. Any dwarf with a map will mine to the target location at the start of their turn."
    },
    {
      "Rusty Bomb",
      "Bomb: 40-80 storm damage\n{0}\nSmall explosion, little bit rusty, but massive damage. Used skillfully this bomb can deteriorate your enemies. Used badly, it will just make a good wall ornament."
    },
    {
      "Time Dilation",
      "Personal:\n{0}\nNeed more time? well stop reading this description and use the spell already! Adds 5 seconds to your current turn per your Cogs familiar level."
    },
    {
      "Healing Spores",
      "Targeted: 15 heal\n{0}\nStay happy and healthy with these nutritious mushrooms. If used on a minion it heals for x2. This spell does not end your turn."
    },
    {
      "Harmony",
      "Personal:\n{0}\nAt the end of your turn, until your next turn, you and your minions share all damage taken (The primary target takes 50% of that damage unless that damage is less then the total minions that are under the effect of harmony, in which case its shared amongst the healthiest). This spell does not end your turn."
    },
    {
      "Enchanted Axes",
      "Bolt: {1}x2 damage\n{0}\nThrow two axes in the directions of your choosing. These axes will deal +2 damage (per axe, up to a maximum of +30) for each turn since the last time they were used. This bonus damage does not begin until after the first cast of axes. The axes also ignore effectors which would influence its velocity: Flame Shield/Wind Shield/Spirit Shield/Vortex/Magical Barrier/Static Shield/Wrath/Clouds and any other movement inhibiting effector..."
    },
    {
      "Summon Will o' the Wisp",
      "Arcane Minion: 25 health\n{0}\nHas the spells: Spirit Walk and Spirit Link."
    },
    {
      "Verdant Javelin",
      "Ball: {1} damage\n{0}\nDamage scales up quickly based off distance traveled. Missing will leave the javelin in the terrain, allowing the caster to pick it back up and try again. Will be destroyed if it takes damage. Every time the javelin is cast: its bonus damage is increased by 7 unless it misses and fails to travel far enough to reach its max potential damage."
    },
    {
      "Prickly Barrier",
      "Targeted: 30 damage\n{0}\nThis will damage anyone foolish enough to get to close. Will spread each turn, but all your barrier's snares share the same damage source. The snare will only last 4 turns."
    },
    {
      "Bear Claw",
      "Melee: 50 damage\n{0}\nWhether you're knocking down trees or enemies, it'll get the job done."
    },
    {
      "Faerie Jump",
      "Bolt: 0 damage\n{0}\n Jump in the chosen direction, allowing you to get to hard to reach places. This spell does not end your turn."
    },
    {
      "Summon Boar",
      "Minion: 40 health\n{0}\nCharges at the closest enemy and then lunges towards that enemy dealing 15 damage on impact."
    },
    {
      "Summon Tiger",
      "Minion: 75 health\n{0}\nHas the spells: Stalk and pounce."
    },
    {
      "Compete!",
      "Minions: 2x minions; 40 health each\n{0}\nFeeding Frenzy? Well here's two more mouths to feed. Still in the learning phase, so can only stalk their prey."
    },
    {
      "Grove Renewal",
      "Targeted:\n{0}\nCreates a living grove at the targeted location, which blocks projectiles and absorbs all damage and heals units within for 20 hitpoints. The grove cannot be created where existing units stand and will die after 5 turns."
    },
    {
      "Bear Form",
      "Personal:\n{0}\nTransform into a bear for the next 2 turns giving you the spells Rampage, Swipe and the passive 'Provoke' allowing you to deal bonus damage for each percentage of health you are missing.. Restricts you to Druidism and Arcane spellbooks."
    },
    {
      "Rampage",
      "Targeted: up to 100 damage\n{0}\nSlice and dice your enemies and anything else in your way."
    },
    {
      "Swipe",
      "Melee: 15 damage\n{0}\nA quick attack which does not end your turn."
    },
    {
      "Bite",
      "Melee: 45 damage\n{0}\nTheir bite may be worse than their bark after all."
    },
    {
      "Large Bite",
      "Melee: 55 damage\n{0}\nTheir bite is so much worse than their bark."
    },
    {
      "Herd Mentality",
      "Minion: 40 health\n{0}\nSummons another boar to fight alongside. This spell does not end your turn."
    },
    {
      "Pack Leader",
      "Minion: 40 health\n{0}\nSummons another wolf to fight alongside. This spell does not end your turn."
    },
    {
      "Spirit Link",
      "Targeted:\n{0}\nSacrifice the wisp and teleport the nearby allied target to its location. This spell does not end your turn. Cannot be used in close proximity of another entity."
    },
    {
      "Wormhole",
      "Arena:\n(1 active per player){0}\nOne way portal to anywhere. Any size unit can go through but spells do not go through. Permanent until recast. Going through the wormhole will apply gravity until the start of your next turn. Shares a cooldown with Arcane Portal."
    },
    {
      "Dark Matter Bomb",
      "Bomb: up to 160 damage\n{0}\nAn additional bomb is created near the initial explosion at the beginning of your next turn."
    },
    {
      "Collision Course",
      "Ball: up to 120 damage\n{0}\nThrow a ball of magical Stardust that sticks to anything that it hits. The initial hit drops one projectile of Starfire on the opponent, along with one more Starfire projectile each turn for the next 2 turns."
    },
    {
      "Abduction",
      "Bolt:\n{0}\nMakes the target float in a beam of light for a short time and pulls them slowly towards the caster, then drops them. If they were not flying, gravity is applied till the start of their next turn. Lasts twice as long and 50% further reach if the caster is in a MACAIR. This spell does not end your turn."
    },
    {
      "MACAIR",
      "Tower: 50 health\n{0}\nA Mechanically Advanced Craft Allowing Intergalactic Reconnaissance which hovers in midair allowing you to move left, right, up, and down."
    },
    {
      "Drone Strike",
      "Bolt: 15 damage\n{0}\nA weak, short ranged perfectly accurate bolt of energy that destroys no terrain and has zero knockback. This spell does not end your turn."
    },
    {
      "Fusion",
      "Personal: 30 heal\n{0}\n At the start of each turn, for as long as Fusion persists, you heal a further 10 hp. Fusion is broken by taking damage or casting a spell. Minions can still cast spells without breaking Fusion. Ends automatically after 5 turns if it hasn’t been interrupted."
    },
    {
      "Gravity Pulse",
      "Ball: 30 damage\n{0}\nApplies Gravity for 3 turns. Explosion pulls in rather than out."
    },
    {
      "Shooting Stars",
      "Targeted: up to 240 damage\n{0}\nShoots an increasing number of Starfire projectiles on this turn and next 2 turns."
    },
    {
      "Star Bolt",
      "Bolt: 25x2 damage\n{0}\nShoots two independently aimed, perfectly accurate shots."
    },
    {
      "Cosmic Horror",
      "Soulbound Minion: flying; 75 health\n{0}\nCan cast Tentacle Smash by default, and Abduction, Gravity Pulse, and Dark Matter Bomb if they are in your spellbook. This unit cannot be affected by Gravity under any circumstances."
    },
    {
      "Black Hole",
      "Targeted:\n{0}\nStays until moved or the caster is destroyed. Pulls spells and units alike into its center. Units that fall into the center will sunk unless they are an allied unit and an active wormhole is on the battle field they will instead be sent through the wormhole. Spells and Towers are destroyed."
    },
    {
      "Supernova",
      "Targeted: up to 2880 damage\n{0}\nOver the next 5 turns, the Supernova spews out waves of Starfire in every direction outwards. Increases in intensity over the course of its lifetime."
    },
    {
      "Summon Drone",
      "Soulbound Minion: flying; 40 health\n{0}\nSmall extraterrestrial being which can cast Star Bolt (and Drone Strike with level 5 cosmos familiar)."
    },
    {
      "Tentacle Smash",
      "Melee: 30 damage\n{0}\nWhen projectiles fail to work, gravitate to more barbaric means."
    },
    {
      "Gravity Well",
      "Ball: 50 damage\n{0}\nApplies the gravity well debuff for the next 5 turns on any affected units, which pulls in projectiles in the surrounding area. Explosion pulls in rather than out."
    },
    {
      "Summon Bees",
      "Arcane Minion: flying; 1 health\n{0}\nStrings the first chance it gets and is stunned when it stings. Controlled by the hive. This spell does not end your turn."
    },
    {
      "Butterfly Jar",
      "Targeted:\n{0}\nCreates a jar at the targeted location. This jar can encapsulate a small minion, if it does that minion loses the ability to damage the jar. The jar can be destroyed with any damaging spell. Any minion trapped inside will be eternally grateful and fight for their liberator."
    },
    {
      "Spirit Walk",
      "Targeted:\n{0}\nSeparate mind and body, becoming a phantom-like creature allowing the wisp to move through terrain. Does not end your turn."
    },
    {
      "Retribution",
      "Personal:\n{0}\nEye for eye and tooth for tooth. Over the next 5 turns enemies that deal damage to you will have 50% of that damage also be dealt to them (still doing the full amount to you). This damage is additive and cannot exceed 50 total hitpoints."
    },
    {
      "Blood Clot",
      "Bolt: up to 60 damage\n{0}\nA great way to restrict your enemies movements is by clogging everything up."
    },
    {
      "Death and Decay",
      "Personal:\n{0}\nMakes the swarm's aura permanent; as it initially only lasts 5 turns."
    },
    {
      "Summon Titan",
      "Minion: Level 3\n{0}\nAllows you to harness the greatest powers known to you."
    },
    {
      "Pounce",
      "Bolt: 50 damage\n{0}\n Jump in the target direction, dealing damage when you land."
    },
    {
      "Stalk",
      "Targeted: 25 damage\n{0}\nDisappear from sight, pouncing on the closest enemy at the start of your next turn. This spell does not end your turn."
    }
  };

  public static string GetDrownMessage(ZCreature died) => died.parent.name + " drowned.";

  public static string GetDeathMessage(
    SpellEnum spellEnum,
    ISpellBridge spell,
    ZCreature died,
    ZCreature enemy)
  {
    if ((ZComponent) died == (object) null)
      return "";
    if (spellEnum == SpellEnum.Mine && (ZComponent) enemy != (object) null && enemy.type == CreatureType.Boar)
    {
      spellEnum = SpellEnum.Summon_Boar;
      spell = (ISpellBridge) null;
    }
    string format = (string) null;
    string key1 = spell != null ? (spell.GetName.EndsWith("(Clone)") ? spell.GetName.Substring(0, spell.GetName.Length - 7) : spell.GetName) : "Attack";
    if (spell == null || !Descriptions.deathMessages.TryGetValue(key1, out format))
    {
      switch (spellEnum)
      {
        case SpellEnum.HighHigh:
          format = "{0} tried to get the high ground.";
          break;
        case SpellEnum.PowerOfLight:
          format = (ZComponent) enemy != (object) null ? (enemy.parent == died.parent ? "{0} was attempting to harness the power of light." : "{0} was taught the power of light by {1}.") : "{0} witnessed the power of light.";
          break;
        default:
          string key2 = spellEnum.ToString().Replace('_', ' ');
          Descriptions.deathMessages.TryGetValue(key2, out format);
          break;
      }
    }
    format = !string.IsNullOrEmpty(format) ? string.Format(format, (object) died.parent.name, (ZComponent) enemy != (object) null ? (enemy.parent == died.parent ? (object) "their own" : (object) enemy.parent.name) : (object) "a") : string.Format("{0} died to {1}'s " + key1 + ".", (object) died.parent.name, (ZComponent) enemy != (object) null ? (enemy.parent == died.parent ? (object) "their own" : (object) enemy.parent.name) : (object) "a");
    if ((ZComponent) enemy == (object) null)
      return format.Replace("a's", "a");
    if (enemy.parent != died.parent)
      return format;
    format = format.Replace("their own's", "their own");
    if (format.EndsWith("by their own."))
      format = format.Substring(0, format.Length - "by their own.".Length) + ".";
    return format;
  }

  public static bool GetRunTimeDescription(string name, out string s)
  {
    Spell spell = Inert.GetSpell(name);
    if ((UnityEngine.Object) spell != (UnityEngine.Object) null && spell.runTimeStats.description != null && spell.runTimeStats.description.Length > 10)
    {
      s = spell.runTimeStats.description;
      return true;
    }
    s = "";
    return false;
  }

  public static (string description, string extra) GetSpellDescription(
    string name,
    SpellSlot slot = null,
    bool combined = false)
  {
    string s;
    if (!Descriptions.GetRunTimeDescription(name, out s) && !Descriptions.spellDescriptions.TryGetValue(name, out s))
      return ("Missing Description", "N/A");
    Spell spell = slot?.spell;
    if ((UnityEngine.Object) spell == (UnityEngine.Object) null)
      Inert.Instance.spells.TryGetValue(name, out spell);
    if ((UnityEngine.Object) spell == (UnityEngine.Object) null)
      spell = Inert.Instance._otherspells.Find((Predicate<Spell>) (x => string.Equals(x.name, name)));
    if (!((UnityEngine.Object) spell != (UnityEngine.Object) null))
      return (s, "");
    string str;
    if (slot != null)
    {
      str = slot.MaxUses <= -1 ? "Uses: Infinite" : "Uses: " + (object) slot.MaxUses;
      if (slot.RechargeTime > 0)
      {
        if (str.Length > 0)
          str += "\n";
        str = str + "Cooldown: " + (object) (slot.RechargeTime + 1) + " Turns";
      }
      if (slot.TurnsTillFirstUse > 0)
      {
        if (str.Length > 0)
          str += "\n";
        if (slot.spell.spellEnum != SpellEnum.Forest_Seed && Client.game == null || Client.game.MaxTurnTime >= 20)
          str = str + "Available: Turn " + (object) (slot.TurnsTillFirstUse + 1);
      }
      if (slot.spell.MaxMinionCount > 0)
      {
        if (str.Length > 0)
          str += "\n";
        str = str + "Limit: " + (object) slot.spell.MaxMinionCount + " at a time";
      }
      if (slot.spell.targetType == TargetType.Directional_Controlled)
      {
        if (str.Length > 0)
          str += "\n";
        str += "Directional if > 10s";
      }
      else if (slot.spell.targetType == TargetType.Directional_Controlled_Always)
      {
        if (str.Length > 0)
          str += "\n";
        str += "Directional";
      }
    }
    else
    {
      str = spell.MaxUses <= -1 ? "Uses: Infinite" : "Uses: " + (object) spell.MaxUses;
      if (spell.RechargeTime > 0)
      {
        if (str.Length > 0)
          str += "\n";
        str = str + "Cooldown: " + (object) (spell.RechargeTime + 1) + " Turns";
      }
      if (spell.TurnsTillFirstUse > 0)
      {
        if (str.Length > 0)
          str += "\n";
        str = str + "Available: Turn " + (object) (float) ((double) (spell.TurnsTillFirstUse + 1) + (spell.halfFirstTurn ? -0.5 : 0.0));
        if (spell.spellEnum == SpellEnum.Forest_Seed)
          str += " (0 if time < 21s)";
      }
      if (spell.MaxMinionCount > 0)
      {
        if (str.Length > 0)
          str += "\n";
        str = str + "Limit: " + (object) spell.MaxMinionCount + " at a time";
      }
      if (spell.targetType == TargetType.Directional_Controlled)
      {
        if (str.Length > 0)
          str += "\n";
        str += "Directional if > 10s";
      }
      else if (spell.targetType == TargetType.Directional_Controlled_Always)
      {
        if (str.Length > 0)
          str += "\n";
        str += "Directional";
      }
    }
    if (string.Equals(name, "Enchanted Axes"))
      return combined ? (string.Format(s, (object) str, (object) (slot == null || !((UnityEngine.Object) Player.Instance != (UnityEngine.Object) null) || slot.bonusDmg <= 0 ? spell.damage : spell.damage + Mathf.Min((Player.Instance.person.localTurn - slot.bonusDmg) * 2, 30 + Player.Instance.person.familiarLevels[13] * 2))), str) : (string.Format(s, (object) "", (object) (slot == null || !((UnityEngine.Object) Player.Instance != (UnityEngine.Object) null) || slot.bonusDmg <= 0 ? spell.damage : spell.damage + Mathf.Min((Player.Instance.person.localTurn - slot.bonusDmg) * 2, 30 + Player.Instance.person.familiarLevels[13] * 2))), str);
    if (string.Equals(name, "Verdant Javelin"))
    {
      int num = slot != null ? spell.damage + slot.bonusDmg : spell.damage;
      return combined ? (string.Format(s, (object) str, (object) ((num - num / 2).ToString() + "-" + (object) num)), str) : (string.Format(s, (object) "", (object) ((num - num / 2).ToString() + "-" + (object) num)), str);
    }
    return combined ? (string.Format(s, (object) str), str) : (string.Format(s, (object) ""), str);
  }

  public static string GetBookDescription(BookOf b)
  {
    if (b == BookOf.Arcane && Client.GetAccount(Client.Name).accountType.has(AccountType.Developer | AccountType.Arcane_Monster | AccountType.Game_Director))
      return "Arcane Neutrality:\n(drains 100 health at casting)\nRemoves familiar effects from the game.\n" + Descriptions.bookDesriptions["Arcane"];
    if (b == BookOf.Seasons)
    {
      Player instance = Player.Instance;
      int num;
      if (instance == null)
      {
        num = 0;
      }
      else
      {
        bool? seasonIsHoliday = instance.person?.seasonISHoliday;
        bool flag = true;
        num = seasonIsHoliday.GetValueOrDefault() == flag & seasonIsHoliday.HasValue ? 1 : 0;
      }
      if (num != 0)
        return Descriptions.bookDesriptions["Holiday"];
    }
    string stringX = b.ToStringX();
    string str;
    return Descriptions.bookDesriptions.TryGetValue(stringX, out str) ? str : "Missing Description";
  }

  public static string GetBookHolidayDescription() => Descriptions.bookDesriptions["Holiday"];

  public static string GetHolidayHeader() => Descriptions.bookHeader["Holiday"];

  public static string GetBookHeader(BookOf b)
  {
    string stringX = b.ToStringX();
    string str;
    return Descriptions.bookHeader.TryGetValue(stringX, out str) ? str : "Missing Description";
  }

  public static string _getSpellDescriptionRaw(string name)
  {
    string str;
    return Descriptions.spellDescriptions.TryGetValue(name, out str) ? str : "Missing Description";
  }
}

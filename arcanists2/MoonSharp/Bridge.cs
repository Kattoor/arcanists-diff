// Decompiled with JetBrains decompiler
// Type: MoonSharp.Bridge
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using Educative;
using MoonSharp.Interpreter;
using MoonSharp.Interpreter.Interop;
using MoonSharp.Interpreter.Interop.BasicDescriptors;
using MoonSharp.Interpreter.Interop.StandardDescriptors.HardwiredDescriptors;
using MyPolling;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
namespace MoonSharp
{
  public abstract class Bridge
  {
    private Bridge()
    {
    }

    public static void Initialize()
    {
      UserData.RegisterType((IUserDataDescriptor) new Bridge.TYPE_caafb71c7af049829362a7a17b309837());
      UserData.RegisterType((IUserDataDescriptor) new Bridge.TYPE_31a3a41981ed427cb292bb7317e1c94b());
      UserData.RegisterType((IUserDataDescriptor) new Bridge.TYPE_e03d96fda4a7401886a92c78fd5b3e2b());
      UserData.RegisterType((IUserDataDescriptor) new Bridge.TYPE_e10a01835d594f689146ef35e3a66dfe());
      UserData.RegisterType((IUserDataDescriptor) new Bridge.TYPE_a0a43c0696fc406a8dfb4a90e09fb5ea());
      UserData.RegisterType((IUserDataDescriptor) new Bridge.TYPE_f368f307eac3440bbd1df12c504d2d1b());
      UserData.RegisterType((IUserDataDescriptor) new Bridge.TYPE_ec2fdb8bd1194ee381640fff2dd3955f());
      UserData.RegisterType((IUserDataDescriptor) new Bridge.TYPE_07c986f7088d435a9164948b8dddc6e3());
      UserData.RegisterType((IUserDataDescriptor) new Bridge.TYPE_b7424a4f59e64e839ea2847c2a69408a());
      UserData.RegisterType((IUserDataDescriptor) new Bridge.TYPE_dc6ed2eb2d314d9980e5b6d91b221894());
      UserData.RegisterType((IUserDataDescriptor) new Bridge.TYPE_125eff4cc4dc4707bba5e4087da7644c());
      UserData.RegisterType((IUserDataDescriptor) new Bridge.TYPE_6e8ede65746d49d4bf65cbdc608ba3e1());
      UserData.RegisterType((IUserDataDescriptor) new Bridge.TYPE_04f7a46397b74888a5c780ef6e1a29b5());
      UserData.RegisterType((IUserDataDescriptor) new Bridge.TYPE_f9eba7dcf93540dc8b586654c78e3e0e());
    }

    private sealed class TYPE_caafb71c7af049829362a7a17b309837 : HardwiredUserDataDescriptor
    {
      internal TYPE_caafb71c7af049829362a7a17b309837()
        : base(typeof (ContainerGame))
      {
        this.AddMember("getUser", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("getUser", typeof (ContainerGame), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_caafb71c7af049829362a7a17b309837.MTHD_dbd11b2b2d0f4055847ad9ae2b7c6cd4()
        }));
        this.AddMember("getPlayers", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("getPlayers", typeof (ContainerGame), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_caafb71c7af049829362a7a17b309837.MTHD_bc1e649435454134b67e5ba333d483c7()
        }));
        this.AddMember("getPlayerCount", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("getPlayerCount", typeof (ContainerGame), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_caafb71c7af049829362a7a17b309837.MTHD_0376bd29f27d406f966c697e0dd8ad28()
        }));
        this.AddMember("getCreatures", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("getCreatures", typeof (ContainerGame), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_caafb71c7af049829362a7a17b309837.MTHD_4fbcab06b98240afa76151c379102568()
        }));
        this.AddMember("findCreatures", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("findCreatures", typeof (ContainerGame), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_caafb71c7af049829362a7a17b309837.MTHD_6b852b2def0843a5b64e346a685a05fb()
        }));
        this.AddMember("findEffectors", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("findEffectors", typeof (ContainerGame), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_caafb71c7af049829362a7a17b309837.MTHD_f946e47b34f441648597c66b506bddfa()
        }));
        this.AddMember("LineCast", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("LineCast", typeof (ContainerGame), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_caafb71c7af049829362a7a17b309837.MTHD_b2ee3afe195a4e7ab4d56debfbda2bed()
        }));
        this.AddMember("LineCastOnlyCreatures", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("LineCastOnlyCreatures", typeof (ContainerGame), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_caafb71c7af049829362a7a17b309837.MTHD_06af8d2bb7a64762adcc9ff46ea758c3()
        }));
        this.AddMember("LineCastOnlyTerrain", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("LineCastOnlyTerrain", typeof (ContainerGame), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_caafb71c7af049829362a7a17b309837.MTHD_5679babef7974c388c0fbb11d06cf804()
        }));
        this.AddMember("ShowInfo", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("ShowInfo", typeof (ContainerGame), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_caafb71c7af049829362a7a17b309837.MTHD_4a0e1090d5ae44b0a441c528cc406c3c()
        }));
        this.AddMember("get_turn", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_turn", typeof (ContainerGame), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_caafb71c7af049829362a7a17b309837.MTHD_1a708ea3b61548f8872174dd2d0276b6()
        }));
        this.AddMember("set_turn", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("set_turn", typeof (ContainerGame), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_caafb71c7af049829362a7a17b309837.MTHD_2e3b15882a154f2a970a92fd2219ba93()
        }));
        this.AddMember("get_timeLimit", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_timeLimit", typeof (ContainerGame), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_caafb71c7af049829362a7a17b309837.MTHD_35af48c76e414115aeeef49a45b26e35()
        }));
        this.AddMember("set_timeLimit", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("set_timeLimit", typeof (ContainerGame), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_caafb71c7af049829362a7a17b309837.MTHD_0d6f3e787d994a41a61995f18450fbee()
        }));
        this.AddMember("get_totalTimeElapsed", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_totalTimeElapsed", typeof (ContainerGame), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_caafb71c7af049829362a7a17b309837.MTHD_fbb283589cab4bd7a904d257af4cf4a3()
        }));
        this.AddMember("get_turnTime", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_turnTime", typeof (ContainerGame), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_caafb71c7af049829362a7a17b309837.MTHD_b5e86f78efdc494d890d94b95f4ed296()
        }));
        this.AddMember("get_frame", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_frame", typeof (ContainerGame), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_caafb71c7af049829362a7a17b309837.MTHD_c7bbea65a17042d4a75faa32c2a433c9()
        }));
        this.AddMember("random", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("random", typeof (ContainerGame), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_caafb71c7af049829362a7a17b309837.MTHD_018855187ab34b398653b0ae2f433c4c()
        }));
        this.AddMember("get_winOnDeath", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_winOnDeath", typeof (ContainerGame), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_caafb71c7af049829362a7a17b309837.MTHD_e552cdcbc81b41e2955896dda08467d4()
        }));
        this.AddMember("set_winOnDeath", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("set_winOnDeath", typeof (ContainerGame), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_caafb71c7af049829362a7a17b309837.MTHD_f112ed3d13034b148158c5f50a6ff6e9()
        }));
        this.AddMember("get_allowMovement", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_allowMovement", typeof (ContainerGame), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_caafb71c7af049829362a7a17b309837.MTHD_57672bd3eb03477686f7157c9c53cfd6()
        }));
        this.AddMember("set_allowMovement", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("set_allowMovement", typeof (ContainerGame), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_caafb71c7af049829362a7a17b309837.MTHD_5950b2113cfe4bceb980742a6e4fb0f8()
        }));
        this.AddMember("get_allowInput", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_allowInput", typeof (ContainerGame), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_caafb71c7af049829362a7a17b309837.MTHD_b34ea9ef5cba4bd8b3ae06160e9698ea()
        }));
        this.AddMember("set_allowInput", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("set_allowInput", typeof (ContainerGame), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_caafb71c7af049829362a7a17b309837.MTHD_129d294d0488401e8cdc6dafbccaf8a9()
        }));
        this.AddMember("get_allowCallbacks", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_allowCallbacks", typeof (ContainerGame), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_caafb71c7af049829362a7a17b309837.MTHD_b2fd114c84c044f091a09ddafeea15a6()
        }));
        this.AddMember("set_allowCallbacks", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("set_allowCallbacks", typeof (ContainerGame), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_caafb71c7af049829362a7a17b309837.MTHD_6b0fadde83a34ac59b90183a94b6c721()
        }));
        this.AddMember("get_allowSkipTurn", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_allowSkipTurn", typeof (ContainerGame), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_caafb71c7af049829362a7a17b309837.MTHD_c0d4831baba54f9fb69ab536b4e1d1a8()
        }));
        this.AddMember("set_allowSkipTurn", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("set_allowSkipTurn", typeof (ContainerGame), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_caafb71c7af049829362a7a17b309837.MTHD_cd27e526d2f4419d8218c21706b3bded()
        }));
        this.AddMember("get_terrainDestruction", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_terrainDestruction", typeof (ContainerGame), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_caafb71c7af049829362a7a17b309837.MTHD_6275b0b9e33949249fdd9a43509b6932()
        }));
        this.AddMember("set_terrainDestruction", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("set_terrainDestruction", typeof (ContainerGame), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_caafb71c7af049829362a7a17b309837.MTHD_1ef8b663463742cf8ec863cecf96ce9c()
        }));
        this.AddMember("get_takeDamage", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_takeDamage", typeof (ContainerGame), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_caafb71c7af049829362a7a17b309837.MTHD_20d03005e8944d8e99cb7833899ebdb2()
        }));
        this.AddMember("set_takeDamage", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("set_takeDamage", typeof (ContainerGame), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_caafb71c7af049829362a7a17b309837.MTHD_9f311107a94343e08019573b60b7bf7c()
        }));
        this.AddMember("get_armageddon", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_armageddon", typeof (ContainerGame), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_caafb71c7af049829362a7a17b309837.MTHD_21fccdb8d77a4151b27dcc54d9e8798d()
        }));
        this.AddMember("set_armageddon", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("set_armageddon", typeof (ContainerGame), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_caafb71c7af049829362a7a17b309837.MTHD_5eeae623720d4e7ca80ed82e1c189aa2()
        }));
        this.AddMember("get_armageddonTurn", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_armageddonTurn", typeof (ContainerGame), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_caafb71c7af049829362a7a17b309837.MTHD_ab1e79de0c8949748c1d34979b7b8cd2()
        }));
        this.AddMember("set_armageddonTurn", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("set_armageddonTurn", typeof (ContainerGame), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_caafb71c7af049829362a7a17b309837.MTHD_d6c50b30e77147b3b7261519b5cff5e9()
        }));
        this.AddMember("get_waiting", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_waiting", typeof (ContainerGame), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_caafb71c7af049829362a7a17b309837.MTHD_b55bcb69a352463185e0ee5f158b6b89()
        }));
        this.AddMember("get_paused", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_paused", typeof (ContainerGame), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_caafb71c7af049829362a7a17b309837.MTHD_ef5fa6322c8842049186857da3ceb476()
        }));
        this.AddMember("set_paused", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("set_paused", typeof (ContainerGame), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_caafb71c7af049829362a7a17b309837.MTHD_cb031b53f6df40918d3ed339e7cf8f1a()
        }));
        this.AddMember("get_busy", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_busy", typeof (ContainerGame), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_caafb71c7af049829362a7a17b309837.MTHD_aa9618d3b7f34a2888f5e0bffb8e920b()
        }));
        this.AddMember("get_ongoing", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_ongoing", typeof (ContainerGame), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_caafb71c7af049829362a7a17b309837.MTHD_a031f2edd98c49ed81d780ce67e60811()
        }));
        this.AddMember("get_gravity", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_gravity", typeof (ContainerGame), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_caafb71c7af049829362a7a17b309837.MTHD_514fe932772842018f82d3a855605d30()
        }));
        this.AddMember("set_gravity", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("set_gravity", typeof (ContainerGame), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_caafb71c7af049829362a7a17b309837.MTHD_7ffcd2a08b414692856fa26132490076()
        }));
        this.AddMember("get_wind", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_wind", typeof (ContainerGame), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_caafb71c7af049829362a7a17b309837.MTHD_2f416a0b814746938d2642f26437d886()
        }));
        this.AddMember("set_wind", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("set_wind", typeof (ContainerGame), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_caafb71c7af049829362a7a17b309837.MTHD_1c4b3326221d4222b4850e272bc437ae()
        }));
        this.AddMember("get_windDir", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_windDir", typeof (ContainerGame), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_caafb71c7af049829362a7a17b309837.MTHD_eaaa7dcc817f4d64a74bf6a462fb2341()
        }));
        this.AddMember("set_windDir", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("set_windDir", typeof (ContainerGame), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_caafb71c7af049829362a7a17b309837.MTHD_d30bc146d15b4a5a800822e139b1279b()
        }));
        this.AddMember("get_windPower", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_windPower", typeof (ContainerGame), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_caafb71c7af049829362a7a17b309837.MTHD_283f622e136f4cd293630f01f5e1d862()
        }));
        this.AddMember("set_windPower", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("set_windPower", typeof (ContainerGame), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_caafb71c7af049829362a7a17b309837.MTHD_1327bb8888d840639ed0374339f2d499()
        }));
        this.AddMember("get_width", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_width", typeof (ContainerGame), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_caafb71c7af049829362a7a17b309837.MTHD_2d42eebd22244b66a8d28fdfe90ad145()
        }));
        this.AddMember("get_height", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_height", typeof (ContainerGame), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_caafb71c7af049829362a7a17b309837.MTHD_e306a504be7240e2bd4d5bd3328e5dc9()
        }));
        this.AddMember("get_mousePosition", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_mousePosition", typeof (ContainerGame), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_caafb71c7af049829362a7a17b309837.MTHD_2e25faae88784ab0b0ca42c7fe00ba61()
        }));
        this.AddMember("get_mouseOverUI", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_mouseOverUI", typeof (ContainerGame), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_caafb71c7af049829362a7a17b309837.MTHD_c8784089cc9f4e07b85db60f361ddcca()
        }));
        this.AddMember("worldToCanvas", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("worldToCanvas", typeof (ContainerGame), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_caafb71c7af049829362a7a17b309837.MTHD_3fb99630a1a3422fa09b923538d0f464()
        }));
        this.AddMember("canvasToWorld", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("canvasToWorld", typeof (ContainerGame), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_caafb71c7af049829362a7a17b309837.MTHD_4aad47464c83497ab54eb8e383b7072a()
        }));
        this.AddMember("get_cameraPosition", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_cameraPosition", typeof (ContainerGame), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_caafb71c7af049829362a7a17b309837.MTHD_cf70f0058f9f4c2391aaa2f6a6293447()
        }));
        this.AddMember("set_cameraPosition", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("set_cameraPosition", typeof (ContainerGame), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_caafb71c7af049829362a7a17b309837.MTHD_86c81bd8cc4c48eb9a35d2fbf2311c81()
        }));
        this.AddMember("get_cameraZoom", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_cameraZoom", typeof (ContainerGame), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_caafb71c7af049829362a7a17b309837.MTHD_999c229e466148a4964897acac4639e2()
        }));
        this.AddMember("set_cameraZoom", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("set_cameraZoom", typeof (ContainerGame), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_caafb71c7af049829362a7a17b309837.MTHD_85c7ae813a454afeacf83a501c1d1a1e()
        }));
        this.AddMember("getMapPixel", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("getMapPixel", typeof (ContainerGame), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_caafb71c7af049829362a7a17b309837.MTHD_248b7785213e434c8003a4b27cb598c7()
        }));
        this.AddMember("setMapPixel", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("setMapPixel", typeof (ContainerGame), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_caafb71c7af049829362a7a17b309837.MTHD_71f4d29e27f148aeb27d4c183be3aca2()
        }));
        this.AddMember("drawRectangle", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("drawRectangle", typeof (ContainerGame), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_caafb71c7af049829362a7a17b309837.MTHD_05e9a65b771d4ab7841c3d4d9c5b7e00()
        }));
        this.AddMember("drawEllipseOutline", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("drawEllipseOutline", typeof (ContainerGame), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_caafb71c7af049829362a7a17b309837.MTHD_c85feaaea44c4c2386459c70a6a24165()
        }));
        this.AddMember("drawEllipse", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("drawEllipse", typeof (ContainerGame), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_caafb71c7af049829362a7a17b309837.MTHD_1a9062e2e18b41bfb30f054605038ed0()
        }));
        this.AddMember("blit", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("blit", typeof (ContainerGame), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_caafb71c7af049829362a7a17b309837.MTHD_1f7fda8e388e40b6917c92c928e7d729()
        }));
        this.AddMember("blitRotate", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("blitRotate", typeof (ContainerGame), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_caafb71c7af049829362a7a17b309837.MTHD_10cf632b5eb04982b07402d469e0dc45()
        }));
        this.AddMember("applyDraw", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("applyDraw", typeof (ContainerGame), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_caafb71c7af049829362a7a17b309837.MTHD_679a0c2e75bb4b8c9a5187becb43893f()
        }));
        this.AddMember("get_allowBounce", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_allowBounce", typeof (ContainerGame), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_caafb71c7af049829362a7a17b309837.MTHD_148a445314d44e358ae6c30c46bd7d35()
        }));
        this.AddMember("set_allowBounce", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("set_allowBounce", typeof (ContainerGame), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_caafb71c7af049829362a7a17b309837.MTHD_b38d54de36f54842979de43247f60b07()
        }));
        this.AddMember("get_isUsingTouchControls", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_isUsingTouchControls", typeof (ContainerGame), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_caafb71c7af049829362a7a17b309837.MTHD_d579723a54d54f9abb12f19662e9ef1f()
        }));
        this.AddMember("clientRefreshSpells", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("clientRefreshSpells", typeof (ContainerGame), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_caafb71c7af049829362a7a17b309837.MTHD_c3b4029beaa84a43aadcbd2375c63bb0()
        }));
        this.AddMember("startCoroutine", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("startCoroutine", typeof (ContainerGame), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_caafb71c7af049829362a7a17b309837.MTHD_64c48ddff7834c2bb36e7433f44b0a19()
        }));
        this.AddMember("devCommand", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("devCommand", typeof (ContainerGame), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_caafb71c7af049829362a7a17b309837.MTHD_a11fcf72ece24eec9946a215b6f19ada()
        }));
        this.AddMember("win", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("win", typeof (ContainerGame), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_caafb71c7af049829362a7a17b309837.MTHD_3bec9b6788934cde9e643463e6cee0c9()
        }));
        this.AddMember("lose", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("lose", typeof (ContainerGame), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_caafb71c7af049829362a7a17b309837.MTHD_40d2e0c29d69497d98c29e49c80ce510()
        }));
        this.AddMember("nextTurn", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("nextTurn", typeof (ContainerGame), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_caafb71c7af049829362a7a17b309837.MTHD_a894af6d56d84f05b49cb40735701dd8()
        }));
        this.AddMember("resetMap", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("resetMap", typeof (ContainerGame), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_caafb71c7af049829362a7a17b309837.MTHD_91d9b81cd8b84edd8e9d1d79eb2662df()
        }));
        this.AddMember("clearMap", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("clearMap", typeof (ContainerGame), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_caafb71c7af049829362a7a17b309837.MTHD_efc4ac1a01764642a7cfa1f9e7506fb3()
        }));
        this.AddMember("getSpell", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("getSpell", typeof (ContainerGame), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_caafb71c7af049829362a7a17b309837.MTHD_33cf8ed9e4b743fcb757a204f139d6ef()
        }));
        this.AddMember("getSpellEnum", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("getSpellEnum", typeof (ContainerGame), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_caafb71c7af049829362a7a17b309837.MTHD_189d4e8f99584e67a6f2dd944c9a5031()
        }));
        this.AddMember("getSpellName", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("getSpellName", typeof (ContainerGame), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_caafb71c7af049829362a7a17b309837.MTHD_7790e979eeb54d45b94493b769f8ce45()
        }));
        this.AddMember("getMapID", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("getMapID", typeof (ContainerGame), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_caafb71c7af049829362a7a17b309837.MTHD_225d620fdcf44ebf9753fe6c968aedf8()
        }));
        this.AddMember("getMapRealName", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("getMapRealName", typeof (ContainerGame), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_caafb71c7af049829362a7a17b309837.MTHD_ffda2158541f4aeba37b0a4ff993acd3()
        }));
        this.AddMember("getMapShortName", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("getMapShortName", typeof (ContainerGame), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_caafb71c7af049829362a7a17b309837.MTHD_5682e71e59e843f986b1378ea2fcfb0b()
        }));
        this.AddMember("getArmageddonName", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("getArmageddonName", typeof (ContainerGame), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_caafb71c7af049829362a7a17b309837.MTHD_c1472cadc4624d96ab8b016b500d286b()
        }));
        this.AddMember("panCamera", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("panCamera", typeof (ContainerGame), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_caafb71c7af049829362a7a17b309837.MTHD_65730f78f9d844c3a3878a02fc4efba9()
        }));
        this.AddMember("getInputString", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("getInputString", typeof (ContainerGame), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_caafb71c7af049829362a7a17b309837.MTHD_c7682ab05cbf45f8b8eb5942fc7d006a()
        }));
        this.AddMember("get_screenWidth", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_screenWidth", typeof (ContainerGame), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_caafb71c7af049829362a7a17b309837.MTHD_e058e01adf27442397f025b2787e9f96()
        }));
        this.AddMember("get_screenHeight", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_screenHeight", typeof (ContainerGame), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_caafb71c7af049829362a7a17b309837.MTHD_e16270a73084487396c5d9f82c2593ab()
        }));
        this.AddMember("createCreature", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("createCreature", typeof (ContainerGame), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_caafb71c7af049829362a7a17b309837.MTHD_760ab6bbb4f74679ad81e61906c67b42()
        }));
        this.AddMember("clearIndicators", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("clearIndicators", typeof (ContainerGame), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_caafb71c7af049829362a7a17b309837.MTHD_33ddc15fe8f748679a2b11da489bf8d8()
        }));
        this.AddMember("createIndicator", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("createIndicator", typeof (ContainerGame), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_caafb71c7af049829362a7a17b309837.MTHD_1938b4e55b9c47938aa20fa7b8ad9ec7()
        }));
        this.AddMember("Equals", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("Equals", typeof (ContainerGame), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_caafb71c7af049829362a7a17b309837.MTHD_ceeb06bae7f14611bcdd0e344c79cef1()
        }));
        this.AddMember("GetHashCode", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("GetHashCode", typeof (ContainerGame), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_caafb71c7af049829362a7a17b309837.MTHD_a446a7970fa848ff8ef7349d2989b3da()
        }));
        this.AddMember("GetType", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("GetType", typeof (ContainerGame), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_caafb71c7af049829362a7a17b309837.MTHD_a463a426c6c3461aa2b91b52868db866()
        }));
        this.AddMember("ToString", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("ToString", typeof (ContainerGame), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_caafb71c7af049829362a7a17b309837.MTHD_623628d6271e4229bbf93df0f30ca9ca()
        }));
        this.AddMember("turn", (IMemberDescriptor) new Bridge.TYPE_caafb71c7af049829362a7a17b309837.PROP_7434347434e54d1998a510cc35895058());
        this.AddMember("timeLimit", (IMemberDescriptor) new Bridge.TYPE_caafb71c7af049829362a7a17b309837.PROP_c6626db76bed4ac3bc5c4f392825fc75());
        this.AddMember("totalTimeElapsed", (IMemberDescriptor) new Bridge.TYPE_caafb71c7af049829362a7a17b309837.PROP_cfca68845f004ff291dabc409fd25f41());
        this.AddMember("turnTime", (IMemberDescriptor) new Bridge.TYPE_caafb71c7af049829362a7a17b309837.PROP_6c708f3327334686afb0412571852645());
        this.AddMember("frame", (IMemberDescriptor) new Bridge.TYPE_caafb71c7af049829362a7a17b309837.PROP_e742ac8bd45b49aab809b17909c52d31());
        this.AddMember("winOnDeath", (IMemberDescriptor) new Bridge.TYPE_caafb71c7af049829362a7a17b309837.PROP_9cc47f40d6824b85a6c864c029ad0d2e());
        this.AddMember("allowMovement", (IMemberDescriptor) new Bridge.TYPE_caafb71c7af049829362a7a17b309837.PROP_96fdfec8906242b3bee8de3e3dae1fc1());
        this.AddMember("allowInput", (IMemberDescriptor) new Bridge.TYPE_caafb71c7af049829362a7a17b309837.PROP_2b6e616675c44b54a90b3aeb4d3e34f8());
        this.AddMember("allowCallbacks", (IMemberDescriptor) new Bridge.TYPE_caafb71c7af049829362a7a17b309837.PROP_97e7b4510e104f11acef38f865bade5c());
        this.AddMember("allowSkipTurn", (IMemberDescriptor) new Bridge.TYPE_caafb71c7af049829362a7a17b309837.PROP_e37d8eb9d5fd48f0af49be4f423eb97b());
        this.AddMember("terrainDestruction", (IMemberDescriptor) new Bridge.TYPE_caafb71c7af049829362a7a17b309837.PROP_ce3735ff3248494d8145dbff1b2aa91d());
        this.AddMember("takeDamage", (IMemberDescriptor) new Bridge.TYPE_caafb71c7af049829362a7a17b309837.PROP_530b019bdbdf4ab3aa572ef2f75641d3());
        this.AddMember("armageddon", (IMemberDescriptor) new Bridge.TYPE_caafb71c7af049829362a7a17b309837.PROP_abb11fefe97545d6b050571333c4f819());
        this.AddMember("armageddonTurn", (IMemberDescriptor) new Bridge.TYPE_caafb71c7af049829362a7a17b309837.PROP_9875bc06b48741d3a0508d43daf59376());
        this.AddMember("waiting", (IMemberDescriptor) new Bridge.TYPE_caafb71c7af049829362a7a17b309837.PROP_3a4564d541114f90983407334899251e());
        this.AddMember("paused", (IMemberDescriptor) new Bridge.TYPE_caafb71c7af049829362a7a17b309837.PROP_4839a8de19cc4c2192afff8ff3d3f82f());
        this.AddMember("busy", (IMemberDescriptor) new Bridge.TYPE_caafb71c7af049829362a7a17b309837.PROP_d41f993c23c44250bb253155cda158b9());
        this.AddMember("ongoing", (IMemberDescriptor) new Bridge.TYPE_caafb71c7af049829362a7a17b309837.PROP_d6d1c3c775a3489db1f9d62229686180());
        this.AddMember("gravity", (IMemberDescriptor) new Bridge.TYPE_caafb71c7af049829362a7a17b309837.PROP_81a42c6ca59845b39e6b73f8d9f6ce49());
        this.AddMember("wind", (IMemberDescriptor) new Bridge.TYPE_caafb71c7af049829362a7a17b309837.PROP_df80247f9f6646f7b83ae5bfd669e763());
        this.AddMember("windDir", (IMemberDescriptor) new Bridge.TYPE_caafb71c7af049829362a7a17b309837.PROP_fb8ad7f5552e4a8b9f70c67068b51382());
        this.AddMember("windPower", (IMemberDescriptor) new Bridge.TYPE_caafb71c7af049829362a7a17b309837.PROP_2ccd739731474540920d9427617e0ea4());
        this.AddMember("width", (IMemberDescriptor) new Bridge.TYPE_caafb71c7af049829362a7a17b309837.PROP_f0cbc87eec604462a60a8aa1dd1880b3());
        this.AddMember("height", (IMemberDescriptor) new Bridge.TYPE_caafb71c7af049829362a7a17b309837.PROP_19fdc6e91d4f408dae226d550f8bf100());
        this.AddMember("mousePosition", (IMemberDescriptor) new Bridge.TYPE_caafb71c7af049829362a7a17b309837.PROP_b6a69f04d9574552947c6f2b89d67408());
        this.AddMember("mouseOverUI", (IMemberDescriptor) new Bridge.TYPE_caafb71c7af049829362a7a17b309837.PROP_e0a2aab2fa3141b68df6f5cae8cdb6d6());
        this.AddMember("cameraPosition", (IMemberDescriptor) new Bridge.TYPE_caafb71c7af049829362a7a17b309837.PROP_f1365632ae8b42a283a62332f78fa278());
        this.AddMember("cameraZoom", (IMemberDescriptor) new Bridge.TYPE_caafb71c7af049829362a7a17b309837.PROP_6f546c98923a4fb4846a90cfe29d5a15());
        this.AddMember("allowBounce", (IMemberDescriptor) new Bridge.TYPE_caafb71c7af049829362a7a17b309837.PROP_4a1c4583474b45c2b49afa892388e1de());
        this.AddMember("isUsingTouchControls", (IMemberDescriptor) new Bridge.TYPE_caafb71c7af049829362a7a17b309837.PROP_078d3997d0854443b08b9398f7c55c54());
        this.AddMember("screenWidth", (IMemberDescriptor) new Bridge.TYPE_caafb71c7af049829362a7a17b309837.PROP_2480385c59b14b3b8fbf664dae4fe6cc());
        this.AddMember("screenHeight", (IMemberDescriptor) new Bridge.TYPE_caafb71c7af049829362a7a17b309837.PROP_de2a3b694fc946488995369c14613ed8());
      }

      private sealed class MTHD_dbd11b2b2d0f4055847ad9ae2b7c6cd4 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_dbd11b2b2d0f4055847ad9ae2b7c6cd4()
        {
          this.Initialize("getUser", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerGame) obj).getUser();
        }
      }

      private sealed class MTHD_bc1e649435454134b67e5ba333d483c7 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_bc1e649435454134b67e5ba333d483c7()
        {
          this.Initialize("getPlayers", false, new ParameterDescriptor[1]
          {
            new ParameterDescriptor("script", typeof (Script))
          }, false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerGame) obj).getPlayers((Script) pars[0]);
        }
      }

      private sealed class MTHD_0376bd29f27d406f966c697e0dd8ad28 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_0376bd29f27d406f966c697e0dd8ad28()
        {
          this.Initialize("getPlayerCount", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerGame) obj).getPlayerCount();
        }
      }

      private sealed class MTHD_4fbcab06b98240afa76151c379102568 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_4fbcab06b98240afa76151c379102568()
        {
          this.Initialize("getCreatures", false, new ParameterDescriptor[1]
          {
            new ParameterDescriptor("script", typeof (Script))
          }, false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerGame) obj).getCreatures((Script) pars[0]);
        }
      }

      private sealed class MTHD_6b852b2def0843a5b64e346a685a05fb : HardwiredMethodMemberDescriptor
      {
        internal MTHD_6b852b2def0843a5b64e346a685a05fb()
        {
          this.Initialize("findCreatures", false, new ParameterDescriptor[3]
          {
            new ParameterDescriptor("script", typeof (Script)),
            new ParameterDescriptor("point", typeof (Educative.Point)),
            new ParameterDescriptor("radius", typeof (int))
          }, false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerGame) obj).findCreatures((Script) pars[0], (Educative.Point) pars[1], (int) pars[2]);
        }
      }

      private sealed class MTHD_f946e47b34f441648597c66b506bddfa : HardwiredMethodMemberDescriptor
      {
        internal MTHD_f946e47b34f441648597c66b506bddfa()
        {
          this.Initialize("findEffectors", false, new ParameterDescriptor[3]
          {
            new ParameterDescriptor("script", typeof (Script)),
            new ParameterDescriptor("point", typeof (Educative.Point)),
            new ParameterDescriptor("radius", typeof (int))
          }, false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerGame) obj).findEffectors((Script) pars[0], (Educative.Point) pars[1], (int) pars[2]);
        }
      }

      private sealed class MTHD_b2ee3afe195a4e7ab4d56debfbda2bed : HardwiredMethodMemberDescriptor
      {
        internal MTHD_b2ee3afe195a4e7ab4d56debfbda2bed()
        {
          this.Initialize("LineCast", false, new ParameterDescriptor[6]
          {
            new ParameterDescriptor("a", typeof (Educative.Point)),
            new ParameterDescriptor("b", typeof (Educative.Point)),
            new ParameterDescriptor("ignore", typeof (ContainerCreature), true, (object) new DefaultValue()),
            new ParameterDescriptor("includeCreatures", typeof (bool), true, (object) new DefaultValue()),
            new ParameterDescriptor("includeEffectors", typeof (bool), true, (object) new DefaultValue()),
            new ParameterDescriptor("includePhantom", typeof (bool), true, (object) new DefaultValue())
          }, false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          if (argscount <= 2)
            return (object) ((ContainerGame) obj).LineCast((Educative.Point) pars[0], (Educative.Point) pars[1]);
          if (argscount <= 3)
            return (object) ((ContainerGame) obj).LineCast((Educative.Point) pars[0], (Educative.Point) pars[1], (ContainerCreature) pars[2]);
          if (argscount <= 4)
            return (object) ((ContainerGame) obj).LineCast((Educative.Point) pars[0], (Educative.Point) pars[1], (ContainerCreature) pars[2], (bool) pars[3]);
          return argscount <= 5 ? (object) ((ContainerGame) obj).LineCast((Educative.Point) pars[0], (Educative.Point) pars[1], (ContainerCreature) pars[2], (bool) pars[3], (bool) pars[4]) : (object) ((ContainerGame) obj).LineCast((Educative.Point) pars[0], (Educative.Point) pars[1], (ContainerCreature) pars[2], (bool) pars[3], (bool) pars[4], (bool) pars[5]);
        }
      }

      private sealed class MTHD_06af8d2bb7a64762adcc9ff46ea758c3 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_06af8d2bb7a64762adcc9ff46ea758c3()
        {
          this.Initialize("LineCastOnlyCreatures", false, new ParameterDescriptor[5]
          {
            new ParameterDescriptor("a", typeof (Educative.Point)),
            new ParameterDescriptor("b", typeof (Educative.Point)),
            new ParameterDescriptor("ignore", typeof (ContainerCreature), true, (object) new DefaultValue()),
            new ParameterDescriptor("includeCreatures", typeof (bool), true, (object) new DefaultValue()),
            new ParameterDescriptor("includePhantom", typeof (bool), true, (object) new DefaultValue())
          }, false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          if (argscount <= 2)
            return (object) ((ContainerGame) obj).LineCastOnlyCreatures((Educative.Point) pars[0], (Educative.Point) pars[1]);
          if (argscount <= 3)
            return (object) ((ContainerGame) obj).LineCastOnlyCreatures((Educative.Point) pars[0], (Educative.Point) pars[1], (ContainerCreature) pars[2]);
          return argscount <= 4 ? (object) ((ContainerGame) obj).LineCastOnlyCreatures((Educative.Point) pars[0], (Educative.Point) pars[1], (ContainerCreature) pars[2], (bool) pars[3]) : (object) ((ContainerGame) obj).LineCastOnlyCreatures((Educative.Point) pars[0], (Educative.Point) pars[1], (ContainerCreature) pars[2], (bool) pars[3], (bool) pars[4]);
        }
      }

      private sealed class MTHD_5679babef7974c388c0fbb11d06cf804 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_5679babef7974c388c0fbb11d06cf804()
        {
          this.Initialize("LineCastOnlyTerrain", false, new ParameterDescriptor[2]
          {
            new ParameterDescriptor("a", typeof (Educative.Point)),
            new ParameterDescriptor("b", typeof (Educative.Point))
          }, false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerGame) obj).LineCastOnlyTerrain((Educative.Point) pars[0], (Educative.Point) pars[1]);
        }
      }

      private sealed class MTHD_4a0e1090d5ae44b0a441c528cc406c3c : HardwiredMethodMemberDescriptor
      {
        internal MTHD_4a0e1090d5ae44b0a441c528cc406c3c()
        {
          this.Initialize("ShowInfo", false, new ParameterDescriptor[4]
          {
            new ParameterDescriptor("message", typeof (string)),
            new ParameterDescriptor("onContinue", typeof (bool), true, (object) new DefaultValue()),
            new ParameterDescriptor("pauseGame", typeof (bool), true, (object) new DefaultValue()),
            new ParameterDescriptor("unpauseGame", typeof (bool), true, (object) new DefaultValue())
          }, false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          if (argscount <= 1)
          {
            ((ContainerGame) obj).ShowInfo((string) pars[0]);
            return (object) DynValue.Void;
          }
          if (argscount <= 2)
          {
            ((ContainerGame) obj).ShowInfo((string) pars[0], (bool) pars[1]);
            return (object) DynValue.Void;
          }
          if (argscount <= 3)
          {
            ((ContainerGame) obj).ShowInfo((string) pars[0], (bool) pars[1], (bool) pars[2]);
            return (object) DynValue.Void;
          }
          ((ContainerGame) obj).ShowInfo((string) pars[0], (bool) pars[1], (bool) pars[2], (bool) pars[3]);
          return (object) DynValue.Void;
        }
      }

      private sealed class MTHD_1a708ea3b61548f8872174dd2d0276b6 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_1a708ea3b61548f8872174dd2d0276b6()
        {
          this.Initialize("get_turn", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerGame) obj).turn;
        }
      }

      private sealed class MTHD_2e3b15882a154f2a970a92fd2219ba93 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_2e3b15882a154f2a970a92fd2219ba93()
        {
          this.Initialize("set_turn", false, new ParameterDescriptor[1]
          {
            new ParameterDescriptor("value", typeof (int))
          }, false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          ((ContainerGame) obj).turn = (int) pars[0];
          return (object) null;
        }
      }

      private sealed class MTHD_35af48c76e414115aeeef49a45b26e35 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_35af48c76e414115aeeef49a45b26e35()
        {
          this.Initialize("get_timeLimit", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerGame) obj).timeLimit;
        }
      }

      private sealed class MTHD_0d6f3e787d994a41a61995f18450fbee : HardwiredMethodMemberDescriptor
      {
        internal MTHD_0d6f3e787d994a41a61995f18450fbee()
        {
          this.Initialize("set_timeLimit", false, new ParameterDescriptor[1]
          {
            new ParameterDescriptor("value", typeof (int))
          }, false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          ((ContainerGame) obj).timeLimit = (int) pars[0];
          return (object) null;
        }
      }

      private sealed class MTHD_fbb283589cab4bd7a904d257af4cf4a3 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_fbb283589cab4bd7a904d257af4cf4a3()
        {
          this.Initialize("get_totalTimeElapsed", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerGame) obj).totalTimeElapsed;
        }
      }

      private sealed class MTHD_b5e86f78efdc494d890d94b95f4ed296 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_b5e86f78efdc494d890d94b95f4ed296()
        {
          this.Initialize("get_turnTime", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerGame) obj).turnTime;
        }
      }

      private sealed class MTHD_c7bbea65a17042d4a75faa32c2a433c9 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_c7bbea65a17042d4a75faa32c2a433c9()
        {
          this.Initialize("get_frame", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerGame) obj).frame;
        }
      }

      private sealed class MTHD_018855187ab34b398653b0ae2f433c4c : HardwiredMethodMemberDescriptor
      {
        internal MTHD_018855187ab34b398653b0ae2f433c4c()
        {
          this.Initialize("random", false, new ParameterDescriptor[2]
          {
            new ParameterDescriptor("lower", typeof (int)),
            new ParameterDescriptor("upper", typeof (int))
          }, false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerGame) obj).random((int) pars[0], (int) pars[1]);
        }
      }

      private sealed class MTHD_e552cdcbc81b41e2955896dda08467d4 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_e552cdcbc81b41e2955896dda08467d4()
        {
          this.Initialize("get_winOnDeath", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerGame) obj).winOnDeath;
        }
      }

      private sealed class MTHD_f112ed3d13034b148158c5f50a6ff6e9 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_f112ed3d13034b148158c5f50a6ff6e9()
        {
          this.Initialize("set_winOnDeath", false, new ParameterDescriptor[1]
          {
            new ParameterDescriptor("value", typeof (bool))
          }, false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          ((ContainerGame) obj).winOnDeath = (bool) pars[0];
          return (object) null;
        }
      }

      private sealed class MTHD_57672bd3eb03477686f7157c9c53cfd6 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_57672bd3eb03477686f7157c9c53cfd6()
        {
          this.Initialize("get_allowMovement", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerGame) obj).allowMovement;
        }
      }

      private sealed class MTHD_5950b2113cfe4bceb980742a6e4fb0f8 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_5950b2113cfe4bceb980742a6e4fb0f8()
        {
          this.Initialize("set_allowMovement", false, new ParameterDescriptor[1]
          {
            new ParameterDescriptor("value", typeof (bool))
          }, false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          ((ContainerGame) obj).allowMovement = (bool) pars[0];
          return (object) null;
        }
      }

      private sealed class MTHD_b34ea9ef5cba4bd8b3ae06160e9698ea : HardwiredMethodMemberDescriptor
      {
        internal MTHD_b34ea9ef5cba4bd8b3ae06160e9698ea()
        {
          this.Initialize("get_allowInput", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerGame) obj).allowInput;
        }
      }

      private sealed class MTHD_129d294d0488401e8cdc6dafbccaf8a9 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_129d294d0488401e8cdc6dafbccaf8a9()
        {
          this.Initialize("set_allowInput", false, new ParameterDescriptor[1]
          {
            new ParameterDescriptor("value", typeof (bool))
          }, false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          ((ContainerGame) obj).allowInput = (bool) pars[0];
          return (object) null;
        }
      }

      private sealed class MTHD_b2fd114c84c044f091a09ddafeea15a6 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_b2fd114c84c044f091a09ddafeea15a6()
        {
          this.Initialize("get_allowCallbacks", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerGame) obj).allowCallbacks;
        }
      }

      private sealed class MTHD_6b0fadde83a34ac59b90183a94b6c721 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_6b0fadde83a34ac59b90183a94b6c721()
        {
          this.Initialize("set_allowCallbacks", false, new ParameterDescriptor[1]
          {
            new ParameterDescriptor("value", typeof (bool))
          }, false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          ((ContainerGame) obj).allowCallbacks = (bool) pars[0];
          return (object) null;
        }
      }

      private sealed class MTHD_c0d4831baba54f9fb69ab536b4e1d1a8 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_c0d4831baba54f9fb69ab536b4e1d1a8()
        {
          this.Initialize("get_allowSkipTurn", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerGame) obj).allowSkipTurn;
        }
      }

      private sealed class MTHD_cd27e526d2f4419d8218c21706b3bded : HardwiredMethodMemberDescriptor
      {
        internal MTHD_cd27e526d2f4419d8218c21706b3bded()
        {
          this.Initialize("set_allowSkipTurn", false, new ParameterDescriptor[1]
          {
            new ParameterDescriptor("value", typeof (bool))
          }, false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          ((ContainerGame) obj).allowSkipTurn = (bool) pars[0];
          return (object) null;
        }
      }

      private sealed class MTHD_6275b0b9e33949249fdd9a43509b6932 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_6275b0b9e33949249fdd9a43509b6932()
        {
          this.Initialize("get_terrainDestruction", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerGame) obj).terrainDestruction;
        }
      }

      private sealed class MTHD_1ef8b663463742cf8ec863cecf96ce9c : HardwiredMethodMemberDescriptor
      {
        internal MTHD_1ef8b663463742cf8ec863cecf96ce9c()
        {
          this.Initialize("set_terrainDestruction", false, new ParameterDescriptor[1]
          {
            new ParameterDescriptor("value", typeof (bool))
          }, false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          ((ContainerGame) obj).terrainDestruction = (bool) pars[0];
          return (object) null;
        }
      }

      private sealed class MTHD_20d03005e8944d8e99cb7833899ebdb2 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_20d03005e8944d8e99cb7833899ebdb2()
        {
          this.Initialize("get_takeDamage", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerGame) obj).takeDamage;
        }
      }

      private sealed class MTHD_9f311107a94343e08019573b60b7bf7c : HardwiredMethodMemberDescriptor
      {
        internal MTHD_9f311107a94343e08019573b60b7bf7c()
        {
          this.Initialize("set_takeDamage", false, new ParameterDescriptor[1]
          {
            new ParameterDescriptor("value", typeof (bool))
          }, false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          ((ContainerGame) obj).takeDamage = (bool) pars[0];
          return (object) null;
        }
      }

      private sealed class MTHD_21fccdb8d77a4151b27dcc54d9e8798d : HardwiredMethodMemberDescriptor
      {
        internal MTHD_21fccdb8d77a4151b27dcc54d9e8798d()
        {
          this.Initialize("get_armageddon", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerGame) obj).armageddon;
        }
      }

      private sealed class MTHD_5eeae623720d4e7ca80ed82e1c189aa2 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_5eeae623720d4e7ca80ed82e1c189aa2()
        {
          this.Initialize("set_armageddon", false, new ParameterDescriptor[1]
          {
            new ParameterDescriptor("value", typeof (MapEnum))
          }, false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          ((ContainerGame) obj).armageddon = (MapEnum) pars[0];
          return (object) null;
        }
      }

      private sealed class MTHD_ab1e79de0c8949748c1d34979b7b8cd2 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_ab1e79de0c8949748c1d34979b7b8cd2()
        {
          this.Initialize("get_armageddonTurn", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerGame) obj).armageddonTurn;
        }
      }

      private sealed class MTHD_d6c50b30e77147b3b7261519b5cff5e9 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_d6c50b30e77147b3b7261519b5cff5e9()
        {
          this.Initialize("set_armageddonTurn", false, new ParameterDescriptor[1]
          {
            new ParameterDescriptor("value", typeof (int))
          }, false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          ((ContainerGame) obj).armageddonTurn = (int) pars[0];
          return (object) null;
        }
      }

      private sealed class MTHD_b55bcb69a352463185e0ee5f158b6b89 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_b55bcb69a352463185e0ee5f158b6b89()
        {
          this.Initialize("get_waiting", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerGame) obj).waiting;
        }
      }

      private sealed class MTHD_ef5fa6322c8842049186857da3ceb476 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_ef5fa6322c8842049186857da3ceb476()
        {
          this.Initialize("get_paused", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerGame) obj).paused;
        }
      }

      private sealed class MTHD_cb031b53f6df40918d3ed339e7cf8f1a : HardwiredMethodMemberDescriptor
      {
        internal MTHD_cb031b53f6df40918d3ed339e7cf8f1a()
        {
          this.Initialize("set_paused", false, new ParameterDescriptor[1]
          {
            new ParameterDescriptor("value", typeof (bool))
          }, false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          ((ContainerGame) obj).paused = (bool) pars[0];
          return (object) null;
        }
      }

      private sealed class MTHD_aa9618d3b7f34a2888f5e0bffb8e920b : HardwiredMethodMemberDescriptor
      {
        internal MTHD_aa9618d3b7f34a2888f5e0bffb8e920b()
        {
          this.Initialize("get_busy", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerGame) obj).busy;
        }
      }

      private sealed class MTHD_a031f2edd98c49ed81d780ce67e60811 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_a031f2edd98c49ed81d780ce67e60811()
        {
          this.Initialize("get_ongoing", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerGame) obj).ongoing;
        }
      }

      private sealed class MTHD_514fe932772842018f82d3a855605d30 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_514fe932772842018f82d3a855605d30()
        {
          this.Initialize("get_gravity", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerGame) obj).gravity;
        }
      }

      private sealed class MTHD_7ffcd2a08b414692856fa26132490076 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_7ffcd2a08b414692856fa26132490076()
        {
          this.Initialize("set_gravity", false, new ParameterDescriptor[1]
          {
            new ParameterDescriptor("value", typeof (double))
          }, false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          ((ContainerGame) obj).gravity = (double) pars[0];
          return (object) null;
        }
      }

      private sealed class MTHD_2f416a0b814746938d2642f26437d886 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_2f416a0b814746938d2642f26437d886()
        {
          this.Initialize("get_wind", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerGame) obj).wind;
        }
      }

      private sealed class MTHD_1c4b3326221d4222b4850e272bc437ae : HardwiredMethodMemberDescriptor
      {
        internal MTHD_1c4b3326221d4222b4850e272bc437ae()
        {
          this.Initialize("set_wind", false, new ParameterDescriptor[1]
          {
            new ParameterDescriptor("value", typeof (bool))
          }, false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          ((ContainerGame) obj).wind = (bool) pars[0];
          return (object) null;
        }
      }

      private sealed class MTHD_eaaa7dcc817f4d64a74bf6a462fb2341 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_eaaa7dcc817f4d64a74bf6a462fb2341()
        {
          this.Initialize("get_windDir", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerGame) obj).windDir;
        }
      }

      private sealed class MTHD_d30bc146d15b4a5a800822e139b1279b : HardwiredMethodMemberDescriptor
      {
        internal MTHD_d30bc146d15b4a5a800822e139b1279b()
        {
          this.Initialize("set_windDir", false, new ParameterDescriptor[1]
          {
            new ParameterDescriptor("value", typeof (double))
          }, false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          ((ContainerGame) obj).windDir = (double) pars[0];
          return (object) null;
        }
      }

      private sealed class MTHD_283f622e136f4cd293630f01f5e1d862 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_283f622e136f4cd293630f01f5e1d862()
        {
          this.Initialize("get_windPower", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerGame) obj).windPower;
        }
      }

      private sealed class MTHD_1327bb8888d840639ed0374339f2d499 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_1327bb8888d840639ed0374339f2d499()
        {
          this.Initialize("set_windPower", false, new ParameterDescriptor[1]
          {
            new ParameterDescriptor("value", typeof (double))
          }, false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          ((ContainerGame) obj).windPower = (double) pars[0];
          return (object) null;
        }
      }

      private sealed class MTHD_2d42eebd22244b66a8d28fdfe90ad145 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_2d42eebd22244b66a8d28fdfe90ad145()
        {
          this.Initialize("get_width", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerGame) obj).width;
        }
      }

      private sealed class MTHD_e306a504be7240e2bd4d5bd3328e5dc9 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_e306a504be7240e2bd4d5bd3328e5dc9()
        {
          this.Initialize("get_height", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerGame) obj).height;
        }
      }

      private sealed class MTHD_2e25faae88784ab0b0ca42c7fe00ba61 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_2e25faae88784ab0b0ca42c7fe00ba61()
        {
          this.Initialize("get_mousePosition", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerGame) obj).mousePosition;
        }
      }

      private sealed class MTHD_c8784089cc9f4e07b85db60f361ddcca : HardwiredMethodMemberDescriptor
      {
        internal MTHD_c8784089cc9f4e07b85db60f361ddcca()
        {
          this.Initialize("get_mouseOverUI", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerGame) obj).mouseOverUI;
        }
      }

      private sealed class MTHD_3fb99630a1a3422fa09b923538d0f464 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_3fb99630a1a3422fa09b923538d0f464()
        {
          this.Initialize("worldToCanvas", false, new ParameterDescriptor[1]
          {
            new ParameterDescriptor("p", typeof (Educative.Point))
          }, false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerGame) obj).worldToCanvas((Educative.Point) pars[0]);
        }
      }

      private sealed class MTHD_4aad47464c83497ab54eb8e383b7072a : HardwiredMethodMemberDescriptor
      {
        internal MTHD_4aad47464c83497ab54eb8e383b7072a()
        {
          this.Initialize("canvasToWorld", false, new ParameterDescriptor[1]
          {
            new ParameterDescriptor("p", typeof (Educative.Point))
          }, false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerGame) obj).canvasToWorld((Educative.Point) pars[0]);
        }
      }

      private sealed class MTHD_cf70f0058f9f4c2391aaa2f6a6293447 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_cf70f0058f9f4c2391aaa2f6a6293447()
        {
          this.Initialize("get_cameraPosition", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerGame) obj).cameraPosition;
        }
      }

      private sealed class MTHD_86c81bd8cc4c48eb9a35d2fbf2311c81 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_86c81bd8cc4c48eb9a35d2fbf2311c81()
        {
          this.Initialize("set_cameraPosition", false, new ParameterDescriptor[1]
          {
            new ParameterDescriptor("value", typeof (Educative.Point))
          }, false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          ((ContainerGame) obj).cameraPosition = (Educative.Point) pars[0];
          return (object) null;
        }
      }

      private sealed class MTHD_999c229e466148a4964897acac4639e2 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_999c229e466148a4964897acac4639e2()
        {
          this.Initialize("get_cameraZoom", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerGame) obj).cameraZoom;
        }
      }

      private sealed class MTHD_85c7ae813a454afeacf83a501c1d1a1e : HardwiredMethodMemberDescriptor
      {
        internal MTHD_85c7ae813a454afeacf83a501c1d1a1e()
        {
          this.Initialize("set_cameraZoom", false, new ParameterDescriptor[1]
          {
            new ParameterDescriptor("value", typeof (double))
          }, false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          ((ContainerGame) obj).cameraZoom = (double) pars[0];
          return (object) null;
        }
      }

      private sealed class MTHD_248b7785213e434c8003a4b27cb598c7 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_248b7785213e434c8003a4b27cb598c7()
        {
          this.Initialize("getMapPixel", false, new ParameterDescriptor[2]
          {
            new ParameterDescriptor("x", typeof (int)),
            new ParameterDescriptor("y", typeof (int))
          }, false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerGame) obj).getMapPixel((int) pars[0], (int) pars[1]);
        }
      }

      private sealed class MTHD_71f4d29e27f148aeb27d4c183be3aca2 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_71f4d29e27f148aeb27d4c183be3aca2()
        {
          this.Initialize("setMapPixel", false, new ParameterDescriptor[3]
          {
            new ParameterDescriptor("x", typeof (int)),
            new ParameterDescriptor("y", typeof (int)),
            new ParameterDescriptor("c", typeof (LuaColor))
          }, false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          ((ContainerGame) obj).setMapPixel((int) pars[0], (int) pars[1], (LuaColor) pars[2]);
          return (object) DynValue.Void;
        }
      }

      private sealed class MTHD_05e9a65b771d4ab7841c3d4d9c5b7e00 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_05e9a65b771d4ab7841c3d4d9c5b7e00()
        {
          this.Initialize("drawRectangle", false, new ParameterDescriptor[5]
          {
            new ParameterDescriptor("x", typeof (int)),
            new ParameterDescriptor("y", typeof (int)),
            new ParameterDescriptor("width", typeof (int)),
            new ParameterDescriptor("height", typeof (int)),
            new ParameterDescriptor("c", typeof (LuaColor))
          }, false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          ((ContainerGame) obj).drawRectangle((int) pars[0], (int) pars[1], (int) pars[2], (int) pars[3], (LuaColor) pars[4]);
          return (object) DynValue.Void;
        }
      }

      private sealed class MTHD_c85feaaea44c4c2386459c70a6a24165 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_c85feaaea44c4c2386459c70a6a24165()
        {
          this.Initialize("drawEllipseOutline", false, new ParameterDescriptor[6]
          {
            new ParameterDescriptor("x", typeof (int)),
            new ParameterDescriptor("y", typeof (int)),
            new ParameterDescriptor("radiusX", typeof (int)),
            new ParameterDescriptor("radiusY", typeof (int)),
            new ParameterDescriptor("outlineWidth", typeof (int)),
            new ParameterDescriptor("c", typeof (LuaColor))
          }, false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          ((ContainerGame) obj).drawEllipseOutline((int) pars[0], (int) pars[1], (int) pars[2], (int) pars[3], (int) pars[4], (LuaColor) pars[5]);
          return (object) DynValue.Void;
        }
      }

      private sealed class MTHD_1a9062e2e18b41bfb30f054605038ed0 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_1a9062e2e18b41bfb30f054605038ed0()
        {
          this.Initialize("drawEllipse", false, new ParameterDescriptor[5]
          {
            new ParameterDescriptor("x", typeof (int)),
            new ParameterDescriptor("y", typeof (int)),
            new ParameterDescriptor("radiusX", typeof (int)),
            new ParameterDescriptor("radiusY", typeof (int)),
            new ParameterDescriptor("c", typeof (LuaColor))
          }, false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          ((ContainerGame) obj).drawEllipse((int) pars[0], (int) pars[1], (int) pars[2], (int) pars[3], (LuaColor) pars[4]);
          return (object) DynValue.Void;
        }
      }

      private sealed class MTHD_1f7fda8e388e40b6917c92c928e7d729 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_1f7fda8e388e40b6917c92c928e7d729()
        {
          this.Initialize("blit", false, new ParameterDescriptor[4]
          {
            new ParameterDescriptor("ex", typeof (ExplosionCutout)),
            new ParameterDescriptor("point", typeof (Educative.Point)),
            new ParameterDescriptor("ignoreAlpha", typeof (bool), true, (object) new DefaultValue()),
            new ParameterDescriptor("apply", typeof (bool), true, (object) new DefaultValue())
          }, false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          if (argscount <= 2)
          {
            ((ContainerGame) obj).blit((ExplosionCutout) pars[0], (Educative.Point) pars[1]);
            return (object) DynValue.Void;
          }
          if (argscount <= 3)
          {
            ((ContainerGame) obj).blit((ExplosionCutout) pars[0], (Educative.Point) pars[1], (bool) pars[2]);
            return (object) DynValue.Void;
          }
          ((ContainerGame) obj).blit((ExplosionCutout) pars[0], (Educative.Point) pars[1], (bool) pars[2], (bool) pars[3]);
          return (object) DynValue.Void;
        }
      }

      private sealed class MTHD_10cf632b5eb04982b07402d469e0dc45 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_10cf632b5eb04982b07402d469e0dc45()
        {
          this.Initialize("blitRotate", false, new ParameterDescriptor[5]
          {
            new ParameterDescriptor("ex", typeof (ExplosionCutout)),
            new ParameterDescriptor("point", typeof (Educative.Point)),
            new ParameterDescriptor("rotation", typeof (double), true, (object) new DefaultValue()),
            new ParameterDescriptor("inFront", typeof (bool), true, (object) new DefaultValue()),
            new ParameterDescriptor("apply", typeof (bool), true, (object) new DefaultValue())
          }, false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          if (argscount <= 2)
          {
            ((ContainerGame) obj).blitRotate((ExplosionCutout) pars[0], (Educative.Point) pars[1]);
            return (object) DynValue.Void;
          }
          if (argscount <= 3)
          {
            ((ContainerGame) obj).blitRotate((ExplosionCutout) pars[0], (Educative.Point) pars[1], (double) pars[2]);
            return (object) DynValue.Void;
          }
          if (argscount <= 4)
          {
            ((ContainerGame) obj).blitRotate((ExplosionCutout) pars[0], (Educative.Point) pars[1], (double) pars[2], (bool) pars[3]);
            return (object) DynValue.Void;
          }
          ((ContainerGame) obj).blitRotate((ExplosionCutout) pars[0], (Educative.Point) pars[1], (double) pars[2], (bool) pars[3], (bool) pars[4]);
          return (object) DynValue.Void;
        }
      }

      private sealed class MTHD_679a0c2e75bb4b8c9a5187becb43893f : HardwiredMethodMemberDescriptor
      {
        internal MTHD_679a0c2e75bb4b8c9a5187becb43893f()
        {
          this.Initialize("applyDraw", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          ((ContainerGame) obj).applyDraw();
          return (object) DynValue.Void;
        }
      }

      private sealed class MTHD_148a445314d44e358ae6c30c46bd7d35 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_148a445314d44e358ae6c30c46bd7d35()
        {
          this.Initialize("get_allowBounce", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerGame) obj).allowBounce;
        }
      }

      private sealed class MTHD_b38d54de36f54842979de43247f60b07 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_b38d54de36f54842979de43247f60b07()
        {
          this.Initialize("set_allowBounce", false, new ParameterDescriptor[1]
          {
            new ParameterDescriptor("value", typeof (bool))
          }, false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          ((ContainerGame) obj).allowBounce = (bool) pars[0];
          return (object) null;
        }
      }

      private sealed class MTHD_d579723a54d54f9abb12f19662e9ef1f : HardwiredMethodMemberDescriptor
      {
        internal MTHD_d579723a54d54f9abb12f19662e9ef1f()
        {
          this.Initialize("get_isUsingTouchControls", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerGame) obj).isUsingTouchControls;
        }
      }

      private sealed class MTHD_c3b4029beaa84a43aadcbd2375c63bb0 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_c3b4029beaa84a43aadcbd2375c63bb0()
        {
          this.Initialize("clientRefreshSpells", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          ((ContainerGame) obj).clientRefreshSpells();
          return (object) DynValue.Void;
        }
      }

      private sealed class MTHD_64c48ddff7834c2bb36e7433f44b0a19 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_64c48ddff7834c2bb36e7433f44b0a19()
        {
          this.Initialize("startCoroutine", false, new ParameterDescriptor[4]
          {
            new ParameterDescriptor("script", typeof (Script)),
            new ParameterDescriptor("f", typeof (DynValue)),
            new ParameterDescriptor("runAsCoroutine", typeof (bool), true, (object) new DefaultValue()),
            new ParameterDescriptor("seconds", typeof (float), true, (object) new DefaultValue())
          }, false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          if (argscount <= 2)
          {
            ((ContainerGame) obj).startCoroutine((Script) pars[0], (DynValue) pars[1]);
            return (object) DynValue.Void;
          }
          if (argscount <= 3)
          {
            ((ContainerGame) obj).startCoroutine((Script) pars[0], (DynValue) pars[1], (bool) pars[2]);
            return (object) DynValue.Void;
          }
          ((ContainerGame) obj).startCoroutine((Script) pars[0], (DynValue) pars[1], (bool) pars[2], (float) pars[3]);
          return (object) DynValue.Void;
        }
      }

      private sealed class MTHD_a11fcf72ece24eec9946a215b6f19ada : HardwiredMethodMemberDescriptor
      {
        internal MTHD_a11fcf72ece24eec9946a215b6f19ada()
        {
          this.Initialize("devCommand", false, new ParameterDescriptor[2]
          {
            new ParameterDescriptor("s", typeof (string)),
            new ParameterDescriptor("p", typeof (Educative.Point), true, (object) new DefaultValue())
          }, false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          if (argscount <= 1)
          {
            ((ContainerGame) obj).devCommand((string) pars[0]);
            return (object) DynValue.Void;
          }
          ((ContainerGame) obj).devCommand((string) pars[0], (Educative.Point) pars[1]);
          return (object) DynValue.Void;
        }
      }

      private sealed class MTHD_3bec9b6788934cde9e643463e6cee0c9 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_3bec9b6788934cde9e643463e6cee0c9()
        {
          this.Initialize("win", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          ((ContainerGame) obj).win();
          return (object) DynValue.Void;
        }
      }

      private sealed class MTHD_40d2e0c29d69497d98c29e49c80ce510 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_40d2e0c29d69497d98c29e49c80ce510()
        {
          this.Initialize("lose", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          ((ContainerGame) obj).lose();
          return (object) DynValue.Void;
        }
      }

      private sealed class MTHD_a894af6d56d84f05b49cb40735701dd8 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_a894af6d56d84f05b49cb40735701dd8()
        {
          this.Initialize("nextTurn", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          ((ContainerGame) obj).nextTurn();
          return (object) DynValue.Void;
        }
      }

      private sealed class MTHD_91d9b81cd8b84edd8e9d1d79eb2662df : HardwiredMethodMemberDescriptor
      {
        internal MTHD_91d9b81cd8b84edd8e9d1d79eb2662df()
        {
          this.Initialize("resetMap", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          ((ContainerGame) obj).resetMap();
          return (object) DynValue.Void;
        }
      }

      private sealed class MTHD_efc4ac1a01764642a7cfa1f9e7506fb3 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_efc4ac1a01764642a7cfa1f9e7506fb3()
        {
          this.Initialize("clearMap", false, new ParameterDescriptor[2]
          {
            new ParameterDescriptor("newWidth", typeof (int), true, (object) new DefaultValue()),
            new ParameterDescriptor("newHeight", typeof (int), true, (object) new DefaultValue())
          }, false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          if (argscount <= 0)
          {
            ((ContainerGame) obj).clearMap();
            return (object) DynValue.Void;
          }
          if (argscount <= 1)
          {
            ((ContainerGame) obj).clearMap((int) pars[0]);
            return (object) DynValue.Void;
          }
          ((ContainerGame) obj).clearMap((int) pars[0], (int) pars[1]);
          return (object) DynValue.Void;
        }
      }

      private sealed class MTHD_33cf8ed9e4b743fcb757a204f139d6ef : HardwiredMethodMemberDescriptor
      {
        internal MTHD_33cf8ed9e4b743fcb757a204f139d6ef()
        {
          this.Initialize("getSpell", true, new ParameterDescriptor[1]
          {
            new ParameterDescriptor("name", typeof (string))
          }, false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ContainerGame.getSpell((string) pars[0]);
        }
      }

      private sealed class MTHD_189d4e8f99584e67a6f2dd944c9a5031 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_189d4e8f99584e67a6f2dd944c9a5031()
        {
          this.Initialize("getSpellEnum", true, new ParameterDescriptor[1]
          {
            new ParameterDescriptor("name", typeof (string))
          }, false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ContainerGame.getSpellEnum((string) pars[0]);
        }
      }

      private sealed class MTHD_7790e979eeb54d45b94493b769f8ce45 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_7790e979eeb54d45b94493b769f8ce45()
        {
          this.Initialize("getSpellName", true, new ParameterDescriptor[1]
          {
            new ParameterDescriptor("id", typeof (SpellEnum))
          }, false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ContainerGame.getSpellName((SpellEnum) pars[0]);
        }
      }

      private sealed class MTHD_225d620fdcf44ebf9753fe6c968aedf8 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_225d620fdcf44ebf9753fe6c968aedf8()
        {
          this.Initialize("getMapID", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerGame) obj).getMapID();
        }
      }

      private sealed class MTHD_ffda2158541f4aeba37b0a4ff993acd3 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_ffda2158541f4aeba37b0a4ff993acd3()
        {
          this.Initialize("getMapRealName", true, new ParameterDescriptor[1]
          {
            new ParameterDescriptor("id", typeof (MapEnum))
          }, false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ContainerGame.getMapRealName((MapEnum) pars[0]);
        }
      }

      private sealed class MTHD_5682e71e59e843f986b1378ea2fcfb0b : HardwiredMethodMemberDescriptor
      {
        internal MTHD_5682e71e59e843f986b1378ea2fcfb0b()
        {
          this.Initialize("getMapShortName", true, new ParameterDescriptor[1]
          {
            new ParameterDescriptor("id", typeof (MapEnum))
          }, false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ContainerGame.getMapShortName((MapEnum) pars[0]);
        }
      }

      private sealed class MTHD_c1472cadc4624d96ab8b016b500d286b : HardwiredMethodMemberDescriptor
      {
        internal MTHD_c1472cadc4624d96ab8b016b500d286b()
        {
          this.Initialize("getArmageddonName", false, new ParameterDescriptor[1]
          {
            new ParameterDescriptor("id", typeof (MapEnum))
          }, false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerGame) obj).getArmageddonName((MapEnum) pars[0]);
        }
      }

      private sealed class MTHD_65730f78f9d844c3a3878a02fc4efba9 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_65730f78f9d844c3a3878a02fc4efba9()
        {
          this.Initialize("panCamera", false, new ParameterDescriptor[2]
          {
            new ParameterDescriptor("point", typeof (Educative.Point)),
            new ParameterDescriptor("interuptable", typeof (bool), true, (object) new DefaultValue())
          }, false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          if (argscount <= 1)
          {
            ((ContainerGame) obj).panCamera((Educative.Point) pars[0]);
            return (object) DynValue.Void;
          }
          ((ContainerGame) obj).panCamera((Educative.Point) pars[0], (bool) pars[1]);
          return (object) DynValue.Void;
        }
      }

      private sealed class MTHD_c7682ab05cbf45f8b8eb5942fc7d006a : HardwiredMethodMemberDescriptor
      {
        internal MTHD_c7682ab05cbf45f8b8eb5942fc7d006a()
        {
          this.Initialize("getInputString", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerGame) obj).getInputString();
        }
      }

      private sealed class MTHD_e058e01adf27442397f025b2787e9f96 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_e058e01adf27442397f025b2787e9f96()
        {
          this.Initialize("get_screenWidth", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerGame) obj).screenWidth;
        }
      }

      private sealed class MTHD_e16270a73084487396c5d9f82c2593ab : HardwiredMethodMemberDescriptor
      {
        internal MTHD_e16270a73084487396c5d9f82c2593ab()
        {
          this.Initialize("get_screenHeight", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerGame) obj).screenHeight;
        }
      }

      private sealed class MTHD_760ab6bbb4f74679ad81e61906c67b42 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_760ab6bbb4f74679ad81e61906c67b42()
        {
          this.Initialize("createCreature", false, new ParameterDescriptor[2]
          {
            new ParameterDescriptor("script", typeof (Script)),
            new ParameterDescriptor("summon", typeof (Summon))
          }, false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerGame) obj).createCreature((Script) pars[0], (Summon) pars[1]);
        }
      }

      private sealed class MTHD_33ddc15fe8f748679a2b11da489bf8d8 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_33ddc15fe8f748679a2b11da489bf8d8()
        {
          this.Initialize("clearIndicators", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          ((ContainerGame) obj).clearIndicators();
          return (object) DynValue.Void;
        }
      }

      private sealed class MTHD_1938b4e55b9c47938aa20fa7b8ad9ec7 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_1938b4e55b9c47938aa20fa7b8ad9ec7()
        {
          this.Initialize("createIndicator", false, new ParameterDescriptor[6]
          {
            new ParameterDescriptor("kind", typeof (IndicatorKind)),
            new ParameterDescriptor("point", typeof (Educative.Point)),
            new ParameterDescriptor("hexColor", typeof (string), true, (object) new DefaultValue()),
            new ParameterDescriptor("radius", typeof (double), true, (object) new DefaultValue()),
            new ParameterDescriptor("angle", typeof (double), true, (object) new DefaultValue()),
            new ParameterDescriptor("lifetime", typeof (double), true, (object) new DefaultValue())
          }, false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          if (argscount <= 2)
            return (object) ((ContainerGame) obj).createIndicator((IndicatorKind) pars[0], (Educative.Point) pars[1]);
          if (argscount <= 3)
            return (object) ((ContainerGame) obj).createIndicator((IndicatorKind) pars[0], (Educative.Point) pars[1], (string) pars[2]);
          if (argscount <= 4)
            return (object) ((ContainerGame) obj).createIndicator((IndicatorKind) pars[0], (Educative.Point) pars[1], (string) pars[2], (double) pars[3]);
          return argscount <= 5 ? (object) ((ContainerGame) obj).createIndicator((IndicatorKind) pars[0], (Educative.Point) pars[1], (string) pars[2], (double) pars[3], (double) pars[4]) : (object) ((ContainerGame) obj).createIndicator((IndicatorKind) pars[0], (Educative.Point) pars[1], (string) pars[2], (double) pars[3], (double) pars[4], (double) pars[5]);
        }
      }

      private sealed class MTHD_ceeb06bae7f14611bcdd0e344c79cef1 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_ceeb06bae7f14611bcdd0e344c79cef1()
        {
          this.Initialize("Equals", false, new ParameterDescriptor[1]
          {
            new ParameterDescriptor("obj", typeof (object))
          }, false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) obj.Equals(pars[0]);
        }
      }

      private sealed class MTHD_a446a7970fa848ff8ef7349d2989b3da : HardwiredMethodMemberDescriptor
      {
        internal MTHD_a446a7970fa848ff8ef7349d2989b3da()
        {
          this.Initialize("GetHashCode", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) obj.GetHashCode();
        }
      }

      private sealed class MTHD_a463a426c6c3461aa2b91b52868db866 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_a463a426c6c3461aa2b91b52868db866()
        {
          this.Initialize("GetType", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) obj.GetType();
        }
      }

      private sealed class MTHD_623628d6271e4229bbf93df0f30ca9ca : HardwiredMethodMemberDescriptor
      {
        internal MTHD_623628d6271e4229bbf93df0f30ca9ca()
        {
          this.Initialize("ToString", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) obj.ToString();
        }
      }

      private sealed class PROP_7434347434e54d1998a510cc35895058 : HardwiredMemberDescriptor
      {
        internal PROP_7434347434e54d1998a510cc35895058()
          : base(typeof (int), "turn", false, MemberDescriptorAccess.CanRead | MemberDescriptorAccess.CanWrite)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((ContainerGame) obj).turn;
        }

        protected override void SetValueImpl(Script script, object obj, object value)
        {
          ((ContainerGame) obj).turn = (int) value;
        }
      }

      private sealed class PROP_c6626db76bed4ac3bc5c4f392825fc75 : HardwiredMemberDescriptor
      {
        internal PROP_c6626db76bed4ac3bc5c4f392825fc75()
          : base(typeof (int), "timeLimit", false, MemberDescriptorAccess.CanRead | MemberDescriptorAccess.CanWrite)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((ContainerGame) obj).timeLimit;
        }

        protected override void SetValueImpl(Script script, object obj, object value)
        {
          ((ContainerGame) obj).timeLimit = (int) value;
        }
      }

      private sealed class PROP_cfca68845f004ff291dabc409fd25f41 : HardwiredMemberDescriptor
      {
        internal PROP_cfca68845f004ff291dabc409fd25f41()
          : base(typeof (double), "totalTimeElapsed", false, MemberDescriptorAccess.CanRead)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((ContainerGame) obj).totalTimeElapsed;
        }
      }

      private sealed class PROP_6c708f3327334686afb0412571852645 : HardwiredMemberDescriptor
      {
        internal PROP_6c708f3327334686afb0412571852645()
          : base(typeof (double), "turnTime", false, MemberDescriptorAccess.CanRead)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((ContainerGame) obj).turnTime;
        }
      }

      private sealed class PROP_e742ac8bd45b49aab809b17909c52d31 : HardwiredMemberDescriptor
      {
        internal PROP_e742ac8bd45b49aab809b17909c52d31()
          : base(typeof (int), "frame", false, MemberDescriptorAccess.CanRead)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((ContainerGame) obj).frame;
        }
      }

      private sealed class PROP_9cc47f40d6824b85a6c864c029ad0d2e : HardwiredMemberDescriptor
      {
        internal PROP_9cc47f40d6824b85a6c864c029ad0d2e()
          : base(typeof (bool), "winOnDeath", false, MemberDescriptorAccess.CanRead | MemberDescriptorAccess.CanWrite)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((ContainerGame) obj).winOnDeath;
        }

        protected override void SetValueImpl(Script script, object obj, object value)
        {
          ((ContainerGame) obj).winOnDeath = (bool) value;
        }
      }

      private sealed class PROP_96fdfec8906242b3bee8de3e3dae1fc1 : HardwiredMemberDescriptor
      {
        internal PROP_96fdfec8906242b3bee8de3e3dae1fc1()
          : base(typeof (bool), "allowMovement", false, MemberDescriptorAccess.CanRead | MemberDescriptorAccess.CanWrite)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((ContainerGame) obj).allowMovement;
        }

        protected override void SetValueImpl(Script script, object obj, object value)
        {
          ((ContainerGame) obj).allowMovement = (bool) value;
        }
      }

      private sealed class PROP_2b6e616675c44b54a90b3aeb4d3e34f8 : HardwiredMemberDescriptor
      {
        internal PROP_2b6e616675c44b54a90b3aeb4d3e34f8()
          : base(typeof (bool), "allowInput", false, MemberDescriptorAccess.CanRead | MemberDescriptorAccess.CanWrite)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((ContainerGame) obj).allowInput;
        }

        protected override void SetValueImpl(Script script, object obj, object value)
        {
          ((ContainerGame) obj).allowInput = (bool) value;
        }
      }

      private sealed class PROP_97e7b4510e104f11acef38f865bade5c : HardwiredMemberDescriptor
      {
        internal PROP_97e7b4510e104f11acef38f865bade5c()
          : base(typeof (bool), "allowCallbacks", false, MemberDescriptorAccess.CanRead | MemberDescriptorAccess.CanWrite)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((ContainerGame) obj).allowCallbacks;
        }

        protected override void SetValueImpl(Script script, object obj, object value)
        {
          ((ContainerGame) obj).allowCallbacks = (bool) value;
        }
      }

      private sealed class PROP_e37d8eb9d5fd48f0af49be4f423eb97b : HardwiredMemberDescriptor
      {
        internal PROP_e37d8eb9d5fd48f0af49be4f423eb97b()
          : base(typeof (bool), "allowSkipTurn", false, MemberDescriptorAccess.CanRead | MemberDescriptorAccess.CanWrite)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((ContainerGame) obj).allowSkipTurn;
        }

        protected override void SetValueImpl(Script script, object obj, object value)
        {
          ((ContainerGame) obj).allowSkipTurn = (bool) value;
        }
      }

      private sealed class PROP_ce3735ff3248494d8145dbff1b2aa91d : HardwiredMemberDescriptor
      {
        internal PROP_ce3735ff3248494d8145dbff1b2aa91d()
          : base(typeof (bool), "terrainDestruction", false, MemberDescriptorAccess.CanRead | MemberDescriptorAccess.CanWrite)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((ContainerGame) obj).terrainDestruction;
        }

        protected override void SetValueImpl(Script script, object obj, object value)
        {
          ((ContainerGame) obj).terrainDestruction = (bool) value;
        }
      }

      private sealed class PROP_530b019bdbdf4ab3aa572ef2f75641d3 : HardwiredMemberDescriptor
      {
        internal PROP_530b019bdbdf4ab3aa572ef2f75641d3()
          : base(typeof (bool), "takeDamage", false, MemberDescriptorAccess.CanRead | MemberDescriptorAccess.CanWrite)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((ContainerGame) obj).takeDamage;
        }

        protected override void SetValueImpl(Script script, object obj, object value)
        {
          ((ContainerGame) obj).takeDamage = (bool) value;
        }
      }

      private sealed class PROP_abb11fefe97545d6b050571333c4f819 : HardwiredMemberDescriptor
      {
        internal PROP_abb11fefe97545d6b050571333c4f819()
          : base(typeof (MapEnum), "armageddon", false, MemberDescriptorAccess.CanRead | MemberDescriptorAccess.CanWrite)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((ContainerGame) obj).armageddon;
        }

        protected override void SetValueImpl(Script script, object obj, object value)
        {
          ((ContainerGame) obj).armageddon = (MapEnum) value;
        }
      }

      private sealed class PROP_9875bc06b48741d3a0508d43daf59376 : HardwiredMemberDescriptor
      {
        internal PROP_9875bc06b48741d3a0508d43daf59376()
          : base(typeof (int), "armageddonTurn", false, MemberDescriptorAccess.CanRead | MemberDescriptorAccess.CanWrite)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((ContainerGame) obj).armageddonTurn;
        }

        protected override void SetValueImpl(Script script, object obj, object value)
        {
          ((ContainerGame) obj).armageddonTurn = (int) value;
        }
      }

      private sealed class PROP_3a4564d541114f90983407334899251e : HardwiredMemberDescriptor
      {
        internal PROP_3a4564d541114f90983407334899251e()
          : base(typeof (bool), "waiting", false, MemberDescriptorAccess.CanRead)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((ContainerGame) obj).waiting;
        }
      }

      private sealed class PROP_4839a8de19cc4c2192afff8ff3d3f82f : HardwiredMemberDescriptor
      {
        internal PROP_4839a8de19cc4c2192afff8ff3d3f82f()
          : base(typeof (bool), "paused", false, MemberDescriptorAccess.CanRead | MemberDescriptorAccess.CanWrite)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((ContainerGame) obj).paused;
        }

        protected override void SetValueImpl(Script script, object obj, object value)
        {
          ((ContainerGame) obj).paused = (bool) value;
        }
      }

      private sealed class PROP_d41f993c23c44250bb253155cda158b9 : HardwiredMemberDescriptor
      {
        internal PROP_d41f993c23c44250bb253155cda158b9()
          : base(typeof (bool), "busy", false, MemberDescriptorAccess.CanRead)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((ContainerGame) obj).busy;
        }
      }

      private sealed class PROP_d6d1c3c775a3489db1f9d62229686180 : HardwiredMemberDescriptor
      {
        internal PROP_d6d1c3c775a3489db1f9d62229686180()
          : base(typeof (bool), "ongoing", false, MemberDescriptorAccess.CanRead)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((ContainerGame) obj).ongoing;
        }
      }

      private sealed class PROP_81a42c6ca59845b39e6b73f8d9f6ce49 : HardwiredMemberDescriptor
      {
        internal PROP_81a42c6ca59845b39e6b73f8d9f6ce49()
          : base(typeof (double), "gravity", false, MemberDescriptorAccess.CanRead | MemberDescriptorAccess.CanWrite)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((ContainerGame) obj).gravity;
        }

        protected override void SetValueImpl(Script script, object obj, object value)
        {
          ((ContainerGame) obj).gravity = (double) value;
        }
      }

      private sealed class PROP_df80247f9f6646f7b83ae5bfd669e763 : HardwiredMemberDescriptor
      {
        internal PROP_df80247f9f6646f7b83ae5bfd669e763()
          : base(typeof (bool), "wind", false, MemberDescriptorAccess.CanRead | MemberDescriptorAccess.CanWrite)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((ContainerGame) obj).wind;
        }

        protected override void SetValueImpl(Script script, object obj, object value)
        {
          ((ContainerGame) obj).wind = (bool) value;
        }
      }

      private sealed class PROP_fb8ad7f5552e4a8b9f70c67068b51382 : HardwiredMemberDescriptor
      {
        internal PROP_fb8ad7f5552e4a8b9f70c67068b51382()
          : base(typeof (double), "windDir", false, MemberDescriptorAccess.CanRead | MemberDescriptorAccess.CanWrite)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((ContainerGame) obj).windDir;
        }

        protected override void SetValueImpl(Script script, object obj, object value)
        {
          ((ContainerGame) obj).windDir = (double) value;
        }
      }

      private sealed class PROP_2ccd739731474540920d9427617e0ea4 : HardwiredMemberDescriptor
      {
        internal PROP_2ccd739731474540920d9427617e0ea4()
          : base(typeof (double), "windPower", false, MemberDescriptorAccess.CanRead | MemberDescriptorAccess.CanWrite)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((ContainerGame) obj).windPower;
        }

        protected override void SetValueImpl(Script script, object obj, object value)
        {
          ((ContainerGame) obj).windPower = (double) value;
        }
      }

      private sealed class PROP_f0cbc87eec604462a60a8aa1dd1880b3 : HardwiredMemberDescriptor
      {
        internal PROP_f0cbc87eec604462a60a8aa1dd1880b3()
          : base(typeof (int), "width", false, MemberDescriptorAccess.CanRead)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((ContainerGame) obj).width;
        }
      }

      private sealed class PROP_19fdc6e91d4f408dae226d550f8bf100 : HardwiredMemberDescriptor
      {
        internal PROP_19fdc6e91d4f408dae226d550f8bf100()
          : base(typeof (int), "height", false, MemberDescriptorAccess.CanRead)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((ContainerGame) obj).height;
        }
      }

      private sealed class PROP_b6a69f04d9574552947c6f2b89d67408 : HardwiredMemberDescriptor
      {
        internal PROP_b6a69f04d9574552947c6f2b89d67408()
          : base(typeof (Educative.Point), "mousePosition", false, MemberDescriptorAccess.CanRead)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((ContainerGame) obj).mousePosition;
        }
      }

      private sealed class PROP_e0a2aab2fa3141b68df6f5cae8cdb6d6 : HardwiredMemberDescriptor
      {
        internal PROP_e0a2aab2fa3141b68df6f5cae8cdb6d6()
          : base(typeof (bool), "mouseOverUI", false, MemberDescriptorAccess.CanRead)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((ContainerGame) obj).mouseOverUI;
        }
      }

      private sealed class PROP_f1365632ae8b42a283a62332f78fa278 : HardwiredMemberDescriptor
      {
        internal PROP_f1365632ae8b42a283a62332f78fa278()
          : base(typeof (Educative.Point), "cameraPosition", false, MemberDescriptorAccess.CanRead | MemberDescriptorAccess.CanWrite)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((ContainerGame) obj).cameraPosition;
        }

        protected override void SetValueImpl(Script script, object obj, object value)
        {
          ((ContainerGame) obj).cameraPosition = (Educative.Point) value;
        }
      }

      private sealed class PROP_6f546c98923a4fb4846a90cfe29d5a15 : HardwiredMemberDescriptor
      {
        internal PROP_6f546c98923a4fb4846a90cfe29d5a15()
          : base(typeof (double), "cameraZoom", false, MemberDescriptorAccess.CanRead | MemberDescriptorAccess.CanWrite)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((ContainerGame) obj).cameraZoom;
        }

        protected override void SetValueImpl(Script script, object obj, object value)
        {
          ((ContainerGame) obj).cameraZoom = (double) value;
        }
      }

      private sealed class PROP_4a1c4583474b45c2b49afa892388e1de : HardwiredMemberDescriptor
      {
        internal PROP_4a1c4583474b45c2b49afa892388e1de()
          : base(typeof (bool), "allowBounce", false, MemberDescriptorAccess.CanRead | MemberDescriptorAccess.CanWrite)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((ContainerGame) obj).allowBounce;
        }

        protected override void SetValueImpl(Script script, object obj, object value)
        {
          ((ContainerGame) obj).allowBounce = (bool) value;
        }
      }

      private sealed class PROP_078d3997d0854443b08b9398f7c55c54 : HardwiredMemberDescriptor
      {
        internal PROP_078d3997d0854443b08b9398f7c55c54()
          : base(typeof (bool), "isUsingTouchControls", false, MemberDescriptorAccess.CanRead)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((ContainerGame) obj).isUsingTouchControls;
        }
      }

      private sealed class PROP_2480385c59b14b3b8fbf664dae4fe6cc : HardwiredMemberDescriptor
      {
        internal PROP_2480385c59b14b3b8fbf664dae4fe6cc()
          : base(typeof (double), "screenWidth", false, MemberDescriptorAccess.CanRead)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((ContainerGame) obj).screenWidth;
        }
      }

      private sealed class PROP_de2a3b694fc946488995369c14613ed8 : HardwiredMemberDescriptor
      {
        internal PROP_de2a3b694fc946488995369c14613ed8()
          : base(typeof (double), "screenHeight", false, MemberDescriptorAccess.CanRead)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((ContainerGame) obj).screenHeight;
        }
      }
    }

    private sealed class TYPE_31a3a41981ed427cb292bb7317e1c94b : HardwiredUserDataDescriptor
    {
      internal TYPE_31a3a41981ed427cb292bb7317e1c94b()
        : base(typeof (ContainerPlayer))
      {
        this.AddMember("get_localTurn", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_localTurn", typeof (ContainerPlayer), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_31a3a41981ed427cb292bb7317e1c94b.MTHD_c85d8b2ed3f74cbd9ce6f91298d27e82()
        }));
        this.AddMember("set_localTurn", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("set_localTurn", typeof (ContainerPlayer), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_31a3a41981ed427cb292bb7317e1c94b.MTHD_4274da4724554944bd16a16160ba1d00()
        }));
        this.AddMember("get_name", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_name", typeof (ContainerPlayer), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_31a3a41981ed427cb292bb7317e1c94b.MTHD_eb3f56b2cd7f4a5fbc96e9aea5d506f6()
        }));
        this.AddMember("get_team", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_team", typeof (ContainerPlayer), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_31a3a41981ed427cb292bb7317e1c94b.MTHD_6c34e22452e04eb6912c085f07f3b959()
        }));
        this.AddMember("get_yourTurn", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_yourTurn", typeof (ContainerPlayer), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_31a3a41981ed427cb292bb7317e1c94b.MTHD_f858229607d340ba8b63cfbdb1af1c4d()
        }));
        this.AddMember("getCreatures", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("getCreatures", typeof (ContainerPlayer), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_31a3a41981ed427cb292bb7317e1c94b.MTHD_e45a5350c89141ea9267f51926eb8e00()
        }));
        this.AddMember("getCreature", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("getCreature", typeof (ContainerPlayer), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_31a3a41981ed427cb292bb7317e1c94b.MTHD_1a9db4d2be1d40b5979e8b490e868b6b()
        }));
        this.AddMember("getMinionCount", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("getMinionCount", typeof (ContainerPlayer), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_31a3a41981ed427cb292bb7317e1c94b.MTHD_d3b1456dbc2f44a9b2c28cfa1cf41ecd()
        }));
        this.AddMember("GetHashCode", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("GetHashCode", typeof (ContainerPlayer), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_31a3a41981ed427cb292bb7317e1c94b.MTHD_2d3d4b90b29a4833929b34b012e6e7fd()
        }));
        this.AddMember("Equals", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("Equals", typeof (ContainerPlayer), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_31a3a41981ed427cb292bb7317e1c94b.MTHD_c81b7d5b429c49bb8640dd3bd519e2a6()
        }));
        this.AddMember("GetType", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("GetType", typeof (ContainerPlayer), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_31a3a41981ed427cb292bb7317e1c94b.MTHD_f63138f8d5a4479aaa199bda3b3ce4dd()
        }));
        this.AddMember("ToString", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("ToString", typeof (ContainerPlayer), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_31a3a41981ed427cb292bb7317e1c94b.MTHD_9c54f21c0e0a4b43ac0af2c353d58200()
        }));
        this.AddMember("localTurn", (IMemberDescriptor) new Bridge.TYPE_31a3a41981ed427cb292bb7317e1c94b.PROP_b7138e63d3dc4098afcf0f5fa5eb7669());
        this.AddMember("name", (IMemberDescriptor) new Bridge.TYPE_31a3a41981ed427cb292bb7317e1c94b.PROP_c772bfdecb944438bc41d4ec07f5395a());
        this.AddMember("team", (IMemberDescriptor) new Bridge.TYPE_31a3a41981ed427cb292bb7317e1c94b.PROP_e8466a47b3654ee4849508ad842c079b());
        this.AddMember("yourTurn", (IMemberDescriptor) new Bridge.TYPE_31a3a41981ed427cb292bb7317e1c94b.PROP_4036d241ec64467e885435cf2882f78d());
      }

      private sealed class MTHD_c85d8b2ed3f74cbd9ce6f91298d27e82 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_c85d8b2ed3f74cbd9ce6f91298d27e82()
        {
          this.Initialize("get_localTurn", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerPlayer) obj).localTurn;
        }
      }

      private sealed class MTHD_4274da4724554944bd16a16160ba1d00 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_4274da4724554944bd16a16160ba1d00()
        {
          this.Initialize("set_localTurn", false, new ParameterDescriptor[1]
          {
            new ParameterDescriptor("value", typeof (int))
          }, false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          ((ContainerPlayer) obj).localTurn = (int) pars[0];
          return (object) null;
        }
      }

      private sealed class MTHD_eb3f56b2cd7f4a5fbc96e9aea5d506f6 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_eb3f56b2cd7f4a5fbc96e9aea5d506f6()
        {
          this.Initialize("get_name", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerPlayer) obj).name;
        }
      }

      private sealed class MTHD_6c34e22452e04eb6912c085f07f3b959 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_6c34e22452e04eb6912c085f07f3b959()
        {
          this.Initialize("get_team", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerPlayer) obj).team;
        }
      }

      private sealed class MTHD_f858229607d340ba8b63cfbdb1af1c4d : HardwiredMethodMemberDescriptor
      {
        internal MTHD_f858229607d340ba8b63cfbdb1af1c4d()
        {
          this.Initialize("get_yourTurn", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerPlayer) obj).yourTurn;
        }
      }

      private sealed class MTHD_e45a5350c89141ea9267f51926eb8e00 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_e45a5350c89141ea9267f51926eb8e00()
        {
          this.Initialize("getCreatures", false, new ParameterDescriptor[1]
          {
            new ParameterDescriptor("script", typeof (Script))
          }, false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerPlayer) obj).getCreatures((Script) pars[0]);
        }
      }

      private sealed class MTHD_1a9db4d2be1d40b5979e8b490e868b6b : HardwiredMethodMemberDescriptor
      {
        internal MTHD_1a9db4d2be1d40b5979e8b490e868b6b()
        {
          this.Initialize("getCreature", false, new ParameterDescriptor[1]
          {
            new ParameterDescriptor("index", typeof (int))
          }, false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerPlayer) obj).getCreature((int) pars[0]);
        }
      }

      private sealed class MTHD_d3b1456dbc2f44a9b2c28cfa1cf41ecd : HardwiredMethodMemberDescriptor
      {
        internal MTHD_d3b1456dbc2f44a9b2c28cfa1cf41ecd()
        {
          this.Initialize("getMinionCount", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerPlayer) obj).getMinionCount();
        }
      }

      private sealed class MTHD_2d3d4b90b29a4833929b34b012e6e7fd : HardwiredMethodMemberDescriptor
      {
        internal MTHD_2d3d4b90b29a4833929b34b012e6e7fd()
        {
          this.Initialize("GetHashCode", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerPlayer) obj).GetHashCode();
        }
      }

      private sealed class MTHD_c81b7d5b429c49bb8640dd3bd519e2a6 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_c81b7d5b429c49bb8640dd3bd519e2a6()
        {
          this.Initialize("Equals", false, new ParameterDescriptor[1]
          {
            new ParameterDescriptor("obj", typeof (object))
          }, false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) obj.Equals(pars[0]);
        }
      }

      private sealed class MTHD_f63138f8d5a4479aaa199bda3b3ce4dd : HardwiredMethodMemberDescriptor
      {
        internal MTHD_f63138f8d5a4479aaa199bda3b3ce4dd()
        {
          this.Initialize("GetType", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) obj.GetType();
        }
      }

      private sealed class MTHD_9c54f21c0e0a4b43ac0af2c353d58200 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_9c54f21c0e0a4b43ac0af2c353d58200()
        {
          this.Initialize("ToString", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) obj.ToString();
        }
      }

      private sealed class PROP_b7138e63d3dc4098afcf0f5fa5eb7669 : HardwiredMemberDescriptor
      {
        internal PROP_b7138e63d3dc4098afcf0f5fa5eb7669()
          : base(typeof (int), "localTurn", false, MemberDescriptorAccess.CanRead | MemberDescriptorAccess.CanWrite)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((ContainerPlayer) obj).localTurn;
        }

        protected override void SetValueImpl(Script script, object obj, object value)
        {
          ((ContainerPlayer) obj).localTurn = (int) value;
        }
      }

      private sealed class PROP_c772bfdecb944438bc41d4ec07f5395a : HardwiredMemberDescriptor
      {
        internal PROP_c772bfdecb944438bc41d4ec07f5395a()
          : base(typeof (string), "name", false, MemberDescriptorAccess.CanRead)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((ContainerPlayer) obj).name;
        }
      }

      private sealed class PROP_e8466a47b3654ee4849508ad842c079b : HardwiredMemberDescriptor
      {
        internal PROP_e8466a47b3654ee4849508ad842c079b()
          : base(typeof (int), "team", false, MemberDescriptorAccess.CanRead)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((ContainerPlayer) obj).team;
        }
      }

      private sealed class PROP_4036d241ec64467e885435cf2882f78d : HardwiredMemberDescriptor
      {
        internal PROP_4036d241ec64467e885435cf2882f78d()
          : base(typeof (bool), "yourTurn", false, MemberDescriptorAccess.CanRead)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((ContainerPlayer) obj).yourTurn;
        }
      }
    }

    private sealed class TYPE_e03d96fda4a7401886a92c78fd5b3e2b : HardwiredUserDataDescriptor
    {
      internal TYPE_e03d96fda4a7401886a92c78fd5b3e2b()
        : base(typeof (ContainerCreature))
      {
        this.AddMember("get_team", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_team", typeof (ContainerCreature), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_e03d96fda4a7401886a92c78fd5b3e2b.MTHD_1b021e81186d47e2848b27af910bce27()
        }));
        this.AddMember("get_name", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_name", typeof (ContainerCreature), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_e03d96fda4a7401886a92c78fd5b3e2b.MTHD_4509fa7361fc41748fbc58f3b3a7a500()
        }));
        this.AddMember("get_spellEnum", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_spellEnum", typeof (ContainerCreature), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_e03d96fda4a7401886a92c78fd5b3e2b.MTHD_734132091e224afbbf501436d89c2b26()
        }));
        this.AddMember("get_health", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_health", typeof (ContainerCreature), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_e03d96fda4a7401886a92c78fd5b3e2b.MTHD_5ced4addb8f44cd8996b6e78ea45270e()
        }));
        this.AddMember("set_health", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("set_health", typeof (ContainerCreature), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_e03d96fda4a7401886a92c78fd5b3e2b.MTHD_c797a90665624edb8cb1298d468e97b2()
        }));
        this.AddMember("get_maxHealth", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_maxHealth", typeof (ContainerCreature), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_e03d96fda4a7401886a92c78fd5b3e2b.MTHD_926753561e0f4b56b94d0dd246520334()
        }));
        this.AddMember("set_maxHealth", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("set_maxHealth", typeof (ContainerCreature), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_e03d96fda4a7401886a92c78fd5b3e2b.MTHD_abc01d95272b474583d4ea267902e137()
        }));
        this.AddMember("get_healthAndTower", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_healthAndTower", typeof (ContainerCreature), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_e03d96fda4a7401886a92c78fd5b3e2b.MTHD_7217f8620a884d43851ba9cdfc96763d()
        }));
        this.AddMember("get_shield", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_shield", typeof (ContainerCreature), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_e03d96fda4a7401886a92c78fd5b3e2b.MTHD_8ecba28ba00543faa7cfd297373b6d08()
        }));
        this.AddMember("set_shield", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("set_shield", typeof (ContainerCreature), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_e03d96fda4a7401886a92c78fd5b3e2b.MTHD_54befc0b2f4d44558ad22d6bde321ea9()
        }));
        this.AddMember("get_x", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_x", typeof (ContainerCreature), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_e03d96fda4a7401886a92c78fd5b3e2b.MTHD_2a19ceb7c61b495c92a969b8f5a3d8d4()
        }));
        this.AddMember("set_x", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("set_x", typeof (ContainerCreature), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_e03d96fda4a7401886a92c78fd5b3e2b.MTHD_de530ce92218479ba976fd564a6d5d81()
        }));
        this.AddMember("get_y", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_y", typeof (ContainerCreature), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_e03d96fda4a7401886a92c78fd5b3e2b.MTHD_77c8d6ce0879450dbb4102c9a0a0149d()
        }));
        this.AddMember("set_y", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("set_y", typeof (ContainerCreature), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_e03d96fda4a7401886a92c78fd5b3e2b.MTHD_581e155cc2b14cc38b7b81c1cd8268c8()
        }));
        this.AddMember("get_position", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_position", typeof (ContainerCreature), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_e03d96fda4a7401886a92c78fd5b3e2b.MTHD_a976238bac544f41946450f9f44ca093()
        }));
        this.AddMember("set_position", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("set_position", typeof (ContainerCreature), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_e03d96fda4a7401886a92c78fd5b3e2b.MTHD_f0d97834a2e2450fa33964760b4c9843()
        }));
        this.AddMember("get_parent", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_parent", typeof (ContainerCreature), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_e03d96fda4a7401886a92c78fd5b3e2b.MTHD_29af5d23be4e4ff3a4a504ec9d231d01()
        }));
        this.AddMember("set_parent", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("set_parent", typeof (ContainerCreature), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_e03d96fda4a7401886a92c78fd5b3e2b.MTHD_1460e51ae11647ca8ebdc9cbd5c2479d()
        }));
        this.AddMember("get_weight", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_weight", typeof (ContainerCreature), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_e03d96fda4a7401886a92c78fd5b3e2b.MTHD_d5b7015f52cb42b8bed3cd096d85b17b()
        }));
        this.AddMember("set_weight", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("set_weight", typeof (ContainerCreature), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_e03d96fda4a7401886a92c78fd5b3e2b.MTHD_11a2983cd24641329baf9859f075bbb6()
        }));
        this.AddMember("get_radius", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_radius", typeof (ContainerCreature), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_e03d96fda4a7401886a92c78fd5b3e2b.MTHD_f5cf8f3638ea431fb4185027f7a8f6dc()
        }));
        this.AddMember("set_radius", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("set_radius", typeof (ContainerCreature), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_e03d96fda4a7401886a92c78fd5b3e2b.MTHD_2029bd75ce294914b30b11c1d4c0d712()
        }));
        this.AddMember("get_inTower", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_inTower", typeof (ContainerCreature), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_e03d96fda4a7401886a92c78fd5b3e2b.MTHD_2cf695217d754221855529bb8ad13470()
        }));
        this.AddMember("get_isMoving", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_isMoving", typeof (ContainerCreature), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_e03d96fda4a7401886a92c78fd5b3e2b.MTHD_192cadc2016b4b2f86da65bc434caf82()
        }));
        this.AddMember("get_stunned", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_stunned", typeof (ContainerCreature), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_e03d96fda4a7401886a92c78fd5b3e2b.MTHD_dc9910d9edb741a4bf5ab612d444a966()
        }));
        this.AddMember("set_stunned", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("set_stunned", typeof (ContainerCreature), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_e03d96fda4a7401886a92c78fd5b3e2b.MTHD_4c4f6cc4915742f0835fa3ddb7d3c53c()
        }));
        this.AddMember("get_superStunned", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_superStunned", typeof (ContainerCreature), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_e03d96fda4a7401886a92c78fd5b3e2b.MTHD_88a65e1f244344dda183bc977dd71d3b()
        }));
        this.AddMember("set_superStunned", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("set_superStunned", typeof (ContainerCreature), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_e03d96fda4a7401886a92c78fd5b3e2b.MTHD_2162d803ba264f4ebcefc635a1a2b7fe()
        }));
        this.AddMember("get_waterWalking", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_waterWalking", typeof (ContainerCreature), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_e03d96fda4a7401886a92c78fd5b3e2b.MTHD_592ebe80ca894fc18aa4ac1edf9e7c9d()
        }));
        this.AddMember("set_waterWalking", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("set_waterWalking", typeof (ContainerCreature), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_e03d96fda4a7401886a92c78fd5b3e2b.MTHD_9d7b0be6fae34d20a191fb8bc350dd87()
        }));
        this.AddMember("get_frostWalking", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_frostWalking", typeof (ContainerCreature), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_e03d96fda4a7401886a92c78fd5b3e2b.MTHD_27584d2f6b2b4c8dbf72e04f340c87ad()
        }));
        this.AddMember("set_frostWalking", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("set_frostWalking", typeof (ContainerCreature), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_e03d96fda4a7401886a92c78fd5b3e2b.MTHD_49a5d00af5544f2dbc7b6b6c8be23dc0()
        }));
        this.AddMember("get_inWater", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_inWater", typeof (ContainerCreature), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_e03d96fda4a7401886a92c78fd5b3e2b.MTHD_b2fc9f2714454fd6bcd0ccb043e54b5a()
        }));
        this.AddMember("get_diesInWater", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_diesInWater", typeof (ContainerCreature), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_e03d96fda4a7401886a92c78fd5b3e2b.MTHD_80d3101a7f5748f5bfffa7546da03b9b()
        }));
        this.AddMember("set_diesInWater", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("set_diesInWater", typeof (ContainerCreature), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_e03d96fda4a7401886a92c78fd5b3e2b.MTHD_f40f5c17dd804a4f814033ae6533b2bb()
        }));
        this.AddMember("get_type", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_type", typeof (ContainerCreature), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_e03d96fda4a7401886a92c78fd5b3e2b.MTHD_2576acc2e13344d69af1e22f0238d941()
        }));
        this.AddMember("set_type", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("set_type", typeof (ContainerCreature), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_e03d96fda4a7401886a92c78fd5b3e2b.MTHD_d29308ae8db54ea79eb135ac9b9b0056()
        }));
        this.AddMember("get_race", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_race", typeof (ContainerCreature), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_e03d96fda4a7401886a92c78fd5b3e2b.MTHD_c8679a432a144c6b8e2ab3d57de7744b()
        }));
        this.AddMember("set_race", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("set_race", typeof (ContainerCreature), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_e03d96fda4a7401886a92c78fd5b3e2b.MTHD_439d6871a7fc4395ba0076268ff62a0c()
        }));
        this.AddMember("get_yourTurn", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_yourTurn", typeof (ContainerCreature), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_e03d96fda4a7401886a92c78fd5b3e2b.MTHD_05b1eb120cda4c06ae9ccd962afe3314()
        }));
        this.AddMember("get_isRider", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_isRider", typeof (ContainerCreature), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_e03d96fda4a7401886a92c78fd5b3e2b.MTHD_ca2eaea994d94692b2a1fd9d73d15e90()
        }));
        this.AddMember("get_isMounted", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_isMounted", typeof (ContainerCreature), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_e03d96fda4a7401886a92c78fd5b3e2b.MTHD_f75a0d1a3d6841d4824b7b34edf5e0e3()
        }));
        this.AddMember("get_isDead", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_isDead", typeof (ContainerCreature), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_e03d96fda4a7401886a92c78fd5b3e2b.MTHD_12eac8d7a0d349a3b9960e02dd50c459()
        }));
        this.AddMember("get_isPawn", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_isPawn", typeof (ContainerCreature), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_e03d96fda4a7401886a92c78fd5b3e2b.MTHD_2879171c31974551b7856fbb9e010c96()
        }));
        this.AddMember("get_isFlying", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_isFlying", typeof (ContainerCreature), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_e03d96fda4a7401886a92c78fd5b3e2b.MTHD_8983e51d14ad4401a0c49507781b47c2()
        }));
        this.AddMember("set_isFlying", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("set_isFlying", typeof (ContainerCreature), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_e03d96fda4a7401886a92c78fd5b3e2b.MTHD_7707ccf686304c2db8032845a9cc49b4()
        }));
        this.AddMember("get_isMountable", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_isMountable", typeof (ContainerCreature), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_e03d96fda4a7401886a92c78fd5b3e2b.MTHD_b86bfc5c1c494c1f8e525b8c7feea5ca()
        }));
        this.AddMember("get_canMount", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_canMount", typeof (ContainerCreature), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_e03d96fda4a7401886a92c78fd5b3e2b.MTHD_33968aeb246e4b048491a94740e5ae66()
        }));
        this.AddMember("get_arcaneMonster", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_arcaneMonster", typeof (ContainerCreature), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_e03d96fda4a7401886a92c78fd5b3e2b.MTHD_50441c2c9ce94882a19cf4661bc51d0e()
        }));
        this.AddMember("set_arcaneMonster", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("set_arcaneMonster", typeof (ContainerCreature), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_e03d96fda4a7401886a92c78fd5b3e2b.MTHD_6aa7bbd61d2e4dd695d2bf6b87fecb2e()
        }));
        this.AddMember("get_takeDamage", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_takeDamage", typeof (ContainerCreature), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_e03d96fda4a7401886a92c78fd5b3e2b.MTHD_9dfe8cdb278b463ca46ff190205abd3e()
        }));
        this.AddMember("set_takeDamage", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("set_takeDamage", typeof (ContainerCreature), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_e03d96fda4a7401886a92c78fd5b3e2b.MTHD_b83be496bdf342b98c757ff8e101486f()
        }));
        this.AddMember("getRider", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("getRider", typeof (ContainerCreature), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_e03d96fda4a7401886a92c78fd5b3e2b.MTHD_0786ab55f6f2409f9b2132ea047fe5e4()
        }));
        this.AddMember("getMount", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("getMount", typeof (ContainerCreature), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_e03d96fda4a7401886a92c78fd5b3e2b.MTHD_02fb79538dc84bcaabe728a45bfd622e()
        }));
        this.AddMember("getTower", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("getTower", typeof (ContainerCreature), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_e03d96fda4a7401886a92c78fd5b3e2b.MTHD_a15ad6af07524cff8d36fd3746d29d66()
        }));
        this.AddMember("LongJump", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("LongJump", typeof (ContainerCreature), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_e03d96fda4a7401886a92c78fd5b3e2b.MTHD_b19a5a277bff479cafb1c70a6a51cb73()
        }));
        this.AddMember("HighJump", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("HighJump", typeof (ContainerCreature), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_e03d96fda4a7401886a92c78fd5b3e2b.MTHD_ad6963b68ca74be992cbf4e57825bdb6()
        }));
        this.AddMember("Walk", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("Walk", typeof (ContainerCreature), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_e03d96fda4a7401886a92c78fd5b3e2b.MTHD_64c3c5cdde3f429d9ff35aea20d662c5()
        }));
        this.AddMember("setDirection", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("setDirection", typeof (ContainerCreature), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_e03d96fda4a7401886a92c78fd5b3e2b.MTHD_973c3a3089d54453938a85d0471ecedb()
        }));
        this.AddMember("getFamiliarLevel", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("getFamiliarLevel", typeof (ContainerCreature), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_e03d96fda4a7401886a92c78fd5b3e2b.MTHD_9e5cb829443143e6aa0f939d013d1cb0()
        }));
        this.AddMember("getActivateableFamiliarLevel", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("getActivateableFamiliarLevel", typeof (ContainerCreature), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_e03d96fda4a7401886a92c78fd5b3e2b.MTHD_c1e82057833148abbfc639c11283e587()
        }));
        this.AddMember("getActivateableFamiliarBookID", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("getActivateableFamiliarBookID", typeof (ContainerCreature), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_e03d96fda4a7401886a92c78fd5b3e2b.MTHD_64a42b5b3c7c46fb8f9ec27f4dc75cd1()
        }));
        this.AddMember("increaseFamiliarLevel", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("increaseFamiliarLevel", typeof (ContainerCreature), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_e03d96fda4a7401886a92c78fd5b3e2b.MTHD_8b67ed2dac86475ca99c5c681b61c655()
        }));
        this.AddMember("inside", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("inside", typeof (ContainerCreature), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[2]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_e03d96fda4a7401886a92c78fd5b3e2b.MTHD_9bec654f00c34db0b1682ef55785dd92(),
          (IOverloadableMemberDescriptor) new Bridge.TYPE_e03d96fda4a7401886a92c78fd5b3e2b.MTHD_029b270810ae459eaf8c3bf20e84014d()
        }));
        this.AddMember("overlap", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("overlap", typeof (ContainerCreature), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[2]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_e03d96fda4a7401886a92c78fd5b3e2b.MTHD_6e2d4121fc614ae784f11db89c43e4a3(),
          (IOverloadableMemberDescriptor) new Bridge.TYPE_e03d96fda4a7401886a92c78fd5b3e2b.MTHD_1cbf62c70767474482ffbc5eba33ba28()
        }));
        this.AddMember("die", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("die", typeof (ContainerCreature), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_e03d96fda4a7401886a92c78fd5b3e2b.MTHD_18e155d0a8c34d42bbc5d9167919768a()
        }));
        this.AddMember("addSpell", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("addSpell", typeof (ContainerCreature), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_e03d96fda4a7401886a92c78fd5b3e2b.MTHD_f433b82895b243379df4b35368bb592d()
        }));
        this.AddMember("removeSpell", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("removeSpell", typeof (ContainerCreature), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[2]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_e03d96fda4a7401886a92c78fd5b3e2b.MTHD_1bdcc6398e5644e68e4edf3bdd65413f(),
          (IOverloadableMemberDescriptor) new Bridge.TYPE_e03d96fda4a7401886a92c78fd5b3e2b.MTHD_a9fd1764be4043299231bf7f03c0931d()
        }));
        this.AddMember("getSpells", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("getSpells", typeof (ContainerCreature), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_e03d96fda4a7401886a92c78fd5b3e2b.MTHD_051ecf5488404b3b8547e74b9f9662b0()
        }));
        this.AddMember("getSpell", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("getSpell", typeof (ContainerCreature), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_e03d96fda4a7401886a92c78fd5b3e2b.MTHD_f02fc1d0b71b4ec8ad60c9d3c74c18b7()
        }));
        this.AddMember("getSpellCount", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("getSpellCount", typeof (ContainerCreature), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_e03d96fda4a7401886a92c78fd5b3e2b.MTHD_e532977d50614405836fe6423ebcd496()
        }));
        this.AddMember("castedCount", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("castedCount", typeof (ContainerCreature), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_e03d96fda4a7401886a92c78fd5b3e2b.MTHD_dc91ff18053148f3bfe0b316ca8d0eeb()
        }));
        this.AddMember("castedCountThisTurn", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("castedCountThisTurn", typeof (ContainerCreature), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_e03d96fda4a7401886a92c78fd5b3e2b.MTHD_a43b13e1e7214b759e05d22eaa92a426()
        }));
        this.AddMember("castedDamage", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("castedDamage", typeof (ContainerCreature), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_e03d96fda4a7401886a92c78fd5b3e2b.MTHD_3492546aa1e04e0883282331d1eacbcc()
        }));
        this.AddMember("getMinionCount", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("getMinionCount", typeof (ContainerCreature), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_e03d96fda4a7401886a92c78fd5b3e2b.MTHD_15f7ea4ef9a742d88fb62a4a9d1f9b65()
        }));
        this.AddMember("castSpell", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("castSpell", typeof (ContainerCreature), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_e03d96fda4a7401886a92c78fd5b3e2b.MTHD_21fb7cb4edca4da5bf45755de664b8e8()
        }));
        this.AddMember("fireAt", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("fireAt", typeof (ContainerCreature), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[2]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_e03d96fda4a7401886a92c78fd5b3e2b.MTHD_32f97d6acd6446308e9f2e8ae44dfcb3(),
          (IOverloadableMemberDescriptor) new Bridge.TYPE_e03d96fda4a7401886a92c78fd5b3e2b.MTHD_ef0f080f8e834966967e0b7c8bc29f31()
        }));
        this.AddMember("getEffectors", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("getEffectors", typeof (ContainerCreature), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[2]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_e03d96fda4a7401886a92c78fd5b3e2b.MTHD_8e94fd952ac4466fbbc548e914c87495(),
          (IOverloadableMemberDescriptor) new Bridge.TYPE_e03d96fda4a7401886a92c78fd5b3e2b.MTHD_6546d9e0dd154637a997e302cf575d37()
        }));
        this.AddMember("Equals", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("Equals", typeof (ContainerCreature), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_e03d96fda4a7401886a92c78fd5b3e2b.MTHD_2a803e6285fe46039566a6db53e483b1()
        }));
        this.AddMember("GetHashCode", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("GetHashCode", typeof (ContainerCreature), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_e03d96fda4a7401886a92c78fd5b3e2b.MTHD_de0300117f304568b7ae978a6d170ad7()
        }));
        this.AddMember("op_Equality", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("op_Equality", typeof (ContainerCreature), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_e03d96fda4a7401886a92c78fd5b3e2b.MTHD_d282d61a0ce643d98738e6881d7f9cef()
        }));
        this.AddMember("op_Inequality", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("op_Inequality", typeof (ContainerCreature), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_e03d96fda4a7401886a92c78fd5b3e2b.MTHD_c800709d94cf46b48e85ee57b94b254b()
        }));
        this.AddMember("GetType", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("GetType", typeof (ContainerCreature), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_e03d96fda4a7401886a92c78fd5b3e2b.MTHD_9b55fbaa15604cf68379d7c6112d96eb()
        }));
        this.AddMember("ToString", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("ToString", typeof (ContainerCreature), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_e03d96fda4a7401886a92c78fd5b3e2b.MTHD_b4fb5dcfe18240ca80b4108f4c0530e6()
        }));
        this.AddMember("team", (IMemberDescriptor) new Bridge.TYPE_e03d96fda4a7401886a92c78fd5b3e2b.PROP_fc5d05741456478daf0b29a1855d3b97());
        this.AddMember("name", (IMemberDescriptor) new Bridge.TYPE_e03d96fda4a7401886a92c78fd5b3e2b.PROP_d318f6963888440e8af43661bafc6f31());
        this.AddMember("spellEnum", (IMemberDescriptor) new Bridge.TYPE_e03d96fda4a7401886a92c78fd5b3e2b.PROP_90d3c677e3424d8abcdab137135eb317());
        this.AddMember("health", (IMemberDescriptor) new Bridge.TYPE_e03d96fda4a7401886a92c78fd5b3e2b.PROP_fac322aba1ad45f5bda83aa5cdf10427());
        this.AddMember("maxHealth", (IMemberDescriptor) new Bridge.TYPE_e03d96fda4a7401886a92c78fd5b3e2b.PROP_24513fcf86fa454cb72323319a2e37f1());
        this.AddMember("healthAndTower", (IMemberDescriptor) new Bridge.TYPE_e03d96fda4a7401886a92c78fd5b3e2b.PROP_73a4db71cfed43e58ca648988daac43b());
        this.AddMember("shield", (IMemberDescriptor) new Bridge.TYPE_e03d96fda4a7401886a92c78fd5b3e2b.PROP_e964be1a1cb1498c81178effacfe1f57());
        this.AddMember("x", (IMemberDescriptor) new Bridge.TYPE_e03d96fda4a7401886a92c78fd5b3e2b.PROP_2cd44a56200d4b4699aa044b7974daba());
        this.AddMember("y", (IMemberDescriptor) new Bridge.TYPE_e03d96fda4a7401886a92c78fd5b3e2b.PROP_585931ef4ce2445eb362a8b483eba2aa());
        this.AddMember("position", (IMemberDescriptor) new Bridge.TYPE_e03d96fda4a7401886a92c78fd5b3e2b.PROP_3c450a8f945a459595747b05184a1248());
        this.AddMember("parent", (IMemberDescriptor) new Bridge.TYPE_e03d96fda4a7401886a92c78fd5b3e2b.PROP_5be202e06e6b4ac5855618533be0bab4());
        this.AddMember("weight", (IMemberDescriptor) new Bridge.TYPE_e03d96fda4a7401886a92c78fd5b3e2b.PROP_f1f4925f87c94d79803c9a826ded6b03());
        this.AddMember("radius", (IMemberDescriptor) new Bridge.TYPE_e03d96fda4a7401886a92c78fd5b3e2b.PROP_32d7c1c4a26946b985c9cff010931576());
        this.AddMember("inTower", (IMemberDescriptor) new Bridge.TYPE_e03d96fda4a7401886a92c78fd5b3e2b.PROP_04cbdd81854140aca5f616234d129d17());
        this.AddMember("isMoving", (IMemberDescriptor) new Bridge.TYPE_e03d96fda4a7401886a92c78fd5b3e2b.PROP_a38b6da352b449ff8005875e6691d3df());
        this.AddMember("stunned", (IMemberDescriptor) new Bridge.TYPE_e03d96fda4a7401886a92c78fd5b3e2b.PROP_ba2d66d2505b4c5e803febc53b06f95a());
        this.AddMember("superStunned", (IMemberDescriptor) new Bridge.TYPE_e03d96fda4a7401886a92c78fd5b3e2b.PROP_7527f248a4db445595287e02ce2c9951());
        this.AddMember("waterWalking", (IMemberDescriptor) new Bridge.TYPE_e03d96fda4a7401886a92c78fd5b3e2b.PROP_4cd5365aae524738819e81e655fa7183());
        this.AddMember("frostWalking", (IMemberDescriptor) new Bridge.TYPE_e03d96fda4a7401886a92c78fd5b3e2b.PROP_1d913b501f9a4f6a93d9e45570303a48());
        this.AddMember("inWater", (IMemberDescriptor) new Bridge.TYPE_e03d96fda4a7401886a92c78fd5b3e2b.PROP_bf924cb83e944503b6c18f540da69796());
        this.AddMember("diesInWater", (IMemberDescriptor) new Bridge.TYPE_e03d96fda4a7401886a92c78fd5b3e2b.PROP_f266f1cb50454b44868b702bc3d81406());
        this.AddMember("type", (IMemberDescriptor) new Bridge.TYPE_e03d96fda4a7401886a92c78fd5b3e2b.PROP_32977183d79b46cd946bbd9c350830ce());
        this.AddMember("race", (IMemberDescriptor) new Bridge.TYPE_e03d96fda4a7401886a92c78fd5b3e2b.PROP_85ae86019d5c440a91ef5dc63c0cb34d());
        this.AddMember("yourTurn", (IMemberDescriptor) new Bridge.TYPE_e03d96fda4a7401886a92c78fd5b3e2b.PROP_05dbb28eedd4402395d1906462327edb());
        this.AddMember("isRider", (IMemberDescriptor) new Bridge.TYPE_e03d96fda4a7401886a92c78fd5b3e2b.PROP_3d9a95b7845b4c4197391d8917ec110a());
        this.AddMember("isMounted", (IMemberDescriptor) new Bridge.TYPE_e03d96fda4a7401886a92c78fd5b3e2b.PROP_cdc7cb274f044cd2a6dc1867627c50dd());
        this.AddMember("isDead", (IMemberDescriptor) new Bridge.TYPE_e03d96fda4a7401886a92c78fd5b3e2b.PROP_46a2041c52724d618eaa83c9541d99a5());
        this.AddMember("isPawn", (IMemberDescriptor) new Bridge.TYPE_e03d96fda4a7401886a92c78fd5b3e2b.PROP_207bcf6ef0784e119bed91e27955f2f0());
        this.AddMember("isFlying", (IMemberDescriptor) new Bridge.TYPE_e03d96fda4a7401886a92c78fd5b3e2b.PROP_aca5f2c16d5a46c4af865585c9ff9d8f());
        this.AddMember("isMountable", (IMemberDescriptor) new Bridge.TYPE_e03d96fda4a7401886a92c78fd5b3e2b.PROP_0277c418a9554949acf6b55e32ae7011());
        this.AddMember("canMount", (IMemberDescriptor) new Bridge.TYPE_e03d96fda4a7401886a92c78fd5b3e2b.PROP_fd032eb9081944d0ab33acf250054588());
        this.AddMember("arcaneMonster", (IMemberDescriptor) new Bridge.TYPE_e03d96fda4a7401886a92c78fd5b3e2b.PROP_612e418d9da34643876ab610e61748b4());
        this.AddMember("takeDamage", (IMemberDescriptor) new Bridge.TYPE_e03d96fda4a7401886a92c78fd5b3e2b.PROP_02772e8a3f3446e4bc2aa2bc6f8f0ebf());
      }

      private sealed class MTHD_1b021e81186d47e2848b27af910bce27 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_1b021e81186d47e2848b27af910bce27()
        {
          this.Initialize("get_team", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerCreature) obj).team;
        }
      }

      private sealed class MTHD_4509fa7361fc41748fbc58f3b3a7a500 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_4509fa7361fc41748fbc58f3b3a7a500()
        {
          this.Initialize("get_name", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerCreature) obj).name;
        }
      }

      private sealed class MTHD_734132091e224afbbf501436d89c2b26 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_734132091e224afbbf501436d89c2b26()
        {
          this.Initialize("get_spellEnum", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerCreature) obj).spellEnum;
        }
      }

      private sealed class MTHD_5ced4addb8f44cd8996b6e78ea45270e : HardwiredMethodMemberDescriptor
      {
        internal MTHD_5ced4addb8f44cd8996b6e78ea45270e()
        {
          this.Initialize("get_health", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerCreature) obj).health;
        }
      }

      private sealed class MTHD_c797a90665624edb8cb1298d468e97b2 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_c797a90665624edb8cb1298d468e97b2()
        {
          this.Initialize("set_health", false, new ParameterDescriptor[1]
          {
            new ParameterDescriptor("value", typeof (int))
          }, false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          ((ContainerCreature) obj).health = (int) pars[0];
          return (object) null;
        }
      }

      private sealed class MTHD_926753561e0f4b56b94d0dd246520334 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_926753561e0f4b56b94d0dd246520334()
        {
          this.Initialize("get_maxHealth", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerCreature) obj).maxHealth;
        }
      }

      private sealed class MTHD_abc01d95272b474583d4ea267902e137 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_abc01d95272b474583d4ea267902e137()
        {
          this.Initialize("set_maxHealth", false, new ParameterDescriptor[1]
          {
            new ParameterDescriptor("value", typeof (int))
          }, false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          ((ContainerCreature) obj).maxHealth = (int) pars[0];
          return (object) null;
        }
      }

      private sealed class MTHD_7217f8620a884d43851ba9cdfc96763d : HardwiredMethodMemberDescriptor
      {
        internal MTHD_7217f8620a884d43851ba9cdfc96763d()
        {
          this.Initialize("get_healthAndTower", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerCreature) obj).healthAndTower;
        }
      }

      private sealed class MTHD_8ecba28ba00543faa7cfd297373b6d08 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_8ecba28ba00543faa7cfd297373b6d08()
        {
          this.Initialize("get_shield", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerCreature) obj).shield;
        }
      }

      private sealed class MTHD_54befc0b2f4d44558ad22d6bde321ea9 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_54befc0b2f4d44558ad22d6bde321ea9()
        {
          this.Initialize("set_shield", false, new ParameterDescriptor[1]
          {
            new ParameterDescriptor("value", typeof (int))
          }, false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          ((ContainerCreature) obj).shield = (int) pars[0];
          return (object) null;
        }
      }

      private sealed class MTHD_2a19ceb7c61b495c92a969b8f5a3d8d4 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_2a19ceb7c61b495c92a969b8f5a3d8d4()
        {
          this.Initialize("get_x", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerCreature) obj).x;
        }
      }

      private sealed class MTHD_de530ce92218479ba976fd564a6d5d81 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_de530ce92218479ba976fd564a6d5d81()
        {
          this.Initialize("set_x", false, new ParameterDescriptor[1]
          {
            new ParameterDescriptor("value", typeof (int))
          }, false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          ((ContainerCreature) obj).x = (int) pars[0];
          return (object) null;
        }
      }

      private sealed class MTHD_77c8d6ce0879450dbb4102c9a0a0149d : HardwiredMethodMemberDescriptor
      {
        internal MTHD_77c8d6ce0879450dbb4102c9a0a0149d()
        {
          this.Initialize("get_y", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerCreature) obj).y;
        }
      }

      private sealed class MTHD_581e155cc2b14cc38b7b81c1cd8268c8 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_581e155cc2b14cc38b7b81c1cd8268c8()
        {
          this.Initialize("set_y", false, new ParameterDescriptor[1]
          {
            new ParameterDescriptor("value", typeof (int))
          }, false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          ((ContainerCreature) obj).y = (int) pars[0];
          return (object) null;
        }
      }

      private sealed class MTHD_a976238bac544f41946450f9f44ca093 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_a976238bac544f41946450f9f44ca093()
        {
          this.Initialize("get_position", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerCreature) obj).position;
        }
      }

      private sealed class MTHD_f0d97834a2e2450fa33964760b4c9843 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_f0d97834a2e2450fa33964760b4c9843()
        {
          this.Initialize("set_position", false, new ParameterDescriptor[1]
          {
            new ParameterDescriptor("value", typeof (Educative.Point))
          }, false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          ((ContainerCreature) obj).position = (Educative.Point) pars[0];
          return (object) null;
        }
      }

      private sealed class MTHD_29af5d23be4e4ff3a4a504ec9d231d01 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_29af5d23be4e4ff3a4a504ec9d231d01()
        {
          this.Initialize("get_parent", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerCreature) obj).parent;
        }
      }

      private sealed class MTHD_1460e51ae11647ca8ebdc9cbd5c2479d : HardwiredMethodMemberDescriptor
      {
        internal MTHD_1460e51ae11647ca8ebdc9cbd5c2479d()
        {
          this.Initialize("set_parent", false, new ParameterDescriptor[1]
          {
            new ParameterDescriptor("value", typeof (ContainerPlayer))
          }, false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          ((ContainerCreature) obj).parent = (ContainerPlayer) pars[0];
          return (object) null;
        }
      }

      private sealed class MTHD_d5b7015f52cb42b8bed3cd096d85b17b : HardwiredMethodMemberDescriptor
      {
        internal MTHD_d5b7015f52cb42b8bed3cd096d85b17b()
        {
          this.Initialize("get_weight", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerCreature) obj).weight;
        }
      }

      private sealed class MTHD_11a2983cd24641329baf9859f075bbb6 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_11a2983cd24641329baf9859f075bbb6()
        {
          this.Initialize("set_weight", false, new ParameterDescriptor[1]
          {
            new ParameterDescriptor("value", typeof (int))
          }, false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          ((ContainerCreature) obj).weight = (int) pars[0];
          return (object) null;
        }
      }

      private sealed class MTHD_f5cf8f3638ea431fb4185027f7a8f6dc : HardwiredMethodMemberDescriptor
      {
        internal MTHD_f5cf8f3638ea431fb4185027f7a8f6dc()
        {
          this.Initialize("get_radius", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerCreature) obj).radius;
        }
      }

      private sealed class MTHD_2029bd75ce294914b30b11c1d4c0d712 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_2029bd75ce294914b30b11c1d4c0d712()
        {
          this.Initialize("set_radius", false, new ParameterDescriptor[1]
          {
            new ParameterDescriptor("value", typeof (int))
          }, false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          ((ContainerCreature) obj).radius = (int) pars[0];
          return (object) null;
        }
      }

      private sealed class MTHD_2cf695217d754221855529bb8ad13470 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_2cf695217d754221855529bb8ad13470()
        {
          this.Initialize("get_inTower", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerCreature) obj).inTower;
        }
      }

      private sealed class MTHD_192cadc2016b4b2f86da65bc434caf82 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_192cadc2016b4b2f86da65bc434caf82()
        {
          this.Initialize("get_isMoving", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerCreature) obj).isMoving;
        }
      }

      private sealed class MTHD_dc9910d9edb741a4bf5ab612d444a966 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_dc9910d9edb741a4bf5ab612d444a966()
        {
          this.Initialize("get_stunned", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerCreature) obj).stunned;
        }
      }

      private sealed class MTHD_4c4f6cc4915742f0835fa3ddb7d3c53c : HardwiredMethodMemberDescriptor
      {
        internal MTHD_4c4f6cc4915742f0835fa3ddb7d3c53c()
        {
          this.Initialize("set_stunned", false, new ParameterDescriptor[1]
          {
            new ParameterDescriptor("value", typeof (bool))
          }, false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          ((ContainerCreature) obj).stunned = (bool) pars[0];
          return (object) null;
        }
      }

      private sealed class MTHD_88a65e1f244344dda183bc977dd71d3b : HardwiredMethodMemberDescriptor
      {
        internal MTHD_88a65e1f244344dda183bc977dd71d3b()
        {
          this.Initialize("get_superStunned", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerCreature) obj).superStunned;
        }
      }

      private sealed class MTHD_2162d803ba264f4ebcefc635a1a2b7fe : HardwiredMethodMemberDescriptor
      {
        internal MTHD_2162d803ba264f4ebcefc635a1a2b7fe()
        {
          this.Initialize("set_superStunned", false, new ParameterDescriptor[1]
          {
            new ParameterDescriptor("value", typeof (bool))
          }, false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          ((ContainerCreature) obj).superStunned = (bool) pars[0];
          return (object) null;
        }
      }

      private sealed class MTHD_592ebe80ca894fc18aa4ac1edf9e7c9d : HardwiredMethodMemberDescriptor
      {
        internal MTHD_592ebe80ca894fc18aa4ac1edf9e7c9d()
        {
          this.Initialize("get_waterWalking", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerCreature) obj).waterWalking;
        }
      }

      private sealed class MTHD_9d7b0be6fae34d20a191fb8bc350dd87 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_9d7b0be6fae34d20a191fb8bc350dd87()
        {
          this.Initialize("set_waterWalking", false, new ParameterDescriptor[1]
          {
            new ParameterDescriptor("value", typeof (bool))
          }, false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          ((ContainerCreature) obj).waterWalking = (bool) pars[0];
          return (object) null;
        }
      }

      private sealed class MTHD_27584d2f6b2b4c8dbf72e04f340c87ad : HardwiredMethodMemberDescriptor
      {
        internal MTHD_27584d2f6b2b4c8dbf72e04f340c87ad()
        {
          this.Initialize("get_frostWalking", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerCreature) obj).frostWalking;
        }
      }

      private sealed class MTHD_49a5d00af5544f2dbc7b6b6c8be23dc0 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_49a5d00af5544f2dbc7b6b6c8be23dc0()
        {
          this.Initialize("set_frostWalking", false, new ParameterDescriptor[1]
          {
            new ParameterDescriptor("value", typeof (bool))
          }, false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          ((ContainerCreature) obj).frostWalking = (bool) pars[0];
          return (object) null;
        }
      }

      private sealed class MTHD_b2fc9f2714454fd6bcd0ccb043e54b5a : HardwiredMethodMemberDescriptor
      {
        internal MTHD_b2fc9f2714454fd6bcd0ccb043e54b5a()
        {
          this.Initialize("get_inWater", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerCreature) obj).inWater;
        }
      }

      private sealed class MTHD_80d3101a7f5748f5bfffa7546da03b9b : HardwiredMethodMemberDescriptor
      {
        internal MTHD_80d3101a7f5748f5bfffa7546da03b9b()
        {
          this.Initialize("get_diesInWater", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerCreature) obj).diesInWater;
        }
      }

      private sealed class MTHD_f40f5c17dd804a4f814033ae6533b2bb : HardwiredMethodMemberDescriptor
      {
        internal MTHD_f40f5c17dd804a4f814033ae6533b2bb()
        {
          this.Initialize("set_diesInWater", false, new ParameterDescriptor[1]
          {
            new ParameterDescriptor("value", typeof (bool))
          }, false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          ((ContainerCreature) obj).diesInWater = (bool) pars[0];
          return (object) null;
        }
      }

      private sealed class MTHD_2576acc2e13344d69af1e22f0238d941 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_2576acc2e13344d69af1e22f0238d941()
        {
          this.Initialize("get_type", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerCreature) obj).type;
        }
      }

      private sealed class MTHD_d29308ae8db54ea79eb135ac9b9b0056 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_d29308ae8db54ea79eb135ac9b9b0056()
        {
          this.Initialize("set_type", false, new ParameterDescriptor[1]
          {
            new ParameterDescriptor("value", typeof (CreatureType))
          }, false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          ((ContainerCreature) obj).type = (CreatureType) pars[0];
          return (object) null;
        }
      }

      private sealed class MTHD_c8679a432a144c6b8e2ab3d57de7744b : HardwiredMethodMemberDescriptor
      {
        internal MTHD_c8679a432a144c6b8e2ab3d57de7744b()
        {
          this.Initialize("get_race", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerCreature) obj).race;
        }
      }

      private sealed class MTHD_439d6871a7fc4395ba0076268ff62a0c : HardwiredMethodMemberDescriptor
      {
        internal MTHD_439d6871a7fc4395ba0076268ff62a0c()
        {
          this.Initialize("set_race", false, new ParameterDescriptor[1]
          {
            new ParameterDescriptor("value", typeof (CreatureRace))
          }, false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          ((ContainerCreature) obj).race = (CreatureRace) pars[0];
          return (object) null;
        }
      }

      private sealed class MTHD_05b1eb120cda4c06ae9ccd962afe3314 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_05b1eb120cda4c06ae9ccd962afe3314()
        {
          this.Initialize("get_yourTurn", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerCreature) obj).yourTurn;
        }
      }

      private sealed class MTHD_ca2eaea994d94692b2a1fd9d73d15e90 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_ca2eaea994d94692b2a1fd9d73d15e90()
        {
          this.Initialize("get_isRider", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerCreature) obj).isRider;
        }
      }

      private sealed class MTHD_f75a0d1a3d6841d4824b7b34edf5e0e3 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_f75a0d1a3d6841d4824b7b34edf5e0e3()
        {
          this.Initialize("get_isMounted", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerCreature) obj).isMounted;
        }
      }

      private sealed class MTHD_12eac8d7a0d349a3b9960e02dd50c459 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_12eac8d7a0d349a3b9960e02dd50c459()
        {
          this.Initialize("get_isDead", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerCreature) obj).isDead;
        }
      }

      private sealed class MTHD_2879171c31974551b7856fbb9e010c96 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_2879171c31974551b7856fbb9e010c96()
        {
          this.Initialize("get_isPawn", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerCreature) obj).isPawn;
        }
      }

      private sealed class MTHD_8983e51d14ad4401a0c49507781b47c2 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_8983e51d14ad4401a0c49507781b47c2()
        {
          this.Initialize("get_isFlying", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerCreature) obj).isFlying;
        }
      }

      private sealed class MTHD_7707ccf686304c2db8032845a9cc49b4 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_7707ccf686304c2db8032845a9cc49b4()
        {
          this.Initialize("set_isFlying", false, new ParameterDescriptor[1]
          {
            new ParameterDescriptor("value", typeof (bool))
          }, false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          ((ContainerCreature) obj).isFlying = (bool) pars[0];
          return (object) null;
        }
      }

      private sealed class MTHD_b86bfc5c1c494c1f8e525b8c7feea5ca : HardwiredMethodMemberDescriptor
      {
        internal MTHD_b86bfc5c1c494c1f8e525b8c7feea5ca()
        {
          this.Initialize("get_isMountable", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerCreature) obj).isMountable;
        }
      }

      private sealed class MTHD_33968aeb246e4b048491a94740e5ae66 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_33968aeb246e4b048491a94740e5ae66()
        {
          this.Initialize("get_canMount", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerCreature) obj).canMount;
        }
      }

      private sealed class MTHD_50441c2c9ce94882a19cf4661bc51d0e : HardwiredMethodMemberDescriptor
      {
        internal MTHD_50441c2c9ce94882a19cf4661bc51d0e()
        {
          this.Initialize("get_arcaneMonster", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerCreature) obj).arcaneMonster;
        }
      }

      private sealed class MTHD_6aa7bbd61d2e4dd695d2bf6b87fecb2e : HardwiredMethodMemberDescriptor
      {
        internal MTHD_6aa7bbd61d2e4dd695d2bf6b87fecb2e()
        {
          this.Initialize("set_arcaneMonster", false, new ParameterDescriptor[1]
          {
            new ParameterDescriptor("value", typeof (bool))
          }, false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          ((ContainerCreature) obj).arcaneMonster = (bool) pars[0];
          return (object) null;
        }
      }

      private sealed class MTHD_9dfe8cdb278b463ca46ff190205abd3e : HardwiredMethodMemberDescriptor
      {
        internal MTHD_9dfe8cdb278b463ca46ff190205abd3e()
        {
          this.Initialize("get_takeDamage", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerCreature) obj).takeDamage;
        }
      }

      private sealed class MTHD_b83be496bdf342b98c757ff8e101486f : HardwiredMethodMemberDescriptor
      {
        internal MTHD_b83be496bdf342b98c757ff8e101486f()
        {
          this.Initialize("set_takeDamage", false, new ParameterDescriptor[1]
          {
            new ParameterDescriptor("value", typeof (bool))
          }, false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          ((ContainerCreature) obj).takeDamage = (bool) pars[0];
          return (object) null;
        }
      }

      private sealed class MTHD_0786ab55f6f2409f9b2132ea047fe5e4 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_0786ab55f6f2409f9b2132ea047fe5e4()
        {
          this.Initialize("getRider", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerCreature) obj).getRider();
        }
      }

      private sealed class MTHD_02fb79538dc84bcaabe728a45bfd622e : HardwiredMethodMemberDescriptor
      {
        internal MTHD_02fb79538dc84bcaabe728a45bfd622e()
        {
          this.Initialize("getMount", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerCreature) obj).getMount();
        }
      }

      private sealed class MTHD_a15ad6af07524cff8d36fd3746d29d66 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_a15ad6af07524cff8d36fd3746d29d66()
        {
          this.Initialize("getTower", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerCreature) obj).getTower();
        }
      }

      private sealed class MTHD_b19a5a277bff479cafb1c70a6a51cb73 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_b19a5a277bff479cafb1c70a6a51cb73()
        {
          this.Initialize("LongJump", false, new ParameterDescriptor[1]
          {
            new ParameterDescriptor("dir", typeof (int))
          }, false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          ((ContainerCreature) obj).LongJump((int) pars[0]);
          return (object) DynValue.Void;
        }
      }

      private sealed class MTHD_ad6963b68ca74be992cbf4e57825bdb6 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_ad6963b68ca74be992cbf4e57825bdb6()
        {
          this.Initialize("HighJump", false, new ParameterDescriptor[1]
          {
            new ParameterDescriptor("dir", typeof (int))
          }, false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          ((ContainerCreature) obj).HighJump((int) pars[0]);
          return (object) DynValue.Void;
        }
      }

      private sealed class MTHD_64c3c5cdde3f429d9ff35aea20d662c5 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_64c3c5cdde3f429d9ff35aea20d662c5()
        {
          this.Initialize("Walk", false, new ParameterDescriptor[1]
          {
            new ParameterDescriptor("dir", typeof (int))
          }, false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          ((ContainerCreature) obj).Walk((int) pars[0]);
          return (object) DynValue.Void;
        }
      }

      private sealed class MTHD_973c3a3089d54453938a85d0471ecedb : HardwiredMethodMemberDescriptor
      {
        internal MTHD_973c3a3089d54453938a85d0471ecedb()
        {
          this.Initialize("setDirection", false, new ParameterDescriptor[1]
          {
            new ParameterDescriptor("dir", typeof (int))
          }, false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          ((ContainerCreature) obj).setDirection((int) pars[0]);
          return (object) DynValue.Void;
        }
      }

      private sealed class MTHD_9e5cb829443143e6aa0f939d013d1cb0 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_9e5cb829443143e6aa0f939d013d1cb0()
        {
          this.Initialize("getFamiliarLevel", false, new ParameterDescriptor[1]
          {
            new ParameterDescriptor("book", typeof (BookOf))
          }, false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerCreature) obj).getFamiliarLevel((BookOf) pars[0]);
        }
      }

      private sealed class MTHD_c1e82057833148abbfc639c11283e587 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_c1e82057833148abbfc639c11283e587()
        {
          this.Initialize("getActivateableFamiliarLevel", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerCreature) obj).getActivateableFamiliarLevel();
        }
      }

      private sealed class MTHD_64a42b5b3c7c46fb8f9ec27f4dc75cd1 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_64a42b5b3c7c46fb8f9ec27f4dc75cd1()
        {
          this.Initialize("getActivateableFamiliarBookID", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerCreature) obj).getActivateableFamiliarBookID();
        }
      }

      private sealed class MTHD_8b67ed2dac86475ca99c5c681b61c655 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_8b67ed2dac86475ca99c5c681b61c655()
        {
          this.Initialize("increaseFamiliarLevel", false, new ParameterDescriptor[3]
          {
            new ParameterDescriptor("book", typeof (BookOf), true, (object) new DefaultValue()),
            new ParameterDescriptor("dealDmg", typeof (bool), true, (object) new DefaultValue()),
            new ParameterDescriptor("capAt5", typeof (bool), true, (object) new DefaultValue())
          }, false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          if (argscount <= 0)
          {
            ((ContainerCreature) obj).increaseFamiliarLevel();
            return (object) DynValue.Void;
          }
          if (argscount <= 1)
          {
            ((ContainerCreature) obj).increaseFamiliarLevel((BookOf) pars[0]);
            return (object) DynValue.Void;
          }
          if (argscount <= 2)
          {
            ((ContainerCreature) obj).increaseFamiliarLevel((BookOf) pars[0], (bool) pars[1]);
            return (object) DynValue.Void;
          }
          ((ContainerCreature) obj).increaseFamiliarLevel((BookOf) pars[0], (bool) pars[1], (bool) pars[2]);
          return (object) DynValue.Void;
        }
      }

      private sealed class MTHD_9bec654f00c34db0b1682ef55785dd92 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_9bec654f00c34db0b1682ef55785dd92()
        {
          this.Initialize("inside", false, new ParameterDescriptor[1]
          {
            new ParameterDescriptor("a", typeof (ContainerIndicator))
          }, false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerCreature) obj).inside((ContainerIndicator) pars[0]);
        }
      }

      private sealed class MTHD_029b270810ae459eaf8c3bf20e84014d : HardwiredMethodMemberDescriptor
      {
        internal MTHD_029b270810ae459eaf8c3bf20e84014d()
        {
          this.Initialize("inside", false, new ParameterDescriptor[2]
          {
            new ParameterDescriptor("a", typeof (Educative.Point)),
            new ParameterDescriptor("radius", typeof (double))
          }, false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerCreature) obj).inside((Educative.Point) pars[0], (double) pars[1]);
        }
      }

      private sealed class MTHD_6e2d4121fc614ae784f11db89c43e4a3 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_6e2d4121fc614ae784f11db89c43e4a3()
        {
          this.Initialize("overlap", false, new ParameterDescriptor[1]
          {
            new ParameterDescriptor("a", typeof (ContainerIndicator))
          }, false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerCreature) obj).overlap((ContainerIndicator) pars[0]);
        }
      }

      private sealed class MTHD_1cbf62c70767474482ffbc5eba33ba28 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_1cbf62c70767474482ffbc5eba33ba28()
        {
          this.Initialize("overlap", false, new ParameterDescriptor[2]
          {
            new ParameterDescriptor("a", typeof (Educative.Point)),
            new ParameterDescriptor("radius", typeof (double), true, (object) new DefaultValue())
          }, false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return argscount <= 1 ? (object) ((ContainerCreature) obj).overlap((Educative.Point) pars[0]) : (object) ((ContainerCreature) obj).overlap((Educative.Point) pars[0], (double) pars[1]);
        }
      }

      private sealed class MTHD_18e155d0a8c34d42bbc5d9167919768a : HardwiredMethodMemberDescriptor
      {
        internal MTHD_18e155d0a8c34d42bbc5d9167919768a()
        {
          this.Initialize("die", false, new ParameterDescriptor[1]
          {
            new ParameterDescriptor("playSound", typeof (bool), true, (object) new DefaultValue())
          }, false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          if (argscount <= 0)
          {
            ((ContainerCreature) obj).die();
            return (object) DynValue.Void;
          }
          ((ContainerCreature) obj).die((bool) pars[0]);
          return (object) DynValue.Void;
        }
      }

      private sealed class MTHD_f433b82895b243379df4b35368bb592d : HardwiredMethodMemberDescriptor
      {
        internal MTHD_f433b82895b243379df4b35368bb592d()
        {
          this.Initialize("addSpell", false, new ParameterDescriptor[1]
          {
            new ParameterDescriptor("s", typeof (object))
          }, false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerCreature) obj).addSpell(pars[0]);
        }
      }

      private sealed class MTHD_1bdcc6398e5644e68e4edf3bdd65413f : HardwiredMethodMemberDescriptor
      {
        internal MTHD_1bdcc6398e5644e68e4edf3bdd65413f()
        {
          this.Initialize("removeSpell", false, new ParameterDescriptor[1]
          {
            new ParameterDescriptor("s", typeof (object))
          }, false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          ((ContainerCreature) obj).removeSpell(pars[0]);
          return (object) DynValue.Void;
        }
      }

      private sealed class MTHD_a9fd1764be4043299231bf7f03c0931d : HardwiredMethodMemberDescriptor
      {
        internal MTHD_a9fd1764be4043299231bf7f03c0931d()
        {
          this.Initialize("removeSpell", false, new ParameterDescriptor[1]
          {
            new ParameterDescriptor("spell", typeof (ContainerSpell))
          }, false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          ((ContainerCreature) obj).removeSpell((ContainerSpell) pars[0]);
          return (object) DynValue.Void;
        }
      }

      private sealed class MTHD_051ecf5488404b3b8547e74b9f9662b0 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_051ecf5488404b3b8547e74b9f9662b0()
        {
          this.Initialize("getSpells", false, new ParameterDescriptor[1]
          {
            new ParameterDescriptor("script", typeof (Script))
          }, false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerCreature) obj).getSpells((Script) pars[0]);
        }
      }

      private sealed class MTHD_f02fc1d0b71b4ec8ad60c9d3c74c18b7 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_f02fc1d0b71b4ec8ad60c9d3c74c18b7()
        {
          this.Initialize("getSpell", false, new ParameterDescriptor[1]
          {
            new ParameterDescriptor("spellObj", typeof (object))
          }, false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerCreature) obj).getSpell(pars[0]);
        }
      }

      private sealed class MTHD_e532977d50614405836fe6423ebcd496 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_e532977d50614405836fe6423ebcd496()
        {
          this.Initialize("getSpellCount", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerCreature) obj).getSpellCount();
        }
      }

      private sealed class MTHD_dc91ff18053148f3bfe0b316ca8d0eeb : HardwiredMethodMemberDescriptor
      {
        internal MTHD_dc91ff18053148f3bfe0b316ca8d0eeb()
        {
          this.Initialize("castedCount", false, new ParameterDescriptor[1]
          {
            new ParameterDescriptor("spellID", typeof (SpellEnum))
          }, false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerCreature) obj).castedCount((SpellEnum) pars[0]);
        }
      }

      private sealed class MTHD_a43b13e1e7214b759e05d22eaa92a426 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_a43b13e1e7214b759e05d22eaa92a426()
        {
          this.Initialize("castedCountThisTurn", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerCreature) obj).castedCountThisTurn();
        }
      }

      private sealed class MTHD_3492546aa1e04e0883282331d1eacbcc : HardwiredMethodMemberDescriptor
      {
        internal MTHD_3492546aa1e04e0883282331d1eacbcc()
        {
          this.Initialize("castedDamage", false, new ParameterDescriptor[1]
          {
            new ParameterDescriptor("spellID", typeof (SpellEnum))
          }, false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerCreature) obj).castedDamage((SpellEnum) pars[0]);
        }
      }

      private sealed class MTHD_15f7ea4ef9a742d88fb62a4a9d1f9b65 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_15f7ea4ef9a742d88fb62a4a9d1f9b65()
        {
          this.Initialize("getMinionCount", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerCreature) obj).getMinionCount();
        }
      }

      private sealed class MTHD_21fb7cb4edca4da5bf45755de664b8e8 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_21fb7cb4edca4da5bf45755de664b8e8()
        {
          this.Initialize("castSpell", false, new ParameterDescriptor[5]
          {
            new ParameterDescriptor("spellObj", typeof (object)),
            new ParameterDescriptor("target", typeof (Educative.Point)),
            new ParameterDescriptor("angle", typeof (double), true, (object) new DefaultValue()),
            new ParameterDescriptor("power", typeof (double), true, (object) new DefaultValue()),
            new ParameterDescriptor("secTarget", typeof (Educative.Point), true, (object) new DefaultValue())
          }, false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          if (argscount <= 2)
          {
            ((ContainerCreature) obj).castSpell(pars[0], (Educative.Point) pars[1]);
            return (object) DynValue.Void;
          }
          if (argscount <= 3)
          {
            ((ContainerCreature) obj).castSpell(pars[0], (Educative.Point) pars[1], (double) pars[2]);
            return (object) DynValue.Void;
          }
          if (argscount <= 4)
          {
            ((ContainerCreature) obj).castSpell(pars[0], (Educative.Point) pars[1], (double) pars[2], (double) pars[3]);
            return (object) DynValue.Void;
          }
          ((ContainerCreature) obj).castSpell(pars[0], (Educative.Point) pars[1], (double) pars[2], (double) pars[3], (Educative.Point) pars[4]);
          return (object) DynValue.Void;
        }
      }

      private sealed class MTHD_32f97d6acd6446308e9f2e8ae44dfcb3 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_32f97d6acd6446308e9f2e8ae44dfcb3()
        {
          this.Initialize("fireAt", false, new ParameterDescriptor[3]
          {
            new ParameterDescriptor("spellObj", typeof (object)),
            new ParameterDescriptor("target", typeof (ContainerCreature)),
            new ParameterDescriptor("anglevariance", typeof (double), true, (object) new DefaultValue())
          }, false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          if (argscount <= 2)
          {
            ((ContainerCreature) obj).fireAt(pars[0], (ContainerCreature) pars[1]);
            return (object) DynValue.Void;
          }
          ((ContainerCreature) obj).fireAt(pars[0], (ContainerCreature) pars[1], (double) pars[2]);
          return (object) DynValue.Void;
        }
      }

      private sealed class MTHD_ef0f080f8e834966967e0b7c8bc29f31 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_ef0f080f8e834966967e0b7c8bc29f31()
        {
          this.Initialize("fireAt", false, new ParameterDescriptor[4]
          {
            new ParameterDescriptor("spellObj", typeof (object)),
            new ParameterDescriptor("target", typeof (Educative.Point)),
            new ParameterDescriptor("anglevariance", typeof (double), true, (object) new DefaultValue()),
            new ParameterDescriptor("secTarget", typeof (Educative.Point), true, (object) new DefaultValue())
          }, false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          if (argscount <= 2)
          {
            ((ContainerCreature) obj).fireAt(pars[0], (Educative.Point) pars[1]);
            return (object) DynValue.Void;
          }
          if (argscount <= 3)
          {
            ((ContainerCreature) obj).fireAt(pars[0], (Educative.Point) pars[1], (double) pars[2]);
            return (object) DynValue.Void;
          }
          ((ContainerCreature) obj).fireAt(pars[0], (Educative.Point) pars[1], (double) pars[2], (Educative.Point) pars[3]);
          return (object) DynValue.Void;
        }
      }

      private sealed class MTHD_8e94fd952ac4466fbbc548e914c87495 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_8e94fd952ac4466fbbc548e914c87495()
        {
          this.Initialize("getEffectors", false, new ParameterDescriptor[1]
          {
            new ParameterDescriptor("script", typeof (Script))
          }, false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerCreature) obj).getEffectors((Script) pars[0]);
        }
      }

      private sealed class MTHD_6546d9e0dd154637a997e302cf575d37 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_6546d9e0dd154637a997e302cf575d37()
        {
          this.Initialize("getEffectors", false, new ParameterDescriptor[2]
          {
            new ParameterDescriptor("script", typeof (Script)),
            new ParameterDescriptor("destroyable", typeof (bool))
          }, false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerCreature) obj).getEffectors((Script) pars[0], (bool) pars[1]);
        }
      }

      private sealed class MTHD_2a803e6285fe46039566a6db53e483b1 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_2a803e6285fe46039566a6db53e483b1()
        {
          this.Initialize("Equals", false, new ParameterDescriptor[1]
          {
            new ParameterDescriptor("obj", typeof (object))
          }, false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerCreature) obj).Equals(pars[0]);
        }
      }

      private sealed class MTHD_de0300117f304568b7ae978a6d170ad7 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_de0300117f304568b7ae978a6d170ad7()
        {
          this.Initialize("GetHashCode", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerCreature) obj).GetHashCode();
        }
      }

      private sealed class MTHD_d282d61a0ce643d98738e6881d7f9cef : HardwiredMethodMemberDescriptor
      {
        internal MTHD_d282d61a0ce643d98738e6881d7f9cef()
        {
          this.Initialize("op_Equality", true, new ParameterDescriptor[2]
          {
            new ParameterDescriptor("v", typeof (ContainerCreature)),
            new ParameterDescriptor("x", typeof (ContainerCreature))
          }, false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerCreature) pars[0] == (ContainerCreature) pars[1]);
        }
      }

      private sealed class MTHD_c800709d94cf46b48e85ee57b94b254b : HardwiredMethodMemberDescriptor
      {
        internal MTHD_c800709d94cf46b48e85ee57b94b254b()
        {
          this.Initialize("op_Inequality", true, new ParameterDescriptor[2]
          {
            new ParameterDescriptor("v", typeof (ContainerCreature)),
            new ParameterDescriptor("x", typeof (ContainerCreature))
          }, false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerCreature) pars[0] != (ContainerCreature) pars[1]);
        }
      }

      private sealed class MTHD_9b55fbaa15604cf68379d7c6112d96eb : HardwiredMethodMemberDescriptor
      {
        internal MTHD_9b55fbaa15604cf68379d7c6112d96eb()
        {
          this.Initialize("GetType", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) obj.GetType();
        }
      }

      private sealed class MTHD_b4fb5dcfe18240ca80b4108f4c0530e6 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_b4fb5dcfe18240ca80b4108f4c0530e6()
        {
          this.Initialize("ToString", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) obj.ToString();
        }
      }

      private sealed class PROP_fc5d05741456478daf0b29a1855d3b97 : HardwiredMemberDescriptor
      {
        internal PROP_fc5d05741456478daf0b29a1855d3b97()
          : base(typeof (int), "team", false, MemberDescriptorAccess.CanRead)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((ContainerCreature) obj).team;
        }
      }

      private sealed class PROP_d318f6963888440e8af43661bafc6f31 : HardwiredMemberDescriptor
      {
        internal PROP_d318f6963888440e8af43661bafc6f31()
          : base(typeof (string), "name", false, MemberDescriptorAccess.CanRead)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((ContainerCreature) obj).name;
        }
      }

      private sealed class PROP_90d3c677e3424d8abcdab137135eb317 : HardwiredMemberDescriptor
      {
        internal PROP_90d3c677e3424d8abcdab137135eb317()
          : base(typeof (SpellEnum), "spellEnum", false, MemberDescriptorAccess.CanRead)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((ContainerCreature) obj).spellEnum;
        }
      }

      private sealed class PROP_fac322aba1ad45f5bda83aa5cdf10427 : HardwiredMemberDescriptor
      {
        internal PROP_fac322aba1ad45f5bda83aa5cdf10427()
          : base(typeof (int), "health", false, MemberDescriptorAccess.CanRead | MemberDescriptorAccess.CanWrite)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((ContainerCreature) obj).health;
        }

        protected override void SetValueImpl(Script script, object obj, object value)
        {
          ((ContainerCreature) obj).health = (int) value;
        }
      }

      private sealed class PROP_24513fcf86fa454cb72323319a2e37f1 : HardwiredMemberDescriptor
      {
        internal PROP_24513fcf86fa454cb72323319a2e37f1()
          : base(typeof (int), "maxHealth", false, MemberDescriptorAccess.CanRead | MemberDescriptorAccess.CanWrite)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((ContainerCreature) obj).maxHealth;
        }

        protected override void SetValueImpl(Script script, object obj, object value)
        {
          ((ContainerCreature) obj).maxHealth = (int) value;
        }
      }

      private sealed class PROP_73a4db71cfed43e58ca648988daac43b : HardwiredMemberDescriptor
      {
        internal PROP_73a4db71cfed43e58ca648988daac43b()
          : base(typeof (int), "healthAndTower", false, MemberDescriptorAccess.CanRead)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((ContainerCreature) obj).healthAndTower;
        }
      }

      private sealed class PROP_e964be1a1cb1498c81178effacfe1f57 : HardwiredMemberDescriptor
      {
        internal PROP_e964be1a1cb1498c81178effacfe1f57()
          : base(typeof (int), "shield", false, MemberDescriptorAccess.CanRead | MemberDescriptorAccess.CanWrite)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((ContainerCreature) obj).shield;
        }

        protected override void SetValueImpl(Script script, object obj, object value)
        {
          ((ContainerCreature) obj).shield = (int) value;
        }
      }

      private sealed class PROP_2cd44a56200d4b4699aa044b7974daba : HardwiredMemberDescriptor
      {
        internal PROP_2cd44a56200d4b4699aa044b7974daba()
          : base(typeof (int), "x", false, MemberDescriptorAccess.CanRead | MemberDescriptorAccess.CanWrite)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((ContainerCreature) obj).x;
        }

        protected override void SetValueImpl(Script script, object obj, object value)
        {
          ((ContainerCreature) obj).x = (int) value;
        }
      }

      private sealed class PROP_585931ef4ce2445eb362a8b483eba2aa : HardwiredMemberDescriptor
      {
        internal PROP_585931ef4ce2445eb362a8b483eba2aa()
          : base(typeof (int), "y", false, MemberDescriptorAccess.CanRead | MemberDescriptorAccess.CanWrite)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((ContainerCreature) obj).y;
        }

        protected override void SetValueImpl(Script script, object obj, object value)
        {
          ((ContainerCreature) obj).y = (int) value;
        }
      }

      private sealed class PROP_3c450a8f945a459595747b05184a1248 : HardwiredMemberDescriptor
      {
        internal PROP_3c450a8f945a459595747b05184a1248()
          : base(typeof (Educative.Point), "position", false, MemberDescriptorAccess.CanRead | MemberDescriptorAccess.CanWrite)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((ContainerCreature) obj).position;
        }

        protected override void SetValueImpl(Script script, object obj, object value)
        {
          ((ContainerCreature) obj).position = (Educative.Point) value;
        }
      }

      private sealed class PROP_5be202e06e6b4ac5855618533be0bab4 : HardwiredMemberDescriptor
      {
        internal PROP_5be202e06e6b4ac5855618533be0bab4()
          : base(typeof (ContainerPlayer), "parent", false, MemberDescriptorAccess.CanRead | MemberDescriptorAccess.CanWrite)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((ContainerCreature) obj).parent;
        }

        protected override void SetValueImpl(Script script, object obj, object value)
        {
          ((ContainerCreature) obj).parent = (ContainerPlayer) value;
        }
      }

      private sealed class PROP_f1f4925f87c94d79803c9a826ded6b03 : HardwiredMemberDescriptor
      {
        internal PROP_f1f4925f87c94d79803c9a826ded6b03()
          : base(typeof (int), "weight", false, MemberDescriptorAccess.CanRead | MemberDescriptorAccess.CanWrite)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((ContainerCreature) obj).weight;
        }

        protected override void SetValueImpl(Script script, object obj, object value)
        {
          ((ContainerCreature) obj).weight = (int) value;
        }
      }

      private sealed class PROP_32d7c1c4a26946b985c9cff010931576 : HardwiredMemberDescriptor
      {
        internal PROP_32d7c1c4a26946b985c9cff010931576()
          : base(typeof (int), "radius", false, MemberDescriptorAccess.CanRead | MemberDescriptorAccess.CanWrite)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((ContainerCreature) obj).radius;
        }

        protected override void SetValueImpl(Script script, object obj, object value)
        {
          ((ContainerCreature) obj).radius = (int) value;
        }
      }

      private sealed class PROP_04cbdd81854140aca5f616234d129d17 : HardwiredMemberDescriptor
      {
        internal PROP_04cbdd81854140aca5f616234d129d17()
          : base(typeof (bool), "inTower", false, MemberDescriptorAccess.CanRead)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((ContainerCreature) obj).inTower;
        }
      }

      private sealed class PROP_a38b6da352b449ff8005875e6691d3df : HardwiredMemberDescriptor
      {
        internal PROP_a38b6da352b449ff8005875e6691d3df()
          : base(typeof (bool), "isMoving", false, MemberDescriptorAccess.CanRead)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((ContainerCreature) obj).isMoving;
        }
      }

      private sealed class PROP_ba2d66d2505b4c5e803febc53b06f95a : HardwiredMemberDescriptor
      {
        internal PROP_ba2d66d2505b4c5e803febc53b06f95a()
          : base(typeof (bool), "stunned", false, MemberDescriptorAccess.CanRead | MemberDescriptorAccess.CanWrite)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((ContainerCreature) obj).stunned;
        }

        protected override void SetValueImpl(Script script, object obj, object value)
        {
          ((ContainerCreature) obj).stunned = (bool) value;
        }
      }

      private sealed class PROP_7527f248a4db445595287e02ce2c9951 : HardwiredMemberDescriptor
      {
        internal PROP_7527f248a4db445595287e02ce2c9951()
          : base(typeof (bool), "superStunned", false, MemberDescriptorAccess.CanRead | MemberDescriptorAccess.CanWrite)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((ContainerCreature) obj).superStunned;
        }

        protected override void SetValueImpl(Script script, object obj, object value)
        {
          ((ContainerCreature) obj).superStunned = (bool) value;
        }
      }

      private sealed class PROP_4cd5365aae524738819e81e655fa7183 : HardwiredMemberDescriptor
      {
        internal PROP_4cd5365aae524738819e81e655fa7183()
          : base(typeof (bool), "waterWalking", false, MemberDescriptorAccess.CanRead | MemberDescriptorAccess.CanWrite)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((ContainerCreature) obj).waterWalking;
        }

        protected override void SetValueImpl(Script script, object obj, object value)
        {
          ((ContainerCreature) obj).waterWalking = (bool) value;
        }
      }

      private sealed class PROP_1d913b501f9a4f6a93d9e45570303a48 : HardwiredMemberDescriptor
      {
        internal PROP_1d913b501f9a4f6a93d9e45570303a48()
          : base(typeof (bool), "frostWalking", false, MemberDescriptorAccess.CanRead | MemberDescriptorAccess.CanWrite)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((ContainerCreature) obj).frostWalking;
        }

        protected override void SetValueImpl(Script script, object obj, object value)
        {
          ((ContainerCreature) obj).frostWalking = (bool) value;
        }
      }

      private sealed class PROP_bf924cb83e944503b6c18f540da69796 : HardwiredMemberDescriptor
      {
        internal PROP_bf924cb83e944503b6c18f540da69796()
          : base(typeof (bool), "inWater", false, MemberDescriptorAccess.CanRead)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((ContainerCreature) obj).inWater;
        }
      }

      private sealed class PROP_f266f1cb50454b44868b702bc3d81406 : HardwiredMemberDescriptor
      {
        internal PROP_f266f1cb50454b44868b702bc3d81406()
          : base(typeof (bool), "diesInWater", false, MemberDescriptorAccess.CanRead | MemberDescriptorAccess.CanWrite)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((ContainerCreature) obj).diesInWater;
        }

        protected override void SetValueImpl(Script script, object obj, object value)
        {
          ((ContainerCreature) obj).diesInWater = (bool) value;
        }
      }

      private sealed class PROP_32977183d79b46cd946bbd9c350830ce : HardwiredMemberDescriptor
      {
        internal PROP_32977183d79b46cd946bbd9c350830ce()
          : base(typeof (CreatureType), "type", false, MemberDescriptorAccess.CanRead | MemberDescriptorAccess.CanWrite)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((ContainerCreature) obj).type;
        }

        protected override void SetValueImpl(Script script, object obj, object value)
        {
          ((ContainerCreature) obj).type = (CreatureType) value;
        }
      }

      private sealed class PROP_85ae86019d5c440a91ef5dc63c0cb34d : HardwiredMemberDescriptor
      {
        internal PROP_85ae86019d5c440a91ef5dc63c0cb34d()
          : base(typeof (CreatureRace), "race", false, MemberDescriptorAccess.CanRead | MemberDescriptorAccess.CanWrite)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((ContainerCreature) obj).race;
        }

        protected override void SetValueImpl(Script script, object obj, object value)
        {
          ((ContainerCreature) obj).race = (CreatureRace) value;
        }
      }

      private sealed class PROP_05dbb28eedd4402395d1906462327edb : HardwiredMemberDescriptor
      {
        internal PROP_05dbb28eedd4402395d1906462327edb()
          : base(typeof (bool), "yourTurn", false, MemberDescriptorAccess.CanRead)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((ContainerCreature) obj).yourTurn;
        }
      }

      private sealed class PROP_3d9a95b7845b4c4197391d8917ec110a : HardwiredMemberDescriptor
      {
        internal PROP_3d9a95b7845b4c4197391d8917ec110a()
          : base(typeof (bool), "isRider", false, MemberDescriptorAccess.CanRead)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((ContainerCreature) obj).isRider;
        }
      }

      private sealed class PROP_cdc7cb274f044cd2a6dc1867627c50dd : HardwiredMemberDescriptor
      {
        internal PROP_cdc7cb274f044cd2a6dc1867627c50dd()
          : base(typeof (bool), "isMounted", false, MemberDescriptorAccess.CanRead)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((ContainerCreature) obj).isMounted;
        }
      }

      private sealed class PROP_46a2041c52724d618eaa83c9541d99a5 : HardwiredMemberDescriptor
      {
        internal PROP_46a2041c52724d618eaa83c9541d99a5()
          : base(typeof (bool), "isDead", false, MemberDescriptorAccess.CanRead)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((ContainerCreature) obj).isDead;
        }
      }

      private sealed class PROP_207bcf6ef0784e119bed91e27955f2f0 : HardwiredMemberDescriptor
      {
        internal PROP_207bcf6ef0784e119bed91e27955f2f0()
          : base(typeof (bool), "isPawn", false, MemberDescriptorAccess.CanRead)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((ContainerCreature) obj).isPawn;
        }
      }

      private sealed class PROP_aca5f2c16d5a46c4af865585c9ff9d8f : HardwiredMemberDescriptor
      {
        internal PROP_aca5f2c16d5a46c4af865585c9ff9d8f()
          : base(typeof (bool), "isFlying", false, MemberDescriptorAccess.CanRead | MemberDescriptorAccess.CanWrite)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((ContainerCreature) obj).isFlying;
        }

        protected override void SetValueImpl(Script script, object obj, object value)
        {
          ((ContainerCreature) obj).isFlying = (bool) value;
        }
      }

      private sealed class PROP_0277c418a9554949acf6b55e32ae7011 : HardwiredMemberDescriptor
      {
        internal PROP_0277c418a9554949acf6b55e32ae7011()
          : base(typeof (bool), "isMountable", false, MemberDescriptorAccess.CanRead)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((ContainerCreature) obj).isMountable;
        }
      }

      private sealed class PROP_fd032eb9081944d0ab33acf250054588 : HardwiredMemberDescriptor
      {
        internal PROP_fd032eb9081944d0ab33acf250054588()
          : base(typeof (bool), "canMount", false, MemberDescriptorAccess.CanRead)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((ContainerCreature) obj).canMount;
        }
      }

      private sealed class PROP_612e418d9da34643876ab610e61748b4 : HardwiredMemberDescriptor
      {
        internal PROP_612e418d9da34643876ab610e61748b4()
          : base(typeof (bool), "arcaneMonster", false, MemberDescriptorAccess.CanRead | MemberDescriptorAccess.CanWrite)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((ContainerCreature) obj).arcaneMonster;
        }

        protected override void SetValueImpl(Script script, object obj, object value)
        {
          ((ContainerCreature) obj).arcaneMonster = (bool) value;
        }
      }

      private sealed class PROP_02772e8a3f3446e4bc2aa2bc6f8f0ebf : HardwiredMemberDescriptor
      {
        internal PROP_02772e8a3f3446e4bc2aa2bc6f8f0ebf()
          : base(typeof (bool), "takeDamage", false, MemberDescriptorAccess.CanRead | MemberDescriptorAccess.CanWrite)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((ContainerCreature) obj).takeDamage;
        }

        protected override void SetValueImpl(Script script, object obj, object value)
        {
          ((ContainerCreature) obj).takeDamage = (bool) value;
        }
      }
    }

    private sealed class TYPE_e10a01835d594f689146ef35e3a66dfe : HardwiredUserDataDescriptor
    {
      internal TYPE_e10a01835d594f689146ef35e3a66dfe()
        : base(typeof (ContainerEffector))
      {
        this.AddMember("get_isDead", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_isDead", typeof (ContainerEffector), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_e10a01835d594f689146ef35e3a66dfe.MTHD_622077ebaca64ee68b40df3ab3d044ac()
        }));
        this.AddMember("get_x", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_x", typeof (ContainerEffector), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_e10a01835d594f689146ef35e3a66dfe.MTHD_0d25797003c4487798126617b3847a8f()
        }));
        this.AddMember("set_x", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("set_x", typeof (ContainerEffector), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_e10a01835d594f689146ef35e3a66dfe.MTHD_2798cb9f42cd4584802678d355c85b39()
        }));
        this.AddMember("get_y", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_y", typeof (ContainerEffector), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_e10a01835d594f689146ef35e3a66dfe.MTHD_ed9754311a70425ba15f20dfddfaf0c3()
        }));
        this.AddMember("set_y", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("set_y", typeof (ContainerEffector), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_e10a01835d594f689146ef35e3a66dfe.MTHD_cf76731b31134b2986eaa2da7b3ff032()
        }));
        this.AddMember("get_position", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_position", typeof (ContainerEffector), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_e10a01835d594f689146ef35e3a66dfe.MTHD_7f3de362386a4ba894fc0615fd7c135e()
        }));
        this.AddMember("set_position", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("set_position", typeof (ContainerEffector), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_e10a01835d594f689146ef35e3a66dfe.MTHD_53097b8b1564479dac897de8258234e5()
        }));
        this.AddMember("get_turnsAlive", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_turnsAlive", typeof (ContainerEffector), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_e10a01835d594f689146ef35e3a66dfe.MTHD_cab54daf7982410491580bea2bb25f99()
        }));
        this.AddMember("set_turnsAlive", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("set_turnsAlive", typeof (ContainerEffector), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_e10a01835d594f689146ef35e3a66dfe.MTHD_dbabb753d3fb4e5ba5ad829c45770088()
        }));
        this.AddMember("get_maxTurnsAlive", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_maxTurnsAlive", typeof (ContainerEffector), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_e10a01835d594f689146ef35e3a66dfe.MTHD_f479d3723af64490bc5ac8af28a57424()
        }));
        this.AddMember("set_maxTurnsAlive", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("set_maxTurnsAlive", typeof (ContainerEffector), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_e10a01835d594f689146ef35e3a66dfe.MTHD_214f24270cc940048b6650ff06886566()
        }));
        this.AddMember("get_active", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_active", typeof (ContainerEffector), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_e10a01835d594f689146ef35e3a66dfe.MTHD_62386d151a9342f39460993dcdec7427()
        }));
        this.AddMember("set_active", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("set_active", typeof (ContainerEffector), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_e10a01835d594f689146ef35e3a66dfe.MTHD_705466bbdc7d45bfa0a912a14e0f4474()
        }));
        this.AddMember("get_type", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_type", typeof (ContainerEffector), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_e10a01835d594f689146ef35e3a66dfe.MTHD_0358290bdc2d4de2a4aff8f580bd23f6()
        }));
        this.AddMember("destroy", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("destroy", typeof (ContainerEffector), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_e10a01835d594f689146ef35e3a66dfe.MTHD_11bab0c50a6b475d920e030f7510c791()
        }));
        this.AddMember("turnPassed", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("turnPassed", typeof (ContainerEffector), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_e10a01835d594f689146ef35e3a66dfe.MTHD_190458fede234e5ebfa1fc30ec49718a()
        }));
        this.AddMember("GetHashCode", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("GetHashCode", typeof (ContainerEffector), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_e10a01835d594f689146ef35e3a66dfe.MTHD_7816f76d412d4be3829c8abcbda26d37()
        }));
        this.AddMember("Equals", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("Equals", typeof (ContainerEffector), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_e10a01835d594f689146ef35e3a66dfe.MTHD_dfac1d31da3c4210afd293f9fd548c8c()
        }));
        this.AddMember("GetType", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("GetType", typeof (ContainerEffector), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_e10a01835d594f689146ef35e3a66dfe.MTHD_124719178cc643189afd2c2b4cd080e6()
        }));
        this.AddMember("ToString", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("ToString", typeof (ContainerEffector), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_e10a01835d594f689146ef35e3a66dfe.MTHD_eab71196fdfe4e219b3e4979c1024b9c()
        }));
        this.AddMember("isDead", (IMemberDescriptor) new Bridge.TYPE_e10a01835d594f689146ef35e3a66dfe.PROP_fcc6573cd416408785ee794641bc610e());
        this.AddMember("x", (IMemberDescriptor) new Bridge.TYPE_e10a01835d594f689146ef35e3a66dfe.PROP_56c534454cff4ab5aa839899e495156b());
        this.AddMember("y", (IMemberDescriptor) new Bridge.TYPE_e10a01835d594f689146ef35e3a66dfe.PROP_3d359cf5d9ed44e3948937d36f1d3889());
        this.AddMember("position", (IMemberDescriptor) new Bridge.TYPE_e10a01835d594f689146ef35e3a66dfe.PROP_3e808198da3c44a4a9b85bb2db3777ca());
        this.AddMember("turnsAlive", (IMemberDescriptor) new Bridge.TYPE_e10a01835d594f689146ef35e3a66dfe.PROP_a5e5d2cbc22c4eef95ab04b856e04815());
        this.AddMember("maxTurnsAlive", (IMemberDescriptor) new Bridge.TYPE_e10a01835d594f689146ef35e3a66dfe.PROP_5647853722f54dafb41c77af48cbd271());
        this.AddMember("active", (IMemberDescriptor) new Bridge.TYPE_e10a01835d594f689146ef35e3a66dfe.PROP_a854a570f6474ccd85152919ad2a5751());
        this.AddMember("type", (IMemberDescriptor) new Bridge.TYPE_e10a01835d594f689146ef35e3a66dfe.PROP_25458eaabdd043e1a0c8cc996bef6273());
      }

      private sealed class MTHD_622077ebaca64ee68b40df3ab3d044ac : HardwiredMethodMemberDescriptor
      {
        internal MTHD_622077ebaca64ee68b40df3ab3d044ac()
        {
          this.Initialize("get_isDead", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerEffector) obj).isDead;
        }
      }

      private sealed class MTHD_0d25797003c4487798126617b3847a8f : HardwiredMethodMemberDescriptor
      {
        internal MTHD_0d25797003c4487798126617b3847a8f()
        {
          this.Initialize("get_x", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerEffector) obj).x;
        }
      }

      private sealed class MTHD_2798cb9f42cd4584802678d355c85b39 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_2798cb9f42cd4584802678d355c85b39()
        {
          this.Initialize("set_x", false, new ParameterDescriptor[1]
          {
            new ParameterDescriptor("value", typeof (int))
          }, false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          ((ContainerEffector) obj).x = (int) pars[0];
          return (object) null;
        }
      }

      private sealed class MTHD_ed9754311a70425ba15f20dfddfaf0c3 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_ed9754311a70425ba15f20dfddfaf0c3()
        {
          this.Initialize("get_y", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerEffector) obj).y;
        }
      }

      private sealed class MTHD_cf76731b31134b2986eaa2da7b3ff032 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_cf76731b31134b2986eaa2da7b3ff032()
        {
          this.Initialize("set_y", false, new ParameterDescriptor[1]
          {
            new ParameterDescriptor("value", typeof (int))
          }, false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          ((ContainerEffector) obj).y = (int) pars[0];
          return (object) null;
        }
      }

      private sealed class MTHD_7f3de362386a4ba894fc0615fd7c135e : HardwiredMethodMemberDescriptor
      {
        internal MTHD_7f3de362386a4ba894fc0615fd7c135e()
        {
          this.Initialize("get_position", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerEffector) obj).position;
        }
      }

      private sealed class MTHD_53097b8b1564479dac897de8258234e5 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_53097b8b1564479dac897de8258234e5()
        {
          this.Initialize("set_position", false, new ParameterDescriptor[1]
          {
            new ParameterDescriptor("value", typeof (Educative.Point))
          }, false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          ((ContainerEffector) obj).position = (Educative.Point) pars[0];
          return (object) null;
        }
      }

      private sealed class MTHD_cab54daf7982410491580bea2bb25f99 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_cab54daf7982410491580bea2bb25f99()
        {
          this.Initialize("get_turnsAlive", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerEffector) obj).turnsAlive;
        }
      }

      private sealed class MTHD_dbabb753d3fb4e5ba5ad829c45770088 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_dbabb753d3fb4e5ba5ad829c45770088()
        {
          this.Initialize("set_turnsAlive", false, new ParameterDescriptor[1]
          {
            new ParameterDescriptor("value", typeof (int))
          }, false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          ((ContainerEffector) obj).turnsAlive = (int) pars[0];
          return (object) null;
        }
      }

      private sealed class MTHD_f479d3723af64490bc5ac8af28a57424 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_f479d3723af64490bc5ac8af28a57424()
        {
          this.Initialize("get_maxTurnsAlive", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerEffector) obj).maxTurnsAlive;
        }
      }

      private sealed class MTHD_214f24270cc940048b6650ff06886566 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_214f24270cc940048b6650ff06886566()
        {
          this.Initialize("set_maxTurnsAlive", false, new ParameterDescriptor[1]
          {
            new ParameterDescriptor("value", typeof (int))
          }, false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          ((ContainerEffector) obj).maxTurnsAlive = (int) pars[0];
          return (object) null;
        }
      }

      private sealed class MTHD_62386d151a9342f39460993dcdec7427 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_62386d151a9342f39460993dcdec7427()
        {
          this.Initialize("get_active", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerEffector) obj).active;
        }
      }

      private sealed class MTHD_705466bbdc7d45bfa0a912a14e0f4474 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_705466bbdc7d45bfa0a912a14e0f4474()
        {
          this.Initialize("set_active", false, new ParameterDescriptor[1]
          {
            new ParameterDescriptor("value", typeof (bool))
          }, false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          ((ContainerEffector) obj).active = (bool) pars[0];
          return (object) null;
        }
      }

      private sealed class MTHD_0358290bdc2d4de2a4aff8f580bd23f6 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_0358290bdc2d4de2a4aff8f580bd23f6()
        {
          this.Initialize("get_type", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerEffector) obj).type;
        }
      }

      private sealed class MTHD_11bab0c50a6b475d920e030f7510c791 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_11bab0c50a6b475d920e030f7510c791()
        {
          this.Initialize("destroy", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          ((ContainerEffector) obj).destroy();
          return (object) DynValue.Void;
        }
      }

      private sealed class MTHD_190458fede234e5ebfa1fc30ec49718a : HardwiredMethodMemberDescriptor
      {
        internal MTHD_190458fede234e5ebfa1fc30ec49718a()
        {
          this.Initialize("turnPassed", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          ((ContainerEffector) obj).turnPassed();
          return (object) DynValue.Void;
        }
      }

      private sealed class MTHD_7816f76d412d4be3829c8abcbda26d37 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_7816f76d412d4be3829c8abcbda26d37()
        {
          this.Initialize("GetHashCode", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerEffector) obj).GetHashCode();
        }
      }

      private sealed class MTHD_dfac1d31da3c4210afd293f9fd548c8c : HardwiredMethodMemberDescriptor
      {
        internal MTHD_dfac1d31da3c4210afd293f9fd548c8c()
        {
          this.Initialize("Equals", false, new ParameterDescriptor[1]
          {
            new ParameterDescriptor("obj", typeof (object))
          }, false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) obj.Equals(pars[0]);
        }
      }

      private sealed class MTHD_124719178cc643189afd2c2b4cd080e6 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_124719178cc643189afd2c2b4cd080e6()
        {
          this.Initialize("GetType", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) obj.GetType();
        }
      }

      private sealed class MTHD_eab71196fdfe4e219b3e4979c1024b9c : HardwiredMethodMemberDescriptor
      {
        internal MTHD_eab71196fdfe4e219b3e4979c1024b9c()
        {
          this.Initialize("ToString", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) obj.ToString();
        }
      }

      private sealed class PROP_fcc6573cd416408785ee794641bc610e : HardwiredMemberDescriptor
      {
        internal PROP_fcc6573cd416408785ee794641bc610e()
          : base(typeof (bool), "isDead", false, MemberDescriptorAccess.CanRead)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((ContainerEffector) obj).isDead;
        }
      }

      private sealed class PROP_56c534454cff4ab5aa839899e495156b : HardwiredMemberDescriptor
      {
        internal PROP_56c534454cff4ab5aa839899e495156b()
          : base(typeof (int), "x", false, MemberDescriptorAccess.CanRead | MemberDescriptorAccess.CanWrite)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((ContainerEffector) obj).x;
        }

        protected override void SetValueImpl(Script script, object obj, object value)
        {
          ((ContainerEffector) obj).x = (int) value;
        }
      }

      private sealed class PROP_3d359cf5d9ed44e3948937d36f1d3889 : HardwiredMemberDescriptor
      {
        internal PROP_3d359cf5d9ed44e3948937d36f1d3889()
          : base(typeof (int), "y", false, MemberDescriptorAccess.CanRead | MemberDescriptorAccess.CanWrite)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((ContainerEffector) obj).y;
        }

        protected override void SetValueImpl(Script script, object obj, object value)
        {
          ((ContainerEffector) obj).y = (int) value;
        }
      }

      private sealed class PROP_3e808198da3c44a4a9b85bb2db3777ca : HardwiredMemberDescriptor
      {
        internal PROP_3e808198da3c44a4a9b85bb2db3777ca()
          : base(typeof (Educative.Point), "position", false, MemberDescriptorAccess.CanRead | MemberDescriptorAccess.CanWrite)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((ContainerEffector) obj).position;
        }

        protected override void SetValueImpl(Script script, object obj, object value)
        {
          ((ContainerEffector) obj).position = (Educative.Point) value;
        }
      }

      private sealed class PROP_a5e5d2cbc22c4eef95ab04b856e04815 : HardwiredMemberDescriptor
      {
        internal PROP_a5e5d2cbc22c4eef95ab04b856e04815()
          : base(typeof (int), "turnsAlive", false, MemberDescriptorAccess.CanRead | MemberDescriptorAccess.CanWrite)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((ContainerEffector) obj).turnsAlive;
        }

        protected override void SetValueImpl(Script script, object obj, object value)
        {
          ((ContainerEffector) obj).turnsAlive = (int) value;
        }
      }

      private sealed class PROP_5647853722f54dafb41c77af48cbd271 : HardwiredMemberDescriptor
      {
        internal PROP_5647853722f54dafb41c77af48cbd271()
          : base(typeof (int), "maxTurnsAlive", false, MemberDescriptorAccess.CanRead | MemberDescriptorAccess.CanWrite)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((ContainerEffector) obj).maxTurnsAlive;
        }

        protected override void SetValueImpl(Script script, object obj, object value)
        {
          ((ContainerEffector) obj).maxTurnsAlive = (int) value;
        }
      }

      private sealed class PROP_a854a570f6474ccd85152919ad2a5751 : HardwiredMemberDescriptor
      {
        internal PROP_a854a570f6474ccd85152919ad2a5751()
          : base(typeof (bool), "active", false, MemberDescriptorAccess.CanRead | MemberDescriptorAccess.CanWrite)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((ContainerEffector) obj).active;
        }

        protected override void SetValueImpl(Script script, object obj, object value)
        {
          ((ContainerEffector) obj).active = (bool) value;
        }
      }

      private sealed class PROP_25458eaabdd043e1a0c8cc996bef6273 : HardwiredMemberDescriptor
      {
        internal PROP_25458eaabdd043e1a0c8cc996bef6273()
          : base(typeof (EffectorType), "type", false, MemberDescriptorAccess.CanRead)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((ContainerEffector) obj).type;
        }
      }
    }

    private sealed class TYPE_a0a43c0696fc406a8dfb4a90e09fb5ea : HardwiredUserDataDescriptor
    {
      internal TYPE_a0a43c0696fc406a8dfb4a90e09fb5ea()
        : base(typeof (ContainerIndicator))
      {
        this.AddMember("get_isDead", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_isDead", typeof (ContainerIndicator), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_a0a43c0696fc406a8dfb4a90e09fb5ea.MTHD_bb6d0e08f41f473584f4e506ad9c8c6d()
        }));
        this.AddMember("get_x", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_x", typeof (ContainerIndicator), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_a0a43c0696fc406a8dfb4a90e09fb5ea.MTHD_3134a71dce6745c2a92d3ff1da2c1ef1()
        }));
        this.AddMember("set_x", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("set_x", typeof (ContainerIndicator), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_a0a43c0696fc406a8dfb4a90e09fb5ea.MTHD_8ad39af2c82c4d169180806fa64c39ef()
        }));
        this.AddMember("get_y", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_y", typeof (ContainerIndicator), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_a0a43c0696fc406a8dfb4a90e09fb5ea.MTHD_0e73a0c27f80498b89e465b1a3e9180b()
        }));
        this.AddMember("set_y", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("set_y", typeof (ContainerIndicator), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_a0a43c0696fc406a8dfb4a90e09fb5ea.MTHD_ef03b8c36b414d7298ef1bacd8e0c91d()
        }));
        this.AddMember("get_radius", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_radius", typeof (ContainerIndicator), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_a0a43c0696fc406a8dfb4a90e09fb5ea.MTHD_cd5936b953d546719efad8b846bc214f()
        }));
        this.AddMember("set_radius", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("set_radius", typeof (ContainerIndicator), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_a0a43c0696fc406a8dfb4a90e09fb5ea.MTHD_8ba68e001d0244e5a0688d06b4809d8b()
        }));
        this.AddMember("get_position", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_position", typeof (ContainerIndicator), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_a0a43c0696fc406a8dfb4a90e09fb5ea.MTHD_b6bd484696da4127bd205703f21d7aeb()
        }));
        this.AddMember("set_position", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("set_position", typeof (ContainerIndicator), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_a0a43c0696fc406a8dfb4a90e09fb5ea.MTHD_226e733b164c4ad9a6ea3b3076bf4938()
        }));
        this.AddMember("get_angle", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_angle", typeof (ContainerIndicator), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_a0a43c0696fc406a8dfb4a90e09fb5ea.MTHD_e19d5c4376f94409854a10608021f32d()
        }));
        this.AddMember("set_angle", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("set_angle", typeof (ContainerIndicator), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_a0a43c0696fc406a8dfb4a90e09fb5ea.MTHD_b491e29950aa469fb6a1c17f52fdf884()
        }));
        this.AddMember("get_color", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_color", typeof (ContainerIndicator), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_a0a43c0696fc406a8dfb4a90e09fb5ea.MTHD_d92362e1ac97461dba587eb5ca2284b3()
        }));
        this.AddMember("set_color", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("set_color", typeof (ContainerIndicator), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_a0a43c0696fc406a8dfb4a90e09fb5ea.MTHD_0ee2f4fc18cd419fa358caf69c40e89f()
        }));
        this.AddMember("get_kind", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_kind", typeof (ContainerIndicator), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_a0a43c0696fc406a8dfb4a90e09fb5ea.MTHD_0e5386d9b23a4c7197667e0d1707339b()
        }));
        this.AddMember("Destroy", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("Destroy", typeof (ContainerIndicator), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_a0a43c0696fc406a8dfb4a90e09fb5ea.MTHD_cb999e702a6844ce93ca41cf9c442a19()
        }));
        this.AddMember("Equals", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("Equals", typeof (ContainerIndicator), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_a0a43c0696fc406a8dfb4a90e09fb5ea.MTHD_5ae6e5c0df05439d87af904606897a86()
        }));
        this.AddMember("GetHashCode", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("GetHashCode", typeof (ContainerIndicator), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_a0a43c0696fc406a8dfb4a90e09fb5ea.MTHD_b0d2f210f2ab41d0bd10002b479c122b()
        }));
        this.AddMember("GetType", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("GetType", typeof (ContainerIndicator), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_a0a43c0696fc406a8dfb4a90e09fb5ea.MTHD_ac011956aa1244459ed0396e9a15a8e0()
        }));
        this.AddMember("ToString", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("ToString", typeof (ContainerIndicator), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_a0a43c0696fc406a8dfb4a90e09fb5ea.MTHD_3a07979d3d4a4b979e8117c1dbc52e61()
        }));
        this.AddMember("isDead", (IMemberDescriptor) new Bridge.TYPE_a0a43c0696fc406a8dfb4a90e09fb5ea.PROP_cc070727a28a4622b497b9aea6219b14());
        this.AddMember("x", (IMemberDescriptor) new Bridge.TYPE_a0a43c0696fc406a8dfb4a90e09fb5ea.PROP_26b0d3797e8d4a648d22bbcd632b17fb());
        this.AddMember("y", (IMemberDescriptor) new Bridge.TYPE_a0a43c0696fc406a8dfb4a90e09fb5ea.PROP_0227cc02ae9843a8a2cfdd79478f6dfa());
        this.AddMember("radius", (IMemberDescriptor) new Bridge.TYPE_a0a43c0696fc406a8dfb4a90e09fb5ea.PROP_2b4b5e02d5db445fb05376965e29f294());
        this.AddMember("position", (IMemberDescriptor) new Bridge.TYPE_a0a43c0696fc406a8dfb4a90e09fb5ea.PROP_c8928a5ee41845ecb125b0d6bd1a5da9());
        this.AddMember("angle", (IMemberDescriptor) new Bridge.TYPE_a0a43c0696fc406a8dfb4a90e09fb5ea.PROP_6fa33bbd60a94053b0559fafe9735593());
        this.AddMember("color", (IMemberDescriptor) new Bridge.TYPE_a0a43c0696fc406a8dfb4a90e09fb5ea.PROP_5141bd936d9a46a6888139b16d1fd966());
        this.AddMember("kind", (IMemberDescriptor) new Bridge.TYPE_a0a43c0696fc406a8dfb4a90e09fb5ea.PROP_bf057be36bb44853b91024fc157e4603());
      }

      private sealed class MTHD_bb6d0e08f41f473584f4e506ad9c8c6d : HardwiredMethodMemberDescriptor
      {
        internal MTHD_bb6d0e08f41f473584f4e506ad9c8c6d()
        {
          this.Initialize("get_isDead", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerIndicator) obj).isDead;
        }
      }

      private sealed class MTHD_3134a71dce6745c2a92d3ff1da2c1ef1 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_3134a71dce6745c2a92d3ff1da2c1ef1()
        {
          this.Initialize("get_x", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerIndicator) obj).x;
        }
      }

      private sealed class MTHD_8ad39af2c82c4d169180806fa64c39ef : HardwiredMethodMemberDescriptor
      {
        internal MTHD_8ad39af2c82c4d169180806fa64c39ef()
        {
          this.Initialize("set_x", false, new ParameterDescriptor[1]
          {
            new ParameterDescriptor("value", typeof (int))
          }, false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          ((ContainerIndicator) obj).x = (int) pars[0];
          return (object) null;
        }
      }

      private sealed class MTHD_0e73a0c27f80498b89e465b1a3e9180b : HardwiredMethodMemberDescriptor
      {
        internal MTHD_0e73a0c27f80498b89e465b1a3e9180b()
        {
          this.Initialize("get_y", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerIndicator) obj).y;
        }
      }

      private sealed class MTHD_ef03b8c36b414d7298ef1bacd8e0c91d : HardwiredMethodMemberDescriptor
      {
        internal MTHD_ef03b8c36b414d7298ef1bacd8e0c91d()
        {
          this.Initialize("set_y", false, new ParameterDescriptor[1]
          {
            new ParameterDescriptor("value", typeof (int))
          }, false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          ((ContainerIndicator) obj).y = (int) pars[0];
          return (object) null;
        }
      }

      private sealed class MTHD_cd5936b953d546719efad8b846bc214f : HardwiredMethodMemberDescriptor
      {
        internal MTHD_cd5936b953d546719efad8b846bc214f()
        {
          this.Initialize("get_radius", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerIndicator) obj).radius;
        }
      }

      private sealed class MTHD_8ba68e001d0244e5a0688d06b4809d8b : HardwiredMethodMemberDescriptor
      {
        internal MTHD_8ba68e001d0244e5a0688d06b4809d8b()
        {
          this.Initialize("set_radius", false, new ParameterDescriptor[1]
          {
            new ParameterDescriptor("value", typeof (double))
          }, false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          ((ContainerIndicator) obj).radius = (double) pars[0];
          return (object) null;
        }
      }

      private sealed class MTHD_b6bd484696da4127bd205703f21d7aeb : HardwiredMethodMemberDescriptor
      {
        internal MTHD_b6bd484696da4127bd205703f21d7aeb()
        {
          this.Initialize("get_position", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerIndicator) obj).position;
        }
      }

      private sealed class MTHD_226e733b164c4ad9a6ea3b3076bf4938 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_226e733b164c4ad9a6ea3b3076bf4938()
        {
          this.Initialize("set_position", false, new ParameterDescriptor[1]
          {
            new ParameterDescriptor("value", typeof (Educative.Point))
          }, false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          ((ContainerIndicator) obj).position = (Educative.Point) pars[0];
          return (object) null;
        }
      }

      private sealed class MTHD_e19d5c4376f94409854a10608021f32d : HardwiredMethodMemberDescriptor
      {
        internal MTHD_e19d5c4376f94409854a10608021f32d()
        {
          this.Initialize("get_angle", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerIndicator) obj).angle;
        }
      }

      private sealed class MTHD_b491e29950aa469fb6a1c17f52fdf884 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_b491e29950aa469fb6a1c17f52fdf884()
        {
          this.Initialize("set_angle", false, new ParameterDescriptor[1]
          {
            new ParameterDescriptor("value", typeof (double))
          }, false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          ((ContainerIndicator) obj).angle = (double) pars[0];
          return (object) null;
        }
      }

      private sealed class MTHD_d92362e1ac97461dba587eb5ca2284b3 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_d92362e1ac97461dba587eb5ca2284b3()
        {
          this.Initialize("get_color", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerIndicator) obj).color;
        }
      }

      private sealed class MTHD_0ee2f4fc18cd419fa358caf69c40e89f : HardwiredMethodMemberDescriptor
      {
        internal MTHD_0ee2f4fc18cd419fa358caf69c40e89f()
        {
          this.Initialize("set_color", false, new ParameterDescriptor[1]
          {
            new ParameterDescriptor("value", typeof (string))
          }, false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          ((ContainerIndicator) obj).color = (string) pars[0];
          return (object) null;
        }
      }

      private sealed class MTHD_0e5386d9b23a4c7197667e0d1707339b : HardwiredMethodMemberDescriptor
      {
        internal MTHD_0e5386d9b23a4c7197667e0d1707339b()
        {
          this.Initialize("get_kind", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerIndicator) obj).kind;
        }
      }

      private sealed class MTHD_cb999e702a6844ce93ca41cf9c442a19 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_cb999e702a6844ce93ca41cf9c442a19()
        {
          this.Initialize("Destroy", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          ((ContainerIndicator) obj).Destroy();
          return (object) DynValue.Void;
        }
      }

      private sealed class MTHD_5ae6e5c0df05439d87af904606897a86 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_5ae6e5c0df05439d87af904606897a86()
        {
          this.Initialize("Equals", false, new ParameterDescriptor[1]
          {
            new ParameterDescriptor("obj", typeof (object))
          }, false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) obj.Equals(pars[0]);
        }
      }

      private sealed class MTHD_b0d2f210f2ab41d0bd10002b479c122b : HardwiredMethodMemberDescriptor
      {
        internal MTHD_b0d2f210f2ab41d0bd10002b479c122b()
        {
          this.Initialize("GetHashCode", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) obj.GetHashCode();
        }
      }

      private sealed class MTHD_ac011956aa1244459ed0396e9a15a8e0 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_ac011956aa1244459ed0396e9a15a8e0()
        {
          this.Initialize("GetType", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) obj.GetType();
        }
      }

      private sealed class MTHD_3a07979d3d4a4b979e8117c1dbc52e61 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_3a07979d3d4a4b979e8117c1dbc52e61()
        {
          this.Initialize("ToString", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) obj.ToString();
        }
      }

      private sealed class PROP_cc070727a28a4622b497b9aea6219b14 : HardwiredMemberDescriptor
      {
        internal PROP_cc070727a28a4622b497b9aea6219b14()
          : base(typeof (bool), "isDead", false, MemberDescriptorAccess.CanRead)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((ContainerIndicator) obj).isDead;
        }
      }

      private sealed class PROP_26b0d3797e8d4a648d22bbcd632b17fb : HardwiredMemberDescriptor
      {
        internal PROP_26b0d3797e8d4a648d22bbcd632b17fb()
          : base(typeof (int), "x", false, MemberDescriptorAccess.CanRead | MemberDescriptorAccess.CanWrite)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((ContainerIndicator) obj).x;
        }

        protected override void SetValueImpl(Script script, object obj, object value)
        {
          ((ContainerIndicator) obj).x = (int) value;
        }
      }

      private sealed class PROP_0227cc02ae9843a8a2cfdd79478f6dfa : HardwiredMemberDescriptor
      {
        internal PROP_0227cc02ae9843a8a2cfdd79478f6dfa()
          : base(typeof (int), "y", false, MemberDescriptorAccess.CanRead | MemberDescriptorAccess.CanWrite)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((ContainerIndicator) obj).y;
        }

        protected override void SetValueImpl(Script script, object obj, object value)
        {
          ((ContainerIndicator) obj).y = (int) value;
        }
      }

      private sealed class PROP_2b4b5e02d5db445fb05376965e29f294 : HardwiredMemberDescriptor
      {
        internal PROP_2b4b5e02d5db445fb05376965e29f294()
          : base(typeof (double), "radius", false, MemberDescriptorAccess.CanRead | MemberDescriptorAccess.CanWrite)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((ContainerIndicator) obj).radius;
        }

        protected override void SetValueImpl(Script script, object obj, object value)
        {
          ((ContainerIndicator) obj).radius = (double) value;
        }
      }

      private sealed class PROP_c8928a5ee41845ecb125b0d6bd1a5da9 : HardwiredMemberDescriptor
      {
        internal PROP_c8928a5ee41845ecb125b0d6bd1a5da9()
          : base(typeof (Educative.Point), "position", false, MemberDescriptorAccess.CanRead | MemberDescriptorAccess.CanWrite)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((ContainerIndicator) obj).position;
        }

        protected override void SetValueImpl(Script script, object obj, object value)
        {
          ((ContainerIndicator) obj).position = (Educative.Point) value;
        }
      }

      private sealed class PROP_6fa33bbd60a94053b0559fafe9735593 : HardwiredMemberDescriptor
      {
        internal PROP_6fa33bbd60a94053b0559fafe9735593()
          : base(typeof (double), "angle", false, MemberDescriptorAccess.CanRead | MemberDescriptorAccess.CanWrite)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((ContainerIndicator) obj).angle;
        }

        protected override void SetValueImpl(Script script, object obj, object value)
        {
          ((ContainerIndicator) obj).angle = (double) value;
        }
      }

      private sealed class PROP_5141bd936d9a46a6888139b16d1fd966 : HardwiredMemberDescriptor
      {
        internal PROP_5141bd936d9a46a6888139b16d1fd966()
          : base(typeof (string), "color", false, MemberDescriptorAccess.CanRead | MemberDescriptorAccess.CanWrite)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((ContainerIndicator) obj).color;
        }

        protected override void SetValueImpl(Script script, object obj, object value)
        {
          ((ContainerIndicator) obj).color = (string) value;
        }
      }

      private sealed class PROP_bf057be36bb44853b91024fc157e4603 : HardwiredMemberDescriptor
      {
        internal PROP_bf057be36bb44853b91024fc157e4603()
          : base(typeof (IndicatorKind), "kind", false, MemberDescriptorAccess.CanRead)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((ContainerIndicator) obj).kind;
        }
      }
    }

    private sealed class TYPE_f368f307eac3440bbd1df12c504d2d1b : HardwiredUserDataDescriptor
    {
      internal TYPE_f368f307eac3440bbd1df12c504d2d1b()
        : base(typeof (ContainerSpell))
      {
        this.AddMember("get_uses", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_uses", typeof (ContainerSpell), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_f368f307eac3440bbd1df12c504d2d1b.MTHD_bd0d572e68dd415f9af85222836635f7()
        }));
        this.AddMember("set_uses", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("set_uses", typeof (ContainerSpell), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_f368f307eac3440bbd1df12c504d2d1b.MTHD_d644f10d0bee41359f6e58d792560880()
        }));
        this.AddMember("get_maxUses", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_maxUses", typeof (ContainerSpell), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_f368f307eac3440bbd1df12c504d2d1b.MTHD_1bb43e123b3549f9b94bb630510dd5e9()
        }));
        this.AddMember("set_maxUses", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("set_maxUses", typeof (ContainerSpell), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_f368f307eac3440bbd1df12c504d2d1b.MTHD_6d72abf1fbca47598cfdbbcee1ca6f2f()
        }));
        this.AddMember("get_rechargeTime", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_rechargeTime", typeof (ContainerSpell), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_f368f307eac3440bbd1df12c504d2d1b.MTHD_dfb1ec52d8244c2abc0fe3208ebed77a()
        }));
        this.AddMember("set_rechargeTime", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("set_rechargeTime", typeof (ContainerSpell), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_f368f307eac3440bbd1df12c504d2d1b.MTHD_7b2b53250ddf43288502dcaf78729aa1()
        }));
        this.AddMember("get_lastTurnFired", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_lastTurnFired", typeof (ContainerSpell), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_f368f307eac3440bbd1df12c504d2d1b.MTHD_e474445d26ea4115adc9ecd430ce93e3()
        }));
        this.AddMember("set_lastTurnFired", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("set_lastTurnFired", typeof (ContainerSpell), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_f368f307eac3440bbd1df12c504d2d1b.MTHD_0150f6fe818a4dffa8399df146056042()
        }));
        this.AddMember("get_isPresent", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_isPresent", typeof (ContainerSpell), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_f368f307eac3440bbd1df12c504d2d1b.MTHD_e93d14c82ac54e9d9b4985b9ad1f78fc()
        }));
        this.AddMember("set_isPresent", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("set_isPresent", typeof (ContainerSpell), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_f368f307eac3440bbd1df12c504d2d1b.MTHD_4bb258c9ec5f4b31824d7bbf3f7320ff()
        }));
        this.AddMember("get_locked", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_locked", typeof (ContainerSpell), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_f368f307eac3440bbd1df12c504d2d1b.MTHD_f68621dff9174f8f8a13288124050362()
        }));
        this.AddMember("set_locked", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("set_locked", typeof (ContainerSpell), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_f368f307eac3440bbd1df12c504d2d1b.MTHD_8ed4b7cfeea041a890bda2b94eccfc34()
        }));
        this.AddMember("get_turnsTillFirstUse", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_turnsTillFirstUse", typeof (ContainerSpell), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_f368f307eac3440bbd1df12c504d2d1b.MTHD_e130068113ba4f97b3444dcc3bd7011f()
        }));
        this.AddMember("set_turnsTillFirstUse", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("set_turnsTillFirstUse", typeof (ContainerSpell), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_f368f307eac3440bbd1df12c504d2d1b.MTHD_e68b329afe524e24ad81288b5672d194()
        }));
        this.AddMember("get_spellEnum", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_spellEnum", typeof (ContainerSpell), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_f368f307eac3440bbd1df12c504d2d1b.MTHD_3496481829bb4104b4fa4099932e2061()
        }));
        this.AddMember("get_castType", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_castType", typeof (ContainerSpell), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_f368f307eac3440bbd1df12c504d2d1b.MTHD_957f0b3dc105468fb5e9b81fb7180b00()
        }));
        this.AddMember("get_damageType", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_damageType", typeof (ContainerSpell), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_f368f307eac3440bbd1df12c504d2d1b.MTHD_042022f01cd14343bb9a4f3642fd8eb0()
        }));
        this.AddMember("get_book", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_book", typeof (ContainerSpell), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_f368f307eac3440bbd1df12c504d2d1b.MTHD_ec142f61561346f6bc166acce896b327()
        }));
        this.AddMember("get_damage", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_damage", typeof (ContainerSpell), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_f368f307eac3440bbd1df12c504d2d1b.MTHD_ecfcef3d11204ea2b547565ba940c0dc()
        }));
        this.AddMember("get_explosionRadius", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_explosionRadius", typeof (ContainerSpell), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_f368f307eac3440bbd1df12c504d2d1b.MTHD_04d38732880a47f398ebc333558961eb()
        }));
        this.AddMember("get_description", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_description", typeof (ContainerSpell), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_f368f307eac3440bbd1df12c504d2d1b.MTHD_a76d4a5003f0428180d58c3e5d35dafa()
        }));
        this.AddMember("get_descriptionOnly", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_descriptionOnly", typeof (ContainerSpell), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_f368f307eac3440bbd1df12c504d2d1b.MTHD_942120395ca1489caf4cccc2a3bc28b7()
        }));
        this.AddMember("get_descriptionExtra", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_descriptionExtra", typeof (ContainerSpell), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_f368f307eac3440bbd1df12c504d2d1b.MTHD_ac47f470abc7496ca82c6906b080a250()
        }));
        this.AddMember("get_name", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_name", typeof (ContainerSpell), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_f368f307eac3440bbd1df12c504d2d1b.MTHD_06b1653c54024ce9947a4885ff471982()
        }));
        this.AddMember("Equals", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("Equals", typeof (ContainerSpell), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_f368f307eac3440bbd1df12c504d2d1b.MTHD_7abe87f6a4b647a48f47711cf48cf2a9()
        }));
        this.AddMember("GetHashCode", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("GetHashCode", typeof (ContainerSpell), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_f368f307eac3440bbd1df12c504d2d1b.MTHD_93e804bba58b44ac8296b5b8ab4cc506()
        }));
        this.AddMember("GetType", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("GetType", typeof (ContainerSpell), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_f368f307eac3440bbd1df12c504d2d1b.MTHD_8b84b1130f11468a9e6e2fc104d83aac()
        }));
        this.AddMember("ToString", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("ToString", typeof (ContainerSpell), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_f368f307eac3440bbd1df12c504d2d1b.MTHD_5a1559bd6cee4b119134198d63e59996()
        }));
        this.AddMember("uses", (IMemberDescriptor) new Bridge.TYPE_f368f307eac3440bbd1df12c504d2d1b.PROP_61e8ed3555b647aabcf6c8810fa30196());
        this.AddMember("maxUses", (IMemberDescriptor) new Bridge.TYPE_f368f307eac3440bbd1df12c504d2d1b.PROP_cdbb347fe6674bbf9cdbb45665d6de87());
        this.AddMember("rechargeTime", (IMemberDescriptor) new Bridge.TYPE_f368f307eac3440bbd1df12c504d2d1b.PROP_7512a8b3d7d441958eb879c7c3cdad69());
        this.AddMember("lastTurnFired", (IMemberDescriptor) new Bridge.TYPE_f368f307eac3440bbd1df12c504d2d1b.PROP_633df974d92c4552b3657172f66c6b4c());
        this.AddMember("isPresent", (IMemberDescriptor) new Bridge.TYPE_f368f307eac3440bbd1df12c504d2d1b.PROP_47c817a2711443b68a692201f4b0b5ee());
        this.AddMember("locked", (IMemberDescriptor) new Bridge.TYPE_f368f307eac3440bbd1df12c504d2d1b.PROP_01c73b3d17db45a0bc3b39c50d9154c4());
        this.AddMember("turnsTillFirstUse", (IMemberDescriptor) new Bridge.TYPE_f368f307eac3440bbd1df12c504d2d1b.PROP_31dffdf86c2b4752b5813951c56f2578());
        this.AddMember("spellEnum", (IMemberDescriptor) new Bridge.TYPE_f368f307eac3440bbd1df12c504d2d1b.PROP_0e0ac94fdaca4e46aff371161caae336());
        this.AddMember("castType", (IMemberDescriptor) new Bridge.TYPE_f368f307eac3440bbd1df12c504d2d1b.PROP_5eebd292bbb54e01b8b844e1f6344f81());
        this.AddMember("damageType", (IMemberDescriptor) new Bridge.TYPE_f368f307eac3440bbd1df12c504d2d1b.PROP_ec7c04a11d83416ba27c297eba4fd0d6());
        this.AddMember("book", (IMemberDescriptor) new Bridge.TYPE_f368f307eac3440bbd1df12c504d2d1b.PROP_3ea08e149df74b33b50292c00b349405());
        this.AddMember("damage", (IMemberDescriptor) new Bridge.TYPE_f368f307eac3440bbd1df12c504d2d1b.PROP_cb3ea5507dfb40faa5e24a4956babf09());
        this.AddMember("explosionRadius", (IMemberDescriptor) new Bridge.TYPE_f368f307eac3440bbd1df12c504d2d1b.PROP_e9989b9d32f941cab470f14097fa5e8e());
        this.AddMember("description", (IMemberDescriptor) new Bridge.TYPE_f368f307eac3440bbd1df12c504d2d1b.PROP_d252be305d494dc892122a7552c23114());
        this.AddMember("descriptionOnly", (IMemberDescriptor) new Bridge.TYPE_f368f307eac3440bbd1df12c504d2d1b.PROP_286cd2e4d7f54edf80728159b437e278());
        this.AddMember("descriptionExtra", (IMemberDescriptor) new Bridge.TYPE_f368f307eac3440bbd1df12c504d2d1b.PROP_9c754d220c1f4524b0d9c4a142276251());
        this.AddMember("name", (IMemberDescriptor) new Bridge.TYPE_f368f307eac3440bbd1df12c504d2d1b.PROP_decb6c1f415d42fba7a0349eb54996ea());
      }

      private sealed class MTHD_bd0d572e68dd415f9af85222836635f7 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_bd0d572e68dd415f9af85222836635f7()
        {
          this.Initialize("get_uses", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerSpell) obj).uses;
        }
      }

      private sealed class MTHD_d644f10d0bee41359f6e58d792560880 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_d644f10d0bee41359f6e58d792560880()
        {
          this.Initialize("set_uses", false, new ParameterDescriptor[1]
          {
            new ParameterDescriptor("value", typeof (int))
          }, false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          ((ContainerSpell) obj).uses = (int) pars[0];
          return (object) null;
        }
      }

      private sealed class MTHD_1bb43e123b3549f9b94bb630510dd5e9 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_1bb43e123b3549f9b94bb630510dd5e9()
        {
          this.Initialize("get_maxUses", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerSpell) obj).maxUses;
        }
      }

      private sealed class MTHD_6d72abf1fbca47598cfdbbcee1ca6f2f : HardwiredMethodMemberDescriptor
      {
        internal MTHD_6d72abf1fbca47598cfdbbcee1ca6f2f()
        {
          this.Initialize("set_maxUses", false, new ParameterDescriptor[1]
          {
            new ParameterDescriptor("value", typeof (int))
          }, false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          ((ContainerSpell) obj).maxUses = (int) pars[0];
          return (object) null;
        }
      }

      private sealed class MTHD_dfb1ec52d8244c2abc0fe3208ebed77a : HardwiredMethodMemberDescriptor
      {
        internal MTHD_dfb1ec52d8244c2abc0fe3208ebed77a()
        {
          this.Initialize("get_rechargeTime", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerSpell) obj).rechargeTime;
        }
      }

      private sealed class MTHD_7b2b53250ddf43288502dcaf78729aa1 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_7b2b53250ddf43288502dcaf78729aa1()
        {
          this.Initialize("set_rechargeTime", false, new ParameterDescriptor[1]
          {
            new ParameterDescriptor("value", typeof (int))
          }, false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          ((ContainerSpell) obj).rechargeTime = (int) pars[0];
          return (object) null;
        }
      }

      private sealed class MTHD_e474445d26ea4115adc9ecd430ce93e3 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_e474445d26ea4115adc9ecd430ce93e3()
        {
          this.Initialize("get_lastTurnFired", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerSpell) obj).lastTurnFired;
        }
      }

      private sealed class MTHD_0150f6fe818a4dffa8399df146056042 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_0150f6fe818a4dffa8399df146056042()
        {
          this.Initialize("set_lastTurnFired", false, new ParameterDescriptor[1]
          {
            new ParameterDescriptor("value", typeof (int))
          }, false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          ((ContainerSpell) obj).lastTurnFired = (int) pars[0];
          return (object) null;
        }
      }

      private sealed class MTHD_e93d14c82ac54e9d9b4985b9ad1f78fc : HardwiredMethodMemberDescriptor
      {
        internal MTHD_e93d14c82ac54e9d9b4985b9ad1f78fc()
        {
          this.Initialize("get_isPresent", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerSpell) obj).isPresent;
        }
      }

      private sealed class MTHD_4bb258c9ec5f4b31824d7bbf3f7320ff : HardwiredMethodMemberDescriptor
      {
        internal MTHD_4bb258c9ec5f4b31824d7bbf3f7320ff()
        {
          this.Initialize("set_isPresent", false, new ParameterDescriptor[1]
          {
            new ParameterDescriptor("value", typeof (bool))
          }, false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          ((ContainerSpell) obj).isPresent = (bool) pars[0];
          return (object) null;
        }
      }

      private sealed class MTHD_f68621dff9174f8f8a13288124050362 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_f68621dff9174f8f8a13288124050362()
        {
          this.Initialize("get_locked", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerSpell) obj).locked;
        }
      }

      private sealed class MTHD_8ed4b7cfeea041a890bda2b94eccfc34 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_8ed4b7cfeea041a890bda2b94eccfc34()
        {
          this.Initialize("set_locked", false, new ParameterDescriptor[1]
          {
            new ParameterDescriptor("value", typeof (bool))
          }, false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          ((ContainerSpell) obj).locked = (bool) pars[0];
          return (object) null;
        }
      }

      private sealed class MTHD_e130068113ba4f97b3444dcc3bd7011f : HardwiredMethodMemberDescriptor
      {
        internal MTHD_e130068113ba4f97b3444dcc3bd7011f()
        {
          this.Initialize("get_turnsTillFirstUse", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerSpell) obj).turnsTillFirstUse;
        }
      }

      private sealed class MTHD_e68b329afe524e24ad81288b5672d194 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_e68b329afe524e24ad81288b5672d194()
        {
          this.Initialize("set_turnsTillFirstUse", false, new ParameterDescriptor[1]
          {
            new ParameterDescriptor("value", typeof (int))
          }, false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          ((ContainerSpell) obj).turnsTillFirstUse = (int) pars[0];
          return (object) null;
        }
      }

      private sealed class MTHD_3496481829bb4104b4fa4099932e2061 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_3496481829bb4104b4fa4099932e2061()
        {
          this.Initialize("get_spellEnum", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerSpell) obj).spellEnum;
        }
      }

      private sealed class MTHD_957f0b3dc105468fb5e9b81fb7180b00 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_957f0b3dc105468fb5e9b81fb7180b00()
        {
          this.Initialize("get_castType", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerSpell) obj).castType;
        }
      }

      private sealed class MTHD_042022f01cd14343bb9a4f3642fd8eb0 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_042022f01cd14343bb9a4f3642fd8eb0()
        {
          this.Initialize("get_damageType", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerSpell) obj).damageType;
        }
      }

      private sealed class MTHD_ec142f61561346f6bc166acce896b327 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_ec142f61561346f6bc166acce896b327()
        {
          this.Initialize("get_book", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerSpell) obj).book;
        }
      }

      private sealed class MTHD_ecfcef3d11204ea2b547565ba940c0dc : HardwiredMethodMemberDescriptor
      {
        internal MTHD_ecfcef3d11204ea2b547565ba940c0dc()
        {
          this.Initialize("get_damage", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerSpell) obj).damage;
        }
      }

      private sealed class MTHD_04d38732880a47f398ebc333558961eb : HardwiredMethodMemberDescriptor
      {
        internal MTHD_04d38732880a47f398ebc333558961eb()
        {
          this.Initialize("get_explosionRadius", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerSpell) obj).explosionRadius;
        }
      }

      private sealed class MTHD_a76d4a5003f0428180d58c3e5d35dafa : HardwiredMethodMemberDescriptor
      {
        internal MTHD_a76d4a5003f0428180d58c3e5d35dafa()
        {
          this.Initialize("get_description", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerSpell) obj).description;
        }
      }

      private sealed class MTHD_942120395ca1489caf4cccc2a3bc28b7 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_942120395ca1489caf4cccc2a3bc28b7()
        {
          this.Initialize("get_descriptionOnly", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerSpell) obj).descriptionOnly;
        }
      }

      private sealed class MTHD_ac47f470abc7496ca82c6906b080a250 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_ac47f470abc7496ca82c6906b080a250()
        {
          this.Initialize("get_descriptionExtra", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerSpell) obj).descriptionExtra;
        }
      }

      private sealed class MTHD_06b1653c54024ce9947a4885ff471982 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_06b1653c54024ce9947a4885ff471982()
        {
          this.Initialize("get_name", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerSpell) obj).name;
        }
      }

      private sealed class MTHD_7abe87f6a4b647a48f47711cf48cf2a9 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_7abe87f6a4b647a48f47711cf48cf2a9()
        {
          this.Initialize("Equals", false, new ParameterDescriptor[1]
          {
            new ParameterDescriptor("obj", typeof (object))
          }, false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) obj.Equals(pars[0]);
        }
      }

      private sealed class MTHD_93e804bba58b44ac8296b5b8ab4cc506 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_93e804bba58b44ac8296b5b8ab4cc506()
        {
          this.Initialize("GetHashCode", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) obj.GetHashCode();
        }
      }

      private sealed class MTHD_8b84b1130f11468a9e6e2fc104d83aac : HardwiredMethodMemberDescriptor
      {
        internal MTHD_8b84b1130f11468a9e6e2fc104d83aac()
        {
          this.Initialize("GetType", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) obj.GetType();
        }
      }

      private sealed class MTHD_5a1559bd6cee4b119134198d63e59996 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_5a1559bd6cee4b119134198d63e59996()
        {
          this.Initialize("ToString", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) obj.ToString();
        }
      }

      private sealed class PROP_61e8ed3555b647aabcf6c8810fa30196 : HardwiredMemberDescriptor
      {
        internal PROP_61e8ed3555b647aabcf6c8810fa30196()
          : base(typeof (int), "uses", false, MemberDescriptorAccess.CanRead | MemberDescriptorAccess.CanWrite)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((ContainerSpell) obj).uses;
        }

        protected override void SetValueImpl(Script script, object obj, object value)
        {
          ((ContainerSpell) obj).uses = (int) value;
        }
      }

      private sealed class PROP_cdbb347fe6674bbf9cdbb45665d6de87 : HardwiredMemberDescriptor
      {
        internal PROP_cdbb347fe6674bbf9cdbb45665d6de87()
          : base(typeof (int), "maxUses", false, MemberDescriptorAccess.CanRead | MemberDescriptorAccess.CanWrite)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((ContainerSpell) obj).maxUses;
        }

        protected override void SetValueImpl(Script script, object obj, object value)
        {
          ((ContainerSpell) obj).maxUses = (int) value;
        }
      }

      private sealed class PROP_7512a8b3d7d441958eb879c7c3cdad69 : HardwiredMemberDescriptor
      {
        internal PROP_7512a8b3d7d441958eb879c7c3cdad69()
          : base(typeof (int), "rechargeTime", false, MemberDescriptorAccess.CanRead | MemberDescriptorAccess.CanWrite)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((ContainerSpell) obj).rechargeTime;
        }

        protected override void SetValueImpl(Script script, object obj, object value)
        {
          ((ContainerSpell) obj).rechargeTime = (int) value;
        }
      }

      private sealed class PROP_633df974d92c4552b3657172f66c6b4c : HardwiredMemberDescriptor
      {
        internal PROP_633df974d92c4552b3657172f66c6b4c()
          : base(typeof (int), "lastTurnFired", false, MemberDescriptorAccess.CanRead | MemberDescriptorAccess.CanWrite)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((ContainerSpell) obj).lastTurnFired;
        }

        protected override void SetValueImpl(Script script, object obj, object value)
        {
          ((ContainerSpell) obj).lastTurnFired = (int) value;
        }
      }

      private sealed class PROP_47c817a2711443b68a692201f4b0b5ee : HardwiredMemberDescriptor
      {
        internal PROP_47c817a2711443b68a692201f4b0b5ee()
          : base(typeof (bool), "isPresent", false, MemberDescriptorAccess.CanRead | MemberDescriptorAccess.CanWrite)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((ContainerSpell) obj).isPresent;
        }

        protected override void SetValueImpl(Script script, object obj, object value)
        {
          ((ContainerSpell) obj).isPresent = (bool) value;
        }
      }

      private sealed class PROP_01c73b3d17db45a0bc3b39c50d9154c4 : HardwiredMemberDescriptor
      {
        internal PROP_01c73b3d17db45a0bc3b39c50d9154c4()
          : base(typeof (bool), "locked", false, MemberDescriptorAccess.CanRead | MemberDescriptorAccess.CanWrite)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((ContainerSpell) obj).locked;
        }

        protected override void SetValueImpl(Script script, object obj, object value)
        {
          ((ContainerSpell) obj).locked = (bool) value;
        }
      }

      private sealed class PROP_31dffdf86c2b4752b5813951c56f2578 : HardwiredMemberDescriptor
      {
        internal PROP_31dffdf86c2b4752b5813951c56f2578()
          : base(typeof (int), "turnsTillFirstUse", false, MemberDescriptorAccess.CanRead | MemberDescriptorAccess.CanWrite)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((ContainerSpell) obj).turnsTillFirstUse;
        }

        protected override void SetValueImpl(Script script, object obj, object value)
        {
          ((ContainerSpell) obj).turnsTillFirstUse = (int) value;
        }
      }

      private sealed class PROP_0e0ac94fdaca4e46aff371161caae336 : HardwiredMemberDescriptor
      {
        internal PROP_0e0ac94fdaca4e46aff371161caae336()
          : base(typeof (SpellEnum), "spellEnum", false, MemberDescriptorAccess.CanRead)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((ContainerSpell) obj).spellEnum;
        }
      }

      private sealed class PROP_5eebd292bbb54e01b8b844e1f6344f81 : HardwiredMemberDescriptor
      {
        internal PROP_5eebd292bbb54e01b8b844e1f6344f81()
          : base(typeof (CastType), "castType", false, MemberDescriptorAccess.CanRead)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((ContainerSpell) obj).castType;
        }
      }

      private sealed class PROP_ec7c04a11d83416ba27c297eba4fd0d6 : HardwiredMemberDescriptor
      {
        internal PROP_ec7c04a11d83416ba27c297eba4fd0d6()
          : base(typeof (DamageType), "damageType", false, MemberDescriptorAccess.CanRead)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((ContainerSpell) obj).damageType;
        }
      }

      private sealed class PROP_3ea08e149df74b33b50292c00b349405 : HardwiredMemberDescriptor
      {
        internal PROP_3ea08e149df74b33b50292c00b349405()
          : base(typeof (BookOf), "book", false, MemberDescriptorAccess.CanRead)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((ContainerSpell) obj).book;
        }
      }

      private sealed class PROP_cb3ea5507dfb40faa5e24a4956babf09 : HardwiredMemberDescriptor
      {
        internal PROP_cb3ea5507dfb40faa5e24a4956babf09()
          : base(typeof (int), "damage", false, MemberDescriptorAccess.CanRead)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((ContainerSpell) obj).damage;
        }
      }

      private sealed class PROP_e9989b9d32f941cab470f14097fa5e8e : HardwiredMemberDescriptor
      {
        internal PROP_e9989b9d32f941cab470f14097fa5e8e()
          : base(typeof (int), "explosionRadius", false, MemberDescriptorAccess.CanRead)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((ContainerSpell) obj).explosionRadius;
        }
      }

      private sealed class PROP_d252be305d494dc892122a7552c23114 : HardwiredMemberDescriptor
      {
        internal PROP_d252be305d494dc892122a7552c23114()
          : base(typeof (string), "description", false, MemberDescriptorAccess.CanRead)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((ContainerSpell) obj).description;
        }
      }

      private sealed class PROP_286cd2e4d7f54edf80728159b437e278 : HardwiredMemberDescriptor
      {
        internal PROP_286cd2e4d7f54edf80728159b437e278()
          : base(typeof (string), "descriptionOnly", false, MemberDescriptorAccess.CanRead)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((ContainerSpell) obj).descriptionOnly;
        }
      }

      private sealed class PROP_9c754d220c1f4524b0d9c4a142276251 : HardwiredMemberDescriptor
      {
        internal PROP_9c754d220c1f4524b0d9c4a142276251()
          : base(typeof (string), "descriptionExtra", false, MemberDescriptorAccess.CanRead)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((ContainerSpell) obj).descriptionExtra;
        }
      }

      private sealed class PROP_decb6c1f415d42fba7a0349eb54996ea : HardwiredMemberDescriptor
      {
        internal PROP_decb6c1f415d42fba7a0349eb54996ea()
          : base(typeof (string), "name", false, MemberDescriptorAccess.CanRead)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((ContainerSpell) obj).name;
        }
      }
    }

    private sealed class TYPE_ec2fdb8bd1194ee381640fff2dd3955f : HardwiredUserDataDescriptor
    {
      internal TYPE_ec2fdb8bd1194ee381640fff2dd3955f()
        : base(typeof (ContainerTower))
      {
        this.AddMember("get_health", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_health", typeof (ContainerTower), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_ec2fdb8bd1194ee381640fff2dd3955f.MTHD_ab9a378a9ecf4d4eb5a0dac78c130a78()
        }));
        this.AddMember("set_health", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("set_health", typeof (ContainerTower), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_ec2fdb8bd1194ee381640fff2dd3955f.MTHD_ff633744e1a7446cb9eb81e0eedcb574()
        }));
        this.AddMember("get_maxHealth", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_maxHealth", typeof (ContainerTower), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_ec2fdb8bd1194ee381640fff2dd3955f.MTHD_a8a7f781bfec4200b269f41a11e68db6()
        }));
        this.AddMember("set_maxHealth", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("set_maxHealth", typeof (ContainerTower), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_ec2fdb8bd1194ee381640fff2dd3955f.MTHD_b119397397d64253ae004de907a06fe8()
        }));
        this.AddMember("get_x", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_x", typeof (ContainerTower), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_ec2fdb8bd1194ee381640fff2dd3955f.MTHD_ee190b4004a945248f1681ba9b41f6f4()
        }));
        this.AddMember("set_x", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("set_x", typeof (ContainerTower), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_ec2fdb8bd1194ee381640fff2dd3955f.MTHD_df770d2631aa4034a2823e10a1913905()
        }));
        this.AddMember("get_y", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_y", typeof (ContainerTower), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_ec2fdb8bd1194ee381640fff2dd3955f.MTHD_624bbd0bfc4c42228538078038f3789d()
        }));
        this.AddMember("set_y", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("set_y", typeof (ContainerTower), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_ec2fdb8bd1194ee381640fff2dd3955f.MTHD_6ce0cba37f2547aa8a0596f5b0fe8ab3()
        }));
        this.AddMember("get_position", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_position", typeof (ContainerTower), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_ec2fdb8bd1194ee381640fff2dd3955f.MTHD_d4de563c98ae4fe1907cfb5d12167504()
        }));
        this.AddMember("set_position", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("set_position", typeof (ContainerTower), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_ec2fdb8bd1194ee381640fff2dd3955f.MTHD_fec546c8bb5746cc88e8df09e222cf36()
        }));
        this.AddMember("get_isDead", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_isDead", typeof (ContainerTower), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_ec2fdb8bd1194ee381640fff2dd3955f.MTHD_5a6834169f4b43dbb7c3036d4baf3d35()
        }));
        this.AddMember("getType", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("getType", typeof (ContainerTower), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_ec2fdb8bd1194ee381640fff2dd3955f.MTHD_a734a6e6096f471291ce94f6768f110e()
        }));
        this.AddMember("kill", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("kill", typeof (ContainerTower), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_ec2fdb8bd1194ee381640fff2dd3955f.MTHD_61a257468a444d2f9d558cd3ea6d8345()
        }));
        this.AddMember("GetHashCode", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("GetHashCode", typeof (ContainerTower), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_ec2fdb8bd1194ee381640fff2dd3955f.MTHD_08293f1103a94bdaa065a3f1df42d147()
        }));
        this.AddMember("Equals", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("Equals", typeof (ContainerTower), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_ec2fdb8bd1194ee381640fff2dd3955f.MTHD_e06201e5736449839475921d14db1df0()
        }));
        this.AddMember("GetType", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("GetType", typeof (ContainerTower), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_ec2fdb8bd1194ee381640fff2dd3955f.MTHD_db57409576e74c679d8a94b1c35d7423()
        }));
        this.AddMember("ToString", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("ToString", typeof (ContainerTower), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_ec2fdb8bd1194ee381640fff2dd3955f.MTHD_bb839231a389483f98d07cd82ac7e39d()
        }));
        this.AddMember("health", (IMemberDescriptor) new Bridge.TYPE_ec2fdb8bd1194ee381640fff2dd3955f.PROP_94cfd967e4f146c0b0bf3c85022502fa());
        this.AddMember("maxHealth", (IMemberDescriptor) new Bridge.TYPE_ec2fdb8bd1194ee381640fff2dd3955f.PROP_1dc4a52553994df5b424852fae8258a7());
        this.AddMember("x", (IMemberDescriptor) new Bridge.TYPE_ec2fdb8bd1194ee381640fff2dd3955f.PROP_c3c4c2dbaaac491fa7b79256b2c53902());
        this.AddMember("y", (IMemberDescriptor) new Bridge.TYPE_ec2fdb8bd1194ee381640fff2dd3955f.PROP_b0db739eed4d498b93dfd1450010318d());
        this.AddMember("position", (IMemberDescriptor) new Bridge.TYPE_ec2fdb8bd1194ee381640fff2dd3955f.PROP_53a51fc2fb48404c825fe17b971aab8b());
        this.AddMember("isDead", (IMemberDescriptor) new Bridge.TYPE_ec2fdb8bd1194ee381640fff2dd3955f.PROP_2ba2907775b14f99b819e4742315b680());
      }

      private sealed class MTHD_ab9a378a9ecf4d4eb5a0dac78c130a78 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_ab9a378a9ecf4d4eb5a0dac78c130a78()
        {
          this.Initialize("get_health", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerTower) obj).health;
        }
      }

      private sealed class MTHD_ff633744e1a7446cb9eb81e0eedcb574 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_ff633744e1a7446cb9eb81e0eedcb574()
        {
          this.Initialize("set_health", false, new ParameterDescriptor[1]
          {
            new ParameterDescriptor("value", typeof (int))
          }, false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          ((ContainerTower) obj).health = (int) pars[0];
          return (object) null;
        }
      }

      private sealed class MTHD_a8a7f781bfec4200b269f41a11e68db6 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_a8a7f781bfec4200b269f41a11e68db6()
        {
          this.Initialize("get_maxHealth", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerTower) obj).maxHealth;
        }
      }

      private sealed class MTHD_b119397397d64253ae004de907a06fe8 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_b119397397d64253ae004de907a06fe8()
        {
          this.Initialize("set_maxHealth", false, new ParameterDescriptor[1]
          {
            new ParameterDescriptor("value", typeof (int))
          }, false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          ((ContainerTower) obj).maxHealth = (int) pars[0];
          return (object) null;
        }
      }

      private sealed class MTHD_ee190b4004a945248f1681ba9b41f6f4 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_ee190b4004a945248f1681ba9b41f6f4()
        {
          this.Initialize("get_x", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerTower) obj).x;
        }
      }

      private sealed class MTHD_df770d2631aa4034a2823e10a1913905 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_df770d2631aa4034a2823e10a1913905()
        {
          this.Initialize("set_x", false, new ParameterDescriptor[1]
          {
            new ParameterDescriptor("value", typeof (int))
          }, false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          ((ContainerTower) obj).x = (int) pars[0];
          return (object) null;
        }
      }

      private sealed class MTHD_624bbd0bfc4c42228538078038f3789d : HardwiredMethodMemberDescriptor
      {
        internal MTHD_624bbd0bfc4c42228538078038f3789d()
        {
          this.Initialize("get_y", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerTower) obj).y;
        }
      }

      private sealed class MTHD_6ce0cba37f2547aa8a0596f5b0fe8ab3 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_6ce0cba37f2547aa8a0596f5b0fe8ab3()
        {
          this.Initialize("set_y", false, new ParameterDescriptor[1]
          {
            new ParameterDescriptor("value", typeof (int))
          }, false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          ((ContainerTower) obj).y = (int) pars[0];
          return (object) null;
        }
      }

      private sealed class MTHD_d4de563c98ae4fe1907cfb5d12167504 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_d4de563c98ae4fe1907cfb5d12167504()
        {
          this.Initialize("get_position", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerTower) obj).position;
        }
      }

      private sealed class MTHD_fec546c8bb5746cc88e8df09e222cf36 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_fec546c8bb5746cc88e8df09e222cf36()
        {
          this.Initialize("set_position", false, new ParameterDescriptor[1]
          {
            new ParameterDescriptor("value", typeof (Educative.Point))
          }, false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          ((ContainerTower) obj).position = (Educative.Point) pars[0];
          return (object) null;
        }
      }

      private sealed class MTHD_5a6834169f4b43dbb7c3036d4baf3d35 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_5a6834169f4b43dbb7c3036d4baf3d35()
        {
          this.Initialize("get_isDead", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerTower) obj).isDead;
        }
      }

      private sealed class MTHD_a734a6e6096f471291ce94f6768f110e : HardwiredMethodMemberDescriptor
      {
        internal MTHD_a734a6e6096f471291ce94f6768f110e()
        {
          this.Initialize("getType", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerTower) obj).getType();
        }
      }

      private sealed class MTHD_61a257468a444d2f9d558cd3ea6d8345 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_61a257468a444d2f9d558cd3ea6d8345()
        {
          this.Initialize("kill", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          ((ContainerTower) obj).kill();
          return (object) DynValue.Void;
        }
      }

      private sealed class MTHD_08293f1103a94bdaa065a3f1df42d147 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_08293f1103a94bdaa065a3f1df42d147()
        {
          this.Initialize("GetHashCode", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerTower) obj).GetHashCode();
        }
      }

      private sealed class MTHD_e06201e5736449839475921d14db1df0 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_e06201e5736449839475921d14db1df0()
        {
          this.Initialize("Equals", false, new ParameterDescriptor[1]
          {
            new ParameterDescriptor("obj", typeof (object))
          }, false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) obj.Equals(pars[0]);
        }
      }

      private sealed class MTHD_db57409576e74c679d8a94b1c35d7423 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_db57409576e74c679d8a94b1c35d7423()
        {
          this.Initialize("GetType", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) obj.GetType();
        }
      }

      private sealed class MTHD_bb839231a389483f98d07cd82ac7e39d : HardwiredMethodMemberDescriptor
      {
        internal MTHD_bb839231a389483f98d07cd82ac7e39d()
        {
          this.Initialize("ToString", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) obj.ToString();
        }
      }

      private sealed class PROP_94cfd967e4f146c0b0bf3c85022502fa : HardwiredMemberDescriptor
      {
        internal PROP_94cfd967e4f146c0b0bf3c85022502fa()
          : base(typeof (int), "health", false, MemberDescriptorAccess.CanRead | MemberDescriptorAccess.CanWrite)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((ContainerTower) obj).health;
        }

        protected override void SetValueImpl(Script script, object obj, object value)
        {
          ((ContainerTower) obj).health = (int) value;
        }
      }

      private sealed class PROP_1dc4a52553994df5b424852fae8258a7 : HardwiredMemberDescriptor
      {
        internal PROP_1dc4a52553994df5b424852fae8258a7()
          : base(typeof (int), "maxHealth", false, MemberDescriptorAccess.CanRead | MemberDescriptorAccess.CanWrite)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((ContainerTower) obj).maxHealth;
        }

        protected override void SetValueImpl(Script script, object obj, object value)
        {
          ((ContainerTower) obj).maxHealth = (int) value;
        }
      }

      private sealed class PROP_c3c4c2dbaaac491fa7b79256b2c53902 : HardwiredMemberDescriptor
      {
        internal PROP_c3c4c2dbaaac491fa7b79256b2c53902()
          : base(typeof (int), "x", false, MemberDescriptorAccess.CanRead | MemberDescriptorAccess.CanWrite)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((ContainerTower) obj).x;
        }

        protected override void SetValueImpl(Script script, object obj, object value)
        {
          ((ContainerTower) obj).x = (int) value;
        }
      }

      private sealed class PROP_b0db739eed4d498b93dfd1450010318d : HardwiredMemberDescriptor
      {
        internal PROP_b0db739eed4d498b93dfd1450010318d()
          : base(typeof (int), "y", false, MemberDescriptorAccess.CanRead | MemberDescriptorAccess.CanWrite)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((ContainerTower) obj).y;
        }

        protected override void SetValueImpl(Script script, object obj, object value)
        {
          ((ContainerTower) obj).y = (int) value;
        }
      }

      private sealed class PROP_53a51fc2fb48404c825fe17b971aab8b : HardwiredMemberDescriptor
      {
        internal PROP_53a51fc2fb48404c825fe17b971aab8b()
          : base(typeof (Educative.Point), "position", false, MemberDescriptorAccess.CanRead | MemberDescriptorAccess.CanWrite)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((ContainerTower) obj).position;
        }

        protected override void SetValueImpl(Script script, object obj, object value)
        {
          ((ContainerTower) obj).position = (Educative.Point) value;
        }
      }

      private sealed class PROP_2ba2907775b14f99b819e4742315b680 : HardwiredMemberDescriptor
      {
        internal PROP_2ba2907775b14f99b819e4742315b680()
          : base(typeof (bool), "isDead", false, MemberDescriptorAccess.CanRead)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((ContainerTower) obj).isDead;
        }
      }
    }

    private sealed class TYPE_07c986f7088d435a9164948b8dddc6e3 : HardwiredUserDataDescriptor
    {
      internal TYPE_07c986f7088d435a9164948b8dddc6e3()
        : base(typeof (Educative.Point))
      {
        this.AddMember("__new", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("__new", typeof (Educative.Point), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[2]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_07c986f7088d435a9164948b8dddc6e3.MTHD_68a8da92062147e8b0b1f5211fb25cfe(),
          (IOverloadableMemberDescriptor) new Bridge.TYPE_07c986f7088d435a9164948b8dddc6e3.MTHD_47a51e55bd8e4249b4ab0068f80c228a()
        }));
        this.AddMember("distance", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("distance", typeof (Educative.Point), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[2]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_07c986f7088d435a9164948b8dddc6e3.MTHD_4238f5c9c3c342c8ae2a448a460c6ca3(),
          (IOverloadableMemberDescriptor) new Bridge.TYPE_07c986f7088d435a9164948b8dddc6e3.MTHD_f272e080733045b3b5bc3220289a024f()
        }));
        this.AddMember("construct", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("construct", typeof (Educative.Point), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_07c986f7088d435a9164948b8dddc6e3.MTHD_7b29c03b71d441c4aefc3de8d609af45()
        }));
        this.AddMember("copy", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("copy", typeof (Educative.Point), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_07c986f7088d435a9164948b8dddc6e3.MTHD_556012b8f6f644edbd062a760b355969()
        }));
        this.AddMember("normalized", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("normalized", typeof (Educative.Point), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_07c986f7088d435a9164948b8dddc6e3.MTHD_8546b6e69cbc4638b549f0e9c1b1f31f()
        }));
        this.AddMember("normalize", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("normalize", typeof (Educative.Point), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_07c986f7088d435a9164948b8dddc6e3.MTHD_f5fc5cfb881a497ea8d35a4833ac1044()
        }));
        this.AddMember("Equals", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("Equals", typeof (Educative.Point), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_07c986f7088d435a9164948b8dddc6e3.MTHD_6efe2ecd9ee146879c186a9db28c7708()
        }));
        this.AddMember("GetHashCode", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("GetHashCode", typeof (Educative.Point), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_07c986f7088d435a9164948b8dddc6e3.MTHD_991c0483a0f346fca8f9404c1c16c02a()
        }));
        this.AddMember("op_Equality", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("op_Equality", typeof (Educative.Point), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_07c986f7088d435a9164948b8dddc6e3.MTHD_fd57de0018dd4c5ba937b024df077f91()
        }));
        this.AddMember("op_Subtraction", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("op_Subtraction", typeof (Educative.Point), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[2]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_07c986f7088d435a9164948b8dddc6e3.MTHD_d1ad41e3686841b588d3e04d81e7e5fd(),
          (IOverloadableMemberDescriptor) new Bridge.TYPE_07c986f7088d435a9164948b8dddc6e3.MTHD_e85563195a9b4c3ab4b9c6f039a8ebf9()
        }));
        this.AddMember("op_Addition", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("op_Addition", typeof (Educative.Point), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[2]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_07c986f7088d435a9164948b8dddc6e3.MTHD_0a509b8395a54b428469ab5590e01fba(),
          (IOverloadableMemberDescriptor) new Bridge.TYPE_07c986f7088d435a9164948b8dddc6e3.MTHD_615b1e3653eb4a869600c9ac3b5f53a8()
        }));
        this.AddMember("op_Multiply", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("op_Multiply", typeof (Educative.Point), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[2]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_07c986f7088d435a9164948b8dddc6e3.MTHD_3fbb63362162482794847a101185c9a5(),
          (IOverloadableMemberDescriptor) new Bridge.TYPE_07c986f7088d435a9164948b8dddc6e3.MTHD_e94d5dcc020049bf82e3e21f4bdd98b5()
        }));
        this.AddMember("op_Division", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("op_Division", typeof (Educative.Point), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[2]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_07c986f7088d435a9164948b8dddc6e3.MTHD_1d13a3f8e8a74ea9968334a4e8c998e5(),
          (IOverloadableMemberDescriptor) new Bridge.TYPE_07c986f7088d435a9164948b8dddc6e3.MTHD_d7f00d9a10cd4aa4b2e9726b54638850()
        }));
        this.AddMember("op_Modulus", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("op_Modulus", typeof (Educative.Point), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[2]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_07c986f7088d435a9164948b8dddc6e3.MTHD_7a6d1f9117c34c7f9f3fa36c84716b53(),
          (IOverloadableMemberDescriptor) new Bridge.TYPE_07c986f7088d435a9164948b8dddc6e3.MTHD_e056c59bb33543b18ddc4380750bf8e2()
        }));
        this.AddMember("op_Inequality", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("op_Inequality", typeof (Educative.Point), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_07c986f7088d435a9164948b8dddc6e3.MTHD_d6ed3be9bf7549129c87f9b4692d81c0()
        }));
        this.AddMember("GetType", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("GetType", typeof (Educative.Point), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_07c986f7088d435a9164948b8dddc6e3.MTHD_111398fbc4ca4262a6a3212137a1db88()
        }));
        this.AddMember("x", (IMemberDescriptor) new Bridge.TYPE_07c986f7088d435a9164948b8dddc6e3.FLDV_a366d4e87076412392b39daf29fa7da5());
        this.AddMember("y", (IMemberDescriptor) new Bridge.TYPE_07c986f7088d435a9164948b8dddc6e3.FLDV_f4c285915836401eae1389d2228903f7());
      }

      private sealed class MTHD_68a8da92062147e8b0b1f5211fb25cfe : HardwiredMethodMemberDescriptor
      {
        internal MTHD_68a8da92062147e8b0b1f5211fb25cfe()
        {
          this.Initialize(".ctor", true, new ParameterDescriptor[2]
          {
            new ParameterDescriptor("x", typeof (double), true, (object) new DefaultValue()),
            new ParameterDescriptor("y", typeof (double), true, (object) new DefaultValue())
          }, false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          if (argscount <= 0)
            return (object) new Educative.Point();
          return argscount <= 1 ? (object) new Educative.Point((double) pars[0]) : (object) new Educative.Point((double) pars[0], (double) pars[1]);
        }
      }

      private sealed class MTHD_47a51e55bd8e4249b4ab0068f80c228a : HardwiredMethodMemberDescriptor
      {
        internal MTHD_47a51e55bd8e4249b4ab0068f80c228a()
        {
          this.Initialize(".ctor", true, new ParameterDescriptor[1]
          {
            new ParameterDescriptor("v", typeof (Vector2))
          }, false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) new Educative.Point((Vector2) pars[0]);
        }
      }

      private sealed class MTHD_4238f5c9c3c342c8ae2a448a460c6ca3 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_4238f5c9c3c342c8ae2a448a460c6ca3()
        {
          this.Initialize("distance", true, new ParameterDescriptor[2]
          {
            new ParameterDescriptor("a", typeof (Educative.Point)),
            new ParameterDescriptor("b", typeof (Educative.Point))
          }, false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) Educative.Point.distance((Educative.Point) pars[0], (Educative.Point) pars[1]);
        }
      }

      private sealed class MTHD_f272e080733045b3b5bc3220289a024f : HardwiredMethodMemberDescriptor
      {
        internal MTHD_f272e080733045b3b5bc3220289a024f()
        {
          this.Initialize("distance", false, new ParameterDescriptor[1]
          {
            new ParameterDescriptor("b", typeof (Educative.Point))
          }, false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((Educative.Point) obj).distance((Educative.Point) pars[0]);
        }
      }

      private sealed class MTHD_7b29c03b71d441c4aefc3de8d609af45 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_7b29c03b71d441c4aefc3de8d609af45()
        {
          this.Initialize("construct", true, new ParameterDescriptor[2]
          {
            new ParameterDescriptor("x", typeof (int)),
            new ParameterDescriptor("y", typeof (int))
          }, false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) Educative.Point.construct((int) pars[0], (int) pars[1]);
        }
      }

      private sealed class MTHD_556012b8f6f644edbd062a760b355969 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_556012b8f6f644edbd062a760b355969()
        {
          this.Initialize("copy", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((Educative.Point) obj).copy();
        }
      }

      private sealed class MTHD_8546b6e69cbc4638b549f0e9c1b1f31f : HardwiredMethodMemberDescriptor
      {
        internal MTHD_8546b6e69cbc4638b549f0e9c1b1f31f()
        {
          this.Initialize("normalized", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((Educative.Point) obj).normalized();
        }
      }

      private sealed class MTHD_f5fc5cfb881a497ea8d35a4833ac1044 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_f5fc5cfb881a497ea8d35a4833ac1044()
        {
          this.Initialize("normalize", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          ((Educative.Point) obj).normalize();
          return (object) DynValue.Void;
        }
      }

      private sealed class MTHD_6efe2ecd9ee146879c186a9db28c7708 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_6efe2ecd9ee146879c186a9db28c7708()
        {
          this.Initialize("Equals", false, new ParameterDescriptor[1]
          {
            new ParameterDescriptor("obj", typeof (object))
          }, false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((Educative.Point) obj).Equals(pars[0]);
        }
      }

      private sealed class MTHD_991c0483a0f346fca8f9404c1c16c02a : HardwiredMethodMemberDescriptor
      {
        internal MTHD_991c0483a0f346fca8f9404c1c16c02a()
        {
          this.Initialize("GetHashCode", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((Educative.Point) obj).GetHashCode();
        }
      }

      private sealed class MTHD_fd57de0018dd4c5ba937b024df077f91 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_fd57de0018dd4c5ba937b024df077f91()
        {
          this.Initialize("op_Equality", true, new ParameterDescriptor[2]
          {
            new ParameterDescriptor("v", typeof (Educative.Point)),
            new ParameterDescriptor("x", typeof (Educative.Point))
          }, false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((Educative.Point) pars[0] == (Educative.Point) pars[1]);
        }
      }

      private sealed class MTHD_d1ad41e3686841b588d3e04d81e7e5fd : HardwiredMethodMemberDescriptor
      {
        internal MTHD_d1ad41e3686841b588d3e04d81e7e5fd()
        {
          this.Initialize("op_Subtraction", true, new ParameterDescriptor[2]
          {
            new ParameterDescriptor("x", typeof (Educative.Point)),
            new ParameterDescriptor("v", typeof (double))
          }, false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((Educative.Point) pars[0] - (double) pars[1]);
        }
      }

      private sealed class MTHD_e85563195a9b4c3ab4b9c6f039a8ebf9 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_e85563195a9b4c3ab4b9c6f039a8ebf9()
        {
          this.Initialize("op_Subtraction", true, new ParameterDescriptor[2]
          {
            new ParameterDescriptor("x", typeof (Educative.Point)),
            new ParameterDescriptor("v", typeof (Educative.Point))
          }, false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((Educative.Point) pars[0] - (Educative.Point) pars[1]);
        }
      }

      private sealed class MTHD_0a509b8395a54b428469ab5590e01fba : HardwiredMethodMemberDescriptor
      {
        internal MTHD_0a509b8395a54b428469ab5590e01fba()
        {
          this.Initialize("op_Addition", true, new ParameterDescriptor[2]
          {
            new ParameterDescriptor("x", typeof (Educative.Point)),
            new ParameterDescriptor("v", typeof (double))
          }, false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((Educative.Point) pars[0] + (double) pars[1]);
        }
      }

      private sealed class MTHD_615b1e3653eb4a869600c9ac3b5f53a8 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_615b1e3653eb4a869600c9ac3b5f53a8()
        {
          this.Initialize("op_Addition", true, new ParameterDescriptor[2]
          {
            new ParameterDescriptor("x", typeof (Educative.Point)),
            new ParameterDescriptor("v", typeof (Educative.Point))
          }, false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((Educative.Point) pars[0] + (Educative.Point) pars[1]);
        }
      }

      private sealed class MTHD_3fbb63362162482794847a101185c9a5 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_3fbb63362162482794847a101185c9a5()
        {
          this.Initialize("op_Multiply", true, new ParameterDescriptor[2]
          {
            new ParameterDescriptor("x", typeof (Educative.Point)),
            new ParameterDescriptor("v", typeof (double))
          }, false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((Educative.Point) pars[0] * (double) pars[1]);
        }
      }

      private sealed class MTHD_e94d5dcc020049bf82e3e21f4bdd98b5 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_e94d5dcc020049bf82e3e21f4bdd98b5()
        {
          this.Initialize("op_Multiply", true, new ParameterDescriptor[2]
          {
            new ParameterDescriptor("x", typeof (Educative.Point)),
            new ParameterDescriptor("v", typeof (Educative.Point))
          }, false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((Educative.Point) pars[0] * (Educative.Point) pars[1]);
        }
      }

      private sealed class MTHD_1d13a3f8e8a74ea9968334a4e8c998e5 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_1d13a3f8e8a74ea9968334a4e8c998e5()
        {
          this.Initialize("op_Division", true, new ParameterDescriptor[2]
          {
            new ParameterDescriptor("x", typeof (Educative.Point)),
            new ParameterDescriptor("v", typeof (double))
          }, false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((Educative.Point) pars[0] / (double) pars[1]);
        }
      }

      private sealed class MTHD_d7f00d9a10cd4aa4b2e9726b54638850 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_d7f00d9a10cd4aa4b2e9726b54638850()
        {
          this.Initialize("op_Division", true, new ParameterDescriptor[2]
          {
            new ParameterDescriptor("x", typeof (Educative.Point)),
            new ParameterDescriptor("v", typeof (Educative.Point))
          }, false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((Educative.Point) pars[0] / (Educative.Point) pars[1]);
        }
      }

      private sealed class MTHD_7a6d1f9117c34c7f9f3fa36c84716b53 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_7a6d1f9117c34c7f9f3fa36c84716b53()
        {
          this.Initialize("op_Modulus", true, new ParameterDescriptor[2]
          {
            new ParameterDescriptor("x", typeof (Educative.Point)),
            new ParameterDescriptor("v", typeof (double))
          }, false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((Educative.Point) pars[0] % (double) pars[1]);
        }
      }

      private sealed class MTHD_e056c59bb33543b18ddc4380750bf8e2 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_e056c59bb33543b18ddc4380750bf8e2()
        {
          this.Initialize("op_Modulus", true, new ParameterDescriptor[2]
          {
            new ParameterDescriptor("x", typeof (Educative.Point)),
            new ParameterDescriptor("v", typeof (Educative.Point))
          }, false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((Educative.Point) pars[0] % (Educative.Point) pars[1]);
        }
      }

      private sealed class MTHD_d6ed3be9bf7549129c87f9b4692d81c0 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_d6ed3be9bf7549129c87f9b4692d81c0()
        {
          this.Initialize("op_Inequality", true, new ParameterDescriptor[2]
          {
            new ParameterDescriptor("v", typeof (Educative.Point)),
            new ParameterDescriptor("x", typeof (Educative.Point))
          }, false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((Educative.Point) pars[0] != (Educative.Point) pars[1]);
        }
      }

      private sealed class MTHD_111398fbc4ca4262a6a3212137a1db88 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_111398fbc4ca4262a6a3212137a1db88()
        {
          this.Initialize("GetType", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) obj.GetType();
        }
      }

      private sealed class FLDV_a366d4e87076412392b39daf29fa7da5 : HardwiredMemberDescriptor
      {
        internal FLDV_a366d4e87076412392b39daf29fa7da5()
          : base(typeof (double), "x", false, MemberDescriptorAccess.CanRead | MemberDescriptorAccess.CanWrite)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((Educative.Point) obj).x;
        }

        protected override void SetValueImpl(Script script, object obj, object value)
        {
          ((Educative.Point) obj).x = (double) value;
        }
      }

      private sealed class FLDV_f4c285915836401eae1389d2228903f7 : HardwiredMemberDescriptor
      {
        internal FLDV_f4c285915836401eae1389d2228903f7()
          : base(typeof (double), "y", false, MemberDescriptorAccess.CanRead | MemberDescriptorAccess.CanWrite)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((Educative.Point) obj).y;
        }

        protected override void SetValueImpl(Script script, object obj, object value)
        {
          ((Educative.Point) obj).y = (double) value;
        }
      }
    }

    private sealed class TYPE_b7424a4f59e64e839ea2847c2a69408a : HardwiredUserDataDescriptor
    {
      internal TYPE_b7424a4f59e64e839ea2847c2a69408a()
        : base(typeof (Summon))
      {
        this.AddMember("__new", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("__new", typeof (Summon), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_b7424a4f59e64e839ea2847c2a69408a.MTHD_b01b49babd654b02b43ef42793ea4cb6()
        }));
        this.AddMember("construct", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("construct", typeof (Summon), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_b7424a4f59e64e839ea2847c2a69408a.MTHD_dd3153ea60e54280a08fd8055cbf964c()
        }));
        this.AddMember("Equals", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("Equals", typeof (Summon), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_b7424a4f59e64e839ea2847c2a69408a.MTHD_b420e00cab6f49649d9c0dfe1cf146c4()
        }));
        this.AddMember("GetHashCode", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("GetHashCode", typeof (Summon), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_b7424a4f59e64e839ea2847c2a69408a.MTHD_adc353ecd45b492197d3f6918c8ab740()
        }));
        this.AddMember("GetType", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("GetType", typeof (Summon), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_b7424a4f59e64e839ea2847c2a69408a.MTHD_d67e3cb091564e4d9c413fe3bae199b6()
        }));
        this.AddMember("ToString", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("ToString", typeof (Summon), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_b7424a4f59e64e839ea2847c2a69408a.MTHD_f429cdc47af84b77b779cc77e8040b99()
        }));
        this.AddMember("spell", (IMemberDescriptor) new Bridge.TYPE_b7424a4f59e64e839ea2847c2a69408a.FLDV_8246dc18e5334a5eb2215a02e4010a7d());
        this.AddMember("position", (IMemberDescriptor) new Bridge.TYPE_b7424a4f59e64e839ea2847c2a69408a.FLDV_fe9362f7dd6c4f759a0de4460e36642e());
        this.AddMember("team", (IMemberDescriptor) new Bridge.TYPE_b7424a4f59e64e839ea2847c2a69408a.FLDV_59e351cc39f441a48b92198ed867c8fa());
        this.AddMember("onPlayersPanel", (IMemberDescriptor) new Bridge.TYPE_b7424a4f59e64e839ea2847c2a69408a.FLDV_9e65c26b4aca4d2fb1d26cf7798325f1());
        this.AddMember("useAI", (IMemberDescriptor) new Bridge.TYPE_b7424a4f59e64e839ea2847c2a69408a.FLDV_fe616fe764f245a9a23d135763ff7c54());
        this.AddMember("no_AI_still_use_turn", (IMemberDescriptor) new Bridge.TYPE_b7424a4f59e64e839ea2847c2a69408a.FLDV_9163ab3626a5404899854b0ce40d6265());
        this.AddMember("name", (IMemberDescriptor) new Bridge.TYPE_b7424a4f59e64e839ea2847c2a69408a.FLDV_d8cfe60cbf3c4603bcd7b05533399e87());
        this.AddMember("playSound", (IMemberDescriptor) new Bridge.TYPE_b7424a4f59e64e839ea2847c2a69408a.FLDV_fdad5df80ff04dee8a703dbe48e7c21c());
        this.AddMember("colors", (IMemberDescriptor) new Bridge.TYPE_b7424a4f59e64e839ea2847c2a69408a.FLDV_9a71a9e2930c46aea5108771a8ac9e61());
        this.AddMember("spells", (IMemberDescriptor) new Bridge.TYPE_b7424a4f59e64e839ea2847c2a69408a.FLDV_62136e0de3064d6fbd659b7bf8ddbb00());
        this.AddMember("outfit", (IMemberDescriptor) new Bridge.TYPE_b7424a4f59e64e839ea2847c2a69408a.FLDV_94dc22d310054f01901cd4594113219e());
        this.AddMember("elemental", (IMemberDescriptor) new Bridge.TYPE_b7424a4f59e64e839ea2847c2a69408a.FLDV_e877b4b593bc4e37a2c9065f6f163c04());
      }

      private sealed class MTHD_b01b49babd654b02b43ef42793ea4cb6 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_b01b49babd654b02b43ef42793ea4cb6()
        {
          this.Initialize(".ctor", true, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) new Summon();
        }
      }

      private sealed class MTHD_dd3153ea60e54280a08fd8055cbf964c : HardwiredMethodMemberDescriptor
      {
        internal MTHD_dd3153ea60e54280a08fd8055cbf964c()
        {
          this.Initialize("construct", true, new ParameterDescriptor[10]
          {
            new ParameterDescriptor("spell", typeof (object), true, (object) new DefaultValue()),
            new ParameterDescriptor("point", typeof (Educative.Point), true, (object) new DefaultValue()),
            new ParameterDescriptor("team", typeof (int), true, (object) new DefaultValue()),
            new ParameterDescriptor("colors", typeof (Table), true, (object) new DefaultValue()),
            new ParameterDescriptor("playSound", typeof (bool), true, (object) new DefaultValue()),
            new ParameterDescriptor("onPlayersPanel", typeof (bool), true, (object) new DefaultValue()),
            new ParameterDescriptor("name", typeof (string), true, (object) new DefaultValue()),
            new ParameterDescriptor("outfit", typeof (Table), true, (object) new DefaultValue()),
            new ParameterDescriptor("spells", typeof (Table), true, (object) new DefaultValue()),
            new ParameterDescriptor("elemental", typeof (BookOf), true, (object) new DefaultValue())
          }, false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          if (argscount <= 0)
            return (object) Summon.construct();
          if (argscount <= 1)
            return (object) Summon.construct(pars[0]);
          if (argscount <= 2)
            return (object) Summon.construct(pars[0], (Educative.Point) pars[1]);
          if (argscount <= 3)
            return (object) Summon.construct(pars[0], (Educative.Point) pars[1], (int) pars[2]);
          if (argscount <= 4)
            return (object) Summon.construct(pars[0], (Educative.Point) pars[1], (int) pars[2], (Table) pars[3]);
          if (argscount <= 5)
            return (object) Summon.construct(pars[0], (Educative.Point) pars[1], (int) pars[2], (Table) pars[3], (bool) pars[4]);
          if (argscount <= 6)
            return (object) Summon.construct(pars[0], (Educative.Point) pars[1], (int) pars[2], (Table) pars[3], (bool) pars[4], (bool) pars[5]);
          if (argscount <= 7)
            return (object) Summon.construct(pars[0], (Educative.Point) pars[1], (int) pars[2], (Table) pars[3], (bool) pars[4], (bool) pars[5], (string) pars[6]);
          if (argscount <= 8)
            return (object) Summon.construct(pars[0], (Educative.Point) pars[1], (int) pars[2], (Table) pars[3], (bool) pars[4], (bool) pars[5], (string) pars[6], (Table) pars[7]);
          return argscount <= 9 ? (object) Summon.construct(pars[0], (Educative.Point) pars[1], (int) pars[2], (Table) pars[3], (bool) pars[4], (bool) pars[5], (string) pars[6], (Table) pars[7], (Table) pars[8]) : (object) Summon.construct(pars[0], (Educative.Point) pars[1], (int) pars[2], (Table) pars[3], (bool) pars[4], (bool) pars[5], (string) pars[6], (Table) pars[7], (Table) pars[8], (BookOf) pars[9]);
        }
      }

      private sealed class MTHD_b420e00cab6f49649d9c0dfe1cf146c4 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_b420e00cab6f49649d9c0dfe1cf146c4()
        {
          this.Initialize("Equals", false, new ParameterDescriptor[1]
          {
            new ParameterDescriptor("obj", typeof (object))
          }, false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) obj.Equals(pars[0]);
        }
      }

      private sealed class MTHD_adc353ecd45b492197d3f6918c8ab740 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_adc353ecd45b492197d3f6918c8ab740()
        {
          this.Initialize("GetHashCode", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) obj.GetHashCode();
        }
      }

      private sealed class MTHD_d67e3cb091564e4d9c413fe3bae199b6 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_d67e3cb091564e4d9c413fe3bae199b6()
        {
          this.Initialize("GetType", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) obj.GetType();
        }
      }

      private sealed class MTHD_f429cdc47af84b77b779cc77e8040b99 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_f429cdc47af84b77b779cc77e8040b99()
        {
          this.Initialize("ToString", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) obj.ToString();
        }
      }

      private sealed class FLDV_8246dc18e5334a5eb2215a02e4010a7d : HardwiredMemberDescriptor
      {
        internal FLDV_8246dc18e5334a5eb2215a02e4010a7d()
          : base(typeof (object), "spell", false, MemberDescriptorAccess.CanRead | MemberDescriptorAccess.CanWrite)
        {
        }

        protected override object GetValueImpl(Script script, object obj) => ((Summon) obj).spell;

        protected override void SetValueImpl(Script script, object obj, object value)
        {
          ((Summon) obj).spell = value;
        }
      }

      private sealed class FLDV_fe9362f7dd6c4f759a0de4460e36642e : HardwiredMemberDescriptor
      {
        internal FLDV_fe9362f7dd6c4f759a0de4460e36642e()
          : base(typeof (Educative.Point), "position", false, MemberDescriptorAccess.CanRead | MemberDescriptorAccess.CanWrite)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((Summon) obj).position;
        }

        protected override void SetValueImpl(Script script, object obj, object value)
        {
          ((Summon) obj).position = (Educative.Point) value;
        }
      }

      private sealed class FLDV_59e351cc39f441a48b92198ed867c8fa : HardwiredMemberDescriptor
      {
        internal FLDV_59e351cc39f441a48b92198ed867c8fa()
          : base(typeof (int), "team", false, MemberDescriptorAccess.CanRead | MemberDescriptorAccess.CanWrite)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((Summon) obj).team;
        }

        protected override void SetValueImpl(Script script, object obj, object value)
        {
          ((Summon) obj).team = (int) value;
        }
      }

      private sealed class FLDV_9e65c26b4aca4d2fb1d26cf7798325f1 : HardwiredMemberDescriptor
      {
        internal FLDV_9e65c26b4aca4d2fb1d26cf7798325f1()
          : base(typeof (bool), "onPlayersPanel", false, MemberDescriptorAccess.CanRead | MemberDescriptorAccess.CanWrite)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((Summon) obj).onPlayersPanel;
        }

        protected override void SetValueImpl(Script script, object obj, object value)
        {
          ((Summon) obj).onPlayersPanel = (bool) value;
        }
      }

      private sealed class FLDV_fe616fe764f245a9a23d135763ff7c54 : HardwiredMemberDescriptor
      {
        internal FLDV_fe616fe764f245a9a23d135763ff7c54()
          : base(typeof (bool), "useAI", false, MemberDescriptorAccess.CanRead | MemberDescriptorAccess.CanWrite)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((Summon) obj).useAI;
        }

        protected override void SetValueImpl(Script script, object obj, object value)
        {
          ((Summon) obj).useAI = (bool) value;
        }
      }

      private sealed class FLDV_9163ab3626a5404899854b0ce40d6265 : HardwiredMemberDescriptor
      {
        internal FLDV_9163ab3626a5404899854b0ce40d6265()
          : base(typeof (bool), "no_AI_still_use_turn", false, MemberDescriptorAccess.CanRead | MemberDescriptorAccess.CanWrite)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((Summon) obj).no_AI_still_use_turn;
        }

        protected override void SetValueImpl(Script script, object obj, object value)
        {
          ((Summon) obj).no_AI_still_use_turn = (bool) value;
        }
      }

      private sealed class FLDV_d8cfe60cbf3c4603bcd7b05533399e87 : HardwiredMemberDescriptor
      {
        internal FLDV_d8cfe60cbf3c4603bcd7b05533399e87()
          : base(typeof (string), "name", false, MemberDescriptorAccess.CanRead | MemberDescriptorAccess.CanWrite)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((Summon) obj).name;
        }

        protected override void SetValueImpl(Script script, object obj, object value)
        {
          ((Summon) obj).name = (string) value;
        }
      }

      private sealed class FLDV_fdad5df80ff04dee8a703dbe48e7c21c : HardwiredMemberDescriptor
      {
        internal FLDV_fdad5df80ff04dee8a703dbe48e7c21c()
          : base(typeof (bool), "playSound", false, MemberDescriptorAccess.CanRead | MemberDescriptorAccess.CanWrite)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((Summon) obj).playSound;
        }

        protected override void SetValueImpl(Script script, object obj, object value)
        {
          ((Summon) obj).playSound = (bool) value;
        }
      }

      private sealed class FLDV_9a71a9e2930c46aea5108771a8ac9e61 : HardwiredMemberDescriptor
      {
        internal FLDV_9a71a9e2930c46aea5108771a8ac9e61()
          : base(typeof (Table), "colors", false, MemberDescriptorAccess.CanRead | MemberDescriptorAccess.CanWrite)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((Summon) obj).colors;
        }

        protected override void SetValueImpl(Script script, object obj, object value)
        {
          ((Summon) obj).colors = (Table) value;
        }
      }

      private sealed class FLDV_62136e0de3064d6fbd659b7bf8ddbb00 : HardwiredMemberDescriptor
      {
        internal FLDV_62136e0de3064d6fbd659b7bf8ddbb00()
          : base(typeof (Table), "spells", false, MemberDescriptorAccess.CanRead | MemberDescriptorAccess.CanWrite)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((Summon) obj).spells;
        }

        protected override void SetValueImpl(Script script, object obj, object value)
        {
          ((Summon) obj).spells = (Table) value;
        }
      }

      private sealed class FLDV_94dc22d310054f01901cd4594113219e : HardwiredMemberDescriptor
      {
        internal FLDV_94dc22d310054f01901cd4594113219e()
          : base(typeof (Table), "outfit", false, MemberDescriptorAccess.CanRead | MemberDescriptorAccess.CanWrite)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((Summon) obj).outfit;
        }

        protected override void SetValueImpl(Script script, object obj, object value)
        {
          ((Summon) obj).outfit = (Table) value;
        }
      }

      private sealed class FLDV_e877b4b593bc4e37a2c9065f6f163c04 : HardwiredMemberDescriptor
      {
        internal FLDV_e877b4b593bc4e37a2c9065f6f163c04()
          : base(typeof (BookOf), "elemental", false, MemberDescriptorAccess.CanRead | MemberDescriptorAccess.CanWrite)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((Summon) obj).elemental;
        }

        protected override void SetValueImpl(Script script, object obj, object value)
        {
          ((Summon) obj).elemental = (BookOf) value;
        }
      }
    }

    private sealed class TYPE_dc6ed2eb2d314d9980e5b6d91b221894 : HardwiredUserDataDescriptor
    {
      internal TYPE_dc6ed2eb2d314d9980e5b6d91b221894()
        : base(typeof (UIElement))
      {
        this.AddMember("__new", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("__new", typeof (UIElement), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_dc6ed2eb2d314d9980e5b6d91b221894.MTHD_e3faaab507a648aa9c9c92c63a732004()
        }));
        this.AddMember("get_isDead", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_isDead", typeof (UIElement), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_dc6ed2eb2d314d9980e5b6d91b221894.MTHD_14035c0ad8684eec809b9f61b50fcea9()
        }));
        this.AddMember("get_graphic", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_graphic", typeof (UIElement), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_dc6ed2eb2d314d9980e5b6d91b221894.MTHD_13095de75744453bb584e7c864fd0ec1()
        }));
        this.AddMember("set_graphic", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("set_graphic", typeof (UIElement), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_dc6ed2eb2d314d9980e5b6d91b221894.MTHD_b2030689aa304fe7948bf210e94263df()
        }));
        this.AddMember("get_anchor", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_anchor", typeof (UIElement), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_dc6ed2eb2d314d9980e5b6d91b221894.MTHD_1f378e93e6944402b0cd6c218a802926()
        }));
        this.AddMember("set_anchor", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("set_anchor", typeof (UIElement), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_dc6ed2eb2d314d9980e5b6d91b221894.MTHD_1bf28ea871c44ba7b26acf68f31adeee()
        }));
        this.AddMember("get_width", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_width", typeof (UIElement), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_dc6ed2eb2d314d9980e5b6d91b221894.MTHD_2dca98cfdaab4187a8a6e4e0a0f8cd1a()
        }));
        this.AddMember("set_width", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("set_width", typeof (UIElement), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_dc6ed2eb2d314d9980e5b6d91b221894.MTHD_ab51c6d1a26b4e25a748d7da97db1799()
        }));
        this.AddMember("get_height", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_height", typeof (UIElement), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_dc6ed2eb2d314d9980e5b6d91b221894.MTHD_17b47aadc35d44ad9b5f38d397c41329()
        }));
        this.AddMember("set_height", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("set_height", typeof (UIElement), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_dc6ed2eb2d314d9980e5b6d91b221894.MTHD_7fa21525dad84d759c3bf36c717f81c6()
        }));
        this.AddMember("get_size", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_size", typeof (UIElement), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_dc6ed2eb2d314d9980e5b6d91b221894.MTHD_f8103cc4e0ad46abbc4a28c026441aed()
        }));
        this.AddMember("set_size", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("set_size", typeof (UIElement), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_dc6ed2eb2d314d9980e5b6d91b221894.MTHD_f5ba30877116471faf4e960cfd53dd82()
        }));
        this.AddMember("get_x", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_x", typeof (UIElement), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_dc6ed2eb2d314d9980e5b6d91b221894.MTHD_8a066b147535403a85d1d2fed13fa4f9()
        }));
        this.AddMember("set_x", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("set_x", typeof (UIElement), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_dc6ed2eb2d314d9980e5b6d91b221894.MTHD_5133ebdd0889494aa0bee17cbf43bba8()
        }));
        this.AddMember("get_y", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_y", typeof (UIElement), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_dc6ed2eb2d314d9980e5b6d91b221894.MTHD_f2cb8766cad9488492e0de7d0d28a7fd()
        }));
        this.AddMember("set_y", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("set_y", typeof (UIElement), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_dc6ed2eb2d314d9980e5b6d91b221894.MTHD_d5e1ba4bb08e499dbd2fd9f99add11ff()
        }));
        this.AddMember("get_position", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_position", typeof (UIElement), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_dc6ed2eb2d314d9980e5b6d91b221894.MTHD_52d4879dce69424fa072d7ab0968ea08()
        }));
        this.AddMember("set_position", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("set_position", typeof (UIElement), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_dc6ed2eb2d314d9980e5b6d91b221894.MTHD_2e8603a2d3fd4a6690092181bbbf7f24()
        }));
        this.AddMember("get_pivot_x", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_pivot_x", typeof (UIElement), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_dc6ed2eb2d314d9980e5b6d91b221894.MTHD_af4ee78c6df8450eb674e4ef3521a98f()
        }));
        this.AddMember("set_pivot_x", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("set_pivot_x", typeof (UIElement), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_dc6ed2eb2d314d9980e5b6d91b221894.MTHD_64a6cad29c5e4e5797945cf109e6e071()
        }));
        this.AddMember("get_pivot_y", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_pivot_y", typeof (UIElement), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_dc6ed2eb2d314d9980e5b6d91b221894.MTHD_250e9decd35543258fa369e5328fdbbe()
        }));
        this.AddMember("set_pivot_y", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("set_pivot_y", typeof (UIElement), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_dc6ed2eb2d314d9980e5b6d91b221894.MTHD_cc2e7919847b4a9f8a4325ac192d43ce()
        }));
        this.AddMember("get_pivot", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_pivot", typeof (UIElement), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_dc6ed2eb2d314d9980e5b6d91b221894.MTHD_0995cc0856db49548722dbde0afaae9d()
        }));
        this.AddMember("set_pivot", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("set_pivot", typeof (UIElement), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_dc6ed2eb2d314d9980e5b6d91b221894.MTHD_5199f32dd3334243806f9479940225c8()
        }));
        this.AddMember("get_angle", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_angle", typeof (UIElement), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_dc6ed2eb2d314d9980e5b6d91b221894.MTHD_8a869c3cf7cd420589bfefaa50cdbcdf()
        }));
        this.AddMember("set_angle", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("set_angle", typeof (UIElement), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_dc6ed2eb2d314d9980e5b6d91b221894.MTHD_395cdf0e6b4d4a3ebb270a682e49b06e()
        }));
        this.AddMember("get_onClick", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_onClick", typeof (UIElement), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_dc6ed2eb2d314d9980e5b6d91b221894.MTHD_63bf862dce5a4d1a98874744e2644391()
        }));
        this.AddMember("set_onClick", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("set_onClick", typeof (UIElement), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_dc6ed2eb2d314d9980e5b6d91b221894.MTHD_b25cd079308f49e2a19fcd789208ddde()
        }));
        this.AddMember("get_color", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_color", typeof (UIElement), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_dc6ed2eb2d314d9980e5b6d91b221894.MTHD_8ba21ab33c31488cb54ee1260ec85c6a()
        }));
        this.AddMember("set_color", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("set_color", typeof (UIElement), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_dc6ed2eb2d314d9980e5b6d91b221894.MTHD_10fcf613ac00444e90d7c0218b3ad51a()
        }));
        this.AddMember("get_text", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_text", typeof (UIElement), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_dc6ed2eb2d314d9980e5b6d91b221894.MTHD_2ea17ede2e3d4a4383acec2ef823ad6a()
        }));
        this.AddMember("set_text", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("set_text", typeof (UIElement), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_dc6ed2eb2d314d9980e5b6d91b221894.MTHD_731a178ca02d4241906852f78fa67c3e()
        }));
        this.AddMember("get_textColor", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_textColor", typeof (UIElement), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_dc6ed2eb2d314d9980e5b6d91b221894.MTHD_4394992d37d144db86890a950d9eec8a()
        }));
        this.AddMember("set_textColor", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("set_textColor", typeof (UIElement), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_dc6ed2eb2d314d9980e5b6d91b221894.MTHD_24aaa334fd6a45f58728c74868e97ef6()
        }));
        this.AddMember("get_textStyle", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_textStyle", typeof (UIElement), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_dc6ed2eb2d314d9980e5b6d91b221894.MTHD_84963b890bd84daca499c3af92925fb5()
        }));
        this.AddMember("set_textStyle", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("set_textStyle", typeof (UIElement), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_dc6ed2eb2d314d9980e5b6d91b221894.MTHD_d590b97044db4dd1b5dd7a7df4d5c065()
        }));
        this.AddMember("get_textSize", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_textSize", typeof (UIElement), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_dc6ed2eb2d314d9980e5b6d91b221894.MTHD_49f3f7896a26454ea6dcffd4cb960a77()
        }));
        this.AddMember("set_textSize", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("set_textSize", typeof (UIElement), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_dc6ed2eb2d314d9980e5b6d91b221894.MTHD_2c50addf30fa41599921dfd548cca0c2()
        }));
        this.AddMember("activateInput", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("activateInput", typeof (UIElement), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_dc6ed2eb2d314d9980e5b6d91b221894.MTHD_273cf042d540409b9e7750ab700cddd2()
        }));
        this.AddMember("createUI", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("createUI", typeof (UIElement), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_dc6ed2eb2d314d9980e5b6d91b221894.MTHD_6311106a78ea4034a0457f87a18ab785()
        }));
        this.AddMember("createInput", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("createInput", typeof (UIElement), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_dc6ed2eb2d314d9980e5b6d91b221894.MTHD_15fa09f5b35542f196a4cee7bd8d8b65()
        }));
        this.AddMember("duplicate", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("duplicate", typeof (UIElement), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_dc6ed2eb2d314d9980e5b6d91b221894.MTHD_fcf740dc985843c68196d28af2f34b78()
        }));
        this.AddMember("destroyHierarchy", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("destroyHierarchy", typeof (UIElement), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_dc6ed2eb2d314d9980e5b6d91b221894.MTHD_e68ae1fc0ca649b8a1bea15fd1fcc9b0()
        }));
        this.AddMember("destroy", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("destroy", typeof (UIElement), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_dc6ed2eb2d314d9980e5b6d91b221894.MTHD_87c3fa08ff7b412a85880835d95f9758()
        }));
        this.AddMember("Equals", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("Equals", typeof (UIElement), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_dc6ed2eb2d314d9980e5b6d91b221894.MTHD_5bcdff246d0e4d5fada565b836ec4615()
        }));
        this.AddMember("GetHashCode", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("GetHashCode", typeof (UIElement), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_dc6ed2eb2d314d9980e5b6d91b221894.MTHD_b86c7a4c92b34650bc3faacca07f02a1()
        }));
        this.AddMember("GetType", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("GetType", typeof (UIElement), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_dc6ed2eb2d314d9980e5b6d91b221894.MTHD_b40bd45592a844cab7c71a5d0ad5026c()
        }));
        this.AddMember("ToString", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("ToString", typeof (UIElement), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_dc6ed2eb2d314d9980e5b6d91b221894.MTHD_3ae0b3b3fe1c431b8570ab3c7da69de7()
        }));
        this.AddMember("isDead", (IMemberDescriptor) new Bridge.TYPE_dc6ed2eb2d314d9980e5b6d91b221894.PROP_814c921136434fee88e0d42373de0390());
        this.AddMember("graphic", (IMemberDescriptor) new Bridge.TYPE_dc6ed2eb2d314d9980e5b6d91b221894.PROP_913938ec2d134008b4a926a70b328802());
        this.AddMember("anchor", (IMemberDescriptor) new Bridge.TYPE_dc6ed2eb2d314d9980e5b6d91b221894.PROP_10ed5f3195894f9c9edf8353ca50a6fd());
        this.AddMember("width", (IMemberDescriptor) new Bridge.TYPE_dc6ed2eb2d314d9980e5b6d91b221894.PROP_8c38fe7e324d49f4878c96fa212b2649());
        this.AddMember("height", (IMemberDescriptor) new Bridge.TYPE_dc6ed2eb2d314d9980e5b6d91b221894.PROP_809cf4df01d14b5c842310d6d769247f());
        this.AddMember("size", (IMemberDescriptor) new Bridge.TYPE_dc6ed2eb2d314d9980e5b6d91b221894.PROP_e6c391f60c634da7b04244045173d16a());
        this.AddMember("x", (IMemberDescriptor) new Bridge.TYPE_dc6ed2eb2d314d9980e5b6d91b221894.PROP_ca98de43012d4c80bd88cc1734538a11());
        this.AddMember("y", (IMemberDescriptor) new Bridge.TYPE_dc6ed2eb2d314d9980e5b6d91b221894.PROP_6c455da3c4154623be39e7afedf0b237());
        this.AddMember("position", (IMemberDescriptor) new Bridge.TYPE_dc6ed2eb2d314d9980e5b6d91b221894.PROP_ff8bc270b6b344658829c008fd789677());
        this.AddMember("pivot_x", (IMemberDescriptor) new Bridge.TYPE_dc6ed2eb2d314d9980e5b6d91b221894.PROP_de26e61dc2004c6fb2e1d02438b474e2());
        this.AddMember("pivot_y", (IMemberDescriptor) new Bridge.TYPE_dc6ed2eb2d314d9980e5b6d91b221894.PROP_2ff26947f15a46e5b5f33d9b3dc749cd());
        this.AddMember("pivot", (IMemberDescriptor) new Bridge.TYPE_dc6ed2eb2d314d9980e5b6d91b221894.PROP_062b5c2b95ae48d6991039857903d9fd());
        this.AddMember("angle", (IMemberDescriptor) new Bridge.TYPE_dc6ed2eb2d314d9980e5b6d91b221894.PROP_0126daec0f1b49e6be810a513ba3d35d());
        this.AddMember("onClick", (IMemberDescriptor) new Bridge.TYPE_dc6ed2eb2d314d9980e5b6d91b221894.PROP_3cce4a78c71a49288136e18acc9e4a34());
        this.AddMember("color", (IMemberDescriptor) new Bridge.TYPE_dc6ed2eb2d314d9980e5b6d91b221894.PROP_6c185faeff764b08bd9bd4bfbd6468be());
        this.AddMember("text", (IMemberDescriptor) new Bridge.TYPE_dc6ed2eb2d314d9980e5b6d91b221894.PROP_564c4e80ccfa40719477a35c5a3685eb());
        this.AddMember("textColor", (IMemberDescriptor) new Bridge.TYPE_dc6ed2eb2d314d9980e5b6d91b221894.PROP_6f2a11996f5d417ba5ff69bebddd12ff());
        this.AddMember("textStyle", (IMemberDescriptor) new Bridge.TYPE_dc6ed2eb2d314d9980e5b6d91b221894.PROP_2fb57494f486465d817dcfe1472b07a7());
        this.AddMember("textSize", (IMemberDescriptor) new Bridge.TYPE_dc6ed2eb2d314d9980e5b6d91b221894.PROP_4ff21952d8d940ca93fa641c39c2139f());
        this.AddMember("parent", (IMemberDescriptor) new Bridge.TYPE_dc6ed2eb2d314d9980e5b6d91b221894.FLDV_23af043a37824b7080f49c50fd82c2d3());
        this.AddMember("clickID", (IMemberDescriptor) new Bridge.TYPE_dc6ed2eb2d314d9980e5b6d91b221894.FLDV_fbd3a8fdfece4bae980b41259579147d());
        this.AddMember("clickDestroy", (IMemberDescriptor) new Bridge.TYPE_dc6ed2eb2d314d9980e5b6d91b221894.FLDV_9f0de34241a44aaeb9b4d93184d2fb59());
      }

      private sealed class MTHD_e3faaab507a648aa9c9c92c63a732004 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_e3faaab507a648aa9c9c92c63a732004()
        {
          this.Initialize(".ctor", true, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) new UIElement();
        }
      }

      private sealed class MTHD_14035c0ad8684eec809b9f61b50fcea9 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_14035c0ad8684eec809b9f61b50fcea9()
        {
          this.Initialize("get_isDead", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((UIElement) obj).isDead;
        }
      }

      private sealed class MTHD_13095de75744453bb584e7c864fd0ec1 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_13095de75744453bb584e7c864fd0ec1()
        {
          this.Initialize("get_graphic", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return ((UIElement) obj).graphic;
        }
      }

      private sealed class MTHD_b2030689aa304fe7948bf210e94263df : HardwiredMethodMemberDescriptor
      {
        internal MTHD_b2030689aa304fe7948bf210e94263df()
        {
          this.Initialize("set_graphic", false, new ParameterDescriptor[1]
          {
            new ParameterDescriptor("value", typeof (object))
          }, false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          ((UIElement) obj).graphic = pars[0];
          return (object) null;
        }
      }

      private sealed class MTHD_1f378e93e6944402b0cd6c218a802926 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_1f378e93e6944402b0cd6c218a802926()
        {
          this.Initialize("get_anchor", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((UIElement) obj).anchor;
        }
      }

      private sealed class MTHD_1bf28ea871c44ba7b26acf68f31adeee : HardwiredMethodMemberDescriptor
      {
        internal MTHD_1bf28ea871c44ba7b26acf68f31adeee()
        {
          this.Initialize("set_anchor", false, new ParameterDescriptor[1]
          {
            new ParameterDescriptor("value", typeof (Anchor))
          }, false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          ((UIElement) obj).anchor = (Anchor) pars[0];
          return (object) null;
        }
      }

      private sealed class MTHD_2dca98cfdaab4187a8a6e4e0a0f8cd1a : HardwiredMethodMemberDescriptor
      {
        internal MTHD_2dca98cfdaab4187a8a6e4e0a0f8cd1a()
        {
          this.Initialize("get_width", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((UIElement) obj).width;
        }
      }

      private sealed class MTHD_ab51c6d1a26b4e25a748d7da97db1799 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_ab51c6d1a26b4e25a748d7da97db1799()
        {
          this.Initialize("set_width", false, new ParameterDescriptor[1]
          {
            new ParameterDescriptor("value", typeof (double))
          }, false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          ((UIElement) obj).width = (double) pars[0];
          return (object) null;
        }
      }

      private sealed class MTHD_17b47aadc35d44ad9b5f38d397c41329 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_17b47aadc35d44ad9b5f38d397c41329()
        {
          this.Initialize("get_height", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((UIElement) obj).height;
        }
      }

      private sealed class MTHD_7fa21525dad84d759c3bf36c717f81c6 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_7fa21525dad84d759c3bf36c717f81c6()
        {
          this.Initialize("set_height", false, new ParameterDescriptor[1]
          {
            new ParameterDescriptor("value", typeof (double))
          }, false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          ((UIElement) obj).height = (double) pars[0];
          return (object) null;
        }
      }

      private sealed class MTHD_f8103cc4e0ad46abbc4a28c026441aed : HardwiredMethodMemberDescriptor
      {
        internal MTHD_f8103cc4e0ad46abbc4a28c026441aed()
        {
          this.Initialize("get_size", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((UIElement) obj).size;
        }
      }

      private sealed class MTHD_f5ba30877116471faf4e960cfd53dd82 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_f5ba30877116471faf4e960cfd53dd82()
        {
          this.Initialize("set_size", false, new ParameterDescriptor[1]
          {
            new ParameterDescriptor("value", typeof (Educative.Point))
          }, false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          ((UIElement) obj).size = (Educative.Point) pars[0];
          return (object) null;
        }
      }

      private sealed class MTHD_8a066b147535403a85d1d2fed13fa4f9 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_8a066b147535403a85d1d2fed13fa4f9()
        {
          this.Initialize("get_x", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((UIElement) obj).x;
        }
      }

      private sealed class MTHD_5133ebdd0889494aa0bee17cbf43bba8 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_5133ebdd0889494aa0bee17cbf43bba8()
        {
          this.Initialize("set_x", false, new ParameterDescriptor[1]
          {
            new ParameterDescriptor("value", typeof (double))
          }, false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          ((UIElement) obj).x = (double) pars[0];
          return (object) null;
        }
      }

      private sealed class MTHD_f2cb8766cad9488492e0de7d0d28a7fd : HardwiredMethodMemberDescriptor
      {
        internal MTHD_f2cb8766cad9488492e0de7d0d28a7fd()
        {
          this.Initialize("get_y", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((UIElement) obj).y;
        }
      }

      private sealed class MTHD_d5e1ba4bb08e499dbd2fd9f99add11ff : HardwiredMethodMemberDescriptor
      {
        internal MTHD_d5e1ba4bb08e499dbd2fd9f99add11ff()
        {
          this.Initialize("set_y", false, new ParameterDescriptor[1]
          {
            new ParameterDescriptor("value", typeof (double))
          }, false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          ((UIElement) obj).y = (double) pars[0];
          return (object) null;
        }
      }

      private sealed class MTHD_52d4879dce69424fa072d7ab0968ea08 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_52d4879dce69424fa072d7ab0968ea08()
        {
          this.Initialize("get_position", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((UIElement) obj).position;
        }
      }

      private sealed class MTHD_2e8603a2d3fd4a6690092181bbbf7f24 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_2e8603a2d3fd4a6690092181bbbf7f24()
        {
          this.Initialize("set_position", false, new ParameterDescriptor[1]
          {
            new ParameterDescriptor("value", typeof (Educative.Point))
          }, false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          ((UIElement) obj).position = (Educative.Point) pars[0];
          return (object) null;
        }
      }

      private sealed class MTHD_af4ee78c6df8450eb674e4ef3521a98f : HardwiredMethodMemberDescriptor
      {
        internal MTHD_af4ee78c6df8450eb674e4ef3521a98f()
        {
          this.Initialize("get_pivot_x", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((UIElement) obj).pivot_x;
        }
      }

      private sealed class MTHD_64a6cad29c5e4e5797945cf109e6e071 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_64a6cad29c5e4e5797945cf109e6e071()
        {
          this.Initialize("set_pivot_x", false, new ParameterDescriptor[1]
          {
            new ParameterDescriptor("value", typeof (double))
          }, false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          ((UIElement) obj).pivot_x = (double) pars[0];
          return (object) null;
        }
      }

      private sealed class MTHD_250e9decd35543258fa369e5328fdbbe : HardwiredMethodMemberDescriptor
      {
        internal MTHD_250e9decd35543258fa369e5328fdbbe()
        {
          this.Initialize("get_pivot_y", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((UIElement) obj).pivot_y;
        }
      }

      private sealed class MTHD_cc2e7919847b4a9f8a4325ac192d43ce : HardwiredMethodMemberDescriptor
      {
        internal MTHD_cc2e7919847b4a9f8a4325ac192d43ce()
        {
          this.Initialize("set_pivot_y", false, new ParameterDescriptor[1]
          {
            new ParameterDescriptor("value", typeof (double))
          }, false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          ((UIElement) obj).pivot_y = (double) pars[0];
          return (object) null;
        }
      }

      private sealed class MTHD_0995cc0856db49548722dbde0afaae9d : HardwiredMethodMemberDescriptor
      {
        internal MTHD_0995cc0856db49548722dbde0afaae9d()
        {
          this.Initialize("get_pivot", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((UIElement) obj).pivot;
        }
      }

      private sealed class MTHD_5199f32dd3334243806f9479940225c8 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_5199f32dd3334243806f9479940225c8()
        {
          this.Initialize("set_pivot", false, new ParameterDescriptor[1]
          {
            new ParameterDescriptor("value", typeof (Educative.Point))
          }, false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          ((UIElement) obj).pivot = (Educative.Point) pars[0];
          return (object) null;
        }
      }

      private sealed class MTHD_8a869c3cf7cd420589bfefaa50cdbcdf : HardwiredMethodMemberDescriptor
      {
        internal MTHD_8a869c3cf7cd420589bfefaa50cdbcdf()
        {
          this.Initialize("get_angle", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((UIElement) obj).angle;
        }
      }

      private sealed class MTHD_395cdf0e6b4d4a3ebb270a682e49b06e : HardwiredMethodMemberDescriptor
      {
        internal MTHD_395cdf0e6b4d4a3ebb270a682e49b06e()
        {
          this.Initialize("set_angle", false, new ParameterDescriptor[1]
          {
            new ParameterDescriptor("value", typeof (double))
          }, false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          ((UIElement) obj).angle = (double) pars[0];
          return (object) null;
        }
      }

      private sealed class MTHD_63bf862dce5a4d1a98874744e2644391 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_63bf862dce5a4d1a98874744e2644391()
        {
          this.Initialize("get_onClick", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((UIElement) obj).onClick;
        }
      }

      private sealed class MTHD_b25cd079308f49e2a19fcd789208ddde : HardwiredMethodMemberDescriptor
      {
        internal MTHD_b25cd079308f49e2a19fcd789208ddde()
        {
          this.Initialize("set_onClick", false, new ParameterDescriptor[1]
          {
            new ParameterDescriptor("value", typeof (string))
          }, false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          ((UIElement) obj).onClick = (string) pars[0];
          return (object) null;
        }
      }

      private sealed class MTHD_8ba21ab33c31488cb54ee1260ec85c6a : HardwiredMethodMemberDescriptor
      {
        internal MTHD_8ba21ab33c31488cb54ee1260ec85c6a()
        {
          this.Initialize("get_color", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((UIElement) obj).color;
        }
      }

      private sealed class MTHD_10fcf613ac00444e90d7c0218b3ad51a : HardwiredMethodMemberDescriptor
      {
        internal MTHD_10fcf613ac00444e90d7c0218b3ad51a()
        {
          this.Initialize("set_color", false, new ParameterDescriptor[1]
          {
            new ParameterDescriptor("value", typeof (string))
          }, false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          ((UIElement) obj).color = (string) pars[0];
          return (object) null;
        }
      }

      private sealed class MTHD_2ea17ede2e3d4a4383acec2ef823ad6a : HardwiredMethodMemberDescriptor
      {
        internal MTHD_2ea17ede2e3d4a4383acec2ef823ad6a()
        {
          this.Initialize("get_text", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((UIElement) obj).text;
        }
      }

      private sealed class MTHD_731a178ca02d4241906852f78fa67c3e : HardwiredMethodMemberDescriptor
      {
        internal MTHD_731a178ca02d4241906852f78fa67c3e()
        {
          this.Initialize("set_text", false, new ParameterDescriptor[1]
          {
            new ParameterDescriptor("value", typeof (string))
          }, false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          ((UIElement) obj).text = (string) pars[0];
          return (object) null;
        }
      }

      private sealed class MTHD_4394992d37d144db86890a950d9eec8a : HardwiredMethodMemberDescriptor
      {
        internal MTHD_4394992d37d144db86890a950d9eec8a()
        {
          this.Initialize("get_textColor", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((UIElement) obj).textColor;
        }
      }

      private sealed class MTHD_24aaa334fd6a45f58728c74868e97ef6 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_24aaa334fd6a45f58728c74868e97ef6()
        {
          this.Initialize("set_textColor", false, new ParameterDescriptor[1]
          {
            new ParameterDescriptor("value", typeof (string))
          }, false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          ((UIElement) obj).textColor = (string) pars[0];
          return (object) null;
        }
      }

      private sealed class MTHD_84963b890bd84daca499c3af92925fb5 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_84963b890bd84daca499c3af92925fb5()
        {
          this.Initialize("get_textStyle", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((UIElement) obj).textStyle;
        }
      }

      private sealed class MTHD_d590b97044db4dd1b5dd7a7df4d5c065 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_d590b97044db4dd1b5dd7a7df4d5c065()
        {
          this.Initialize("set_textStyle", false, new ParameterDescriptor[1]
          {
            new ParameterDescriptor("value", typeof (int))
          }, false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          ((UIElement) obj).textStyle = (int) pars[0];
          return (object) null;
        }
      }

      private sealed class MTHD_49f3f7896a26454ea6dcffd4cb960a77 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_49f3f7896a26454ea6dcffd4cb960a77()
        {
          this.Initialize("get_textSize", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((UIElement) obj).textSize;
        }
      }

      private sealed class MTHD_2c50addf30fa41599921dfd548cca0c2 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_2c50addf30fa41599921dfd548cca0c2()
        {
          this.Initialize("set_textSize", false, new ParameterDescriptor[1]
          {
            new ParameterDescriptor("value", typeof (double))
          }, false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          ((UIElement) obj).textSize = (double) pars[0];
          return (object) null;
        }
      }

      private sealed class MTHD_273cf042d540409b9e7750ab700cddd2 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_273cf042d540409b9e7750ab700cddd2()
        {
          this.Initialize("activateInput", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          ((UIElement) obj).activateInput();
          return (object) DynValue.Void;
        }
      }

      private sealed class MTHD_6311106a78ea4034a0457f87a18ab785 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_6311106a78ea4034a0457f87a18ab785()
        {
          this.Initialize("createUI", true, new ParameterDescriptor[2]
          {
            new ParameterDescriptor("script", typeof (Script)),
            new ParameterDescriptor("parent", typeof (UIElement), true, (object) new DefaultValue())
          }, false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return argscount <= 1 ? (object) UIElement.createUI((Script) pars[0]) : (object) UIElement.createUI((Script) pars[0], (UIElement) pars[1]);
        }
      }

      private sealed class MTHD_15fa09f5b35542f196a4cee7bd8d8b65 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_15fa09f5b35542f196a4cee7bd8d8b65()
        {
          this.Initialize("createInput", true, new ParameterDescriptor[6]
          {
            new ParameterDescriptor("script", typeof (Script)),
            new ParameterDescriptor("parent", typeof (UIElement), true, (object) new DefaultValue()),
            new ParameterDescriptor("callback", typeof (string), true, (object) new DefaultValue()),
            new ParameterDescriptor("onSubmit", typeof (bool), true, (object) new DefaultValue()),
            new ParameterDescriptor("active", typeof (bool), true, (object) new DefaultValue()),
            new ParameterDescriptor("forceFocus", typeof (bool), true, (object) new DefaultValue())
          }, false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          if (argscount <= 1)
            return (object) UIElement.createInput((Script) pars[0]);
          if (argscount <= 2)
            return (object) UIElement.createInput((Script) pars[0], (UIElement) pars[1]);
          if (argscount <= 3)
            return (object) UIElement.createInput((Script) pars[0], (UIElement) pars[1], (string) pars[2]);
          if (argscount <= 4)
            return (object) UIElement.createInput((Script) pars[0], (UIElement) pars[1], (string) pars[2], (bool) pars[3]);
          return argscount <= 5 ? (object) UIElement.createInput((Script) pars[0], (UIElement) pars[1], (string) pars[2], (bool) pars[3], (bool) pars[4]) : (object) UIElement.createInput((Script) pars[0], (UIElement) pars[1], (string) pars[2], (bool) pars[3], (bool) pars[4], (bool) pars[5]);
        }
      }

      private sealed class MTHD_fcf740dc985843c68196d28af2f34b78 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_fcf740dc985843c68196d28af2f34b78()
        {
          this.Initialize("duplicate", false, new ParameterDescriptor[2]
          {
            new ParameterDescriptor("script", typeof (Script)),
            new ParameterDescriptor("parent", typeof (UIElement), true, (object) new DefaultValue())
          }, false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return argscount <= 1 ? (object) ((UIElement) obj).duplicate((Script) pars[0]) : (object) ((UIElement) obj).duplicate((Script) pars[0], (UIElement) pars[1]);
        }
      }

      private sealed class MTHD_e68ae1fc0ca649b8a1bea15fd1fcc9b0 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_e68ae1fc0ca649b8a1bea15fd1fcc9b0()
        {
          this.Initialize("destroyHierarchy", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          ((UIElement) obj).destroyHierarchy();
          return (object) DynValue.Void;
        }
      }

      private sealed class MTHD_87c3fa08ff7b412a85880835d95f9758 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_87c3fa08ff7b412a85880835d95f9758()
        {
          this.Initialize("destroy", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          ((UIElement) obj).destroy();
          return (object) DynValue.Void;
        }
      }

      private sealed class MTHD_5bcdff246d0e4d5fada565b836ec4615 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_5bcdff246d0e4d5fada565b836ec4615()
        {
          this.Initialize("Equals", false, new ParameterDescriptor[1]
          {
            new ParameterDescriptor("obj", typeof (object))
          }, false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) obj.Equals(pars[0]);
        }
      }

      private sealed class MTHD_b86c7a4c92b34650bc3faacca07f02a1 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_b86c7a4c92b34650bc3faacca07f02a1()
        {
          this.Initialize("GetHashCode", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) obj.GetHashCode();
        }
      }

      private sealed class MTHD_b40bd45592a844cab7c71a5d0ad5026c : HardwiredMethodMemberDescriptor
      {
        internal MTHD_b40bd45592a844cab7c71a5d0ad5026c()
        {
          this.Initialize("GetType", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) obj.GetType();
        }
      }

      private sealed class MTHD_3ae0b3b3fe1c431b8570ab3c7da69de7 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_3ae0b3b3fe1c431b8570ab3c7da69de7()
        {
          this.Initialize("ToString", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) obj.ToString();
        }
      }

      private sealed class PROP_814c921136434fee88e0d42373de0390 : HardwiredMemberDescriptor
      {
        internal PROP_814c921136434fee88e0d42373de0390()
          : base(typeof (bool), "isDead", false, MemberDescriptorAccess.CanRead)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((UIElement) obj).isDead;
        }
      }

      private sealed class PROP_913938ec2d134008b4a926a70b328802 : HardwiredMemberDescriptor
      {
        internal PROP_913938ec2d134008b4a926a70b328802()
          : base(typeof (object), "graphic", false, MemberDescriptorAccess.CanRead | MemberDescriptorAccess.CanWrite)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return ((UIElement) obj).graphic;
        }

        protected override void SetValueImpl(Script script, object obj, object value)
        {
          ((UIElement) obj).graphic = value;
        }
      }

      private sealed class PROP_10ed5f3195894f9c9edf8353ca50a6fd : HardwiredMemberDescriptor
      {
        internal PROP_10ed5f3195894f9c9edf8353ca50a6fd()
          : base(typeof (Anchor), "anchor", false, MemberDescriptorAccess.CanRead | MemberDescriptorAccess.CanWrite)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((UIElement) obj).anchor;
        }

        protected override void SetValueImpl(Script script, object obj, object value)
        {
          ((UIElement) obj).anchor = (Anchor) value;
        }
      }

      private sealed class PROP_8c38fe7e324d49f4878c96fa212b2649 : HardwiredMemberDescriptor
      {
        internal PROP_8c38fe7e324d49f4878c96fa212b2649()
          : base(typeof (double), "width", false, MemberDescriptorAccess.CanRead | MemberDescriptorAccess.CanWrite)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((UIElement) obj).width;
        }

        protected override void SetValueImpl(Script script, object obj, object value)
        {
          ((UIElement) obj).width = (double) value;
        }
      }

      private sealed class PROP_809cf4df01d14b5c842310d6d769247f : HardwiredMemberDescriptor
      {
        internal PROP_809cf4df01d14b5c842310d6d769247f()
          : base(typeof (double), "height", false, MemberDescriptorAccess.CanRead | MemberDescriptorAccess.CanWrite)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((UIElement) obj).height;
        }

        protected override void SetValueImpl(Script script, object obj, object value)
        {
          ((UIElement) obj).height = (double) value;
        }
      }

      private sealed class PROP_e6c391f60c634da7b04244045173d16a : HardwiredMemberDescriptor
      {
        internal PROP_e6c391f60c634da7b04244045173d16a()
          : base(typeof (Educative.Point), "size", false, MemberDescriptorAccess.CanRead | MemberDescriptorAccess.CanWrite)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((UIElement) obj).size;
        }

        protected override void SetValueImpl(Script script, object obj, object value)
        {
          ((UIElement) obj).size = (Educative.Point) value;
        }
      }

      private sealed class PROP_ca98de43012d4c80bd88cc1734538a11 : HardwiredMemberDescriptor
      {
        internal PROP_ca98de43012d4c80bd88cc1734538a11()
          : base(typeof (double), "x", false, MemberDescriptorAccess.CanRead | MemberDescriptorAccess.CanWrite)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((UIElement) obj).x;
        }

        protected override void SetValueImpl(Script script, object obj, object value)
        {
          ((UIElement) obj).x = (double) value;
        }
      }

      private sealed class PROP_6c455da3c4154623be39e7afedf0b237 : HardwiredMemberDescriptor
      {
        internal PROP_6c455da3c4154623be39e7afedf0b237()
          : base(typeof (double), "y", false, MemberDescriptorAccess.CanRead | MemberDescriptorAccess.CanWrite)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((UIElement) obj).y;
        }

        protected override void SetValueImpl(Script script, object obj, object value)
        {
          ((UIElement) obj).y = (double) value;
        }
      }

      private sealed class PROP_ff8bc270b6b344658829c008fd789677 : HardwiredMemberDescriptor
      {
        internal PROP_ff8bc270b6b344658829c008fd789677()
          : base(typeof (Educative.Point), "position", false, MemberDescriptorAccess.CanRead | MemberDescriptorAccess.CanWrite)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((UIElement) obj).position;
        }

        protected override void SetValueImpl(Script script, object obj, object value)
        {
          ((UIElement) obj).position = (Educative.Point) value;
        }
      }

      private sealed class PROP_de26e61dc2004c6fb2e1d02438b474e2 : HardwiredMemberDescriptor
      {
        internal PROP_de26e61dc2004c6fb2e1d02438b474e2()
          : base(typeof (double), "pivot_x", false, MemberDescriptorAccess.CanRead | MemberDescriptorAccess.CanWrite)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((UIElement) obj).pivot_x;
        }

        protected override void SetValueImpl(Script script, object obj, object value)
        {
          ((UIElement) obj).pivot_x = (double) value;
        }
      }

      private sealed class PROP_2ff26947f15a46e5b5f33d9b3dc749cd : HardwiredMemberDescriptor
      {
        internal PROP_2ff26947f15a46e5b5f33d9b3dc749cd()
          : base(typeof (double), "pivot_y", false, MemberDescriptorAccess.CanRead | MemberDescriptorAccess.CanWrite)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((UIElement) obj).pivot_y;
        }

        protected override void SetValueImpl(Script script, object obj, object value)
        {
          ((UIElement) obj).pivot_y = (double) value;
        }
      }

      private sealed class PROP_062b5c2b95ae48d6991039857903d9fd : HardwiredMemberDescriptor
      {
        internal PROP_062b5c2b95ae48d6991039857903d9fd()
          : base(typeof (Educative.Point), "pivot", false, MemberDescriptorAccess.CanRead | MemberDescriptorAccess.CanWrite)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((UIElement) obj).pivot;
        }

        protected override void SetValueImpl(Script script, object obj, object value)
        {
          ((UIElement) obj).pivot = (Educative.Point) value;
        }
      }

      private sealed class PROP_0126daec0f1b49e6be810a513ba3d35d : HardwiredMemberDescriptor
      {
        internal PROP_0126daec0f1b49e6be810a513ba3d35d()
          : base(typeof (double), "angle", false, MemberDescriptorAccess.CanRead | MemberDescriptorAccess.CanWrite)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((UIElement) obj).angle;
        }

        protected override void SetValueImpl(Script script, object obj, object value)
        {
          ((UIElement) obj).angle = (double) value;
        }
      }

      private sealed class PROP_3cce4a78c71a49288136e18acc9e4a34 : HardwiredMemberDescriptor
      {
        internal PROP_3cce4a78c71a49288136e18acc9e4a34()
          : base(typeof (string), "onClick", false, MemberDescriptorAccess.CanRead | MemberDescriptorAccess.CanWrite)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((UIElement) obj).onClick;
        }

        protected override void SetValueImpl(Script script, object obj, object value)
        {
          ((UIElement) obj).onClick = (string) value;
        }
      }

      private sealed class PROP_6c185faeff764b08bd9bd4bfbd6468be : HardwiredMemberDescriptor
      {
        internal PROP_6c185faeff764b08bd9bd4bfbd6468be()
          : base(typeof (string), "color", false, MemberDescriptorAccess.CanRead | MemberDescriptorAccess.CanWrite)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((UIElement) obj).color;
        }

        protected override void SetValueImpl(Script script, object obj, object value)
        {
          ((UIElement) obj).color = (string) value;
        }
      }

      private sealed class PROP_564c4e80ccfa40719477a35c5a3685eb : HardwiredMemberDescriptor
      {
        internal PROP_564c4e80ccfa40719477a35c5a3685eb()
          : base(typeof (string), "text", false, MemberDescriptorAccess.CanRead | MemberDescriptorAccess.CanWrite)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((UIElement) obj).text;
        }

        protected override void SetValueImpl(Script script, object obj, object value)
        {
          ((UIElement) obj).text = (string) value;
        }
      }

      private sealed class PROP_6f2a11996f5d417ba5ff69bebddd12ff : HardwiredMemberDescriptor
      {
        internal PROP_6f2a11996f5d417ba5ff69bebddd12ff()
          : base(typeof (string), "textColor", false, MemberDescriptorAccess.CanRead | MemberDescriptorAccess.CanWrite)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((UIElement) obj).textColor;
        }

        protected override void SetValueImpl(Script script, object obj, object value)
        {
          ((UIElement) obj).textColor = (string) value;
        }
      }

      private sealed class PROP_2fb57494f486465d817dcfe1472b07a7 : HardwiredMemberDescriptor
      {
        internal PROP_2fb57494f486465d817dcfe1472b07a7()
          : base(typeof (int), "textStyle", false, MemberDescriptorAccess.CanRead | MemberDescriptorAccess.CanWrite)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((UIElement) obj).textStyle;
        }

        protected override void SetValueImpl(Script script, object obj, object value)
        {
          ((UIElement) obj).textStyle = (int) value;
        }
      }

      private sealed class PROP_4ff21952d8d940ca93fa641c39c2139f : HardwiredMemberDescriptor
      {
        internal PROP_4ff21952d8d940ca93fa641c39c2139f()
          : base(typeof (double), "textSize", false, MemberDescriptorAccess.CanRead | MemberDescriptorAccess.CanWrite)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((UIElement) obj).textSize;
        }

        protected override void SetValueImpl(Script script, object obj, object value)
        {
          ((UIElement) obj).textSize = (double) value;
        }
      }

      private sealed class FLDV_23af043a37824b7080f49c50fd82c2d3 : HardwiredMemberDescriptor
      {
        internal FLDV_23af043a37824b7080f49c50fd82c2d3()
          : base(typeof (UIElement), "parent", false, MemberDescriptorAccess.CanRead | MemberDescriptorAccess.CanWrite)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((UIElement) obj).parent;
        }

        protected override void SetValueImpl(Script script, object obj, object value)
        {
          ((UIElement) obj).parent = (UIElement) value;
        }
      }

      private sealed class FLDV_fbd3a8fdfece4bae980b41259579147d : HardwiredMemberDescriptor
      {
        internal FLDV_fbd3a8fdfece4bae980b41259579147d()
          : base(typeof (object), "clickID", false, MemberDescriptorAccess.CanRead | MemberDescriptorAccess.CanWrite)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return ((UIElement) obj).clickID;
        }

        protected override void SetValueImpl(Script script, object obj, object value)
        {
          ((UIElement) obj).clickID = value;
        }
      }

      private sealed class FLDV_9f0de34241a44aaeb9b4d93184d2fb59 : HardwiredMemberDescriptor
      {
        internal FLDV_9f0de34241a44aaeb9b4d93184d2fb59()
          : base(typeof (bool), "clickDestroy", false, MemberDescriptorAccess.CanRead | MemberDescriptorAccess.CanWrite)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((UIElement) obj).clickDestroy;
        }

        protected override void SetValueImpl(Script script, object obj, object value)
        {
          ((UIElement) obj).clickDestroy = (bool) value;
        }
      }
    }

    private sealed class TYPE_125eff4cc4dc4707bba5e4087da7644c : HardwiredUserDataDescriptor
    {
      internal TYPE_125eff4cc4dc4707bba5e4087da7644c()
        : base(typeof (MyPoll.Item))
      {
        this.AddMember("__new", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("__new", typeof (MyPoll.Item), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_125eff4cc4dc4707bba5e4087da7644c.MTHD_6d7ae0c57b7a43ba8d0875ab6789a32e()
        }));
        this.AddMember("addAnswer", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("addAnswer", typeof (MyPoll.Item), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_125eff4cc4dc4707bba5e4087da7644c.MTHD_77f42d1a88764a2ebc512d8fb5a178d9()
        }));
        this.AddMember("Equals", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("Equals", typeof (MyPoll.Item), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_125eff4cc4dc4707bba5e4087da7644c.MTHD_fd32aa9c446547af8c4984db4522e294()
        }));
        this.AddMember("GetHashCode", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("GetHashCode", typeof (MyPoll.Item), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_125eff4cc4dc4707bba5e4087da7644c.MTHD_827b0edf8c864332b8fd0ddb5121ae95()
        }));
        this.AddMember("GetType", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("GetType", typeof (MyPoll.Item), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_125eff4cc4dc4707bba5e4087da7644c.MTHD_6cefa0525ccc40888b4fb663954215c3()
        }));
        this.AddMember("ToString", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("ToString", typeof (MyPoll.Item), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_125eff4cc4dc4707bba5e4087da7644c.MTHD_3412949c6dd746c0a07bef9cbeff807c()
        }));
        this.AddMember("question", (IMemberDescriptor) new Bridge.TYPE_125eff4cc4dc4707bba5e4087da7644c.FLDV_e1f4b4adce164f81bcb2a8baca38b9d4());
        this.AddMember("multipleSelection", (IMemberDescriptor) new Bridge.TYPE_125eff4cc4dc4707bba5e4087da7644c.FLDV_6ba9e31c426548359480d5123372d7e2());
        this.AddMember("requiresAnswer", (IMemberDescriptor) new Bridge.TYPE_125eff4cc4dc4707bba5e4087da7644c.FLDV_7250b129093b4e2296a2f13c9d329dd0());
        this.AddMember("answers", (IMemberDescriptor) new Bridge.TYPE_125eff4cc4dc4707bba5e4087da7644c.FLDV_4a1bf17931bf48d8bccfaa74eb5450e5());
      }

      private sealed class MTHD_6d7ae0c57b7a43ba8d0875ab6789a32e : HardwiredMethodMemberDescriptor
      {
        internal MTHD_6d7ae0c57b7a43ba8d0875ab6789a32e()
        {
          this.Initialize(".ctor", true, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) new MyPoll.Item();
        }
      }

      private sealed class MTHD_77f42d1a88764a2ebc512d8fb5a178d9 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_77f42d1a88764a2ebc512d8fb5a178d9()
        {
          this.Initialize("addAnswer", false, new ParameterDescriptor[3]
          {
            new ParameterDescriptor("answer", typeof (string)),
            new ParameterDescriptor("checkable", typeof (bool), true, (object) new DefaultValue()),
            new ParameterDescriptor("allowUserInput", typeof (bool), true, (object) new DefaultValue())
          }, false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          if (argscount <= 1)
            return (object) ((MyPoll.Item) obj).addAnswer((string) pars[0]);
          return argscount <= 2 ? (object) ((MyPoll.Item) obj).addAnswer((string) pars[0], (bool) pars[1]) : (object) ((MyPoll.Item) obj).addAnswer((string) pars[0], (bool) pars[1], (bool) pars[2]);
        }
      }

      private sealed class MTHD_fd32aa9c446547af8c4984db4522e294 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_fd32aa9c446547af8c4984db4522e294()
        {
          this.Initialize("Equals", false, new ParameterDescriptor[1]
          {
            new ParameterDescriptor("obj", typeof (object))
          }, false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) obj.Equals(pars[0]);
        }
      }

      private sealed class MTHD_827b0edf8c864332b8fd0ddb5121ae95 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_827b0edf8c864332b8fd0ddb5121ae95()
        {
          this.Initialize("GetHashCode", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) obj.GetHashCode();
        }
      }

      private sealed class MTHD_6cefa0525ccc40888b4fb663954215c3 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_6cefa0525ccc40888b4fb663954215c3()
        {
          this.Initialize("GetType", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) obj.GetType();
        }
      }

      private sealed class MTHD_3412949c6dd746c0a07bef9cbeff807c : HardwiredMethodMemberDescriptor
      {
        internal MTHD_3412949c6dd746c0a07bef9cbeff807c()
        {
          this.Initialize("ToString", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) obj.ToString();
        }
      }

      private sealed class FLDV_e1f4b4adce164f81bcb2a8baca38b9d4 : HardwiredMemberDescriptor
      {
        internal FLDV_e1f4b4adce164f81bcb2a8baca38b9d4()
          : base(typeof (string), "question", false, MemberDescriptorAccess.CanRead | MemberDescriptorAccess.CanWrite)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((MyPoll.Item) obj).question;
        }

        protected override void SetValueImpl(Script script, object obj, object value)
        {
          ((MyPoll.Item) obj).question = (string) value;
        }
      }

      private sealed class FLDV_6ba9e31c426548359480d5123372d7e2 : HardwiredMemberDescriptor
      {
        internal FLDV_6ba9e31c426548359480d5123372d7e2()
          : base(typeof (bool), "multipleSelection", false, MemberDescriptorAccess.CanRead | MemberDescriptorAccess.CanWrite)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((MyPoll.Item) obj).multipleSelection;
        }

        protected override void SetValueImpl(Script script, object obj, object value)
        {
          ((MyPoll.Item) obj).multipleSelection = (bool) value;
        }
      }

      private sealed class FLDV_7250b129093b4e2296a2f13c9d329dd0 : HardwiredMemberDescriptor
      {
        internal FLDV_7250b129093b4e2296a2f13c9d329dd0()
          : base(typeof (bool), "requiresAnswer", false, MemberDescriptorAccess.CanRead | MemberDescriptorAccess.CanWrite)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((MyPoll.Item) obj).requiresAnswer;
        }

        protected override void SetValueImpl(Script script, object obj, object value)
        {
          ((MyPoll.Item) obj).requiresAnswer = (bool) value;
        }
      }

      private sealed class FLDV_4a1bf17931bf48d8bccfaa74eb5450e5 : HardwiredMemberDescriptor
      {
        internal FLDV_4a1bf17931bf48d8bccfaa74eb5450e5()
          : base(typeof (List<MyPoll.Answer>), "answers", false, MemberDescriptorAccess.CanRead | MemberDescriptorAccess.CanWrite)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((MyPoll.Item) obj).answers;
        }

        protected override void SetValueImpl(Script script, object obj, object value)
        {
          ((MyPoll.Item) obj).answers = (List<MyPoll.Answer>) value;
        }
      }
    }

    private sealed class TYPE_6e8ede65746d49d4bf65cbdc608ba3e1 : HardwiredUserDataDescriptor
    {
      internal TYPE_6e8ede65746d49d4bf65cbdc608ba3e1()
        : base(typeof (MyPoll.Answer))
      {
        this.AddMember("__new", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("__new", typeof (MyPoll.Answer), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_6e8ede65746d49d4bf65cbdc608ba3e1.MTHD_1e4e34a509df4da0b21a3d839347a65b()
        }));
        this.AddMember("Equals", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("Equals", typeof (MyPoll.Answer), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_6e8ede65746d49d4bf65cbdc608ba3e1.MTHD_bbb1e89832444b4692189c1bf8b843b1()
        }));
        this.AddMember("GetHashCode", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("GetHashCode", typeof (MyPoll.Answer), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_6e8ede65746d49d4bf65cbdc608ba3e1.MTHD_424af78af6b546d3a9a21059eb3aeaa7()
        }));
        this.AddMember("GetType", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("GetType", typeof (MyPoll.Answer), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_6e8ede65746d49d4bf65cbdc608ba3e1.MTHD_1d8ee2ae9643403e8ea3940b65fd91e0()
        }));
        this.AddMember("ToString", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("ToString", typeof (MyPoll.Answer), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_6e8ede65746d49d4bf65cbdc608ba3e1.MTHD_a22444442d4d4ed4a68d1d9ae2bbc0ae()
        }));
        this.AddMember("text", (IMemberDescriptor) new Bridge.TYPE_6e8ede65746d49d4bf65cbdc608ba3e1.FLDV_f16b972e3a794aee81caef17c7d31fa5());
        this.AddMember("allowUserInput", (IMemberDescriptor) new Bridge.TYPE_6e8ede65746d49d4bf65cbdc608ba3e1.FLDV_c234f07ea38141088f5c9e0324eda789());
        this.AddMember("checkable", (IMemberDescriptor) new Bridge.TYPE_6e8ede65746d49d4bf65cbdc608ba3e1.FLDV_d6574902e7c749e1bdf573598e6f1579());
        this.AddMember("userInput", (IMemberDescriptor) new Bridge.TYPE_6e8ede65746d49d4bf65cbdc608ba3e1.FLDV_ec7ebd2931d34a82a126a045a625f657());
        this.AddMember("isChecked", (IMemberDescriptor) new Bridge.TYPE_6e8ede65746d49d4bf65cbdc608ba3e1.FLDV_ba10c93c17a642adaac1ddba4a38afd7());
      }

      private sealed class MTHD_1e4e34a509df4da0b21a3d839347a65b : HardwiredMethodMemberDescriptor
      {
        internal MTHD_1e4e34a509df4da0b21a3d839347a65b()
        {
          this.Initialize(".ctor", true, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) new MyPoll.Answer();
        }
      }

      private sealed class MTHD_bbb1e89832444b4692189c1bf8b843b1 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_bbb1e89832444b4692189c1bf8b843b1()
        {
          this.Initialize("Equals", false, new ParameterDescriptor[1]
          {
            new ParameterDescriptor("obj", typeof (object))
          }, false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) obj.Equals(pars[0]);
        }
      }

      private sealed class MTHD_424af78af6b546d3a9a21059eb3aeaa7 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_424af78af6b546d3a9a21059eb3aeaa7()
        {
          this.Initialize("GetHashCode", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) obj.GetHashCode();
        }
      }

      private sealed class MTHD_1d8ee2ae9643403e8ea3940b65fd91e0 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_1d8ee2ae9643403e8ea3940b65fd91e0()
        {
          this.Initialize("GetType", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) obj.GetType();
        }
      }

      private sealed class MTHD_a22444442d4d4ed4a68d1d9ae2bbc0ae : HardwiredMethodMemberDescriptor
      {
        internal MTHD_a22444442d4d4ed4a68d1d9ae2bbc0ae()
        {
          this.Initialize("ToString", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) obj.ToString();
        }
      }

      private sealed class FLDV_f16b972e3a794aee81caef17c7d31fa5 : HardwiredMemberDescriptor
      {
        internal FLDV_f16b972e3a794aee81caef17c7d31fa5()
          : base(typeof (string), "text", false, MemberDescriptorAccess.CanRead | MemberDescriptorAccess.CanWrite)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((MyPoll.Answer) obj).text;
        }

        protected override void SetValueImpl(Script script, object obj, object value)
        {
          ((MyPoll.Answer) obj).text = (string) value;
        }
      }

      private sealed class FLDV_c234f07ea38141088f5c9e0324eda789 : HardwiredMemberDescriptor
      {
        internal FLDV_c234f07ea38141088f5c9e0324eda789()
          : base(typeof (bool), "allowUserInput", false, MemberDescriptorAccess.CanRead | MemberDescriptorAccess.CanWrite)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((MyPoll.Answer) obj).allowUserInput;
        }

        protected override void SetValueImpl(Script script, object obj, object value)
        {
          ((MyPoll.Answer) obj).allowUserInput = (bool) value;
        }
      }

      private sealed class FLDV_d6574902e7c749e1bdf573598e6f1579 : HardwiredMemberDescriptor
      {
        internal FLDV_d6574902e7c749e1bdf573598e6f1579()
          : base(typeof (bool), "checkable", false, MemberDescriptorAccess.CanRead | MemberDescriptorAccess.CanWrite)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((MyPoll.Answer) obj).checkable;
        }

        protected override void SetValueImpl(Script script, object obj, object value)
        {
          ((MyPoll.Answer) obj).checkable = (bool) value;
        }
      }

      private sealed class FLDV_ec7ebd2931d34a82a126a045a625f657 : HardwiredMemberDescriptor
      {
        internal FLDV_ec7ebd2931d34a82a126a045a625f657()
          : base(typeof (string), "userInput", false, MemberDescriptorAccess.CanRead | MemberDescriptorAccess.CanWrite)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((MyPoll.Answer) obj).userInput;
        }

        protected override void SetValueImpl(Script script, object obj, object value)
        {
          ((MyPoll.Answer) obj).userInput = (string) value;
        }
      }

      private sealed class FLDV_ba10c93c17a642adaac1ddba4a38afd7 : HardwiredMemberDescriptor
      {
        internal FLDV_ba10c93c17a642adaac1ddba4a38afd7()
          : base(typeof (bool), "isChecked", false, MemberDescriptorAccess.CanRead | MemberDescriptorAccess.CanWrite)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((MyPoll.Answer) obj).isChecked;
        }

        protected override void SetValueImpl(Script script, object obj, object value)
        {
          ((MyPoll.Answer) obj).isChecked = (bool) value;
        }
      }
    }

    private sealed class TYPE_04f7a46397b74888a5c780ef6e1a29b5 : HardwiredUserDataDescriptor
    {
      internal TYPE_04f7a46397b74888a5c780ef6e1a29b5()
        : base(typeof (MyPoll))
      {
        this.AddMember("__new", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("__new", typeof (MyPoll), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_04f7a46397b74888a5c780ef6e1a29b5.MTHD_83d2f5ed369943b78a4a62b6e5498c06()
        }));
        this.AddMember("GetQuestionsAsString", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("GetQuestionsAsString", typeof (MyPoll), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_04f7a46397b74888a5c780ef6e1a29b5.MTHD_646b030baf574a7e860e590485361c1a()
        }));
        this.AddMember("SanitizeResponses", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("SanitizeResponses", typeof (MyPoll), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_04f7a46397b74888a5c780ef6e1a29b5.MTHD_7ce66ebfbb2d418e8512547d8d25ad9d()
        }));
        this.AddMember("addItem", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("addItem", typeof (MyPoll), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_04f7a46397b74888a5c780ef6e1a29b5.MTHD_3e10181122274209beb77bd9a4e94b96()
        }));
        this.AddMember("construct", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("construct", typeof (MyPoll), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_04f7a46397b74888a5c780ef6e1a29b5.MTHD_d6306f383de04163909c9774bdf7d255()
        }));
        this.AddMember("ToString", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("ToString", typeof (MyPoll), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_04f7a46397b74888a5c780ef6e1a29b5.MTHD_1794cc61b69848f08ee28da96159cacd()
        }));
        this.AddMember("fromString", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("fromString", typeof (MyPoll), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_04f7a46397b74888a5c780ef6e1a29b5.MTHD_ac270e37346f46d48fd7c7f15f95a670()
        }));
        this.AddMember("sendToServer", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("sendToServer", typeof (MyPoll), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_04f7a46397b74888a5c780ef6e1a29b5.MTHD_050b7f3a37f34e47893fa54a17d20399()
        }));
        this.AddMember("closePoll", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("closePoll", typeof (MyPoll), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_04f7a46397b74888a5c780ef6e1a29b5.MTHD_4ca26011180346afbe9b4be0ce5d8855()
        }));
        this.AddMember("test", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("test", typeof (MyPoll), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_04f7a46397b74888a5c780ef6e1a29b5.MTHD_5c4a508c4ce64a58bab27be3519f6aff()
        }));
        this.AddMember("Equals", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("Equals", typeof (MyPoll), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_04f7a46397b74888a5c780ef6e1a29b5.MTHD_849f6b7e22b840d28881a79cf8b98b7c()
        }));
        this.AddMember("GetHashCode", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("GetHashCode", typeof (MyPoll), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_04f7a46397b74888a5c780ef6e1a29b5.MTHD_51b3a6772dfb4e19a4726ee54924efe5()
        }));
        this.AddMember("GetType", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("GetType", typeof (MyPoll), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_04f7a46397b74888a5c780ef6e1a29b5.MTHD_458bbc173c204a84af06158a16d49735()
        }));
        this.AddMember("name", (IMemberDescriptor) new Bridge.TYPE_04f7a46397b74888a5c780ef6e1a29b5.FLDV_3aca8ee4c5544098bd482ff5779d74d6());
        this.AddMember("id", (IMemberDescriptor) new Bridge.TYPE_04f7a46397b74888a5c780ef6e1a29b5.FLDV_82c4b355b4e742eb86e01dcf100fad75());
        this.AddMember("items", (IMemberDescriptor) new Bridge.TYPE_04f7a46397b74888a5c780ef6e1a29b5.FLDV_6f00587ec2654ae4ac837c0f18949e98());
        this.AddMember("isPublic", (IMemberDescriptor) new Bridge.TYPE_04f7a46397b74888a5c780ef6e1a29b5.FLDV_6d2865af53564196928646ec6ef55909());
        this.AddMember("MsgRetrieve", (IMemberDescriptor) new Bridge.TYPE_04f7a46397b74888a5c780ef6e1a29b5.FLDV_6f33b5fb196d4c19ae378eaddbade14d());
        this.AddMember("MsgSubmit", (IMemberDescriptor) new Bridge.TYPE_04f7a46397b74888a5c780ef6e1a29b5.FLDV_100a46c9af0743089fccdb483b40ce05());
        this.AddMember("MsgCreate", (IMemberDescriptor) new Bridge.TYPE_04f7a46397b74888a5c780ef6e1a29b5.FLDV_15ac7b5da2e94fed8c2d00799f81d423());
        this.AddMember("MsgResults", (IMemberDescriptor) new Bridge.TYPE_04f7a46397b74888a5c780ef6e1a29b5.FLDV_a258336608014d7bb1f272a9fe06deff());
        this.AddMember("Item", (IMemberDescriptor) new Bridge.TYPE_04f7a46397b74888a5c780ef6e1a29b5.DVAL_464bf1eda1204a78bf51f0ee5878b634());
        this.AddMember("Answer", (IMemberDescriptor) new Bridge.TYPE_04f7a46397b74888a5c780ef6e1a29b5.DVAL_f4205f95d82c4ac78590132ddb887044());
      }

      private sealed class MTHD_83d2f5ed369943b78a4a62b6e5498c06 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_83d2f5ed369943b78a4a62b6e5498c06()
        {
          this.Initialize(".ctor", true, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) new MyPoll();
        }
      }

      private sealed class MTHD_646b030baf574a7e860e590485361c1a : HardwiredMethodMemberDescriptor
      {
        internal MTHD_646b030baf574a7e860e590485361c1a()
        {
          this.Initialize("GetQuestionsAsString", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((MyPoll) obj).GetQuestionsAsString();
        }
      }

      private sealed class MTHD_7ce66ebfbb2d418e8512547d8d25ad9d : HardwiredMethodMemberDescriptor
      {
        internal MTHD_7ce66ebfbb2d418e8512547d8d25ad9d()
        {
          this.Initialize("SanitizeResponses", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          ((MyPoll) obj).SanitizeResponses();
          return (object) DynValue.Void;
        }
      }

      private sealed class MTHD_3e10181122274209beb77bd9a4e94b96 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_3e10181122274209beb77bd9a4e94b96()
        {
          this.Initialize("addItem", false, new ParameterDescriptor[3]
          {
            new ParameterDescriptor("question", typeof (string)),
            new ParameterDescriptor("multipleSelection", typeof (bool), true, (object) new DefaultValue()),
            new ParameterDescriptor("requireAnswer", typeof (bool), true, (object) new DefaultValue())
          }, false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          if (argscount <= 1)
            return (object) ((MyPoll) obj).addItem((string) pars[0]);
          return argscount <= 2 ? (object) ((MyPoll) obj).addItem((string) pars[0], (bool) pars[1]) : (object) ((MyPoll) obj).addItem((string) pars[0], (bool) pars[1], (bool) pars[2]);
        }
      }

      private sealed class MTHD_d6306f383de04163909c9774bdf7d255 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_d6306f383de04163909c9774bdf7d255()
        {
          this.Initialize("construct", true, new ParameterDescriptor[1]
          {
            new ParameterDescriptor("name", typeof (string))
          }, false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) MyPoll.construct((string) pars[0]);
        }
      }

      private sealed class MTHD_1794cc61b69848f08ee28da96159cacd : HardwiredMethodMemberDescriptor
      {
        internal MTHD_1794cc61b69848f08ee28da96159cacd()
        {
          this.Initialize("ToString", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((MyPoll) obj).ToString();
        }
      }

      private sealed class MTHD_ac270e37346f46d48fd7c7f15f95a670 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_ac270e37346f46d48fd7c7f15f95a670()
        {
          this.Initialize("fromString", true, new ParameterDescriptor[1]
          {
            new ParameterDescriptor("s", typeof (string))
          }, false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) MyPoll.fromString((string) pars[0]);
        }
      }

      private sealed class MTHD_050b7f3a37f34e47893fa54a17d20399 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_050b7f3a37f34e47893fa54a17d20399()
        {
          this.Initialize("sendToServer", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          ((MyPoll) obj).sendToServer();
          return (object) DynValue.Void;
        }
      }

      private sealed class MTHD_4ca26011180346afbe9b4be0ce5d8855 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_4ca26011180346afbe9b4be0ce5d8855()
        {
          this.Initialize("closePoll", true, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          MyPoll.closePoll();
          return (object) DynValue.Void;
        }
      }

      private sealed class MTHD_5c4a508c4ce64a58bab27be3519f6aff : HardwiredMethodMemberDescriptor
      {
        internal MTHD_5c4a508c4ce64a58bab27be3519f6aff()
        {
          this.Initialize("test", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          ((MyPoll) obj).test();
          return (object) DynValue.Void;
        }
      }

      private sealed class MTHD_849f6b7e22b840d28881a79cf8b98b7c : HardwiredMethodMemberDescriptor
      {
        internal MTHD_849f6b7e22b840d28881a79cf8b98b7c()
        {
          this.Initialize("Equals", false, new ParameterDescriptor[1]
          {
            new ParameterDescriptor("obj", typeof (object))
          }, false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) obj.Equals(pars[0]);
        }
      }

      private sealed class MTHD_51b3a6772dfb4e19a4726ee54924efe5 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_51b3a6772dfb4e19a4726ee54924efe5()
        {
          this.Initialize("GetHashCode", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) obj.GetHashCode();
        }
      }

      private sealed class MTHD_458bbc173c204a84af06158a16d49735 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_458bbc173c204a84af06158a16d49735()
        {
          this.Initialize("GetType", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) obj.GetType();
        }
      }

      private sealed class FLDV_3aca8ee4c5544098bd482ff5779d74d6 : HardwiredMemberDescriptor
      {
        internal FLDV_3aca8ee4c5544098bd482ff5779d74d6()
          : base(typeof (string), "name", false, MemberDescriptorAccess.CanRead | MemberDescriptorAccess.CanWrite)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((MyPoll) obj).name;
        }

        protected override void SetValueImpl(Script script, object obj, object value)
        {
          ((MyPoll) obj).name = (string) value;
        }
      }

      private sealed class FLDV_82c4b355b4e742eb86e01dcf100fad75 : HardwiredMemberDescriptor
      {
        internal FLDV_82c4b355b4e742eb86e01dcf100fad75()
          : base(typeof (int), "id", false, MemberDescriptorAccess.CanRead | MemberDescriptorAccess.CanWrite)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((MyPoll) obj).id;
        }

        protected override void SetValueImpl(Script script, object obj, object value)
        {
          ((MyPoll) obj).id = (int) value;
        }
      }

      private sealed class FLDV_6f00587ec2654ae4ac837c0f18949e98 : HardwiredMemberDescriptor
      {
        internal FLDV_6f00587ec2654ae4ac837c0f18949e98()
          : base(typeof (List<MyPoll.Item>), "items", false, MemberDescriptorAccess.CanRead | MemberDescriptorAccess.CanWrite)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((MyPoll) obj).items;
        }

        protected override void SetValueImpl(Script script, object obj, object value)
        {
          ((MyPoll) obj).items = (List<MyPoll.Item>) value;
        }
      }

      private sealed class FLDV_6d2865af53564196928646ec6ef55909 : HardwiredMemberDescriptor
      {
        internal FLDV_6d2865af53564196928646ec6ef55909()
          : base(typeof (bool), "isPublic", false, MemberDescriptorAccess.CanRead | MemberDescriptorAccess.CanWrite)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((MyPoll) obj).isPublic;
        }

        protected override void SetValueImpl(Script script, object obj, object value)
        {
          ((MyPoll) obj).isPublic = (bool) value;
        }
      }

      private sealed class FLDV_6f33b5fb196d4c19ae378eaddbade14d : HardwiredMemberDescriptor
      {
        internal FLDV_6f33b5fb196d4c19ae378eaddbade14d()
          : base(typeof (byte), "MsgRetrieve", true, MemberDescriptorAccess.CanRead)
        {
        }

        protected override object GetValueImpl(Script script, object obj) => (object) (byte) 1;
      }

      private sealed class FLDV_100a46c9af0743089fccdb483b40ce05 : HardwiredMemberDescriptor
      {
        internal FLDV_100a46c9af0743089fccdb483b40ce05()
          : base(typeof (byte), "MsgSubmit", true, MemberDescriptorAccess.CanRead)
        {
        }

        protected override object GetValueImpl(Script script, object obj) => (object) (byte) 2;
      }

      private sealed class FLDV_15ac7b5da2e94fed8c2d00799f81d423 : HardwiredMemberDescriptor
      {
        internal FLDV_15ac7b5da2e94fed8c2d00799f81d423()
          : base(typeof (byte), "MsgCreate", true, MemberDescriptorAccess.CanRead)
        {
        }

        protected override object GetValueImpl(Script script, object obj) => (object) (byte) 3;
      }

      private sealed class FLDV_a258336608014d7bb1f272a9fe06deff : HardwiredMemberDescriptor
      {
        internal FLDV_a258336608014d7bb1f272a9fe06deff()
          : base(typeof (byte), "MsgResults", true, MemberDescriptorAccess.CanRead)
        {
        }

        protected override object GetValueImpl(Script script, object obj) => (object) (byte) 4;
      }

      private sealed class DVAL_464bf1eda1204a78bf51f0ee5878b634 : DynValueMemberDescriptor
      {
        internal DVAL_464bf1eda1204a78bf51f0ee5878b634()
          : base("Item")
        {
        }

        public override DynValue Value => UserData.CreateStatic(typeof (MyPoll.Item));
      }

      private sealed class DVAL_f4205f95d82c4ac78590132ddb887044 : DynValueMemberDescriptor
      {
        internal DVAL_f4205f95d82c4ac78590132ddb887044()
          : base("Answer")
        {
        }

        public override DynValue Value => UserData.CreateStatic(typeof (MyPoll.Answer));
      }
    }

    private sealed class TYPE_f9eba7dcf93540dc8b586654c78e3e0e : HardwiredUserDataDescriptor
    {
      internal TYPE_f9eba7dcf93540dc8b586654c78e3e0e()
        : base(typeof (LuaColor))
      {
        this.AddMember("__new", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("__new", typeof (LuaColor), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_f9eba7dcf93540dc8b586654c78e3e0e.MTHD_3495274b79dd4eefbb62262652cc1ea5()
        }));
        this.AddMember("toHex", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("toHex", typeof (LuaColor), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_f9eba7dcf93540dc8b586654c78e3e0e.MTHD_41a45358ba8f4be6aa24a5d6549846ae()
        }));
        this.AddMember("construct", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("construct", typeof (LuaColor), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[2]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_f9eba7dcf93540dc8b586654c78e3e0e.MTHD_e24f6220d5114592bc662c83da9d8cf3(),
          (IOverloadableMemberDescriptor) new Bridge.TYPE_f9eba7dcf93540dc8b586654c78e3e0e.MTHD_d4dedaa0ab7c4fffbc37491ba96867f7()
        }));
        this.AddMember("From", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("From", typeof (LuaColor), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[2]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_f9eba7dcf93540dc8b586654c78e3e0e.MTHD_aa2b93131aa24775983ff3e97a0c34e5(),
          (IOverloadableMemberDescriptor) new Bridge.TYPE_f9eba7dcf93540dc8b586654c78e3e0e.MTHD_d5de7f9c0fb64f55b7a9d9d531e95c94()
        }));
        this.AddMember("Equals", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("Equals", typeof (LuaColor), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_f9eba7dcf93540dc8b586654c78e3e0e.MTHD_5d6218898b7645a79a3b884153822f37()
        }));
        this.AddMember("GetHashCode", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("GetHashCode", typeof (LuaColor), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_f9eba7dcf93540dc8b586654c78e3e0e.MTHD_44887ba5c50444e9a9648edad471ee15()
        }));
        this.AddMember("GetType", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("GetType", typeof (LuaColor), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_f9eba7dcf93540dc8b586654c78e3e0e.MTHD_7c2f60d259e3493ba2c47dddccea4be9()
        }));
        this.AddMember("ToString", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("ToString", typeof (LuaColor), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_f9eba7dcf93540dc8b586654c78e3e0e.MTHD_41718d714ddd49bf9d026f755a10c492()
        }));
        this.AddMember("r", (IMemberDescriptor) new Bridge.TYPE_f9eba7dcf93540dc8b586654c78e3e0e.FLDV_2730cc58286945b48dead4aed210aa08());
        this.AddMember("g", (IMemberDescriptor) new Bridge.TYPE_f9eba7dcf93540dc8b586654c78e3e0e.FLDV_02a4c42dfa1f4f918d047aeaef4d7f8a());
        this.AddMember("b", (IMemberDescriptor) new Bridge.TYPE_f9eba7dcf93540dc8b586654c78e3e0e.FLDV_5010437e3fb74d88856cd6d7cb12685c());
        this.AddMember("a", (IMemberDescriptor) new Bridge.TYPE_f9eba7dcf93540dc8b586654c78e3e0e.FLDV_c7bd85c9d73948d49f16d369736db3e7());
      }

      private sealed class MTHD_3495274b79dd4eefbb62262652cc1ea5 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_3495274b79dd4eefbb62262652cc1ea5()
        {
          this.Initialize(".ctor", true, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) new LuaColor();
        }
      }

      private sealed class MTHD_41a45358ba8f4be6aa24a5d6549846ae : HardwiredMethodMemberDescriptor
      {
        internal MTHD_41a45358ba8f4be6aa24a5d6549846ae()
        {
          this.Initialize("toHex", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((LuaColor) obj).toHex();
        }
      }

      private sealed class MTHD_e24f6220d5114592bc662c83da9d8cf3 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_e24f6220d5114592bc662c83da9d8cf3()
        {
          this.Initialize("construct", true, new ParameterDescriptor[4]
          {
            new ParameterDescriptor("r", typeof (byte)),
            new ParameterDescriptor("g", typeof (byte)),
            new ParameterDescriptor("b", typeof (byte)),
            new ParameterDescriptor("a", typeof (byte))
          }, false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) LuaColor.construct((byte) pars[0], (byte) pars[1], (byte) pars[2], (byte) pars[3]);
        }
      }

      private sealed class MTHD_d4dedaa0ab7c4fffbc37491ba96867f7 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_d4dedaa0ab7c4fffbc37491ba96867f7()
        {
          this.Initialize("construct", true, new ParameterDescriptor[1]
          {
            new ParameterDescriptor("hex", typeof (string))
          }, false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) LuaColor.construct((string) pars[0]);
        }
      }

      private sealed class MTHD_aa2b93131aa24775983ff3e97a0c34e5 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_aa2b93131aa24775983ff3e97a0c34e5()
        {
          this.Initialize("From", true, new ParameterDescriptor[1]
          {
            new ParameterDescriptor("c", typeof (Color32))
          }, false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) LuaColor.From((Color32) pars[0]);
        }
      }

      private sealed class MTHD_d5de7f9c0fb64f55b7a9d9d531e95c94 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_d5de7f9c0fb64f55b7a9d9d531e95c94()
        {
          this.Initialize("From", true, new ParameterDescriptor[1]
          {
            new ParameterDescriptor("c", typeof (LuaColor))
          }, false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) LuaColor.From((LuaColor) pars[0]);
        }
      }

      private sealed class MTHD_5d6218898b7645a79a3b884153822f37 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_5d6218898b7645a79a3b884153822f37()
        {
          this.Initialize("Equals", false, new ParameterDescriptor[1]
          {
            new ParameterDescriptor("obj", typeof (object))
          }, false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) obj.Equals(pars[0]);
        }
      }

      private sealed class MTHD_44887ba5c50444e9a9648edad471ee15 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_44887ba5c50444e9a9648edad471ee15()
        {
          this.Initialize("GetHashCode", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) obj.GetHashCode();
        }
      }

      private sealed class MTHD_7c2f60d259e3493ba2c47dddccea4be9 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_7c2f60d259e3493ba2c47dddccea4be9()
        {
          this.Initialize("GetType", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) obj.GetType();
        }
      }

      private sealed class MTHD_41718d714ddd49bf9d026f755a10c492 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_41718d714ddd49bf9d026f755a10c492()
        {
          this.Initialize("ToString", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) obj.ToString();
        }
      }

      private sealed class FLDV_2730cc58286945b48dead4aed210aa08 : HardwiredMemberDescriptor
      {
        internal FLDV_2730cc58286945b48dead4aed210aa08()
          : base(typeof (byte), "r", false, MemberDescriptorAccess.CanRead | MemberDescriptorAccess.CanWrite)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((LuaColor) obj).r;
        }

        protected override void SetValueImpl(Script script, object obj, object value)
        {
          ((LuaColor) obj).r = (byte) value;
        }
      }

      private sealed class FLDV_02a4c42dfa1f4f918d047aeaef4d7f8a : HardwiredMemberDescriptor
      {
        internal FLDV_02a4c42dfa1f4f918d047aeaef4d7f8a()
          : base(typeof (byte), "g", false, MemberDescriptorAccess.CanRead | MemberDescriptorAccess.CanWrite)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((LuaColor) obj).g;
        }

        protected override void SetValueImpl(Script script, object obj, object value)
        {
          ((LuaColor) obj).g = (byte) value;
        }
      }

      private sealed class FLDV_5010437e3fb74d88856cd6d7cb12685c : HardwiredMemberDescriptor
      {
        internal FLDV_5010437e3fb74d88856cd6d7cb12685c()
          : base(typeof (byte), "b", false, MemberDescriptorAccess.CanRead | MemberDescriptorAccess.CanWrite)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((LuaColor) obj).b;
        }

        protected override void SetValueImpl(Script script, object obj, object value)
        {
          ((LuaColor) obj).b = (byte) value;
        }
      }

      private sealed class FLDV_c7bd85c9d73948d49f16d369736db3e7 : HardwiredMemberDescriptor
      {
        internal FLDV_c7bd85c9d73948d49f16d369736db3e7()
          : base(typeof (byte), "a", false, MemberDescriptorAccess.CanRead | MemberDescriptorAccess.CanWrite)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((LuaColor) obj).a;
        }

        protected override void SetValueImpl(Script script, object obj, object value)
        {
          ((LuaColor) obj).a = (byte) value;
        }
      }
    }
  }
}

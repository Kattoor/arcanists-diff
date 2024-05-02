

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
      UserData.RegisterType((IUserDataDescriptor) new Bridge.TYPE_df374f602d28478db5f0ee37fae1ac37());
      UserData.RegisterType((IUserDataDescriptor) new Bridge.TYPE_f1ecff994a454a7b982cb7976a758ebd());
      UserData.RegisterType((IUserDataDescriptor) new Bridge.TYPE_fbef48a9042141c2b33d0fe295021fba());
      UserData.RegisterType((IUserDataDescriptor) new Bridge.TYPE_0d6c47a182c1466da53be9def40de094());
      UserData.RegisterType((IUserDataDescriptor) new Bridge.TYPE_d4713a751cdb42afade7a7955a883875());
      UserData.RegisterType((IUserDataDescriptor) new Bridge.TYPE_2b55a2dc2b96473184f157b103f346a2());
      UserData.RegisterType((IUserDataDescriptor) new Bridge.TYPE_2cc268fff90f4c3aa2d0a9bd2a92ee18());
      UserData.RegisterType((IUserDataDescriptor) new Bridge.TYPE_1c0f540c44204c3fa838f7f5d5b02290());
      UserData.RegisterType((IUserDataDescriptor) new Bridge.TYPE_5e7de831bd6e4560964114c8b777928e());
      UserData.RegisterType((IUserDataDescriptor) new Bridge.TYPE_b3e8f0bef7044e8ea2e318068adb7316());
      UserData.RegisterType((IUserDataDescriptor) new Bridge.TYPE_0cfafa9d829143359759f8bd8378a201());
      UserData.RegisterType((IUserDataDescriptor) new Bridge.TYPE_7268ac70a9894056b228131cf98b9520());
      UserData.RegisterType((IUserDataDescriptor) new Bridge.TYPE_8d99cb16d56c42c1bd10716ce12bcbc1());
      UserData.RegisterType((IUserDataDescriptor) new Bridge.TYPE_cc81db70c47a48568088483aa2925801());
    }

    private sealed class TYPE_df374f602d28478db5f0ee37fae1ac37 : HardwiredUserDataDescriptor
    {
      internal TYPE_df374f602d28478db5f0ee37fae1ac37()
        : base(typeof (ContainerGame))
      {
        this.AddMember("getUser", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("getUser", typeof (ContainerGame), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_df374f602d28478db5f0ee37fae1ac37.MTHD_5258b84407084cfcabb2bad6342349e3()
        }));
        this.AddMember("getPlayers", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("getPlayers", typeof (ContainerGame), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_df374f602d28478db5f0ee37fae1ac37.MTHD_487997e85d1640c289e7a54e296a9cb3()
        }));
        this.AddMember("getPlayerCount", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("getPlayerCount", typeof (ContainerGame), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_df374f602d28478db5f0ee37fae1ac37.MTHD_a5549187fee84c2e9a25aa7012a48199()
        }));
        this.AddMember("getCreatures", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("getCreatures", typeof (ContainerGame), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_df374f602d28478db5f0ee37fae1ac37.MTHD_e20af6ff79884292b42f48a5d7de2f30()
        }));
        this.AddMember("findCreatures", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("findCreatures", typeof (ContainerGame), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_df374f602d28478db5f0ee37fae1ac37.MTHD_d51b4eaa67f54a02baad3c5d5e4b74e5()
        }));
        this.AddMember("findEffectors", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("findEffectors", typeof (ContainerGame), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_df374f602d28478db5f0ee37fae1ac37.MTHD_3013086fc1cc44fc8390305f8290ae60()
        }));
        this.AddMember("LineCast", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("LineCast", typeof (ContainerGame), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_df374f602d28478db5f0ee37fae1ac37.MTHD_744d5c4d237b498b9084b4b6823e9f57()
        }));
        this.AddMember("LineCastOnlyCreatures", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("LineCastOnlyCreatures", typeof (ContainerGame), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_df374f602d28478db5f0ee37fae1ac37.MTHD_dc66095371154dc2811c293f445e59ee()
        }));
        this.AddMember("LineCastOnlyTerrain", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("LineCastOnlyTerrain", typeof (ContainerGame), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_df374f602d28478db5f0ee37fae1ac37.MTHD_ad77983bf7b6406eae2c8ad12d8e30c1()
        }));
        this.AddMember("ShowInfo", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("ShowInfo", typeof (ContainerGame), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_df374f602d28478db5f0ee37fae1ac37.MTHD_87e5782a66e245018a9f05cc6e075fac()
        }));
        this.AddMember("get_turn", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_turn", typeof (ContainerGame), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_df374f602d28478db5f0ee37fae1ac37.MTHD_58a698cd2f2e4ed88b65872772efca20()
        }));
        this.AddMember("set_turn", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("set_turn", typeof (ContainerGame), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_df374f602d28478db5f0ee37fae1ac37.MTHD_5b7a933a380b4af980140524c27eef7a()
        }));
        this.AddMember("get_timeLimit", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_timeLimit", typeof (ContainerGame), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_df374f602d28478db5f0ee37fae1ac37.MTHD_a563a77999724060b35e103d53de8841()
        }));
        this.AddMember("set_timeLimit", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("set_timeLimit", typeof (ContainerGame), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_df374f602d28478db5f0ee37fae1ac37.MTHD_56e99bdb9b524667b85c78b56f381c17()
        }));
        this.AddMember("get_totalTimeElapsed", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_totalTimeElapsed", typeof (ContainerGame), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_df374f602d28478db5f0ee37fae1ac37.MTHD_cbe99221e29f498881fd33e60fe92dc1()
        }));
        this.AddMember("get_turnTime", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_turnTime", typeof (ContainerGame), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_df374f602d28478db5f0ee37fae1ac37.MTHD_030f6a3b928b458f83ddbad7d57eb0c8()
        }));
        this.AddMember("get_frame", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_frame", typeof (ContainerGame), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_df374f602d28478db5f0ee37fae1ac37.MTHD_849c3d77795f4b629cf7efa72b96e09e()
        }));
        this.AddMember("random", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("random", typeof (ContainerGame), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_df374f602d28478db5f0ee37fae1ac37.MTHD_ed1428540c494d2e8570381c2a62b382()
        }));
        this.AddMember("get_winOnDeath", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_winOnDeath", typeof (ContainerGame), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_df374f602d28478db5f0ee37fae1ac37.MTHD_fed21a93a52244c5be333453bd19eb52()
        }));
        this.AddMember("set_winOnDeath", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("set_winOnDeath", typeof (ContainerGame), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_df374f602d28478db5f0ee37fae1ac37.MTHD_22150297dbaf47f6b55e5967a5d2d00b()
        }));
        this.AddMember("get_allowMovement", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_allowMovement", typeof (ContainerGame), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_df374f602d28478db5f0ee37fae1ac37.MTHD_b0d97ceda9164770999719ef1b0bf7d5()
        }));
        this.AddMember("set_allowMovement", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("set_allowMovement", typeof (ContainerGame), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_df374f602d28478db5f0ee37fae1ac37.MTHD_5c53dc2dbaaf4cc4a4d5d988ceaebcb7()
        }));
        this.AddMember("get_allowInput", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_allowInput", typeof (ContainerGame), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_df374f602d28478db5f0ee37fae1ac37.MTHD_612e92af8e274ec88335ce6259900e85()
        }));
        this.AddMember("set_allowInput", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("set_allowInput", typeof (ContainerGame), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_df374f602d28478db5f0ee37fae1ac37.MTHD_936fadbbc642404fbcb078b882633239()
        }));
        this.AddMember("get_allowCallbacks", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_allowCallbacks", typeof (ContainerGame), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_df374f602d28478db5f0ee37fae1ac37.MTHD_becac51f2dc444d8bd89fa4f5f157efb()
        }));
        this.AddMember("set_allowCallbacks", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("set_allowCallbacks", typeof (ContainerGame), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_df374f602d28478db5f0ee37fae1ac37.MTHD_f7e8186af1db44b99fb5dbc1ba5f3749()
        }));
        this.AddMember("get_allowSkipTurn", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_allowSkipTurn", typeof (ContainerGame), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_df374f602d28478db5f0ee37fae1ac37.MTHD_830db6e6219d484484e206c424a949bc()
        }));
        this.AddMember("set_allowSkipTurn", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("set_allowSkipTurn", typeof (ContainerGame), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_df374f602d28478db5f0ee37fae1ac37.MTHD_1cc282a1b31e4bdb9476812009df13d1()
        }));
        this.AddMember("get_terrainDestruction", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_terrainDestruction", typeof (ContainerGame), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_df374f602d28478db5f0ee37fae1ac37.MTHD_386db306b8b240119a567c77b769d9e4()
        }));
        this.AddMember("set_terrainDestruction", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("set_terrainDestruction", typeof (ContainerGame), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_df374f602d28478db5f0ee37fae1ac37.MTHD_3e979d84e2f744f0ab3b65af86ea64ec()
        }));
        this.AddMember("get_takeDamage", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_takeDamage", typeof (ContainerGame), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_df374f602d28478db5f0ee37fae1ac37.MTHD_d6a440fc51a946fd85d96228dd743097()
        }));
        this.AddMember("set_takeDamage", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("set_takeDamage", typeof (ContainerGame), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_df374f602d28478db5f0ee37fae1ac37.MTHD_5c6c67c830c443e3bb9fda31553d344b()
        }));
        this.AddMember("get_armageddon", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_armageddon", typeof (ContainerGame), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_df374f602d28478db5f0ee37fae1ac37.MTHD_186190ccb6a4428591191d5473af034f()
        }));
        this.AddMember("set_armageddon", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("set_armageddon", typeof (ContainerGame), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_df374f602d28478db5f0ee37fae1ac37.MTHD_142e411ca3c244ebaa7c7b80e8d30d00()
        }));
        this.AddMember("get_armageddonTurn", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_armageddonTurn", typeof (ContainerGame), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_df374f602d28478db5f0ee37fae1ac37.MTHD_dc78334274db4b0b811cce5e00f2584d()
        }));
        this.AddMember("set_armageddonTurn", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("set_armageddonTurn", typeof (ContainerGame), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_df374f602d28478db5f0ee37fae1ac37.MTHD_a3f96e920b584b3383c0f5d6da4a2761()
        }));
        this.AddMember("get_waiting", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_waiting", typeof (ContainerGame), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_df374f602d28478db5f0ee37fae1ac37.MTHD_2e8d98ef02a74948a496b2a70a3f551d()
        }));
        this.AddMember("get_paused", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_paused", typeof (ContainerGame), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_df374f602d28478db5f0ee37fae1ac37.MTHD_c014253c37a546c7a97b875cb8b53b7c()
        }));
        this.AddMember("set_paused", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("set_paused", typeof (ContainerGame), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_df374f602d28478db5f0ee37fae1ac37.MTHD_abf6caec072c436594a85dac04827b47()
        }));
        this.AddMember("get_busy", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_busy", typeof (ContainerGame), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_df374f602d28478db5f0ee37fae1ac37.MTHD_ab242d76b4284e40b25152fb9f0877b3()
        }));
        this.AddMember("get_ongoing", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_ongoing", typeof (ContainerGame), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_df374f602d28478db5f0ee37fae1ac37.MTHD_18f5f936fec84585b02cdccfc55eb25a()
        }));
        this.AddMember("get_gravity", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_gravity", typeof (ContainerGame), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_df374f602d28478db5f0ee37fae1ac37.MTHD_f3c559bbb6384df3919dbe45c4119f6e()
        }));
        this.AddMember("set_gravity", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("set_gravity", typeof (ContainerGame), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_df374f602d28478db5f0ee37fae1ac37.MTHD_d4dbf2f042064c63bd9c447ffa41cec6()
        }));
        this.AddMember("get_wind", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_wind", typeof (ContainerGame), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_df374f602d28478db5f0ee37fae1ac37.MTHD_799324f47e2b4f80bf3e82c5a17be70c()
        }));
        this.AddMember("set_wind", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("set_wind", typeof (ContainerGame), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_df374f602d28478db5f0ee37fae1ac37.MTHD_5bf5d36dc23b4181978d3f38b6236dbb()
        }));
        this.AddMember("get_windDir", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_windDir", typeof (ContainerGame), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_df374f602d28478db5f0ee37fae1ac37.MTHD_d936cac515924d78967f653f46abf397()
        }));
        this.AddMember("set_windDir", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("set_windDir", typeof (ContainerGame), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_df374f602d28478db5f0ee37fae1ac37.MTHD_abace182bdbf452f929d3edcb952f515()
        }));
        this.AddMember("get_windPower", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_windPower", typeof (ContainerGame), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_df374f602d28478db5f0ee37fae1ac37.MTHD_d0ff7bc467a14b03b71bac5d7b9e96f0()
        }));
        this.AddMember("set_windPower", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("set_windPower", typeof (ContainerGame), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_df374f602d28478db5f0ee37fae1ac37.MTHD_2b77f9275d6e431c93e1faa61e0dd242()
        }));
        this.AddMember("get_width", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_width", typeof (ContainerGame), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_df374f602d28478db5f0ee37fae1ac37.MTHD_136d989373764a79b48523b641ad959e()
        }));
        this.AddMember("get_height", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_height", typeof (ContainerGame), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_df374f602d28478db5f0ee37fae1ac37.MTHD_9e1d6da04dba49fb8dfea51fa7bdc060()
        }));
        this.AddMember("get_mousePosition", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_mousePosition", typeof (ContainerGame), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_df374f602d28478db5f0ee37fae1ac37.MTHD_a6b8c228a7154d6085c2dc5fa9669d67()
        }));
        this.AddMember("get_mouseOverUI", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_mouseOverUI", typeof (ContainerGame), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_df374f602d28478db5f0ee37fae1ac37.MTHD_42f356b988e845c48f32e66b40b419b7()
        }));
        this.AddMember("worldToCanvas", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("worldToCanvas", typeof (ContainerGame), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_df374f602d28478db5f0ee37fae1ac37.MTHD_4262f85e723749208c7717f8c054a98b()
        }));
        this.AddMember("canvasToWorld", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("canvasToWorld", typeof (ContainerGame), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_df374f602d28478db5f0ee37fae1ac37.MTHD_144fafdc8ece482292208490c7360718()
        }));
        this.AddMember("get_cameraPosition", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_cameraPosition", typeof (ContainerGame), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_df374f602d28478db5f0ee37fae1ac37.MTHD_471bef3a844d456a801d6ed3973334e6()
        }));
        this.AddMember("set_cameraPosition", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("set_cameraPosition", typeof (ContainerGame), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_df374f602d28478db5f0ee37fae1ac37.MTHD_afccd40078ca419f9cd059b90d644be8()
        }));
        this.AddMember("get_cameraZoom", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_cameraZoom", typeof (ContainerGame), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_df374f602d28478db5f0ee37fae1ac37.MTHD_5341baa5aecc4a6d85e51249b7afe2e5()
        }));
        this.AddMember("set_cameraZoom", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("set_cameraZoom", typeof (ContainerGame), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_df374f602d28478db5f0ee37fae1ac37.MTHD_bfdf7ddb084e4bae84bfdc5ab0eab365()
        }));
        this.AddMember("getMapPixel", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("getMapPixel", typeof (ContainerGame), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_df374f602d28478db5f0ee37fae1ac37.MTHD_1f5984389d1e411bab339817b572fee0()
        }));
        this.AddMember("setMapPixel", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("setMapPixel", typeof (ContainerGame), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_df374f602d28478db5f0ee37fae1ac37.MTHD_04794890632047a5b4184c495e28dc96()
        }));
        this.AddMember("drawRectangle", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("drawRectangle", typeof (ContainerGame), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_df374f602d28478db5f0ee37fae1ac37.MTHD_b3571dffe07e41cfad7ed937fb7b3958()
        }));
        this.AddMember("drawEllipseOutline", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("drawEllipseOutline", typeof (ContainerGame), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_df374f602d28478db5f0ee37fae1ac37.MTHD_1fd168ef84434632ab70d40e38c09222()
        }));
        this.AddMember("drawEllipse", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("drawEllipse", typeof (ContainerGame), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_df374f602d28478db5f0ee37fae1ac37.MTHD_28c65bf79f204cfdaaf0f47a48d4b57f()
        }));
        this.AddMember("blit", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("blit", typeof (ContainerGame), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_df374f602d28478db5f0ee37fae1ac37.MTHD_924f279c024949ad8393cd3d5d4fc0ec()
        }));
        this.AddMember("blitRotate", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("blitRotate", typeof (ContainerGame), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_df374f602d28478db5f0ee37fae1ac37.MTHD_43d9f7d3a54a4e299bc6c2015666ac7a()
        }));
        this.AddMember("applyDraw", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("applyDraw", typeof (ContainerGame), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_df374f602d28478db5f0ee37fae1ac37.MTHD_8e94ec9ef013461ca9fd4e0e5f7f6dca()
        }));
        this.AddMember("get_allowBounce", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_allowBounce", typeof (ContainerGame), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_df374f602d28478db5f0ee37fae1ac37.MTHD_ab7db456aa354cb48d0cebe6e1d904dc()
        }));
        this.AddMember("set_allowBounce", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("set_allowBounce", typeof (ContainerGame), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_df374f602d28478db5f0ee37fae1ac37.MTHD_54877ce2e7b145ffbd6cc15477d07bb5()
        }));
        this.AddMember("get_isUsingTouchControls", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_isUsingTouchControls", typeof (ContainerGame), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_df374f602d28478db5f0ee37fae1ac37.MTHD_89385d12005841b7a07df7cdfe66f9d8()
        }));
        this.AddMember("clientRefreshSpells", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("clientRefreshSpells", typeof (ContainerGame), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_df374f602d28478db5f0ee37fae1ac37.MTHD_c69f2dc436e446b2a7b327212afcae64()
        }));
        this.AddMember("startCoroutine", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("startCoroutine", typeof (ContainerGame), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_df374f602d28478db5f0ee37fae1ac37.MTHD_38dce31134254cca8fd03e1fffee11bb()
        }));
        this.AddMember("devCommand", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("devCommand", typeof (ContainerGame), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_df374f602d28478db5f0ee37fae1ac37.MTHD_3aa32660856d4e928118197d75346fce()
        }));
        this.AddMember("win", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("win", typeof (ContainerGame), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_df374f602d28478db5f0ee37fae1ac37.MTHD_e2560c0d1675413c9053cd5bece54128()
        }));
        this.AddMember("lose", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("lose", typeof (ContainerGame), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_df374f602d28478db5f0ee37fae1ac37.MTHD_5916b60a53dc4fa280531c9ef08ed051()
        }));
        this.AddMember("nextTurn", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("nextTurn", typeof (ContainerGame), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_df374f602d28478db5f0ee37fae1ac37.MTHD_45c8fa0b3c5848e080a1a3a403708b80()
        }));
        this.AddMember("resetMap", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("resetMap", typeof (ContainerGame), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_df374f602d28478db5f0ee37fae1ac37.MTHD_18c3a9ff47c249f0b15490b2b32c9063()
        }));
        this.AddMember("clearMap", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("clearMap", typeof (ContainerGame), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_df374f602d28478db5f0ee37fae1ac37.MTHD_30cd147adef44be0985c7ef46a27ae9f()
        }));
        this.AddMember("getSpell", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("getSpell", typeof (ContainerGame), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_df374f602d28478db5f0ee37fae1ac37.MTHD_2249b92f911e479abef961c82a9beff8()
        }));
        this.AddMember("getSpellEnum", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("getSpellEnum", typeof (ContainerGame), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_df374f602d28478db5f0ee37fae1ac37.MTHD_eba7fffe3ddf46c39bd41aefb6d74ead()
        }));
        this.AddMember("getSpellName", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("getSpellName", typeof (ContainerGame), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_df374f602d28478db5f0ee37fae1ac37.MTHD_bebb563e8aff43deaab11bcc37b17289()
        }));
        this.AddMember("getMapID", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("getMapID", typeof (ContainerGame), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_df374f602d28478db5f0ee37fae1ac37.MTHD_517a65e4ef354529b12cebd12f60e31b()
        }));
        this.AddMember("getMapRealName", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("getMapRealName", typeof (ContainerGame), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_df374f602d28478db5f0ee37fae1ac37.MTHD_968bc74bae354b028c1b64228f5477cd()
        }));
        this.AddMember("getMapShortName", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("getMapShortName", typeof (ContainerGame), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_df374f602d28478db5f0ee37fae1ac37.MTHD_c9a41623f13d48a8a7a44b0b63a07c97()
        }));
        this.AddMember("getArmageddonName", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("getArmageddonName", typeof (ContainerGame), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_df374f602d28478db5f0ee37fae1ac37.MTHD_c36028a649694495917e1078d375754d()
        }));
        this.AddMember("panCamera", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("panCamera", typeof (ContainerGame), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_df374f602d28478db5f0ee37fae1ac37.MTHD_74da347c1ba14db094d3291e5c253fc3()
        }));
        this.AddMember("getInputString", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("getInputString", typeof (ContainerGame), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_df374f602d28478db5f0ee37fae1ac37.MTHD_c9e10f2452cb4ef9853fd9f8c19df41e()
        }));
        this.AddMember("get_screenWidth", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_screenWidth", typeof (ContainerGame), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_df374f602d28478db5f0ee37fae1ac37.MTHD_4d4343057a9d49aaa4bd3c7127923093()
        }));
        this.AddMember("get_screenHeight", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_screenHeight", typeof (ContainerGame), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_df374f602d28478db5f0ee37fae1ac37.MTHD_fe8d4c1bcff14c15b517808f3b864dc6()
        }));
        this.AddMember("createCreature", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("createCreature", typeof (ContainerGame), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_df374f602d28478db5f0ee37fae1ac37.MTHD_bf4f7b95ca7d48198c9beb3b93651a5e()
        }));
        this.AddMember("clearIndicators", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("clearIndicators", typeof (ContainerGame), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_df374f602d28478db5f0ee37fae1ac37.MTHD_3ef809c6c720405abd84d65cc4e3bd60()
        }));
        this.AddMember("createIndicator", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("createIndicator", typeof (ContainerGame), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_df374f602d28478db5f0ee37fae1ac37.MTHD_696fcda0e5934290aa75a06093e7a0c7()
        }));
        this.AddMember("Equals", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("Equals", typeof (ContainerGame), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_df374f602d28478db5f0ee37fae1ac37.MTHD_c21ec077d5ce4cd5a48b9479e4ccd377()
        }));
        this.AddMember("GetHashCode", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("GetHashCode", typeof (ContainerGame), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_df374f602d28478db5f0ee37fae1ac37.MTHD_28f6027a02d04fd996e1dd7a5c86d609()
        }));
        this.AddMember("GetType", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("GetType", typeof (ContainerGame), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_df374f602d28478db5f0ee37fae1ac37.MTHD_7046659eeaca4b60874a5145ec5875e9()
        }));
        this.AddMember("ToString", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("ToString", typeof (ContainerGame), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_df374f602d28478db5f0ee37fae1ac37.MTHD_11cd557c08684e40894bb8cdcc67f440()
        }));
        this.AddMember("turn", (IMemberDescriptor) new Bridge.TYPE_df374f602d28478db5f0ee37fae1ac37.PROP_4b3acd09ab6b4721843c40398aa607cf());
        this.AddMember("timeLimit", (IMemberDescriptor) new Bridge.TYPE_df374f602d28478db5f0ee37fae1ac37.PROP_1aad3d515cc54d90ba08e21fdaa399c8());
        this.AddMember("totalTimeElapsed", (IMemberDescriptor) new Bridge.TYPE_df374f602d28478db5f0ee37fae1ac37.PROP_121033ee271448a5a91f9adc789be36f());
        this.AddMember("turnTime", (IMemberDescriptor) new Bridge.TYPE_df374f602d28478db5f0ee37fae1ac37.PROP_ece30cc25fe44e96876141747598501e());
        this.AddMember("frame", (IMemberDescriptor) new Bridge.TYPE_df374f602d28478db5f0ee37fae1ac37.PROP_dc8bab449a2d4b12be6c5232257b12ce());
        this.AddMember("winOnDeath", (IMemberDescriptor) new Bridge.TYPE_df374f602d28478db5f0ee37fae1ac37.PROP_b4e5d06df6b84d2a8522e272d41ea875());
        this.AddMember("allowMovement", (IMemberDescriptor) new Bridge.TYPE_df374f602d28478db5f0ee37fae1ac37.PROP_cd1cdf117ff840e4befb60722d87ac63());
        this.AddMember("allowInput", (IMemberDescriptor) new Bridge.TYPE_df374f602d28478db5f0ee37fae1ac37.PROP_3f121e861b554a6bbd355e3474897e4a());
        this.AddMember("allowCallbacks", (IMemberDescriptor) new Bridge.TYPE_df374f602d28478db5f0ee37fae1ac37.PROP_df4e56b21419407e902141d227af0610());
        this.AddMember("allowSkipTurn", (IMemberDescriptor) new Bridge.TYPE_df374f602d28478db5f0ee37fae1ac37.PROP_6f8495c09bf343c5aa56be614f137c64());
        this.AddMember("terrainDestruction", (IMemberDescriptor) new Bridge.TYPE_df374f602d28478db5f0ee37fae1ac37.PROP_9b07749e0d1640368b5f98dbf0b59db1());
        this.AddMember("takeDamage", (IMemberDescriptor) new Bridge.TYPE_df374f602d28478db5f0ee37fae1ac37.PROP_54c6124824b046b39a060101cd9cd0ca());
        this.AddMember("armageddon", (IMemberDescriptor) new Bridge.TYPE_df374f602d28478db5f0ee37fae1ac37.PROP_5958cae567a945e49165e45845d6c5f4());
        this.AddMember("armageddonTurn", (IMemberDescriptor) new Bridge.TYPE_df374f602d28478db5f0ee37fae1ac37.PROP_f4aa43c4b2e446e08546fc2cbe087623());
        this.AddMember("waiting", (IMemberDescriptor) new Bridge.TYPE_df374f602d28478db5f0ee37fae1ac37.PROP_ee916072c8904e8a9739c543de0229e9());
        this.AddMember("paused", (IMemberDescriptor) new Bridge.TYPE_df374f602d28478db5f0ee37fae1ac37.PROP_adfd14951e2f41ebaea3d89b58cea8b6());
        this.AddMember("busy", (IMemberDescriptor) new Bridge.TYPE_df374f602d28478db5f0ee37fae1ac37.PROP_0f66d157bfb84890bf86399d1dc1cd5f());
        this.AddMember("ongoing", (IMemberDescriptor) new Bridge.TYPE_df374f602d28478db5f0ee37fae1ac37.PROP_1141561ff150443c92437ce4e3948d41());
        this.AddMember("gravity", (IMemberDescriptor) new Bridge.TYPE_df374f602d28478db5f0ee37fae1ac37.PROP_7c478de331b34d4bbc9770f1a3197469());
        this.AddMember("wind", (IMemberDescriptor) new Bridge.TYPE_df374f602d28478db5f0ee37fae1ac37.PROP_7f10765f5b7443c6bfd53ed5898a7a4b());
        this.AddMember("windDir", (IMemberDescriptor) new Bridge.TYPE_df374f602d28478db5f0ee37fae1ac37.PROP_df9df495e634407aa5dabc404255353f());
        this.AddMember("windPower", (IMemberDescriptor) new Bridge.TYPE_df374f602d28478db5f0ee37fae1ac37.PROP_3e24efff03334ab9932e3b410f566a4b());
        this.AddMember("width", (IMemberDescriptor) new Bridge.TYPE_df374f602d28478db5f0ee37fae1ac37.PROP_4c94cce5f8804faaba25ab804a2bac27());
        this.AddMember("height", (IMemberDescriptor) new Bridge.TYPE_df374f602d28478db5f0ee37fae1ac37.PROP_19045e05e836461f87ad751d338712bd());
        this.AddMember("mousePosition", (IMemberDescriptor) new Bridge.TYPE_df374f602d28478db5f0ee37fae1ac37.PROP_988eac09dccc44ca9d99c301796977dd());
        this.AddMember("mouseOverUI", (IMemberDescriptor) new Bridge.TYPE_df374f602d28478db5f0ee37fae1ac37.PROP_7927f9f252df4692a30c907cf1cbcc3d());
        this.AddMember("cameraPosition", (IMemberDescriptor) new Bridge.TYPE_df374f602d28478db5f0ee37fae1ac37.PROP_5889ab47a8da4661af36bd5adfa337c2());
        this.AddMember("cameraZoom", (IMemberDescriptor) new Bridge.TYPE_df374f602d28478db5f0ee37fae1ac37.PROP_051c268e92814b1ea2fb8cbc2081910e());
        this.AddMember("allowBounce", (IMemberDescriptor) new Bridge.TYPE_df374f602d28478db5f0ee37fae1ac37.PROP_22e3c129e3aa4b369beb397236e06297());
        this.AddMember("isUsingTouchControls", (IMemberDescriptor) new Bridge.TYPE_df374f602d28478db5f0ee37fae1ac37.PROP_7486a7558b5240869d96d653843ecf48());
        this.AddMember("screenWidth", (IMemberDescriptor) new Bridge.TYPE_df374f602d28478db5f0ee37fae1ac37.PROP_524b58959eeb441cb043b6566b86ddaa());
        this.AddMember("screenHeight", (IMemberDescriptor) new Bridge.TYPE_df374f602d28478db5f0ee37fae1ac37.PROP_f5482c28fc594f3f9d072054cbba618d());
      }

      private sealed class MTHD_5258b84407084cfcabb2bad6342349e3 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_5258b84407084cfcabb2bad6342349e3()
        {
          this.Initialize("getUser", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerGame) obj).getUser();
        }
      }

      private sealed class MTHD_487997e85d1640c289e7a54e296a9cb3 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_487997e85d1640c289e7a54e296a9cb3()
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

      private sealed class MTHD_a5549187fee84c2e9a25aa7012a48199 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_a5549187fee84c2e9a25aa7012a48199()
        {
          this.Initialize("getPlayerCount", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerGame) obj).getPlayerCount();
        }
      }

      private sealed class MTHD_e20af6ff79884292b42f48a5d7de2f30 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_e20af6ff79884292b42f48a5d7de2f30()
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

      private sealed class MTHD_d51b4eaa67f54a02baad3c5d5e4b74e5 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_d51b4eaa67f54a02baad3c5d5e4b74e5()
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

      private sealed class MTHD_3013086fc1cc44fc8390305f8290ae60 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_3013086fc1cc44fc8390305f8290ae60()
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

      private sealed class MTHD_744d5c4d237b498b9084b4b6823e9f57 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_744d5c4d237b498b9084b4b6823e9f57()
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

      private sealed class MTHD_dc66095371154dc2811c293f445e59ee : HardwiredMethodMemberDescriptor
      {
        internal MTHD_dc66095371154dc2811c293f445e59ee()
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

      private sealed class MTHD_ad77983bf7b6406eae2c8ad12d8e30c1 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_ad77983bf7b6406eae2c8ad12d8e30c1()
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

      private sealed class MTHD_87e5782a66e245018a9f05cc6e075fac : HardwiredMethodMemberDescriptor
      {
        internal MTHD_87e5782a66e245018a9f05cc6e075fac()
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

      private sealed class MTHD_58a698cd2f2e4ed88b65872772efca20 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_58a698cd2f2e4ed88b65872772efca20()
        {
          this.Initialize("get_turn", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerGame) obj).turn;
        }
      }

      private sealed class MTHD_5b7a933a380b4af980140524c27eef7a : HardwiredMethodMemberDescriptor
      {
        internal MTHD_5b7a933a380b4af980140524c27eef7a()
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

      private sealed class MTHD_a563a77999724060b35e103d53de8841 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_a563a77999724060b35e103d53de8841()
        {
          this.Initialize("get_timeLimit", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerGame) obj).timeLimit;
        }
      }

      private sealed class MTHD_56e99bdb9b524667b85c78b56f381c17 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_56e99bdb9b524667b85c78b56f381c17()
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

      private sealed class MTHD_cbe99221e29f498881fd33e60fe92dc1 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_cbe99221e29f498881fd33e60fe92dc1()
        {
          this.Initialize("get_totalTimeElapsed", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerGame) obj).totalTimeElapsed;
        }
      }

      private sealed class MTHD_030f6a3b928b458f83ddbad7d57eb0c8 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_030f6a3b928b458f83ddbad7d57eb0c8()
        {
          this.Initialize("get_turnTime", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerGame) obj).turnTime;
        }
      }

      private sealed class MTHD_849c3d77795f4b629cf7efa72b96e09e : HardwiredMethodMemberDescriptor
      {
        internal MTHD_849c3d77795f4b629cf7efa72b96e09e()
        {
          this.Initialize("get_frame", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerGame) obj).frame;
        }
      }

      private sealed class MTHD_ed1428540c494d2e8570381c2a62b382 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_ed1428540c494d2e8570381c2a62b382()
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

      private sealed class MTHD_fed21a93a52244c5be333453bd19eb52 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_fed21a93a52244c5be333453bd19eb52()
        {
          this.Initialize("get_winOnDeath", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerGame) obj).winOnDeath;
        }
      }

      private sealed class MTHD_22150297dbaf47f6b55e5967a5d2d00b : HardwiredMethodMemberDescriptor
      {
        internal MTHD_22150297dbaf47f6b55e5967a5d2d00b()
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

      private sealed class MTHD_b0d97ceda9164770999719ef1b0bf7d5 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_b0d97ceda9164770999719ef1b0bf7d5()
        {
          this.Initialize("get_allowMovement", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerGame) obj).allowMovement;
        }
      }

      private sealed class MTHD_5c53dc2dbaaf4cc4a4d5d988ceaebcb7 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_5c53dc2dbaaf4cc4a4d5d988ceaebcb7()
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

      private sealed class MTHD_612e92af8e274ec88335ce6259900e85 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_612e92af8e274ec88335ce6259900e85()
        {
          this.Initialize("get_allowInput", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerGame) obj).allowInput;
        }
      }

      private sealed class MTHD_936fadbbc642404fbcb078b882633239 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_936fadbbc642404fbcb078b882633239()
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

      private sealed class MTHD_becac51f2dc444d8bd89fa4f5f157efb : HardwiredMethodMemberDescriptor
      {
        internal MTHD_becac51f2dc444d8bd89fa4f5f157efb()
        {
          this.Initialize("get_allowCallbacks", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerGame) obj).allowCallbacks;
        }
      }

      private sealed class MTHD_f7e8186af1db44b99fb5dbc1ba5f3749 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_f7e8186af1db44b99fb5dbc1ba5f3749()
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

      private sealed class MTHD_830db6e6219d484484e206c424a949bc : HardwiredMethodMemberDescriptor
      {
        internal MTHD_830db6e6219d484484e206c424a949bc()
        {
          this.Initialize("get_allowSkipTurn", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerGame) obj).allowSkipTurn;
        }
      }

      private sealed class MTHD_1cc282a1b31e4bdb9476812009df13d1 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_1cc282a1b31e4bdb9476812009df13d1()
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

      private sealed class MTHD_386db306b8b240119a567c77b769d9e4 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_386db306b8b240119a567c77b769d9e4()
        {
          this.Initialize("get_terrainDestruction", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerGame) obj).terrainDestruction;
        }
      }

      private sealed class MTHD_3e979d84e2f744f0ab3b65af86ea64ec : HardwiredMethodMemberDescriptor
      {
        internal MTHD_3e979d84e2f744f0ab3b65af86ea64ec()
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

      private sealed class MTHD_d6a440fc51a946fd85d96228dd743097 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_d6a440fc51a946fd85d96228dd743097()
        {
          this.Initialize("get_takeDamage", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerGame) obj).takeDamage;
        }
      }

      private sealed class MTHD_5c6c67c830c443e3bb9fda31553d344b : HardwiredMethodMemberDescriptor
      {
        internal MTHD_5c6c67c830c443e3bb9fda31553d344b()
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

      private sealed class MTHD_186190ccb6a4428591191d5473af034f : HardwiredMethodMemberDescriptor
      {
        internal MTHD_186190ccb6a4428591191d5473af034f()
        {
          this.Initialize("get_armageddon", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerGame) obj).armageddon;
        }
      }

      private sealed class MTHD_142e411ca3c244ebaa7c7b80e8d30d00 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_142e411ca3c244ebaa7c7b80e8d30d00()
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

      private sealed class MTHD_dc78334274db4b0b811cce5e00f2584d : HardwiredMethodMemberDescriptor
      {
        internal MTHD_dc78334274db4b0b811cce5e00f2584d()
        {
          this.Initialize("get_armageddonTurn", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerGame) obj).armageddonTurn;
        }
      }

      private sealed class MTHD_a3f96e920b584b3383c0f5d6da4a2761 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_a3f96e920b584b3383c0f5d6da4a2761()
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

      private sealed class MTHD_2e8d98ef02a74948a496b2a70a3f551d : HardwiredMethodMemberDescriptor
      {
        internal MTHD_2e8d98ef02a74948a496b2a70a3f551d()
        {
          this.Initialize("get_waiting", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerGame) obj).waiting;
        }
      }

      private sealed class MTHD_c014253c37a546c7a97b875cb8b53b7c : HardwiredMethodMemberDescriptor
      {
        internal MTHD_c014253c37a546c7a97b875cb8b53b7c()
        {
          this.Initialize("get_paused", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerGame) obj).paused;
        }
      }

      private sealed class MTHD_abf6caec072c436594a85dac04827b47 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_abf6caec072c436594a85dac04827b47()
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

      private sealed class MTHD_ab242d76b4284e40b25152fb9f0877b3 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_ab242d76b4284e40b25152fb9f0877b3()
        {
          this.Initialize("get_busy", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerGame) obj).busy;
        }
      }

      private sealed class MTHD_18f5f936fec84585b02cdccfc55eb25a : HardwiredMethodMemberDescriptor
      {
        internal MTHD_18f5f936fec84585b02cdccfc55eb25a()
        {
          this.Initialize("get_ongoing", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerGame) obj).ongoing;
        }
      }

      private sealed class MTHD_f3c559bbb6384df3919dbe45c4119f6e : HardwiredMethodMemberDescriptor
      {
        internal MTHD_f3c559bbb6384df3919dbe45c4119f6e()
        {
          this.Initialize("get_gravity", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerGame) obj).gravity;
        }
      }

      private sealed class MTHD_d4dbf2f042064c63bd9c447ffa41cec6 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_d4dbf2f042064c63bd9c447ffa41cec6()
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

      private sealed class MTHD_799324f47e2b4f80bf3e82c5a17be70c : HardwiredMethodMemberDescriptor
      {
        internal MTHD_799324f47e2b4f80bf3e82c5a17be70c()
        {
          this.Initialize("get_wind", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerGame) obj).wind;
        }
      }

      private sealed class MTHD_5bf5d36dc23b4181978d3f38b6236dbb : HardwiredMethodMemberDescriptor
      {
        internal MTHD_5bf5d36dc23b4181978d3f38b6236dbb()
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

      private sealed class MTHD_d936cac515924d78967f653f46abf397 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_d936cac515924d78967f653f46abf397()
        {
          this.Initialize("get_windDir", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerGame) obj).windDir;
        }
      }

      private sealed class MTHD_abace182bdbf452f929d3edcb952f515 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_abace182bdbf452f929d3edcb952f515()
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

      private sealed class MTHD_d0ff7bc467a14b03b71bac5d7b9e96f0 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_d0ff7bc467a14b03b71bac5d7b9e96f0()
        {
          this.Initialize("get_windPower", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerGame) obj).windPower;
        }
      }

      private sealed class MTHD_2b77f9275d6e431c93e1faa61e0dd242 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_2b77f9275d6e431c93e1faa61e0dd242()
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

      private sealed class MTHD_136d989373764a79b48523b641ad959e : HardwiredMethodMemberDescriptor
      {
        internal MTHD_136d989373764a79b48523b641ad959e()
        {
          this.Initialize("get_width", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerGame) obj).width;
        }
      }

      private sealed class MTHD_9e1d6da04dba49fb8dfea51fa7bdc060 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_9e1d6da04dba49fb8dfea51fa7bdc060()
        {
          this.Initialize("get_height", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerGame) obj).height;
        }
      }

      private sealed class MTHD_a6b8c228a7154d6085c2dc5fa9669d67 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_a6b8c228a7154d6085c2dc5fa9669d67()
        {
          this.Initialize("get_mousePosition", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerGame) obj).mousePosition;
        }
      }

      private sealed class MTHD_42f356b988e845c48f32e66b40b419b7 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_42f356b988e845c48f32e66b40b419b7()
        {
          this.Initialize("get_mouseOverUI", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerGame) obj).mouseOverUI;
        }
      }

      private sealed class MTHD_4262f85e723749208c7717f8c054a98b : HardwiredMethodMemberDescriptor
      {
        internal MTHD_4262f85e723749208c7717f8c054a98b()
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

      private sealed class MTHD_144fafdc8ece482292208490c7360718 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_144fafdc8ece482292208490c7360718()
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

      private sealed class MTHD_471bef3a844d456a801d6ed3973334e6 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_471bef3a844d456a801d6ed3973334e6()
        {
          this.Initialize("get_cameraPosition", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerGame) obj).cameraPosition;
        }
      }

      private sealed class MTHD_afccd40078ca419f9cd059b90d644be8 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_afccd40078ca419f9cd059b90d644be8()
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

      private sealed class MTHD_5341baa5aecc4a6d85e51249b7afe2e5 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_5341baa5aecc4a6d85e51249b7afe2e5()
        {
          this.Initialize("get_cameraZoom", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerGame) obj).cameraZoom;
        }
      }

      private sealed class MTHD_bfdf7ddb084e4bae84bfdc5ab0eab365 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_bfdf7ddb084e4bae84bfdc5ab0eab365()
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

      private sealed class MTHD_1f5984389d1e411bab339817b572fee0 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_1f5984389d1e411bab339817b572fee0()
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

      private sealed class MTHD_04794890632047a5b4184c495e28dc96 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_04794890632047a5b4184c495e28dc96()
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

      private sealed class MTHD_b3571dffe07e41cfad7ed937fb7b3958 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_b3571dffe07e41cfad7ed937fb7b3958()
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

      private sealed class MTHD_1fd168ef84434632ab70d40e38c09222 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_1fd168ef84434632ab70d40e38c09222()
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

      private sealed class MTHD_28c65bf79f204cfdaaf0f47a48d4b57f : HardwiredMethodMemberDescriptor
      {
        internal MTHD_28c65bf79f204cfdaaf0f47a48d4b57f()
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

      private sealed class MTHD_924f279c024949ad8393cd3d5d4fc0ec : HardwiredMethodMemberDescriptor
      {
        internal MTHD_924f279c024949ad8393cd3d5d4fc0ec()
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

      private sealed class MTHD_43d9f7d3a54a4e299bc6c2015666ac7a : HardwiredMethodMemberDescriptor
      {
        internal MTHD_43d9f7d3a54a4e299bc6c2015666ac7a()
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

      private sealed class MTHD_8e94ec9ef013461ca9fd4e0e5f7f6dca : HardwiredMethodMemberDescriptor
      {
        internal MTHD_8e94ec9ef013461ca9fd4e0e5f7f6dca()
        {
          this.Initialize("applyDraw", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          ((ContainerGame) obj).applyDraw();
          return (object) DynValue.Void;
        }
      }

      private sealed class MTHD_ab7db456aa354cb48d0cebe6e1d904dc : HardwiredMethodMemberDescriptor
      {
        internal MTHD_ab7db456aa354cb48d0cebe6e1d904dc()
        {
          this.Initialize("get_allowBounce", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerGame) obj).allowBounce;
        }
      }

      private sealed class MTHD_54877ce2e7b145ffbd6cc15477d07bb5 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_54877ce2e7b145ffbd6cc15477d07bb5()
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

      private sealed class MTHD_89385d12005841b7a07df7cdfe66f9d8 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_89385d12005841b7a07df7cdfe66f9d8()
        {
          this.Initialize("get_isUsingTouchControls", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerGame) obj).isUsingTouchControls;
        }
      }

      private sealed class MTHD_c69f2dc436e446b2a7b327212afcae64 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_c69f2dc436e446b2a7b327212afcae64()
        {
          this.Initialize("clientRefreshSpells", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          ((ContainerGame) obj).clientRefreshSpells();
          return (object) DynValue.Void;
        }
      }

      private sealed class MTHD_38dce31134254cca8fd03e1fffee11bb : HardwiredMethodMemberDescriptor
      {
        internal MTHD_38dce31134254cca8fd03e1fffee11bb()
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

      private sealed class MTHD_3aa32660856d4e928118197d75346fce : HardwiredMethodMemberDescriptor
      {
        internal MTHD_3aa32660856d4e928118197d75346fce()
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

      private sealed class MTHD_e2560c0d1675413c9053cd5bece54128 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_e2560c0d1675413c9053cd5bece54128()
        {
          this.Initialize("win", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          ((ContainerGame) obj).win();
          return (object) DynValue.Void;
        }
      }

      private sealed class MTHD_5916b60a53dc4fa280531c9ef08ed051 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_5916b60a53dc4fa280531c9ef08ed051()
        {
          this.Initialize("lose", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          ((ContainerGame) obj).lose();
          return (object) DynValue.Void;
        }
      }

      private sealed class MTHD_45c8fa0b3c5848e080a1a3a403708b80 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_45c8fa0b3c5848e080a1a3a403708b80()
        {
          this.Initialize("nextTurn", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          ((ContainerGame) obj).nextTurn();
          return (object) DynValue.Void;
        }
      }

      private sealed class MTHD_18c3a9ff47c249f0b15490b2b32c9063 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_18c3a9ff47c249f0b15490b2b32c9063()
        {
          this.Initialize("resetMap", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          ((ContainerGame) obj).resetMap();
          return (object) DynValue.Void;
        }
      }

      private sealed class MTHD_30cd147adef44be0985c7ef46a27ae9f : HardwiredMethodMemberDescriptor
      {
        internal MTHD_30cd147adef44be0985c7ef46a27ae9f()
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

      private sealed class MTHD_2249b92f911e479abef961c82a9beff8 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_2249b92f911e479abef961c82a9beff8()
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

      private sealed class MTHD_eba7fffe3ddf46c39bd41aefb6d74ead : HardwiredMethodMemberDescriptor
      {
        internal MTHD_eba7fffe3ddf46c39bd41aefb6d74ead()
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

      private sealed class MTHD_bebb563e8aff43deaab11bcc37b17289 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_bebb563e8aff43deaab11bcc37b17289()
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

      private sealed class MTHD_517a65e4ef354529b12cebd12f60e31b : HardwiredMethodMemberDescriptor
      {
        internal MTHD_517a65e4ef354529b12cebd12f60e31b()
        {
          this.Initialize("getMapID", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerGame) obj).getMapID();
        }
      }

      private sealed class MTHD_968bc74bae354b028c1b64228f5477cd : HardwiredMethodMemberDescriptor
      {
        internal MTHD_968bc74bae354b028c1b64228f5477cd()
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

      private sealed class MTHD_c9a41623f13d48a8a7a44b0b63a07c97 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_c9a41623f13d48a8a7a44b0b63a07c97()
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

      private sealed class MTHD_c36028a649694495917e1078d375754d : HardwiredMethodMemberDescriptor
      {
        internal MTHD_c36028a649694495917e1078d375754d()
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

      private sealed class MTHD_74da347c1ba14db094d3291e5c253fc3 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_74da347c1ba14db094d3291e5c253fc3()
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

      private sealed class MTHD_c9e10f2452cb4ef9853fd9f8c19df41e : HardwiredMethodMemberDescriptor
      {
        internal MTHD_c9e10f2452cb4ef9853fd9f8c19df41e()
        {
          this.Initialize("getInputString", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerGame) obj).getInputString();
        }
      }

      private sealed class MTHD_4d4343057a9d49aaa4bd3c7127923093 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_4d4343057a9d49aaa4bd3c7127923093()
        {
          this.Initialize("get_screenWidth", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerGame) obj).screenWidth;
        }
      }

      private sealed class MTHD_fe8d4c1bcff14c15b517808f3b864dc6 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_fe8d4c1bcff14c15b517808f3b864dc6()
        {
          this.Initialize("get_screenHeight", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerGame) obj).screenHeight;
        }
      }

      private sealed class MTHD_bf4f7b95ca7d48198c9beb3b93651a5e : HardwiredMethodMemberDescriptor
      {
        internal MTHD_bf4f7b95ca7d48198c9beb3b93651a5e()
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

      private sealed class MTHD_3ef809c6c720405abd84d65cc4e3bd60 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_3ef809c6c720405abd84d65cc4e3bd60()
        {
          this.Initialize("clearIndicators", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          ((ContainerGame) obj).clearIndicators();
          return (object) DynValue.Void;
        }
      }

      private sealed class MTHD_696fcda0e5934290aa75a06093e7a0c7 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_696fcda0e5934290aa75a06093e7a0c7()
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

      private sealed class MTHD_c21ec077d5ce4cd5a48b9479e4ccd377 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_c21ec077d5ce4cd5a48b9479e4ccd377()
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

      private sealed class MTHD_28f6027a02d04fd996e1dd7a5c86d609 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_28f6027a02d04fd996e1dd7a5c86d609()
        {
          this.Initialize("GetHashCode", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) obj.GetHashCode();
        }
      }

      private sealed class MTHD_7046659eeaca4b60874a5145ec5875e9 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_7046659eeaca4b60874a5145ec5875e9()
        {
          this.Initialize("GetType", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) obj.GetType();
        }
      }

      private sealed class MTHD_11cd557c08684e40894bb8cdcc67f440 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_11cd557c08684e40894bb8cdcc67f440()
        {
          this.Initialize("ToString", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) obj.ToString();
        }
      }

      private sealed class PROP_4b3acd09ab6b4721843c40398aa607cf : HardwiredMemberDescriptor
      {
        internal PROP_4b3acd09ab6b4721843c40398aa607cf()
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

      private sealed class PROP_1aad3d515cc54d90ba08e21fdaa399c8 : HardwiredMemberDescriptor
      {
        internal PROP_1aad3d515cc54d90ba08e21fdaa399c8()
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

      private sealed class PROP_121033ee271448a5a91f9adc789be36f : HardwiredMemberDescriptor
      {
        internal PROP_121033ee271448a5a91f9adc789be36f()
          : base(typeof (double), "totalTimeElapsed", false, MemberDescriptorAccess.CanRead)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((ContainerGame) obj).totalTimeElapsed;
        }
      }

      private sealed class PROP_ece30cc25fe44e96876141747598501e : HardwiredMemberDescriptor
      {
        internal PROP_ece30cc25fe44e96876141747598501e()
          : base(typeof (double), "turnTime", false, MemberDescriptorAccess.CanRead)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((ContainerGame) obj).turnTime;
        }
      }

      private sealed class PROP_dc8bab449a2d4b12be6c5232257b12ce : HardwiredMemberDescriptor
      {
        internal PROP_dc8bab449a2d4b12be6c5232257b12ce()
          : base(typeof (int), "frame", false, MemberDescriptorAccess.CanRead)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((ContainerGame) obj).frame;
        }
      }

      private sealed class PROP_b4e5d06df6b84d2a8522e272d41ea875 : HardwiredMemberDescriptor
      {
        internal PROP_b4e5d06df6b84d2a8522e272d41ea875()
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

      private sealed class PROP_cd1cdf117ff840e4befb60722d87ac63 : HardwiredMemberDescriptor
      {
        internal PROP_cd1cdf117ff840e4befb60722d87ac63()
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

      private sealed class PROP_3f121e861b554a6bbd355e3474897e4a : HardwiredMemberDescriptor
      {
        internal PROP_3f121e861b554a6bbd355e3474897e4a()
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

      private sealed class PROP_df4e56b21419407e902141d227af0610 : HardwiredMemberDescriptor
      {
        internal PROP_df4e56b21419407e902141d227af0610()
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

      private sealed class PROP_6f8495c09bf343c5aa56be614f137c64 : HardwiredMemberDescriptor
      {
        internal PROP_6f8495c09bf343c5aa56be614f137c64()
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

      private sealed class PROP_9b07749e0d1640368b5f98dbf0b59db1 : HardwiredMemberDescriptor
      {
        internal PROP_9b07749e0d1640368b5f98dbf0b59db1()
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

      private sealed class PROP_54c6124824b046b39a060101cd9cd0ca : HardwiredMemberDescriptor
      {
        internal PROP_54c6124824b046b39a060101cd9cd0ca()
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

      private sealed class PROP_5958cae567a945e49165e45845d6c5f4 : HardwiredMemberDescriptor
      {
        internal PROP_5958cae567a945e49165e45845d6c5f4()
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

      private sealed class PROP_f4aa43c4b2e446e08546fc2cbe087623 : HardwiredMemberDescriptor
      {
        internal PROP_f4aa43c4b2e446e08546fc2cbe087623()
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

      private sealed class PROP_ee916072c8904e8a9739c543de0229e9 : HardwiredMemberDescriptor
      {
        internal PROP_ee916072c8904e8a9739c543de0229e9()
          : base(typeof (bool), "waiting", false, MemberDescriptorAccess.CanRead)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((ContainerGame) obj).waiting;
        }
      }

      private sealed class PROP_adfd14951e2f41ebaea3d89b58cea8b6 : HardwiredMemberDescriptor
      {
        internal PROP_adfd14951e2f41ebaea3d89b58cea8b6()
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

      private sealed class PROP_0f66d157bfb84890bf86399d1dc1cd5f : HardwiredMemberDescriptor
      {
        internal PROP_0f66d157bfb84890bf86399d1dc1cd5f()
          : base(typeof (bool), "busy", false, MemberDescriptorAccess.CanRead)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((ContainerGame) obj).busy;
        }
      }

      private sealed class PROP_1141561ff150443c92437ce4e3948d41 : HardwiredMemberDescriptor
      {
        internal PROP_1141561ff150443c92437ce4e3948d41()
          : base(typeof (bool), "ongoing", false, MemberDescriptorAccess.CanRead)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((ContainerGame) obj).ongoing;
        }
      }

      private sealed class PROP_7c478de331b34d4bbc9770f1a3197469 : HardwiredMemberDescriptor
      {
        internal PROP_7c478de331b34d4bbc9770f1a3197469()
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

      private sealed class PROP_7f10765f5b7443c6bfd53ed5898a7a4b : HardwiredMemberDescriptor
      {
        internal PROP_7f10765f5b7443c6bfd53ed5898a7a4b()
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

      private sealed class PROP_df9df495e634407aa5dabc404255353f : HardwiredMemberDescriptor
      {
        internal PROP_df9df495e634407aa5dabc404255353f()
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

      private sealed class PROP_3e24efff03334ab9932e3b410f566a4b : HardwiredMemberDescriptor
      {
        internal PROP_3e24efff03334ab9932e3b410f566a4b()
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

      private sealed class PROP_4c94cce5f8804faaba25ab804a2bac27 : HardwiredMemberDescriptor
      {
        internal PROP_4c94cce5f8804faaba25ab804a2bac27()
          : base(typeof (int), "width", false, MemberDescriptorAccess.CanRead)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((ContainerGame) obj).width;
        }
      }

      private sealed class PROP_19045e05e836461f87ad751d338712bd : HardwiredMemberDescriptor
      {
        internal PROP_19045e05e836461f87ad751d338712bd()
          : base(typeof (int), "height", false, MemberDescriptorAccess.CanRead)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((ContainerGame) obj).height;
        }
      }

      private sealed class PROP_988eac09dccc44ca9d99c301796977dd : HardwiredMemberDescriptor
      {
        internal PROP_988eac09dccc44ca9d99c301796977dd()
          : base(typeof (Educative.Point), "mousePosition", false, MemberDescriptorAccess.CanRead)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((ContainerGame) obj).mousePosition;
        }
      }

      private sealed class PROP_7927f9f252df4692a30c907cf1cbcc3d : HardwiredMemberDescriptor
      {
        internal PROP_7927f9f252df4692a30c907cf1cbcc3d()
          : base(typeof (bool), "mouseOverUI", false, MemberDescriptorAccess.CanRead)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((ContainerGame) obj).mouseOverUI;
        }
      }

      private sealed class PROP_5889ab47a8da4661af36bd5adfa337c2 : HardwiredMemberDescriptor
      {
        internal PROP_5889ab47a8da4661af36bd5adfa337c2()
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

      private sealed class PROP_051c268e92814b1ea2fb8cbc2081910e : HardwiredMemberDescriptor
      {
        internal PROP_051c268e92814b1ea2fb8cbc2081910e()
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

      private sealed class PROP_22e3c129e3aa4b369beb397236e06297 : HardwiredMemberDescriptor
      {
        internal PROP_22e3c129e3aa4b369beb397236e06297()
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

      private sealed class PROP_7486a7558b5240869d96d653843ecf48 : HardwiredMemberDescriptor
      {
        internal PROP_7486a7558b5240869d96d653843ecf48()
          : base(typeof (bool), "isUsingTouchControls", false, MemberDescriptorAccess.CanRead)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((ContainerGame) obj).isUsingTouchControls;
        }
      }

      private sealed class PROP_524b58959eeb441cb043b6566b86ddaa : HardwiredMemberDescriptor
      {
        internal PROP_524b58959eeb441cb043b6566b86ddaa()
          : base(typeof (double), "screenWidth", false, MemberDescriptorAccess.CanRead)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((ContainerGame) obj).screenWidth;
        }
      }

      private sealed class PROP_f5482c28fc594f3f9d072054cbba618d : HardwiredMemberDescriptor
      {
        internal PROP_f5482c28fc594f3f9d072054cbba618d()
          : base(typeof (double), "screenHeight", false, MemberDescriptorAccess.CanRead)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((ContainerGame) obj).screenHeight;
        }
      }
    }

    private sealed class TYPE_f1ecff994a454a7b982cb7976a758ebd : HardwiredUserDataDescriptor
    {
      internal TYPE_f1ecff994a454a7b982cb7976a758ebd()
        : base(typeof (ContainerPlayer))
      {
        this.AddMember("get_localTurn", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_localTurn", typeof (ContainerPlayer), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_f1ecff994a454a7b982cb7976a758ebd.MTHD_f9469071bdf74cecb5b469fc149d7a13()
        }));
        this.AddMember("set_localTurn", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("set_localTurn", typeof (ContainerPlayer), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_f1ecff994a454a7b982cb7976a758ebd.MTHD_d0f7479eed994a1eb8f373530aa1a0f8()
        }));
        this.AddMember("get_name", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_name", typeof (ContainerPlayer), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_f1ecff994a454a7b982cb7976a758ebd.MTHD_922cb654d1bc4656a340c4adcd9e1726()
        }));
        this.AddMember("get_team", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_team", typeof (ContainerPlayer), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_f1ecff994a454a7b982cb7976a758ebd.MTHD_6758bf1f202f4d5b8dd19da5cb077409()
        }));
        this.AddMember("get_yourTurn", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_yourTurn", typeof (ContainerPlayer), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_f1ecff994a454a7b982cb7976a758ebd.MTHD_e5564ad94b49454c9e71b7ffc661f66e()
        }));
        this.AddMember("getCreatures", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("getCreatures", typeof (ContainerPlayer), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_f1ecff994a454a7b982cb7976a758ebd.MTHD_815e821e172348699b6c67e1445f4de9()
        }));
        this.AddMember("getCreature", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("getCreature", typeof (ContainerPlayer), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_f1ecff994a454a7b982cb7976a758ebd.MTHD_e72d8ed74e6647e49c282a673ffe8c5d()
        }));
        this.AddMember("getMinionCount", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("getMinionCount", typeof (ContainerPlayer), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_f1ecff994a454a7b982cb7976a758ebd.MTHD_3f0eb7fb56ac4073bb46ab9ffb5707b9()
        }));
        this.AddMember("GetHashCode", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("GetHashCode", typeof (ContainerPlayer), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_f1ecff994a454a7b982cb7976a758ebd.MTHD_86c727cfc14142d2b99c9426f0449b1f()
        }));
        this.AddMember("Equals", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("Equals", typeof (ContainerPlayer), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_f1ecff994a454a7b982cb7976a758ebd.MTHD_06ad1aae788d4d759b2c7cce7967c4c1()
        }));
        this.AddMember("GetType", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("GetType", typeof (ContainerPlayer), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_f1ecff994a454a7b982cb7976a758ebd.MTHD_cbd2b7b02e2e4e8daffc44992ce6611e()
        }));
        this.AddMember("ToString", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("ToString", typeof (ContainerPlayer), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_f1ecff994a454a7b982cb7976a758ebd.MTHD_6ac7048d96af49a2b9b8f8d18f1f506a()
        }));
        this.AddMember("localTurn", (IMemberDescriptor) new Bridge.TYPE_f1ecff994a454a7b982cb7976a758ebd.PROP_e8872e7143054339abb29f7923f920cc());
        this.AddMember("name", (IMemberDescriptor) new Bridge.TYPE_f1ecff994a454a7b982cb7976a758ebd.PROP_68702df127e44c2a845cc511248cf60b());
        this.AddMember("team", (IMemberDescriptor) new Bridge.TYPE_f1ecff994a454a7b982cb7976a758ebd.PROP_b493b1ebad804178a223f9f3dd689b12());
        this.AddMember("yourTurn", (IMemberDescriptor) new Bridge.TYPE_f1ecff994a454a7b982cb7976a758ebd.PROP_571068ae352b4b698d7943717722afa2());
      }

      private sealed class MTHD_f9469071bdf74cecb5b469fc149d7a13 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_f9469071bdf74cecb5b469fc149d7a13()
        {
          this.Initialize("get_localTurn", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerPlayer) obj).localTurn;
        }
      }

      private sealed class MTHD_d0f7479eed994a1eb8f373530aa1a0f8 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_d0f7479eed994a1eb8f373530aa1a0f8()
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

      private sealed class MTHD_922cb654d1bc4656a340c4adcd9e1726 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_922cb654d1bc4656a340c4adcd9e1726()
        {
          this.Initialize("get_name", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerPlayer) obj).name;
        }
      }

      private sealed class MTHD_6758bf1f202f4d5b8dd19da5cb077409 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_6758bf1f202f4d5b8dd19da5cb077409()
        {
          this.Initialize("get_team", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerPlayer) obj).team;
        }
      }

      private sealed class MTHD_e5564ad94b49454c9e71b7ffc661f66e : HardwiredMethodMemberDescriptor
      {
        internal MTHD_e5564ad94b49454c9e71b7ffc661f66e()
        {
          this.Initialize("get_yourTurn", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerPlayer) obj).yourTurn;
        }
      }

      private sealed class MTHD_815e821e172348699b6c67e1445f4de9 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_815e821e172348699b6c67e1445f4de9()
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

      private sealed class MTHD_e72d8ed74e6647e49c282a673ffe8c5d : HardwiredMethodMemberDescriptor
      {
        internal MTHD_e72d8ed74e6647e49c282a673ffe8c5d()
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

      private sealed class MTHD_3f0eb7fb56ac4073bb46ab9ffb5707b9 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_3f0eb7fb56ac4073bb46ab9ffb5707b9()
        {
          this.Initialize("getMinionCount", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerPlayer) obj).getMinionCount();
        }
      }

      private sealed class MTHD_86c727cfc14142d2b99c9426f0449b1f : HardwiredMethodMemberDescriptor
      {
        internal MTHD_86c727cfc14142d2b99c9426f0449b1f()
        {
          this.Initialize("GetHashCode", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerPlayer) obj).GetHashCode();
        }
      }

      private sealed class MTHD_06ad1aae788d4d759b2c7cce7967c4c1 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_06ad1aae788d4d759b2c7cce7967c4c1()
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

      private sealed class MTHD_cbd2b7b02e2e4e8daffc44992ce6611e : HardwiredMethodMemberDescriptor
      {
        internal MTHD_cbd2b7b02e2e4e8daffc44992ce6611e()
        {
          this.Initialize("GetType", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) obj.GetType();
        }
      }

      private sealed class MTHD_6ac7048d96af49a2b9b8f8d18f1f506a : HardwiredMethodMemberDescriptor
      {
        internal MTHD_6ac7048d96af49a2b9b8f8d18f1f506a()
        {
          this.Initialize("ToString", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) obj.ToString();
        }
      }

      private sealed class PROP_e8872e7143054339abb29f7923f920cc : HardwiredMemberDescriptor
      {
        internal PROP_e8872e7143054339abb29f7923f920cc()
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

      private sealed class PROP_68702df127e44c2a845cc511248cf60b : HardwiredMemberDescriptor
      {
        internal PROP_68702df127e44c2a845cc511248cf60b()
          : base(typeof (string), "name", false, MemberDescriptorAccess.CanRead)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((ContainerPlayer) obj).name;
        }
      }

      private sealed class PROP_b493b1ebad804178a223f9f3dd689b12 : HardwiredMemberDescriptor
      {
        internal PROP_b493b1ebad804178a223f9f3dd689b12()
          : base(typeof (int), "team", false, MemberDescriptorAccess.CanRead)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((ContainerPlayer) obj).team;
        }
      }

      private sealed class PROP_571068ae352b4b698d7943717722afa2 : HardwiredMemberDescriptor
      {
        internal PROP_571068ae352b4b698d7943717722afa2()
          : base(typeof (bool), "yourTurn", false, MemberDescriptorAccess.CanRead)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((ContainerPlayer) obj).yourTurn;
        }
      }
    }

    private sealed class TYPE_fbef48a9042141c2b33d0fe295021fba : HardwiredUserDataDescriptor
    {
      internal TYPE_fbef48a9042141c2b33d0fe295021fba()
        : base(typeof (ContainerCreature))
      {
        this.AddMember("get_team", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_team", typeof (ContainerCreature), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_fbef48a9042141c2b33d0fe295021fba.MTHD_f43e6b5c6a694cdf80ab057201cdfe14()
        }));
        this.AddMember("get_name", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_name", typeof (ContainerCreature), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_fbef48a9042141c2b33d0fe295021fba.MTHD_fcb14aa90c4d4af6b8c6c139204e5f00()
        }));
        this.AddMember("get_spellEnum", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_spellEnum", typeof (ContainerCreature), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_fbef48a9042141c2b33d0fe295021fba.MTHD_d9cb5400aa1a45a8a5860d30c2af430b()
        }));
        this.AddMember("get_health", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_health", typeof (ContainerCreature), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_fbef48a9042141c2b33d0fe295021fba.MTHD_76ffd1497f044e5e8847f7ccaf723136()
        }));
        this.AddMember("set_health", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("set_health", typeof (ContainerCreature), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_fbef48a9042141c2b33d0fe295021fba.MTHD_7ef8f88fd6414f2aaf9e9bb121a2a0be()
        }));
        this.AddMember("get_maxHealth", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_maxHealth", typeof (ContainerCreature), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_fbef48a9042141c2b33d0fe295021fba.MTHD_c3e643f052fd4c32be8c66afb738b25b()
        }));
        this.AddMember("set_maxHealth", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("set_maxHealth", typeof (ContainerCreature), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_fbef48a9042141c2b33d0fe295021fba.MTHD_1c9521de921c4ff59a58e4adeda051a4()
        }));
        this.AddMember("get_healthAndTower", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_healthAndTower", typeof (ContainerCreature), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_fbef48a9042141c2b33d0fe295021fba.MTHD_097d3cf6ebfb4fcf8c5cda9cffbaa466()
        }));
        this.AddMember("get_shield", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_shield", typeof (ContainerCreature), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_fbef48a9042141c2b33d0fe295021fba.MTHD_12375515bfea465e88abcb35666d6483()
        }));
        this.AddMember("set_shield", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("set_shield", typeof (ContainerCreature), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_fbef48a9042141c2b33d0fe295021fba.MTHD_5a1306d8051443f59876a7c8adbe04a9()
        }));
        this.AddMember("get_x", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_x", typeof (ContainerCreature), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_fbef48a9042141c2b33d0fe295021fba.MTHD_eee8dd0cbba7446580e830ceb97437db()
        }));
        this.AddMember("set_x", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("set_x", typeof (ContainerCreature), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_fbef48a9042141c2b33d0fe295021fba.MTHD_a7f0954f03834b93a11e2482ef9d90d8()
        }));
        this.AddMember("get_y", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_y", typeof (ContainerCreature), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_fbef48a9042141c2b33d0fe295021fba.MTHD_8caeb62ccef74c07a746fcd08d81b9e3()
        }));
        this.AddMember("set_y", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("set_y", typeof (ContainerCreature), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_fbef48a9042141c2b33d0fe295021fba.MTHD_ef0fbf6cce7c45e8897773fbe807cf00()
        }));
        this.AddMember("get_position", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_position", typeof (ContainerCreature), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_fbef48a9042141c2b33d0fe295021fba.MTHD_6b11fff2493448cab4dd95c9b71f6101()
        }));
        this.AddMember("set_position", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("set_position", typeof (ContainerCreature), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_fbef48a9042141c2b33d0fe295021fba.MTHD_f20eb71819384169976fcb831943456e()
        }));
        this.AddMember("get_parent", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_parent", typeof (ContainerCreature), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_fbef48a9042141c2b33d0fe295021fba.MTHD_b7972fc43ae44a33b1f28ca8cd37507e()
        }));
        this.AddMember("set_parent", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("set_parent", typeof (ContainerCreature), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_fbef48a9042141c2b33d0fe295021fba.MTHD_0fc1dad22e504693b2b7ad6775b54f34()
        }));
        this.AddMember("get_weight", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_weight", typeof (ContainerCreature), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_fbef48a9042141c2b33d0fe295021fba.MTHD_849fc85974f746f6ad4fac4d97075335()
        }));
        this.AddMember("set_weight", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("set_weight", typeof (ContainerCreature), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_fbef48a9042141c2b33d0fe295021fba.MTHD_0a54797583a54943a87cc3efd8ee7b2b()
        }));
        this.AddMember("get_radius", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_radius", typeof (ContainerCreature), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_fbef48a9042141c2b33d0fe295021fba.MTHD_9c335b58ab3b43ed9ea0808571713caa()
        }));
        this.AddMember("set_radius", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("set_radius", typeof (ContainerCreature), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_fbef48a9042141c2b33d0fe295021fba.MTHD_8664d999df874ae08d7e09a6db6b3cfd()
        }));
        this.AddMember("get_inTower", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_inTower", typeof (ContainerCreature), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_fbef48a9042141c2b33d0fe295021fba.MTHD_b1b309234a774aaaa012b77c01046525()
        }));
        this.AddMember("get_isMoving", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_isMoving", typeof (ContainerCreature), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_fbef48a9042141c2b33d0fe295021fba.MTHD_61eb7b0c88aa46fea5280b33aebb0444()
        }));
        this.AddMember("get_stunned", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_stunned", typeof (ContainerCreature), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_fbef48a9042141c2b33d0fe295021fba.MTHD_ead37d9b0d1e4e138fd1a975d2499b2e()
        }));
        this.AddMember("set_stunned", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("set_stunned", typeof (ContainerCreature), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_fbef48a9042141c2b33d0fe295021fba.MTHD_6ee8f1f5c3cd44bb9290e52aa416e22e()
        }));
        this.AddMember("get_superStunned", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_superStunned", typeof (ContainerCreature), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_fbef48a9042141c2b33d0fe295021fba.MTHD_bba1ddfc917a4b2ca6b6473346f7bd9f()
        }));
        this.AddMember("set_superStunned", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("set_superStunned", typeof (ContainerCreature), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_fbef48a9042141c2b33d0fe295021fba.MTHD_9afab20e6cb14f7088ecb774a768d49c()
        }));
        this.AddMember("get_waterWalking", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_waterWalking", typeof (ContainerCreature), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_fbef48a9042141c2b33d0fe295021fba.MTHD_2463c9606244437fa6369cc1f045ecbf()
        }));
        this.AddMember("set_waterWalking", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("set_waterWalking", typeof (ContainerCreature), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_fbef48a9042141c2b33d0fe295021fba.MTHD_15bb612a5e2d404cbad96a2423890ba9()
        }));
        this.AddMember("get_frostWalking", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_frostWalking", typeof (ContainerCreature), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_fbef48a9042141c2b33d0fe295021fba.MTHD_3a9f9225c8f94b5cbbdfeb4827982aa9()
        }));
        this.AddMember("set_frostWalking", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("set_frostWalking", typeof (ContainerCreature), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_fbef48a9042141c2b33d0fe295021fba.MTHD_265beee427ab45d69cc5dbe1131a6c73()
        }));
        this.AddMember("get_inWater", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_inWater", typeof (ContainerCreature), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_fbef48a9042141c2b33d0fe295021fba.MTHD_8c323e00e8d644d5a80364d4343306da()
        }));
        this.AddMember("get_diesInWater", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_diesInWater", typeof (ContainerCreature), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_fbef48a9042141c2b33d0fe295021fba.MTHD_d865285cd70b46349602c0dbffddfd77()
        }));
        this.AddMember("set_diesInWater", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("set_diesInWater", typeof (ContainerCreature), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_fbef48a9042141c2b33d0fe295021fba.MTHD_3cc52fdcd84544d6abecb32258e07e01()
        }));
        this.AddMember("get_type", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_type", typeof (ContainerCreature), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_fbef48a9042141c2b33d0fe295021fba.MTHD_a7c30c60e3644ec6b9c08e9637e99ea7()
        }));
        this.AddMember("set_type", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("set_type", typeof (ContainerCreature), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_fbef48a9042141c2b33d0fe295021fba.MTHD_f6b0aad4781148f7bbb9d8425d1e9ce9()
        }));
        this.AddMember("get_race", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_race", typeof (ContainerCreature), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_fbef48a9042141c2b33d0fe295021fba.MTHD_d9eb64a78e014e89b5a2cbb5f94dd42f()
        }));
        this.AddMember("set_race", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("set_race", typeof (ContainerCreature), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_fbef48a9042141c2b33d0fe295021fba.MTHD_247aaa80c31e4c00996b087aa5dfdb58()
        }));
        this.AddMember("get_yourTurn", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_yourTurn", typeof (ContainerCreature), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_fbef48a9042141c2b33d0fe295021fba.MTHD_dfc66f46f9aa4b5d9594029e7f24aa77()
        }));
        this.AddMember("get_isRider", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_isRider", typeof (ContainerCreature), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_fbef48a9042141c2b33d0fe295021fba.MTHD_7455793ad49c4241b8ec173e4c020f64()
        }));
        this.AddMember("get_isMounted", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_isMounted", typeof (ContainerCreature), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_fbef48a9042141c2b33d0fe295021fba.MTHD_23c715d2b7414e94a204f3317f53a1c7()
        }));
        this.AddMember("get_isDead", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_isDead", typeof (ContainerCreature), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_fbef48a9042141c2b33d0fe295021fba.MTHD_37a487d8323d4706bc85ba282c726bd0()
        }));
        this.AddMember("get_isPawn", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_isPawn", typeof (ContainerCreature), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_fbef48a9042141c2b33d0fe295021fba.MTHD_03bd3bb00c7e49c79b1eabce9324fc60()
        }));
        this.AddMember("get_isFlying", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_isFlying", typeof (ContainerCreature), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_fbef48a9042141c2b33d0fe295021fba.MTHD_9575ef605dcb44659c09336f15c86584()
        }));
        this.AddMember("set_isFlying", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("set_isFlying", typeof (ContainerCreature), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_fbef48a9042141c2b33d0fe295021fba.MTHD_8d485702d8f2485d974933f3ae92d3c3()
        }));
        this.AddMember("get_isMountable", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_isMountable", typeof (ContainerCreature), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_fbef48a9042141c2b33d0fe295021fba.MTHD_2e2586948c3b43ff9bf52d452863131b()
        }));
        this.AddMember("get_canMount", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_canMount", typeof (ContainerCreature), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_fbef48a9042141c2b33d0fe295021fba.MTHD_c9e1f98f545e479096d666ded2e07c57()
        }));
        this.AddMember("get_arcaneMonster", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_arcaneMonster", typeof (ContainerCreature), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_fbef48a9042141c2b33d0fe295021fba.MTHD_6bf3309996e24d7690652589f654d090()
        }));
        this.AddMember("set_arcaneMonster", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("set_arcaneMonster", typeof (ContainerCreature), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_fbef48a9042141c2b33d0fe295021fba.MTHD_de6094eb77314a279a0beae080a03998()
        }));
        this.AddMember("get_takeDamage", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_takeDamage", typeof (ContainerCreature), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_fbef48a9042141c2b33d0fe295021fba.MTHD_ce137519e18044929cc0aeb28e26c40d()
        }));
        this.AddMember("set_takeDamage", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("set_takeDamage", typeof (ContainerCreature), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_fbef48a9042141c2b33d0fe295021fba.MTHD_bce6e2af9d694f1aac843c7129814e2c()
        }));
        this.AddMember("getRider", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("getRider", typeof (ContainerCreature), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_fbef48a9042141c2b33d0fe295021fba.MTHD_7cce0e56e20240a0b909c216267ae026()
        }));
        this.AddMember("getMount", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("getMount", typeof (ContainerCreature), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_fbef48a9042141c2b33d0fe295021fba.MTHD_996797e34dd3458b8f426ad480be872e()
        }));
        this.AddMember("getTower", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("getTower", typeof (ContainerCreature), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_fbef48a9042141c2b33d0fe295021fba.MTHD_36fbe94339ad44bda114dcce122ec172()
        }));
        this.AddMember("LongJump", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("LongJump", typeof (ContainerCreature), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_fbef48a9042141c2b33d0fe295021fba.MTHD_40b63161b3e340ae936e570c62bf0a2b()
        }));
        this.AddMember("HighJump", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("HighJump", typeof (ContainerCreature), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_fbef48a9042141c2b33d0fe295021fba.MTHD_1096f241e33c46d6b6f8d170a11cab38()
        }));
        this.AddMember("Walk", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("Walk", typeof (ContainerCreature), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_fbef48a9042141c2b33d0fe295021fba.MTHD_ba21eba0e4704423bad54c77a781dc84()
        }));
        this.AddMember("setDirection", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("setDirection", typeof (ContainerCreature), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_fbef48a9042141c2b33d0fe295021fba.MTHD_f47b14912d24418886b5a49e21bbf5bf()
        }));
        this.AddMember("getFamiliarLevel", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("getFamiliarLevel", typeof (ContainerCreature), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_fbef48a9042141c2b33d0fe295021fba.MTHD_09fe846257a54f6b8823d6b12bec985e()
        }));
        this.AddMember("getActivateableFamiliarLevel", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("getActivateableFamiliarLevel", typeof (ContainerCreature), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_fbef48a9042141c2b33d0fe295021fba.MTHD_bbd313d9d5c64930b9f4a6b725ea898a()
        }));
        this.AddMember("getActivateableFamiliarBookID", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("getActivateableFamiliarBookID", typeof (ContainerCreature), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_fbef48a9042141c2b33d0fe295021fba.MTHD_3ac2e4a20410464f9627e89878ee3904()
        }));
        this.AddMember("increaseFamiliarLevel", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("increaseFamiliarLevel", typeof (ContainerCreature), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_fbef48a9042141c2b33d0fe295021fba.MTHD_ed74447a6b724c9d8a6910bb5920af45()
        }));
        this.AddMember("inside", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("inside", typeof (ContainerCreature), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[2]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_fbef48a9042141c2b33d0fe295021fba.MTHD_02f40ede7c7543b1a103f2bbfd1daa82(),
          (IOverloadableMemberDescriptor) new Bridge.TYPE_fbef48a9042141c2b33d0fe295021fba.MTHD_0c429b1676f74af09ccfc0ebb1651c4e()
        }));
        this.AddMember("overlap", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("overlap", typeof (ContainerCreature), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[2]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_fbef48a9042141c2b33d0fe295021fba.MTHD_bac5a77aa3ea4b4d87b7ee63c01210d3(),
          (IOverloadableMemberDescriptor) new Bridge.TYPE_fbef48a9042141c2b33d0fe295021fba.MTHD_4db1f06045cc4729b4c51fa3da89dfa6()
        }));
        this.AddMember("die", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("die", typeof (ContainerCreature), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_fbef48a9042141c2b33d0fe295021fba.MTHD_c3bc342d40bb4686b71ef46e0d7214cd()
        }));
        this.AddMember("addSpell", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("addSpell", typeof (ContainerCreature), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_fbef48a9042141c2b33d0fe295021fba.MTHD_d594e10e6cec4aedb6dd2f714942d503()
        }));
        this.AddMember("removeSpell", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("removeSpell", typeof (ContainerCreature), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[2]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_fbef48a9042141c2b33d0fe295021fba.MTHD_1de84d75283940fbaa48e48f1ece642b(),
          (IOverloadableMemberDescriptor) new Bridge.TYPE_fbef48a9042141c2b33d0fe295021fba.MTHD_04aeb506af59457f98c3c8440a084e5f()
        }));
        this.AddMember("getSpells", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("getSpells", typeof (ContainerCreature), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_fbef48a9042141c2b33d0fe295021fba.MTHD_c42fb041ecfa42348ab8bf0e3f97a60b()
        }));
        this.AddMember("getSpell", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("getSpell", typeof (ContainerCreature), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_fbef48a9042141c2b33d0fe295021fba.MTHD_959e1f1aeae2446a81ab27f163a6383b()
        }));
        this.AddMember("getSpellCount", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("getSpellCount", typeof (ContainerCreature), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_fbef48a9042141c2b33d0fe295021fba.MTHD_3100c3bd5e704704bfe61dbe860fcae2()
        }));
        this.AddMember("castedCount", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("castedCount", typeof (ContainerCreature), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_fbef48a9042141c2b33d0fe295021fba.MTHD_5a699f756cc3457fa4bcfc9705b06faf()
        }));
        this.AddMember("castedCountThisTurn", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("castedCountThisTurn", typeof (ContainerCreature), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_fbef48a9042141c2b33d0fe295021fba.MTHD_91d2cc3795bd4ccba7a063a736954669()
        }));
        this.AddMember("castedDamage", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("castedDamage", typeof (ContainerCreature), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_fbef48a9042141c2b33d0fe295021fba.MTHD_abd32a28c5134e149a959bff4d0f068e()
        }));
        this.AddMember("getMinionCount", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("getMinionCount", typeof (ContainerCreature), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_fbef48a9042141c2b33d0fe295021fba.MTHD_d0c8524d49f9464e8a8c4ba1ec29de3f()
        }));
        this.AddMember("castSpell", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("castSpell", typeof (ContainerCreature), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_fbef48a9042141c2b33d0fe295021fba.MTHD_06b85dd563794e89aeac890e1767cdb4()
        }));
        this.AddMember("fireAt", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("fireAt", typeof (ContainerCreature), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[2]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_fbef48a9042141c2b33d0fe295021fba.MTHD_6d8963e289e9449991b6bcace7b3b6e1(),
          (IOverloadableMemberDescriptor) new Bridge.TYPE_fbef48a9042141c2b33d0fe295021fba.MTHD_92e969ec1b684e68aa47367bc1642bcf()
        }));
        this.AddMember("getEffectors", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("getEffectors", typeof (ContainerCreature), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[2]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_fbef48a9042141c2b33d0fe295021fba.MTHD_3fea25ddeeb2465f880f3847b228ea04(),
          (IOverloadableMemberDescriptor) new Bridge.TYPE_fbef48a9042141c2b33d0fe295021fba.MTHD_5f6053bcfaf74010a2da470f75b2b93b()
        }));
        this.AddMember("Equals", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("Equals", typeof (ContainerCreature), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_fbef48a9042141c2b33d0fe295021fba.MTHD_1ac3660148bf44329498c09cfa2dd0b7()
        }));
        this.AddMember("GetHashCode", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("GetHashCode", typeof (ContainerCreature), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_fbef48a9042141c2b33d0fe295021fba.MTHD_a3883c55b1004631b283b2f953259b25()
        }));
        this.AddMember("op_Equality", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("op_Equality", typeof (ContainerCreature), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_fbef48a9042141c2b33d0fe295021fba.MTHD_130ed3131aa74f5abe1c2b0583da3b1e()
        }));
        this.AddMember("op_Inequality", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("op_Inequality", typeof (ContainerCreature), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_fbef48a9042141c2b33d0fe295021fba.MTHD_8e8b84bd5d564199b80093583a0c638d()
        }));
        this.AddMember("GetType", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("GetType", typeof (ContainerCreature), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_fbef48a9042141c2b33d0fe295021fba.MTHD_e107639a7161484cbe272e697cb75f5c()
        }));
        this.AddMember("ToString", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("ToString", typeof (ContainerCreature), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_fbef48a9042141c2b33d0fe295021fba.MTHD_93b49ade54d74f289c077bc883ab2e74()
        }));
        this.AddMember("team", (IMemberDescriptor) new Bridge.TYPE_fbef48a9042141c2b33d0fe295021fba.PROP_da7d3bbe44b74fbaad4b6da54ac1fee5());
        this.AddMember("name", (IMemberDescriptor) new Bridge.TYPE_fbef48a9042141c2b33d0fe295021fba.PROP_eec1ed6d9f5f42c19743e8f72fd2eeba());
        this.AddMember("spellEnum", (IMemberDescriptor) new Bridge.TYPE_fbef48a9042141c2b33d0fe295021fba.PROP_3de14e66283d4db7a0025899c6b90a95());
        this.AddMember("health", (IMemberDescriptor) new Bridge.TYPE_fbef48a9042141c2b33d0fe295021fba.PROP_2171f7eda0094257af0b8c2c20965038());
        this.AddMember("maxHealth", (IMemberDescriptor) new Bridge.TYPE_fbef48a9042141c2b33d0fe295021fba.PROP_ec308edb8912458eb40bcda4b9b7881a());
        this.AddMember("healthAndTower", (IMemberDescriptor) new Bridge.TYPE_fbef48a9042141c2b33d0fe295021fba.PROP_0a5675a55c4841d2872e23124aa77a80());
        this.AddMember("shield", (IMemberDescriptor) new Bridge.TYPE_fbef48a9042141c2b33d0fe295021fba.PROP_898d756723a94a0d9f8dc37d33df6ac1());
        this.AddMember("x", (IMemberDescriptor) new Bridge.TYPE_fbef48a9042141c2b33d0fe295021fba.PROP_9b2a055b903b4ef4a03890830ba1df35());
        this.AddMember("y", (IMemberDescriptor) new Bridge.TYPE_fbef48a9042141c2b33d0fe295021fba.PROP_ed8f035a74c54732bf8b9ff99e951e53());
        this.AddMember("position", (IMemberDescriptor) new Bridge.TYPE_fbef48a9042141c2b33d0fe295021fba.PROP_7e3e3ac65c584873ad1dd5cd4f000749());
        this.AddMember("parent", (IMemberDescriptor) new Bridge.TYPE_fbef48a9042141c2b33d0fe295021fba.PROP_7a7abe7b6f0e48638d427818610f22a7());
        this.AddMember("weight", (IMemberDescriptor) new Bridge.TYPE_fbef48a9042141c2b33d0fe295021fba.PROP_8ba77f2f1f2349079ad08b8359597b90());
        this.AddMember("radius", (IMemberDescriptor) new Bridge.TYPE_fbef48a9042141c2b33d0fe295021fba.PROP_d40894f65ff14bd886bca065071d074b());
        this.AddMember("inTower", (IMemberDescriptor) new Bridge.TYPE_fbef48a9042141c2b33d0fe295021fba.PROP_c5fb12310be8414ba06876dc83ea779d());
        this.AddMember("isMoving", (IMemberDescriptor) new Bridge.TYPE_fbef48a9042141c2b33d0fe295021fba.PROP_9ffe4c7f39ac4e7ca14be64b9503918b());
        this.AddMember("stunned", (IMemberDescriptor) new Bridge.TYPE_fbef48a9042141c2b33d0fe295021fba.PROP_23872424c7154760ad7164dd79a9aff6());
        this.AddMember("superStunned", (IMemberDescriptor) new Bridge.TYPE_fbef48a9042141c2b33d0fe295021fba.PROP_94351b0c9d654b73a16a9173d87ee727());
        this.AddMember("waterWalking", (IMemberDescriptor) new Bridge.TYPE_fbef48a9042141c2b33d0fe295021fba.PROP_1b080ef483ae4967835478e735492c40());
        this.AddMember("frostWalking", (IMemberDescriptor) new Bridge.TYPE_fbef48a9042141c2b33d0fe295021fba.PROP_ddeaad2179dc4b1da8bc08e29085ff0a());
        this.AddMember("inWater", (IMemberDescriptor) new Bridge.TYPE_fbef48a9042141c2b33d0fe295021fba.PROP_4c9a67fee9e94672b16716af964ae6aa());
        this.AddMember("diesInWater", (IMemberDescriptor) new Bridge.TYPE_fbef48a9042141c2b33d0fe295021fba.PROP_7f332fbd9abb415e87c8d6da5393fdc5());
        this.AddMember("type", (IMemberDescriptor) new Bridge.TYPE_fbef48a9042141c2b33d0fe295021fba.PROP_ce005b71f07a4b5aa9769ca618d20bd9());
        this.AddMember("race", (IMemberDescriptor) new Bridge.TYPE_fbef48a9042141c2b33d0fe295021fba.PROP_de5fe28b126b4cfbb25ee0a87666135f());
        this.AddMember("yourTurn", (IMemberDescriptor) new Bridge.TYPE_fbef48a9042141c2b33d0fe295021fba.PROP_34bf80af520643f69a6cbf8550376b50());
        this.AddMember("isRider", (IMemberDescriptor) new Bridge.TYPE_fbef48a9042141c2b33d0fe295021fba.PROP_30a28d127f2147bd8bc058ea065cc3de());
        this.AddMember("isMounted", (IMemberDescriptor) new Bridge.TYPE_fbef48a9042141c2b33d0fe295021fba.PROP_fe3000bc04d845e5a0715a1b3d267523());
        this.AddMember("isDead", (IMemberDescriptor) new Bridge.TYPE_fbef48a9042141c2b33d0fe295021fba.PROP_894f2f7250a04f25b3ba3a3ee5d7edf4());
        this.AddMember("isPawn", (IMemberDescriptor) new Bridge.TYPE_fbef48a9042141c2b33d0fe295021fba.PROP_e8042d5084c84d4a9cbd35b2eed457f1());
        this.AddMember("isFlying", (IMemberDescriptor) new Bridge.TYPE_fbef48a9042141c2b33d0fe295021fba.PROP_4ac12697b3ed404c842843f911a84adb());
        this.AddMember("isMountable", (IMemberDescriptor) new Bridge.TYPE_fbef48a9042141c2b33d0fe295021fba.PROP_e62aa8d1c05f440a9f8a01d30ee42792());
        this.AddMember("canMount", (IMemberDescriptor) new Bridge.TYPE_fbef48a9042141c2b33d0fe295021fba.PROP_683e41ffddd44eceac85b7ccfdf190b5());
        this.AddMember("arcaneMonster", (IMemberDescriptor) new Bridge.TYPE_fbef48a9042141c2b33d0fe295021fba.PROP_af1b34cf5b61463294f8b8206863caf4());
        this.AddMember("takeDamage", (IMemberDescriptor) new Bridge.TYPE_fbef48a9042141c2b33d0fe295021fba.PROP_c34956a45e0f4c63a45ca37afb579ed5());
      }

      private sealed class MTHD_f43e6b5c6a694cdf80ab057201cdfe14 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_f43e6b5c6a694cdf80ab057201cdfe14()
        {
          this.Initialize("get_team", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerCreature) obj).team;
        }
      }

      private sealed class MTHD_fcb14aa90c4d4af6b8c6c139204e5f00 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_fcb14aa90c4d4af6b8c6c139204e5f00()
        {
          this.Initialize("get_name", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerCreature) obj).name;
        }
      }

      private sealed class MTHD_d9cb5400aa1a45a8a5860d30c2af430b : HardwiredMethodMemberDescriptor
      {
        internal MTHD_d9cb5400aa1a45a8a5860d30c2af430b()
        {
          this.Initialize("get_spellEnum", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerCreature) obj).spellEnum;
        }
      }

      private sealed class MTHD_76ffd1497f044e5e8847f7ccaf723136 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_76ffd1497f044e5e8847f7ccaf723136()
        {
          this.Initialize("get_health", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerCreature) obj).health;
        }
      }

      private sealed class MTHD_7ef8f88fd6414f2aaf9e9bb121a2a0be : HardwiredMethodMemberDescriptor
      {
        internal MTHD_7ef8f88fd6414f2aaf9e9bb121a2a0be()
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

      private sealed class MTHD_c3e643f052fd4c32be8c66afb738b25b : HardwiredMethodMemberDescriptor
      {
        internal MTHD_c3e643f052fd4c32be8c66afb738b25b()
        {
          this.Initialize("get_maxHealth", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerCreature) obj).maxHealth;
        }
      }

      private sealed class MTHD_1c9521de921c4ff59a58e4adeda051a4 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_1c9521de921c4ff59a58e4adeda051a4()
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

      private sealed class MTHD_097d3cf6ebfb4fcf8c5cda9cffbaa466 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_097d3cf6ebfb4fcf8c5cda9cffbaa466()
        {
          this.Initialize("get_healthAndTower", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerCreature) obj).healthAndTower;
        }
      }

      private sealed class MTHD_12375515bfea465e88abcb35666d6483 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_12375515bfea465e88abcb35666d6483()
        {
          this.Initialize("get_shield", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerCreature) obj).shield;
        }
      }

      private sealed class MTHD_5a1306d8051443f59876a7c8adbe04a9 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_5a1306d8051443f59876a7c8adbe04a9()
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

      private sealed class MTHD_eee8dd0cbba7446580e830ceb97437db : HardwiredMethodMemberDescriptor
      {
        internal MTHD_eee8dd0cbba7446580e830ceb97437db()
        {
          this.Initialize("get_x", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerCreature) obj).x;
        }
      }

      private sealed class MTHD_a7f0954f03834b93a11e2482ef9d90d8 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_a7f0954f03834b93a11e2482ef9d90d8()
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

      private sealed class MTHD_8caeb62ccef74c07a746fcd08d81b9e3 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_8caeb62ccef74c07a746fcd08d81b9e3()
        {
          this.Initialize("get_y", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerCreature) obj).y;
        }
      }

      private sealed class MTHD_ef0fbf6cce7c45e8897773fbe807cf00 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_ef0fbf6cce7c45e8897773fbe807cf00()
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

      private sealed class MTHD_6b11fff2493448cab4dd95c9b71f6101 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_6b11fff2493448cab4dd95c9b71f6101()
        {
          this.Initialize("get_position", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerCreature) obj).position;
        }
      }

      private sealed class MTHD_f20eb71819384169976fcb831943456e : HardwiredMethodMemberDescriptor
      {
        internal MTHD_f20eb71819384169976fcb831943456e()
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

      private sealed class MTHD_b7972fc43ae44a33b1f28ca8cd37507e : HardwiredMethodMemberDescriptor
      {
        internal MTHD_b7972fc43ae44a33b1f28ca8cd37507e()
        {
          this.Initialize("get_parent", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerCreature) obj).parent;
        }
      }

      private sealed class MTHD_0fc1dad22e504693b2b7ad6775b54f34 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_0fc1dad22e504693b2b7ad6775b54f34()
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

      private sealed class MTHD_849fc85974f746f6ad4fac4d97075335 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_849fc85974f746f6ad4fac4d97075335()
        {
          this.Initialize("get_weight", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerCreature) obj).weight;
        }
      }

      private sealed class MTHD_0a54797583a54943a87cc3efd8ee7b2b : HardwiredMethodMemberDescriptor
      {
        internal MTHD_0a54797583a54943a87cc3efd8ee7b2b()
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

      private sealed class MTHD_9c335b58ab3b43ed9ea0808571713caa : HardwiredMethodMemberDescriptor
      {
        internal MTHD_9c335b58ab3b43ed9ea0808571713caa()
        {
          this.Initialize("get_radius", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerCreature) obj).radius;
        }
      }

      private sealed class MTHD_8664d999df874ae08d7e09a6db6b3cfd : HardwiredMethodMemberDescriptor
      {
        internal MTHD_8664d999df874ae08d7e09a6db6b3cfd()
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

      private sealed class MTHD_b1b309234a774aaaa012b77c01046525 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_b1b309234a774aaaa012b77c01046525()
        {
          this.Initialize("get_inTower", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerCreature) obj).inTower;
        }
      }

      private sealed class MTHD_61eb7b0c88aa46fea5280b33aebb0444 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_61eb7b0c88aa46fea5280b33aebb0444()
        {
          this.Initialize("get_isMoving", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerCreature) obj).isMoving;
        }
      }

      private sealed class MTHD_ead37d9b0d1e4e138fd1a975d2499b2e : HardwiredMethodMemberDescriptor
      {
        internal MTHD_ead37d9b0d1e4e138fd1a975d2499b2e()
        {
          this.Initialize("get_stunned", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerCreature) obj).stunned;
        }
      }

      private sealed class MTHD_6ee8f1f5c3cd44bb9290e52aa416e22e : HardwiredMethodMemberDescriptor
      {
        internal MTHD_6ee8f1f5c3cd44bb9290e52aa416e22e()
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

      private sealed class MTHD_bba1ddfc917a4b2ca6b6473346f7bd9f : HardwiredMethodMemberDescriptor
      {
        internal MTHD_bba1ddfc917a4b2ca6b6473346f7bd9f()
        {
          this.Initialize("get_superStunned", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerCreature) obj).superStunned;
        }
      }

      private sealed class MTHD_9afab20e6cb14f7088ecb774a768d49c : HardwiredMethodMemberDescriptor
      {
        internal MTHD_9afab20e6cb14f7088ecb774a768d49c()
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

      private sealed class MTHD_2463c9606244437fa6369cc1f045ecbf : HardwiredMethodMemberDescriptor
      {
        internal MTHD_2463c9606244437fa6369cc1f045ecbf()
        {
          this.Initialize("get_waterWalking", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerCreature) obj).waterWalking;
        }
      }

      private sealed class MTHD_15bb612a5e2d404cbad96a2423890ba9 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_15bb612a5e2d404cbad96a2423890ba9()
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

      private sealed class MTHD_3a9f9225c8f94b5cbbdfeb4827982aa9 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_3a9f9225c8f94b5cbbdfeb4827982aa9()
        {
          this.Initialize("get_frostWalking", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerCreature) obj).frostWalking;
        }
      }

      private sealed class MTHD_265beee427ab45d69cc5dbe1131a6c73 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_265beee427ab45d69cc5dbe1131a6c73()
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

      private sealed class MTHD_8c323e00e8d644d5a80364d4343306da : HardwiredMethodMemberDescriptor
      {
        internal MTHD_8c323e00e8d644d5a80364d4343306da()
        {
          this.Initialize("get_inWater", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerCreature) obj).inWater;
        }
      }

      private sealed class MTHD_d865285cd70b46349602c0dbffddfd77 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_d865285cd70b46349602c0dbffddfd77()
        {
          this.Initialize("get_diesInWater", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerCreature) obj).diesInWater;
        }
      }

      private sealed class MTHD_3cc52fdcd84544d6abecb32258e07e01 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_3cc52fdcd84544d6abecb32258e07e01()
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

      private sealed class MTHD_a7c30c60e3644ec6b9c08e9637e99ea7 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_a7c30c60e3644ec6b9c08e9637e99ea7()
        {
          this.Initialize("get_type", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerCreature) obj).type;
        }
      }

      private sealed class MTHD_f6b0aad4781148f7bbb9d8425d1e9ce9 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_f6b0aad4781148f7bbb9d8425d1e9ce9()
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

      private sealed class MTHD_d9eb64a78e014e89b5a2cbb5f94dd42f : HardwiredMethodMemberDescriptor
      {
        internal MTHD_d9eb64a78e014e89b5a2cbb5f94dd42f()
        {
          this.Initialize("get_race", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerCreature) obj).race;
        }
      }

      private sealed class MTHD_247aaa80c31e4c00996b087aa5dfdb58 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_247aaa80c31e4c00996b087aa5dfdb58()
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

      private sealed class MTHD_dfc66f46f9aa4b5d9594029e7f24aa77 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_dfc66f46f9aa4b5d9594029e7f24aa77()
        {
          this.Initialize("get_yourTurn", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerCreature) obj).yourTurn;
        }
      }

      private sealed class MTHD_7455793ad49c4241b8ec173e4c020f64 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_7455793ad49c4241b8ec173e4c020f64()
        {
          this.Initialize("get_isRider", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerCreature) obj).isRider;
        }
      }

      private sealed class MTHD_23c715d2b7414e94a204f3317f53a1c7 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_23c715d2b7414e94a204f3317f53a1c7()
        {
          this.Initialize("get_isMounted", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerCreature) obj).isMounted;
        }
      }

      private sealed class MTHD_37a487d8323d4706bc85ba282c726bd0 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_37a487d8323d4706bc85ba282c726bd0()
        {
          this.Initialize("get_isDead", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerCreature) obj).isDead;
        }
      }

      private sealed class MTHD_03bd3bb00c7e49c79b1eabce9324fc60 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_03bd3bb00c7e49c79b1eabce9324fc60()
        {
          this.Initialize("get_isPawn", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerCreature) obj).isPawn;
        }
      }

      private sealed class MTHD_9575ef605dcb44659c09336f15c86584 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_9575ef605dcb44659c09336f15c86584()
        {
          this.Initialize("get_isFlying", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerCreature) obj).isFlying;
        }
      }

      private sealed class MTHD_8d485702d8f2485d974933f3ae92d3c3 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_8d485702d8f2485d974933f3ae92d3c3()
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

      private sealed class MTHD_2e2586948c3b43ff9bf52d452863131b : HardwiredMethodMemberDescriptor
      {
        internal MTHD_2e2586948c3b43ff9bf52d452863131b()
        {
          this.Initialize("get_isMountable", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerCreature) obj).isMountable;
        }
      }

      private sealed class MTHD_c9e1f98f545e479096d666ded2e07c57 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_c9e1f98f545e479096d666ded2e07c57()
        {
          this.Initialize("get_canMount", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerCreature) obj).canMount;
        }
      }

      private sealed class MTHD_6bf3309996e24d7690652589f654d090 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_6bf3309996e24d7690652589f654d090()
        {
          this.Initialize("get_arcaneMonster", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerCreature) obj).arcaneMonster;
        }
      }

      private sealed class MTHD_de6094eb77314a279a0beae080a03998 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_de6094eb77314a279a0beae080a03998()
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

      private sealed class MTHD_ce137519e18044929cc0aeb28e26c40d : HardwiredMethodMemberDescriptor
      {
        internal MTHD_ce137519e18044929cc0aeb28e26c40d()
        {
          this.Initialize("get_takeDamage", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerCreature) obj).takeDamage;
        }
      }

      private sealed class MTHD_bce6e2af9d694f1aac843c7129814e2c : HardwiredMethodMemberDescriptor
      {
        internal MTHD_bce6e2af9d694f1aac843c7129814e2c()
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

      private sealed class MTHD_7cce0e56e20240a0b909c216267ae026 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_7cce0e56e20240a0b909c216267ae026()
        {
          this.Initialize("getRider", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerCreature) obj).getRider();
        }
      }

      private sealed class MTHD_996797e34dd3458b8f426ad480be872e : HardwiredMethodMemberDescriptor
      {
        internal MTHD_996797e34dd3458b8f426ad480be872e()
        {
          this.Initialize("getMount", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerCreature) obj).getMount();
        }
      }

      private sealed class MTHD_36fbe94339ad44bda114dcce122ec172 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_36fbe94339ad44bda114dcce122ec172()
        {
          this.Initialize("getTower", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerCreature) obj).getTower();
        }
      }

      private sealed class MTHD_40b63161b3e340ae936e570c62bf0a2b : HardwiredMethodMemberDescriptor
      {
        internal MTHD_40b63161b3e340ae936e570c62bf0a2b()
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

      private sealed class MTHD_1096f241e33c46d6b6f8d170a11cab38 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_1096f241e33c46d6b6f8d170a11cab38()
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

      private sealed class MTHD_ba21eba0e4704423bad54c77a781dc84 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_ba21eba0e4704423bad54c77a781dc84()
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

      private sealed class MTHD_f47b14912d24418886b5a49e21bbf5bf : HardwiredMethodMemberDescriptor
      {
        internal MTHD_f47b14912d24418886b5a49e21bbf5bf()
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

      private sealed class MTHD_09fe846257a54f6b8823d6b12bec985e : HardwiredMethodMemberDescriptor
      {
        internal MTHD_09fe846257a54f6b8823d6b12bec985e()
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

      private sealed class MTHD_bbd313d9d5c64930b9f4a6b725ea898a : HardwiredMethodMemberDescriptor
      {
        internal MTHD_bbd313d9d5c64930b9f4a6b725ea898a()
        {
          this.Initialize("getActivateableFamiliarLevel", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerCreature) obj).getActivateableFamiliarLevel();
        }
      }

      private sealed class MTHD_3ac2e4a20410464f9627e89878ee3904 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_3ac2e4a20410464f9627e89878ee3904()
        {
          this.Initialize("getActivateableFamiliarBookID", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerCreature) obj).getActivateableFamiliarBookID();
        }
      }

      private sealed class MTHD_ed74447a6b724c9d8a6910bb5920af45 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_ed74447a6b724c9d8a6910bb5920af45()
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

      private sealed class MTHD_02f40ede7c7543b1a103f2bbfd1daa82 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_02f40ede7c7543b1a103f2bbfd1daa82()
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

      private sealed class MTHD_0c429b1676f74af09ccfc0ebb1651c4e : HardwiredMethodMemberDescriptor
      {
        internal MTHD_0c429b1676f74af09ccfc0ebb1651c4e()
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

      private sealed class MTHD_bac5a77aa3ea4b4d87b7ee63c01210d3 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_bac5a77aa3ea4b4d87b7ee63c01210d3()
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

      private sealed class MTHD_4db1f06045cc4729b4c51fa3da89dfa6 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_4db1f06045cc4729b4c51fa3da89dfa6()
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

      private sealed class MTHD_c3bc342d40bb4686b71ef46e0d7214cd : HardwiredMethodMemberDescriptor
      {
        internal MTHD_c3bc342d40bb4686b71ef46e0d7214cd()
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

      private sealed class MTHD_d594e10e6cec4aedb6dd2f714942d503 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_d594e10e6cec4aedb6dd2f714942d503()
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

      private sealed class MTHD_1de84d75283940fbaa48e48f1ece642b : HardwiredMethodMemberDescriptor
      {
        internal MTHD_1de84d75283940fbaa48e48f1ece642b()
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

      private sealed class MTHD_04aeb506af59457f98c3c8440a084e5f : HardwiredMethodMemberDescriptor
      {
        internal MTHD_04aeb506af59457f98c3c8440a084e5f()
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

      private sealed class MTHD_c42fb041ecfa42348ab8bf0e3f97a60b : HardwiredMethodMemberDescriptor
      {
        internal MTHD_c42fb041ecfa42348ab8bf0e3f97a60b()
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

      private sealed class MTHD_959e1f1aeae2446a81ab27f163a6383b : HardwiredMethodMemberDescriptor
      {
        internal MTHD_959e1f1aeae2446a81ab27f163a6383b()
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

      private sealed class MTHD_3100c3bd5e704704bfe61dbe860fcae2 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_3100c3bd5e704704bfe61dbe860fcae2()
        {
          this.Initialize("getSpellCount", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerCreature) obj).getSpellCount();
        }
      }

      private sealed class MTHD_5a699f756cc3457fa4bcfc9705b06faf : HardwiredMethodMemberDescriptor
      {
        internal MTHD_5a699f756cc3457fa4bcfc9705b06faf()
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

      private sealed class MTHD_91d2cc3795bd4ccba7a063a736954669 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_91d2cc3795bd4ccba7a063a736954669()
        {
          this.Initialize("castedCountThisTurn", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerCreature) obj).castedCountThisTurn();
        }
      }

      private sealed class MTHD_abd32a28c5134e149a959bff4d0f068e : HardwiredMethodMemberDescriptor
      {
        internal MTHD_abd32a28c5134e149a959bff4d0f068e()
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

      private sealed class MTHD_d0c8524d49f9464e8a8c4ba1ec29de3f : HardwiredMethodMemberDescriptor
      {
        internal MTHD_d0c8524d49f9464e8a8c4ba1ec29de3f()
        {
          this.Initialize("getMinionCount", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerCreature) obj).getMinionCount();
        }
      }

      private sealed class MTHD_06b85dd563794e89aeac890e1767cdb4 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_06b85dd563794e89aeac890e1767cdb4()
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

      private sealed class MTHD_6d8963e289e9449991b6bcace7b3b6e1 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_6d8963e289e9449991b6bcace7b3b6e1()
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

      private sealed class MTHD_92e969ec1b684e68aa47367bc1642bcf : HardwiredMethodMemberDescriptor
      {
        internal MTHD_92e969ec1b684e68aa47367bc1642bcf()
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

      private sealed class MTHD_3fea25ddeeb2465f880f3847b228ea04 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_3fea25ddeeb2465f880f3847b228ea04()
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

      private sealed class MTHD_5f6053bcfaf74010a2da470f75b2b93b : HardwiredMethodMemberDescriptor
      {
        internal MTHD_5f6053bcfaf74010a2da470f75b2b93b()
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

      private sealed class MTHD_1ac3660148bf44329498c09cfa2dd0b7 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_1ac3660148bf44329498c09cfa2dd0b7()
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

      private sealed class MTHD_a3883c55b1004631b283b2f953259b25 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_a3883c55b1004631b283b2f953259b25()
        {
          this.Initialize("GetHashCode", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerCreature) obj).GetHashCode();
        }
      }

      private sealed class MTHD_130ed3131aa74f5abe1c2b0583da3b1e : HardwiredMethodMemberDescriptor
      {
        internal MTHD_130ed3131aa74f5abe1c2b0583da3b1e()
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

      private sealed class MTHD_8e8b84bd5d564199b80093583a0c638d : HardwiredMethodMemberDescriptor
      {
        internal MTHD_8e8b84bd5d564199b80093583a0c638d()
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

      private sealed class MTHD_e107639a7161484cbe272e697cb75f5c : HardwiredMethodMemberDescriptor
      {
        internal MTHD_e107639a7161484cbe272e697cb75f5c()
        {
          this.Initialize("GetType", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) obj.GetType();
        }
      }

      private sealed class MTHD_93b49ade54d74f289c077bc883ab2e74 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_93b49ade54d74f289c077bc883ab2e74()
        {
          this.Initialize("ToString", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) obj.ToString();
        }
      }

      private sealed class PROP_da7d3bbe44b74fbaad4b6da54ac1fee5 : HardwiredMemberDescriptor
      {
        internal PROP_da7d3bbe44b74fbaad4b6da54ac1fee5()
          : base(typeof (int), "team", false, MemberDescriptorAccess.CanRead)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((ContainerCreature) obj).team;
        }
      }

      private sealed class PROP_eec1ed6d9f5f42c19743e8f72fd2eeba : HardwiredMemberDescriptor
      {
        internal PROP_eec1ed6d9f5f42c19743e8f72fd2eeba()
          : base(typeof (string), "name", false, MemberDescriptorAccess.CanRead)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((ContainerCreature) obj).name;
        }
      }

      private sealed class PROP_3de14e66283d4db7a0025899c6b90a95 : HardwiredMemberDescriptor
      {
        internal PROP_3de14e66283d4db7a0025899c6b90a95()
          : base(typeof (SpellEnum), "spellEnum", false, MemberDescriptorAccess.CanRead)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((ContainerCreature) obj).spellEnum;
        }
      }

      private sealed class PROP_2171f7eda0094257af0b8c2c20965038 : HardwiredMemberDescriptor
      {
        internal PROP_2171f7eda0094257af0b8c2c20965038()
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

      private sealed class PROP_ec308edb8912458eb40bcda4b9b7881a : HardwiredMemberDescriptor
      {
        internal PROP_ec308edb8912458eb40bcda4b9b7881a()
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

      private sealed class PROP_0a5675a55c4841d2872e23124aa77a80 : HardwiredMemberDescriptor
      {
        internal PROP_0a5675a55c4841d2872e23124aa77a80()
          : base(typeof (int), "healthAndTower", false, MemberDescriptorAccess.CanRead)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((ContainerCreature) obj).healthAndTower;
        }
      }

      private sealed class PROP_898d756723a94a0d9f8dc37d33df6ac1 : HardwiredMemberDescriptor
      {
        internal PROP_898d756723a94a0d9f8dc37d33df6ac1()
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

      private sealed class PROP_9b2a055b903b4ef4a03890830ba1df35 : HardwiredMemberDescriptor
      {
        internal PROP_9b2a055b903b4ef4a03890830ba1df35()
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

      private sealed class PROP_ed8f035a74c54732bf8b9ff99e951e53 : HardwiredMemberDescriptor
      {
        internal PROP_ed8f035a74c54732bf8b9ff99e951e53()
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

      private sealed class PROP_7e3e3ac65c584873ad1dd5cd4f000749 : HardwiredMemberDescriptor
      {
        internal PROP_7e3e3ac65c584873ad1dd5cd4f000749()
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

      private sealed class PROP_7a7abe7b6f0e48638d427818610f22a7 : HardwiredMemberDescriptor
      {
        internal PROP_7a7abe7b6f0e48638d427818610f22a7()
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

      private sealed class PROP_8ba77f2f1f2349079ad08b8359597b90 : HardwiredMemberDescriptor
      {
        internal PROP_8ba77f2f1f2349079ad08b8359597b90()
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

      private sealed class PROP_d40894f65ff14bd886bca065071d074b : HardwiredMemberDescriptor
      {
        internal PROP_d40894f65ff14bd886bca065071d074b()
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

      private sealed class PROP_c5fb12310be8414ba06876dc83ea779d : HardwiredMemberDescriptor
      {
        internal PROP_c5fb12310be8414ba06876dc83ea779d()
          : base(typeof (bool), "inTower", false, MemberDescriptorAccess.CanRead)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((ContainerCreature) obj).inTower;
        }
      }

      private sealed class PROP_9ffe4c7f39ac4e7ca14be64b9503918b : HardwiredMemberDescriptor
      {
        internal PROP_9ffe4c7f39ac4e7ca14be64b9503918b()
          : base(typeof (bool), "isMoving", false, MemberDescriptorAccess.CanRead)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((ContainerCreature) obj).isMoving;
        }
      }

      private sealed class PROP_23872424c7154760ad7164dd79a9aff6 : HardwiredMemberDescriptor
      {
        internal PROP_23872424c7154760ad7164dd79a9aff6()
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

      private sealed class PROP_94351b0c9d654b73a16a9173d87ee727 : HardwiredMemberDescriptor
      {
        internal PROP_94351b0c9d654b73a16a9173d87ee727()
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

      private sealed class PROP_1b080ef483ae4967835478e735492c40 : HardwiredMemberDescriptor
      {
        internal PROP_1b080ef483ae4967835478e735492c40()
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

      private sealed class PROP_ddeaad2179dc4b1da8bc08e29085ff0a : HardwiredMemberDescriptor
      {
        internal PROP_ddeaad2179dc4b1da8bc08e29085ff0a()
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

      private sealed class PROP_4c9a67fee9e94672b16716af964ae6aa : HardwiredMemberDescriptor
      {
        internal PROP_4c9a67fee9e94672b16716af964ae6aa()
          : base(typeof (bool), "inWater", false, MemberDescriptorAccess.CanRead)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((ContainerCreature) obj).inWater;
        }
      }

      private sealed class PROP_7f332fbd9abb415e87c8d6da5393fdc5 : HardwiredMemberDescriptor
      {
        internal PROP_7f332fbd9abb415e87c8d6da5393fdc5()
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

      private sealed class PROP_ce005b71f07a4b5aa9769ca618d20bd9 : HardwiredMemberDescriptor
      {
        internal PROP_ce005b71f07a4b5aa9769ca618d20bd9()
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

      private sealed class PROP_de5fe28b126b4cfbb25ee0a87666135f : HardwiredMemberDescriptor
      {
        internal PROP_de5fe28b126b4cfbb25ee0a87666135f()
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

      private sealed class PROP_34bf80af520643f69a6cbf8550376b50 : HardwiredMemberDescriptor
      {
        internal PROP_34bf80af520643f69a6cbf8550376b50()
          : base(typeof (bool), "yourTurn", false, MemberDescriptorAccess.CanRead)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((ContainerCreature) obj).yourTurn;
        }
      }

      private sealed class PROP_30a28d127f2147bd8bc058ea065cc3de : HardwiredMemberDescriptor
      {
        internal PROP_30a28d127f2147bd8bc058ea065cc3de()
          : base(typeof (bool), "isRider", false, MemberDescriptorAccess.CanRead)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((ContainerCreature) obj).isRider;
        }
      }

      private sealed class PROP_fe3000bc04d845e5a0715a1b3d267523 : HardwiredMemberDescriptor
      {
        internal PROP_fe3000bc04d845e5a0715a1b3d267523()
          : base(typeof (bool), "isMounted", false, MemberDescriptorAccess.CanRead)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((ContainerCreature) obj).isMounted;
        }
      }

      private sealed class PROP_894f2f7250a04f25b3ba3a3ee5d7edf4 : HardwiredMemberDescriptor
      {
        internal PROP_894f2f7250a04f25b3ba3a3ee5d7edf4()
          : base(typeof (bool), "isDead", false, MemberDescriptorAccess.CanRead)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((ContainerCreature) obj).isDead;
        }
      }

      private sealed class PROP_e8042d5084c84d4a9cbd35b2eed457f1 : HardwiredMemberDescriptor
      {
        internal PROP_e8042d5084c84d4a9cbd35b2eed457f1()
          : base(typeof (bool), "isPawn", false, MemberDescriptorAccess.CanRead)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((ContainerCreature) obj).isPawn;
        }
      }

      private sealed class PROP_4ac12697b3ed404c842843f911a84adb : HardwiredMemberDescriptor
      {
        internal PROP_4ac12697b3ed404c842843f911a84adb()
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

      private sealed class PROP_e62aa8d1c05f440a9f8a01d30ee42792 : HardwiredMemberDescriptor
      {
        internal PROP_e62aa8d1c05f440a9f8a01d30ee42792()
          : base(typeof (bool), "isMountable", false, MemberDescriptorAccess.CanRead)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((ContainerCreature) obj).isMountable;
        }
      }

      private sealed class PROP_683e41ffddd44eceac85b7ccfdf190b5 : HardwiredMemberDescriptor
      {
        internal PROP_683e41ffddd44eceac85b7ccfdf190b5()
          : base(typeof (bool), "canMount", false, MemberDescriptorAccess.CanRead)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((ContainerCreature) obj).canMount;
        }
      }

      private sealed class PROP_af1b34cf5b61463294f8b8206863caf4 : HardwiredMemberDescriptor
      {
        internal PROP_af1b34cf5b61463294f8b8206863caf4()
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

      private sealed class PROP_c34956a45e0f4c63a45ca37afb579ed5 : HardwiredMemberDescriptor
      {
        internal PROP_c34956a45e0f4c63a45ca37afb579ed5()
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

    private sealed class TYPE_0d6c47a182c1466da53be9def40de094 : HardwiredUserDataDescriptor
    {
      internal TYPE_0d6c47a182c1466da53be9def40de094()
        : base(typeof (ContainerEffector))
      {
        this.AddMember("get_isDead", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_isDead", typeof (ContainerEffector), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_0d6c47a182c1466da53be9def40de094.MTHD_0355474782fd42ea84654d4fc2226713()
        }));
        this.AddMember("get_x", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_x", typeof (ContainerEffector), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_0d6c47a182c1466da53be9def40de094.MTHD_38b428c8437d4ae4be636a405c92987c()
        }));
        this.AddMember("set_x", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("set_x", typeof (ContainerEffector), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_0d6c47a182c1466da53be9def40de094.MTHD_09c4e7be32eb4355a5324361827a732a()
        }));
        this.AddMember("get_y", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_y", typeof (ContainerEffector), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_0d6c47a182c1466da53be9def40de094.MTHD_2a6c2e7641c54d359796fe9d9c7d45b3()
        }));
        this.AddMember("set_y", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("set_y", typeof (ContainerEffector), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_0d6c47a182c1466da53be9def40de094.MTHD_2b5eab9ffbef4db2bcc84a74c528160c()
        }));
        this.AddMember("get_position", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_position", typeof (ContainerEffector), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_0d6c47a182c1466da53be9def40de094.MTHD_0be271ca13a641f99a0c5c6902af6e68()
        }));
        this.AddMember("set_position", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("set_position", typeof (ContainerEffector), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_0d6c47a182c1466da53be9def40de094.MTHD_c37224c7ec6d48bda765b425874b1ef2()
        }));
        this.AddMember("get_turnsAlive", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_turnsAlive", typeof (ContainerEffector), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_0d6c47a182c1466da53be9def40de094.MTHD_d02116b0f61a4fc895f9c17bc63edaa9()
        }));
        this.AddMember("set_turnsAlive", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("set_turnsAlive", typeof (ContainerEffector), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_0d6c47a182c1466da53be9def40de094.MTHD_261a0bc32b49438e8d1a91fb765fc7d3()
        }));
        this.AddMember("get_maxTurnsAlive", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_maxTurnsAlive", typeof (ContainerEffector), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_0d6c47a182c1466da53be9def40de094.MTHD_229765f6c44c44068bd324ee9cf72f36()
        }));
        this.AddMember("set_maxTurnsAlive", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("set_maxTurnsAlive", typeof (ContainerEffector), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_0d6c47a182c1466da53be9def40de094.MTHD_61c627e07bf34582a751ef53c7842023()
        }));
        this.AddMember("get_active", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_active", typeof (ContainerEffector), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_0d6c47a182c1466da53be9def40de094.MTHD_ae4c914520e44cd097cd2152e062d7ec()
        }));
        this.AddMember("set_active", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("set_active", typeof (ContainerEffector), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_0d6c47a182c1466da53be9def40de094.MTHD_972cf7258308452aafa599ad18a06d01()
        }));
        this.AddMember("get_type", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_type", typeof (ContainerEffector), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_0d6c47a182c1466da53be9def40de094.MTHD_294b75e28f6e47f38ae36715525c200e()
        }));
        this.AddMember("destroy", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("destroy", typeof (ContainerEffector), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_0d6c47a182c1466da53be9def40de094.MTHD_6a68f09b19234bc48f555e5a5654b5a6()
        }));
        this.AddMember("turnPassed", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("turnPassed", typeof (ContainerEffector), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_0d6c47a182c1466da53be9def40de094.MTHD_504914d03b13445a81f5ff7b31cb348a()
        }));
        this.AddMember("GetHashCode", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("GetHashCode", typeof (ContainerEffector), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_0d6c47a182c1466da53be9def40de094.MTHD_abc8fc41e2c5413481ae0975f7ec81ec()
        }));
        this.AddMember("Equals", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("Equals", typeof (ContainerEffector), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_0d6c47a182c1466da53be9def40de094.MTHD_80d29a35f2634a4cb8bd258ff50c6bf6()
        }));
        this.AddMember("GetType", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("GetType", typeof (ContainerEffector), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_0d6c47a182c1466da53be9def40de094.MTHD_975324efa5654de0bb7f2759925a7e2d()
        }));
        this.AddMember("ToString", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("ToString", typeof (ContainerEffector), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_0d6c47a182c1466da53be9def40de094.MTHD_45e1593a8d184bbe881ccf10dad8d549()
        }));
        this.AddMember("isDead", (IMemberDescriptor) new Bridge.TYPE_0d6c47a182c1466da53be9def40de094.PROP_5ff2d11954fa4a66b12cbe96d0434d58());
        this.AddMember("x", (IMemberDescriptor) new Bridge.TYPE_0d6c47a182c1466da53be9def40de094.PROP_e092deb54dd2476b8f4ff6f5e7c4b4d3());
        this.AddMember("y", (IMemberDescriptor) new Bridge.TYPE_0d6c47a182c1466da53be9def40de094.PROP_ef1a1c79e6d1458ab1dd1ea2421bd62c());
        this.AddMember("position", (IMemberDescriptor) new Bridge.TYPE_0d6c47a182c1466da53be9def40de094.PROP_49359a13c3474ebf9a4cb9c1d8e8a65f());
        this.AddMember("turnsAlive", (IMemberDescriptor) new Bridge.TYPE_0d6c47a182c1466da53be9def40de094.PROP_15320c0afe8042c9a78d5de7627886c0());
        this.AddMember("maxTurnsAlive", (IMemberDescriptor) new Bridge.TYPE_0d6c47a182c1466da53be9def40de094.PROP_03f49af89d5845ec92ae4b1d4f3263c7());
        this.AddMember("active", (IMemberDescriptor) new Bridge.TYPE_0d6c47a182c1466da53be9def40de094.PROP_125d4d9ebc6640a2a4bd9bcbea38b7e3());
        this.AddMember("type", (IMemberDescriptor) new Bridge.TYPE_0d6c47a182c1466da53be9def40de094.PROP_326787295fd846d683a898fe0dc69377());
      }

      private sealed class MTHD_0355474782fd42ea84654d4fc2226713 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_0355474782fd42ea84654d4fc2226713()
        {
          this.Initialize("get_isDead", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerEffector) obj).isDead;
        }
      }

      private sealed class MTHD_38b428c8437d4ae4be636a405c92987c : HardwiredMethodMemberDescriptor
      {
        internal MTHD_38b428c8437d4ae4be636a405c92987c()
        {
          this.Initialize("get_x", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerEffector) obj).x;
        }
      }

      private sealed class MTHD_09c4e7be32eb4355a5324361827a732a : HardwiredMethodMemberDescriptor
      {
        internal MTHD_09c4e7be32eb4355a5324361827a732a()
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

      private sealed class MTHD_2a6c2e7641c54d359796fe9d9c7d45b3 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_2a6c2e7641c54d359796fe9d9c7d45b3()
        {
          this.Initialize("get_y", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerEffector) obj).y;
        }
      }

      private sealed class MTHD_2b5eab9ffbef4db2bcc84a74c528160c : HardwiredMethodMemberDescriptor
      {
        internal MTHD_2b5eab9ffbef4db2bcc84a74c528160c()
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

      private sealed class MTHD_0be271ca13a641f99a0c5c6902af6e68 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_0be271ca13a641f99a0c5c6902af6e68()
        {
          this.Initialize("get_position", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerEffector) obj).position;
        }
      }

      private sealed class MTHD_c37224c7ec6d48bda765b425874b1ef2 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_c37224c7ec6d48bda765b425874b1ef2()
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

      private sealed class MTHD_d02116b0f61a4fc895f9c17bc63edaa9 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_d02116b0f61a4fc895f9c17bc63edaa9()
        {
          this.Initialize("get_turnsAlive", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerEffector) obj).turnsAlive;
        }
      }

      private sealed class MTHD_261a0bc32b49438e8d1a91fb765fc7d3 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_261a0bc32b49438e8d1a91fb765fc7d3()
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

      private sealed class MTHD_229765f6c44c44068bd324ee9cf72f36 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_229765f6c44c44068bd324ee9cf72f36()
        {
          this.Initialize("get_maxTurnsAlive", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerEffector) obj).maxTurnsAlive;
        }
      }

      private sealed class MTHD_61c627e07bf34582a751ef53c7842023 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_61c627e07bf34582a751ef53c7842023()
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

      private sealed class MTHD_ae4c914520e44cd097cd2152e062d7ec : HardwiredMethodMemberDescriptor
      {
        internal MTHD_ae4c914520e44cd097cd2152e062d7ec()
        {
          this.Initialize("get_active", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerEffector) obj).active;
        }
      }

      private sealed class MTHD_972cf7258308452aafa599ad18a06d01 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_972cf7258308452aafa599ad18a06d01()
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

      private sealed class MTHD_294b75e28f6e47f38ae36715525c200e : HardwiredMethodMemberDescriptor
      {
        internal MTHD_294b75e28f6e47f38ae36715525c200e()
        {
          this.Initialize("get_type", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerEffector) obj).type;
        }
      }

      private sealed class MTHD_6a68f09b19234bc48f555e5a5654b5a6 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_6a68f09b19234bc48f555e5a5654b5a6()
        {
          this.Initialize("destroy", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          ((ContainerEffector) obj).destroy();
          return (object) DynValue.Void;
        }
      }

      private sealed class MTHD_504914d03b13445a81f5ff7b31cb348a : HardwiredMethodMemberDescriptor
      {
        internal MTHD_504914d03b13445a81f5ff7b31cb348a()
        {
          this.Initialize("turnPassed", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          ((ContainerEffector) obj).turnPassed();
          return (object) DynValue.Void;
        }
      }

      private sealed class MTHD_abc8fc41e2c5413481ae0975f7ec81ec : HardwiredMethodMemberDescriptor
      {
        internal MTHD_abc8fc41e2c5413481ae0975f7ec81ec()
        {
          this.Initialize("GetHashCode", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerEffector) obj).GetHashCode();
        }
      }

      private sealed class MTHD_80d29a35f2634a4cb8bd258ff50c6bf6 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_80d29a35f2634a4cb8bd258ff50c6bf6()
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

      private sealed class MTHD_975324efa5654de0bb7f2759925a7e2d : HardwiredMethodMemberDescriptor
      {
        internal MTHD_975324efa5654de0bb7f2759925a7e2d()
        {
          this.Initialize("GetType", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) obj.GetType();
        }
      }

      private sealed class MTHD_45e1593a8d184bbe881ccf10dad8d549 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_45e1593a8d184bbe881ccf10dad8d549()
        {
          this.Initialize("ToString", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) obj.ToString();
        }
      }

      private sealed class PROP_5ff2d11954fa4a66b12cbe96d0434d58 : HardwiredMemberDescriptor
      {
        internal PROP_5ff2d11954fa4a66b12cbe96d0434d58()
          : base(typeof (bool), "isDead", false, MemberDescriptorAccess.CanRead)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((ContainerEffector) obj).isDead;
        }
      }

      private sealed class PROP_e092deb54dd2476b8f4ff6f5e7c4b4d3 : HardwiredMemberDescriptor
      {
        internal PROP_e092deb54dd2476b8f4ff6f5e7c4b4d3()
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

      private sealed class PROP_ef1a1c79e6d1458ab1dd1ea2421bd62c : HardwiredMemberDescriptor
      {
        internal PROP_ef1a1c79e6d1458ab1dd1ea2421bd62c()
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

      private sealed class PROP_49359a13c3474ebf9a4cb9c1d8e8a65f : HardwiredMemberDescriptor
      {
        internal PROP_49359a13c3474ebf9a4cb9c1d8e8a65f()
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

      private sealed class PROP_15320c0afe8042c9a78d5de7627886c0 : HardwiredMemberDescriptor
      {
        internal PROP_15320c0afe8042c9a78d5de7627886c0()
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

      private sealed class PROP_03f49af89d5845ec92ae4b1d4f3263c7 : HardwiredMemberDescriptor
      {
        internal PROP_03f49af89d5845ec92ae4b1d4f3263c7()
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

      private sealed class PROP_125d4d9ebc6640a2a4bd9bcbea38b7e3 : HardwiredMemberDescriptor
      {
        internal PROP_125d4d9ebc6640a2a4bd9bcbea38b7e3()
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

      private sealed class PROP_326787295fd846d683a898fe0dc69377 : HardwiredMemberDescriptor
      {
        internal PROP_326787295fd846d683a898fe0dc69377()
          : base(typeof (EffectorType), "type", false, MemberDescriptorAccess.CanRead)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((ContainerEffector) obj).type;
        }
      }
    }

    private sealed class TYPE_d4713a751cdb42afade7a7955a883875 : HardwiredUserDataDescriptor
    {
      internal TYPE_d4713a751cdb42afade7a7955a883875()
        : base(typeof (ContainerIndicator))
      {
        this.AddMember("get_isDead", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_isDead", typeof (ContainerIndicator), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_d4713a751cdb42afade7a7955a883875.MTHD_e62df9277c6543ab8eac98216011fd16()
        }));
        this.AddMember("get_x", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_x", typeof (ContainerIndicator), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_d4713a751cdb42afade7a7955a883875.MTHD_82f3113d0e394952af83edd19cdb55ba()
        }));
        this.AddMember("set_x", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("set_x", typeof (ContainerIndicator), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_d4713a751cdb42afade7a7955a883875.MTHD_69edccbcb35e4f6e9b6923902e5b026a()
        }));
        this.AddMember("get_y", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_y", typeof (ContainerIndicator), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_d4713a751cdb42afade7a7955a883875.MTHD_9145b3475da74bd88ced6c11f5ab45a8()
        }));
        this.AddMember("set_y", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("set_y", typeof (ContainerIndicator), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_d4713a751cdb42afade7a7955a883875.MTHD_abbb7e503c38445d9ea6fa734069a8f6()
        }));
        this.AddMember("get_radius", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_radius", typeof (ContainerIndicator), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_d4713a751cdb42afade7a7955a883875.MTHD_eb3f850a09e64aba8b6ef473b13eb127()
        }));
        this.AddMember("set_radius", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("set_radius", typeof (ContainerIndicator), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_d4713a751cdb42afade7a7955a883875.MTHD_3127770301344baf8101c2dbb11cbba6()
        }));
        this.AddMember("get_position", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_position", typeof (ContainerIndicator), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_d4713a751cdb42afade7a7955a883875.MTHD_ce76aa17764d446c8fc5f4375af2970a()
        }));
        this.AddMember("set_position", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("set_position", typeof (ContainerIndicator), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_d4713a751cdb42afade7a7955a883875.MTHD_611120d2cc164eb2936b3cf1aac3cfef()
        }));
        this.AddMember("get_angle", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_angle", typeof (ContainerIndicator), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_d4713a751cdb42afade7a7955a883875.MTHD_039dc7b2ef484a1c9339ee9c6303011b()
        }));
        this.AddMember("set_angle", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("set_angle", typeof (ContainerIndicator), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_d4713a751cdb42afade7a7955a883875.MTHD_bf09f078f0974de193ce60a4f9962040()
        }));
        this.AddMember("get_color", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_color", typeof (ContainerIndicator), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_d4713a751cdb42afade7a7955a883875.MTHD_692c022d1fca4b45b520aadc65572c97()
        }));
        this.AddMember("set_color", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("set_color", typeof (ContainerIndicator), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_d4713a751cdb42afade7a7955a883875.MTHD_58f79cc21beb499aae35cbf46e62cd58()
        }));
        this.AddMember("get_kind", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_kind", typeof (ContainerIndicator), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_d4713a751cdb42afade7a7955a883875.MTHD_08cd8c5af5514e569d7f5e45c361241d()
        }));
        this.AddMember("Destroy", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("Destroy", typeof (ContainerIndicator), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_d4713a751cdb42afade7a7955a883875.MTHD_c73bee9ea68d45b6a20a72f424cce5f5()
        }));
        this.AddMember("Equals", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("Equals", typeof (ContainerIndicator), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_d4713a751cdb42afade7a7955a883875.MTHD_b29297ecc9b344b7898bbbb607badf0c()
        }));
        this.AddMember("GetHashCode", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("GetHashCode", typeof (ContainerIndicator), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_d4713a751cdb42afade7a7955a883875.MTHD_64540ff471394b9a8782710954a01253()
        }));
        this.AddMember("GetType", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("GetType", typeof (ContainerIndicator), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_d4713a751cdb42afade7a7955a883875.MTHD_1aae3aeb524340e1b8f17fe3ff7e1131()
        }));
        this.AddMember("ToString", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("ToString", typeof (ContainerIndicator), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_d4713a751cdb42afade7a7955a883875.MTHD_8e4d6878bf274734b299deabfaf6d907()
        }));
        this.AddMember("isDead", (IMemberDescriptor) new Bridge.TYPE_d4713a751cdb42afade7a7955a883875.PROP_0a21901aa6864ab5b7e29c03c3c84ef0());
        this.AddMember("x", (IMemberDescriptor) new Bridge.TYPE_d4713a751cdb42afade7a7955a883875.PROP_4148c4cc217d4fc886244822e45d2aef());
        this.AddMember("y", (IMemberDescriptor) new Bridge.TYPE_d4713a751cdb42afade7a7955a883875.PROP_ebdc7f63a39f425eba532bd1a0184e07());
        this.AddMember("radius", (IMemberDescriptor) new Bridge.TYPE_d4713a751cdb42afade7a7955a883875.PROP_d8c2dded35a946e6ae7fefcee936ce20());
        this.AddMember("position", (IMemberDescriptor) new Bridge.TYPE_d4713a751cdb42afade7a7955a883875.PROP_1f9495d19ced40f5a9f6dd1b0d3ea1ea());
        this.AddMember("angle", (IMemberDescriptor) new Bridge.TYPE_d4713a751cdb42afade7a7955a883875.PROP_b6fb97f98e784deeb231492a78758209());
        this.AddMember("color", (IMemberDescriptor) new Bridge.TYPE_d4713a751cdb42afade7a7955a883875.PROP_df3ea527340c4c3fa23ecc6da81daa7a());
        this.AddMember("kind", (IMemberDescriptor) new Bridge.TYPE_d4713a751cdb42afade7a7955a883875.PROP_7d3aed7fe04b4ae9b9b8e484fb686faf());
      }

      private sealed class MTHD_e62df9277c6543ab8eac98216011fd16 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_e62df9277c6543ab8eac98216011fd16()
        {
          this.Initialize("get_isDead", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerIndicator) obj).isDead;
        }
      }

      private sealed class MTHD_82f3113d0e394952af83edd19cdb55ba : HardwiredMethodMemberDescriptor
      {
        internal MTHD_82f3113d0e394952af83edd19cdb55ba()
        {
          this.Initialize("get_x", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerIndicator) obj).x;
        }
      }

      private sealed class MTHD_69edccbcb35e4f6e9b6923902e5b026a : HardwiredMethodMemberDescriptor
      {
        internal MTHD_69edccbcb35e4f6e9b6923902e5b026a()
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

      private sealed class MTHD_9145b3475da74bd88ced6c11f5ab45a8 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_9145b3475da74bd88ced6c11f5ab45a8()
        {
          this.Initialize("get_y", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerIndicator) obj).y;
        }
      }

      private sealed class MTHD_abbb7e503c38445d9ea6fa734069a8f6 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_abbb7e503c38445d9ea6fa734069a8f6()
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

      private sealed class MTHD_eb3f850a09e64aba8b6ef473b13eb127 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_eb3f850a09e64aba8b6ef473b13eb127()
        {
          this.Initialize("get_radius", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerIndicator) obj).radius;
        }
      }

      private sealed class MTHD_3127770301344baf8101c2dbb11cbba6 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_3127770301344baf8101c2dbb11cbba6()
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

      private sealed class MTHD_ce76aa17764d446c8fc5f4375af2970a : HardwiredMethodMemberDescriptor
      {
        internal MTHD_ce76aa17764d446c8fc5f4375af2970a()
        {
          this.Initialize("get_position", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerIndicator) obj).position;
        }
      }

      private sealed class MTHD_611120d2cc164eb2936b3cf1aac3cfef : HardwiredMethodMemberDescriptor
      {
        internal MTHD_611120d2cc164eb2936b3cf1aac3cfef()
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

      private sealed class MTHD_039dc7b2ef484a1c9339ee9c6303011b : HardwiredMethodMemberDescriptor
      {
        internal MTHD_039dc7b2ef484a1c9339ee9c6303011b()
        {
          this.Initialize("get_angle", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerIndicator) obj).angle;
        }
      }

      private sealed class MTHD_bf09f078f0974de193ce60a4f9962040 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_bf09f078f0974de193ce60a4f9962040()
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

      private sealed class MTHD_692c022d1fca4b45b520aadc65572c97 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_692c022d1fca4b45b520aadc65572c97()
        {
          this.Initialize("get_color", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerIndicator) obj).color;
        }
      }

      private sealed class MTHD_58f79cc21beb499aae35cbf46e62cd58 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_58f79cc21beb499aae35cbf46e62cd58()
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

      private sealed class MTHD_08cd8c5af5514e569d7f5e45c361241d : HardwiredMethodMemberDescriptor
      {
        internal MTHD_08cd8c5af5514e569d7f5e45c361241d()
        {
          this.Initialize("get_kind", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerIndicator) obj).kind;
        }
      }

      private sealed class MTHD_c73bee9ea68d45b6a20a72f424cce5f5 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_c73bee9ea68d45b6a20a72f424cce5f5()
        {
          this.Initialize("Destroy", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          ((ContainerIndicator) obj).Destroy();
          return (object) DynValue.Void;
        }
      }

      private sealed class MTHD_b29297ecc9b344b7898bbbb607badf0c : HardwiredMethodMemberDescriptor
      {
        internal MTHD_b29297ecc9b344b7898bbbb607badf0c()
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

      private sealed class MTHD_64540ff471394b9a8782710954a01253 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_64540ff471394b9a8782710954a01253()
        {
          this.Initialize("GetHashCode", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) obj.GetHashCode();
        }
      }

      private sealed class MTHD_1aae3aeb524340e1b8f17fe3ff7e1131 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_1aae3aeb524340e1b8f17fe3ff7e1131()
        {
          this.Initialize("GetType", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) obj.GetType();
        }
      }

      private sealed class MTHD_8e4d6878bf274734b299deabfaf6d907 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_8e4d6878bf274734b299deabfaf6d907()
        {
          this.Initialize("ToString", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) obj.ToString();
        }
      }

      private sealed class PROP_0a21901aa6864ab5b7e29c03c3c84ef0 : HardwiredMemberDescriptor
      {
        internal PROP_0a21901aa6864ab5b7e29c03c3c84ef0()
          : base(typeof (bool), "isDead", false, MemberDescriptorAccess.CanRead)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((ContainerIndicator) obj).isDead;
        }
      }

      private sealed class PROP_4148c4cc217d4fc886244822e45d2aef : HardwiredMemberDescriptor
      {
        internal PROP_4148c4cc217d4fc886244822e45d2aef()
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

      private sealed class PROP_ebdc7f63a39f425eba532bd1a0184e07 : HardwiredMemberDescriptor
      {
        internal PROP_ebdc7f63a39f425eba532bd1a0184e07()
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

      private sealed class PROP_d8c2dded35a946e6ae7fefcee936ce20 : HardwiredMemberDescriptor
      {
        internal PROP_d8c2dded35a946e6ae7fefcee936ce20()
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

      private sealed class PROP_1f9495d19ced40f5a9f6dd1b0d3ea1ea : HardwiredMemberDescriptor
      {
        internal PROP_1f9495d19ced40f5a9f6dd1b0d3ea1ea()
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

      private sealed class PROP_b6fb97f98e784deeb231492a78758209 : HardwiredMemberDescriptor
      {
        internal PROP_b6fb97f98e784deeb231492a78758209()
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

      private sealed class PROP_df3ea527340c4c3fa23ecc6da81daa7a : HardwiredMemberDescriptor
      {
        internal PROP_df3ea527340c4c3fa23ecc6da81daa7a()
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

      private sealed class PROP_7d3aed7fe04b4ae9b9b8e484fb686faf : HardwiredMemberDescriptor
      {
        internal PROP_7d3aed7fe04b4ae9b9b8e484fb686faf()
          : base(typeof (IndicatorKind), "kind", false, MemberDescriptorAccess.CanRead)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((ContainerIndicator) obj).kind;
        }
      }
    }

    private sealed class TYPE_2b55a2dc2b96473184f157b103f346a2 : HardwiredUserDataDescriptor
    {
      internal TYPE_2b55a2dc2b96473184f157b103f346a2()
        : base(typeof (ContainerSpell))
      {
        this.AddMember("get_uses", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_uses", typeof (ContainerSpell), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_2b55a2dc2b96473184f157b103f346a2.MTHD_d17d4c1bfd194b929e866e58c6619c68()
        }));
        this.AddMember("set_uses", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("set_uses", typeof (ContainerSpell), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_2b55a2dc2b96473184f157b103f346a2.MTHD_88bfe043973242ca94651b38c6258aa2()
        }));
        this.AddMember("get_maxUses", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_maxUses", typeof (ContainerSpell), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_2b55a2dc2b96473184f157b103f346a2.MTHD_892c71e45556460aac2682e0364a15d0()
        }));
        this.AddMember("set_maxUses", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("set_maxUses", typeof (ContainerSpell), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_2b55a2dc2b96473184f157b103f346a2.MTHD_e47a11250ade4826b06e0cdbf6d9370b()
        }));
        this.AddMember("get_rechargeTime", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_rechargeTime", typeof (ContainerSpell), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_2b55a2dc2b96473184f157b103f346a2.MTHD_ca5549244bce4dc0ac7680010fbf023c()
        }));
        this.AddMember("set_rechargeTime", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("set_rechargeTime", typeof (ContainerSpell), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_2b55a2dc2b96473184f157b103f346a2.MTHD_986afc96b8604963b065f366ba2c1549()
        }));
        this.AddMember("get_lastTurnFired", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_lastTurnFired", typeof (ContainerSpell), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_2b55a2dc2b96473184f157b103f346a2.MTHD_314ad49624b64774bacb35552b5ea861()
        }));
        this.AddMember("set_lastTurnFired", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("set_lastTurnFired", typeof (ContainerSpell), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_2b55a2dc2b96473184f157b103f346a2.MTHD_e149931019ea44acba1fa1e69f4dcd91()
        }));
        this.AddMember("get_isPresent", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_isPresent", typeof (ContainerSpell), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_2b55a2dc2b96473184f157b103f346a2.MTHD_1218540ffd6e4179b4e42f15392906d8()
        }));
        this.AddMember("set_isPresent", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("set_isPresent", typeof (ContainerSpell), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_2b55a2dc2b96473184f157b103f346a2.MTHD_77c6c957eddf4c94a56d8578eed4b5d3()
        }));
        this.AddMember("get_locked", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_locked", typeof (ContainerSpell), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_2b55a2dc2b96473184f157b103f346a2.MTHD_e393ce0b4e6e4623b600398f97b4572e()
        }));
        this.AddMember("set_locked", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("set_locked", typeof (ContainerSpell), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_2b55a2dc2b96473184f157b103f346a2.MTHD_b81773dc6ad54afdbfb5ceb4e815d6af()
        }));
        this.AddMember("get_spellEnum", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_spellEnum", typeof (ContainerSpell), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_2b55a2dc2b96473184f157b103f346a2.MTHD_7bb3ac4655734690b7abef87ad45127a()
        }));
        this.AddMember("get_castType", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_castType", typeof (ContainerSpell), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_2b55a2dc2b96473184f157b103f346a2.MTHD_3a0a562eeb56428d80bfe2614e906776()
        }));
        this.AddMember("get_damageType", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_damageType", typeof (ContainerSpell), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_2b55a2dc2b96473184f157b103f346a2.MTHD_3c422c08d1884155be95f0ecbf4978a3()
        }));
        this.AddMember("get_book", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_book", typeof (ContainerSpell), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_2b55a2dc2b96473184f157b103f346a2.MTHD_4f0311711e3e45ee9344e8febcdd99bc()
        }));
        this.AddMember("get_damage", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_damage", typeof (ContainerSpell), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_2b55a2dc2b96473184f157b103f346a2.MTHD_50d371c0c2bb46eb92cd7c918d2faafc()
        }));
        this.AddMember("get_explosionRadius", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_explosionRadius", typeof (ContainerSpell), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_2b55a2dc2b96473184f157b103f346a2.MTHD_a6212a5de8044c348971808b5ca31b1c()
        }));
        this.AddMember("get_description", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_description", typeof (ContainerSpell), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_2b55a2dc2b96473184f157b103f346a2.MTHD_522af7586842431a9e9865f68849e6f2()
        }));
        this.AddMember("get_descriptionOnly", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_descriptionOnly", typeof (ContainerSpell), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_2b55a2dc2b96473184f157b103f346a2.MTHD_6d9334f89b7043a082c28f44b408f610()
        }));
        this.AddMember("get_descriptionExtra", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_descriptionExtra", typeof (ContainerSpell), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_2b55a2dc2b96473184f157b103f346a2.MTHD_d25cb8e03f1d4b0fb3874e34c7263ca7()
        }));
        this.AddMember("get_name", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_name", typeof (ContainerSpell), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_2b55a2dc2b96473184f157b103f346a2.MTHD_fdf979f0767349f48bc524b530d61702()
        }));
        this.AddMember("Equals", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("Equals", typeof (ContainerSpell), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_2b55a2dc2b96473184f157b103f346a2.MTHD_1dfd287b71e046b8b6f3e4776eee0072()
        }));
        this.AddMember("GetHashCode", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("GetHashCode", typeof (ContainerSpell), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_2b55a2dc2b96473184f157b103f346a2.MTHD_07d5e9ea220f43de879f3d34ee40167e()
        }));
        this.AddMember("GetType", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("GetType", typeof (ContainerSpell), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_2b55a2dc2b96473184f157b103f346a2.MTHD_a6fab28e36d2459b8ac4d41f125f00b9()
        }));
        this.AddMember("ToString", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("ToString", typeof (ContainerSpell), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_2b55a2dc2b96473184f157b103f346a2.MTHD_fde90538549b44f1a72632e2975cdbb8()
        }));
        this.AddMember("uses", (IMemberDescriptor) new Bridge.TYPE_2b55a2dc2b96473184f157b103f346a2.PROP_b86e6f240ba14c1696e654924abba19d());
        this.AddMember("maxUses", (IMemberDescriptor) new Bridge.TYPE_2b55a2dc2b96473184f157b103f346a2.PROP_63556a6ec165423cbac177e4306ab9b4());
        this.AddMember("rechargeTime", (IMemberDescriptor) new Bridge.TYPE_2b55a2dc2b96473184f157b103f346a2.PROP_8d650ec816e34cc4bb436b20ae4346ca());
        this.AddMember("lastTurnFired", (IMemberDescriptor) new Bridge.TYPE_2b55a2dc2b96473184f157b103f346a2.PROP_018a8f9b911d48c6877a91e1cc62edc7());
        this.AddMember("isPresent", (IMemberDescriptor) new Bridge.TYPE_2b55a2dc2b96473184f157b103f346a2.PROP_7710f835c5594c628eb6f3394dc7514f());
        this.AddMember("locked", (IMemberDescriptor) new Bridge.TYPE_2b55a2dc2b96473184f157b103f346a2.PROP_838ebf34f376409f9da846885cd8b879());
        this.AddMember("spellEnum", (IMemberDescriptor) new Bridge.TYPE_2b55a2dc2b96473184f157b103f346a2.PROP_c7950ae997be42b1a1a130a4671880d8());
        this.AddMember("castType", (IMemberDescriptor) new Bridge.TYPE_2b55a2dc2b96473184f157b103f346a2.PROP_09018f8fb1334226abf65abc5f001e4d());
        this.AddMember("damageType", (IMemberDescriptor) new Bridge.TYPE_2b55a2dc2b96473184f157b103f346a2.PROP_80a8ed639a364537b1f86c91d2bd04ef());
        this.AddMember("book", (IMemberDescriptor) new Bridge.TYPE_2b55a2dc2b96473184f157b103f346a2.PROP_70a255d9f0c1480db0f2ce098da37f12());
        this.AddMember("damage", (IMemberDescriptor) new Bridge.TYPE_2b55a2dc2b96473184f157b103f346a2.PROP_ce35ab19a9704dee8a7e39c2130d7803());
        this.AddMember("explosionRadius", (IMemberDescriptor) new Bridge.TYPE_2b55a2dc2b96473184f157b103f346a2.PROP_5b61c068ba524fbb930d4038d4376c03());
        this.AddMember("description", (IMemberDescriptor) new Bridge.TYPE_2b55a2dc2b96473184f157b103f346a2.PROP_42703f477619461da342ecfcaccc9b83());
        this.AddMember("descriptionOnly", (IMemberDescriptor) new Bridge.TYPE_2b55a2dc2b96473184f157b103f346a2.PROP_0fce7ef87ad246fc97e898a3542e715d());
        this.AddMember("descriptionExtra", (IMemberDescriptor) new Bridge.TYPE_2b55a2dc2b96473184f157b103f346a2.PROP_02e6a7460f394f60ade485287f4202f0());
        this.AddMember("name", (IMemberDescriptor) new Bridge.TYPE_2b55a2dc2b96473184f157b103f346a2.PROP_6331c957829a443fb3780d55876f3439());
      }

      private sealed class MTHD_d17d4c1bfd194b929e866e58c6619c68 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_d17d4c1bfd194b929e866e58c6619c68()
        {
          this.Initialize("get_uses", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerSpell) obj).uses;
        }
      }

      private sealed class MTHD_88bfe043973242ca94651b38c6258aa2 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_88bfe043973242ca94651b38c6258aa2()
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

      private sealed class MTHD_892c71e45556460aac2682e0364a15d0 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_892c71e45556460aac2682e0364a15d0()
        {
          this.Initialize("get_maxUses", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerSpell) obj).maxUses;
        }
      }

      private sealed class MTHD_e47a11250ade4826b06e0cdbf6d9370b : HardwiredMethodMemberDescriptor
      {
        internal MTHD_e47a11250ade4826b06e0cdbf6d9370b()
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

      private sealed class MTHD_ca5549244bce4dc0ac7680010fbf023c : HardwiredMethodMemberDescriptor
      {
        internal MTHD_ca5549244bce4dc0ac7680010fbf023c()
        {
          this.Initialize("get_rechargeTime", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerSpell) obj).rechargeTime;
        }
      }

      private sealed class MTHD_986afc96b8604963b065f366ba2c1549 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_986afc96b8604963b065f366ba2c1549()
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

      private sealed class MTHD_314ad49624b64774bacb35552b5ea861 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_314ad49624b64774bacb35552b5ea861()
        {
          this.Initialize("get_lastTurnFired", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerSpell) obj).lastTurnFired;
        }
      }

      private sealed class MTHD_e149931019ea44acba1fa1e69f4dcd91 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_e149931019ea44acba1fa1e69f4dcd91()
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

      private sealed class MTHD_1218540ffd6e4179b4e42f15392906d8 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_1218540ffd6e4179b4e42f15392906d8()
        {
          this.Initialize("get_isPresent", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerSpell) obj).isPresent;
        }
      }

      private sealed class MTHD_77c6c957eddf4c94a56d8578eed4b5d3 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_77c6c957eddf4c94a56d8578eed4b5d3()
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

      private sealed class MTHD_e393ce0b4e6e4623b600398f97b4572e : HardwiredMethodMemberDescriptor
      {
        internal MTHD_e393ce0b4e6e4623b600398f97b4572e()
        {
          this.Initialize("get_locked", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerSpell) obj).locked;
        }
      }

      private sealed class MTHD_b81773dc6ad54afdbfb5ceb4e815d6af : HardwiredMethodMemberDescriptor
      {
        internal MTHD_b81773dc6ad54afdbfb5ceb4e815d6af()
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

      private sealed class MTHD_7bb3ac4655734690b7abef87ad45127a : HardwiredMethodMemberDescriptor
      {
        internal MTHD_7bb3ac4655734690b7abef87ad45127a()
        {
          this.Initialize("get_spellEnum", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerSpell) obj).spellEnum;
        }
      }

      private sealed class MTHD_3a0a562eeb56428d80bfe2614e906776 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_3a0a562eeb56428d80bfe2614e906776()
        {
          this.Initialize("get_castType", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerSpell) obj).castType;
        }
      }

      private sealed class MTHD_3c422c08d1884155be95f0ecbf4978a3 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_3c422c08d1884155be95f0ecbf4978a3()
        {
          this.Initialize("get_damageType", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerSpell) obj).damageType;
        }
      }

      private sealed class MTHD_4f0311711e3e45ee9344e8febcdd99bc : HardwiredMethodMemberDescriptor
      {
        internal MTHD_4f0311711e3e45ee9344e8febcdd99bc()
        {
          this.Initialize("get_book", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerSpell) obj).book;
        }
      }

      private sealed class MTHD_50d371c0c2bb46eb92cd7c918d2faafc : HardwiredMethodMemberDescriptor
      {
        internal MTHD_50d371c0c2bb46eb92cd7c918d2faafc()
        {
          this.Initialize("get_damage", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerSpell) obj).damage;
        }
      }

      private sealed class MTHD_a6212a5de8044c348971808b5ca31b1c : HardwiredMethodMemberDescriptor
      {
        internal MTHD_a6212a5de8044c348971808b5ca31b1c()
        {
          this.Initialize("get_explosionRadius", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerSpell) obj).explosionRadius;
        }
      }

      private sealed class MTHD_522af7586842431a9e9865f68849e6f2 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_522af7586842431a9e9865f68849e6f2()
        {
          this.Initialize("get_description", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerSpell) obj).description;
        }
      }

      private sealed class MTHD_6d9334f89b7043a082c28f44b408f610 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_6d9334f89b7043a082c28f44b408f610()
        {
          this.Initialize("get_descriptionOnly", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerSpell) obj).descriptionOnly;
        }
      }

      private sealed class MTHD_d25cb8e03f1d4b0fb3874e34c7263ca7 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_d25cb8e03f1d4b0fb3874e34c7263ca7()
        {
          this.Initialize("get_descriptionExtra", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerSpell) obj).descriptionExtra;
        }
      }

      private sealed class MTHD_fdf979f0767349f48bc524b530d61702 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_fdf979f0767349f48bc524b530d61702()
        {
          this.Initialize("get_name", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerSpell) obj).name;
        }
      }

      private sealed class MTHD_1dfd287b71e046b8b6f3e4776eee0072 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_1dfd287b71e046b8b6f3e4776eee0072()
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

      private sealed class MTHD_07d5e9ea220f43de879f3d34ee40167e : HardwiredMethodMemberDescriptor
      {
        internal MTHD_07d5e9ea220f43de879f3d34ee40167e()
        {
          this.Initialize("GetHashCode", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) obj.GetHashCode();
        }
      }

      private sealed class MTHD_a6fab28e36d2459b8ac4d41f125f00b9 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_a6fab28e36d2459b8ac4d41f125f00b9()
        {
          this.Initialize("GetType", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) obj.GetType();
        }
      }

      private sealed class MTHD_fde90538549b44f1a72632e2975cdbb8 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_fde90538549b44f1a72632e2975cdbb8()
        {
          this.Initialize("ToString", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) obj.ToString();
        }
      }

      private sealed class PROP_b86e6f240ba14c1696e654924abba19d : HardwiredMemberDescriptor
      {
        internal PROP_b86e6f240ba14c1696e654924abba19d()
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

      private sealed class PROP_63556a6ec165423cbac177e4306ab9b4 : HardwiredMemberDescriptor
      {
        internal PROP_63556a6ec165423cbac177e4306ab9b4()
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

      private sealed class PROP_8d650ec816e34cc4bb436b20ae4346ca : HardwiredMemberDescriptor
      {
        internal PROP_8d650ec816e34cc4bb436b20ae4346ca()
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

      private sealed class PROP_018a8f9b911d48c6877a91e1cc62edc7 : HardwiredMemberDescriptor
      {
        internal PROP_018a8f9b911d48c6877a91e1cc62edc7()
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

      private sealed class PROP_7710f835c5594c628eb6f3394dc7514f : HardwiredMemberDescriptor
      {
        internal PROP_7710f835c5594c628eb6f3394dc7514f()
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

      private sealed class PROP_838ebf34f376409f9da846885cd8b879 : HardwiredMemberDescriptor
      {
        internal PROP_838ebf34f376409f9da846885cd8b879()
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

      private sealed class PROP_c7950ae997be42b1a1a130a4671880d8 : HardwiredMemberDescriptor
      {
        internal PROP_c7950ae997be42b1a1a130a4671880d8()
          : base(typeof (SpellEnum), "spellEnum", false, MemberDescriptorAccess.CanRead)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((ContainerSpell) obj).spellEnum;
        }
      }

      private sealed class PROP_09018f8fb1334226abf65abc5f001e4d : HardwiredMemberDescriptor
      {
        internal PROP_09018f8fb1334226abf65abc5f001e4d()
          : base(typeof (CastType), "castType", false, MemberDescriptorAccess.CanRead)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((ContainerSpell) obj).castType;
        }
      }

      private sealed class PROP_80a8ed639a364537b1f86c91d2bd04ef : HardwiredMemberDescriptor
      {
        internal PROP_80a8ed639a364537b1f86c91d2bd04ef()
          : base(typeof (DamageType), "damageType", false, MemberDescriptorAccess.CanRead)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((ContainerSpell) obj).damageType;
        }
      }

      private sealed class PROP_70a255d9f0c1480db0f2ce098da37f12 : HardwiredMemberDescriptor
      {
        internal PROP_70a255d9f0c1480db0f2ce098da37f12()
          : base(typeof (BookOf), "book", false, MemberDescriptorAccess.CanRead)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((ContainerSpell) obj).book;
        }
      }

      private sealed class PROP_ce35ab19a9704dee8a7e39c2130d7803 : HardwiredMemberDescriptor
      {
        internal PROP_ce35ab19a9704dee8a7e39c2130d7803()
          : base(typeof (int), "damage", false, MemberDescriptorAccess.CanRead)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((ContainerSpell) obj).damage;
        }
      }

      private sealed class PROP_5b61c068ba524fbb930d4038d4376c03 : HardwiredMemberDescriptor
      {
        internal PROP_5b61c068ba524fbb930d4038d4376c03()
          : base(typeof (int), "explosionRadius", false, MemberDescriptorAccess.CanRead)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((ContainerSpell) obj).explosionRadius;
        }
      }

      private sealed class PROP_42703f477619461da342ecfcaccc9b83 : HardwiredMemberDescriptor
      {
        internal PROP_42703f477619461da342ecfcaccc9b83()
          : base(typeof (string), "description", false, MemberDescriptorAccess.CanRead)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((ContainerSpell) obj).description;
        }
      }

      private sealed class PROP_0fce7ef87ad246fc97e898a3542e715d : HardwiredMemberDescriptor
      {
        internal PROP_0fce7ef87ad246fc97e898a3542e715d()
          : base(typeof (string), "descriptionOnly", false, MemberDescriptorAccess.CanRead)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((ContainerSpell) obj).descriptionOnly;
        }
      }

      private sealed class PROP_02e6a7460f394f60ade485287f4202f0 : HardwiredMemberDescriptor
      {
        internal PROP_02e6a7460f394f60ade485287f4202f0()
          : base(typeof (string), "descriptionExtra", false, MemberDescriptorAccess.CanRead)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((ContainerSpell) obj).descriptionExtra;
        }
      }

      private sealed class PROP_6331c957829a443fb3780d55876f3439 : HardwiredMemberDescriptor
      {
        internal PROP_6331c957829a443fb3780d55876f3439()
          : base(typeof (string), "name", false, MemberDescriptorAccess.CanRead)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((ContainerSpell) obj).name;
        }
      }
    }

    private sealed class TYPE_2cc268fff90f4c3aa2d0a9bd2a92ee18 : HardwiredUserDataDescriptor
    {
      internal TYPE_2cc268fff90f4c3aa2d0a9bd2a92ee18()
        : base(typeof (ContainerTower))
      {
        this.AddMember("get_health", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_health", typeof (ContainerTower), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_2cc268fff90f4c3aa2d0a9bd2a92ee18.MTHD_64c8e74faaa843778aec80fd21b41421()
        }));
        this.AddMember("set_health", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("set_health", typeof (ContainerTower), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_2cc268fff90f4c3aa2d0a9bd2a92ee18.MTHD_a194aedd91684ab6963eaf7d2ad98d63()
        }));
        this.AddMember("get_maxHealth", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_maxHealth", typeof (ContainerTower), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_2cc268fff90f4c3aa2d0a9bd2a92ee18.MTHD_756c3d54cb4642ec9c1197699cdc887d()
        }));
        this.AddMember("set_maxHealth", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("set_maxHealth", typeof (ContainerTower), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_2cc268fff90f4c3aa2d0a9bd2a92ee18.MTHD_6a88db64c9a8468c86aadd40d583858f()
        }));
        this.AddMember("get_x", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_x", typeof (ContainerTower), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_2cc268fff90f4c3aa2d0a9bd2a92ee18.MTHD_2c64a0c26a14414296993bf26971e8a7()
        }));
        this.AddMember("set_x", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("set_x", typeof (ContainerTower), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_2cc268fff90f4c3aa2d0a9bd2a92ee18.MTHD_a681f66111c34d0581a1c884b847d2db()
        }));
        this.AddMember("get_y", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_y", typeof (ContainerTower), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_2cc268fff90f4c3aa2d0a9bd2a92ee18.MTHD_d6694e5af9b840b78cdcf765866267d6()
        }));
        this.AddMember("set_y", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("set_y", typeof (ContainerTower), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_2cc268fff90f4c3aa2d0a9bd2a92ee18.MTHD_24ce088db3a744b1a66b4dc5ce5630c5()
        }));
        this.AddMember("get_position", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_position", typeof (ContainerTower), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_2cc268fff90f4c3aa2d0a9bd2a92ee18.MTHD_089c5f6e531943eb8598889793a2c7a4()
        }));
        this.AddMember("set_position", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("set_position", typeof (ContainerTower), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_2cc268fff90f4c3aa2d0a9bd2a92ee18.MTHD_d714c3322cd3477aa265fc0cab24b578()
        }));
        this.AddMember("get_isDead", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_isDead", typeof (ContainerTower), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_2cc268fff90f4c3aa2d0a9bd2a92ee18.MTHD_5be0140fbdcc4712af3a6a87e9c4139f()
        }));
        this.AddMember("getType", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("getType", typeof (ContainerTower), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_2cc268fff90f4c3aa2d0a9bd2a92ee18.MTHD_bf9f61103b784e4cab13337e15fe27c1()
        }));
        this.AddMember("kill", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("kill", typeof (ContainerTower), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_2cc268fff90f4c3aa2d0a9bd2a92ee18.MTHD_e21482af91c54e039c3cb3bd458a8690()
        }));
        this.AddMember("GetHashCode", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("GetHashCode", typeof (ContainerTower), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_2cc268fff90f4c3aa2d0a9bd2a92ee18.MTHD_b287542968af40f193cdf24f7d4927c7()
        }));
        this.AddMember("Equals", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("Equals", typeof (ContainerTower), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_2cc268fff90f4c3aa2d0a9bd2a92ee18.MTHD_a3a8e64103524c838797e1241c00c0b5()
        }));
        this.AddMember("GetType", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("GetType", typeof (ContainerTower), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_2cc268fff90f4c3aa2d0a9bd2a92ee18.MTHD_dc5519a61aec470fa80e4a1542cc7850()
        }));
        this.AddMember("ToString", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("ToString", typeof (ContainerTower), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_2cc268fff90f4c3aa2d0a9bd2a92ee18.MTHD_c9f43c84b4224998a59520ad7fe2fc2a()
        }));
        this.AddMember("health", (IMemberDescriptor) new Bridge.TYPE_2cc268fff90f4c3aa2d0a9bd2a92ee18.PROP_676d481e588d47f48b7f3e6009b0fdec());
        this.AddMember("maxHealth", (IMemberDescriptor) new Bridge.TYPE_2cc268fff90f4c3aa2d0a9bd2a92ee18.PROP_eb45baadfbe849f4b48fee7e871992e3());
        this.AddMember("x", (IMemberDescriptor) new Bridge.TYPE_2cc268fff90f4c3aa2d0a9bd2a92ee18.PROP_31be52104a7648568bc5d93c204ada9e());
        this.AddMember("y", (IMemberDescriptor) new Bridge.TYPE_2cc268fff90f4c3aa2d0a9bd2a92ee18.PROP_a142e5f0678f4d0db90a467d6c6f62fe());
        this.AddMember("position", (IMemberDescriptor) new Bridge.TYPE_2cc268fff90f4c3aa2d0a9bd2a92ee18.PROP_d7a79094758a4733a222a2bf73fd3392());
        this.AddMember("isDead", (IMemberDescriptor) new Bridge.TYPE_2cc268fff90f4c3aa2d0a9bd2a92ee18.PROP_085b8c53ca7147c8b7e2218174ca1cd8());
      }

      private sealed class MTHD_64c8e74faaa843778aec80fd21b41421 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_64c8e74faaa843778aec80fd21b41421()
        {
          this.Initialize("get_health", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerTower) obj).health;
        }
      }

      private sealed class MTHD_a194aedd91684ab6963eaf7d2ad98d63 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_a194aedd91684ab6963eaf7d2ad98d63()
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

      private sealed class MTHD_756c3d54cb4642ec9c1197699cdc887d : HardwiredMethodMemberDescriptor
      {
        internal MTHD_756c3d54cb4642ec9c1197699cdc887d()
        {
          this.Initialize("get_maxHealth", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerTower) obj).maxHealth;
        }
      }

      private sealed class MTHD_6a88db64c9a8468c86aadd40d583858f : HardwiredMethodMemberDescriptor
      {
        internal MTHD_6a88db64c9a8468c86aadd40d583858f()
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

      private sealed class MTHD_2c64a0c26a14414296993bf26971e8a7 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_2c64a0c26a14414296993bf26971e8a7()
        {
          this.Initialize("get_x", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerTower) obj).x;
        }
      }

      private sealed class MTHD_a681f66111c34d0581a1c884b847d2db : HardwiredMethodMemberDescriptor
      {
        internal MTHD_a681f66111c34d0581a1c884b847d2db()
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

      private sealed class MTHD_d6694e5af9b840b78cdcf765866267d6 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_d6694e5af9b840b78cdcf765866267d6()
        {
          this.Initialize("get_y", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerTower) obj).y;
        }
      }

      private sealed class MTHD_24ce088db3a744b1a66b4dc5ce5630c5 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_24ce088db3a744b1a66b4dc5ce5630c5()
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

      private sealed class MTHD_089c5f6e531943eb8598889793a2c7a4 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_089c5f6e531943eb8598889793a2c7a4()
        {
          this.Initialize("get_position", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerTower) obj).position;
        }
      }

      private sealed class MTHD_d714c3322cd3477aa265fc0cab24b578 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_d714c3322cd3477aa265fc0cab24b578()
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

      private sealed class MTHD_5be0140fbdcc4712af3a6a87e9c4139f : HardwiredMethodMemberDescriptor
      {
        internal MTHD_5be0140fbdcc4712af3a6a87e9c4139f()
        {
          this.Initialize("get_isDead", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerTower) obj).isDead;
        }
      }

      private sealed class MTHD_bf9f61103b784e4cab13337e15fe27c1 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_bf9f61103b784e4cab13337e15fe27c1()
        {
          this.Initialize("getType", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerTower) obj).getType();
        }
      }

      private sealed class MTHD_e21482af91c54e039c3cb3bd458a8690 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_e21482af91c54e039c3cb3bd458a8690()
        {
          this.Initialize("kill", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          ((ContainerTower) obj).kill();
          return (object) DynValue.Void;
        }
      }

      private sealed class MTHD_b287542968af40f193cdf24f7d4927c7 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_b287542968af40f193cdf24f7d4927c7()
        {
          this.Initialize("GetHashCode", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((ContainerTower) obj).GetHashCode();
        }
      }

      private sealed class MTHD_a3a8e64103524c838797e1241c00c0b5 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_a3a8e64103524c838797e1241c00c0b5()
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

      private sealed class MTHD_dc5519a61aec470fa80e4a1542cc7850 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_dc5519a61aec470fa80e4a1542cc7850()
        {
          this.Initialize("GetType", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) obj.GetType();
        }
      }

      private sealed class MTHD_c9f43c84b4224998a59520ad7fe2fc2a : HardwiredMethodMemberDescriptor
      {
        internal MTHD_c9f43c84b4224998a59520ad7fe2fc2a()
        {
          this.Initialize("ToString", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) obj.ToString();
        }
      }

      private sealed class PROP_676d481e588d47f48b7f3e6009b0fdec : HardwiredMemberDescriptor
      {
        internal PROP_676d481e588d47f48b7f3e6009b0fdec()
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

      private sealed class PROP_eb45baadfbe849f4b48fee7e871992e3 : HardwiredMemberDescriptor
      {
        internal PROP_eb45baadfbe849f4b48fee7e871992e3()
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

      private sealed class PROP_31be52104a7648568bc5d93c204ada9e : HardwiredMemberDescriptor
      {
        internal PROP_31be52104a7648568bc5d93c204ada9e()
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

      private sealed class PROP_a142e5f0678f4d0db90a467d6c6f62fe : HardwiredMemberDescriptor
      {
        internal PROP_a142e5f0678f4d0db90a467d6c6f62fe()
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

      private sealed class PROP_d7a79094758a4733a222a2bf73fd3392 : HardwiredMemberDescriptor
      {
        internal PROP_d7a79094758a4733a222a2bf73fd3392()
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

      private sealed class PROP_085b8c53ca7147c8b7e2218174ca1cd8 : HardwiredMemberDescriptor
      {
        internal PROP_085b8c53ca7147c8b7e2218174ca1cd8()
          : base(typeof (bool), "isDead", false, MemberDescriptorAccess.CanRead)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((ContainerTower) obj).isDead;
        }
      }
    }

    private sealed class TYPE_1c0f540c44204c3fa838f7f5d5b02290 : HardwiredUserDataDescriptor
    {
      internal TYPE_1c0f540c44204c3fa838f7f5d5b02290()
        : base(typeof (Educative.Point))
      {
        this.AddMember("__new", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("__new", typeof (Educative.Point), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[2]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_1c0f540c44204c3fa838f7f5d5b02290.MTHD_5f808854426341ef909bd07ec41e266b(),
          (IOverloadableMemberDescriptor) new Bridge.TYPE_1c0f540c44204c3fa838f7f5d5b02290.MTHD_89ea6dd9ae9d4522ae3dafceb35dc592()
        }));
        this.AddMember("distance", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("distance", typeof (Educative.Point), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[2]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_1c0f540c44204c3fa838f7f5d5b02290.MTHD_b6bb0721bfac4a48aa41c972d1f335c4(),
          (IOverloadableMemberDescriptor) new Bridge.TYPE_1c0f540c44204c3fa838f7f5d5b02290.MTHD_eccb03cc91e046d0931837b747883b76()
        }));
        this.AddMember("construct", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("construct", typeof (Educative.Point), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_1c0f540c44204c3fa838f7f5d5b02290.MTHD_e718b936c8ba4127827c46a342ebcf5b()
        }));
        this.AddMember("copy", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("copy", typeof (Educative.Point), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_1c0f540c44204c3fa838f7f5d5b02290.MTHD_7e52921fece94a13b6fa9206bd3fc876()
        }));
        this.AddMember("normalized", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("normalized", typeof (Educative.Point), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_1c0f540c44204c3fa838f7f5d5b02290.MTHD_37501a84017344b58f08b2dc08edb5cb()
        }));
        this.AddMember("normalize", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("normalize", typeof (Educative.Point), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_1c0f540c44204c3fa838f7f5d5b02290.MTHD_c8d6420072914debbc18b4b37d5e2fc6()
        }));
        this.AddMember("Equals", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("Equals", typeof (Educative.Point), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_1c0f540c44204c3fa838f7f5d5b02290.MTHD_ddecc3f673b1443cbaaba125e1ed5f94()
        }));
        this.AddMember("GetHashCode", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("GetHashCode", typeof (Educative.Point), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_1c0f540c44204c3fa838f7f5d5b02290.MTHD_68537535da3943a1810def8c497c27ad()
        }));
        this.AddMember("op_Equality", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("op_Equality", typeof (Educative.Point), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_1c0f540c44204c3fa838f7f5d5b02290.MTHD_e1a09af151bf49df87a772df36e6c90c()
        }));
        this.AddMember("op_Subtraction", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("op_Subtraction", typeof (Educative.Point), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[2]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_1c0f540c44204c3fa838f7f5d5b02290.MTHD_e953a070b99e4698b9d809a781851b4a(),
          (IOverloadableMemberDescriptor) new Bridge.TYPE_1c0f540c44204c3fa838f7f5d5b02290.MTHD_b05333436ba248649b13647c4b62429d()
        }));
        this.AddMember("op_Addition", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("op_Addition", typeof (Educative.Point), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[2]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_1c0f540c44204c3fa838f7f5d5b02290.MTHD_9dd1ae4fec33468fb07dbe20e600f5ee(),
          (IOverloadableMemberDescriptor) new Bridge.TYPE_1c0f540c44204c3fa838f7f5d5b02290.MTHD_560cbdd4551f4ea89e7df21817fe6598()
        }));
        this.AddMember("op_Multiply", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("op_Multiply", typeof (Educative.Point), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[2]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_1c0f540c44204c3fa838f7f5d5b02290.MTHD_65bac04d817f43d5809668a258b887cc(),
          (IOverloadableMemberDescriptor) new Bridge.TYPE_1c0f540c44204c3fa838f7f5d5b02290.MTHD_8ecea52b24714260ba83483395a593fa()
        }));
        this.AddMember("op_Division", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("op_Division", typeof (Educative.Point), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[2]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_1c0f540c44204c3fa838f7f5d5b02290.MTHD_6f4d4584463743a1bdd9f268d9326f1f(),
          (IOverloadableMemberDescriptor) new Bridge.TYPE_1c0f540c44204c3fa838f7f5d5b02290.MTHD_f112f76fb9bf46a0843d1609b2e20679()
        }));
        this.AddMember("op_Modulus", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("op_Modulus", typeof (Educative.Point), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[2]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_1c0f540c44204c3fa838f7f5d5b02290.MTHD_10b5f199abd74b1aaa3bb8b73c6e759a(),
          (IOverloadableMemberDescriptor) new Bridge.TYPE_1c0f540c44204c3fa838f7f5d5b02290.MTHD_166e67d8dd6a40f0a0f17a4a3532a967()
        }));
        this.AddMember("op_Inequality", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("op_Inequality", typeof (Educative.Point), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_1c0f540c44204c3fa838f7f5d5b02290.MTHD_42297609a9414da188452728c5f7dae6()
        }));
        this.AddMember("GetType", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("GetType", typeof (Educative.Point), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_1c0f540c44204c3fa838f7f5d5b02290.MTHD_07abbb874f3e44028c0c5afccf4f8b48()
        }));
        this.AddMember("x", (IMemberDescriptor) new Bridge.TYPE_1c0f540c44204c3fa838f7f5d5b02290.FLDV_3753bf46c0fd494f8247fa276cf2d8ce());
        this.AddMember("y", (IMemberDescriptor) new Bridge.TYPE_1c0f540c44204c3fa838f7f5d5b02290.FLDV_b72794285fa24d27a38687c2a154ce79());
      }

      private sealed class MTHD_5f808854426341ef909bd07ec41e266b : HardwiredMethodMemberDescriptor
      {
        internal MTHD_5f808854426341ef909bd07ec41e266b()
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

      private sealed class MTHD_89ea6dd9ae9d4522ae3dafceb35dc592 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_89ea6dd9ae9d4522ae3dafceb35dc592()
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

      private sealed class MTHD_b6bb0721bfac4a48aa41c972d1f335c4 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_b6bb0721bfac4a48aa41c972d1f335c4()
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

      private sealed class MTHD_eccb03cc91e046d0931837b747883b76 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_eccb03cc91e046d0931837b747883b76()
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

      private sealed class MTHD_e718b936c8ba4127827c46a342ebcf5b : HardwiredMethodMemberDescriptor
      {
        internal MTHD_e718b936c8ba4127827c46a342ebcf5b()
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

      private sealed class MTHD_7e52921fece94a13b6fa9206bd3fc876 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_7e52921fece94a13b6fa9206bd3fc876()
        {
          this.Initialize("copy", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((Educative.Point) obj).copy();
        }
      }

      private sealed class MTHD_37501a84017344b58f08b2dc08edb5cb : HardwiredMethodMemberDescriptor
      {
        internal MTHD_37501a84017344b58f08b2dc08edb5cb()
        {
          this.Initialize("normalized", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((Educative.Point) obj).normalized();
        }
      }

      private sealed class MTHD_c8d6420072914debbc18b4b37d5e2fc6 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_c8d6420072914debbc18b4b37d5e2fc6()
        {
          this.Initialize("normalize", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          ((Educative.Point) obj).normalize();
          return (object) DynValue.Void;
        }
      }

      private sealed class MTHD_ddecc3f673b1443cbaaba125e1ed5f94 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_ddecc3f673b1443cbaaba125e1ed5f94()
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

      private sealed class MTHD_68537535da3943a1810def8c497c27ad : HardwiredMethodMemberDescriptor
      {
        internal MTHD_68537535da3943a1810def8c497c27ad()
        {
          this.Initialize("GetHashCode", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((Educative.Point) obj).GetHashCode();
        }
      }

      private sealed class MTHD_e1a09af151bf49df87a772df36e6c90c : HardwiredMethodMemberDescriptor
      {
        internal MTHD_e1a09af151bf49df87a772df36e6c90c()
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

      private sealed class MTHD_e953a070b99e4698b9d809a781851b4a : HardwiredMethodMemberDescriptor
      {
        internal MTHD_e953a070b99e4698b9d809a781851b4a()
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

      private sealed class MTHD_b05333436ba248649b13647c4b62429d : HardwiredMethodMemberDescriptor
      {
        internal MTHD_b05333436ba248649b13647c4b62429d()
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

      private sealed class MTHD_9dd1ae4fec33468fb07dbe20e600f5ee : HardwiredMethodMemberDescriptor
      {
        internal MTHD_9dd1ae4fec33468fb07dbe20e600f5ee()
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

      private sealed class MTHD_560cbdd4551f4ea89e7df21817fe6598 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_560cbdd4551f4ea89e7df21817fe6598()
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

      private sealed class MTHD_65bac04d817f43d5809668a258b887cc : HardwiredMethodMemberDescriptor
      {
        internal MTHD_65bac04d817f43d5809668a258b887cc()
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

      private sealed class MTHD_8ecea52b24714260ba83483395a593fa : HardwiredMethodMemberDescriptor
      {
        internal MTHD_8ecea52b24714260ba83483395a593fa()
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

      private sealed class MTHD_6f4d4584463743a1bdd9f268d9326f1f : HardwiredMethodMemberDescriptor
      {
        internal MTHD_6f4d4584463743a1bdd9f268d9326f1f()
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

      private sealed class MTHD_f112f76fb9bf46a0843d1609b2e20679 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_f112f76fb9bf46a0843d1609b2e20679()
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

      private sealed class MTHD_10b5f199abd74b1aaa3bb8b73c6e759a : HardwiredMethodMemberDescriptor
      {
        internal MTHD_10b5f199abd74b1aaa3bb8b73c6e759a()
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

      private sealed class MTHD_166e67d8dd6a40f0a0f17a4a3532a967 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_166e67d8dd6a40f0a0f17a4a3532a967()
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

      private sealed class MTHD_42297609a9414da188452728c5f7dae6 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_42297609a9414da188452728c5f7dae6()
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

      private sealed class MTHD_07abbb874f3e44028c0c5afccf4f8b48 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_07abbb874f3e44028c0c5afccf4f8b48()
        {
          this.Initialize("GetType", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) obj.GetType();
        }
      }

      private sealed class FLDV_3753bf46c0fd494f8247fa276cf2d8ce : HardwiredMemberDescriptor
      {
        internal FLDV_3753bf46c0fd494f8247fa276cf2d8ce()
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

      private sealed class FLDV_b72794285fa24d27a38687c2a154ce79 : HardwiredMemberDescriptor
      {
        internal FLDV_b72794285fa24d27a38687c2a154ce79()
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

    private sealed class TYPE_5e7de831bd6e4560964114c8b777928e : HardwiredUserDataDescriptor
    {
      internal TYPE_5e7de831bd6e4560964114c8b777928e()
        : base(typeof (Summon))
      {
        this.AddMember("__new", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("__new", typeof (Summon), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_5e7de831bd6e4560964114c8b777928e.MTHD_00fa6a4886e747cfbde682b3dcf78ba5()
        }));
        this.AddMember("construct", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("construct", typeof (Summon), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_5e7de831bd6e4560964114c8b777928e.MTHD_3765c307e5ad44379844c9093bdc204b()
        }));
        this.AddMember("Equals", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("Equals", typeof (Summon), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_5e7de831bd6e4560964114c8b777928e.MTHD_71a870aa00e846e7bed9a7e357fcaa01()
        }));
        this.AddMember("GetHashCode", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("GetHashCode", typeof (Summon), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_5e7de831bd6e4560964114c8b777928e.MTHD_6be30ad5a6c24521b237c62f23b05d3e()
        }));
        this.AddMember("GetType", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("GetType", typeof (Summon), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_5e7de831bd6e4560964114c8b777928e.MTHD_9138d1102bfa4d699b797aa606f05baa()
        }));
        this.AddMember("ToString", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("ToString", typeof (Summon), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_5e7de831bd6e4560964114c8b777928e.MTHD_e6b222beaf744003a1f6ba2abca97590()
        }));
        this.AddMember("spell", (IMemberDescriptor) new Bridge.TYPE_5e7de831bd6e4560964114c8b777928e.FLDV_5442ff72958a419d9d7fc7304aa07041());
        this.AddMember("position", (IMemberDescriptor) new Bridge.TYPE_5e7de831bd6e4560964114c8b777928e.FLDV_ab0639ec918b43d1809a9d601176358d());
        this.AddMember("team", (IMemberDescriptor) new Bridge.TYPE_5e7de831bd6e4560964114c8b777928e.FLDV_cab1151bb8da4f4a90250a06b5c1310f());
        this.AddMember("onPlayersPanel", (IMemberDescriptor) new Bridge.TYPE_5e7de831bd6e4560964114c8b777928e.FLDV_9ec1e851ed004ed59cbe7139e39bd0a4());
        this.AddMember("useAI", (IMemberDescriptor) new Bridge.TYPE_5e7de831bd6e4560964114c8b777928e.FLDV_85d4ca43864f439089c8edd722e1deab());
        this.AddMember("no_AI_still_use_turn", (IMemberDescriptor) new Bridge.TYPE_5e7de831bd6e4560964114c8b777928e.FLDV_c2eff7fb40714b5582e50b307fb0af92());
        this.AddMember("name", (IMemberDescriptor) new Bridge.TYPE_5e7de831bd6e4560964114c8b777928e.FLDV_98b259696ed84de4b5f1b4955ce0055c());
        this.AddMember("playSound", (IMemberDescriptor) new Bridge.TYPE_5e7de831bd6e4560964114c8b777928e.FLDV_500ee08491e5445b9cb2ce357259172d());
        this.AddMember("colors", (IMemberDescriptor) new Bridge.TYPE_5e7de831bd6e4560964114c8b777928e.FLDV_38dafdfce0dc4a359ec6e1ee968bf5dc());
        this.AddMember("spells", (IMemberDescriptor) new Bridge.TYPE_5e7de831bd6e4560964114c8b777928e.FLDV_0b5a0814a2534f959d2a655a31a25bda());
        this.AddMember("outfit", (IMemberDescriptor) new Bridge.TYPE_5e7de831bd6e4560964114c8b777928e.FLDV_687d649a287841db8776086173e40e88());
        this.AddMember("elemental", (IMemberDescriptor) new Bridge.TYPE_5e7de831bd6e4560964114c8b777928e.FLDV_198523c949de4d41853b51749207bf1c());
      }

      private sealed class MTHD_00fa6a4886e747cfbde682b3dcf78ba5 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_00fa6a4886e747cfbde682b3dcf78ba5()
        {
          this.Initialize(".ctor", true, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) new Summon();
        }
      }

      private sealed class MTHD_3765c307e5ad44379844c9093bdc204b : HardwiredMethodMemberDescriptor
      {
        internal MTHD_3765c307e5ad44379844c9093bdc204b()
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

      private sealed class MTHD_71a870aa00e846e7bed9a7e357fcaa01 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_71a870aa00e846e7bed9a7e357fcaa01()
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

      private sealed class MTHD_6be30ad5a6c24521b237c62f23b05d3e : HardwiredMethodMemberDescriptor
      {
        internal MTHD_6be30ad5a6c24521b237c62f23b05d3e()
        {
          this.Initialize("GetHashCode", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) obj.GetHashCode();
        }
      }

      private sealed class MTHD_9138d1102bfa4d699b797aa606f05baa : HardwiredMethodMemberDescriptor
      {
        internal MTHD_9138d1102bfa4d699b797aa606f05baa()
        {
          this.Initialize("GetType", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) obj.GetType();
        }
      }

      private sealed class MTHD_e6b222beaf744003a1f6ba2abca97590 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_e6b222beaf744003a1f6ba2abca97590()
        {
          this.Initialize("ToString", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) obj.ToString();
        }
      }

      private sealed class FLDV_5442ff72958a419d9d7fc7304aa07041 : HardwiredMemberDescriptor
      {
        internal FLDV_5442ff72958a419d9d7fc7304aa07041()
          : base(typeof (object), "spell", false, MemberDescriptorAccess.CanRead | MemberDescriptorAccess.CanWrite)
        {
        }

        protected override object GetValueImpl(Script script, object obj) => ((Summon) obj).spell;

        protected override void SetValueImpl(Script script, object obj, object value)
        {
          ((Summon) obj).spell = value;
        }
      }

      private sealed class FLDV_ab0639ec918b43d1809a9d601176358d : HardwiredMemberDescriptor
      {
        internal FLDV_ab0639ec918b43d1809a9d601176358d()
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

      private sealed class FLDV_cab1151bb8da4f4a90250a06b5c1310f : HardwiredMemberDescriptor
      {
        internal FLDV_cab1151bb8da4f4a90250a06b5c1310f()
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

      private sealed class FLDV_9ec1e851ed004ed59cbe7139e39bd0a4 : HardwiredMemberDescriptor
      {
        internal FLDV_9ec1e851ed004ed59cbe7139e39bd0a4()
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

      private sealed class FLDV_85d4ca43864f439089c8edd722e1deab : HardwiredMemberDescriptor
      {
        internal FLDV_85d4ca43864f439089c8edd722e1deab()
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

      private sealed class FLDV_c2eff7fb40714b5582e50b307fb0af92 : HardwiredMemberDescriptor
      {
        internal FLDV_c2eff7fb40714b5582e50b307fb0af92()
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

      private sealed class FLDV_98b259696ed84de4b5f1b4955ce0055c : HardwiredMemberDescriptor
      {
        internal FLDV_98b259696ed84de4b5f1b4955ce0055c()
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

      private sealed class FLDV_500ee08491e5445b9cb2ce357259172d : HardwiredMemberDescriptor
      {
        internal FLDV_500ee08491e5445b9cb2ce357259172d()
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

      private sealed class FLDV_38dafdfce0dc4a359ec6e1ee968bf5dc : HardwiredMemberDescriptor
      {
        internal FLDV_38dafdfce0dc4a359ec6e1ee968bf5dc()
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

      private sealed class FLDV_0b5a0814a2534f959d2a655a31a25bda : HardwiredMemberDescriptor
      {
        internal FLDV_0b5a0814a2534f959d2a655a31a25bda()
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

      private sealed class FLDV_687d649a287841db8776086173e40e88 : HardwiredMemberDescriptor
      {
        internal FLDV_687d649a287841db8776086173e40e88()
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

      private sealed class FLDV_198523c949de4d41853b51749207bf1c : HardwiredMemberDescriptor
      {
        internal FLDV_198523c949de4d41853b51749207bf1c()
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

    private sealed class TYPE_b3e8f0bef7044e8ea2e318068adb7316 : HardwiredUserDataDescriptor
    {
      internal TYPE_b3e8f0bef7044e8ea2e318068adb7316()
        : base(typeof (UIElement))
      {
        this.AddMember("__new", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("__new", typeof (UIElement), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_b3e8f0bef7044e8ea2e318068adb7316.MTHD_feda7bace67c4f9aa8418b10e7740b33()
        }));
        this.AddMember("get_isDead", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_isDead", typeof (UIElement), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_b3e8f0bef7044e8ea2e318068adb7316.MTHD_1a25d92a6a2241b6a5eb65f75b56b2f5()
        }));
        this.AddMember("get_graphic", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_graphic", typeof (UIElement), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_b3e8f0bef7044e8ea2e318068adb7316.MTHD_afdeb0aee5e14acebc5c196da85795e1()
        }));
        this.AddMember("set_graphic", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("set_graphic", typeof (UIElement), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_b3e8f0bef7044e8ea2e318068adb7316.MTHD_14d9d1f0cb4a462fa2f541dea468526b()
        }));
        this.AddMember("get_anchor", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_anchor", typeof (UIElement), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_b3e8f0bef7044e8ea2e318068adb7316.MTHD_1714415934e04aa0903518efd03252b1()
        }));
        this.AddMember("set_anchor", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("set_anchor", typeof (UIElement), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_b3e8f0bef7044e8ea2e318068adb7316.MTHD_12bbd074f3ba4ed9b5da1b869a423699()
        }));
        this.AddMember("get_width", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_width", typeof (UIElement), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_b3e8f0bef7044e8ea2e318068adb7316.MTHD_b10472e61ce546e5b54206d07a168716()
        }));
        this.AddMember("set_width", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("set_width", typeof (UIElement), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_b3e8f0bef7044e8ea2e318068adb7316.MTHD_8dd48df26b7f4f198abf46fb6728092d()
        }));
        this.AddMember("get_height", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_height", typeof (UIElement), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_b3e8f0bef7044e8ea2e318068adb7316.MTHD_c4ba808f6f2f484aba9f8cbf9fc060a5()
        }));
        this.AddMember("set_height", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("set_height", typeof (UIElement), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_b3e8f0bef7044e8ea2e318068adb7316.MTHD_9b9891f174c14492a120a3479b98567a()
        }));
        this.AddMember("get_size", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_size", typeof (UIElement), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_b3e8f0bef7044e8ea2e318068adb7316.MTHD_a91e486944db435da7ae5e67201e468a()
        }));
        this.AddMember("set_size", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("set_size", typeof (UIElement), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_b3e8f0bef7044e8ea2e318068adb7316.MTHD_b19a507629904b26b663640b3e411c7e()
        }));
        this.AddMember("get_x", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_x", typeof (UIElement), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_b3e8f0bef7044e8ea2e318068adb7316.MTHD_a8ea993927af40d5b25419567a67ecf1()
        }));
        this.AddMember("set_x", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("set_x", typeof (UIElement), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_b3e8f0bef7044e8ea2e318068adb7316.MTHD_adffa8c21495496697bfd72026f4b77a()
        }));
        this.AddMember("get_y", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_y", typeof (UIElement), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_b3e8f0bef7044e8ea2e318068adb7316.MTHD_21a9355400b248e2829d9b82bbe5844a()
        }));
        this.AddMember("set_y", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("set_y", typeof (UIElement), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_b3e8f0bef7044e8ea2e318068adb7316.MTHD_972f0326bab8445880c64e4a1e1be7c2()
        }));
        this.AddMember("get_position", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_position", typeof (UIElement), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_b3e8f0bef7044e8ea2e318068adb7316.MTHD_a40df9526b534a78824982cee4f877e7()
        }));
        this.AddMember("set_position", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("set_position", typeof (UIElement), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_b3e8f0bef7044e8ea2e318068adb7316.MTHD_6fac11e9129b4ab8bc2db52d29505487()
        }));
        this.AddMember("get_pivot_x", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_pivot_x", typeof (UIElement), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_b3e8f0bef7044e8ea2e318068adb7316.MTHD_f4eddeb1c82c4d22adab77215c5e14d1()
        }));
        this.AddMember("set_pivot_x", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("set_pivot_x", typeof (UIElement), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_b3e8f0bef7044e8ea2e318068adb7316.MTHD_109310c3b16440e593a7c2177da29bb9()
        }));
        this.AddMember("get_pivot_y", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_pivot_y", typeof (UIElement), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_b3e8f0bef7044e8ea2e318068adb7316.MTHD_f933e61a98ce47908bdbf700acb013b4()
        }));
        this.AddMember("set_pivot_y", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("set_pivot_y", typeof (UIElement), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_b3e8f0bef7044e8ea2e318068adb7316.MTHD_cd59f546513b40b3a2aa7586ab82a125()
        }));
        this.AddMember("get_pivot", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_pivot", typeof (UIElement), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_b3e8f0bef7044e8ea2e318068adb7316.MTHD_4dd1e7953b384b179c52d8f5e1f696cf()
        }));
        this.AddMember("set_pivot", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("set_pivot", typeof (UIElement), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_b3e8f0bef7044e8ea2e318068adb7316.MTHD_d7995e0d9a4b4875aa4e03d7646ab300()
        }));
        this.AddMember("get_angle", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_angle", typeof (UIElement), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_b3e8f0bef7044e8ea2e318068adb7316.MTHD_d5f6198664be400cb8b3ce93e65c4d42()
        }));
        this.AddMember("set_angle", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("set_angle", typeof (UIElement), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_b3e8f0bef7044e8ea2e318068adb7316.MTHD_7ca82e52543d4ec98535489a745a4610()
        }));
        this.AddMember("get_onClick", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_onClick", typeof (UIElement), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_b3e8f0bef7044e8ea2e318068adb7316.MTHD_c19783dfb6d64b1eb735fda598b42e36()
        }));
        this.AddMember("set_onClick", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("set_onClick", typeof (UIElement), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_b3e8f0bef7044e8ea2e318068adb7316.MTHD_4182dcda150a403d971689f731362c51()
        }));
        this.AddMember("get_color", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_color", typeof (UIElement), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_b3e8f0bef7044e8ea2e318068adb7316.MTHD_fae425c9f49049f6bdbd757c4150ad39()
        }));
        this.AddMember("set_color", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("set_color", typeof (UIElement), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_b3e8f0bef7044e8ea2e318068adb7316.MTHD_03421eafe8dc4262ab45151e6a022f15()
        }));
        this.AddMember("get_text", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_text", typeof (UIElement), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_b3e8f0bef7044e8ea2e318068adb7316.MTHD_6db645627313428a95f0b582bed0ce47()
        }));
        this.AddMember("set_text", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("set_text", typeof (UIElement), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_b3e8f0bef7044e8ea2e318068adb7316.MTHD_18c6877ae9be4272a96a98e77d8447ad()
        }));
        this.AddMember("get_textColor", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_textColor", typeof (UIElement), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_b3e8f0bef7044e8ea2e318068adb7316.MTHD_cf998ec1a01c40b090f66fc3c482b7a1()
        }));
        this.AddMember("set_textColor", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("set_textColor", typeof (UIElement), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_b3e8f0bef7044e8ea2e318068adb7316.MTHD_7045dfed517f4bf9bfc7205a8fd5730f()
        }));
        this.AddMember("get_textStyle", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_textStyle", typeof (UIElement), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_b3e8f0bef7044e8ea2e318068adb7316.MTHD_1868d016c3ee4f6698efd415b24b53ff()
        }));
        this.AddMember("set_textStyle", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("set_textStyle", typeof (UIElement), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_b3e8f0bef7044e8ea2e318068adb7316.MTHD_08e8cdc04c9143d6b3e84d9efd2b751d()
        }));
        this.AddMember("get_textSize", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("get_textSize", typeof (UIElement), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_b3e8f0bef7044e8ea2e318068adb7316.MTHD_e9d0d2084ced4dc9b8333a6efd1fe4d0()
        }));
        this.AddMember("set_textSize", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("set_textSize", typeof (UIElement), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_b3e8f0bef7044e8ea2e318068adb7316.MTHD_bc81a81f6d3c4539b2674bded9dbc40e()
        }));
        this.AddMember("activateInput", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("activateInput", typeof (UIElement), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_b3e8f0bef7044e8ea2e318068adb7316.MTHD_4b113d2a6cdd478ebc2a3101bad75826()
        }));
        this.AddMember("createUI", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("createUI", typeof (UIElement), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_b3e8f0bef7044e8ea2e318068adb7316.MTHD_a3e5463e279d495fa9c0a3d6e7460907()
        }));
        this.AddMember("createInput", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("createInput", typeof (UIElement), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_b3e8f0bef7044e8ea2e318068adb7316.MTHD_ee59e8b3f95b4c82b0efafd32f7109e3()
        }));
        this.AddMember("duplicate", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("duplicate", typeof (UIElement), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_b3e8f0bef7044e8ea2e318068adb7316.MTHD_cae8b5ac800748dfbfa4c8ee0bc5514c()
        }));
        this.AddMember("destroyHierarchy", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("destroyHierarchy", typeof (UIElement), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_b3e8f0bef7044e8ea2e318068adb7316.MTHD_2cea8ad19ec74e8bbcbfbc94b0c4a5d7()
        }));
        this.AddMember("destroy", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("destroy", typeof (UIElement), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_b3e8f0bef7044e8ea2e318068adb7316.MTHD_2d6e5de6bfdc4e718cb914d13ae68f32()
        }));
        this.AddMember("Equals", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("Equals", typeof (UIElement), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_b3e8f0bef7044e8ea2e318068adb7316.MTHD_d4f2484f658b4cec8865ee04d25f8367()
        }));
        this.AddMember("GetHashCode", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("GetHashCode", typeof (UIElement), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_b3e8f0bef7044e8ea2e318068adb7316.MTHD_151154231b35469d986f50d508fd0f44()
        }));
        this.AddMember("GetType", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("GetType", typeof (UIElement), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_b3e8f0bef7044e8ea2e318068adb7316.MTHD_3138ce06fd904751b5a7fc90d5cf1603()
        }));
        this.AddMember("ToString", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("ToString", typeof (UIElement), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_b3e8f0bef7044e8ea2e318068adb7316.MTHD_4a3ece720c4f4fdabe18f5d72aec55f1()
        }));
        this.AddMember("isDead", (IMemberDescriptor) new Bridge.TYPE_b3e8f0bef7044e8ea2e318068adb7316.PROP_1f3003154c824452ad6e700c02749793());
        this.AddMember("graphic", (IMemberDescriptor) new Bridge.TYPE_b3e8f0bef7044e8ea2e318068adb7316.PROP_ec1b4bc10143443db600690c300194b3());
        this.AddMember("anchor", (IMemberDescriptor) new Bridge.TYPE_b3e8f0bef7044e8ea2e318068adb7316.PROP_6b7f1b71f4744062a3ca5c6a72cb04b3());
        this.AddMember("width", (IMemberDescriptor) new Bridge.TYPE_b3e8f0bef7044e8ea2e318068adb7316.PROP_e257e30f588c4205a84d9b24a515ce01());
        this.AddMember("height", (IMemberDescriptor) new Bridge.TYPE_b3e8f0bef7044e8ea2e318068adb7316.PROP_9f185cc1ae944bd1817b8c48cf0c62fa());
        this.AddMember("size", (IMemberDescriptor) new Bridge.TYPE_b3e8f0bef7044e8ea2e318068adb7316.PROP_7ec6beeeb7544b1e9058c8f9956ca7d6());
        this.AddMember("x", (IMemberDescriptor) new Bridge.TYPE_b3e8f0bef7044e8ea2e318068adb7316.PROP_e1587c0182c8494696e1a8e6dd353667());
        this.AddMember("y", (IMemberDescriptor) new Bridge.TYPE_b3e8f0bef7044e8ea2e318068adb7316.PROP_8fadcb831e544081896d9d3ff6604f7a());
        this.AddMember("position", (IMemberDescriptor) new Bridge.TYPE_b3e8f0bef7044e8ea2e318068adb7316.PROP_5263ceaf9e314f76ab673bb2c8f36436());
        this.AddMember("pivot_x", (IMemberDescriptor) new Bridge.TYPE_b3e8f0bef7044e8ea2e318068adb7316.PROP_e8e06f59caef4e71a91ba5da9f0cce88());
        this.AddMember("pivot_y", (IMemberDescriptor) new Bridge.TYPE_b3e8f0bef7044e8ea2e318068adb7316.PROP_7f218be6b7524664b3e444220618cefa());
        this.AddMember("pivot", (IMemberDescriptor) new Bridge.TYPE_b3e8f0bef7044e8ea2e318068adb7316.PROP_c1a95a8cfe154368be03bc02b2f1f754());
        this.AddMember("angle", (IMemberDescriptor) new Bridge.TYPE_b3e8f0bef7044e8ea2e318068adb7316.PROP_70eda7c599534f4e833443e9c389af43());
        this.AddMember("onClick", (IMemberDescriptor) new Bridge.TYPE_b3e8f0bef7044e8ea2e318068adb7316.PROP_6333fb03256d4f0bbe3d120fc7d4bdb3());
        this.AddMember("color", (IMemberDescriptor) new Bridge.TYPE_b3e8f0bef7044e8ea2e318068adb7316.PROP_5dc3b5d156b847bfb05f84a10e0bdfdb());
        this.AddMember("text", (IMemberDescriptor) new Bridge.TYPE_b3e8f0bef7044e8ea2e318068adb7316.PROP_d8493271556a475b9e6186d2f0f9472f());
        this.AddMember("textColor", (IMemberDescriptor) new Bridge.TYPE_b3e8f0bef7044e8ea2e318068adb7316.PROP_ee7367ade38d44408ea9035be0f606cd());
        this.AddMember("textStyle", (IMemberDescriptor) new Bridge.TYPE_b3e8f0bef7044e8ea2e318068adb7316.PROP_ed611977568f48779299698619516b4b());
        this.AddMember("textSize", (IMemberDescriptor) new Bridge.TYPE_b3e8f0bef7044e8ea2e318068adb7316.PROP_4feb7268d0194a59b39eda105a800480());
        this.AddMember("parent", (IMemberDescriptor) new Bridge.TYPE_b3e8f0bef7044e8ea2e318068adb7316.FLDV_eeb07d8a21fd43aaa868c0df6084387d());
        this.AddMember("clickID", (IMemberDescriptor) new Bridge.TYPE_b3e8f0bef7044e8ea2e318068adb7316.FLDV_e424c3f20bad4ce8898cd5df45553f45());
        this.AddMember("clickDestroy", (IMemberDescriptor) new Bridge.TYPE_b3e8f0bef7044e8ea2e318068adb7316.FLDV_01be07f5cc59431791acbcee573a8861());
      }

      private sealed class MTHD_feda7bace67c4f9aa8418b10e7740b33 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_feda7bace67c4f9aa8418b10e7740b33()
        {
          this.Initialize(".ctor", true, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) new UIElement();
        }
      }

      private sealed class MTHD_1a25d92a6a2241b6a5eb65f75b56b2f5 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_1a25d92a6a2241b6a5eb65f75b56b2f5()
        {
          this.Initialize("get_isDead", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((UIElement) obj).isDead;
        }
      }

      private sealed class MTHD_afdeb0aee5e14acebc5c196da85795e1 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_afdeb0aee5e14acebc5c196da85795e1()
        {
          this.Initialize("get_graphic", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return ((UIElement) obj).graphic;
        }
      }

      private sealed class MTHD_14d9d1f0cb4a462fa2f541dea468526b : HardwiredMethodMemberDescriptor
      {
        internal MTHD_14d9d1f0cb4a462fa2f541dea468526b()
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

      private sealed class MTHD_1714415934e04aa0903518efd03252b1 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_1714415934e04aa0903518efd03252b1()
        {
          this.Initialize("get_anchor", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((UIElement) obj).anchor;
        }
      }

      private sealed class MTHD_12bbd074f3ba4ed9b5da1b869a423699 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_12bbd074f3ba4ed9b5da1b869a423699()
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

      private sealed class MTHD_b10472e61ce546e5b54206d07a168716 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_b10472e61ce546e5b54206d07a168716()
        {
          this.Initialize("get_width", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((UIElement) obj).width;
        }
      }

      private sealed class MTHD_8dd48df26b7f4f198abf46fb6728092d : HardwiredMethodMemberDescriptor
      {
        internal MTHD_8dd48df26b7f4f198abf46fb6728092d()
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

      private sealed class MTHD_c4ba808f6f2f484aba9f8cbf9fc060a5 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_c4ba808f6f2f484aba9f8cbf9fc060a5()
        {
          this.Initialize("get_height", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((UIElement) obj).height;
        }
      }

      private sealed class MTHD_9b9891f174c14492a120a3479b98567a : HardwiredMethodMemberDescriptor
      {
        internal MTHD_9b9891f174c14492a120a3479b98567a()
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

      private sealed class MTHD_a91e486944db435da7ae5e67201e468a : HardwiredMethodMemberDescriptor
      {
        internal MTHD_a91e486944db435da7ae5e67201e468a()
        {
          this.Initialize("get_size", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((UIElement) obj).size;
        }
      }

      private sealed class MTHD_b19a507629904b26b663640b3e411c7e : HardwiredMethodMemberDescriptor
      {
        internal MTHD_b19a507629904b26b663640b3e411c7e()
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

      private sealed class MTHD_a8ea993927af40d5b25419567a67ecf1 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_a8ea993927af40d5b25419567a67ecf1()
        {
          this.Initialize("get_x", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((UIElement) obj).x;
        }
      }

      private sealed class MTHD_adffa8c21495496697bfd72026f4b77a : HardwiredMethodMemberDescriptor
      {
        internal MTHD_adffa8c21495496697bfd72026f4b77a()
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

      private sealed class MTHD_21a9355400b248e2829d9b82bbe5844a : HardwiredMethodMemberDescriptor
      {
        internal MTHD_21a9355400b248e2829d9b82bbe5844a()
        {
          this.Initialize("get_y", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((UIElement) obj).y;
        }
      }

      private sealed class MTHD_972f0326bab8445880c64e4a1e1be7c2 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_972f0326bab8445880c64e4a1e1be7c2()
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

      private sealed class MTHD_a40df9526b534a78824982cee4f877e7 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_a40df9526b534a78824982cee4f877e7()
        {
          this.Initialize("get_position", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((UIElement) obj).position;
        }
      }

      private sealed class MTHD_6fac11e9129b4ab8bc2db52d29505487 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_6fac11e9129b4ab8bc2db52d29505487()
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

      private sealed class MTHD_f4eddeb1c82c4d22adab77215c5e14d1 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_f4eddeb1c82c4d22adab77215c5e14d1()
        {
          this.Initialize("get_pivot_x", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((UIElement) obj).pivot_x;
        }
      }

      private sealed class MTHD_109310c3b16440e593a7c2177da29bb9 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_109310c3b16440e593a7c2177da29bb9()
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

      private sealed class MTHD_f933e61a98ce47908bdbf700acb013b4 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_f933e61a98ce47908bdbf700acb013b4()
        {
          this.Initialize("get_pivot_y", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((UIElement) obj).pivot_y;
        }
      }

      private sealed class MTHD_cd59f546513b40b3a2aa7586ab82a125 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_cd59f546513b40b3a2aa7586ab82a125()
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

      private sealed class MTHD_4dd1e7953b384b179c52d8f5e1f696cf : HardwiredMethodMemberDescriptor
      {
        internal MTHD_4dd1e7953b384b179c52d8f5e1f696cf()
        {
          this.Initialize("get_pivot", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((UIElement) obj).pivot;
        }
      }

      private sealed class MTHD_d7995e0d9a4b4875aa4e03d7646ab300 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_d7995e0d9a4b4875aa4e03d7646ab300()
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

      private sealed class MTHD_d5f6198664be400cb8b3ce93e65c4d42 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_d5f6198664be400cb8b3ce93e65c4d42()
        {
          this.Initialize("get_angle", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((UIElement) obj).angle;
        }
      }

      private sealed class MTHD_7ca82e52543d4ec98535489a745a4610 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_7ca82e52543d4ec98535489a745a4610()
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

      private sealed class MTHD_c19783dfb6d64b1eb735fda598b42e36 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_c19783dfb6d64b1eb735fda598b42e36()
        {
          this.Initialize("get_onClick", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((UIElement) obj).onClick;
        }
      }

      private sealed class MTHD_4182dcda150a403d971689f731362c51 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_4182dcda150a403d971689f731362c51()
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

      private sealed class MTHD_fae425c9f49049f6bdbd757c4150ad39 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_fae425c9f49049f6bdbd757c4150ad39()
        {
          this.Initialize("get_color", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((UIElement) obj).color;
        }
      }

      private sealed class MTHD_03421eafe8dc4262ab45151e6a022f15 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_03421eafe8dc4262ab45151e6a022f15()
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

      private sealed class MTHD_6db645627313428a95f0b582bed0ce47 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_6db645627313428a95f0b582bed0ce47()
        {
          this.Initialize("get_text", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((UIElement) obj).text;
        }
      }

      private sealed class MTHD_18c6877ae9be4272a96a98e77d8447ad : HardwiredMethodMemberDescriptor
      {
        internal MTHD_18c6877ae9be4272a96a98e77d8447ad()
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

      private sealed class MTHD_cf998ec1a01c40b090f66fc3c482b7a1 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_cf998ec1a01c40b090f66fc3c482b7a1()
        {
          this.Initialize("get_textColor", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((UIElement) obj).textColor;
        }
      }

      private sealed class MTHD_7045dfed517f4bf9bfc7205a8fd5730f : HardwiredMethodMemberDescriptor
      {
        internal MTHD_7045dfed517f4bf9bfc7205a8fd5730f()
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

      private sealed class MTHD_1868d016c3ee4f6698efd415b24b53ff : HardwiredMethodMemberDescriptor
      {
        internal MTHD_1868d016c3ee4f6698efd415b24b53ff()
        {
          this.Initialize("get_textStyle", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((UIElement) obj).textStyle;
        }
      }

      private sealed class MTHD_08e8cdc04c9143d6b3e84d9efd2b751d : HardwiredMethodMemberDescriptor
      {
        internal MTHD_08e8cdc04c9143d6b3e84d9efd2b751d()
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

      private sealed class MTHD_e9d0d2084ced4dc9b8333a6efd1fe4d0 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_e9d0d2084ced4dc9b8333a6efd1fe4d0()
        {
          this.Initialize("get_textSize", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((UIElement) obj).textSize;
        }
      }

      private sealed class MTHD_bc81a81f6d3c4539b2674bded9dbc40e : HardwiredMethodMemberDescriptor
      {
        internal MTHD_bc81a81f6d3c4539b2674bded9dbc40e()
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

      private sealed class MTHD_4b113d2a6cdd478ebc2a3101bad75826 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_4b113d2a6cdd478ebc2a3101bad75826()
        {
          this.Initialize("activateInput", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          ((UIElement) obj).activateInput();
          return (object) DynValue.Void;
        }
      }

      private sealed class MTHD_a3e5463e279d495fa9c0a3d6e7460907 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_a3e5463e279d495fa9c0a3d6e7460907()
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

      private sealed class MTHD_ee59e8b3f95b4c82b0efafd32f7109e3 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_ee59e8b3f95b4c82b0efafd32f7109e3()
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

      private sealed class MTHD_cae8b5ac800748dfbfa4c8ee0bc5514c : HardwiredMethodMemberDescriptor
      {
        internal MTHD_cae8b5ac800748dfbfa4c8ee0bc5514c()
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

      private sealed class MTHD_2cea8ad19ec74e8bbcbfbc94b0c4a5d7 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_2cea8ad19ec74e8bbcbfbc94b0c4a5d7()
        {
          this.Initialize("destroyHierarchy", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          ((UIElement) obj).destroyHierarchy();
          return (object) DynValue.Void;
        }
      }

      private sealed class MTHD_2d6e5de6bfdc4e718cb914d13ae68f32 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_2d6e5de6bfdc4e718cb914d13ae68f32()
        {
          this.Initialize("destroy", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          ((UIElement) obj).destroy();
          return (object) DynValue.Void;
        }
      }

      private sealed class MTHD_d4f2484f658b4cec8865ee04d25f8367 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_d4f2484f658b4cec8865ee04d25f8367()
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

      private sealed class MTHD_151154231b35469d986f50d508fd0f44 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_151154231b35469d986f50d508fd0f44()
        {
          this.Initialize("GetHashCode", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) obj.GetHashCode();
        }
      }

      private sealed class MTHD_3138ce06fd904751b5a7fc90d5cf1603 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_3138ce06fd904751b5a7fc90d5cf1603()
        {
          this.Initialize("GetType", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) obj.GetType();
        }
      }

      private sealed class MTHD_4a3ece720c4f4fdabe18f5d72aec55f1 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_4a3ece720c4f4fdabe18f5d72aec55f1()
        {
          this.Initialize("ToString", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) obj.ToString();
        }
      }

      private sealed class PROP_1f3003154c824452ad6e700c02749793 : HardwiredMemberDescriptor
      {
        internal PROP_1f3003154c824452ad6e700c02749793()
          : base(typeof (bool), "isDead", false, MemberDescriptorAccess.CanRead)
        {
        }

        protected override object GetValueImpl(Script script, object obj)
        {
          return (object) ((UIElement) obj).isDead;
        }
      }

      private sealed class PROP_ec1b4bc10143443db600690c300194b3 : HardwiredMemberDescriptor
      {
        internal PROP_ec1b4bc10143443db600690c300194b3()
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

      private sealed class PROP_6b7f1b71f4744062a3ca5c6a72cb04b3 : HardwiredMemberDescriptor
      {
        internal PROP_6b7f1b71f4744062a3ca5c6a72cb04b3()
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

      private sealed class PROP_e257e30f588c4205a84d9b24a515ce01 : HardwiredMemberDescriptor
      {
        internal PROP_e257e30f588c4205a84d9b24a515ce01()
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

      private sealed class PROP_9f185cc1ae944bd1817b8c48cf0c62fa : HardwiredMemberDescriptor
      {
        internal PROP_9f185cc1ae944bd1817b8c48cf0c62fa()
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

      private sealed class PROP_7ec6beeeb7544b1e9058c8f9956ca7d6 : HardwiredMemberDescriptor
      {
        internal PROP_7ec6beeeb7544b1e9058c8f9956ca7d6()
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

      private sealed class PROP_e1587c0182c8494696e1a8e6dd353667 : HardwiredMemberDescriptor
      {
        internal PROP_e1587c0182c8494696e1a8e6dd353667()
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

      private sealed class PROP_8fadcb831e544081896d9d3ff6604f7a : HardwiredMemberDescriptor
      {
        internal PROP_8fadcb831e544081896d9d3ff6604f7a()
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

      private sealed class PROP_5263ceaf9e314f76ab673bb2c8f36436 : HardwiredMemberDescriptor
      {
        internal PROP_5263ceaf9e314f76ab673bb2c8f36436()
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

      private sealed class PROP_e8e06f59caef4e71a91ba5da9f0cce88 : HardwiredMemberDescriptor
      {
        internal PROP_e8e06f59caef4e71a91ba5da9f0cce88()
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

      private sealed class PROP_7f218be6b7524664b3e444220618cefa : HardwiredMemberDescriptor
      {
        internal PROP_7f218be6b7524664b3e444220618cefa()
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

      private sealed class PROP_c1a95a8cfe154368be03bc02b2f1f754 : HardwiredMemberDescriptor
      {
        internal PROP_c1a95a8cfe154368be03bc02b2f1f754()
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

      private sealed class PROP_70eda7c599534f4e833443e9c389af43 : HardwiredMemberDescriptor
      {
        internal PROP_70eda7c599534f4e833443e9c389af43()
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

      private sealed class PROP_6333fb03256d4f0bbe3d120fc7d4bdb3 : HardwiredMemberDescriptor
      {
        internal PROP_6333fb03256d4f0bbe3d120fc7d4bdb3()
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

      private sealed class PROP_5dc3b5d156b847bfb05f84a10e0bdfdb : HardwiredMemberDescriptor
      {
        internal PROP_5dc3b5d156b847bfb05f84a10e0bdfdb()
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

      private sealed class PROP_d8493271556a475b9e6186d2f0f9472f : HardwiredMemberDescriptor
      {
        internal PROP_d8493271556a475b9e6186d2f0f9472f()
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

      private sealed class PROP_ee7367ade38d44408ea9035be0f606cd : HardwiredMemberDescriptor
      {
        internal PROP_ee7367ade38d44408ea9035be0f606cd()
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

      private sealed class PROP_ed611977568f48779299698619516b4b : HardwiredMemberDescriptor
      {
        internal PROP_ed611977568f48779299698619516b4b()
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

      private sealed class PROP_4feb7268d0194a59b39eda105a800480 : HardwiredMemberDescriptor
      {
        internal PROP_4feb7268d0194a59b39eda105a800480()
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

      private sealed class FLDV_eeb07d8a21fd43aaa868c0df6084387d : HardwiredMemberDescriptor
      {
        internal FLDV_eeb07d8a21fd43aaa868c0df6084387d()
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

      private sealed class FLDV_e424c3f20bad4ce8898cd5df45553f45 : HardwiredMemberDescriptor
      {
        internal FLDV_e424c3f20bad4ce8898cd5df45553f45()
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

      private sealed class FLDV_01be07f5cc59431791acbcee573a8861 : HardwiredMemberDescriptor
      {
        internal FLDV_01be07f5cc59431791acbcee573a8861()
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

    private sealed class TYPE_0cfafa9d829143359759f8bd8378a201 : HardwiredUserDataDescriptor
    {
      internal TYPE_0cfafa9d829143359759f8bd8378a201()
        : base(typeof (MyPoll.Item))
      {
        this.AddMember("__new", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("__new", typeof (MyPoll.Item), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_0cfafa9d829143359759f8bd8378a201.MTHD_987f09a95a3f43568c90b8ba19969a79()
        }));
        this.AddMember("addAnswer", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("addAnswer", typeof (MyPoll.Item), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_0cfafa9d829143359759f8bd8378a201.MTHD_42d7f88e8bda48fe9867042654feff56()
        }));
        this.AddMember("Equals", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("Equals", typeof (MyPoll.Item), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_0cfafa9d829143359759f8bd8378a201.MTHD_e9d4c73e861c461f9f0cab734059396a()
        }));
        this.AddMember("GetHashCode", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("GetHashCode", typeof (MyPoll.Item), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_0cfafa9d829143359759f8bd8378a201.MTHD_6fdd9d842b1d4c2ca1d700ccc8524701()
        }));
        this.AddMember("GetType", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("GetType", typeof (MyPoll.Item), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_0cfafa9d829143359759f8bd8378a201.MTHD_d8a837f48a914ec4bd2dc5261eb29cfb()
        }));
        this.AddMember("ToString", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("ToString", typeof (MyPoll.Item), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_0cfafa9d829143359759f8bd8378a201.MTHD_4dd44309563d4d7a8e765abe483c279c()
        }));
        this.AddMember("question", (IMemberDescriptor) new Bridge.TYPE_0cfafa9d829143359759f8bd8378a201.FLDV_0665b4be77e345db8628c91a582cdd0b());
        this.AddMember("multipleSelection", (IMemberDescriptor) new Bridge.TYPE_0cfafa9d829143359759f8bd8378a201.FLDV_d8e0594e33f345d6a789d888f489c0cb());
        this.AddMember("requiresAnswer", (IMemberDescriptor) new Bridge.TYPE_0cfafa9d829143359759f8bd8378a201.FLDV_e133d5ae213d44e3880301e5c70ceb08());
        this.AddMember("answers", (IMemberDescriptor) new Bridge.TYPE_0cfafa9d829143359759f8bd8378a201.FLDV_9e5a1369e9ab4358be6cae9a5688cca0());
      }

      private sealed class MTHD_987f09a95a3f43568c90b8ba19969a79 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_987f09a95a3f43568c90b8ba19969a79()
        {
          this.Initialize(".ctor", true, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) new MyPoll.Item();
        }
      }

      private sealed class MTHD_42d7f88e8bda48fe9867042654feff56 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_42d7f88e8bda48fe9867042654feff56()
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

      private sealed class MTHD_e9d4c73e861c461f9f0cab734059396a : HardwiredMethodMemberDescriptor
      {
        internal MTHD_e9d4c73e861c461f9f0cab734059396a()
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

      private sealed class MTHD_6fdd9d842b1d4c2ca1d700ccc8524701 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_6fdd9d842b1d4c2ca1d700ccc8524701()
        {
          this.Initialize("GetHashCode", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) obj.GetHashCode();
        }
      }

      private sealed class MTHD_d8a837f48a914ec4bd2dc5261eb29cfb : HardwiredMethodMemberDescriptor
      {
        internal MTHD_d8a837f48a914ec4bd2dc5261eb29cfb()
        {
          this.Initialize("GetType", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) obj.GetType();
        }
      }

      private sealed class MTHD_4dd44309563d4d7a8e765abe483c279c : HardwiredMethodMemberDescriptor
      {
        internal MTHD_4dd44309563d4d7a8e765abe483c279c()
        {
          this.Initialize("ToString", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) obj.ToString();
        }
      }

      private sealed class FLDV_0665b4be77e345db8628c91a582cdd0b : HardwiredMemberDescriptor
      {
        internal FLDV_0665b4be77e345db8628c91a582cdd0b()
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

      private sealed class FLDV_d8e0594e33f345d6a789d888f489c0cb : HardwiredMemberDescriptor
      {
        internal FLDV_d8e0594e33f345d6a789d888f489c0cb()
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

      private sealed class FLDV_e133d5ae213d44e3880301e5c70ceb08 : HardwiredMemberDescriptor
      {
        internal FLDV_e133d5ae213d44e3880301e5c70ceb08()
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

      private sealed class FLDV_9e5a1369e9ab4358be6cae9a5688cca0 : HardwiredMemberDescriptor
      {
        internal FLDV_9e5a1369e9ab4358be6cae9a5688cca0()
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

    private sealed class TYPE_7268ac70a9894056b228131cf98b9520 : HardwiredUserDataDescriptor
    {
      internal TYPE_7268ac70a9894056b228131cf98b9520()
        : base(typeof (MyPoll.Answer))
      {
        this.AddMember("__new", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("__new", typeof (MyPoll.Answer), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_7268ac70a9894056b228131cf98b9520.MTHD_70809d9e5e93472c87efaa15f934537b()
        }));
        this.AddMember("Equals", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("Equals", typeof (MyPoll.Answer), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_7268ac70a9894056b228131cf98b9520.MTHD_9847e39a25f640d68ffdeef1111c738b()
        }));
        this.AddMember("GetHashCode", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("GetHashCode", typeof (MyPoll.Answer), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_7268ac70a9894056b228131cf98b9520.MTHD_c11c261fb5944ac59422df088b3c2f4b()
        }));
        this.AddMember("GetType", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("GetType", typeof (MyPoll.Answer), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_7268ac70a9894056b228131cf98b9520.MTHD_6c3729006b984b9fae8e2553d66da015()
        }));
        this.AddMember("ToString", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("ToString", typeof (MyPoll.Answer), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_7268ac70a9894056b228131cf98b9520.MTHD_bf273371383c44af88c65462b774c8c5()
        }));
        this.AddMember("text", (IMemberDescriptor) new Bridge.TYPE_7268ac70a9894056b228131cf98b9520.FLDV_4753dc129171438d9021587bc8f755e6());
        this.AddMember("allowUserInput", (IMemberDescriptor) new Bridge.TYPE_7268ac70a9894056b228131cf98b9520.FLDV_c5b2f30b11254df996e374f349cc9400());
        this.AddMember("checkable", (IMemberDescriptor) new Bridge.TYPE_7268ac70a9894056b228131cf98b9520.FLDV_f8a950a3b5544b1aa823e0d7a141b760());
        this.AddMember("userInput", (IMemberDescriptor) new Bridge.TYPE_7268ac70a9894056b228131cf98b9520.FLDV_aa08181cecd54cf8a278091cc8d74186());
        this.AddMember("isChecked", (IMemberDescriptor) new Bridge.TYPE_7268ac70a9894056b228131cf98b9520.FLDV_8a3b49db28464f31bec852dcaacb92ed());
      }

      private sealed class MTHD_70809d9e5e93472c87efaa15f934537b : HardwiredMethodMemberDescriptor
      {
        internal MTHD_70809d9e5e93472c87efaa15f934537b()
        {
          this.Initialize(".ctor", true, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) new MyPoll.Answer();
        }
      }

      private sealed class MTHD_9847e39a25f640d68ffdeef1111c738b : HardwiredMethodMemberDescriptor
      {
        internal MTHD_9847e39a25f640d68ffdeef1111c738b()
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

      private sealed class MTHD_c11c261fb5944ac59422df088b3c2f4b : HardwiredMethodMemberDescriptor
      {
        internal MTHD_c11c261fb5944ac59422df088b3c2f4b()
        {
          this.Initialize("GetHashCode", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) obj.GetHashCode();
        }
      }

      private sealed class MTHD_6c3729006b984b9fae8e2553d66da015 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_6c3729006b984b9fae8e2553d66da015()
        {
          this.Initialize("GetType", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) obj.GetType();
        }
      }

      private sealed class MTHD_bf273371383c44af88c65462b774c8c5 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_bf273371383c44af88c65462b774c8c5()
        {
          this.Initialize("ToString", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) obj.ToString();
        }
      }

      private sealed class FLDV_4753dc129171438d9021587bc8f755e6 : HardwiredMemberDescriptor
      {
        internal FLDV_4753dc129171438d9021587bc8f755e6()
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

      private sealed class FLDV_c5b2f30b11254df996e374f349cc9400 : HardwiredMemberDescriptor
      {
        internal FLDV_c5b2f30b11254df996e374f349cc9400()
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

      private sealed class FLDV_f8a950a3b5544b1aa823e0d7a141b760 : HardwiredMemberDescriptor
      {
        internal FLDV_f8a950a3b5544b1aa823e0d7a141b760()
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

      private sealed class FLDV_aa08181cecd54cf8a278091cc8d74186 : HardwiredMemberDescriptor
      {
        internal FLDV_aa08181cecd54cf8a278091cc8d74186()
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

      private sealed class FLDV_8a3b49db28464f31bec852dcaacb92ed : HardwiredMemberDescriptor
      {
        internal FLDV_8a3b49db28464f31bec852dcaacb92ed()
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

    private sealed class TYPE_8d99cb16d56c42c1bd10716ce12bcbc1 : HardwiredUserDataDescriptor
    {
      internal TYPE_8d99cb16d56c42c1bd10716ce12bcbc1()
        : base(typeof (MyPoll))
      {
        this.AddMember("__new", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("__new", typeof (MyPoll), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_8d99cb16d56c42c1bd10716ce12bcbc1.MTHD_9bd9616037ef447e93beeacc9a050d9a()
        }));
        this.AddMember("GetQuestionsAsString", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("GetQuestionsAsString", typeof (MyPoll), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_8d99cb16d56c42c1bd10716ce12bcbc1.MTHD_3de43af8ff56485c834d42b5a13a79b4()
        }));
        this.AddMember("SanitizeResponses", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("SanitizeResponses", typeof (MyPoll), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_8d99cb16d56c42c1bd10716ce12bcbc1.MTHD_70e5a5b3856d444e8588249be350e561()
        }));
        this.AddMember("addItem", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("addItem", typeof (MyPoll), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_8d99cb16d56c42c1bd10716ce12bcbc1.MTHD_012388a0bd4a46eaa30f93543465d3c4()
        }));
        this.AddMember("construct", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("construct", typeof (MyPoll), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_8d99cb16d56c42c1bd10716ce12bcbc1.MTHD_460b17dac44e4c37bb3d718eb8a61c69()
        }));
        this.AddMember("ToString", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("ToString", typeof (MyPoll), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_8d99cb16d56c42c1bd10716ce12bcbc1.MTHD_e6c03d5771cc421b8c428f8e25daabe3()
        }));
        this.AddMember("fromString", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("fromString", typeof (MyPoll), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_8d99cb16d56c42c1bd10716ce12bcbc1.MTHD_abd9e9c4ee8e4548b6c7e44061fbeebd()
        }));
        this.AddMember("sendToServer", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("sendToServer", typeof (MyPoll), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_8d99cb16d56c42c1bd10716ce12bcbc1.MTHD_f81039ea41cc45ecb6b15343c6878158()
        }));
        this.AddMember("closePoll", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("closePoll", typeof (MyPoll), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_8d99cb16d56c42c1bd10716ce12bcbc1.MTHD_036ccb6ee5d0451f972fca58eb3c0c51()
        }));
        this.AddMember("test", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("test", typeof (MyPoll), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_8d99cb16d56c42c1bd10716ce12bcbc1.MTHD_1d0aa64284854a529848529e5036e519()
        }));
        this.AddMember("Equals", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("Equals", typeof (MyPoll), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_8d99cb16d56c42c1bd10716ce12bcbc1.MTHD_a0def91dc8b544f7a686572e6d91a3ce()
        }));
        this.AddMember("GetHashCode", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("GetHashCode", typeof (MyPoll), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_8d99cb16d56c42c1bd10716ce12bcbc1.MTHD_240a203bc92740f3bc1c60d842bdf702()
        }));
        this.AddMember("GetType", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("GetType", typeof (MyPoll), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_8d99cb16d56c42c1bd10716ce12bcbc1.MTHD_0f8a584f54e14acf98f5e0d2318e7518()
        }));
        this.AddMember("name", (IMemberDescriptor) new Bridge.TYPE_8d99cb16d56c42c1bd10716ce12bcbc1.FLDV_864d19f481214db0979f3255e239f0bd());
        this.AddMember("id", (IMemberDescriptor) new Bridge.TYPE_8d99cb16d56c42c1bd10716ce12bcbc1.FLDV_c4e7b733a4eb4664818dd0668ea24eeb());
        this.AddMember("items", (IMemberDescriptor) new Bridge.TYPE_8d99cb16d56c42c1bd10716ce12bcbc1.FLDV_1caada82078f411ab0109b9fac769989());
        this.AddMember("isPublic", (IMemberDescriptor) new Bridge.TYPE_8d99cb16d56c42c1bd10716ce12bcbc1.FLDV_5282705d9e4e4a2c91d87e08ffa9e03b());
        this.AddMember("MsgRetrieve", (IMemberDescriptor) new Bridge.TYPE_8d99cb16d56c42c1bd10716ce12bcbc1.FLDV_319629bead024e65a805fca5a9e2b7fc());
        this.AddMember("MsgSubmit", (IMemberDescriptor) new Bridge.TYPE_8d99cb16d56c42c1bd10716ce12bcbc1.FLDV_8b4bebf9e92e487f9e664675da73f35f());
        this.AddMember("MsgCreate", (IMemberDescriptor) new Bridge.TYPE_8d99cb16d56c42c1bd10716ce12bcbc1.FLDV_815b5ad22d904472a5af66f0109ce071());
        this.AddMember("MsgResults", (IMemberDescriptor) new Bridge.TYPE_8d99cb16d56c42c1bd10716ce12bcbc1.FLDV_5ff67c85e04c4459af18d8eac8c2de10());
        this.AddMember("Item", (IMemberDescriptor) new Bridge.TYPE_8d99cb16d56c42c1bd10716ce12bcbc1.DVAL_b51e7dd77848465984621c6870ac8b0c());
        this.AddMember("Answer", (IMemberDescriptor) new Bridge.TYPE_8d99cb16d56c42c1bd10716ce12bcbc1.DVAL_3cae75edd3ed47c1962cec9e53343bcd());
      }

      private sealed class MTHD_9bd9616037ef447e93beeacc9a050d9a : HardwiredMethodMemberDescriptor
      {
        internal MTHD_9bd9616037ef447e93beeacc9a050d9a()
        {
          this.Initialize(".ctor", true, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) new MyPoll();
        }
      }

      private sealed class MTHD_3de43af8ff56485c834d42b5a13a79b4 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_3de43af8ff56485c834d42b5a13a79b4()
        {
          this.Initialize("GetQuestionsAsString", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((MyPoll) obj).GetQuestionsAsString();
        }
      }

      private sealed class MTHD_70e5a5b3856d444e8588249be350e561 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_70e5a5b3856d444e8588249be350e561()
        {
          this.Initialize("SanitizeResponses", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          ((MyPoll) obj).SanitizeResponses();
          return (object) DynValue.Void;
        }
      }

      private sealed class MTHD_012388a0bd4a46eaa30f93543465d3c4 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_012388a0bd4a46eaa30f93543465d3c4()
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

      private sealed class MTHD_460b17dac44e4c37bb3d718eb8a61c69 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_460b17dac44e4c37bb3d718eb8a61c69()
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

      private sealed class MTHD_e6c03d5771cc421b8c428f8e25daabe3 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_e6c03d5771cc421b8c428f8e25daabe3()
        {
          this.Initialize("ToString", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((MyPoll) obj).ToString();
        }
      }

      private sealed class MTHD_abd9e9c4ee8e4548b6c7e44061fbeebd : HardwiredMethodMemberDescriptor
      {
        internal MTHD_abd9e9c4ee8e4548b6c7e44061fbeebd()
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

      private sealed class MTHD_f81039ea41cc45ecb6b15343c6878158 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_f81039ea41cc45ecb6b15343c6878158()
        {
          this.Initialize("sendToServer", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          ((MyPoll) obj).sendToServer();
          return (object) DynValue.Void;
        }
      }

      private sealed class MTHD_036ccb6ee5d0451f972fca58eb3c0c51 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_036ccb6ee5d0451f972fca58eb3c0c51()
        {
          this.Initialize("closePoll", true, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          MyPoll.closePoll();
          return (object) DynValue.Void;
        }
      }

      private sealed class MTHD_1d0aa64284854a529848529e5036e519 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_1d0aa64284854a529848529e5036e519()
        {
          this.Initialize("test", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          ((MyPoll) obj).test();
          return (object) DynValue.Void;
        }
      }

      private sealed class MTHD_a0def91dc8b544f7a686572e6d91a3ce : HardwiredMethodMemberDescriptor
      {
        internal MTHD_a0def91dc8b544f7a686572e6d91a3ce()
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

      private sealed class MTHD_240a203bc92740f3bc1c60d842bdf702 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_240a203bc92740f3bc1c60d842bdf702()
        {
          this.Initialize("GetHashCode", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) obj.GetHashCode();
        }
      }

      private sealed class MTHD_0f8a584f54e14acf98f5e0d2318e7518 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_0f8a584f54e14acf98f5e0d2318e7518()
        {
          this.Initialize("GetType", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) obj.GetType();
        }
      }

      private sealed class FLDV_864d19f481214db0979f3255e239f0bd : HardwiredMemberDescriptor
      {
        internal FLDV_864d19f481214db0979f3255e239f0bd()
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

      private sealed class FLDV_c4e7b733a4eb4664818dd0668ea24eeb : HardwiredMemberDescriptor
      {
        internal FLDV_c4e7b733a4eb4664818dd0668ea24eeb()
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

      private sealed class FLDV_1caada82078f411ab0109b9fac769989 : HardwiredMemberDescriptor
      {
        internal FLDV_1caada82078f411ab0109b9fac769989()
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

      private sealed class FLDV_5282705d9e4e4a2c91d87e08ffa9e03b : HardwiredMemberDescriptor
      {
        internal FLDV_5282705d9e4e4a2c91d87e08ffa9e03b()
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

      private sealed class FLDV_319629bead024e65a805fca5a9e2b7fc : HardwiredMemberDescriptor
      {
        internal FLDV_319629bead024e65a805fca5a9e2b7fc()
          : base(typeof (byte), "MsgRetrieve", true, MemberDescriptorAccess.CanRead)
        {
        }

        protected override object GetValueImpl(Script script, object obj) => (object) (byte) 1;
      }

      private sealed class FLDV_8b4bebf9e92e487f9e664675da73f35f : HardwiredMemberDescriptor
      {
        internal FLDV_8b4bebf9e92e487f9e664675da73f35f()
          : base(typeof (byte), "MsgSubmit", true, MemberDescriptorAccess.CanRead)
        {
        }

        protected override object GetValueImpl(Script script, object obj) => (object) (byte) 2;
      }

      private sealed class FLDV_815b5ad22d904472a5af66f0109ce071 : HardwiredMemberDescriptor
      {
        internal FLDV_815b5ad22d904472a5af66f0109ce071()
          : base(typeof (byte), "MsgCreate", true, MemberDescriptorAccess.CanRead)
        {
        }

        protected override object GetValueImpl(Script script, object obj) => (object) (byte) 3;
      }

      private sealed class FLDV_5ff67c85e04c4459af18d8eac8c2de10 : HardwiredMemberDescriptor
      {
        internal FLDV_5ff67c85e04c4459af18d8eac8c2de10()
          : base(typeof (byte), "MsgResults", true, MemberDescriptorAccess.CanRead)
        {
        }

        protected override object GetValueImpl(Script script, object obj) => (object) (byte) 4;
      }

      private sealed class DVAL_b51e7dd77848465984621c6870ac8b0c : DynValueMemberDescriptor
      {
        internal DVAL_b51e7dd77848465984621c6870ac8b0c()
          : base("Item")
        {
        }

        public override DynValue Value => UserData.CreateStatic(typeof (MyPoll.Item));
      }

      private sealed class DVAL_3cae75edd3ed47c1962cec9e53343bcd : DynValueMemberDescriptor
      {
        internal DVAL_3cae75edd3ed47c1962cec9e53343bcd()
          : base("Answer")
        {
        }

        public override DynValue Value => UserData.CreateStatic(typeof (MyPoll.Answer));
      }
    }

    private sealed class TYPE_cc81db70c47a48568088483aa2925801 : HardwiredUserDataDescriptor
    {
      internal TYPE_cc81db70c47a48568088483aa2925801()
        : base(typeof (LuaColor))
      {
        this.AddMember("__new", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("__new", typeof (LuaColor), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_cc81db70c47a48568088483aa2925801.MTHD_7dc4457f8872424a91f18fb3b0f4f735()
        }));
        this.AddMember("toHex", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("toHex", typeof (LuaColor), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_cc81db70c47a48568088483aa2925801.MTHD_eaf3758966824b5f95a7d1f6c9672b76()
        }));
        this.AddMember("construct", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("construct", typeof (LuaColor), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[2]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_cc81db70c47a48568088483aa2925801.MTHD_515e87a492e84086b2a1455360d21445(),
          (IOverloadableMemberDescriptor) new Bridge.TYPE_cc81db70c47a48568088483aa2925801.MTHD_adeb8c905daf4dbf801dea90217a4085()
        }));
        this.AddMember("From", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("From", typeof (LuaColor), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[2]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_cc81db70c47a48568088483aa2925801.MTHD_8a53bb3292bb4640b91c9dd1dd0baa1c(),
          (IOverloadableMemberDescriptor) new Bridge.TYPE_cc81db70c47a48568088483aa2925801.MTHD_2614e613e5224fe0a5f724adaaf50b4d()
        }));
        this.AddMember("Equals", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("Equals", typeof (LuaColor), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_cc81db70c47a48568088483aa2925801.MTHD_89b86211c6ae497ea566368f241e5eee()
        }));
        this.AddMember("GetHashCode", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("GetHashCode", typeof (LuaColor), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_cc81db70c47a48568088483aa2925801.MTHD_a3e15e9ccfc440a09b0d3177744b092c()
        }));
        this.AddMember("GetType", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("GetType", typeof (LuaColor), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_cc81db70c47a48568088483aa2925801.MTHD_3c5238e4d8434197a7e77b5c97b586c6()
        }));
        this.AddMember("ToString", (IMemberDescriptor) new OverloadedMethodMemberDescriptor("ToString", typeof (LuaColor), (IEnumerable<IOverloadableMemberDescriptor>) new IOverloadableMemberDescriptor[1]
        {
          (IOverloadableMemberDescriptor) new Bridge.TYPE_cc81db70c47a48568088483aa2925801.MTHD_d3014085a3a04f3396e0db79dcf7ce5b()
        }));
        this.AddMember("r", (IMemberDescriptor) new Bridge.TYPE_cc81db70c47a48568088483aa2925801.FLDV_daba1e996a9d418ca86892ac5b27ae84());
        this.AddMember("g", (IMemberDescriptor) new Bridge.TYPE_cc81db70c47a48568088483aa2925801.FLDV_eed4fc2f79b84c51862d4eb579a92c5e());
        this.AddMember("b", (IMemberDescriptor) new Bridge.TYPE_cc81db70c47a48568088483aa2925801.FLDV_5388dc6b60a941c2963bd119461cbc12());
        this.AddMember("a", (IMemberDescriptor) new Bridge.TYPE_cc81db70c47a48568088483aa2925801.FLDV_d94d8399764a4fc19bc90fa733ef92e6());
      }

      private sealed class MTHD_7dc4457f8872424a91f18fb3b0f4f735 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_7dc4457f8872424a91f18fb3b0f4f735()
        {
          this.Initialize(".ctor", true, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) new LuaColor();
        }
      }

      private sealed class MTHD_eaf3758966824b5f95a7d1f6c9672b76 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_eaf3758966824b5f95a7d1f6c9672b76()
        {
          this.Initialize("toHex", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) ((LuaColor) obj).toHex();
        }
      }

      private sealed class MTHD_515e87a492e84086b2a1455360d21445 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_515e87a492e84086b2a1455360d21445()
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

      private sealed class MTHD_adeb8c905daf4dbf801dea90217a4085 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_adeb8c905daf4dbf801dea90217a4085()
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

      private sealed class MTHD_8a53bb3292bb4640b91c9dd1dd0baa1c : HardwiredMethodMemberDescriptor
      {
        internal MTHD_8a53bb3292bb4640b91c9dd1dd0baa1c()
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

      private sealed class MTHD_2614e613e5224fe0a5f724adaaf50b4d : HardwiredMethodMemberDescriptor
      {
        internal MTHD_2614e613e5224fe0a5f724adaaf50b4d()
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

      private sealed class MTHD_89b86211c6ae497ea566368f241e5eee : HardwiredMethodMemberDescriptor
      {
        internal MTHD_89b86211c6ae497ea566368f241e5eee()
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

      private sealed class MTHD_a3e15e9ccfc440a09b0d3177744b092c : HardwiredMethodMemberDescriptor
      {
        internal MTHD_a3e15e9ccfc440a09b0d3177744b092c()
        {
          this.Initialize("GetHashCode", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) obj.GetHashCode();
        }
      }

      private sealed class MTHD_3c5238e4d8434197a7e77b5c97b586c6 : HardwiredMethodMemberDescriptor
      {
        internal MTHD_3c5238e4d8434197a7e77b5c97b586c6()
        {
          this.Initialize("GetType", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) obj.GetType();
        }
      }

      private sealed class MTHD_d3014085a3a04f3396e0db79dcf7ce5b : HardwiredMethodMemberDescriptor
      {
        internal MTHD_d3014085a3a04f3396e0db79dcf7ce5b()
        {
          this.Initialize("ToString", false, new ParameterDescriptor[0], false);
        }

        protected override object Invoke(Script script, object obj, object[] pars, int argscount)
        {
          return (object) obj.ToString();
        }
      }

      private sealed class FLDV_daba1e996a9d418ca86892ac5b27ae84 : HardwiredMemberDescriptor
      {
        internal FLDV_daba1e996a9d418ca86892ac5b27ae84()
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

      private sealed class FLDV_eed4fc2f79b84c51862d4eb579a92c5e : HardwiredMemberDescriptor
      {
        internal FLDV_eed4fc2f79b84c51862d4eb579a92c5e()
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

      private sealed class FLDV_5388dc6b60a941c2963bd119461cbc12 : HardwiredMemberDescriptor
      {
        internal FLDV_5388dc6b60a941c2963bd119461cbc12()
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

      private sealed class FLDV_d94d8399764a4fc19bc90fa733ef92e6 : HardwiredMemberDescriptor
      {
        internal FLDV_d94d8399764a4fc19bc90fa733ef92e6()
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

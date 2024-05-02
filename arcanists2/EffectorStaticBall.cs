

#nullable disable
public class EffectorStaticBall : Effector
{
  private ZEffectorStaticBall daspell;

  private void Start() => this.daspell = (ZEffectorStaticBall) this.serverObj;

  private void Update() => this.daspell.Update();
}

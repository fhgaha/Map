internal class MoveToSelected : ISoldierState
{
    private Soldier soldier;
    private SoldierFollower follower;
    private int targetId;

    public MoveToSelected(Soldier soldier, SoldierFollower follower, int targetId)
    {
        this.soldier = soldier;
        this.follower = follower;
        this.targetId = targetId;
    }

    public void OnEnter()
    {
        follower.GoToTarget(targetId);
        follower.coroutineAllowed = true;
    }

    public void OnExit()
    {
        follower.coroutineAllowed = false;
    }

    public void Tick()
    {
        soldier.CheckIfItsTimeToDie();
    }


}
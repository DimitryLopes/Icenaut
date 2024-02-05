public abstract class BossAction
{
    protected Boss boss;
    protected bool isActionFinished;

    public BossAction(Boss boss)
    {
        this.boss = boss;
    }

    public abstract void ExecuteAction();
    public abstract bool IsFinished();

    public abstract void OnActionFinished();
}

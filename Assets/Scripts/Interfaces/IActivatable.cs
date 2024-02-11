public interface IActivatable
{
    public bool IsActive { get; set; }

    public void Activate();

    public void Deactivate();
}

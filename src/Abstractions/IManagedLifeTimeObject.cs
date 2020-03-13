namespace FragmentManager.Abstractions
{
  public interface IManagedLifeTimeObject : IInternalManagedObject
  {
    void OnPause();

    void OnResume();
  }
}

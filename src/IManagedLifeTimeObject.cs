namespace FragmentManager
{
  public interface IManagedLifeTimeObject : IInternalManagedObject
  {
    void OnPause();

    void OnResume();
  }
}

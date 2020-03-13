namespace FragmentManager.Abstractions
{
  public interface IInternalManagedObject
  {
    void OnCreated();

    void OnDestroy();
  }
}

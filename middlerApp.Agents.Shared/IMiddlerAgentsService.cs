namespace middlerApp.Agents.Shared
{
    public interface IMiddlerAgentsService
    {
        T GetInterface<T>() where T: class;
    }
}
namespace NewBookingApp.Core.Data
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
}
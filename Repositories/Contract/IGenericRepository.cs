namespace AppCrud.Repositories.Contract
{
    public interface IGenericRepository<T> where T: class
    {
        Task<List<T>> List();
        Task<bool> save(T modelo);
        Task<bool> Edit(T modelo);
        Task<bool> Delete(int id);
    }
}

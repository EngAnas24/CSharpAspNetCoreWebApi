namespace MyFirstApiProject.Data
{
    public interface IDatahelper<T> where T : class
    {
        public List<T> GetAll();
        public T Get(int id);
        public T Find(int id);
        public void Delete(int id);
        public void Add(T entity);
        public void Update(T entity, int id);
    }
}

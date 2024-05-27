namespace DefaultNamespace.Abstract_classes
{
    public interface IDataReturner
    {
        public void SetCurrent(int index);
        public int GetCurrent();
        public int GetMaxCount();
    }
}
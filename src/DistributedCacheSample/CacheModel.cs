public class CacheModel
{
    public CacheModel()
    {
        MyProperty = Enumerable.Range(0, 10).Select(s => new TestModel()).ToList();
    }
    public List<TestModel> MyProperty { get; set; }
}

namespace SpaceBattle.Lib.Test;


public class AdapterBuilderTests
{
    [Fact]
    public void SuccessfulAdapterBuilderBuild()
    {
        var adaptableType = typeof(IUObject);
        var adaptiveType = typeof(IMovable);

        var adapterBuilder = new AdapterBuilder(adaptableType, adaptiveType);
        adaptiveType.GetProperties().ToList().ForEach(property => adapterBuilder.AddProperty(property));

        var result = adapterBuilder.Build();

        var adapter = @"public class IMovableAdapter : IMovable
    {
        private IUObject obj;

        public IMovableAdapter(IUObject obj) => this.obj = obj;

        public Vector Speed
        {
            get => IoC.Resolve<Vector>(""GetSpeed"", obj);
            
        }

        public Vector Pos
        {
            get => IoC.Resolve<Vector>(""GetPos"", obj);
            set => IoC.Resolve<ICommand>(""SetPos"", obj, value).Execute();
        }
    }";

        Assert.Equal(adapter, result);
    }
}

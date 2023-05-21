using System.Reflection;
using Hwdtech;
using Scriban;

namespace SpaceBattle.Lib;


public class AdapterBuilder : IBuilder
{
    private IList<PropertyInfo> propertyInfo = new List<PropertyInfo>();
    private Type adaptableType;
    private Type adaptiveType;
    private Template template = Template.Parse(@"public class {{a}}Adapter : {{a}}
    {
        private {{b}} obj;

        public {{a}}Adapter({{b}} obj) => this.obj = obj;

        {{- for propInfo in (c)}}

        public {{propInfo.property_type.name}} {{propInfo.name}}
        {
            {{if propInfo.get_method != null}}get => IoC.Resolve<{{propInfo.property_type.name}}>(""Get{{propInfo.name}}"", obj);{{ end }}
            {{if propInfo.set_method != null}}set => IoC.Resolve<ICommand>(""Set{{propInfo.name}}"", obj, value).Execute();{{ end }}
        }{{ end }}
    }");

    public AdapterBuilder(Type adaptableType, Type adaptiveType)
    {
        this.adaptableType = adaptableType;
        this.adaptiveType = adaptiveType;
    }

    public void AddProperty(object property)
    {
        this.propertyInfo.Add((PropertyInfo)property);
    }

    public String Build()
    {
        return template.Render(new { a = this.adaptiveType.Name, b = this.adaptableType.Name, c = this.propertyInfo});
    }
}

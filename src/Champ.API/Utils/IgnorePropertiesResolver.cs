using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Champ.API.Utils;

public class IgnorePropertiesResolver : DefaultContractResolver {
    private readonly IEnumerable<string> _ignoredPropertyNames;

    public IgnorePropertiesResolver(IEnumerable<string> ignoredPropertyNames) {
        _ignoredPropertyNames = ignoredPropertyNames;
    }

    protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization) {
        var property = base.CreateProperty(member, memberSerialization);
        if (_ignoredPropertyNames.Contains(property.PropertyName)) {
            property.ShouldSerialize = _ => false;
        }

        return property;
    }
}

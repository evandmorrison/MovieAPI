using System.Reflection;

public static class DbModelTransformer
{
    public static TTarget ToModel<TTarget, TSource>(this TSource modelToTransform) where TTarget : new()
    {
        TTarget newModel = new TTarget();

        IList<PropertyInfo> oldProperties = typeof(TSource).GetProperties().ToList();

        foreach (var property in oldProperties)
        {
            var newTypeProperty = typeof(TTarget).GetProperties().ToList().Find(p => p.Name == property.Name);

            // property exists on new type
            if (newTypeProperty != null)
            {
                //set property on new generic type equal to the value of the same property on old generic type
                newModel.GetType().GetProperty(newTypeProperty.Name).SetValue(newModel, modelToTransform.GetType().GetProperty(property.Name).GetValue(modelToTransform));
            }
        }

        return newModel;
    }

    public static List<TTarget> ToModels<TTarget, TSource>(this List<TSource> listToTransform) where TTarget : new()
    {
        return listToTransform.Select(t => t.ToModel<TTarget, TSource>()).ToList();
    }
}
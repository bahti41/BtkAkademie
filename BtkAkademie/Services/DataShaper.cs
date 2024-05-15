using Entities.Concrete;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class DataShaper<T> : IDataShaper<T> where T : class
    {
        // this properties
        public PropertyInfo[] Properties { get; set; }
        public DataShaper()
        {
            Properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
        }


        // Methodlar
        public IEnumerable<ShapedEntity> ShapeData(IEnumerable<T> entities, string fieldsString)
        {
            var requeriedFields = GetRequiredProperties(fieldsString);
            return FetchData(entities, requeriedFields);
        }

        public ShapedEntity ShapeData(T entity, string fieldsString)
        {
            var requiredProperties = GetRequiredProperties(fieldsString);
            return FetchDataForEntity(entity, requiredProperties);
        }



        // BussinesRun

        // İhtiyac Duyulan Propertilerin filitreleme ve alınma işlemi
        private IEnumerable<PropertyInfo> GetRequiredProperties(string fieldsString)
        {
            var requiredFields = new List<PropertyInfo>();
            if (!string.IsNullOrWhiteSpace(fieldsString))
            {
                var fields = fieldsString.Split(',' ,StringSplitOptions.RemoveEmptyEntries);

                foreach (var field in fields)
                {
                    var property = Properties
                        .FirstOrDefault(pi => pi.Name.Equals(field.Trim(),
                        StringComparison.InvariantCultureIgnoreCase));
                    if (property is null)
                        continue;
                    requiredFields.Add(property);
                }
            }
            else
            {
                requiredFields = Properties.ToList();
            }

            return requiredFields;
        }


        // Property Tek bir deger Dönecek Fonksiyon shapeData'da Cagarılacak(ExpandoObject)
        private ShapedEntity FetchDataForEntity(T entity,IEnumerable<PropertyInfo> requiredProperties)
        {
            var shapedObject =  new ShapedEntity();
            foreach (var property in requiredProperties)
            {
                var objectPropertyValue = property.GetValue(entity);
                shapedObject.Entity.TryAdd(property.Name, objectPropertyValue);
            }

            var objectProperty = entity.GetType().GetProperty("Id");
            shapedObject.Id = (int)objectProperty.GetValue(entity);

            return shapedObject;
        }


        // Property Listesini Dönecek Fonksiyon shapeData'da Cagarılacak(IEnumerable'de)
        private IEnumerable<ShapedEntity> FetchData(IEnumerable<T> entities,IEnumerable<PropertyInfo> requiredProperties)
        {
            var shapedData = new List<ShapedEntity>();
            foreach (var entity in entities)
            {
                var shapedObject = FetchDataForEntity(entity,requiredProperties);
                shapedData.Add(shapedObject);
            }
            return shapedData;
        }
    }
}

using System.Reflection;

namespace LINQDistinctByMutlipleDynamicFileds
{
    public class PropertyComparer<T> : IEqualityComparer<T>
    {
        private PropertyInfo _propertyInfo;
        private List<PropertyInfo> _propertyInfoList = new List<PropertyInfo>();
        public PropertyComparer(List<string> propertNames)
        {
            if (propertNames == null)
                throw new ArgumentNullException("propertNames param is null");
            foreach (var propertyName in propertNames)
            {
                //store a reference to the property info object for use during the comparison
                _propertyInfo = typeof(T).GetProperty(propertyName,BindingFlags.GetProperty | BindingFlags.Instance | BindingFlags.Public);
                if (_propertyInfoList == null)
                {
                    throw new ArgumentException(string.Format("{0} is not a property of type { 1 }.", propertyName, typeof(T)));
                }
                _propertyInfoList.Add(_propertyInfo);
            }           
        }

        #region IEqualityComparer<T> Members

        public bool Equals(T obj1, T obj2)
        {
            var isEqual = true;
            foreach (var propertyInfo in _propertyInfoList)
            {
                //get the property value of the comparison property of obj1 and of obj1
                object obj1Value = propertyInfo.GetValue(obj1);
                object obj2Value = propertyInfo.GetValue(obj2);
                //if the xValue is null then we consider them equal if and only if yValue is null
                if (obj1Value == null)
                    isEqual = (obj1Value == null);
                else
                    //use the default comparer for whatever type the comparison property is.
                    isEqual = (obj1Value.Equals(obj2Value));
                if (!isEqual)
                    break;
            }
            return isEqual;
        }

        public int GetHashCode(T obj)
        {
            int hashCode = 0;
            foreach (var propertyInfo in _propertyInfoList)
            {
                //get the value of the comparison property out of obj
                object propertyValue = propertyInfo.GetValue(obj);

                if (propertyValue == null)
                    hashCode += 0;
                else
                    hashCode += propertyValue.GetHashCode();
            }
            return hashCode;
        }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace SkillPool.Core.Extensions
{
    public static class ObservableExtension
    {
        /// <summary>
        /// 将List<T>转化为ObservableCollection<T>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> source)
        {
            ObservableCollection<T> collection = new ObservableCollection<T>();
            foreach (T item in source)
            {
                collection.Add(item);
            }
            return collection;
        }
    }
}

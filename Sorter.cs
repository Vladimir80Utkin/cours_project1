using System;
using System.Reflection;
using System.ComponentModel;

namespace cours_project
{
    public class Sorter
    {
        public static void InsertionSort(Flat[] flats, int propertyIndex)
        {
            PropertyInfo[] properties = Flat.GetProperties();

            PropertyInfo property = properties[propertyIndex];

            var displayNameAttribute = property.GetCustomAttribute<DisplayNameAttribute>();
            string displayName;

            if (displayNameAttribute != null) displayName = displayNameAttribute.DisplayName;
            else displayName = property.Name;

            try
            {
                for (int i = 1; i < flats.Length; i++)
                {
                    var current = flats[i];
                    var key = property.GetValue(current);
                    int j = i - 1;

                    while (j >= 0)
                    {
                        var previous = flats[j];
                        var prevValue = property.GetValue(previous);

                        if (Comparer<object>.Default.Compare(key, prevValue) < 0) flats[j + 1] = flats[j];
                        else break;

                        j--;
                    }

                    flats[j + 1] = current;
                }

                ConsoleMessages.WriteSuccessMessage($"Квартиры успешно отсортированы по свойству: {displayName}.");
            }
            catch (Exception exception) {ConsoleMessages.WriteErrorMessage($"Произошла ошибка при сортировке: {exception.Message}");}
        }
    }
}

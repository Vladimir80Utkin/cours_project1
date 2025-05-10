using System.Reflection;
using System.Text.RegularExpressions;
using System.ComponentModel;

namespace cours_project
{
    public class Flat
    {
        private string  tenantFullName;
        private string flatAddress;
        private int peopleCount;
        private double tariffPerPerson;

        [DisplayName("ФИО арендатора")]
        public string TenantFullName{
            get{return tenantFullName;}
            set{
                if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("Имя арендатора не может быть пустым!");
                if (!Regex.IsMatch(value, @"^[A-ZА-Я][a-zа-я]*(\s[A-ZА-Я][a-zа-я]*)*$")) throw new ArgumentException("Имя арендатора должно начинаться с заглавной буквы, содержать только буквы и один пробел между словами!");
                else tenantFullName = value;
            }
        }

        [DisplayName("Адрес квартиры")]
        public string FlatAddress{
            get{return flatAddress;}
            set{
                if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("Адрес квартиры не может быть пустым!");
                else flatAddress = value;
            }
        }

        [DisplayName("Количество человек")]
        public int PeopleCount{
            get{return peopleCount;}
            set{
                if (value < 1) throw new ArgumentException("Количество человек не может быть меньше 1!");
                else peopleCount = value;
            }
        }

        [DisplayName("Тариф за человека")]
        public double TariffPerPerson{
            get{return tariffPerPerson;}
            set{
                if (value < 1) throw new ArgumentException("Тариф не может быть меньше 1!");
                else tariffPerPerson = value;
            }
        }

        public string Info(){
            string result = "";

            PropertyInfo[] properties = this.GetType().GetProperties();

            foreach (var property in properties)
            {
                string displayName;

                var displayNameAttribute = property.GetCustomAttribute<DisplayNameAttribute>();

                if (displayNameAttribute != null) {displayName = displayNameAttribute.DisplayName;}
                else {displayName = property.Name;}

                result += $"{displayName}: {property.GetValue(this)}\n";
            }
            return result;
        }

        public string GetPropertiesNames(){

            string result = "";

            PropertyInfo[] properties = this.GetType().GetProperties();

            for (int i = 0; i < properties.Length; i++)
            {
                string displayName;   
                var displayNameAttribute = properties[i].GetCustomAttribute<DisplayNameAttribute>();

                if (displayNameAttribute != null) {displayName = displayNameAttribute.DisplayName;}
                else {displayName = properties[i].Name;}

                result += $"{i+1}: {displayName}\n";
            }
            return result;
        }

        public int GetPropertiesCount()
        {
            PropertyInfo[] properties = this.GetType().GetProperties();
            return properties.Length;
        }

        public static PropertyInfo[] GetProperties()
        {
            return typeof(Flat).GetProperties();
        }
    }
}
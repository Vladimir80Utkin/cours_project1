using System.Reflection;
using System.Text.RegularExpressions;

namespace cours_project
{
    class Flat
    {
        private string  tenantFullName;
        private string flatAddress;
        private int peopleCount;
        private double tariffPerPerson;

        public string TenantFullName{
            get{return tenantFullName;}
            set{
                if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("Имя арендатора не может быть пустым!");
                if (!Regex.IsMatch(value, @"^[A-ZА-Я][a-zа-я]*(\s[A-ZА-Я][a-zа-я]*)*$")) throw new ArgumentException("Имя арендатора должно начинаться с заглавной буквы, содержать только буквы и один пробел между словами!");
                else tenantFullName = value;
            }
        }
        public string FlatAddress{
            get{return flatAddress;}
            set{
                if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("Адрес квартиры не может быть пустым!");
                else flatAddress = value;
            }
        }
        public int PeopleCount{
            get{return peopleCount;}
            set{
                if (value < 1) throw new ArgumentException("Количество человек не может быть меньше 1!");
                else peopleCount = value;
            }
        }
        public double TariffPerPerson{
            get{return tariffPerPerson;}
            set{
                if (value < 1) throw new ArgumentException("Тариф не может быть иеньше 1!");
                else tariffPerPerson = value;
            }
        }

        public Flat(string _tenantFullName, string _flatAddress){
            tenantFullName = _tenantFullName;
            flatAddress = _flatAddress;
        }

        public string Info(){
            string result = "";
            PropertyInfo[] properties = this.GetType().GetProperties();
            foreach (var property in properties) result += $"{property.Name}: {property.GetValue(this)}\n";
            return result;
        }
    }
}
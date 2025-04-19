namespace cours_project;

public class FlatSorter
{
    public bool Sort(List<Flat> flats, string? choice)
    {
        switch (choice)
        {
            case "1":
                flats.Sort((a, b) => a.TenantFullName.CompareTo(b.TenantFullName));
                return true;

            case "2":
                flats.Sort((a, b) => a.FlatAddress.CompareTo(b.FlatAddress));
                return true;

            case "3":
                flats.Sort((a, b) => a.PeopleCount.CompareTo(b.PeopleCount));
                return true;

            case "4":
                flats.Sort((a, b) => a.TariffPerPerson.CompareTo(b.TariffPerPerson));
                return true;

            default:
                return false;
        }
    }
}

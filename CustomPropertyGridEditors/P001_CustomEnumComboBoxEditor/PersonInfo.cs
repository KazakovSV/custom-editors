using System.ComponentModel;

namespace P001_CustomEnumComboBoxEditor
{
    public enum Gender
    {
        [Description("Женщина")]
        Female,

        [Description("Мужчина")]
        Male
    }

    public class PersonInfo
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Age { get; set; }

        public Gender Sex { get; set; }

        public PersonInfo()
        {
            FirstName = "Unknown";
            LastName = "Unknown";
            Age = 1;
            Sex = Gender.Female;
        }
    }
}

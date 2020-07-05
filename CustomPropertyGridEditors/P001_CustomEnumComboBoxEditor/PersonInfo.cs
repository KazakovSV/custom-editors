using System.ComponentModel;

using P001_CustomEnumComboBoxEditor.Editor;

namespace P001_CustomEnumComboBoxEditor
{
    public enum Gender
    {
        [Description("Женщина")]
        Female,

        [Description("Мужчина")]
        Male
    }

    public enum Profession
    {
        [Description("Столяр")]
        Carpenter,

        Locksmith,

        [Description("Программист")]
        Programmer
    }

    public class PersonInfo
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Age { get; set; }

        [Editor(typeof(CustomEnumComboBoxEditor), typeof(CustomEnumComboBoxEditor))]
        public Gender Sex { get; set; }

        [Editor(typeof(CustomEnumComboBoxEditor), typeof(CustomEnumComboBoxEditor))]
        public Profession Profession { get; set; }

        public PersonInfo()
        {
            FirstName = "Unknown";
            LastName = "Unknown";
            Age = 1;
            Sex = Gender.Female;
            Profession = Profession.Programmer;
        }
    }
}

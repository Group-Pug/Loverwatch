using System.ComponentModel;
using System.Reflection;

public class EnumReflection {

    public static string GetDescription(Profession val)
    {
        FieldInfo fi = val.GetType().GetField(val.ToString());

        if (fi != null)
        {
            object[] profs = fi.GetCustomAttributes(typeof(DescriptionAttribute), true);
            if (profs != null && profs.Length > 0)
            {
                return ((DescriptionAttribute)profs[0]).Description;
            }
        }

        return val.ToString();
    }
}

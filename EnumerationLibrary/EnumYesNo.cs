using System.Linq;

namespace EnumerationLibrary
{
    public class EnumYesNo : Enumeration<string>
    {
        public static EnumYesNo Yes = new EnumYesNo("Y", "Yes");
        public static EnumYesNo No = new EnumYesNo("N", "No");
        public static EnumYesNo Undefined = new EnumYesNo("", "");
        
        public EnumYesNo() : this(Undefined.Code, Undefined.Description)
        { }

        public EnumYesNo(string code, string description) : base(code, description)
        { }

        public static EnumYesNo FromBool(bool value) => (value ? Yes : No);

        public bool IsYes => Code.Equals(Yes.Code);
        public bool IsNo => Code.Equals(No.Code);
        public bool IsUndefined => Code.Equals(Undefined.Code);
    }
}

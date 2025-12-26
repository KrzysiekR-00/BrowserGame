namespace Shared.Characters;

public class AttributesDto
{
    public Dictionary<AttributeDto, int> Values { get; set; } = new Dictionary<AttributeDto, int>();
}

//public readonly record struct Attributes(
//    int AreaAttack,
//    int RapidAttack,
//    int PrecisionAttack
//    )
//{
//    public static Attributes operator +(Attributes a, Attributes b)
//    {
//        return new Attributes(
//            a.AreaAttack + b.AreaAttack,
//            a.RapidAttack + b.RapidAttack,
//            a.PrecisionAttack + b.PrecisionAttack
//        );
//    }

//    public static Attributes operator -(Attributes a, Attributes b)
//    {
//        return new Attributes(
//            a.AreaAttack - b.AreaAttack,
//            a.RapidAttack - b.RapidAttack,
//            a.PrecisionAttack - b.PrecisionAttack
//        );
//    }
//}

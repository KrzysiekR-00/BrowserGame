//using Shared.Characters;

//namespace GameAPI.Data.Parsers;

//internal static class AttributesCsvParser
//{
//    internal static string ToCsv(this Attributes attributes)
//    {
//        return string.Join(
//            ",",
//            attributes.AreaAttack,
//            attributes.RapidAttack,
//            attributes.PrecisionAttack
//            );
//    }

//    internal static Attributes ToAttributes(this string csv)
//    {
//        if (string.IsNullOrEmpty(csv)) return new Attributes();

//        string[] parts = csv.Split(",");

//        if (parts.Length != 3) return new Attributes();

//        var attributesInts = parts.Select(x => int.TryParse(x, out int i) ? i : 0).ToArray();

//        return new Attributes(
//            attributesInts[0],
//            attributesInts[1],
//            attributesInts[2]
//            );
//    }
//}

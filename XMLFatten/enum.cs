using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XMLFatten
{
    public enum Direction
    {
        Out,
        In
    }

    public enum SubItemMapRelation
    {
        ONE,
        ONE_OPTION,
        MANY,
        MANY_OPTION
    }

    public static class PropName
    {
        public const string Name = "Name";
        public const string XPath = "XPath";
        public const string SubItemMapRelation = "SubItemMapRelation";
        public const string Order = "Order";
        public const string Depth = "Depth";
    }

    public static class Label
    {
        public const string Element = "Element";
        public const string Attribute = "Attribute";
        public const string Leaf = "Leaf";
        public const string Level = "Level";
    }

    public static class RelationType
    {
        public const string IS_FATHER_OF = "IS_FATHER_OF";
        public const string BELONG_TO = "BELONG_TO";        

    }
}

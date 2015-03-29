using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XMLFatten
{

    public static class XmlGraphExtensions
    {
        public static void AddXPathToAllXmlElements(this XmlGraph graph)
        {
            graph.RootXmlNode.AddXPathToSubAndThisXmlElements(string.Empty);            
        }

        private static void AddXPathToSubAndThisXmlElements(this Node xmlElement,string fatherXPath)
        {
            var elemtName = xmlElement.GetProperty<string>(PropName.Name);
            var XPath = string.Empty;
            if (xmlElement.ContainsLabel(Label.Attribute))
            {
                 XPath = fatherXPath + @"/@" + elemtName;
                 xmlElement.SetProperty(PropName.XPath, XPath);
            }
            else if (xmlElement.ContainsLabel(Label.Element))
            {
                XPath = fatherXPath + @"/" + elemtName;
                //add XPath to rootXml
                xmlElement.SetProperty(PropName.XPath, XPath);

                var xmlSubElemts = xmlElement.GetRelationships(Direction.Out, RelationType.IS_FATHER_OF, (r) =>
                {
                    return r.EndNode.ContainsAnyLabels(Label.Element, Label.Attribute);
                }).Select(r => r.EndNode);
                foreach (var xml_ele in xmlSubElemts)
                {
                    xml_ele.AddXPathToSubAndThisXmlElements(XPath);
                }
            }


            
            
        }
    }

    public static class NodeExtensions
    {
        public static void AddProperty(this Node node, string name, object value)
        {
            node.Properties.Add(name, value);
        }

        public static Relationship CreateSubItemMapRelation(this Node node, Node subNode, SubItemMapRelation relation)
        {
            var rel = new Relationship(node, subNode, RelationType.IS_FATHER_OF);
            rel.Properties.Add(PropName.SubItemMapRelation, relation);
            rel.Properties.Add(PropName.Order, -1);
            return rel;
        }

        public static Relationship CreateSubItemMapRelation(this Node node, Node subNode, SubItemMapRelation relation, int order)
        {
            var rel = new Relationship(node, subNode, RelationType.IS_FATHER_OF);
            rel.Properties.Add(PropName.SubItemMapRelation, relation);
            rel.Properties.Add(PropName.Order, order);
            return rel;
        }
    }




    public static class FluentHelper
    {
        public static IEnumerable<Node> ContainsLabel(this IEnumerable<Node> nodes, string label)
        {
            return nodes.Where(n => n.ContainsLabel(label));
        }

        public static IEnumerable<Node> ContainsAllLabels(this IEnumerable<Node> nodes,params string[] labels)
        {
            return nodes.Where(n => n.ConstainsAllLabels(labels));
        }

        public static IEnumerable<Node> ContainsAnyLabels(this IEnumerable<Node> nodes, params string[] labels)
        {
            return nodes.Where(n =>n.ContainsAnyLabels(labels));
        }
    }
}

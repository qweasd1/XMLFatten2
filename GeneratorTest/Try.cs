using System;
using System.Reflection;
using System.Linq;
using System.Collections.Generic;
using System.CodeDom;
using CodeDomFluentHelper;
using XMLFatten;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Linq.Expressions;
using System.Xml.XPath;

namespace GeneratorTest
{
    [TestClass]
    public class Try
    {
        [TestMethod]
        public void TestGenerate()
        {
            var ns = Namespace.New("GenericParser.PlugIn");            
            var codeType = ns.AddClass("SpecialParser");
            codeType.Implements<IParser>();
            var parseMethod = codeType.Method("Parse");
            
            
                
            var code  =  codeType.ToCode();

         }

        private XmlGraph CreateXmlGraph()
        {
            var graph = new XmlGraph();
            var person_root = graph.NewRootElement("Person");

            var Id_attr = graph.NewAttribute("Id");
            var Name_ele = graph.NewElement("Name");
            var Pets_ele = graph.NewElement("Pets");
            var Pet_ele = graph.NewElement("Pet");
            var friend_ele = graph.NewElement("Friend");

            var rel = person_root.CreateSubItemMapRelation(Id_attr, SubItemMapRelation.ONE);
            person_root.CreateSubItemMapRelation(Name_ele, SubItemMapRelation.ONE, 0);
            person_root.CreateSubItemMapRelation(Pets_ele, SubItemMapRelation.ONE, 1);
            person_root.CreateSubItemMapRelation(friend_ele, SubItemMapRelation.MANY_OPTION, 2);

            Pets_ele.CreateSubItemMapRelation(Pet_ele, SubItemMapRelation.MANY_OPTION);

            return graph;
        }


        [TestMethod]
        public void TestXPath()
        {
            using (StringReader reader = new StringReader(SampleXml))
            {
                XPathDocument doc = new XPathDocument(reader);
                
                var nv = doc.CreateNavigator();
                var t = nv.Select("/Persion/Id").Current;
                
            }
        }


        private const string SampleXml = @"<?xml version=""1.0"" encoding=""utf-8"" ?> 
<Person xmlns=""http://tempuri.org/Sample.xsd""
         Id=""001"">
  <Name>Bob</Name>
  <Pets>
    <Pet>Tom</Pet>
    <Pet>Jerry</Pet>
  </Pets>
  <Friend>Jimmy</Friend>
</Person>
";
    }
}

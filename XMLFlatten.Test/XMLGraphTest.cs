using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using XMLFatten;

namespace XMLFlatten.Test
{
    [TestClass]
    public class XMLGraphTest
    {
        [TestMethod]
        public void TestGraph()
        {
            var graph = new XmlGraph();
            var person_root = graph.NewRootElement("Person");

            var Id_attr = graph.NewAttribute("Id");

            var rel = person_root.CreateRelationshipTo(Id_attr, RelationType.IS_FATHER_OF);



            Assert.AreEqual(person_root, rel.StartNode);
            Assert.AreEqual(Id_attr, rel.EndNode);
        }


        [TestMethod]
        public void TestCreateSubItemMapRelation()
        {
            var graph = new XmlGraph();
            var person_root = graph.NewRootElement("Person");

            var Id_attr = graph.NewAttribute("Id");

            var rel = person_root.CreateSubItemMapRelation(Id_attr,SubItemMapRelation.ONE);



            //Assert.AreEqual(person_root, rel.StartNode);
            //Assert.AreEqual(Id_attr, rel.EndNode);
            Assert.AreEqual(SubItemMapRelation.ONE, rel.Properties[PropName.SubItemMapRelation]);
        }

        [TestMethod]
        public void TestCreateXMLXsdAggregate()
        {
            var graph = new XmlGraph();
            var person_root = graph.NewRootElement("Person");

            var Id_attr = graph.NewAttribute("Id");
            var Name_ele = graph.NewElement("Name");
            var Address_ele = graph.NewElement("Address");
            var Pets_ele = graph.NewElement("Pets");
            var Pet_ele = graph.NewElement("Pet");

            var rel = person_root.CreateSubItemMapRelation(Id_attr, SubItemMapRelation.ONE);
            person_root.CreateSubItemMapRelation(Name_ele, SubItemMapRelation.ONE, 0);
            person_root.CreateSubItemMapRelation(Pets_ele, SubItemMapRelation.ONE, 1);
            Pets_ele.CreateSubItemMapRelation(Pet_ele, SubItemMapRelation.MANY_OPTION);


            //Assert.AreEqual(person_root, rel.StartNode);
            //Assert.AreEqual(Id_attr, rel.EndNode);
            
        }

        [TestMethod]
        public void TestCreateLevel()
        {
            var graph = new XmlGraph();
            var rootLevel = graph.NewRootLevel();
            var sublevel = graph.NewLevel(rootLevel);
        }

        [TestMethod]
        public void TestLevelVisitor()
        {
            var graph = createCompletedGraph();
            var levelVisitor = new LevelVisitor(graph);
            levelVisitor.Visit();


        }

        [TestMethod]
        public void TestAddXPathToAllXmlElements()
        {
            var graph = createCompletedGraph();
            graph.AddXPathToAllXmlElements();   

        }

        private static XmlGraph createCompletedGraph()
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
    }
}

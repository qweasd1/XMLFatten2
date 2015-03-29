using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XMLFatten
{
    public class LevelVisitor
    {
        private Node _levelNode;
        private Node _startXmlNode;
        private XmlGraph _graph;
        


        public void Visit()
        {
            _startXmlNode.CreateRelationshipTo(_levelNode, RelationType.BELONG_TO);
            Visit(_startXmlNode);           
            
        }

        private void Visit(Node xmlNode)
        {
            
            //OneToOneElement:  to connect these nodes to current level, recusive use this visitor to visit these nodes
            var oneToOneNodes = xmlNode.GetRelationships(Direction.Out,RelationType.IS_FATHER_OF,IsOneToOne).Select(r => r.EndNode);
            foreach (var node in oneToOneNodes)
            {
                node.CreateRelationshipTo(_levelNode, RelationType.BELONG_TO);

                //if the node contains "element" label, recursive use this visitor to visit it
                if(node.ContainsLabel(Label.Element))
                {
                    Visit(node);
                }                
            }

            //OneToManyElement: create a new LevelVisiter to visit 
            var oneToManyNodes = xmlNode.GetRelationships(Direction.Out,RelationType.IS_FATHER_OF,(r=>!IsOneToOne(r))).Select(r => r.EndNode);
            foreach (var node in oneToManyNodes)
            {
                var levelVisitor = new LevelVisitor(node, _levelNode, _graph);
                levelVisitor.Visit();
            }
        }

        public LevelVisitor(XmlGraph graph)
        {
            _graph = graph;
            _levelNode = graph.NewRootLevel();
            _startXmlNode = graph.RootXmlNode;
        }

        public LevelVisitor(Node xmlElement, Node fatherLevel, XmlGraph graph)
        {
            _graph = graph;
            _levelNode = graph.NewLevel(fatherLevel);
            _startXmlNode = xmlElement;
        }


        private static bool IsOneToOne(Relationship relationship)
        {
            var mapRelation = relationship.GetProperty<SubItemMapRelation>(PropName.SubItemMapRelation);
            var result = mapRelation == SubItemMapRelation.ONE || mapRelation == SubItemMapRelation.ONE_OPTION;
            return result;
        }
    }
}

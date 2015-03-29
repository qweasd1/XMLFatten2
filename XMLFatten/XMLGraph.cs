using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XMLFatten
{
    public  class XmlGraph
    {

        private List<Node> _allXmlNodes;        
        private List<Node> _allLevelNodes;
        private Node _rootXmlNode;
        private Node _rootLevelNode;


        public List<Node> AllXmlNodes
        {
            get { return _allXmlNodes; }
        }

        public List<Node> AllLevelNodes
        {
            get { return _allLevelNodes; }
        }

        public Node RootXmlNode
        {
            get { return _rootXmlNode; }
        }

        public Node RootLevelNode
        {
            get { return _rootLevelNode; }
        }



        public XmlGraph()
        {
            _allXmlNodes = new List<Node>();
            _allLevelNodes = new List<Node>();
        }

        private Node NewNode(string name)
        {
            var node = new Node();
            node.Properties.Add(PropName.Name, name);
            _allXmlNodes.Add(node);
            return node;
        }

        public Node NewElement(string name)
        {
            var node = new Node();

            node.Properties.Add(PropName.Name, name);
            node.Labels.Add(Label.Element);

            _allXmlNodes.Add(node);

            return node;
        }

        public Node NewAttribute(string name)
        {
            var node = new Node();

            node.Properties.Add(PropName.Name, name);
            node.Labels.Add(Label.Attribute);

            _allXmlNodes.Add(node);

            return node;
        }

        public Node NewRootElement(string name)
        {
            var node = new Node();

            node.Labels.Add(Label.Element);
            node.Properties.Add(PropName.Name, name);
            

            _allXmlNodes.Add(node);
            _rootXmlNode = node;
            return node;
        }

        public Node NewRootLevel()
        {
            var level = new Node();

            level.Labels.Add(Label.Level);
            level.Properties.Add(PropName.Depth, 0);

            _allLevelNodes.Add(level);
            _rootLevelNode = level;
            return level;
        }

        public Node NewLevel(Node fatherLevel)
        {
            var level = new Node();

            level.Labels.Add(Label.Level);
            level.Properties.Add(PropName.Depth, ((int)fatherLevel.Properties[PropName.Depth] + 1));
            fatherLevel.CreateRelationshipTo(level, RelationType.IS_FATHER_OF);
            
            _allLevelNodes.Add(level);            
            return level;
        }

    }
}

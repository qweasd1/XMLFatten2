using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XMLFatten
{
    public class Node
    {
        public List<string> Labels { get; set; }
        public Dictionary<string,object> Properties { get; set; }
        public List<Relationship> Relationships { get; set; }

        private List<Relationship> OutRelationShips
        {
            get
            {
                return Relationships.Where(x => x.StartNode == this).ToList();
            }
        }

        private List<Relationship> InRelationShips
        {
            get
            {
                return Relationships.Where(x => x.EndNode == this).ToList();
            }
        }

        internal Node()
        {
            Labels = new List<string>();
            Properties = new Dictionary<string, object>();
            Relationships = new List<Relationship>();
        }

        public T GetProperty<T>(string name)
        {
            return (T)Properties[name];
        }

        public Relationship CreateRelationshipTo(Node node, string relationType)
        {
            return new Relationship(this, node, relationType);
        }

        public IList<Relationship> GetRelationships(Direction direction, string relationType)
        {
            if (direction == Direction.In)
            {
                return Relationships.Where(r => r.EndNode == this && r.Type == relationType).ToList();
            }
            else
            {
                return Relationships.Where(r => r.StartNode == this && r.Type == relationType).ToList();
            }
        }

        public IList<Relationship> GetRelationships(Direction direction, string relationType, Predicate<Relationship> predicate)
        {
            if (direction == Direction.In)
            {
                return Relationships.Where(r => r.EndNode == this && r.Type == relationType && predicate.Invoke(r)).ToList();
            }
            else
            {
                var t = Relationships.Where(r => r.StartNode == this && r.Type == relationType && predicate.Invoke(r)).ToList();
                return Relationships.Where(r => r.StartNode == this && r.Type == relationType && predicate.Invoke(r)).ToList();
            }
        }

        public bool ContainsLabel(string label)
        {
            return Labels.Contains(label);
        }

        public bool ConstainsAllLabels(params string[] labels)
        {
            return labels.All(l => Labels.Contains(l));
        }

        public bool ContainsAnyLabels(params string[] labels)
        {
            return labels.Any(l => Labels.Contains(l));
        }

        internal void SetProperty(string name, string value)
        {
            Properties.Add(name, value);
        }
    }
}

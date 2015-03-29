using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XMLFatten
{
    public class Relationship
    {
        public Node StartNode { get; set; }
        public Node EndNode { get; set; }
        public string Type { get; set; }
        public Dictionary<string,object> Properties { get; set; }

        private Relationship()
        {
            Properties = new Dictionary<string, object>();
        }

        public T GetProperty<T>(string name)
        {
            return (T)Properties[name];
        }

        internal Relationship(Node start, Node end, string type)
            :this()
        {
            StartNode = start;
            EndNode = end;
            Type = type;

            start.Relationships.Add(this);
            end.Relationships.Add(this);
        }
    }
}

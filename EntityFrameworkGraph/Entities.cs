using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections.ObjectModel;

using System.Data.Entity;

using System.ComponentModel.DataAnnotations.Schema;

namespace EntityFrameworkGraph
{
    public class Node
    {
        public int ID { get; set; }
        public string Title { get; set; }

        public virtual ObservableCollection<Tag> Tags { get; set; }

        [InverseProperty("A")]
        public virtual ObservableCollection<Relationship> Outgoing { get; set; }
        [InverseProperty("B")]
        public virtual ObservableCollection<Relationship> Incoming { get; set; }

        public virtual ObservableCollection<Attribute> Attributes { get; set; }

        public Node()
        {
            Tags = new ObservableCollection<Tag>();
            Outgoing = new ObservableCollection<Relationship>();
            Incoming = new ObservableCollection<Relationship>();
            Attributes = new ObservableCollection<Attribute>();
        }
    }

    public class Tag
    {
        public int ID { get; set; }
        public string Title { get; set; }

        public virtual ObservableCollection<Node> Nodes { get; set; }
    }

    public class Attribute
    {
        public int ID { get; set; }
        public string Key { get; set; }
        public string Val { get; set; }

        public virtual Node Node { get; set; }
    }


    public class Label
    {
        public int ID { get; set; }
        public string Title { get; set; }
    }

    public class Relationship
    {
        public int ID { get; set; }
        public Node A { get; set; }
        public Node B { get; set; }
        public Label Label { get; set; }

        public virtual ObservableCollection<Property> Properties { get; set; }

        public Relationship()
        {
            Properties = new ObservableCollection<Property>();
        }

    }

    public class Property
    {
        public int ID { get; set; }
        public string Key { get; set; }
        public string Val { get; set; }

        public virtual Relationship Relationship { get; set; }
    }

    public class Context : DbContext
    {
        public DbSet<Node> Nodes { get; set; }
        public DbSet<Label> Labels { get; set; }
        public DbSet<Relationship> Relationships { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Property> Properties { get; set; }

        public Context()
        {
            Database.SetInitializer<Context>(new DropCreateDatabaseAlways<Context>());
        }
    }
}

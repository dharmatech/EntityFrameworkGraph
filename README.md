# EntityFrameworkGraph

* Graph database in Entity Framework
* Data browser in WPF
* Code-first definitions are in [Entities.cs](EntityFrameworkGraph/Entities.cs)
* The data browser is in [BrowserWindow.cs](EntityFrameworkGraph/BrowserWindow.cs)
* The data browser is pure C# - no XAML
* Entitlements and Access Control Management [example](EntityFrameworkGraph/EntitlementsAndAccessControlManagement.cs). [Original Cypher version](http://gist.neo4j.org/?4471127413fd724ed0a3). 

# Comparison with Neo4j terminology

| EntityFrameworkGraph | Neo4j                   |
|----------------------|-------------------------|
| Nodes                | Nodes                   |
| Tags                 | Node labels             |
| Attributes           | Node properties         |
| Relationships        | Relationships           |
| Label                | Relationship label      |
| Properties           | Relationship properties |

# Data browser

The *company* tag is selected. Nodes with the *company* tag are shown. The *Subsidry* node is selected. The incoming relationships are shown on the left. Outgoing relationships are on the right. Below the nodes are the selected nodes attributes. To the right of the node attributes are the selected relationship's properties.

![](http://i.imgur.com/UHyuDNX.png)

# Creating nodes

Cypher

````
CREATE
(`Ben`:administrator {name:'Ben'}),
(`Sarah`:administrator {name:'Sarah'}),
(`Liz`:administrator {name:'Liz'}),
(`Phil`:administrator {name:'Phil'})
````

C#

```
var Ben = new Node() { Title = "Ben", Tags = { administrator }, Attributes = { new Attribute() { Key = "name", Val = "Ben" } } };
var Sarah = new Node() { Title = "Sarah", Tags = { administrator }, Attributes = { new Attribute() { Key = "name", Val = "Sarah" } } };
var Liz = new Node() { Title = "Liz", Tags = { administrator }, Attributes = { new Attribute() { Key = "name", Val = "Liz" } } };
var Phil = new Node() { Title = "Phil", Tags = { administrator }, Attributes = { new Attribute() { Key = "name", Val = "Phil" } } };
```

# Query Example

Cypher

```cypher
MATCH

  (admin:administrator {name: 'Ben'})-[:MEMBER_OF]->(grp),

  (grp)-[:ALLOWED_INHERIT]->(parent),

  (parent)<-[:CHILD_OF*0..3]-(child), 

  (child)<-[:WORKS_FOR]-(emp),

  (emp)-[:HAS_ACCOUNT]->(acc)

RETURN admin.name, grp.name, parent.name, child.name, emp.name, acc.name
```

C#

```C#
var admin = Ben;

foreach (var grp in admin.Outgoing.Where(rel => rel.Label == MEMBER_OF).Select(rel => rel.B))
    foreach (var parent in grp.Outgoing.Where(rel => rel.Label == ALLOWED_INHERIT).Select(rel => rel.B))
    {
        var children = new List<Node>();

        children.Add(parent);

        foreach (var child_1 in parent.Incoming.Where(rel => rel.Label == CHILD_OF).Select(rel => rel.A))
        {
            children.Add(child_1);

            foreach (var child_2 in child_1.Incoming.Where(rel => rel.Label == CHILD_OF).Select(rel => rel.A))
            {
                children.Add(child_2);

                foreach (var child_3 in child_2.Incoming.Where(rel => rel.Label == CHILD_OF).Select(rel => rel.A))
                    children.Add(child_3);                                
            }
        }

        foreach (var child in children)
            foreach (var emp in child.Incoming.Where(rel => rel.Label == WORKS_FOR).Select(rel => rel.A))
                foreach (var acc in emp.Outgoing.Where(rel => rel.Label == HAS_ACCOUNT).Select(rel => rel.B))
                    Console.WriteLine("{0, -10} {1, -10} {2, -10} {3, -10} {4, -10} {5, -10}",
                        admin.Title, grp.Title, parent.Title, child.Title, emp.Title, acc.Title);
    }

```

With `In` and `Out` extension methods

```C#
var admin = Ben;

foreach (var grp in admin.Out(MEMBER_OF))
    foreach (var parent in grp.Out(ALLOWED_INHERIT))
        foreach (var child in parent.In(CHILD_OF, 0, 3))
            foreach (var emp in child.In(WORKS_FOR))
                foreach (var acc in emp.Out(HAS_ACCOUNT))
                    Console.WriteLine("{0, -10} {1, -10} {2, -10} {3, -10} {4, -10} {5, -10}",
                        admin.Title, grp.Title, parent.Title, child.Title, emp.Title, acc.Title);
```

Results of query on console:

![](http://i.imgur.com/PD6H39r.png)

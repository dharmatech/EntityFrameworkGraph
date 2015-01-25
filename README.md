# EntityFrameworkGraph

* Graph database in Entity Framework
* Data browser in WPF
* Code-first definitions are in [Entities.cs](EntityFrameworkGraph/Entities.cs).
* The data browser is in [BrowserWindow.cs](EntityFrameworkGraph/Entities.cs).
* The data browser is pure C# - no XAML
* Entitlements and Access Control Management [example](EntityFrameworkGraph/EntitlementsAndAccessControlManagement.cs)

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

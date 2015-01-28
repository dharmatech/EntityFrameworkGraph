using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.Entity;

using System.Collections.ObjectModel;

namespace EntityFrameworkGraph
{
    static class EntitlementsAndAccessControlManagement
    {
        static string AsString(this Relationship rel) { return string.Format("{0} {1} {2}", rel.A.Title, rel.Label.Title, rel.B.Title); }

        static IEnumerable<Relationship> WithLabel(this ObservableCollection<Relationship> rels, Label label)
        { return rels.Where(rel => rel.Label == label); }

        static IEnumerable<Node> Out(this Node node, Label label)
        { return node.Outgoing.Where(rel => rel.Label == label).Select(rel => rel.B); }

        static IEnumerable<Node> In(this Node node, Label label)
        { return node.Incoming.Where(rel => rel.Label == label).Select(rel => rel.A); }

        static void NestedForLoopsAux(Node node, int min, int max, Label label, int index, List<Node> nodes)
        {
            if (index > max) { }
            else
            {
                foreach (var child in node.In(label))
                {
                    if (index >= min && index <= max) nodes.Add(child);

                    NestedForLoopsAux(child, min, max, label, index + 1, nodes);
                }
            }
        }

        static List<Node> In(this Node node, Label label, int min, int max)
        {
            var nodes = new List<Node>();

            if (min == 0) 
            { 
                nodes.Add(node);

                NestedForLoopsAux(node, min, max, label, 1, nodes);
            }
            else NestedForLoopsAux(node, min, max, label, min, nodes);
            
            return nodes;
        }

        static IEnumerable<Node> OfTag(this IEnumerable<Node> nodes, Tag tag)
        { return nodes.Where(node => node.Tags.Contains(tag)); }

        public static void Init()
        {
            var context = new Context();

            context.Nodes.Load();
            context.Tags.Load();
            context.Labels.Load();
            context.Relationships.Load();

            var administrator = new Tag() { Title = "administrator" };

            var Ben = new Node() { Title = "Ben", Tags = { administrator }, Attributes = { new Attribute() { Key = "name", Val = "Ben" } } };
            var Sarah = new Node() { Title = "Sarah", Tags = { administrator }, Attributes = { new Attribute() { Key = "name", Val = "Sarah" } } };
            var Liz = new Node() { Title = "Liz", Tags = { administrator }, Attributes = { new Attribute() { Key = "name", Val = "Liz" } } };
            var Phil = new Node() { Title = "Phil", Tags = { administrator }, Attributes = { new Attribute() { Key = "name", Val = "Phil" } } };

            var group = new Tag() { Title = "group" };

            var Group1 = new Node() { Title = "Group1", Tags = { group }, Attributes = { new Attribute() { Key = "name", Val = "Group1" } } };
            var Group2 = new Node() { Title = "Group2", Tags = { group }, Attributes = { new Attribute() { Key = "name", Val = "Group2" } } };
            var Group3 = new Node() { Title = "Group3", Tags = { group }, Attributes = { new Attribute() { Key = "name", Val = "Group3" } } };
            var Group4 = new Node() { Title = "Group4", Tags = { group }, Attributes = { new Attribute() { Key = "name", Val = "Group4" } } };
            var Group5 = new Node() { Title = "Group5", Tags = { group }, Attributes = { new Attribute() { Key = "name", Val = "Group5" } } };
            var Group6 = new Node() { Title = "Group6", Tags = { group }, Attributes = { new Attribute() { Key = "name", Val = "Group6" } } };
            var Group7 = new Node() { Title = "Group7", Tags = { group }, Attributes = { new Attribute() { Key = "name", Val = "Group7" } } };

            var company = new Tag() { Title = "company" };

            var Acme = new Node() { Title = "Acme", Tags = { company }, Attributes = { new Attribute() { Key = "name", Val = "Acme" } } };
            var Spinoff = new Node() { Title = "Spinoff", Tags = { company }, Attributes = { new Attribute() { Key = "name", Val = "Spinoff" } } };
            var Startup = new Node() { Title = "Startup", Tags = { company }, Attributes = { new Attribute() { Key = "name", Val = "Startup" } } };
            var Skunkworkz = new Node() { Title = "Skunkworkz", Tags = { company }, Attributes = { new Attribute() { Key = "name", Val = "Skunkworkz" } } };
            var BigCo = new Node() { Title = "BigCo", Tags = { company }, Attributes = { new Attribute() { Key = "name", Val = "BigCo" } } };
            var Aquired = new Node() { Title = "Aquired", Tags = { company }, Attributes = { new Attribute() { Key = "name", Val = "Aquired" } } };
            var Subsidry = new Node() { Title = "Subsidry", Tags = { company }, Attributes = { new Attribute() { Key = "name", Val = "Subsidry" } } };
            var DevShop = new Node() { Title = "DevShop", Tags = { company }, Attributes = { new Attribute() { Key = "name", Val = "DevShop" } } };
            var OneManShop = new Node() { Title = "OneManShop", Tags = { company }, Attributes = { new Attribute() { Key = "name", Val = "OneManShop" } } };

            var employee = new Tag() { Title = "employee" };

            var Arnold = new Node() { Title = "Arnold", Tags = { employee }, Attributes = { new Attribute() { Key = "name", Val = "Arnold" } } };
            var Charlie = new Node() { Title = "Charlie", Tags = { employee }, Attributes = { new Attribute() { Key = "name", Val = "Charlie" } } };
            var Emily = new Node() { Title = "Emily", Tags = { employee }, Attributes = { new Attribute() { Key = "name", Val = "Emily" } } };
            var Gordon = new Node() { Title = "Gordon", Tags = { employee }, Attributes = { new Attribute() { Key = "name", Val = "Gordon" } } };
            var Lucy = new Node() { Title = "Lucy", Tags = { employee }, Attributes = { new Attribute() { Key = "name", Val = "Lucy" } } };
            var Kate = new Node() { Title = "Kate", Tags = { employee }, Attributes = { new Attribute() { Key = "name", Val = "Kate" } } };
            var Alister = new Node() { Title = "Alister", Tags = { employee }, Attributes = { new Attribute() { Key = "name", Val = "Alister" } } };
            var Eve = new Node() { Title = "Eve", Tags = { employee }, Attributes = { new Attribute() { Key = "name", Val = "Eve" } } };
            var Gary = new Node() { Title = "Gary", Tags = { employee }, Attributes = { new Attribute() { Key = "name", Val = "Gary" } } };
            var Bill = new Node() { Title = "Bill", Tags = { employee }, Attributes = { new Attribute() { Key = "name", Val = "Bill" } } };
            var Mary = new Node() { Title = "Mary", Tags = { employee }, Attributes = { new Attribute() { Key = "name", Val = "Mary" } } };

            var account = new Tag() { Title = "account" };

            var account1 = new Node() { Title = "account1", Tags = { account }, Attributes = { new Attribute() { Key = "name", Val = "Acct 1" } } };
            var account2 = new Node() { Title = "account2", Tags = { account }, Attributes = { new Attribute() { Key = "name", Val = "Acct 2" } } };
            var account3 = new Node() { Title = "account3", Tags = { account }, Attributes = { new Attribute() { Key = "name", Val = "Acct 3" } } };
            var account4 = new Node() { Title = "account4", Tags = { account }, Attributes = { new Attribute() { Key = "name", Val = "Acct 4" } } };
            var account5 = new Node() { Title = "account5", Tags = { account }, Attributes = { new Attribute() { Key = "name", Val = "Acct 5" } } };
            var account6 = new Node() { Title = "account6", Tags = { account }, Attributes = { new Attribute() { Key = "name", Val = "Acct 6" } } };
            var account7 = new Node() { Title = "account7", Tags = { account }, Attributes = { new Attribute() { Key = "name", Val = "Acct 7" } } };
            var account8 = new Node() { Title = "account8", Tags = { account }, Attributes = { new Attribute() { Key = "name", Val = "Acct 8" } } };
            var account9 = new Node() { Title = "account9", Tags = { account }, Attributes = { new Attribute() { Key = "name", Val = "Acct 9" } } };
            var account10 = new Node() { Title = "account10", Tags = { account }, Attributes = { new Attribute() { Key = "name", Val = "Acct 10" } } };
            var account11 = new Node() { Title = "account11", Tags = { account }, Attributes = { new Attribute() { Key = "name", Val = "Acct 11" } } };
            var account12 = new Node() { Title = "account12", Tags = { account }, Attributes = { new Attribute() { Key = "name", Val = "Acct 12" } } };

            var MEMBER_OF = new Label() { Title = "MEMBER_OF" };

            context.Relationships.Local.Add(new Relationship() { A = Ben, Label = MEMBER_OF, B = Group1 });
            context.Relationships.Local.Add(new Relationship() { A = Ben, Label = MEMBER_OF, B = Group3 });
            context.Relationships.Local.Add(new Relationship() { A = Sarah, Label = MEMBER_OF, B = Group2 });
            context.Relationships.Local.Add(new Relationship() { A = Sarah, Label = MEMBER_OF, B = Group3 });
            context.Relationships.Local.Add(new Relationship() { A = Liz, Label = MEMBER_OF, B = Group4 });
            context.Relationships.Local.Add(new Relationship() { A = Liz, Label = MEMBER_OF, B = Group5 });
            context.Relationships.Local.Add(new Relationship() { A = Liz, Label = MEMBER_OF, B = Group6 });
            context.Relationships.Local.Add(new Relationship() { A = Phil, Label = MEMBER_OF, B = Group7 });

            var ALLOWED_INHERIT = new Label() { Title = "ALLOWED_INHERIT" };

            context.Relationships.Local.Add(new Relationship() { A = Group1, Label = ALLOWED_INHERIT, B = Acme });

            var ALLOWED_DO_NOT_INHERIT = new Label() { Title = "ALLOWED_DO_NOT_INHERIT" };

            context.Relationships.Local.Add(new Relationship() { A = Group2, Label = ALLOWED_DO_NOT_INHERIT, B = Acme });

            var DENIED = new Label() { Title = "DENIED" };

            context.Relationships.Local.Add(new Relationship() { A = Group2, Label = DENIED, B = Skunkworkz });
            context.Relationships.Local.Add(new Relationship() { A = Group3, Label = ALLOWED_INHERIT, B = Startup });
            context.Relationships.Local.Add(new Relationship() { A = Group4, Label = ALLOWED_INHERIT, B = BigCo });
            context.Relationships.Local.Add(new Relationship() { A = Group5, Label = DENIED, B = Aquired });
            context.Relationships.Local.Add(new Relationship() { A = Group6, Label = ALLOWED_DO_NOT_INHERIT, B = OneManShop });
            context.Relationships.Local.Add(new Relationship() { A = Group7, Label = ALLOWED_INHERIT, B = Subsidry });

            var CHILD_OF = new Label() { Title = "CHILD_OF" };

            context.Relationships.Local.Add(new Relationship() { A = Spinoff, Label = CHILD_OF, B = Acme });
            context.Relationships.Local.Add(new Relationship() { A = Skunkworkz, Label = CHILD_OF, B = Startup });
            context.Relationships.Local.Add(new Relationship() { A = Aquired, Label = CHILD_OF, B = BigCo });
            context.Relationships.Local.Add(new Relationship() { A = Subsidry, Label = CHILD_OF, B = Aquired });
            context.Relationships.Local.Add(new Relationship() { A = DevShop, Label = CHILD_OF, B = Subsidry });
            context.Relationships.Local.Add(new Relationship() { A = OneManShop, Label = CHILD_OF, B = Subsidry });

            var WORKS_FOR = new Label() { Title = "WORKS_FOR" };

            context.Relationships.Local.Add(new Relationship() { A = Arnold, Label = WORKS_FOR, B = Acme });
            context.Relationships.Local.Add(new Relationship() { A = Charlie, Label = WORKS_FOR, B = Acme });
            context.Relationships.Local.Add(new Relationship() { A = Emily, Label = WORKS_FOR, B = Spinoff });
            context.Relationships.Local.Add(new Relationship() { A = Gordon, Label = WORKS_FOR, B = Startup });
            context.Relationships.Local.Add(new Relationship() { A = Lucy, Label = WORKS_FOR, B = Startup });
            context.Relationships.Local.Add(new Relationship() { A = Kate, Label = WORKS_FOR, B = Skunkworkz });
            context.Relationships.Local.Add(new Relationship() { A = Alister, Label = WORKS_FOR, B = BigCo });
            context.Relationships.Local.Add(new Relationship() { A = Eve, Label = WORKS_FOR, B = Aquired });
            context.Relationships.Local.Add(new Relationship() { A = Gary, Label = WORKS_FOR, B = Subsidry });
            context.Relationships.Local.Add(new Relationship() { A = Bill, Label = WORKS_FOR, B = OneManShop });
            context.Relationships.Local.Add(new Relationship() { A = Mary, Label = WORKS_FOR, B = DevShop });

            var HAS_ACCOUNT = new Label() { Title = "HAS_ACCOUNT" };

            context.Relationships.Local.Add(new Relationship() { A = Arnold, Label = HAS_ACCOUNT, B = account1 });
            context.Relationships.Local.Add(new Relationship() { A = Arnold, Label = HAS_ACCOUNT, B = account2 });
            context.Relationships.Local.Add(new Relationship() { A = Charlie, Label = HAS_ACCOUNT, B = account3 });
            context.Relationships.Local.Add(new Relationship() { A = Emily, Label = HAS_ACCOUNT, B = account6 });
            context.Relationships.Local.Add(new Relationship() { A = Gordon, Label = HAS_ACCOUNT, B = account4 });
            context.Relationships.Local.Add(new Relationship() { A = Lucy, Label = HAS_ACCOUNT, B = account5 });
            context.Relationships.Local.Add(new Relationship() { A = Kate, Label = HAS_ACCOUNT, B = account7 });
            context.Relationships.Local.Add(new Relationship() { A = Alister, Label = HAS_ACCOUNT, B = account8 });
            context.Relationships.Local.Add(new Relationship() { A = Eve, Label = HAS_ACCOUNT, B = account9 });
            context.Relationships.Local.Add(new Relationship() { A = Gary, Label = HAS_ACCOUNT, B = account11 });
            context.Relationships.Local.Add(new Relationship() { A = Bill, Label = HAS_ACCOUNT, B = account10 });
            context.Relationships.Local.Add(new Relationship() { A = Mary, Label = HAS_ACCOUNT, B = account12 });

            context.SaveChanges();

            {
                Console.WriteLine("{0, -10} {1, -10} {2, -10} {3, -10} {4, -10} {5, -10}", "Admin", "Group", "Parent", "Child", "Employee", "Account");
                Console.WriteLine("{0, -10} {1, -10} {2, -10} {3, -10} {4, -10} {5, -10}", "-----", "-----", "------", "-----", "--------", "-------");

                var admin = Ben;

                foreach (var grp in admin.Out(MEMBER_OF))
                    foreach (var parent in grp.Out(ALLOWED_INHERIT).OfTag(company))
                        foreach (var child in parent.In(CHILD_OF, 0, 3).OfTag(company))
                            foreach (var emp in child.In(WORKS_FOR))
                                foreach (var acc in emp.Out(HAS_ACCOUNT))
                                    Console.WriteLine("{0, -10} {1, -10} {2, -10} {3, -10} {4, -10} {5, -10}",
                                        admin.Title, grp.Title, parent.Title, child.Title, emp.Title, acc.Title);
            }

            Console.WriteLine();

            {
                Console.WriteLine("{0, -10} {1, -10} {2, -10} {3, -10} {4, -10} {5, -10}", "Admin", "Group", "Parent", "Child", "Employee", "Account");
                Console.WriteLine("{0, -10} {1, -10} {2, -10} {3, -10} {4, -10} {5, -10}", "-----", "-----", "------", "-----", "--------", "-------");

                var admin = Sarah;

                foreach (var grp in admin.Out(MEMBER_OF))
                    foreach (var parent in grp.Out(ALLOWED_DO_NOT_INHERIT).OfTag(company))
                        foreach (var child in parent.In(CHILD_OF, 1, 3).OfTag(company))
                            foreach (var emp in child.In(WORKS_FOR).OfTag(employee))
                                foreach (var acc in emp.Out(HAS_ACCOUNT))
                                    Console.WriteLine("{0, -10} {1, -10} {2, -10} {3, -10} {4, -10} {5, -10}",
                                        admin.Title, grp.Title, parent.Title, child.Title, emp.Title, acc.Title);
            }





            
        }
    }

}

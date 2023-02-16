using System.Collections.Generic;
using System.Linq;
using System.Xml;

internal sealed class Methods
{
    private class User
    {
        public int Id { get; }
        public int HeadId { get; }
        public string Post { get; }
        public string Name { get; }
        public XmlElement Element { get; }
        
        public User(int id, int headId, string post, string name, XmlDocument document)
        {
            Id = id;
            HeadId = headId;
            Post = post;
            Name = name;
            Element = CreateElement(document);
        }

        public bool IsBoss()
        {
            return Id == HeadId;
        }
        
        private XmlElement CreateElement(XmlDocument document)
        {
            var element = document.CreateElement("person");

            var id = document.CreateAttribute("id");
            id.Value = Id.ToString();
            element.Attributes.Append(id);
            
            var name = document.CreateAttribute("name");
            name.Value = Name;
            element.Attributes.Append(name);
            
            var post = document.CreateAttribute("position");
            post.Value = Post;
            element.Attributes.Append(post);

            return element;
        }
    }
    
    private static User GetPersonByID(List<User> people, int id)
    {
        return people.First(x => x.Id == id);
    }

    private static List<User> GetPeopleList(List<string> people, XmlDocument document)
    {
        var validPeople = new List<User>();
        foreach (var person in people)
        {
            string[] split = person.Split("\t");
            validPeople.Add(
                new User(
                    int.Parse(split[0]), 
                    int.Parse(split[1]), 
                    split[2], 
                    split[3], 
                    document));
        }

        return validPeople;
    }

    private static (XmlDocument, XmlElement, XmlAttribute) PrepareDocument()
    {
        var document = new XmlDocument();
        document.InsertBefore(
            document.CreateXmlDeclaration("1.0", "UTF-8", null),
            document.DocumentElement);

        var company = document.CreateElement("company");
        var name = document.CreateAttribute("name");
        return (document, company, name);
    }
    
    public static XmlDocument GetDocument(string companyString, List<string> people)
    {
        var (document, company, name) = PrepareDocument();
        name.Value = companyString;
        company.Attributes.Append(name);
        document.AppendChild(company);
        
        var validPeople = GetPeopleList(people, document);
        company.AppendChild(validPeople.First().Element);
        
        foreach (var person in validPeople)
        {
            if (person.IsBoss())
            {
                company.AppendChild(person.Element);
            }
            else
            {
                var neededPerson = GetPersonByID(validPeople, person.HeadId);
                neededPerson.Element.AppendChild(person.Element);
            }
        }

        return document;    
    }
}
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace LinqXmlContactsBLL
{
    public class LinqXmlContactsBLManager
    {
        public List<Contact> GetAllContacts()
        {
            XDocument xmlDocument = XDocument.Load("Contacts.xml");

            IEnumerable<XElement> xmlContacts = xmlDocument.Element("Contacts")
                .Elements("Contact");

            List<Contact> contacts = new List<Contact>();

            //fill the list of contacts
            foreach (XElement i in xmlContacts)
            {
                Contact contact = new Contact();

                contact.Id = int.Parse(i.Attribute("Id").Value);
                contact.FirstName = i.Element("FirstName").Value;
                contact.LastName = i.Element("LastName").Value;
                contact.Company = i.Element("Company").Value;
                contact.City = i.Element("City").Value;
                contact.Telephone = i.Element("Telephone").Value;
                contact.Fax = i.Element("Fax").Value;
                contact.Email = i.Element("Email").Value;

                contacts.Add(contact);
            }

            return contacts;
        }

        public Contact GetContactById(int id)
        {
            XDocument xmlDocument = XDocument.Load("Contacts.xml");

            XElement xmlContact = xmlDocument.Element("Contacts").Elements("Contact")
                .Where(a => a.Attribute("Id").Value == id.ToString()).FirstOrDefault();

            Contact contact = new Contact();

            //insert values to the contact
            contact.Id = int.Parse(xmlContact.Attribute("Id").Value);
            contact.FirstName = xmlContact.Element("FirstName").Value;
            contact.LastName = xmlContact.Element("LastName").Value;
            contact.Company = xmlContact.Element("Company").Value;
            contact.City = xmlContact.Element("City").Value;
            contact.Telephone = xmlContact.Element("Telephone").Value;
            contact.Fax = xmlContact.Element("Fax").Value;
            contact.Email = xmlContact.Element("Email").Value;

            return contact;
        }

        public void AddContact(Contact contact)
        {
            XDocument xmlDocument = XDocument.Load("Contacts.xml");

            //add the contact
            xmlDocument.Element("Contacts").Add(
                new XElement("Contact",
                    new XAttribute("Id", contact.Id),
                    new XElement("FirstName", contact.FirstName),
                    new XElement("LastName", contact.LastName),
                    new XElement("Company", contact.Company),
                    new XElement("City", contact.City),
                    new XElement("Telephone", contact.Telephone),
                    new XElement("Fax", contact.Fax),
                    new XElement("Email", contact.Email)
                    ));

            xmlDocument.Save("Contacts.xml");
        }

        public void UpdateContact(Contact contact)
        {
            XDocument xmlDocument = XDocument.Load("Contacts.xml");

            //get the contact to be updated by his id
            XElement xmlContact = xmlDocument.Element("Contacts").Elements("Contact")
                .Where(x => x.Attribute("Id").Value == contact.Id.ToString())
                .FirstOrDefault();

            //update the contact
            xmlContact.SetElementValue("FirstName", contact.FirstName);
            xmlContact.SetElementValue("LastName", contact.LastName);
            xmlContact.SetElementValue("Company", contact.Company);
            xmlContact.SetElementValue("City", contact.City);
            xmlContact.SetElementValue("Telephone", contact.Telephone);
            xmlContact.SetElementValue("Fax", contact.Fax);
            xmlContact.SetElementValue("Email", contact.Email);

            xmlDocument.Save("Contacts.xml");
        }

        public void DeleteContact(int id)
        {
            XDocument xmlDocument = XDocument.Load("Contacts.xml");

            //get the contact and delete him
            xmlDocument.Root.Elements().Where(x => x.Attribute("Id").Value == id.ToString())
                .FirstOrDefault().Remove();

            xmlDocument.Save("Contacts.xml");
        }

        public int GetNextId()
        {
            int currentId;
            XDocument xmlDocument = XDocument.Load("Contacts.xml");

            //get the collection of id's
            IEnumerable<int> contactIds = xmlDocument.Element("Contacts").Elements("Contact")
                .Select(x => int.Parse(x.Attribute("Id").Value));

            //if the collection is empty, set the variable to zero
            //else, set the variable to the highest id number
            if (contactIds.Count() == 0)
                currentId = 0;
            else
                currentId = contactIds.Max();

            return currentId + 1;
        }

        public void SetXmlFile()
        {
            //if the xml file is missing, create it with the root element
            if (!(File.Exists("Contacts.xml")))
            {
                XDocument xmlDocument = new XDocument(
                    new XDeclaration("1.0", "utf-8", "yes"),
                    new XElement("Contacts"));

                xmlDocument.Save("Contacts.xml");
            }
        }

        public bool IdExists(int id)
        {
            XDocument xmlDocument = XDocument.Load("Contacts.xml");

            return xmlDocument.Element("Contacts").Elements("Contact")
                .Select(x => int.Parse(x.Attribute("Id").Value))
                .Where(y => y == id).Count() > 0;
        }
    }
}

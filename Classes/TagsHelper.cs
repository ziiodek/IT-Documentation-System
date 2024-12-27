using System.Diagnostics;
using System.Reflection.PortableExecutable;
using System.Text.Json.Nodes;

namespace ITDocumentation.Classes
{
    public class TagsHelper
    {
        JsonReader reader = new JsonReader();
        string getDocumentTags(int documentID)
        {
            string tagString = "";
            JsonNode documentTags = reader.getTags("DocumentTags.json");
            JsonObject jsonObj = documentTags.AsObject();
            if (jsonObj.ContainsKey(documentID.ToString()))
            {
                if (documentTags[documentID.ToString()]!.AsArray().Count > 0)
                {

                    foreach (var tagName in documentTags[documentID.ToString()]!.AsArray())
                    {
                        tagString += tagName!.ToString() + ",";
                    }
                }
            }

            return tagString;
        }

        public string getContentTags(int ID, string parent)
        {
            Console.WriteLine(parent);
            Console.WriteLine(ID.ToString());
            string tagString = "";
            JsonNode documentTags = reader.getTags("ContentTags.json");
            JsonObject jsonObj = documentTags.AsObject();
            if (jsonObj.ContainsKey(ID.ToString()))
            {
                

                   
                    if (documentTags[ID.ToString()]["Type"].ToString() == parent)
                    {

                        foreach (var tagName in documentTags[ID.ToString()]["Tags"].AsArray())
                        {
                           
                            tagString += tagName!.ToString() + ",";
                        }
                    }
                
            }
            return tagString;
        }


    }
}

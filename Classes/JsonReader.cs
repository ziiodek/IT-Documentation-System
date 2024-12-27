using System;
using ITDocumentation.Data;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.IO;
using System.Reflection.Metadata;
using System.Reflection.PortableExecutable;
using Azure;
using Microsoft.Extensions.Options;

namespace ITDocumentation
{

    public class JsonReader
    {

        public JsonNode getTags(string jsonFileName)
        {
            string directory = Directory.GetCurrentDirectory() + "/Json/";
            string filePath = directory + jsonFileName;
            string jsonString = ReadJSONFile(filePath);
            JsonNode documentsList = JsonNode.Parse(jsonString);

            return documentsList;
        }

        public void DeleteJsonTagNode(int ID, string type, string jsonFileName)
        {
            JsonNode tags = getTags(jsonFileName);
            JsonObject jsonObj = tags.AsObject();

            if (type == "Document") {
                if (jsonObj.ContainsKey(ID.ToString()))
                {
                   
                    tags.AsObject().Remove(ID.ToString());
                    writeJsonFile(tags, jsonFileName);

                }
            }

            if (jsonObj.ContainsKey(ID.ToString()))
            {
                JsonNode tagItem = tags[ID.ToString()];
                if (tagItem["Type"].ToString() == type)
                {

                    tags.AsObject().Remove(ID.ToString());
                    writeJsonFile(tags,jsonFileName);
                }

            }

        }

        void writeJsonFile(JsonNode json,string jsonFileName) {
            string directory = Directory.GetCurrentDirectory() + "/Json/";
            string filePath = directory + jsonFileName;
            var options = new JsonSerializerOptions() { WriteIndented = true };
            var newJson = json.ToJsonString(options);
            File.WriteAllText(filePath, newJson);

        }

        public List<string> DeleteDocumentTag(string tag, int ID,string jsonFileName) {
            JsonNode tags = getTags(jsonFileName);
            JsonObject jsonObj = tags.AsObject();
            if (jsonObj.ContainsKey(ID.ToString()))
            {
                    List<string> currentTags = new List<string>();
                    if (tags[ID.ToString()].AsArray().Count > 1)
                    {

                        foreach (var tagName in tags[ID.ToString()].AsArray())
                        {
                            if (tag != tagName.ToString())
                            {

                                currentTags.Add(tagName.ToString());
                            }

                        }
                    }

                    return currentTags;

                

            }

            return null;
        }
        public List<string> DeleteTag(string tag, int ID, string type, string jsonFileName)
        {

            if (type == "Document") {
                return DeleteDocumentTag(tag, ID, jsonFileName);
            }

            JsonNode tags = getTags(jsonFileName);
            JsonObject jsonObj = tags.AsObject();
            if (jsonObj.ContainsKey(ID.ToString()))
            {
                JsonNode tagItem = tags[ID.ToString()];
                if (tagItem["Type"].ToString() == type)
                {

                    List<string> currentTags = new List<string>();
                    JsonArray items = tagItem["Tags"].AsArray();
                    if (tagItem["Tags"].AsArray().Count > 1)
                    {

                        foreach (var tagName in tagItem["Tags"].AsArray())
                        {
                            if (tag != tagName.ToString())
                            {
                                
                                currentTags.Add(tagName.ToString());
                            }

                        }
                    }

                    return currentTags;

                }

            }

            return null;

        }

        public void StoreDocumentTags(DocumentTags documentTags, string jsonFileName)
        {
            string directory = Directory.GetCurrentDirectory() + "/Json/";
            string filePath = directory + jsonFileName;
            string jsonString = ReadJSONFile(filePath);
            JsonNode documentTagsList = JsonNode.Parse(jsonString);
            JsonArray tagList = new JsonArray();
            foreach (var tag in documentTags.Tags)
            {
                tagList.Add(tag);
                Console.WriteLine(tag);
            }

            documentTagsList[documentTags.ID.ToString()] = tagList;
            var options = new JsonSerializerOptions() { WriteIndented = true };
            var newJson = documentTagsList.ToJsonString(options);
            File.WriteAllText(filePath, newJson);
        }



        public void StoreContentTags(ContentTags contentTags, string jsonFileName)
        {
            string directory = Directory.GetCurrentDirectory() + "/Json/";
            string filePath = directory + jsonFileName;
            string jsonString = ReadJSONFile(filePath);
            JsonNode contentTagsList = JsonNode.Parse(jsonString);
            JsonArray tagList = new JsonArray();
            foreach (var tag in contentTags.Tags)
            {
                tagList.Add(tag);
            }



            var node = new JsonObject()
            {
                ["Type"] = contentTags.Type.ToString(),
                ["Tags"] = tagList

            };


            contentTagsList[contentTags.ID.ToString()] = node;
            var options = new JsonSerializerOptions() { WriteIndented = true };
            var newJson = contentTagsList.ToJsonString(options);
            File.WriteAllText(filePath, newJson);

        }

        public List<string> GetContentTags(int ID, string type, string jsonFileName)
        {
            JsonNode tags = getTags(jsonFileName);
            JsonObject jsonObj = tags.AsObject();
            if (jsonObj.ContainsKey(ID.ToString()))
            {
                JsonNode tagItem = tags[ID.ToString()];
                if (tagItem["Type"].ToString() == type)
                {

                    List<string> currentTags = new List<string>();
                    foreach (var tagName in tagItem["Tags"].AsArray())
                    {
                        currentTags.Add(tagName.ToString());
                    }
                    return currentTags;
                }

            }

            return null;
        }


        public string ReadJSONFile(string jsonFileName)
        {
            string jsonString = "";
            foreach (string line in File.ReadLines(jsonFileName))
            {
                jsonString += line;
            }

            return jsonString;
        }


    }
}
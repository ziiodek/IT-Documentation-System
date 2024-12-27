
using System;
using System.Linq;
using System.Reflection.PortableExecutable;
using ITDocumentation.Data;

namespace ITDocumentation
{
    public class SinglePagesHelper
    {
        ApplicationDbContext dbContext;
        JsonReader reader;

        public SinglePagesHelper(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void deleteSinglePage__menuItems(int singlePageID)
        {
            if (dbContext.MenuItem.Any(m => m.PageID == singlePageID && m.Type == "singlePage"))
            {
                MenuItem menuItem = dbContext.MenuItem.First(m => m.PageID == singlePageID && m.Type == "singlePage");
                dbContext.MenuItem.Remove(menuItem);
                dbContext.SaveChanges();
            }
        }


        public void deleteProject__menuItems(int projectID)
        {
            if (dbContext.MenuItem.Any(m => m.PageID == projectID && m.Type == "project"))
            {
                MenuItem menuItem = dbContext.MenuItem.First(m => m.PageID == projectID && m.Type == "project");
                dbContext.MenuItem.Remove(menuItem);
                dbContext.SaveChanges();
            }
        }

        public void DeleteJsonTagsNode(int ID, string type)
        {
            reader = new JsonReader();
            reader.DeleteJsonTagNode(ID, type, "ContentTags.json");
        }

        public void storeTags(int ID, string type, List<string> tags, string jsonFileName)
        {
            reader = new JsonReader();
            List<string> newTagList;


            if (tags != null && tags.Count > 0)
            {

                List<string> storedTags = reader.GetContentTags(ID, type, jsonFileName);


                ContentTags contentTag = new ContentTags();
                contentTag.ID = ID;
                contentTag.Type = type;

                if (storedTags != null && storedTags.Count > 0)
                {
                    newTagList = storedTags;
                    foreach (string tag in tags)
                    {
                        if (!storedTags.Contains(tag))
                        {
                            newTagList.Add(tag);
                        }

                    }
                    contentTag.Tags = newTagList;
                }
                else
                {
                    contentTag.Tags = tags;
                }


                reader.StoreContentTags(contentTag, jsonFileName);
            }

        }

        public void StoreNewTagList(int ID, string type, List<string> tags, string jsonFileName)
        {
            reader = new JsonReader();
            if (tags != null && tags.Count > 0)
            {

                if (type == "Document")
                {
                    DocumentTags documentTags = new DocumentTags();
                    documentTags.ID = ID;
                    documentTags.Tags = tags;
                    reader.StoreDocumentTags(documentTags, jsonFileName);

                }
                else
                {

                    ContentTags contentTags = new ContentTags();
                    contentTags.ID = ID;
                    contentTags.Type = type;
                    contentTags.Tags = tags;
                    reader.StoreContentTags(contentTags, jsonFileName);
                }
            }
            else
            {
                List<string> emptyList = new List<string>();

                if (type == "Document")
                {
                    DocumentTags documentTags = new DocumentTags();
                    documentTags.ID = ID;
                    documentTags.Tags = tags;
                    reader.StoreDocumentTags(documentTags, jsonFileName);

                }
                else
                {

                    ContentTags contentTags = new ContentTags();
                    contentTags.ID = ID;
                    contentTags.Type = type;
                    contentTags.Tags = emptyList;
                    reader.StoreContentTags(contentTags, jsonFileName);
                }

            }


        }


        public void DeleteTags(string tag, int ID, string type, string jsonFileName)
        {
            reader = new JsonReader();
            StoreNewTagList(ID, type, reader.DeleteTag(tag, ID, type, jsonFileName), jsonFileName);

        }


    }
}



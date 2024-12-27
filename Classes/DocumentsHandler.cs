using System;
using ITDocumentation.Data;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;



namespace ITDocumentation
{
    public class DocumentsHandler
    {

        ApplicationDbContext dbContext;
        string? directoryURL;
        string? tmpDirectoryURL;

        string? userName;
        string? authorName;
        List<string>? tags;
        List<string>? additionalTags;
        JsonReader reader;


        public DocumentsHandler(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
            directoryURL = Directory.GetCurrentDirectory() + "/wwwroot/DocumentsUploaded";
            tmpDirectoryURL = Directory.GetCurrentDirectory() + "/wwwroot/DocumentsUploaded" + "/tmp";
            reader = new JsonReader();
        }

        public void setTagsList(List<string> tags)
        {
            this.tags = tags;
        }

        public void setAddTagList(List<string> additionalTags)
        {
            this.additionalTags = additionalTags;
        }


        public string getTmpDirectoryURL()
        {
            return tmpDirectoryURL!;
        }

        public string getDirectoryURL()
        {
            return directoryURL!;
        }

        public string setDirectoryURL(string parent, int pageID)
        {
            return directoryURL + parent + "/" + pageID;
        }

        public void moveDirectory(string sourcePath, string destinationPath)
        {
            if (Directory.Exists(sourcePath))
            {
                if (sourcePath != destinationPath)
                {

                    Directory.Move(sourcePath, destinationPath);
                }
            }

        }

        public void deleteDocuments__files(List<Document> documents, string directoryURL)
        {
            foreach (var document in documents)
            {
                string filePath = directoryURL + document.Name;

                try
                {
                    if (File.Exists(filePath))
                    {
                        System.IO.File.Delete(filePath);
                    }

                }
                catch (Exception e)
                {
                    Console.Write(e);
                }
            }
        }

        public void deletePendingDocuments__files(List<PendingDocument> pendingDocuments, string directoryURL)
        {
            foreach (var pendingDocument in pendingDocuments)
            {
                string filePath = directoryURL + pendingDocument.Name;
                try
                {
                    if (File.Exists(filePath))
                    {
                        System.IO.File.Delete(filePath);
                    }
                }
                catch (Exception e)
                {
                    Console.Write(e);
                }
            }
        }


        public void updateDocumentTags(int documentID) {
            Console.WriteLine("Test "+documentID);
            DocumentTags documentTags = new DocumentTags();
            documentTags.ID = documentID;
            documentTags.Tags = tags;
            reader.StoreDocumentTags(documentTags, "DocumentTags.json");

        }

        public bool saveDocuments(Document document, string state, string parent)
        {
           
                if (state == "create")
                {
                    if (parent == "singlePage")
                    {
                        SinglePage singlePage = dbContext.SinglePage.OrderByDescending(p => p.ID).FirstOrDefault()!;
                        document.PageID = singlePage.ID;
                    }
                    else if (parent == "projectPage")
                    {
                        ProjectPage projectPage = dbContext.ProjectPage.OrderByDescending(p => p.ID).FirstOrDefault()!;
                        document.PageID = projectPage.ID;
                    }
                }

                if (dbContext.Document.Any(d => d.Name == document.Name && d.Parent == document.Parent && d.PageID == document.PageID && d.IsArchive == false) == false)
                {
                    dbContext.Document.Add(document);
                    dbContext.SaveChanges();


                    return false;
                   
                }
                
                    return true;    
                
                

        }



        public bool directoryExists(string directoryPath)
        {
            if (Directory.Exists(directoryPath))
            {
                return true;
            }

            return false;
        }


        public void savePendingDocuments(List<PendingDocument> pendingDocuments, string state, string parent)
        {
            foreach (var pendingDocument in pendingDocuments)
            {
                if (state == "create")
                {
                    if (parent == "singlePage")
                    {
                        SinglePage singlePage = dbContext.SinglePage.OrderByDescending(p => p.ID).FirstOrDefault()!;
                        pendingDocument.PageID = singlePage.ID!;
                    }
                    else if (parent == "project")
                    {
                        ProjectPage projectPage = dbContext.ProjectPage.OrderByDescending(p => p.ID).FirstOrDefault()!;
                        pendingDocument.PageID = projectPage.ID!;
                    }
                }

                dbContext.PendingDocument.Add(pendingDocument);
                dbContext.SaveChanges();
                PendingDocument lastInserted = dbContext.PendingDocument.OrderByDescending(d => d.ID).FirstOrDefault()!;
                DocumentTags documentTags = new DocumentTags();
                documentTags.ID = lastInserted.ID;
                documentTags.Tags = tags;
        
                reader.StoreDocumentTags(documentTags, "DocumentTags.json");



                if (getDepartmentEditor() != null)
                {
                    string userEditor = getDepartmentEditor();
                    PendingDocument tmpPendingDoc = dbContext.PendingDocument.OrderByDescending(d => d.ID).FirstOrDefault()!;

                    DocumentApproval documentApproval = new DocumentApproval();
                    documentApproval.RequestedBy = pendingDocument.AuthorName;
                    documentApproval.DateTime = pendingDocument.DateTime;
                    documentApproval.DocumentID = tmpPendingDoc!.ID;
                    dbContext.DocumentApproval.Add(documentApproval);
                    dbContext.SaveChanges();
                    int counter = 0;

                }

            }

        }

        public void setUsername(string userName)
        {
            this.userName = userName;
        }

        public void setAuthorname(string authorName)
        {
            this.authorName = authorName;
        }
        public string getDepartmentEditor()
        {
            UserSubdepartment currentUser = dbContext.UserSubdepartment.First(u => u.Username == userName);
            IList<UserRole> userRoleList = dbContext.UserRole.Where(u => u.DepartmentID == currentUser.DepartmentID && u.RoleName == "EDITOR").ToList();

            foreach (var userRole in userRoleList)
            {
                UserSubdepartment editorUser = dbContext.UserSubdepartment.First(u => u.Username == userRole.Username);
                if (editorUser.SubdepartmentID == currentUser.SubdepartmentID)
                {
                    return userRole.Name!;
                }
            }
            return "";
        }


        public List<Document> initDocuments(List<Document> documents, string parent, int pageID)
        {
            IList<Document> documentsList = dbContext.Document.Where(d => d.Parent == parent && d.IsArchive == false).ToList();
            documents = new List<Document>();
            foreach (var document in documentsList)
            {
                if (document.PageID == pageID)
                {
                    documents.Add(document);
                }
            }

            return documents;
        }


        public List<PendingDocument> initPendingDocuments(List<PendingDocument> pendingDocuments, string parent, int pageID)
        {
            IList<PendingDocument> pendingDocumentsList = dbContext.PendingDocument.Where(p => p.Parent == parent).ToList();
            pendingDocuments = new List<PendingDocument>();
            foreach (var pendingDocument in pendingDocumentsList)
            {
                if (pendingDocument.PageID == pageID)
                {
                    pendingDocuments.Add(pendingDocument);
                }
            }

            return pendingDocuments;
        }



        public void deleteAllDocuments(string parent, int pageID, string user)
        {
            List<Document> documents = dbContext.Document.Where(d => d.Parent == parent && d.PageID == pageID).ToList();
            directoryURL = directoryURL + parent + "/" + pageID + "/";

            //deleteDocuments__files(documents, directoryURL);
            //deleteDirectory();

            foreach (var document in documents)
            {
               
                document.IsArchive = true;
                document.ArchiveTime = DateTime.Now;
                document.ArchiveBy = user.ToUpper();
                Document prevDocument = dbContext.Document.First(d => d.ID == document.ID);
                dbContext.Entry(prevDocument).CurrentValues.SetValues(document);
                dbContext.SaveChanges();
                //documentHandler.DeleteJsonTagsNode(document.ID, "Document");
                //reloadDocumentsTable__parentEvent.InvokeAsync();
                //ds.Close(false);
                //dbContext.Document.Remove(document);
            }
            dbContext.SaveChanges();
        }

        public void deleteAllPendingDocuments(string parent, int pageID)
        {
            List<PendingDocument> pendingDocuments = dbContext.PendingDocument.Where(d => d.Parent == parent && d.PageID == pageID).ToList();
            directoryURL = directoryURL + parent + "/" + pageID + "/";

            deletePendingDocuments__files(pendingDocuments, directoryURL);
            deleteDirectory();

            foreach (var pendingDocument in pendingDocuments)
            {
                dbContext.PendingDocument.Remove(pendingDocument);
            }
            dbContext.SaveChanges();
        }

        public void deleteAllDocumentsApprovals(string parent, int pageID)
        {
            List<PendingDocument> pendingDocuments = dbContext.PendingDocument.Where(d => d.Parent == parent && d.PageID == pageID).ToList();
            foreach (var pendingDocument in pendingDocuments)
            {
                DocumentApproval documentApproval = dbContext.DocumentApproval.First(d => d.DocumentID == pendingDocument.ID);
                dbContext.DocumentApproval.Remove(documentApproval);

            }

            dbContext.SaveChanges();

        }
        public void deleteDirectory()
        {
            string directoryPath = directoryURL!;
            Console.Write("\n" + directoryPath + "\n");

            try
            {
                if (Directory.Exists(directoryPath))
                {
                    System.IO.Directory.Delete(directoryPath);
                }

            }
            catch (Exception e)
            {
                Console.Write(e);
            }
        }
        public void deleteProjectPage__documents(ProjectPage page)
        {
            var documents = dbContext.Document.Where(d => d.Parent == "project" && d.PageID == page.ID).ToList();
            foreach (var document in documents)
            {
                dbContext.Document.Remove(document);
                dbContext.SaveChanges();
            }
        }


        public void DeleteJsonTagsNode(int ID, string type)
        {
            reader = new JsonReader();
            reader.DeleteJsonTagNode(ID, type, "DocumentTags.json");
        }

    }
}





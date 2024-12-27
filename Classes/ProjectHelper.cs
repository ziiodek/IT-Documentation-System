using System;
using ITDocumentation.Data;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace ITDocumentation
{
    public class ProjectHelper
    {
        ApplicationDbContext dbContext;
        DocumentsHandler documentsHandler;

        public ProjectHelper(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
            documentsHandler = new DocumentsHandler(dbContext);
        }

        public void deleteProject__pages(int projectID,string user)
        {
            Console.Write("\n" + "Project ID " + projectID + "\n");
            if (dbContext.ProjectPage.Any(p => p.ProjectID == projectID))
            {
                var pages = this.dbContext.ProjectPage.Where(p => p.ProjectID == projectID).ToList();
                foreach (var page in pages)
                {
                    documentsHandler.deleteAllDocuments("projectPage", page.ID,user);
                    //documentsHandler.deleteProjectPage__documents(page);
                    dbContext.ProjectPage.Remove(page);
                    dbContext.SaveChanges();
                }

            }
        }
    }
}
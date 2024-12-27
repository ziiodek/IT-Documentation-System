using System;
using ITDocumentation.Data;

namespace ITDocumentation
{
    public class SubdepartmentHelper
    {
        ApplicationDbContext dbContext;
        DocumentsHandler documentsHandler;


        public SubdepartmentHelper(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
            documentsHandler = new DocumentsHandler(dbContext);
        }

        public void deleteSubdepartment__singlePages(int subdepartmentID,string user)
        {
            var singlePages = dbContext.SinglePage.Where(p => p.SubdepartmentID == subdepartmentID).ToList();
            if (singlePages.Count > 0)
            {
                foreach (var singlePage in singlePages)
                {
                    documentsHandler.deleteAllDocuments("singlePage", singlePage.ID,user);
                    dbContext.SinglePage.Remove(singlePage);
                    dbContext.SaveChanges();
                }
            }
        }

        public void deleteSubdepartment__projects(int subdepartmentID,string user)
        {
            ProjectHelper projectHelper = new ProjectHelper(this.dbContext);
            var projects = dbContext.Project.Where(p => p.SubdepartmentID == subdepartmentID).ToList();
            if (projects.Count > 0)
            {
                foreach (var project in projects)
                {
                    projectHelper.deleteProject__pages(project.ID,user);
                }

            }
        }

        public void deleteSubdepartment__menuItems(int subdepartmentID)
        {
            var singlePages = dbContext.SinglePage.Where(p => p.SubdepartmentID == subdepartmentID).ToList();
            if (singlePages.Count > 0)
            {
                foreach (var singlePage in singlePages)
                {
                    if (dbContext.MenuItem.Any(m => m.PageID == singlePage.ID))
                    {

                        MenuItem menuItem = dbContext.MenuItem.First(m => m.PageID == singlePage.ID);
                        dbContext.MenuItem.Remove(menuItem);
                        dbContext.SaveChanges();
                    }
                }
            }
        }

        public void deleteSubdepartment__users(int subdepartmentID)
        {
            if (dbContext.UserSubdepartment.Any(u => u.SubdepartmentID == subdepartmentID))
            {
                List<UserSubdepartment> userSubdepartments = dbContext.UserSubdepartment.Where(u => u.SubdepartmentID == subdepartmentID).ToList();
                if (userSubdepartments.Count > 0)
                {
                    foreach (var userSub in userSubdepartments)
                    {
                        dbContext.UserSubdepartment.Remove(userSub);
                        dbContext.SaveChanges();
                    }
                }
            }
        }
    }
}
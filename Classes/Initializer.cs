using System;
using ITDocumentation.Data;
using Microsoft.Identity.Client;

namespace ITDocumentation
{
    public class Initializer
    {

        ApplicationDbContext dbContext;
        ActiveDirectoryCon activeDirectory;

        public Initializer(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
            activeDirectory = new ActiveDirectoryCon();
        }


        public void initDepartments()
        {
            List<string> departments = activeDirectory.getAllDepartments();
            if (departments != null)
            {
                foreach (var departmentName in departments)
                {
                    if (dbContext.Department.Any(d => d.Name == departmentName) == false)
                    {
                        Department department = new Department();
                        department.Name = departmentName;
                        department.DateTime = DateTime.Now;
                        department.AuthorName = "System";
                        dbContext.Add(department);
                        dbContext.SaveChanges();
                    }
                }
            }


        }

        public void initMenuPages()
        {
            Subdepartment admin = dbContext.Subdepartment.First(d => d.Name == "Admin");
            List<SinglePage> requiredPages = dbContext.SinglePage.Where(s => s.SubdepartmentID == admin.ID).ToList();
            foreach (var page in requiredPages)
            {

                if (!dbContext.MenuItem.Any(i => i.Name == page.Name))
                {

                    MenuItem item = new MenuItem();
                    item.Name = page.Name;
                    item.PageID = page.ID;
                    item.AuthorName = "System";
                    item.DateTime = DateTime.Now;
                    dbContext.Add(item);
                }

            }
            dbContext.SaveChanges();
        }

        public void createRequiredPages()
        {
            if (dbContext.Subdepartment.Any(d => d.Name == "Admin"))
            {
                Subdepartment admin = dbContext.Subdepartment.First(d => d.Name == "Admin");




                if (!dbContext.SinglePage.Any(s => s.Name == "SYM Master List"))
                {
                    SinglePage symList = new SinglePage();
                    symList.Name = "SYM Master List";
                    symList.AuthorName = "System";
                    symList.SubdepartmentID = admin.ID;
                    symList.DateTime = DateTime.Now;
                    symList.PageContent = "SYM Master List additional content";
                    dbContext.Add(symList);


                }

                if (!dbContext.SinglePage.Any(s => s.Name == "Server List"))
                {
                    SinglePage serverList = new SinglePage();
                    serverList.Name = "Server List";
                    serverList.AuthorName = "System";
                    serverList.SubdepartmentID = admin.ID;
                    serverList.DateTime = DateTime.Now;
                    serverList.PageContent = "Server List additional content";
                    dbContext.Add(serverList);

                }

                if (!dbContext.SinglePage.Any(s => s.Name == "Database List"))
                {
                    SinglePage databaseList = new SinglePage();
                    databaseList.Name = "Database List";
                    databaseList.AuthorName = "System";
                    databaseList.SubdepartmentID = admin.ID;
                    databaseList.DateTime = DateTime.Now;
                    databaseList.PageContent = "Database List additional content";
                    dbContext.Add(databaseList);

                }

                if (!dbContext.SinglePage.Any(s => s.Name == "Manual Procedures"))
                {
                    SinglePage manualProc = new SinglePage();
                    manualProc.Name = "Manual Procedures";
                    manualProc.AuthorName = "System";
                    manualProc.SubdepartmentID = admin.ID;
                    manualProc.DateTime = DateTime.Now;
                    manualProc.PageContent = "Manual Procedures additional content";
                    dbContext.Add(manualProc);

                }

                if (!dbContext.SinglePage.Any(s => s.Name == "Core Values"))
                {
                    SinglePage departmentAbout = new SinglePage();
                    departmentAbout.Name = "Core Values";
                    departmentAbout.AuthorName = "System";
                    departmentAbout.SubdepartmentID = admin.ID;
                    departmentAbout.DateTime = DateTime.Now;
                    departmentAbout.PageContent = "Core Values additional content";
                    dbContext.Add(departmentAbout);

                }

                dbContext.SaveChanges();
            } 

        }

        public void initRoles()
        {
            Role admin = new Role();
            admin.RoleName = "ADMIN";

            Role editor = new Role();
            editor.RoleName = "EDITOR";

            Role regular = new Role();
            regular.RoleName = "REGULAR";

            dbContext.Role.Add(admin);
            dbContext.Role.Add(editor);
            dbContext.Role.Add(regular);
            dbContext.SaveChanges();

        }

        public void initUsers(string departmentName)
        {
            List<User> users = activeDirectory.getAllUsers(departmentName);
            Department department = dbContext.Department.First(d => d.Name == departmentName.ToUpper());
            if (users != null)
            {
                foreach (var user in users)
                {
                    if (dbContext.UserRole.Any(u => u.Username == user.Username) == false)
                    {
                        UserRole userRole = new UserRole();
                        userRole.Username = user.Username ?? "";
                        userRole.Name = user.Name ?? "";
                        userRole.DepartmentID = department.ID;
                        userRole.RoleName = "REGULAR";
                        dbContext.Add(userRole);
                        // TODO: Make sure to update this outside the for loop in another branch -DG
                        dbContext.SaveChanges();
                    }
                }
            }

        }
    }
}